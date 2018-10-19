using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Ashx.RegionManage
{
    /// <summary>
    /// FindRegion 的摘要说明
    /// </summary>
    public class FindRegion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //获取Pid
            int  pid = int.Parse(context.Request["Pid"] ?? "0");

           
            RegionBll bll = new RegionBll();
            //调用 bll层 获取数据
            List<Region>  list=bll.getListByPid(pid);
            //序列化成  json
            System.Web.Script.Serialization.JavaScriptSerializer javaScrpitSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string json= javaScrpitSerializer.Serialize(list);
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