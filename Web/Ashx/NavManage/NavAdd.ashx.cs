﻿using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx
{
    /// <summary>
    /// NavAdd 的摘要说明
    /// </summary>
    public class NavAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NavInfo nav = new NavInfo();
            //采集数据
            nav.Oder =int.Parse( context.Request["Oder"]??"99");
            nav.Text = context.Request["Text"];
            nav.Address = context.Request["Address"];
            nav.Remark= context.Request["Remark"];

            NavInfoBll navBll = new NavInfoBll();
            //调用业务逻辑层方法保存到数据库
            if (navBll.Add(nav)) context.Response.Write("Ok");
            else context.Response.Write("No");
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