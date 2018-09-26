using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PageNav
    {
        /// <summary>
        ///  生成分页 HTML代码
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static string PageNavGenerate(int pageIndex, int pageSize, int total)
        {
            int pageCount = Convert.ToInt32(Math.Ceiling(1.0 * total / pageSize));
            StringBuilder sb = new StringBuilder();
            int up = pageIndex - 1 < 1 ? 1 : pageIndex - 1;
            int down = pageIndex + 1 > pageCount ? pageCount : pageIndex + 1;
            sb.Append("<nav aria-label='Page navigation'>");
            sb.Append("<ul class='pagination'>");
            sb.AppendFormat(" <li><a href='/Ashx/SensitivePageList.ashx?pageIndex={0}' aria-label='Previous'> <span aria-hidden='true'>&laquo;</span></a></li>", up);
            sb.Append("");
            for (int i = 1; i <= pageCount; i++)
            {
                if (i == pageIndex)
                    sb.AppendFormat("<li class='active'><a href='/Ashx/SensitivePageList.ashx?pageIndex={0}'>{0}</a></li>", i);
                else
                    sb.AppendFormat("<li><a href='/Ashx/SensitivePageList.ashx?pageIndex={0}'>{0}</a></li>", i);
            }
            sb.AppendFormat(" <li><a href='/Ashx/SensitivePageList.ashx?pageIndex={0}' aria-label='Next'> <span aria-hidden='true'>&raquo;</span></a></li>", down);
            sb.Append("</ul> </nav>");
            return sb.ToString();
        }
    }
}
