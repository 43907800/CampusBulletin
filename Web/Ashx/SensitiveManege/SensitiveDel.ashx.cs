using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.SensitiveManege
{
    /// <summary>
    /// SensitiveDel 的摘要说明
    /// </summary>
    public class SensitiveDel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = int.Parse(context.Request["Id"] ?? "0");
            SensitiveBll sensitiveBll = new SensitiveBll();
            if (sensitiveBll.Remove(id))
            {
                context.Response.Write("Ok");
            }
            //context.Response.Write("Hello World");
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