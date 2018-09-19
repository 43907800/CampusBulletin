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
            if (model==null)Response.Redirect("/Admin.html");
            else adminName = model.UserName;
        }
    }
}