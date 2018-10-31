using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.DiscussManege
{
    /// <summary>
    /// DiscussAdd 的摘要说明
    /// </summary>
    public class DiscussAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Discuss model = new Discuss();
            model.UserName = context.Request["userName"];
            model.BulletinId =int.Parse( context.Request["bulletitsId"] ??"0");
            model.Content = context.Request["content"];
            if (string.IsNullOrWhiteSpace(model.UserName)) //是否登录
            {
                context.Response.Write("请登录后在评论!!!");
                return;
            }
            SensitiveBll sensitiveBll = new SensitiveBll();
          if(sensitiveBll.CheckBanned(model.Content)) {  //是否存在禁用词
                context.Response.Write("内容包含禁用词!!!");
                return;
            }
            if (sensitiveBll.CheckMod(model.Content))//是否存在 审核词
            {
                context.Response.Write("内容包含审核词,请等待审核!!!");
                model.State = "待审核";
            }
            DiscussBll bll = new DiscussBll();
            model.State = "审核通过";
            if (bll.Add(model))//添加到数据库
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