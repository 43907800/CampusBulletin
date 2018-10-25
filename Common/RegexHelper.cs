using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 正则帮助类
    /// </summary>
    public static class RegexHelper
    {
        
        public static string Replace(string str, string regStr)
        {
            Regex reg = new Regex(regStr);
            Regex.Replace(str, regStr, "");
            return "";
        }
    }
}
