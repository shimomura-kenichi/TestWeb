using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// サービス共通インタフェース
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// サービスメッセージ
        /// </summary>
        ServiceMessage ServiceMessage { get; set; }

        /// <summary>
        /// ユーザー情報
        /// </summary>
        UserInfoModel UserInfoModel { set; }

        /// <summary>
        /// DB保存用メソッド
        /// </summary>
        Func<bool> SaveChangeFunc { set; }
    }
}
