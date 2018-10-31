using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Web.Ashx.UserManage
{
    /// <summary>
    /// UserLogout 的摘要说明
    /// </summary>
    public class UserLogout : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Session.Remove("userInfo");//删除 session 
            context.Response.Redirect("/index.aspx");//重定向到首页
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