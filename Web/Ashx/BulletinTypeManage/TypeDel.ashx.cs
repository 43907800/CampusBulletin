﻿using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.BulletinTypeManage
{
    /// <summary>
    /// TypeDel 的摘要说明
    /// </summary>
    public class TypeDel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = int.Parse(context.Request["Id"] ?? "0");
            BulletinTypeBll bll = new BulletinTypeBll();
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