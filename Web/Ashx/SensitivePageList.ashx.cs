using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Web.Ashx
{
    /// <summary>
    /// SensitivePageList 的摘要说明
    /// </summary>
    public class SensitivePageList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            SensitiveBll bll = new SensitiveBll();
            //当前页码
            int pageIndex = int.Parse(context.Request["pageIndex"] ?? "1");
            int pageSize = 10;//每页显示数量
            int total = bll.GetCount();//总数量
            //总页数
            int pageCount = Convert.ToInt32(Math.Ceiling(1.0 * total / pageSize));
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;

            //获取 数据
            List<Sensitive> list = bll.GetPageList(pageIndex, pageSize);
            // 分页 html 代码 
            string pageNavHtml = Common.PageNav.PageNavGenerate(pageIndex,pageSize,total);
            //序列化成 json
            JavaScriptSerializer json = new JavaScriptSerializer();
            string jsonStr= json.Serialize(new {NavHtml=pageNavHtml,List=list });
            //发送到前端
            context.Response.Write(jsonStr);
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