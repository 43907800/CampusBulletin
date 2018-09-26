using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx
{
    /// <summary>
    /// SensitiveAdd 的摘要说明
    /// </summary>
    public class SensitiveAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string SensitiveStrs = context.Request["SensitiveStrs"];
            SensitiveBll bll = new SensitiveBll();
            int count = bll.AddList(SensitiveStrs);
            context.Response.Write(count.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}