using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS.Models;

namespace WMS.Reports.Graph
{
    public partial class Graphs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGridViewLocation("");
            }
        }
        TAS2013Entities db = new TAS2013Entities();
        private void BindGridViewLocation(string search)
        {
            GridViewLocation.DataSource = db.Locations.ToList();
            GridViewLocation.DataBind();
        }
    }
}