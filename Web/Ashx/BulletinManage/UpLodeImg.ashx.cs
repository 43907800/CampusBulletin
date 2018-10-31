using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.Ashx.BulletinManage
{
    /// <summary>
    /// UpLodeImg 的摘要说明
    /// </summary>
    public class UpLodeImg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            fileUpload(context);
        }


        #region 图片上传保存
        /// <summary>
        /// 图片上传保存到本地
        /// </summary>
        /// <param name="context"></param>
        private void fileUpload(HttpContext context)
        {
            HttpFileCollection collection = context.Request.Files;
            bool isSucess = false;
            if (collection.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExt = Path.GetExtension(fileName);
                    if (fileExt == ".jpg"|| fileExt == ".png" || fileExt == ".gif")
                    {
                        string dir = "/UpLoadImage/" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "/";
                        if (!Directory.Exists(context.Request.MapPath(dir)))
                        {
                            Directory.CreateDirectory(context.Request.MapPath(dir));
                        }
                        string newfileName = Guid.NewGuid().ToString();
                        string fullDir = dir + newfileName + fileExt;
                        file.SaveAs(context.Request.MapPath(fullDir));
                        isSucess = true;
                        using (Image img = Image.FromFile(context.Request.MapPath(fullDir)))
                        {
                            System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                           string json= javaScriptSerializer.Serialize(new { errno=0, data=new string[] { fullDir } });
                            context.Response.Write(json);
                            //context.Response.Write("ok:" + fullDir + ":" + img.Width + ":" + img.Height);
                        }
                        //file.SaveAs(context.Request.MapPath("/UploadImage/" + fileName));
                        //context.Response.Write("/UploadImage/" + fileName);
                    }
                }
            }
            if (!isSucess)
            {
                context.Response.Write("no:上传失败!!");
            }
        }
        #endregion


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}