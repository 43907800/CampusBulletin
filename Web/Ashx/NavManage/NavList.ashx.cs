using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx
{
    /// <summary>
    /// NavList 的摘要说明
    /// </summary>
    public class NavList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NavInfoBll navBll = new NavInfoBll();

            int pageIndex =int.Parse( context.Request["pageIndex"]??"1");
            int pageSize = 10;
            int total = navBll.GetNavCount();
            List<NavInfo> list = navBll.GetPageList(pageIndex, pageSize);
            string pageNavHtml = Common.PageNav.PageNavGenerate(pageIndex, pageSize, total);
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
           string jsonStr= json.Serialize(new { NavHtml = pageNavHtml, List = list });

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