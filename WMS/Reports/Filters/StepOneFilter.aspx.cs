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
    public partial class StepOneFilter : System.Web.UI.Page
    {
        private TAS2013Entities da = new TAS2013Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Bind Grid View According to Filters
                BindGridView("");
                BindGridViewRegion("");
                List<string> list = Session["ReportSession"] as List<string>;
                dateFrom.Value = list[0];
                dateTo.Value = list[1];
            }
            else
            {
                SaveShiftIDs();
                SaveRegionIDs();
            }
            if (Session["FiltersModel"] != null)
            {
                //1.Write a function that checks the checkbox state 
                //2.Save the unchecked checkboxes to the session



                // Check and Uncheck Items in grid view according to Session Filters Model
              //  WMSLibrary.Filters.SetGridViewCheckState(GridViewCompany, Session["FiltersModel"] as FiltersModel, "Company");
                WMSLibrary.Filters.SetGridViewCheckState(GridViewRegion, Session["FiltersModel"] as FiltersModel, "Region");
                WMSLibrary.Filters.SetGridViewCheckState(GridViewShift, Session["FiltersModel"] as FiltersModel, "Shift");
            }
        }

        #region --GridView Company--
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            string date = dateFrom.Value.ToString();
            // Save selected Company ID and Name in Session
            SaveCompanyIDs();
           // BindGridView(TextBoxSearch.Text.Trim());
            // Check and set Check box state
           // WMSLibrary.Filters.SetGridViewCheckState(GridViewCompany, Session["FiltersModel"] as FiltersModel, "Company");
        }

        protected void GridViewShift_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveShiftIDs();

            //change page index
           // GridViewCompany.PageIndex = e.NewPageIndex;
            BindGridView("");
            // Check and set Check box state
           // WMSLibrary.Filters.SetGridViewCheckState(GridViewCompany, Session["FiltersModel"] as FiltersModel, "Company");
        }

        protected void GridViewShift_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
             //   e.Row.Cells[0].Text = "Page " + (GridViewCompany.PageIndex + 1) + " of " + GridViewCompany.PageCount;
            }
        }

        private void SaveShiftIDs()
        {
           WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewShift, Session["FiltersModel"] as FiltersModel, "Shift");
           Session["FiltersModel"] = FM;
        }
        private void SaveCompanyIDs()
        {
           // WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewCompany, Session["FiltersModel"] as FiltersModel, "Company");
           // Session["FiltersModel"] = FM;
        }

        private void BindGridView(string search)
        {
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            //string query = qb.QueryForCompanyView(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from Shift ");
            List<Shift> _View = dt.ToList<Shift>();
            GridViewShift.DataSource = _View.Where(aa => aa.ShiftName.ToUpper().Contains(search.ToUpper())).ToList();
            GridViewShift.DataBind();
        }

        #endregion
        protected void ButtonSearchShift_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveRegionIDs();
            BindGridView(tbSearch_Shift.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewRegion, Session["FiltersModel"] as FiltersModel, "Shift");
        }
        #region --GridView Region--
        protected void ButtonSearchRegion_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveRegionIDs();
            BindGridViewRegion(tbSearch_Region.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewRegion, Session["FiltersModel"] as FiltersModel, "Region");
        }
        #region --DeleteAll Filters--
        protected void ButtonDeleteAll_Click(object sender, EventArgs e)
        {
            WMSLibrary.Filters filtersHelper = new WMSLibrary.Filters();
            Session["FiltersModel"] = filtersHelper.DeleteAllFilters(Session["FiltersModel"] as FiltersModel);

            WMSLibrary.Filters.SetGridViewCheckState(GridViewRegion, Session["FiltersModel"] as FiltersModel, "Region");           
            WMSLibrary.Filters.SetGridViewCheckState(GridViewShift, Session["FiltersModel"] as FiltersModel, "Shift");
            
        }
        #endregion
        protected void GridViewRegion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveRegionIDs();

            //change page index
            GridViewRegion.PageIndex = e.NewPageIndex;
            BindGridViewRegion("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewRegion, Session["FiltersModel"] as FiltersModel, "Region");
        }

        protected void GridViewRegion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewRegion.PageIndex + 1) + " of " + GridViewRegion.PageCount;
            }
        }

        private void SaveRegionIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewRegion, Session["FiltersModel"] as FiltersModel, "Region");
            Session["FiltersModel"] = FM;
        }

        private void BindGridViewRegion(string search)
        {
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            //string query = qb.QueryForLocationTableSegeration(LoggedInUser);
            DataTable dt = qb.GetValuesfromDB("select * from Region ");
            List<Region> _View = dt.ToList<Region>();
            GridViewRegion.DataSource = _View.Where(aa => aa.RegionName.ToUpper().Contains(search.ToUpper())).ToList();
            GridViewRegion.DataBind();
        }

        #endregion

        private void SaveDateSession()
        {
            List<string> list = Session["ReportSession"] as List<string>;
            list[0] = DateFrom.ToString("yyyy-MM-dd");
            list[1] = DateTo.ToString("yyyy-MM-dd");
            Session["ReportSession"] = list;
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

        #region Navigation Buttons
        private void NavigationCommonCalls(string path)
        {
            SaveDateSession();
            //SaveCompanyIDs();
            SaveRegionIDs();
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
            //SaveCompanyIDs();
            SaveRegionIDs();
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            if (MyHelper.UserHasValuesInSession(fm))
            {
                Response.Redirect("~/Reports/Filters/StepSixFilter.aspx");
            }
        }


        #endregion





    }
}