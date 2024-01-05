using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiseman.PJC.Service.GroupSettings.RDB
{
    public class SQLFunction
    {
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SQLFunction() { }

        /// <summary>
        /// SQL用文字列生成(文字列型)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Char(string? str)
        {
            var result = "";
            if (str == null)
            {
                result = "NULL";
            }
            else
            {
                result = "\'" + str + "\'";
            }

            return result;
        }

        /// <summary>
        /// SQL用文字列生成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Number(long? str)
        {
            var result = "";
            if (str == null)
            {
                result = "NULL";
            }
            else
            {
                result = str.ToString();
            }

            return result;
        }
    }
}
