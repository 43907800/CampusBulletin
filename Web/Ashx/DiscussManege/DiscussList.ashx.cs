using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.DiscussManege
{
    /// <summary>
    /// DiscussList 的摘要说明
    /// </summary>
    public class DiscussList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //获取前端传过来 的 公告id
            int bId = int.Parse(context.Request["bulletitsId"] ?? "0");

            DiscussBll bll = new DiscussBll();
            List<Discuss> list = bll.GetAllByBulletinId(bId);//根据公告id查询评论
            System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string json = javaScriptSerializer.Serialize(list);
            context.Response.Write(json);
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