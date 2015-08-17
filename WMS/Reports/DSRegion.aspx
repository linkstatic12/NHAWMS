<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DSRegion.aspx.cs" Inherits="WMS.Reports.DSRegion" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <script src="../Scripts/modernizr-2.6.2.js"></script>
    <script src="../Scripts/jquery-2.1.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="report-container">
         <div class="report-filter">
             <div style="height:20px;"></div>
                 <div class="button-divDate">
                     <span>From: </span><input id="dateFrom" type="date" runat="server" style="height:30px;" />
                     <span>To: </span><input id="dateTo" type="date" runat="server" style="height:30px;" />
                 </div>
                <div style="margin-left:10px; margin-bottom:20px; margin-top:10px">
                    <div><asp:RadioButton GroupName="colors"  ID="rb1" runat="server" ForeColor="White" Font-Size="12" Text ="Strength Report" /></div>
                    <div><asp:RadioButton GroupName="colors"  ID="rb2" runat="server" ForeColor="White" Font-Size="12" Text ="Work Times" /></div>
                    <div><asp:RadioButton GroupName="colors" ID="rb3" runat="server" ForeColor="White" Font-Size="12" Text ="Consolidated" /></div>
                </div>
             <div class="button-div">
                <asp:Button ID="btnLoc" CssClass="btn btn-primary" runat="server" 
                Text="Select Regions" onclick="btnRegion_Click" Width="180px"/>
                <div><asp:Label ID="lbRegionCount" runat="server" Text="Selected Regions: 0" Font-Bold="False" Font-Italic="True" Font-Size="10pt" ForeColor="#F9F9F9"></asp:Label></div>
            </div>
                <div class="button-div">
                    <asp:Button ID="btnGenerateReport" CssClass="btn btn-success" 
                      runat="server" Text="Generate Report" onclick="btnGenerateReport_Click" Width="190px" />
                </div>
             </div>

         <div class="report-viewer-container" style="margin: 0 auto;width:775px;">
              <%-- Region Grid --%>
             <div runat="server" id="DivRegionGrid" style="margin: 20px;">
                 <div style="font-size: 15px;margin: 10px;font-weight: bold;">Press Ctrl+F to Find a Region </div>
                 <asp:GridView ID="grid_Region" runat="server" AutoGenerateColumns="False" DataKeyNames="RegionID">
                     <Columns>
                         <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCtrlRegion" runat="server"  oncheckedchanged="chkCtrlRegion_CheckedChanged"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                         <asp:BoundField DataField="RegionID" HeaderText="ID" InsertVisible="False" ReadOnly="True" />
                         <asp:BoundField DataField="RegionName" HeaderText="Name" SortExpression="RegionName" />
                     </Columns>
                 </asp:GridView>
                 
             </div>

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



