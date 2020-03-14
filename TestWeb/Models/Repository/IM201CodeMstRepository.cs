using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb.Models.Repository
{
    /// <summary>
    /// コードマスターリポジトリインタフェース
    /// </summary>
    public interface IM201CodeMstRepository
    {
        /// <summary>
        /// コードマスターリスト返却
        /// </summary>
        /// <param name="cdKind">コード区分</param>
        /// <returns>結果リスト</returns>
        List<M201CodeMst> GetCodeMstList(string cdKind);
    }
}
