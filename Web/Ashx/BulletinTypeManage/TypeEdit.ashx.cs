using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.BulletinTypeManage
{
    /// <summary>
    /// TypeEdit 的摘要说明
    /// </summary>
    public class TypeEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BulletinTypeBll bll = new BulletinTypeBll();
            if (context.Request.RequestType.ToUpper() == "GET")
            {
                int id = int.Parse(context.Request["Id"] ?? "0");
                BulletinType type = bll.GetModelById(id);//根据Id 获取数据
                System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jsonStr = javaScriptSerializer.Serialize(type);//将对象序列化成json
                context.Response.Write(jsonStr);
            }
            else
            {
                BulletinType type = new BulletinType();
                type.Id =int.Parse( context.Request["EditId"]??"0");
                type.TypeName = context.Request["EditText"];
                if (bll.Update(type))
                {
                    context.Response.Write("Ok");
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