using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.UserManage
{
    /// <summary>
    /// UserUpdateHeadPic 的摘要说明
    /// </summary>
    public class UserUpdateHeadPic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string picAddr = context.Request["HeadPicAddr"];
            string userName = context.Request["userName"];
            UserBll bll = new UserBll();
            if(bll.UpdateHeadPic(picAddr,userName))
                context.Response.Write("Ok");
            else
                context.Response.Write("No");

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