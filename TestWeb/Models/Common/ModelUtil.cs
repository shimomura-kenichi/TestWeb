using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.ModelBinding;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// モデル操作
    /// </summary>
    public static class ModelUtil
    {
        /// <summary>
        /// モデルからモデルに値をコピーする
        /// </summary>
        /// <param name="source">コピー元</param>
        /// <param name="dest">コピー先</param>
        /// <returns>コピー先</returns>
        public static object CopyModelToModel(object source, object dest)
        {
            // コピー先のメンバー一覧を取得する
            Type destType = dest.GetType();
            PropertyInfo[] destMember = destType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.GetProperty);

            // コピー元のメンバー一覧を取得する
            Type sourceType = source.GetType();
            PropertyInfo[] sourceMember = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.GetProperty);

            // コピー元を軸にして値をセットする
            foreach (var prop in sourceMember)
            {
                // コピー先に存在するか
                var destProp = destMember.Where(m => m.Name == prop.Name).FirstOrDefault();
                if (destProp != null)
                {
                    if (IsPrimitive(destProp.PropertyType))
                    {
                        // コピー
                        destProp.SetValue(dest, prop.GetValue(source));
                    }
                }
            }
            return dest;
        }

        /// <summary>
        /// プロパティ名取得
        /// </summary>
        /// <typeparam name="TModel">モデル</typeparam>
        /// <param name="expression">プロパティのラムダ式</param>
        /// <returns>プロパティ名</returns>
        public static string GetPropertyName<TModel>(Expression<Func<TModel, object>>expression)
        {
            var method = (MemberExpression)(expression.Body);
            var name = method.Member.Name;
            return name;
        }

        /// <summary>
        /// プリミティブ準拠判定
        /// </summary>
        /// <param name="type">タイプ</param>
        /// <returns>true:プリミティブ準拠（コピーできる）</returns>
        public static bool IsPrimitive(Type type)
        {
            Type[] typeList = { typeof(string), typeof(int), typeof(int?), typeof(long), typeof(long?)
                    , typeof(double), typeof(double?), typeof(decimal), typeof(decimal?)
                    , typeof(bool), typeof(bool?), typeof(DateTime), typeof(DateTime?)
                    , typeof(TimeSpan), typeof(TimeSpan?) };

            bool isPrimitive = false;
            if (typeList.Any(m => m == type))
            {
                isPrimitive = true;
            }

            return isPrimitive;
        }

        /// <summary>
        /// DisplayName取得
        /// </summary>
        /// <typeparam name="TModel">モデル</typeparam>
        /// <param name="expression">メンバーのラムダ式</param>
        /// <returns>DisplayName</returns>
        public static string GetDisplayName<TModel>(Expression<Func<TModel, object>> expression)
        {
            var member = (MemberExpression)(expression.Body);
            DisplayNameAttribute displayNameAttribute = (DisplayNameAttribute)member.Member.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
            if (displayNameAttribute != null)
            {
                return displayNameAttribute.DisplayName;
            } else
            {
                return string.Empty;
            }
        }
    }
}