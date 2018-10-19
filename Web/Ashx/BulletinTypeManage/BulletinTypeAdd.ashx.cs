using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.BulletinTypeManage
{
    /// <summary>
    /// BulletinTypeAdd 的摘要说明
    /// </summary>
    public class BulletinTypeAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BulletinType type = new BulletinType();
            type.TypeName = context.Request["TypeName"];
            BulletinTypeBll bll = new BulletinTypeBll();
            if (bll.Add(type))
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