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
            else if (user.UserName.Length<6|| user.UserName.Length>16||passwrod.Length<6||passwrod.Length>16)
            {
                context.Response.Write("用户名或密码的长度过短或过长!!!");
                return;
            }
            UserBll bll = new UserBll();
            if (bll.ExistByUserName(user.UserName))//检验用户是否存在
            {
                context.Response.Write("用户名已存在!!");
                return;
            }
            user.Password = passwrod;
            user.Mail = context.Request["Email"];
            if (!user.Mail.Contains("@"))//校验邮箱
            {
                context.Response.Write("邮箱格式不正确!!");
                return;
            }
            user.Name = context.Request["Name"];
            user.Sex = context.Request["Sex"]=="男";
            user.Region = context.Request["Region"];
            
            //调用 业务逻辑层 添加数据方法 保存到数据库
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