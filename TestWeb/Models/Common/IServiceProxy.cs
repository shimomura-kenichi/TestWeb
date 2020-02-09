using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// サービスプロキシインタフェース
    /// </summary>
    public interface IServiceProxy<TService>
        where TService : IService
    {
        /// <summary>
        /// サービスメソッド実行
        /// </summary>
        /// <typeparam name="TResult">戻り値の型</typeparam>
        /// <param name="expression">実行するメソッド(および引数)</param>
        /// <param name="userInfoModel">ユーザー情報</param>
        /// <returns>戻り値</returns>
        TResult InvokeService<TResult>(Expression<Func<TService, TResult>> expression, UserInfoModel userInfoModel);

        /// <summary>
        /// サービスメッセージ返却
        /// </summary>
        ServiceMessage ServiceMessage { get; }

    }
}
