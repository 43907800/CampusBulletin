using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.UserManage
{
    /// <summary>
    /// UserAdd 的摘要说明
    /// </summary>
    public class UserAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            UserInfo user = new UserInfo();
            user.UserName = context.Request["UserName"];
            string passwrod= context.Request["Password"];
            if (passwrod!=context.Request["Password1"])
            {
                context.Response.Write("密码不一致!");
                return;
            }
            user.Password = passwrod;
            user.Mail = context.Request["Email"];
            user.Name = context.Request["Name"];
            user.Sex = context.Request["Sex"]=="男";
            user.Region = context.Request["Region"];
            
            UserBll bll = new UserBll();
            if (bll.Add(user))
            {
                context.Response.Write("Ok");
            }
            else
            {
                context.Response.Write("No!");
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