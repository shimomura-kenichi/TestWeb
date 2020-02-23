using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeb.Models.AttendanceTime.InputModel;
using TestWeb.Models.AttendanceTime.ViewModel;
using TestWeb.Models.Attributes;
using TestWeb.Models.Common;

namespace TestWeb.Models.AttendanceTime
{
    /// <summary>
    /// 出退勤時間サービスインタフェース
    /// </summary>
    public interface IAttendanceTimeService : IService
    {
        /// <summary>
        /// 出退勤時間画面初期表示
        /// </summary>
        /// <returns>ビューモデル</returns>
        [TransactionStart()]
        AttendanceTimeViewModel Init();

        /// <summary>
        /// 出退勤画面入力画面表示
        /// </summary>
        /// <param name="keyModel">キー情報</param>
        /// <returns>ビューモデル</returns>
        AttendanceTimeModel InitDetail(AttendanceTimeKeyInputModel keyModel);

        /// <summary>
        /// 明細情報取得
        /// </summary>
        /// <param name="keyModel">キー情報</param>
        /// <returns>ビューモデル</returns>
        AttendanceTimeModel GetDetail(AttendanceTimeKeyInputModel keyModel);

        /// <summary>
        /// 出退勤時間更新
        /// </summary>
        /// <param name="inputModel">入力情報</param>
        /// <returns>ビューモデル</returns>
        [TransactionStart()]
        AttendanceTimeModel UpdateDetail(AttendanceTimeInputModel inputModel);

        /// <summary>
        /// 出退勤時間空行追加
        /// </summary>
        /// <param name="keyModel">追加対象の年月日までのキー情報</param>
        /// <returns>ビューモデル</returns>
        [TransactionStart()]
        AttendanceTimeModel AddEmptyDetail(AttendanceTimeKeyInputModel keyModel);

        /// <summary>
        /// 出退勤時間行削除
        /// </summary>
        /// <param name="keyModel">キー情報</param>
        /// <returns>ビューモデル</returns>
        [TransactionStart()]
        AttendanceTimeModel DeleteDetail(AttendanceTimeKeyInputModel keyModel);
    }
}
