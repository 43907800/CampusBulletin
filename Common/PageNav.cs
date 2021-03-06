﻿using System;
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
            if (pageCount<2) return string.Empty;
            sb.Append("<nav aria-label='Page navigation'>");
            sb.Append("<ul class='pagination'>");
            sb.AppendFormat(" <li><a href='/Ashx/SensitivePageList.ashx?pageIndex={0}' aria-label='Previous'> <span aria-hidden='true'>&laquo;</span></a></li>", up);
            int start = (pageIndex - 2) < 1 ? 1 : pageIndex - 2;
            int end = start + 5;
            if (end>pageCount)
            {
                start = pageCount - 5;
                end = pageCount;
            }
            start = start < 1 ? 1 : start;
            for (int i = start; i <=end; i++)
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

        /// <summary>
        ///  生成分页 HTML代码
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static string PageNavGenerate(int pageIndex, int pageSize, int total,int typeId)
        {
            int pageCount = Convert.ToInt32(Math.Ceiling(1.0 * total / pageSize));
            StringBuilder sb = new StringBuilder();
            int up = pageIndex - 1 < 1 ? 1 : pageIndex - 1;
            int down = pageIndex + 1 > pageCount ? pageCount : pageIndex + 1;
            if (pageCount < 2) return string.Empty;
            sb.Append("<nav aria-label='Page navigation'>");
            sb.Append("<ul class='pagination'>");
            sb.AppendFormat(" <li><a href='/Ashx/SensitivePageList.ashx?pageIndex={0}&typeId={1}' aria-label='Previous'> <span aria-hidden='true'>&laquo;</span></a></li>", up,typeId);
            int start = (pageIndex - 2) < 1 ? 1 : pageIndex - 2;
            int end = start + 5;
            if (end > pageCount)
            {
                start = pageCount - 5;
                end = pageCount;
            }
            start = start < 1 ? 1 : start;
            for (int i = start; i <= end; i++)
            {
                if (i == pageIndex)
                    sb.AppendFormat("<li class='active'><a href='/Ashx/SensitivePageList.ashx?pageIndex={0}&typeId={1}'>{0}</a></li>", i, typeId);
                else
                    sb.AppendFormat("<li><a href='/Ashx/SensitivePageList.ashx?pageIndex={0}&typeId={1}'>{0}</a></li>", i, typeId);
            }
            sb.AppendFormat(" <li><a href='/Ashx/SensitivePageList.ashx?pageIndex={0}&typeId={1}' aria-label='Next'> <span aria-hidden='true'>&raquo;</span></a></li>", down, typeId);
            sb.Append("</ul> </nav>");
            return sb.ToString();
        }
    }
}
