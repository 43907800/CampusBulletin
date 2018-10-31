using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.IndexPage
{
    public partial class IndexParent : System.Web.UI.MasterPage
    {
       public List<NavInfo> navList;
        public UserInfo userInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            NavInfoBll navBll = new NavInfoBll();
            navList = navBll.GetAllNav();
            userInfo =  Session["userInfo"] as UserInfo;
        }
    }
}