using log4net;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI.WebControls.Expressions;
using TestWeb.Models.Attributes;
using WebGrease;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// サービスプロキシ
    /// </summary>
    public class ServiceProxy<TService> : IServiceProxy<TService>
        where TService : IService
    {
        /// <summary>
        /// DbContext
        /// </summary>
        private DbContext _DbContext;
        /// <summary>
        /// トランザクションコネクション
        /// </summary>
        private DbContextTransaction _Tran;

        /// <summary>
        /// サービス
        /// </summary>
        private TService _Service;

        /// <summary>
        /// トランザクション対象フラグ
        /// </summary>
        private bool _IsTransaction;

        /// <summary>
        /// ロガー
        /// </summary>
        private ILog _Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="dbContext">DbContext</param>
        /// <param name="service">サービス</param>
        public ServiceProxy(DbContext dbContext, TService service)
        {
            this._DbContext = dbContext;
            this._Service = service;
        }

        /// <summary>
        /// サービスメソッド実行
        /// </summary>
        /// <typeparam name="TResult">戻り値の型</typeparam>
        /// <param name="expression">実行するメソッド(および引数)</param>
        /// <param name="userInfoModel">ユーザー情報</param>
        /// <returns>戻り値</returns>
        public TResult InvokeService<TResult>(Expression<Func<TService, TResult>> expression, UserInfoModel userInfoModel)
        {
            TResult result;


            // トランザクション対象であるかチェックする
            var method = (MethodCallExpression)(expression.Body);
            var name = method.Method.Name;

            object[] attributes = method.Method.GetCustomAttributes(typeof(TransactionStart), false);
            if (attributes != null && attributes.Length != 0)
            {
                this._IsTransaction = true;
            } else
            {
                this._IsTransaction = false;
            }

            // サービスにユーザー情報を設定する
            this._Service.UserInfoModel = userInfoModel;
            // SaveChangesファンクションをセットする
            this._Service.SaveChangeFunc = () => this.SaveChangeFunc();
            // メッセージを設定する
            this._Service.ServiceMessage = new ServiceMessage();

            // 開始ログを出力する
            this._Log.Debug(string.Format("method={0} Start. Transaction={1}.", name, this._IsTransaction.ToString()));

            // 実行するサービスのメソッドを取得する
            Func<TService, TResult> serviceMethod = expression.Compile();

            // トランザクション開始
            if (this._IsTransaction)
            {
                using (this._Tran = this._DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // サービスのメソッドを実行する
                        result = serviceMethod.Invoke(this._Service);

                        // コミット
                        this._Tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        // ここではDbUpdateConcurrencyException や DbUpdateExceptionは発生しない想定
                        // 上記２つのExceptionは、SaveChangeFuncメソッド内で処理する

                        _Log.Error(ex);

                        // ロールバック
                        this._Tran.Rollback();
                        throw ex;
                    }
                }
            } else
            {
                // サービスのメソッドを実行する

                result = serviceMethod.Invoke(this._Service);
            }
            // 終了ログを出力する
            this._Log.Debug(string.Format("method={0} End. Transaction={1}.", name, this._IsTransaction.ToString()));


            return result;
        }

        /// <summary>
        /// サービスメッセージ返却
        /// </summary>
        public ServiceMessage ServiceMessage
        {
            get
            {
                return this._Service.ServiceMessage;
            }
        }

        /// <summary>
        /// DB確定用ファンクション
        /// </summary>
        /// <returns>true:成功</returns>
        private bool SaveChangeFunc()
        {
            bool result;
            try
            {
                // DB確定
                int updateCount = this._DbContext.SaveChanges();
                result = true;
            } catch(DbUpdateConcurrencyException ucx)
            {
                result = false;

                // DbContextにトラッキングされている変更を取り消す
                CancelTracking(ucx.Entries);

                // ロールバックする
                if (this._IsTransaction)
                {
                    this._Tran.Rollback();
                }
            } catch(DbEntityValidationException vex)
            {
                result = false;

                // DbContextにトラッキングされている変更を取り消す
                CancelTracking(this._DbContext.ChangeTracker.Entries());

                // ロールバックする
                if (this._IsTransaction)
                {
                    this._Tran.Rollback();
                }
                foreach (var entityError in vex.EntityValidationErrors)
                {
                    foreach (var validError in entityError.ValidationErrors)
                    {
                        this._Log.Error(string.Format("Db Validation Error property={0} message={1}.", validError.PropertyName, validError.ErrorMessage));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// トラッキング取り消し
        /// </summary>
        /// <param name="entries">エントリー</param>
        private void CancelTracking(IEnumerable<DbEntityEntry> entries)
        {
            foreach (DbEntityEntry entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}