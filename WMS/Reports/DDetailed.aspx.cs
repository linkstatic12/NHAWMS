using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using WMS.CustomClass;
using WMS.Models;

namespace WMS.Reports
{
    public partial class DDetailed : System.Web.UI.Page
    {
        string PathString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
             
                DateTime date = DateTime.Today.Date.AddDays(-1);
                if (GlobalVariables.DeploymentType == false)
                {
                    PathString = "/Reports/RDLC/DRDetailed.rdlc";
                }
                else
                    PathString = "/WMS/Reports/RDLC/DRDetailed.rdlc";
                //List<ViewMultipleInOut> _View = new List<ViewMultipleInOut>();
                User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
                QueryBuilder qb = new QueryBuilder();
                string query = qb.MakeCustomizeQuery(LoggedInUser);
                DataTable dt = qb.GetValuesfromDB("select * from ViewMultipleInOut " + query +" and AttDate = '"+ date.Date.Year.ToString()+"-"+date.Date.Month.ToString()+"-"+date.Date.Day.ToString()+"'");
                List<ViewMultipleInOut> _View = dt.ToList<ViewMultipleInOut>();
                LoadReport(PathString, _View);
            }
        }
       

        
        //#region --CheckBoxes Changed Events --
        ////for sections
        //protected void chkCtrlSection_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in grid_Section.Rows)
        //    {
        //        CheckBox ck = ((CheckBox)row.FindControl("chkCtrlSection"));
        //        Section _sec = new Section();
        //        _sec.SectionID = Convert.ToInt16(row.Cells[1].Text);
        //        _sec.SectionName = row.Cells[2].Text;
        //        if (ck.Checked)
        //        {
        //            if (SelectedSections.Where(sec => sec.SectionID == _sec.SectionID).Count() == 0)
        //                SelectedSections.Add(_sec);
        //        }
        //        else
        //        {
        //            if (SelectedSections.Where(sec => sec.SectionID == _sec.SectionID).Count() > 0)
        //            {
        //                var _section = SelectedSections.Where(sec => sec.SectionID == _sec.SectionID).First();
        //                SelectedSections.Remove(_section);
        //            }
        //        }
        //    }
        //}

        ////for departments
        //protected void chkCtrlDept_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in grid_Dept.Rows)
        //    {
        //        CheckBox ck = ((CheckBox)row.FindControl("chkCtrlDept"));
        //        Department _dept = new Department();
        //        _dept.DeptID = Convert.ToInt16(row.Cells[1].Text);
        //        _dept.DeptName = row.Cells[2].Text;
        //        if (ck.Checked)
        //        {
        //            if (SelectedDepts.Where(aa => aa.DeptID == _dept.DeptID).Count() == 0)
        //                SelectedDepts.Add(_dept);
        //        }
        //        else
        //        {
        //            if (SelectedDepts.Where(aa => aa.DeptID == _dept.DeptID).Count() > 0)
        //            {
        //                var dept = SelectedDepts.Where(aa => aa.DeptID == _dept.DeptID).First();
        //                SelectedDepts.Remove(dept);
        //            }
        //        }
        //    }
        //}

        ////for shift
        //protected void chkCtrlShift_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in grid_Shift.Rows)
        //    {
        //        CheckBox ck = ((CheckBox)row.FindControl("chkCtrlShift"));
        //        Shift _Shift = new Shift();
        //        _Shift.ShiftID = Convert.ToByte(row.Cells[1].Text);
        //        _Shift.ShiftName = row.Cells[2].Text;
        //        if (ck.Checked)
        //        {
        //            if (SelectedShifts.Where(aa => aa.ShiftID == _Shift.ShiftID).Count() == 0)
        //                SelectedShifts.Add(_Shift);
        //        }
        //        else
        //        {
        //            if (SelectedShifts.Where(aa => aa.ShiftID == _Shift.ShiftID).Count() > 0)
        //            {
        //                var shift = SelectedShifts.Where(aa => aa.ShiftID == _Shift.ShiftID).First();
        //                SelectedShifts.Remove(shift);
        //            }
        //        }
        //    }
        //}
        ////For employees
        //protected void chkCtrlEmp_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in grid_Employee.Rows)
        //    {
        //        CheckBox ck = ((CheckBox)row.FindControl("chkCtrlEmp"));
        //        EmpView _EmpView = new EmpView();
        //        _EmpView.EmpID = Convert.ToInt32(row.Cells[1].Text);
        //        _EmpView.EmpName = row.Cells[3].Text;
        //        if (ck.Checked)
        //        {
        //            if (SelectedEmps.Where(aa => aa.EmpID == _EmpView.EmpID).Count() == 0)
        //                SelectedEmps.Add(_EmpView);
        //        }
        //        else
        //        {
        //            if (SelectedEmps.Where(aa => aa.EmpID == _EmpView.EmpID).Count() > 0)
        //            {
        //                var _emp = SelectedEmps.Where(aa => aa.EmpID == _EmpView.EmpID).First();
        //                SelectedEmps.Remove(_emp);
        //            }
        //        }
        //    }
        //}
        ////For crew
        //protected void chkCtrlCrew_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in grid_Crew.Rows)
        //    {
        //        CheckBox ck = ((CheckBox)row.FindControl("chkCtrlCrew"));
        //        Crew _crew = new Crew();
        //        _crew.CrewID = Convert.ToInt16(row.Cells[1].Text);
        //        _crew.CrewName = row.Cells[2].Text;
        //        if (ck.Checked)
        //        {
        //            if (SelectedCrews.Where(aa => aa.CrewID == _crew.CrewID).Count() == 0)
        //                SelectedCrews.Add(_crew);
        //        }
        //        else
        //        {
        //            if (SelectedCrews.Where(aa => aa.CrewID == _crew.CrewID).Count() > 0)
        //            {
        //                var crew = SelectedCrews.Where(aa => aa.CrewID == _crew.CrewID).First();
        //                SelectedCrews.Remove(crew);
        //            }
        //        }
        //    }
        //}
        ////for locations
        //protected void chkCtrlLoc_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in grid_Location.Rows)
        //    {
        //        CheckBox ck = ((CheckBox)row.FindControl("chkCtrlLoc"));
        //        Location _loc = new Location();
        //        _loc.LocID = Convert.ToInt16(row.Cells[1].Text);
        //        _loc.LocName = row.Cells[2].Text;
        //        if (ck.Checked)
        //        {
        //            if (SelectedLocs.Where(aa => aa.LocID == _loc.LocID).Count() == 0)
        //                SelectedLocs.Add(_loc);
        //        }
        //        else
        //        {
        //            if (SelectedLocs.Where(aa => aa.LocID == _loc.LocID).Count() > 0)
        //            {
        //                var loc = SelectedLocs.Where(aa => aa.LocID == _loc.LocID).First();
        //                SelectedLocs.Remove(loc);
        //            }
        //        }
        //    }
        //}
        ////for locations
        //protected void chkCtrlCompany_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in grid_Company.Rows)
        //    {
        //        CheckBox ck = ((CheckBox)row.FindControl("chkCtrlCompany"));
        //        Company _comp = new Company();
        //        _comp.CompID = Convert.ToInt16(row.Cells[1].Text);
        //        _comp.CompName = row.Cells[2].Text;
        //        if (ck.Checked)
        //        {
        //            if (SelectedLocs.Where(aa => aa.LocID == _comp.CompID).Count() == 0)
        //                SelectedComps.Add(_comp);
        //        }
        //        else
        //        {
        //            if (SelectedComps.Where(aa => aa.CompID == _comp.CompID).Count() > 0)
        //            {
        //                var comp = SelectedComps.Where(aa => aa.CompID == _comp.CompID).First();
        //                SelectedComps.Remove(comp);
        //            }
        //        }
        //    }
        //}
        ////for Types
        //protected void chkCtrlType_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in grid_EmpType.Rows)
        //    {
        //        CheckBox ck = ((CheckBox)row.FindControl("chkCtrlType"));
        //        EmpType _Cat = new EmpType();
        //        _Cat.TypeID = Convert.ToByte(row.Cells[1].Text);
        //        _Cat.TypeName = row.Cells[2].Text;
        //        if (ck.Checked)
        //        {
        //            if (SelectedTypes.Where(aa => aa.TypeID == _Cat.TypeID).Count() == 0)
        //                SelectedTypes.Add(_Cat);
        //        }
        //        else
        //        {
        //            if (SelectedTypes.Where(aa => aa.TypeID == _Cat.TypeID).Count() > 0)
        //            {
        //                var cat = SelectedTypes.Where(aa => aa.TypeID == _Cat.TypeID).First();
        //                SelectedTypes.Remove(cat);
        //            }
        //        }
        //    }
        //}
        //#endregion

        TAS2013Entities context = new TAS2013Entities();
        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
       
           
            ReportViewer1.Visible = true;
            //List<ViewMultipleInOut> _ViewList = new List<ViewMultipleInOut>();
            List<ViewMultipleInOut> _TempViewList = new List<ViewMultipleInOut>();
            User LoggedInUser = HttpContext.Current.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.MakeCustomizeQuery(LoggedInUser);
            string _dateTo = "'"+DateTo.Date.Year.ToString() + "-" + DateTo.Date.Month.ToString() + "-" + DateTo.Date.Day.ToString()+"'";
            string _dateFrom = "'" + DateFrom.Date.Year.ToString() + "-" + DateFrom.Date.Month.ToString() + "-" + DateFrom.Date.Day.ToString() + "'";
            DataTable dt = qb.GetValuesfromDB("select * from ViewMultipleInOut " + query + " and (AttDate >= "+_dateFrom+" and AttDate <= "+_dateTo+" )" );
            List<ViewMultipleInOut> _ViewList = dt.ToList<ViewMultipleInOut>();

          
            if (GlobalVariables.DeploymentType == false)
            {
                PathString = "/Reports/RDLC/DRDetailed.rdlc";
            }
            else
                PathString = "/WMS/Reports/RDLC/DRDetailed.rdlc";
            LoadReport(PathString, _ViewList);
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
        private void LoadReport(string path, List<ViewMultipleInOut> _Employee)
        {
            string DateToFor = "";
            if (DateFrom.Date.ToString("d") == DateTo.ToString("d"))
            {
                DateToFor = "Date : " + DateFrom.Date.ToString("d");
            }
            else
            {
                DateToFor = "From : " + DateFrom.Date.ToString("d") + " To: " + DateTo.Date.ToString("d");
            }
            string _Header = "Daily Attendance Report";
            this.ReportViewer1.LocalReport.DisplayName = "Daily Attendance Report";
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
            System.Security.PermissionSet sec = new System.Security.PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
            ReportViewer1.LocalReport.SetBasePermissionsForSandboxAppDomain(sec);
            IEnumerable<ViewMultipleInOut> ie;
            ie = _Employee.AsQueryable();
            ReportDataSource datasource1 = new ReportDataSource("DataSet1", ie);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource1);
            ReportParameter rp = new ReportParameter("Date", DateToFor, false);
            ReportParameter rp1 = new ReportParameter("Header", _Header, false);
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp, rp1 });
            ReportViewer1.LocalReport.Refresh();
        }
    }
}