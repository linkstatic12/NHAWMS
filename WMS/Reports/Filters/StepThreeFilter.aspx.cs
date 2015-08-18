using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS.CustomClass;
using WMS.HelperClass;
using WMS.Models;
using WMSLibrary;

namespace WMS.Reports.Filters
{
    public partial class StepThreeFilter : System.Web.UI.Page
    {
        private TAS2013Entities da = new TAS2013Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind Grid View According to Filters
                BindGridViewDepartment("");
                BindGridViewLocation("");
                List<string> list = Session["ReportSession"] as List<string>;
                dateFrom.Value = list[0];
                dateTo.Value = list[1];
                //dateFrom.Value = "2015-08-09";
            }
            else
            {
                SaveDepartmentIDs();
                SaveTypeIDs();
            }
            if (Session["FiltersModel"] != null)
            {
                // Check and Uncheck Items in grid view according to Session Filters Model
                WMSLibrary.Filters.SetGridViewCheckState(GridViewWing, Session["FiltersModel"] as FiltersModel, "Department");
                WMSLibrary.Filters.SetGridViewCheckState(GridViewLocation, Session["FiltersModel"] as FiltersModel, "Location");
            }
        }
        protected void ButtonSearchWing_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDepartmentIDs();
            BindGridViewDepartment(tbSearch_Wing.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewWing, Session["FiltersModel"] as FiltersModel, "Department");
        }
        protected void ButtonSearchLocation_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveTypeIDs();
            BindGridViewLocation(tbSearch_Location.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewLocation, Session["FiltersModel"] as FiltersModel, "Location");
        }
        protected void GridViewWing_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDepartmentIDs();

            //change page index
            GridViewWing.PageIndex = e.NewPageIndex;
            BindGridViewDepartment("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewWing, Session["FiltersModel"] as FiltersModel, "Department");
        }

        protected void GridViewLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveTypeIDs();

            //change page index
            GridViewLocation.PageIndex = e.NewPageIndex;
            BindGridViewLocation("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewLocation, Session["FiltersModel"] as FiltersModel, "Location");
        }

        private void SaveDepartmentIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewWing, Session["FiltersModel"] as FiltersModel, "Department");
            Session["FiltersModel"] = FM;
        }
        private void SaveTypeIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewLocation, Session["FiltersModel"] as FiltersModel, "Location");
            Session["FiltersModel"] = FM;
        }

        private void BindGridViewDepartment(string search)
        {
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            List<ViewDepartment> _View = new List<ViewDepartment>();
            List<ViewDepartment> _TempView = new List<ViewDepartment>();
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForCompanyFilters(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from ViewDepartment " + query);
            _View = dt.ToList<ViewDepartment>();
            //if (fm.CompanyFilter.Count > 0)
            //{
            //    foreach (var comp in fm.CompanyFilter)
            //    {
            //        short _compID = Convert.ToInt16(comp.ID);
            //        _TempView.AddRange(_View.Where(aa => aa.CompID == _compID).ToList());
            //    }
            //    _View = _TempView.ToList();
            //}
            if (fm.DivisionFilter.Count > 0)
            {
                _TempView.Clear();
                foreach (var div in fm.DivisionFilter)
                {
                    short _divID = Convert.ToInt16(div.ID);
                    _TempView.AddRange(_View.Where(aa => aa.DivID == _divID).ToList());
                }
                _View = _TempView.ToList();
            }
            GridViewWing.DataSource = _View.Where(aa => aa.DeptName.Contains(search)).ToList();
            GridViewWing.DataBind();
        }

        private void BindGridViewLocation(string search)
        {
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            List<Location> _View = new List<Location>();
            List<Location> _TempView = new List<Location>();
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForLocationTableSegeration(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from Location " + query);
            _View = dt.ToList<Location>();
            if (fm.CompanyFilter.Count > 0)
            {
                foreach (var citi in fm.CityFilter)
                {
                    short _compID = Convert.ToInt16(citi.ID);
                    _TempView.AddRange(_View.Where(aa => aa.CityID == _compID).ToList());
                }
                _View = _TempView.ToList();
            }
            GridViewLocation.DataSource = _View.Where(aa => aa.LocName.Contains(search)).ToList();
            GridViewLocation.DataBind();
        }

        #region Navigation Buttons
        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            SaveDateSession();
            // Save selected Company ID and Name in Session
            SaveDepartmentIDs();
            SaveTypeIDs();
            // Go to the next page
            string url = "~/Reports/Filters/StepFourFilter.aspx";
            Response.Redirect(url);
        }
        protected void ButtonSkip_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDepartmentIDs();
            // Go to the next page
            string url = "~/Filters/DeptFilter.aspx";
            Response.Redirect(url);
        }
        protected void ButtonFinish_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveDepartmentIDs();
            SaveTypeIDs();
            // Go to the next page
            string url = "~/Reports/ReportContainer.aspx";
            Response.Redirect(url);
        }
        #endregion

        protected void GridViewWing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewWing.PageIndex + 1) + " of " + GridViewWing.PageCount;
            }
        }

        protected void GridViewLocation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewLocation.PageIndex + 1) + " of " + GridViewLocation.PageCount;
            }
        }
        public DateTime DateFrom
        {
            get
            {
                if (dateFrom.Value == "")
                    return DateTime.Today.Date.AddDays(-1);
                else
                    return DateTime.Parse(dateFrom.Value);
            }
        }
        public DateTime DateTo
        {
            get
            {
                if (dateTo.Value == "")
                    return DateTime.Today.Date.AddDays(-1);
                else
                    return DateTime.Parse(dateTo.Value);
            }
        }
        private void SaveDateSession()
        {
            List<string> list = Session["ReportSession"] as List<string>;
            list[0] = DateFrom.ToString("yyyy-MM-dd");
            list[1] = DateTo.ToString("yyyy-MM-dd");
            Session["ReportSession"] = list;
        }
        #region Navigation Buttons
        private void NavigationCommonCalls(string path)
        {
            SaveDateSession();
            SaveDepartmentIDs();
            SaveTypeIDs();
            Response.Redirect(path);
        }
        protected void btnStepOne_Click(object sender, EventArgs e)
        {
            NavigationCommonCalls("~/Reports/Filters/StepOneFilter.aspx");
        }

        protected void btnStepTwo_Click(object sender, EventArgs e)
        {
            NavigationCommonCalls("~/Reports/Filters/StepTwoFilter.aspx");
        }

        protected void btnStepThree_Click(object sender, EventArgs e)
        {
            NavigationCommonCalls("~/Reports/Filters/StepThreeFilter.aspx");
        }

        protected void btnStepFour_Click(object sender, EventArgs e)
        {
            NavigationCommonCalls("~/Reports/Filters/StepFourFilter.aspx");
        }

        protected void btnStepFive_Click(object sender, EventArgs e)
        {
            NavigationCommonCalls("~/Reports/Filters/StepFiveFilter.aspx");
        }

        protected void btnStepSix_Click(object sender, EventArgs e)
        {
            SaveDateSession();
            SaveDepartmentIDs();
            SaveTypeIDs();
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            if (MyHelper.UserHasValuesInSession(fm))
            {
                Response.Redirect("~/Reports/Filters/StepSixFilter.aspx");
            }
        }


        #endregion
    }
}