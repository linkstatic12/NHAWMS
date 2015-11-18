using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WMS.Reports.Filters
{
    public partial class SummaryReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<string> list = Session["ReportSession"] as List<string>;
                //dateFrom.Value = list[0];
                //dateTo.Value = list[1];
            }
        }

        //public DateTime DateFrom
        //{
        //    get
        //    {
        //        if (dateFrom.Value == "")
        //            return DateTime.Today.Date.AddDays(0);
        //        else
        //            return DateTime.Parse(dateFrom.Value);
        //    }
        //    set
        //    {
        //        List<string> list = Session["ReportSession"] as List<string>;
        //        list[0] = DateFrom.ToString("yyyy-MM-dd");
        //        list[1] = DateTo.ToString("yyyy-MM-dd");
        //        Session["ReportSession"] = list;
        //    }
        //}
        //public DateTime DateTo
        //{
        //    get
        //    {
        //        if (dateTo.Value == "")
        //            return DateTime.Today.Date.AddDays(0);
        //        else
        //            return DateTime.Parse(dateTo.Value);
        //    }
        //    set
        //    {
        //        List<string> list = Session["ReportSession"] as List<string>;
        //        list[0] = DateFrom.ToString("yyyy-MM-dd");
        //        list[1] = DateTo.ToString("yyyy-MM-dd");
        //        Session["ReportSession"] = list;
        //    }
        //}
    }
}