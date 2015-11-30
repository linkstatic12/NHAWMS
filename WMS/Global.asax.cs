using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WMS.CustomClass;

namespace WMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            // Initialize Session["FiltersModel"] -- Move to First Page
            Session["FiltersModel"] = new WMSLibrary.FiltersModel();
            Session["LoginCount"] = null;
            LoadSessionValues();
            //LoadSession();
        }


        protected void Session_Start()
        {
            Session["FiltersModel"] = new WMSLibrary.FiltersModel();
            Session["CustomFilter"] = new DataAccess.CustomFilter();
            LoadSessionValues();
        }
        private void LoadSessionValues()
        {
            Session["ReportSession"] = new List<string>();
            List<string> list = Session["ReportSession"] as List<string>;
            list.Add(DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"));
            list.Add(DateTime.Today.ToString("yyyy-MM-dd"));
            list.Add("EmpView");
            Session["ReportSession"] = list;
        }
        protected void Session_End(object sender, EventArgs e)
        {
            Session["FiltersModel"] = null;
            Session["LogedUserID"] = null;
            Session["LoggedUser"] = null;
        }
        protected void Session_End()
        {
            Session["FiltersModel"] = null;
            Session["LogedUserID"] = null;
            Session["LoggedUser"] = null;
        }
    }
}
