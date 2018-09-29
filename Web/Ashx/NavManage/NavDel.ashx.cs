using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.NavManage
{
    /// <summary>
    /// NavDel 的摘要说明
    /// </summary>
    public class NavDel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = int.Parse(context.Request["Id"] ?? "0");
            NavInfoBll navBll = new NavInfoBll();
            if (navBll.Remove(id))
            {
                context.Response.Write("Ok");
            }
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