using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.admin
{
    public partial class Index : System.Web.UI.Page
    {
        public string adminName { get; set; }

        AdminInfoBll bll = new AdminInfoBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            AdminInfo model = Session["admin"] as AdminInfo;
            if (model==null)
            {
                string userName = Request["UserName"];
                string pwd = Request["Password"];
                model= bll.Login(userName,pwd);
                if (model==null)
                {
                    Response.Write("<scrept>alert('账号或密码错误!')</scrept>");
                    Response.Redirect("/Admin.html");
                }
            }
            adminName = model.UserName;
        }
    }
}