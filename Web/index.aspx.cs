using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class index : System.Web.UI.Page
    {
        public string userName;
        public string userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            userName=(Session["userInfo"] as UserInfo) != null ? (Session["userInfo"] as UserInfo).UserName : "";
            userId= (Session["userInfo"] as UserInfo) != null ? (Session["userInfo"] as UserInfo).Id.ToString() : "";
        }
    }
}