﻿using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Web.Ashx
{
    /// <summary>
    /// adminLogin 的摘要说明
    /// </summary>
    public class adminLogin : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //AdminInfo model = context.Session["admin"] as AdminInfo;
            //if (model != null)
            //{
            //    context.Response.Redirect("admin/Index.aspx");
            //}
            AdminInfo model = new AdminInfo();
            string userName = context.Request["UserName"];//采集数据
            string pwd = context.Request["Password"];//采集数据
            if (userName.Length > 16 || pwd.Length > 16)//校验数据
                context.Response.Write("No");
            AdminInfoBll bll = new AdminInfoBll();

            model = bll.Login(userName, pwd);//调用Bll层登陆方法
            if (model == null) context.Response.Write("No");
            else
            {
                context.Session["admin"] = model;
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