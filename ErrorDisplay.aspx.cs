using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WoWinfo
{
    public partial class ErrorDisplay : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            var errormessage = Session["ErrorMessage"];
            Literal1.Text = errormessage.ToString() ;
        }

    }
}