using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Web.Ashx.BulletinManage
{
    /// <summary>
    /// BulletinAdd 的摘要说明
    /// </summary>
    public class BulletinAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Bulletin model = new Bulletin();
            SensitiveBll senstitiveBll = new SensitiveBll();
            string title = context.Request["Title"];
            string content = context.Request["content"];
            string  userName= context.Request["UserName"];
            model.State = "审核通过";
            model.TypeId = int.Parse(context.Request["TypeId"] ?? "0");
            model.UserName = userName;
            model.Content = content;
            model.Title = title;
            BulletinBll bll = new BulletinBll();
            if (title==""|| title.Length<1|| content.Length<1|| content==""|| userName.Length<1|| userName=="")
            {
                context.Response.Write("No:内容或标题包或发布人不能为空!!!");
                return;
            }

            if (senstitiveBll.CheckBanned(content) || senstitiveBll.CheckBanned(title))
            {
                context.Response.Write("No:内容或标题包含禁用词!!!");
                return;
            }
            else if (senstitiveBll.CheckMod(content) || senstitiveBll.CheckMod(title))
            {
                model.State = "待审核";
                if (bll.Add(model))
                {
                    context.Response.Write("Ok:内容或标题包含审核词,请等待审核!!!");
                }
            }
            else
            {
                if (bll.Add(model))
                {
                    context.Response.Write("Ok:成功");
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