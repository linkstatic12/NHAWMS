using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS.CustomClass;
using WMS.Models;
using WMSLibrary;

namespace WMS.Reports
{
    public partial class SummaryReports : System.Web.UI.Page
    {
        String title = "";
        string _dateFrom = "";
        List<EmpPhoto> companyimage = new List<EmpPhoto>();

        TAS2013Entities contex = new TAS2013Entities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String reportName = Request.QueryString["reportname"];
                List<string> list = Session["ReportSession"] as List<string>;
                FiltersModel fm = Session["FiltersModel"] as FiltersModel;
                User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
                QueryBuilder qb = new QueryBuilder();
                string query = qb.MakeCustomizeQuery(LoggedInUser);
                _dateFrom = list[0];
                string _dateTo = list[1];
                DateTime DateFrom = Convert.ToDateTime(list[0]);
                DateTime DateTo = Convert.ToDateTime(list[1]);
                DateFrom =DateFrom.AddDays(1);
             
                string PathString = "";
                string consolidatedMonth = "";
                switch (reportName)
                {
                    case "company_consolidated":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSConsolidated.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSConsolidated.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "R"), _dateFrom + " TO " + _dateTo, "Company Consolidated Summary");
                        break;
                    case "company_strength":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "R"), _dateFrom + " TO " + _dateTo, "Company Strength Summary");
                        break;
                    case "company_worktimes":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSWorkSummary.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSWorkSummary.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "R"), _dateFrom + " TO " + _dateTo, "Company Work Times Summary");
                        break;
                    case "location_consolidated":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSConsolidated.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSConsolidated.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "L"), _dateFrom + " TO " + _dateTo, "Location Consolidated Summary");
                        break;
                    case "location_strength":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "L"), _dateFrom + " TO " + _dateTo, "Location Strength Summary");
                        break;
                    case "location_worktimes":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSWorkSummary.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSWorkSummary.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "L"), _dateFrom + " TO " + _dateTo, "Location Work Times Summary");
                        break;
                    case "shift_consolidated":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSConsolidated.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSConsolidated.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "S"), _dateFrom + " TO " + _dateTo, "Shift Consolidated Summary");
                        break;
                    case "shift_strength":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "S"), _dateFrom + " TO " + _dateTo, "Shift Strength Summary");
                        break;
                    case "shift_worktimes":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSWorkSummary.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSWorkSummary.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "S"), _dateFrom + " TO " + _dateTo, "Shift Work Times Summary");
                        break;
                    case "category_consolidated":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSConsolidated.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSConsolidated.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "A"), _dateFrom + " TO " + _dateTo, "Category Consolidated Summary");
                        break;
                    case "category_strength":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "A"), _dateFrom + " TO " + _dateTo, "Category Strength Summary");
                        break;
                    case "category_worktimes":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSWorkSummary.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSWorkSummary.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "A"), _dateFrom + " TO " + _dateTo, "Category Work Times Summary");
                        break;
                    case "type_consolidated":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSConsolidated.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSConsolidated.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "T"), _dateFrom + " TO " + _dateTo, "Employee Type Consolidated Summary");
                        break;
                    case "type_strength":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "T"), _dateFrom + " TO " + _dateTo, "Employee Type Strength Summary");
                        break;
                    case "type_worktimes":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSWorkSummary.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSWorkSummary.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "T"), _dateFrom + " TO " + _dateTo, "Employee Type Work Times Summary");
                        break;
                    case "dept_consolidated":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSConsolidated.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSConsolidated.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "D"), _dateFrom + " TO " + _dateTo, "Department Consolidated Summary");
                        break;
                    case "dept_strength":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "D"), _dateFrom + " TO " + _dateTo, "Department Strength Summary");
                        break;
                    case "dept_worktimes":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSWorkSummary.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSWorkSummary.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "D"), _dateFrom + " TO " + _dateTo, "Department Work Times Summary");
                        break;
                    case "section_consolidated":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSConsolidated.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSConsolidated.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "E"), _dateFrom + " TO " + _dateTo, "Section Consolidated Summary");
                        break;
                    case "section_strength":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "E"), _dateFrom + " TO " + _dateTo, "Section Strength Summary");
                        break;
                    case "section_worktimes":
                        if (GlobalVariables.DeploymentType == false)
                            PathString = "/Reports/RDLC/DSWorkSummary.rdlc";
                        else
                            PathString = "/WMS/Reports/RDLC/DSWorkSummary.rdlc";
                        LoadReport(PathString, ReportsFilterImplementation(fm, _dateFrom, _dateTo, "E"), _dateFrom + " TO " + _dateTo, "Section Work Times Summary");
                        break;


                }
            }
        }

        private void LoadReport(string PathString, List<DailySummary> list, string p)
        {
            string _Header = title;
            this.ReportViewer1.LocalReport.DisplayName = title;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(PathString);
            System.Security.PermissionSet sec = new System.Security.PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
            ReportViewer1.LocalReport.SetBasePermissionsForSandboxAppDomain(sec);
            IEnumerable<DailySummary> ie;
            ie = list.AsQueryable();
            IEnumerable<DailySummary> companyImage;
            ReportDataSource datasource1 = new ReportDataSource("DataSet1", ie);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.DataSources.Add(datasource1);
            ReportParameter rp = new ReportParameter("Date", p, false);
            ReportParameter rp1 = new ReportParameter("Header", _Header, false);
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp });
            ReportViewer1.LocalReport.Refresh();
        }

        private List<DailySummary> ReportsFilterImplementation(FiltersModel fm, List<DailySummary> _TempViewList, List<DailySummary> _ViewList)
        {
            //for company
            if (fm.CompanyFilter.Count > 0)
            {
                foreach (var comp in fm.CompanyFilter)
                {
                    short _compID = Convert.ToInt16(comp.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "C" && aa.Criteria == comp.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();



            //for location
            if (fm.LocationFilter.Count > 0)
            {
                foreach (var loc in fm.LocationFilter)
                {
                    short _locID = Convert.ToInt16(loc.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "L" && aa.Criteria == loc.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for shifts
            if (fm.ShiftFilter.Count > 0)
            {
                foreach (var shift in fm.ShiftFilter)
                {
                    short _shiftID = Convert.ToInt16(shift.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "S" && aa.Criteria == shift.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();


            _TempViewList.Clear();

            //for type
            if (fm.TypeFilter.Count > 0)
            {
                foreach (var type in fm.TypeFilter)
                {
                    short _typeID = Convert.ToInt16(type.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "T" && aa.Criteria == type.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            ////for crews
            //if (fm.CrewFilter.Count > 0)
            //{
            //    foreach (var cre in fm.CrewFilter)
            //    {
            //        short _crewID = Convert.ToInt16(cre.ID);
            //        _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "C" && aa.Criteria == loc.ID).ToList());
            //    }
            //    _ViewList = _TempViewList.ToList();
            //}
            //else
            //    _TempViewList = _ViewList.ToList();
            //_TempViewList.Clear();





            ////for division
            //if (fm.DivisionFilter.Count > 0)
            //{
            //    foreach (var div in fm.DivisionFilter)
            //    {
            //        //baldi doesnt have the division id so using division name in
            //        _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "L" && aa.Criteria == loc.ID).ToList());
            //    }
            //    _ViewList = _TempViewList.ToList();
            //}
            //else
            //    _TempViewList = _ViewList.ToList();
            //_TempViewList.Clear();

            //for department
            if (fm.DepartmentFilter.Count > 0)
            {
                foreach (var dept in fm.DepartmentFilter)
                {
                    short _deptID = Convert.ToInt16(dept.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "D" && aa.Criteria == dept.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for sections
            if (fm.SectionFilter.Count > 0)
            {
                foreach (var sec in fm.SectionFilter)
                {
                    short _secID = Convert.ToInt16(sec.ID);
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "E" && aa.Criteria == sec.ID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            ////Employee
            //if (fm.EmployeeFilter.Count > 0)
            //{
            //    foreach (var emp in fm.EmployeeFilter)
            //    {
            //        int _empID = Convert.ToInt32(emp.ID);
            //        _TempViewList.AddRange(_ViewList.Where(aa => aa.Criteria == "L" && aa.Criteria == loc.ID).ToList());
            //    }
            //    _ViewList = _TempViewList.ToList();
            //}
            //else
            //    _TempViewList = _ViewList.ToList();
            //_TempViewList.Clear();


            return _ViewList;
        }
        private List<DailySummary> ReportsFilterImplementation(FiltersModel fm, string dateFrom, string dateTo, string Criteria)
        {
            List<DailySummary> ViewDS = new List<DailySummary>();
            List<DailySummary> TempDS = new List<DailySummary>();
            QueryBuilder qb = new QueryBuilder();
            DataTable dt = new DataTable();
            switch (Criteria)
            {
                case "R":
                    //for company
                    dt = qb.GetValuesfromDB("select * from DailySummary " + " where Criteria = '" + Criteria + "' and (Date >= " + "'" + dateFrom + "'" + " and Date <= " + "'"
                                                     + dateTo + "'" + " )");
                    ViewDS = dt.ToList<DailySummary>();
                    if (fm.RegionFilter.Count > 0)
                    {
                        foreach (var comp in fm.RegionFilter)
                        {
                            short _compID = Convert.ToInt16(comp.ID);
                            TempDS.AddRange(ViewDS.Where(aa => aa.CriteriaValue == _compID && aa.Criteria == Criteria).ToList());
                        }
                        ViewDS = TempDS.ToList();
                    }
                    else
                    {
                        TempDS = ViewDS.ToList();
                        ViewDS.Clear();
                    }
                    TempDS.Clear();
                    break;
                case "L":
                    dt = qb.GetValuesfromDB("select * from DailySummary " + " where Criteria = '" + Criteria + "' and (Date >= " + "'" + dateFrom + "'" + " and Date <= " + "'"
                                                     + dateTo + "'" + " )");
                    ViewDS = dt.ToList<DailySummary>();
                    if (fm.LocationFilter.Count > 0)
                    {
                        foreach (var loc in fm.LocationFilter)
                        {
                            short _locID = Convert.ToInt16(loc.ID);
                            TempDS.AddRange(ViewDS.Where(aa => aa.CriteriaValue == _locID && aa.Criteria == Criteria).ToList());
                        }
                        ViewDS = TempDS.ToList();
                    }
                    else
                        ViewDS.Clear();
                    TempDS.Clear();
                    break;
                case "D":
                    dt = qb.GetValuesfromDB("select * from DailySummary " + " where Criteria = '" + Criteria + "' and (Date >= " + "'" + dateFrom + "'" + " and Date <= " + "'"
                                                     + dateTo + "'" + " )");
                    ViewDS = dt.ToList<DailySummary>();
                    if (fm.DepartmentFilter.Count > 0)
                    {
                        foreach (var dept in fm.DepartmentFilter)
                        {
                            short _deptID = Convert.ToInt16(dept.ID);
                            TempDS.AddRange(ViewDS.Where(aa => aa.CriteriaValue == _deptID && aa.Criteria == Criteria).ToList());
                        }
                        ViewDS = TempDS.ToList();
                    }
                    else
                        ViewDS.Clear();
                    TempDS.Clear();
                    break;
                case "E":
                    dt = qb.GetValuesfromDB("select * from DailySummary " + " where Criteria = '" + Criteria + "' and (Date >= " + "'" + dateFrom + "'" + " and Date <= " + "'"
                                                     + dateTo + "'" + " )");
                    ViewDS = dt.ToList<DailySummary>();
                    if (fm.SectionFilter.Count > 0)
                    {
                        foreach (var sec in fm.SectionFilter)
                        {
                            short _secID = Convert.ToInt16(sec.ID);
                            TempDS.AddRange(ViewDS.Where(aa => aa.CriteriaValue == _secID && aa.Criteria == Criteria).ToList());
                        }
                        ViewDS = TempDS.ToList();
                    }
                    else
                        ViewDS.Clear();
                    TempDS.Clear();
                    break;

                case "S":
                    dt = qb.GetValuesfromDB("select * from DailySummary " + " where Criteria = '" + Criteria + "' and (Date >= " + "'" + dateFrom + "'" + " and Date <= " + "'"
                                                     + dateTo + "'" + " )");
                    ViewDS = dt.ToList<DailySummary>();
                    if (fm.ShiftFilter.Count > 0)
                    {
                        foreach (var shift in fm.ShiftFilter)
                        {
                            short _shiftID = Convert.ToInt16(shift.ID);
                            TempDS.AddRange(ViewDS.Where(aa => aa.CriteriaValue == _shiftID && aa.Criteria == Criteria).ToList());
                        }
                        ViewDS = TempDS.ToList();
                    }
                    else
                        ViewDS.Clear();
                    TempDS.Clear();
                    break;
                case "T":
                    dt = qb.GetValuesfromDB("select * from DailySummary " + " where Criteria = '" + Criteria + "' and (Date >= " + "'" + dateFrom + "'" + " and Date <= " + "'"
                                                     + dateTo + "'" + " )");
                    ViewDS = dt.ToList<DailySummary>();
                    if (fm.TypeFilter.Count > 0)
                    {
                        foreach (var type in fm.TypeFilter)
                        {
                            short _typeID = Convert.ToInt16(type.ID);
                            TempDS.AddRange(ViewDS.Where(aa => aa.CriteriaValue == _typeID && aa.Criteria == Criteria).ToList());
                        }
                        ViewDS = TempDS.ToList();
                    }
                    else
                        ViewDS.Clear();
                    TempDS.Clear();
                    break;
                //case "A":
                //    if (fm.CompanyFilter.Count > 0)
                //    {
                //        foreach (var comp in fm.CompanyFilter)
                //        {
                //            short _compID = Convert.ToInt16(comp.ID);
                //            TempDS.AddRange(ViewDS.Where(aa => aa.CriteriaValue == _compID && aa.Criteria == Criteria).ToList());
                //        }
                //        ViewDS = TempDS.ToList();
                //    }
                //    else
                //        TempDS = ViewDS.ToList();
                //    TempDS.Clear();
                //    break;
            }
            return ViewDS;
        }
        private void LoadReport(string PathString, List<DailySummary> _ViewListSum, string date, string title)
        {
            string _Header = title;
            this.ReportViewer1.LocalReport.DisplayName = title;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(PathString);
            System.Security.PermissionSet sec = new System.Security.PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
            ReportViewer1.LocalReport.SetBasePermissionsForSandboxAppDomain(sec);
            ReportDataSource datasource1 = new ReportDataSource("DataSet1", _ViewListSum);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource1);
            ReportParameter rp = new ReportParameter("CompanyName", "National Highway Authority", false);
            ReportParameter rp1 = new ReportParameter("Title", title, false);
            ReportParameter rp2 = new ReportParameter("Date", date, false);
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp, rp1, rp2 });
            ReportViewer1.LocalReport.Refresh();
        }
    }
}