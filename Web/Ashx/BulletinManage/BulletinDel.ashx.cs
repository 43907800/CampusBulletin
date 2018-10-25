using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.BulletinManage
{
    /// <summary>
    /// BulletinDel 的摘要说明
    /// </summary>
    public class BulletinDel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = int.Parse(context.Request["Id"] ?? "0");
            BulletinBll bll = new BulletinBll();
            if (bll.Delete(id))
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