using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using TestWeb.Models.AttendanceTime.InputModel;
using TestWeb.Models.AttendanceTime.ViewModel;
using TestWeb.Models.Attributes;
using TestWeb.Models.Common;
using TestWeb.Models.Repository;
using TestWeb.Properties;

namespace TestWeb.Models.AttendanceTime
{
    /// <summary>
    /// 出退勤時間サービス
    /// </summary>
    public class AttendanceTimeService : IAttendanceTimeService
    {
        /// <summary>
        /// サービスメッセージ
        /// </summary>
        public ServiceMessage ServiceMessage { get; set; }

        /// <summary>
        /// ユーザー情報
        /// </summary>
        public UserInfoModel UserInfoModel { get; set; }

        /// <summary>
        /// DB保存用ファンクション
        /// </summary>
        public Func<bool> SaveChangeFunc { get; set; }

        /// <summary>
        /// T200出退勤時間リポジトリ
        /// </summary>
        private IT200AttendanceTimeRepository _T200Repository;

        /// <summary>
        /// V200出退勤時間リポジトリ
        /// </summary>
        private IV200AttendanceTimeRepository _V200Repository;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="t200Repository">T200出退勤時間リポジトリ</param>
        /// <param name="v200Repository">V200出退勤時間リポジトリ</param>
        public AttendanceTimeService(IT200AttendanceTimeRepository t200Repository, IV200AttendanceTimeRepository v200Repository)
        {
            this._T200Repository = t200Repository;
            this._V200Repository = v200Repository;
        }

        /// <summary>
        /// 出退勤時間画面初期表示
        /// </summary>
        /// <returns>ビューモデル</returns>
        [TransactionStart()]
        public AttendanceTimeViewModel Init()
        {
            // 初期表示は、当月分のログインユーザーの出退勤時間を表示する
            DateTime today = DateTime.Now;

            AttendanceTimeViewModel viewModel = new AttendanceTimeViewModel();

            string targetUserId = this.UserInfoModel.UserId;
            int targetYear = today.Year;
            int targetMonth = today.Month;

            // 当月分のデータを検索する
            AttendanceTimeConditionModel condition = new AttendanceTimeConditionModel()
            {
                UserId = targetUserId,
                WorkYear = targetYear,
                WorkMonth = targetMonth
            };

            List<V200AttendanceTime> resultList = this._V200Repository.GetAttendanceTimeList(condition);

            // 結果が該当なしの場合は新規に作成する
            if (resultList.Count == 0)
            {
                int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
                for (int i = 1; i <= daysInMonth; i++)
                {
                    AttendanceTimeKeyInputModel keyModel = new AttendanceTimeKeyInputModel()
                    {
                        UserId = targetUserId,
                        WorkYear = targetYear.ToString(),
                        WorkMonth = targetMonth.ToString(),
                        WorkDay = i.ToString(),
                        WorkNo = "1"
                    };

                    // 空行作成
                    T200AttendanceTime newData = CreateEmptyDetail(keyModel, today);
                    this._T200Repository.InsertAttendanceTime(newData);
                }

                // Dbに登録
                if (!this.SaveChangeFunc())
                {
                    // 失敗時はエラーを表示
                    this.ServiceMessage.AddErrorMessage(null, Resources.MEP0004);
                }
                // 成功しても失敗しても再度検索する（結果０件なら０件で表示する）
                resultList = this._V200Repository.GetAttendanceTimeList(condition);

            }

            viewModel.AttendanceTimeList = new List<AttendanceTimeModel>();
            // コピー
            foreach (var dbModel in resultList)
            {
                // Dbモデルから出退勤モデルを生成する
                AttendanceTimeModel model = CreateAttendanceTimeModel(dbModel);

                // リストに追加
                viewModel.AttendanceTimeList.Add(model);

            }

            return viewModel;
        }

        /// <summary>
        /// 出退勤画面入力画面表示
        /// </summary>
        /// <param name="keyModel">キー情報</param>
        /// <returns>ビューモデル</returns>
        public AttendanceTimeModel InitDetail(AttendanceTimeKeyInputModel keyModel)
        {
            AttendanceTimeModel model = this.GetDetail(keyModel);

            return model;
        }

        /// <summary>
        /// 明細情報取得
        /// </summary>
        /// <param name="keyModel">キー情報</param>
        /// <returns>ビューモデル</returns>
        public AttendanceTimeModel GetDetail(AttendanceTimeKeyInputModel keyModel)
        {
            // キーに対する出退勤情報を取得する
            AttendanceTimeConditionModel condition = new AttendanceTimeConditionModel()
            {
                UserId = this.UserInfoModel.UserId,
                WorkYear = int.Parse(keyModel.WorkYear),
                WorkMonth = int.Parse(keyModel.WorkMonth),
                WorkDay = int.Parse(keyModel.WorkDay),
                WorkNo = int.Parse(keyModel.WorkNo)
            };
            V200AttendanceTime dbModel = this._V200Repository.GetAttendanceTimeByKey(condition);

            // 結果がnullの場合は通常のルートではありえないので、引数エラーとして例外を発生させ、エラー画面に遷移させる
            if (dbModel == null)
            {
                throw new ArgumentException("keyModel");
            }

            // Dbモデルから出退勤モデルを生成する
            AttendanceTimeModel model = CreateAttendanceTimeModel(dbModel);

            return model;
        }

        /// <summary>
        /// 出退勤時間更新
        /// </summary>
        /// <param name="inputModel">入力情報</param>
        /// <returns>ビューモデル</returns>
        [TransactionStart()]
        public AttendanceTimeModel UpdateDetail(AttendanceTimeInputModel inputModel)
        {
            // 現在日時を取得する
            DateTime today = DateTime.Now;

            // 入力モデルのチェック
            // 更新の許可は、本人と経理のみ
            if (inputModel.UserId != this.UserInfoModel.UserId 
                    && !Constants.ROLE_ACT.Split(',').Contains(this.UserInfoModel.CurrentPositionCd)) {
                // 不正アクセスのため例外
                throw new ArgumentException("inputModel");
            }

            // キーに対する出退勤情報を取得する
            AttendanceTimeConditionModel condition = new AttendanceTimeConditionModel()
            {
                UserId = this.UserInfoModel.UserId,
                WorkYear = int.Parse(inputModel.WorkYear),
                WorkMonth = int.Parse(inputModel.WorkMonth),
                WorkDay = int.Parse(inputModel.WorkDay),
                WorkNo = int.Parse(inputModel.WorkNo)
            };

            T200AttendanceTime source = this._T200Repository.GetAttendanceTimeByKey(condition);

            // 該当データがない場合はすでに削除されたと判断する
            if (source == null)
            {
                this.ServiceMessage.AddErrorMessage(null, string.Format(Resources.MEP0004, "出退勤"));
            } else
            {
                // データが存在し、表示時の更新日時と異なる場合は、他のユーザーが更新したと判断する。
                if (source.UpdateDttm != DateConvUtil.ConvertDtmStrToDate(inputModel.UpdateDttmStr)
                    || source.UpdateUserId != inputModel.UpdateUserId)
                {
                    this.ServiceMessage.AddErrorMessage(null, string.Format(Resources.MEP0004, "出退勤"));
                }
            }

            T200AttendanceTime result = null;

            // エラーがない場合は更新処理を行う
            if (this.ServiceMessage.IsValid)
            {
                T200AttendanceTime updModel = new T200AttendanceTime()
                {
                    WorkKind = inputModel.WorkKind,
                    HolidayKind = inputModel.HolidayKind,
                    StartTime = DateConvUtil.ConvertTimeStrHmToTimeSpan(inputModel.StartTime),
                    EndTime = DateConvUtil.ConvertTimeStrHmToTimeSpan(inputModel.EndTime),
                    RestTime = DateConvUtil.ConvertTimeStrHmToTimeSpan(inputModel.RestTime),
                    Remarks = inputModel.Remarks,
                    UpdateUserId = this.UserInfoModel.UserId,
                    UpdateDttm = today
                };

                result = this._T200Repository.UpdateAttendanceTime(source, updModel);

                // Db確定
                if (!this.SaveChangeFunc())
                {
                    // 失敗時は楽観ロックエラー扱いとする
                    this.ServiceMessage.AddErrorMessage(null, string.Format(Resources.MEP0004, "出退勤"));
                }
            }

            // 更新失敗時はnullを返却する
            if (!this.ServiceMessage.IsValid)
            {
                return null;
            }
            // 成功時は更新データを返却する

            // Dbモデルから出退勤モデルを生成する
            AttendanceTimeModel model = CreateAttendanceTimeModel(result);

            return model;
        }

        /// <summary>
        /// 出退勤時間空行追加
        /// </summary>
        /// <param name="keyModel">追加対象の年月日までのキー情報</param>
        /// <returns>ビューモデル</returns>
        [TransactionStart()]
        public AttendanceTimeModel AddEmptyDetail(AttendanceTimeKeyInputModel keyModel)
        {
            // 現在日時を取得する
            DateTime today = DateTime.Now;

            // 入力モデルのチェック
            // 更新の許可は、本人と経理のみ
            if (keyModel.UserId != this.UserInfoModel.UserId
                    && !Constants.ROLE_ACT.Split(',').Contains(this.UserInfoModel.CurrentPositionCd))
            {
                // 不正アクセスのため例外
                throw new ArgumentException("keyModel");
            }

            // 年月日に対する行情報を取得する
            AttendanceTimeConditionModel condition = new AttendanceTimeConditionModel()
            {
                UserId = this.UserInfoModel.UserId,
                WorkYear = int.Parse(keyModel.WorkYear),
                WorkMonth = int.Parse(keyModel.WorkMonth),
                WorkDay = int.Parse(keyModel.WorkDay)
            };

            List<T200AttendanceTime> source = this._T200Repository.GetAttendanceTimeList(condition);
            // 該当なしの場合は不正アクセス
            if (source.Count == 0)
            {
                // 不正アクセスのため例外
                throw new ArgumentException("keyModel");
            }
            T200AttendanceTime maxDetail = source.OrderByDescending(m => m.WorkNo).First();

            // 最大行+1で新しい行を作成する
            AttendanceTimeKeyInputModel newKeyModel = new AttendanceTimeKeyInputModel();
            ModelUtil.CopyModelToModel(keyModel, newKeyModel);
            newKeyModel.WorkNo = (maxDetail.WorkNo + 1).ToString();

            T200AttendanceTime newData = CreateEmptyDetail(newKeyModel, today);
            this._T200Repository.InsertAttendanceTime(newData);
            // Dbに登録
            if (!this.SaveChangeFunc())
            {
                // 失敗時はエラーを表示
                this.ServiceMessage.AddErrorMessage(null, Resources.MEP0004);
            }

            // エラーがない場合は追加情報を返却する
            if (this.ServiceMessage.IsValid)
            {
                // Dbモデルから出退勤モデルを生成する
                AttendanceTimeModel model = CreateAttendanceTimeModel(newData);
                return model;
            } else
            {
                return null;
            }
        }

        /// <summary>
        /// 出退勤時間行削除
        /// </summary>
        /// <param name="keyModel">キー情報</param>
        /// <returns>ビューモデル</returns>
        [TransactionStart()]
        public AttendanceTimeModel DeleteDetail(AttendanceTimeKeyInputModel keyModel)
        {
            // 現在日時を取得する
            DateTime today = DateTime.Now;

            // 入力モデルのチェック
            // 更新の許可は、本人と経理のみ
            if (keyModel.UserId != this.UserInfoModel.UserId
                    && !Constants.ROLE_ACT.Split(',').Contains(this.UserInfoModel.CurrentPositionCd))
            {
                // 不正アクセスのため例外
                throw new ArgumentException("keyModel");
            }

            // キーに対する出退勤情報を取得する
            AttendanceTimeConditionModel condition = new AttendanceTimeConditionModel()
            {
                UserId = this.UserInfoModel.UserId,
                WorkYear = int.Parse(keyModel.WorkYear),
                WorkMonth = int.Parse(keyModel.WorkMonth),
                WorkDay = int.Parse(keyModel.WorkDay),
                WorkNo = int.Parse(keyModel.WorkNo)
            };

            T200AttendanceTime source = this._T200Repository.GetAttendanceTimeByKey(condition);

            // 該当データがない場合はすでに削除されたと判断する
            if (source == null)
            {
                this.ServiceMessage.AddErrorMessage(null, string.Format(Resources.MEP0004, "出退勤"));
            }
            // 削除に関しては楽観ロックを考慮しない。
            // 画面表示→他のユーザーが更新→削除の場合を許容する

            // エラーがない場合は削除処理を行う
            if (this.ServiceMessage.IsValid)
            {
                this._T200Repository.DeleteAttendanceTime(source);

                // Dbに登録
                if (!this.SaveChangeFunc())
                {
                    // 失敗時はエラーを表示
                    this.ServiceMessage.AddErrorMessage(null, Resources.MEP0004);
                }
            }

            // エラーがない場合は削除情報を返却する
            if (this.ServiceMessage.IsValid)
            {
                // Dbモデルから出退勤モデルを生成する
                AttendanceTimeModel model = CreateAttendanceTimeModel(source);
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 空行用の出退勤時間データを作成する
        /// </summary>
        /// <param name="keyModel">キー情報</param>
        /// <param name="processDate">処理日時</param>
        /// <returns>空行用の出退勤時間データ</returns>
        private T200AttendanceTime CreateEmptyDetail(AttendanceTimeKeyInputModel keyModel, DateTime processDate)
        {
            int year = int.Parse(keyModel.WorkYear);
            int month = int.Parse(keyModel.WorkMonth);
            int day = int.Parse(keyModel.WorkDay);
            DateTime loopDay = new DateTime(year, month, day);

            // 週の初めの日を算出（月曜日が週の初め）
            DateTime weekStartDate = new DateTime(year, month, day);
            if ((int)loopDay.DayOfWeek == 0)
            {
                // 日曜日の場合
                weekStartDate = weekStartDate.AddDays(-6);
            }
            else
            {
                weekStartDate = weekStartDate.AddDays(-((int)loopDay.DayOfWeek - 1));
            }

            T200AttendanceTime model = new T200AttendanceTime()
            {
                WorkYear = year,
                WorkMonth = month,
                WorkDay = day,
                WorkNo = int.Parse(keyModel.WorkNo),
                WorkKind = null,
                WeekStartDate = weekStartDate,
                UserId = keyModel.UserId,
                StartTime = null,
                EndTime = null,
                RestTime = null,
                HolidayKind = Constants.HOLIDAY_KIND_NORMAL,
                Remarks = null,
                UpdateUserId = keyModel.UserId,
                CreateDttm = processDate,
                UpdateDttm = processDate,
                DeleteFlg = Constants.DELETE_FLG_NORMAL,
                DepartmentCd = this.UserInfoModel.CurrentDepartmentCd
            };

            return model;
        }

        /// <summary>
        /// 出退勤時間モデル生成
        /// </summary>
        /// <param name="dbModel">V200出退勤時間ビューモデル</param>
        /// <returns>出退勤時間モデル</returns>
        private AttendanceTimeModel CreateAttendanceTimeModel(object dbModel)
        {
            // Dbモデルから出退勤時間モデルにコピーする
            var model = (AttendanceTimeModel)ModelUtil.CopyModelToModel(dbModel, new AttendanceTimeModel());

            // キー情報の設定
            AttendanceTimeKeyInputModel keyModel = new AttendanceTimeKeyInputModel()
            {
                UserId = model.UserId,
                WorkYear = model.WorkYear.ToString(),
                WorkMonth = model.WorkMonth.ToString(),
                WorkDay = model.WorkDay.ToString(),
                WorkNo = model.WorkNo.ToString()
            };
            // キーの設定
            model.KeyValueJSon = Json.Encode(keyModel);

            // 曜日の設定
            model.WeekDay = Constants.WEEK_DAY_KANJI[(int)new DateTime(model.WorkYear, model.WorkMonth, model.WorkDay).DayOfWeek];

            // 楽観ロック用の更新日時
            model.UpdateDttmStr = DateConvUtil.ConvertDateToStrDtmm(model.UpdateDttm);

            return model;
        }
    }
}