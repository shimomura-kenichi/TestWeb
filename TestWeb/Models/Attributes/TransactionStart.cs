using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWeb.Models.Attributes
{
    /// <summary>
    /// トランザクション開始用のアトリビュート
    /// この属性が付与されたサービスのメソッドを実行すると
    /// ServiceProxyでトランザクションを開始する
    /// UnityMvcの関連でコントローラーからサービスの呼び出しはインタフェースで行われるため、
    /// 本属性はインタフェースに付与する
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionStart : Attribute
    {
        // マークに使用するだけなので実装なし
    }
}