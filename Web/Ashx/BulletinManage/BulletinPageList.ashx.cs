using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Web.Ashx.BulletinManage
{
    /// <summary>
    /// BulletinPageList 的摘要说明
    /// </summary>
    public class BulletinPageList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BulletinBll bll = new BulletinBll();
            //当前页码
            int pageIndex = int.Parse(context.Request["pageIndex"] ?? "1");
            string typeId = context.Request["typeId"];
            int pageSize = 5;//每页显示数量
            int total = bll.GetCount();//总数量
            //总页数
            int pageCount = Convert.ToInt32(Math.Ceiling(1.0 * total / pageSize));
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<Bulletin> list = new List<Bulletin>();
            string pageNavHtml = "";
            if (string.IsNullOrEmpty(typeId))
            {
                //分页  获取 所有 数据
                list = bll.GetPageList(pageIndex, pageSize);
                pageNavHtml = Common.PageNav.PageNavGenerate(pageIndex, pageSize, total);
            }
            else
            {
                //根据 类型 分页 获取  数据 
                list = bll.GetPageListByTypeId(pageIndex, pageSize, int.Parse(typeId ?? "0"));
                total = bll.GetCount(int.Parse(typeId??"0"));
                pageNavHtml = Common.PageNav.PageNavGenerate(pageIndex, pageSize, total, int.Parse(typeId ?? "0"));
            }
            UserBll userBll = new UserBll();
            foreach (var b in list)
            {
               b.User=userBll.GetModelByUserName(b.UserName);
            }
            // 分页 html 代码 
            //序列化成 json
            JavaScriptSerializer json = new JavaScriptSerializer();
            string jsonStr = json.Serialize(new { NavHtml = pageNavHtml, List = list });
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