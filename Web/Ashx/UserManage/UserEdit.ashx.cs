﻿using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.UserManage
{
    /// <summary>
    /// UserEdit 的摘要说明
    /// </summary>
    public class UserEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            UserBll bll = new UserBll();
            context.Response.ContentType = "text/plain";
            if (context.Request.RequestType.ToUpper() == "GET")
            {
                int id = int.Parse(context.Request["Id"] ?? "0");
                UserInfo user = bll.GetModelById(id);//根据Id 获取 用户信息
                System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jsonStr = javaScriptSerializer.Serialize(user);//将对象序列化成json
                context.Response.Write(jsonStr);
            }
            else
            {
                UserInfo user = new UserInfo();
                user.Id = int.Parse(context.Request["EditId"] ?? "0");
                user.UserName = context.Request["EditUserName"];
                string passwrod = context.Request["EditPassword"];
                if (passwrod != context.Request["EditPassword1"])
                {
                    context.Response.Write("密码不一致!");
                    return;
                }
                else if (passwrod.Length==32)
                {
                    
                }
                else if ( passwrod.Length < 6 || passwrod.Length > 16)
                {
                    context.Response.Write("用户名或密码的长度过短或过长!!!");
                    return;
                }
                user.Password = passwrod;
                user.Mail = context.Request["EditEmail"];
                if (!user.Mail.Contains("@"))
                {
                    context.Response.Write("邮箱格式不正确!!");
                    return;
                }
                user.Name = context.Request["EditName"];
                user.Sex = context.Request["EditSex"] == "男";
                user.Region = context.Request["EditRegion"];
                user.isDisable= context.Request["EditisDisable"] == "on";
                if (bll.Update(user))
                {
                    context.Response.Write("Ok");
                }
                else
                {
                    context.Response.Write("No");
                }
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