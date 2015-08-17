using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WMS.Models;

namespace WMS.Reports
{
    public partial class DSRegion : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            DivRegionGrid.Visible = false;
            ReportViewer1.Visible = true;
            LoadGridViews();
            ReportViewer1.Width = 1050;
            ReportViewer1.Height = 700;
            rb1.Checked = true;
            if (GlobalVariables.DeploymentType == false)
            {
                PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
            }
            else
                PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
            List<DailySummary> _ViewList = new List<DailySummary>();
            _ViewList = db.DailySummaries.Where(aa => aa.Criteria == "R").ToList();
            LoadReport(PathString, _ViewList, "Region Strength Summary Report");
            //
        }
        #region --Load GridViews --
        private void LoadGridViews()
        {
            User _loggedUser = HttpContext.Current.Session["LoggedUser"] as User;
            LoadRegionGrid(_loggedUser);
        }

        private void LoadRegionGrid(User _loggedUser)
        {
            List<Region> _objectList = new List<Region>();
            _objectList = db.Regions.ToList();
            //_Query = "SELECT * FROM TAS2013.dbo.EmpType where " + selectSQL;
            //grid_EmpType.DataSource = GetValuesFromDatabase(_Query, "EmpType");
            //grid_EmpType.DataBind();
            grid_Region.DataSource = _objectList;
            grid_Region.DataBind();
        }
        #endregion
        #region --Filters Button--
        protected void btnRegion_Click(object sender, EventArgs e)
        {
            ReportViewer1.Visible = false;
            DivRegionGrid.Visible = true;
            RefreshLabels();
        }
        #endregion

        #region --ViewStates--

        protected List<Region> SelectedLocs
        {
            get
            {
                if (this.Session["SelectedRegion"] == null)
                    this.Session["SelectedRegion"] = new List<Region>();
                return (List<Region>)this.Session["SelectedRegion"];
            }
            set
            {
                Session["SelectedRegion"] = value;
            }
        }
        
        #endregion

        private void RefreshLabels()
        {
            lbRegionCount.Text = "Selected Region : " + SelectedLocs.Count.ToString();
        }

        #region --CheckBoxes Changed Events --

        //for locations
        protected void chkCtrlRegion_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grid_Region.Rows)
            {
                CheckBox ck = ((CheckBox)row.FindControl("chkCtrlRegion"));
                Region _loc = new Region();
                _loc.RegionID = Convert.ToByte(row.Cells[1].Text);
                _loc.RegionName = row.Cells[2].Text;
                if (ck.Checked)
                {
                    if (SelectedLocs.Where(aa => aa.RegionID == _loc.RegionID).Count() == 0)
                        SelectedLocs.Add(_loc);
                }
                else
                {
                    if (SelectedLocs.Where(aa => aa.RegionID == _loc.RegionID).Count() > 0)
                    {
                        var loc = SelectedLocs.Where(aa => aa.RegionID == _loc.RegionID).First();
                        SelectedLocs.Remove(loc);
                    }
                }
            }
        }
       
        #endregion
        string PathString = "";
        TAS2013Entities db = new TAS2013Entities();
        string ReportName = "";
        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            List<DailySummary> _TempViewList = new List<DailySummary>();
            List<DailySummary> _ViewList = new List<DailySummary>();
            _ViewList = db.DailySummaries.Where(aa => aa.Criteria == "R" && aa.Date >= DateFrom && aa.Date <= DateTo).ToList();
            if (SelectedLocs.Count > 0)
            {
                foreach (var region in SelectedLocs)
                {
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.CriteriaValue == region.RegionID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();
            ReportViewer1.Visible = true;
            DivRegionGrid.Visible = false;
            if (rb1.Checked == true)
            {
                if (GlobalVariables.DeploymentType == false)
                {
                    PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
                }
                else
                    PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
                ReportName = "Region Strength Summary Report";
            }
            if (rb2.Checked == true)
            {
                if (GlobalVariables.DeploymentType == false)
                {
                    PathString = "/Reports/RDLC/DSWorkSummary.rdlc";
                }
                else
                    PathString = "/WMS/Reports/RDLC/DSWorkSummary.rdlc";
                ReportName = "Region Work Times Summary Report";
            }
            if (rb3.Checked == true)
            {
                if (GlobalVariables.DeploymentType == false)
                {
                    PathString = "/Reports/RDLC/DSConsolidated.rdlc";
                }
                else
                    PathString = "/WMS/Reports/RDLC/DSConsolidated.rdlc";
                ReportName = "Region Consolidated Summary Report";
            }
           
            LoadReport(PathString, _ViewList, ReportName);

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
        private void LoadReport(string path, List<DailySummary> _Summary,string rptName)
        {
            string _Header = rptName;
            this.ReportViewer1.LocalReport.DisplayName = rptName;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(path);
            System.Security.PermissionSet sec = new System.Security.PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
            ReportViewer1.LocalReport.SetBasePermissionsForSandboxAppDomain(sec);
            ReportDataSource datasource1 = new ReportDataSource("DataSet1", _Summary);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource1);
            ReportParameter rp = new ReportParameter("CompanyName", "National Highway Authority", false);
            ReportParameter rp1 = new ReportParameter("Title", rptName, false);
            ReportParameter rp2 = new ReportParameter("Date", DateTo.ToString("dd-MMM-yyyy") + " to " + DateFrom.ToString("dd-MMM-yyyy"), false);
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp,rp1,rp2 });
            ReportViewer1.LocalReport.Refresh();  
        }
        }
}