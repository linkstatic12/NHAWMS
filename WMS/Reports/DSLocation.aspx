<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DSLocation.aspx.cs" Inherits="WMS.Reports.DSLocation" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/Site.css" rel="stylesheet" type="text/css" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/modernizr-2.6.2.js"></script>

    <script src="Scripts/jquery-2.1.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="report-container">
         <div class="report-filter">
             <div style="height:20px;"></div>
                 <div class="button-divDate">
                     <span>From: </span><input id="dateFrom" type="date" runat="server" style="height:30px;" />
                     <span>To: </span><input id="dateTo" type="date" runat="server" style="height:30px;" />
                 </div>
                <div style="margin-left:10px">
                    <asp:RadioButton  ID="rb1" runat="server" ForeColor="White" Font-Size="14" Text ="Strength Report" />
                    <asp:RadioButton  ID="rb2" runat="server" ForeColor="White" Font-Size="14" Text ="Work Times" />
                    <asp:RadioButton  ID="rb3" runat="server" ForeColor="White" Font-Size="14" Text ="Consolidated" />
                </div>
                <div class="button-div">
                    <asp:Button ID="btnGenerateReport" CssClass="btn btn-success" 
                      runat="server" Text="Generate Report" onclick="btnGenerateReport_Click" Width="190px" />
                </div>
             </div>

         <div class="report-viewer-container" style="margin: 0 auto;width:775px;">
             <div>
                 <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="778px">
                     <LocalReport ReportPath="Reports\RDLC\DRPresent.rdlc">
                         <DataSources>
                             <rsweb:ReportDataSource DataSourceId="ObjectDataSource8" Name="DataSet1" />
                         </DataSources>
                     </LocalReport>
                 </rsweb:ReportViewer>
                 <asp:ObjectDataSource ID="ObjectDataSource8" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="WMS.Models.TASReportDataSetTableAdapters.ViewPresentEmpTableAdapter"></asp:ObjectDataSource>
                 <asp:ScriptManager ID="ScriptManager1" runat="server">
                 </asp:ScriptManager>
             </div>
         </div> <%--End div Report-viewer-container--%>
         <div class="clearfix">
             
         </div> 
    </div><%--End div Report-container--%>
</asp:Content>


