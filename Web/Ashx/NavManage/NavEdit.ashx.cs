using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.NavManage
{
    /// <summary>
    /// NavEdit 的摘要说明
    /// </summary>
    public class NavEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NavInfoBll navBll = new NavInfoBll();
            context.Response.ContentType = "text/plain";
            if (context.Request.RequestType.ToUpper() == "GET")
            {
                int id = int.Parse(context.Request["Id"] ?? "0");
                NavInfo nav = navBll.GetModelById(id);//根据Id 获取数据
                System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jsonStr = javaScriptSerializer.Serialize(nav);//将对象序列化成json
                context.Response.Write(jsonStr);
            }
            else
            {
                NavInfo editNav = new NavInfo();
                //获取前端数据
                editNav.Id=int.Parse(context.Request["EditId"] ??"0");
                editNav.Oder = int.Parse(context.Request["EditOder"]??"99");
                editNav.Text = context.Request["EditText"];
                editNav.Address = context.Request["EditAddress"];
                editNav.Remark = context.Request["EditRemark"];

                //更新数据
                if (navBll.Update(editNav))
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