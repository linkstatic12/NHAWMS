﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ReportingEngine.Master" AutoEventWireup="true" CodeBehind="StepSixFilter.aspx.cs" Inherits="WMS.Reports.Filters.StepSixFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="container" style="margin-left:0;margin-right:0;">
        <div class="col-sm-3 col-md-3 col-lg-3" >
            <!-- Sidebar -->
            <div id="sidebar-wrapper">
                <ul class="sidebar-nav">
                    <li class="sidebar-brand">
                        <h4>Filters Navigation</h4>
                    </li>
                    <li >
                        <a class="inactive-link" href="StepOneFilter.aspx">Step One<p>Company, Locations</p></a>
                    </li>
                    <li>
                        <a class="inactive-link" href="StepTwoFilter.aspx">Step Two<p>Divisions, Shifts</p></a>
                    </li>
                    <li>
                        <a class="inactive-link" href="StepThreeFilter.aspx">Step Three<p>Departments, Employee Type</p></a>
                    </li>
                    <li>
                        <a class="inactive-link" href="StepFourFilter.aspx">Step Four<p>Sections, Crew</p></a>
                    </li>
                    <li>
                        <a class="inactive-link" href="StepFiveFilter.aspx">Step Five<p>Employee</p></a>
                    </li>
                    <%--<div style=" margin-left:40px; margin-top:20px">
                        <asp:Button ID="ButtonSkip" runat="server"  Text="Skip"  CssClass="btn-warning btn-sm btnCustomMargin" OnClick="ButtonSkip_Click" />
                        <asp:Button ID="ButtonNext" runat="server"  Text="Next" CssClass="btn-info btn-sm"  OnClick="ButtonNext_Click" />
                        <asp:Button ID="ButtonFinish" runat="server"  Text="Finish"  CssClass="btn-success btn-sm" OnClick="ButtonFinish_Click" />
                    </div>--%>
                </ul>
                
            <!-- /#sidebar-wrapper -->
        </div>
        </div>
        <div class="col-sm-9 col-md-9 col-lg-9">
                <div class="row">
                    <div class="col-md-8">
                        <section class="row">
                            <h2>Choose Report</h2>
                            <ul>
                                <li>
                                    <h5>HR Reports</h5>
                                    <ul>
                                        <li><a href="../ReportContainer.aspx?reportname=emp_record">Employee Record</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=emp_detail_excel">Employee Detail (Only for Excel)</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=leave_application">Leave Application</a></li>
                                    
                                    </ul>
                                </li>
                                <li>
                                    <h5>Daily Attendance</h5>
                                    <ul>
                                        <li><a href="../ReportContainer.aspx?reportname=detailed_att">Detailed Attendance</a></li>
                                            <li><a href="../ReportContainer.aspx?reportname=consolidated_att">Consolidated Attendance</a></li>

                                        <li><a href="../ReportContainer.aspx?reportname=present">Present</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=absent">Absent</a></li>
                                        <%--<li><a href="../ReportContainer.aspx?reportname=lv_application">Leave Application</a></li>--%>
                                        <li><a href="../ReportContainer.aspx?reportname=short_lv">Short Leave</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=late_in">Late In</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=late_out">Late Out</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=early_in">Early In</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=early_out">Early Out</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=overtime">Overtime</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=missing_attendance">Missing Attendance</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=multiple_in_out">Multiple In/Out</a></li>
                                    </ul>
                                </li>
                                <li>
                                    <h5>Monthly</h5>
                                    <ul>
                                        <li><a href="../ReportContainer.aspx?reportname=monthly_leave_sheet">Monthly Leave Sheet</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=monthly_21-20">Monthly Sheet (21th to 20th)</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=monthly_1-31">Monthly Sheet (1st to 31th)</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=monthlysummary_21-20">Monthly Summary (21th to 20th)</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=monthlysummary_1-31">Monthly Summary (1st to 31th)</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=monthly_21-20_excel">Monthly Sheet (21th to 20th)(Excel)</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=monthly_1-31_consolidated">Monthly Consolidated (1st to 31th)</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=monthly_21-20_consolidated">Monthly Consolidated (21th to 20th)(Excel)</a></li>
                                    </ul>
                                </li>
                                <li>
                                    <h5>Top Criteria</h5>
                                    <ul>

                                        <li><a href="../ReportContainer.aspx?reportname=top_present">Top Present</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=top_absent">Top Absent</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=top_leave">Top Leave</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=top_earlyIn">Top Early In</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=top_earlyOut">Top Early Out</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=top_lateIn">Top Late In</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=top_overtime">Top Overtime</a></li>
                                    </ul>
                                </li>
                                <li>
                                    <h5>Detailed</h5>
                                    <ul>
                                        <li><a href="../ReportContainer.aspx?reportname=emp_att">Employee Attendance</a></li>
                                        <li><a href="../ReportContainer.aspx?reportname=emp_absent">Employee Absent</a></li>
                                    </ul>
                                </li>
                                <li>
                                    <h5>Yearly</h5>
                                    <ul>
                                        <li><a href="../ReportContainer.aspx?reportname=lv_quota">Leave Quota</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </section>
                    </div>
                    <section class="col-md-4 selected-filters-wrapper">
                    <h2>Selected Filters...</h2><hr />
                    <div class="panel-group" id="accordion">

	


                    
                    <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).RegionFilter.Count > 0)
                       {
                           {
                               int d = ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).RegionFilter.Count;
                               Response.Write("<div class='panel panel-default'><div class='panel-heading'><h4 class='panel-title'><a style = 'text-decoration: none !important;' data-toggle='collapse' data-parent='#accordion' href='#collapseOne'>Regions</a>  <span style ='float:right;' class='badge'>" + d + "</span></h4></div><div id='collapseOne' class='panel-collapse collapse out'><div class='list-group'>");
                           }
                           foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).RegionFilter)
                           {
                               { Response.Write("<a class='list-group-item'>" + item.FilterName + "</a> "); }
                           }
                           { Response.Write("</div></div></div>"); }
                       }%>
                       
                   </div>
                          <div class="panel-group" id="Div1">

                         <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).ShiftFilter.Count > 0)
                       {
                           {
                               int d = ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).ShiftFilter.Count;
                               Response.Write("<div class='panel panel-default'><div class='panel-heading'><h4 class='panel-title'><a style = 'text-decoration: none !important;' data-toggle='collapse' data-parent='#Div1' href='#collapseShift'>Shift</a>  <span style ='float:right;' class='badge'>" + d + "</span></h4></div><div id='collapseShift' class='panel-collapse collapse out'><div class='list-group'>");
                           }
                           foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).ShiftFilter)
                           {
                               { Response.Write("<a class='list-group-item'>" + item.FilterName + "</a>"); }
                           }
                           { Response.Write("</div></div></div>"); }
                    }%>
                               </div>
                       
                        <div class="panel-group" id="Div2">
                    <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).CityFilter.Count > 0)
                       {
                           {
                               int d = ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).CityFilter.Count;
                               Response.Write("<div class='panel panel-default'><div class='panel-heading'><h4 class='panel-title'><a style = 'text-decoration: none !important;' data-toggle='collapse' data-parent='#Div2' href='#collapseCity'>Cities</a>  <span style ='float:right;' class='badge'>" + d + "</span></h4></div><div id='collapseCity' class='panel-collapse collapse out'><div class='list-group'>");
                           }
                           foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).CityFilter)
                           {
                               { Response.Write("<a class='list-group-item'>" + item.FilterName + "</a>"); }
                           }
                           { Response.Write("</div></div></div>"); }
                    }%> </div>
                        <%-- <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).DivisionFilter.Count > 0)
                       { 
                     { Response.Write("<h3>Divisions</h3>"); }
                     foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).DivisionFilter)
                        {
                            { Response.Write("<br>" + item.FilterName); }
                        } 
                    }%>--%>
                       <div class="panel-group" id="Div3">
                         <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).TypeFilter.Count > 0)
                       {
                           {
                               int d = ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).TypeFilter.Count;
                               Response.Write("<div class='panel panel-default'><div class='panel-heading'><h4 class='panel-title'><a style = 'text-decoration: none !important;' data-toggle='collapse' data-parent='#Div3' href='#collapseType'>Employee Types</a>  <span style ='float:right;' class='badge'>" + d + "</span></h4></div><div id='collapseType' class='panel-collapse collapse out'><div class='list-group'>");
                           }
                           foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).TypeFilter)
                           {
                               { Response.Write("<a class='list-group-item'>" + item.FilterName + "</a>"); }
                           } 
                                { Response.Write("</div></div></div>"); }
                    }%>

                       </div>
                         <div class="panel-group" id="Div4">
                         <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).LocationFilter.Count > 0)
                       {
                           {
                               int d = ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).LocationFilter.Count;
                               Response.Write("<div class='panel panel-default'><div class='panel-heading'><h4 class='panel-title'><a style = 'text-decoration: none !important;'  data-toggle='collapse' data-parent='#Div4' href='#collapseLocation'>Locations<span  style ='float:right;' class='badge'>" + d + "</span></a></h4></div><div id='collapseLocation' class='panel-collapse collapse out'><div class='list-group'>");
                           }
                           foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).LocationFilter)
                           {
                               { Response.Write("<a class='list-group-item'>" + item.FilterName + "</a>"); }
                           }
                           { Response.Write("</div></div></div>"); }
                    }%>
                             </div>
                         <div class="panel-group" id="Div5">
                         <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).DepartmentFilter.Count > 0)
                       {
                           {
                               int d = ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).DepartmentFilter.Count;
                               Response.Write("<div class='panel panel-default'><div class='panel-heading'><h4 class='panel-title'><a data-toggle='collapse' data-parent='#Div5' href='#collapseWing'>Wings</a><span style ='float:right;' class='badge'>" + d + "</span></h4></div><div id='collapseWing' class='panel-collapse collapse out'><div class='list-group'>");
                           }
                           foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).DepartmentFilter)
                           {
                               { Response.Write("<a class='list-group-item'>" + item.FilterName + "</a>"); }
                           }
                           { Response.Write("</div></div></div>"); }
                    }%>

                         </div>

                         <div class="panel-group" id="Div6">
                        <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).CrewFilter.Count > 0)
                       {
                           {
                               int d = ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).CrewFilter.Count;
                               Response.Write("<div class='panel panel-default'><div class='panel-heading'><h4 class='panel-title'><a data-toggle='collapse' data-parent='#Div6' href='#collapseCrew'>Crews</a><span style ='float:right;' class='badge'>" + d + "</span></h4></div><div id='collapseCrew' class='panel-collapse collapse out'><div class='list-group'>");
                           }
                           foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).CrewFilter)
                           {
                               { Response.Write("<a class='list-group-item'>" + item.FilterName + "</a>"); }
                           }   { Response.Write("</div></div></div>"); }
                    }%>

                         </div>
                         <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).SectionFilter.Count > 0)
                       {
                           {
                               int d = ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).SectionFilter.Count;
                               Response.Write("<div class='panel panel-default'><div class='panel-heading'><h4 class='panel-title'><a data-toggle='collapse' data-parent='#Div7' href='#collapseSection'>Sections</a><span style ='float:right;' class='badge'>" + d + "</span></h4></div><div id='collapseSection' class='panel-collapse collapse out'><div class='list-group'>");
                           }
                           foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).SectionFilter)
                           {
                               { Response.Write("<a class='list-group-item'>" + item.FilterName + "</a>"); }
                           }  { Response.Write("</div></div></div><div>"); }
                    }%>
<div class="panel-group" id="Div8">
                        <% if (((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).EmployeeFilter.Count > 0)
                       {
                           {
                               int d = ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).EmployeeFilter.Count;
                               Response.Write("<div class='panel panel-default'><div class='panel-heading'><h4 class='panel-title'><a data-toggle='collapse' data-parent='#Div8' href='#collapseEmployee'>Employees</a><span style ='float:right;' class='badge'>" + d + "</span></h4></div><div id='collapseEmployee' class='panel-collapse collapse out'><div class='list-group'>");
                           }
                           foreach (var item in ((WMSLibrary.FiltersModel)HttpContext.Current.Session["FiltersModel"]).EmployeeFilter)
                           {
                               { Response.Write("<a class='list-group-item'>" + item.FilterName + "</a>"); }
                           }   { Response.Write("</div></div></div>"); }
                    }%>

                         </div>

                </section>
                </div>
                <div class="row">
                    
                </div>
        </div>
    </section>
</asp:Content>
