using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.BulletinManage
{
    /// <summary>
    /// BulletinEdit 的摘要说明
    /// </summary>
    public class BulletinEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BulletinBll bll = new BulletinBll();
            if (context.Request.RequestType.ToUpper() == "GET")
            {
                int id = int.Parse(context.Request["Id"] ?? "0");
                Bulletin model = bll.GetModelById(id);//根据Id 获取数据
                System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jsonStr = javaScriptSerializer.Serialize(model);//将对象序列化成json
                context.Response.Write(jsonStr);
            }
            else
            {
                Bulletin model = new Bulletin();
                string title = context.Request["editTitle"];
                string content = context.Request["editContent"];
                string userName = context.Request["editUserName"];

                model.Id = int.Parse(context.Request["EditId"] ?? "0");
                model.State = context.Request["editselState"];
                model.TypeId = int.Parse(context.Request["editTypeId"] ?? "0");
                model.UserName = userName;
                model.Content = content;
                model.Title = title;
                if (title == "" || title.Length < 1 || content.Length < 1 || content == "" || userName.Length < 1 || userName == "")
                {
                    context.Response.Write("No:内容或标题包或发布人不能为空!!!");
                    return;
                }
                SensitiveBll senstitiveBll = new SensitiveBll();
                if (senstitiveBll.CheckBanned(content) || senstitiveBll.CheckBanned(title))
                {
                    context.Response.Write("No:内容或标题包含禁用词!!!");
                    return;
                }
                else if (senstitiveBll.CheckMod(content) || senstitiveBll.CheckMod(title))
                {
                    model.State = "待审核";
                    if (bll.Update(model))
                    {
                        context.Response.Write("Ok:内容或标题包含审核词,请等待审核!!!");
                    }
                }
                else
                {
                    if (bll.Update(model))
                    {
                        context.Response.Write("Ok:成功");
                    }
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