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
    public partial class DSLocation : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            rb1.Checked = true;
            if (GlobalVariables.DeploymentType == false)
            {
                PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
            }
            else
                PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
            List<DailySummary> _ViewList = new List<DailySummary>();
            _ViewList = db.DailySummaries.Where(aa => aa.Criteria == "C").ToList();
            LoadReport(PathString, _ViewList, "Company Strength Summary Report");
            //
        }
        string PathString = "";
        TAS2013Entities db = new TAS2013Entities();
        string ReportName = "";
        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (rb1.Checked == true)
            {
                if (GlobalVariables.DeploymentType == false)
                {
                    PathString = "/Reports/RDLC/DSEmpStrength.rdlc";
                }
                else
                    PathString = "/WMS/Reports/RDLC/DSEmpStrength.rdlc";
                ReportName = "Company Strength Summary Report";
            }
            if (rb2.Checked == true)
            {
                if (GlobalVariables.DeploymentType == false)
                {
                    PathString = "/Reports/RDLC/DSWorkSummary.rdlc";
                }
                else
                    PathString = "/WMS/Reports/RDLC/DSWorkSummary.rdlc";
                ReportName = "Company Work Times Summary Report";
            }
            if (rb3.Checked == true)
            {
                if (GlobalVariables.DeploymentType == false)
                {
                    PathString = "/Reports/RDLC/DSConsolidated.rdlc";
                }
                else
                    PathString = "/WMS/Reports/RDLC/DSConsolidated.rdlc";
                ReportName = "Company Consolidated Summary Report";
            }
            List<DailySummary> _ViewList = new List<DailySummary>();
            _ViewList = db.DailySummaries.Where(aa => aa.Criteria == "C" && aa.Date >= DateFrom && aa.Date <= DateTo).ToList();
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
        private void LoadReport(string path, List<DailySummary> _Summary, string rptName)
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
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp, rp1, rp2 });
            ReportViewer1.LocalReport.Refresh();
        }
    }
}