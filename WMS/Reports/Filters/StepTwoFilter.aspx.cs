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
    public partial class StepTwoFilter : System.Web.UI.Page
    {
        private TAS2013Entities da = new TAS2013Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Bind Grid View According to Filters
                BindGridViewCity("");
                BindGridViewEmployeeType("");
               // BindGridViewLocation("");
               // BindGridViewShift("");
                List<string> list = Session["ReportSession"] as List<string>;
                dateFrom.Value = list[0];
                dateTo.Value = list[1];
            }
            else
            {
                SaveCityIDs();
                SaveEmployeeTypeIDs();
                //SavelocationIDs();
                //SaveShiftIDs();
            }
            if (Session["FiltersModel"] != null)
            {
                // Check and Uncheck Items in grid view according to Session Filters Model
               // WMSLibrary.Filters.SetGridViewCheckState(GridViewLocation, Session["FiltersModel"] as FiltersModel, "Location");
               // WMSLibrary.Filters.SetGridViewCheckState(GridViewShift, Session["FiltersModel"] as FiltersModel, "Shift");
                WMSLibrary.Filters.SetGridViewCheckState(GridViewCity, Session["FiltersModel"] as FiltersModel, "City");
                WMSLibrary.Filters.SetGridViewCheckState(GridViewEmployeeType, Session["FiltersModel"] as FiltersModel, "Type");
            }
        }
        protected void ButtonSearchEmployeeType_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveEmployeeTypeIDs();
            BindGridViewEmployeeType(tbSearch_EmployeeType.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewEmployeeType, Session["FiltersModel"] as FiltersModel, "Type");
        }

        #region --DeleteAll Filters--
        protected void ButtonDeleteAll_Click(object sender, EventArgs e)
        {
            WMSLibrary.Filters filtersHelper = new WMSLibrary.Filters();
            Session["FiltersModel"] = filtersHelper.DeleteAllFilters(Session["FiltersModel"] as FiltersModel);

            WMSLibrary.Filters.SetGridViewCheckState(GridViewCity, Session["FiltersModel"] as FiltersModel, "City");
            WMSLibrary.Filters.SetGridViewCheckState(GridViewEmployeeType, Session["FiltersModel"] as FiltersModel, "Type");

        }
        #endregion

        protected void ButtonSearchCity_Click(object sender, EventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveCityIDs();
            BindGridViewCity(TextBoxSearchLocation.Text.Trim());
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewCity, Session["FiltersModel"] as FiltersModel, "City");
        }
        protected void GridViewCity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveCityIDs();

            //change page index
            GridViewCity.PageIndex = e.NewPageIndex;
            BindGridViewCity("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewCity, Session["FiltersModel"] as FiltersModel, "City");
        }

        protected void GridViewEmployeeType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Save selected Company ID and Name in Session
            SaveEmployeeTypeIDs();

            //change page index
            GridViewEmployeeType.PageIndex = e.NewPageIndex;
            BindGridViewEmployeeType("");
            // Check and set Check box state
            WMSLibrary.Filters.SetGridViewCheckState(GridViewEmployeeType, Session["FiltersModel"] as FiltersModel, "Type");
        }

        private void SaveEmployeeTypeIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewEmployeeType, Session["FiltersModel"] as FiltersModel, "Type");
            Session["FiltersModel"] = FM;
        }
        private void SaveCityIDs()
        {
            WMSLibrary.FiltersModel FM = WMSLibrary.Filters.SyncGridViewIDs(GridViewCity, Session["FiltersModel"] as FiltersModel, "City");
            Session["FiltersModel"] = FM;
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
        private void BindGridViewEmployeeType(string search)
        {
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            List<EmpView> _View = new List<EmpView>();
            List<EmpView> _TempView = new List<EmpView>();
            List<EmpType> _ViewType = new List<EmpType>();
           
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            
            DataTable dt = qb.GetValuesfromDB("select * from EmpView");
            _View = dt.ToList<EmpView>();
            if (fm.RegionFilter.Count() > 0)
            {
                foreach (var region in fm.RegionFilter)
                {
                    short _compid = Convert.ToInt16(region.ID);
                    _TempView.AddRange(_View.Where(aa => aa.RegionID == _compid).ToList());
                }
                _View = _TempView.ToList();
            }
            if (fm.ShiftFilter.Count() > 0)
            {
                foreach (var shift in fm.ShiftFilter)
                {
                    short _shiftid = Convert.ToInt16(shift.ID);
                    _TempView.AddRange(_View.Where(aa => aa.ShiftID == _shiftid).ToList());
                }
                _View = _TempView.ToList();
            
            
            }
            if (fm.CityFilter.Count() > 0)
            {

                foreach (var city in fm.CityFilter)
                {
                    short _cityid = Convert.ToInt16(city.ID);
                    _TempView.AddRange(_View.Where(aa => aa.CityID == _cityid).ToList());
                }
                _View = _TempView.ToList();
            
            
            }
            //get only the distinct objects from above so we can now search empType from empView
            _View = _View.Distinct().ToList();
            ;
            foreach( var Eval in _View)
            {
                Boolean addInList = true;
                foreach (var test in _ViewType)
                       {
                           if (test.TypeID == Eval.TypeID)
                           {
                               addInList = false;
                               break;
                           }
                       }
                if(addInList)
                _ViewType.Add(da.EmpTypes.Where(aa => aa.TypeID == Eval.TypeID).FirstOrDefault());
            
            }

            GridViewEmployeeType.DataSource = _ViewType.Where(aa => aa.TypeName.ToUpper().Contains(search.ToUpper())).ToList(); ;
            GridViewEmployeeType.DataBind();
          
        }

        private void SaveDateSession()
        {
            List<string> list = Session["ReportSession"] as List<string>;
            list[0] = DateFrom.ToString("yyyy-MM-dd");
            list[1] = DateTo.ToString("yyyy-MM-dd");
            Session["ReportSession"] = list;
        }

        private void BindGridViewCity(string search)
        {
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            List<City> _View = new List<City>();
            List<City> _TempView = new List<City>();
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
           // string query = qb.QueryForUserAccess(LoggedInUser,"City");
            DataTable dt = qb.GetValuesfromDB("select * from City");
            _View = dt.ToList<City>();
            if (fm.RegionFilter.Count > 0)
            {
                foreach (var reg in fm.RegionFilter)
                {
                    short _regID = Convert.ToInt16(reg.ID);
                    _TempView.AddRange(_View.Where(aa => aa.RegionID == _regID).ToList());
                }
                _View = _TempView.ToList();
            }
            GridViewCity.DataSource = _View.Where(aa => aa.CityName.ToUpper().Contains(search.ToUpper())).ToList(); 
            GridViewCity.DataBind();
           
        }


        protected void GridViewCity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewCity.PageIndex + 1) + " of " + GridViewCity.PageCount;
            }
        }

        protected void GridViewEmployeeType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Page " + (GridViewEmployeeType.PageIndex + 1) + " of " + GridViewEmployeeType.PageCount;
            }
        }
        #region Navigation Buttons
        private void NavigationCommonCalls(string path)
        {
            SaveDateSession();
            SaveCityIDs();
            SaveEmployeeTypeIDs();
            //SavelocationIDs();
            //SaveShiftIDs();
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
            SaveCityIDs();
            SaveEmployeeTypeIDs();
            //SaveShiftIDs();
            //SavelocationIDs();
            FiltersModel fm = Session["FiltersModel"] as FiltersModel;
            if (MyHelper.UserHasValuesInSession(fm))
            {
                Response.Redirect("~/Reports/Filters/StepSixFilter.aspx");
            }
        }


        #endregion
    }
}