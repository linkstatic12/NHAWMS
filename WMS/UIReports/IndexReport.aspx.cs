using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS.DataAccess;

namespace WMS.UIReports
{
    public partial class IndexReport : System.Web.UI.Page
    {

        private EmployeeDataAccess da = new DataAccess.EmployeeDataAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            BindGridView("");
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            BindGridView(TextBoxSearch.Text.Trim());
        }

        private void BindGridView(string search)
        {
            GridViewReport.DataSource = da.GetEmployees(search);
            GridViewReport.DataBind();
        }
    }
}