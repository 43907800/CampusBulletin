using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Web.Ashx.UserManage
{
    /// <summary>
    /// UserLogin 的摘要说明
    /// </summary>
    public class UserLogin : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userName = context.Request["LoginUserName"];
            string password = context.Request["LoginPassword"];
            if (userName.Length > 16 || password.Length > 16)//校验数据
            { context.Response.Write("No:用户名或密码过长!!");
                return;
            }
            if (userName.Length < 1 || password.Length < 1)
            {
                context.Response.Write("No:请输入用户名和密码!!!!!");
                return;
            }
            UserBll userBll = new UserBll();
            UserInfo user= userBll.Login(userName, password);
            if (user==null)
            {
                context.Response.Write("No:用户名或密码错误!!!");
            }
            else if (user.isDisable == true)
            {
                context.Response.Write("No:用户被禁用了!!!");
            }
            else 
            {
                context.Session["userInfo"] = user;
                context.Response.Write("Ok:成功");
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