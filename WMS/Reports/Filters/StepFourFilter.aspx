﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ReportingEngine.Master" AutoEventWireup="true" CodeBehind="StepFourFilter.aspx.cs" Inherits="WMS.Reports.Filters.StepFourFilter" %>
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
                        <asp:LinkButton ID="btnStepOne" runat="server" CssClass="inactive-link" OnClick="btnStepOne_Click" >Step One<p>Regions,Shifts</p></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnStepTwo" runat="server" CssClass="inactive-link" OnClick="btnStepTwo_Click" >Step Two<p>City,Employee Types </p></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnStepThree" runat="server"  CssClass="inactive-link" OnClick="btnStepThree_Click" >Step Three<p>Location,Wings </p></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnStepFour" runat="server" CssClass="active-link" OnClick="btnStepFour_Click" >Step Four<p>Crews, Sections</p></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnStepFive" runat="server"  CssClass="inactive-link" OnClick="btnStepFive_Click" >Step Five<p>Employees</p></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnStepSix" runat="server" CssClass="inactive-link" OnClick="btnStepSix_Click" >Finish<p>Generate Report</p></asp:LinkButton>

                    </li>
                </ul>
                
            <!-- /#sidebar-wrapper -->
        </div>
        </div>
        <div class="col-sm-9 col-md-9 col-lg-9">
                <div class="row">
                    <div class="col-md-8">
                        <div class="row"> 
                            <div class="col-md-4">
                                <h3>Apply Filters</h3>
                            </div>
                            <div class="col-md-3">
                              <asp:Button ID="Button3" runat="server" style="margin-top:18px" Text="Clear All Filters" CssClass="btn-warning" OnClick="ButtonDeleteAll_Click" />
                                </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-6">
                                From : <input id="dateFrom"  class="input-sm"  runat="server" type="date" />
                            </div>
                            <div class="col-md-6">
                                To : <input id="dateTo" class="input-sm"  runat="server" type="date" />
                            </div>
                        </div>
                        <hr />
                       
                       
                        <div class="row">
                             <div class="filterHeader"><span class="FilterNameHeading">Crew</span>
                                 <span style="margin-left:10px"><asp:TextBox ID="tbSearch_Crew" CssClass="input-field" runat="server" /> <asp:Button ID="Button2" runat="server" Text="Search" CssClass="btn-primary" OnClick="ButtonSearchCrew_Click" /></span>
                        </div>
                             <section>
                            <asp:GridView ID="GridViewCrew" runat="server" Width="300px" AutoGenerateColumns="False" PagerStyle-CssClass="pgr" CssClass="Grid"                              GridLines="None" AllowPaging="True" AllowSorting="True"                                                OnPageIndexChanging="GridViewCrew_PageIndexChanging" BorderColor="#0094FF" BorderStyle="None" OnRowDataBound="GridViewCrew_RowDataBound" ShowFooter="True" BorderWidth="1px"  >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <%--<asp:CheckBox ID="CheckAll" runat="server" />--%>
                                            <input style="margin-left:6px" id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 'GridViewCrew');" 
                                            runat="server" type="checkbox" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox style="margin-left:6px"  ID="CheckOne" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="CrewID" HeaderText="ID" />
                                        <asp:BoundField DataField="CrewName" HeaderText="Name" />
                    
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black" Wrap="False" />
                                <HeaderStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black" BorderColor="#0094FF" BorderStyle="None" BorderWidth="1px" />
                                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev" Mode="NextPreviousFirstLast" />
                                <PagerStyle BackColor="White" ForeColor="#0094FF" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </section>
                        </div>
                        <hr />
                         <div class="row">
                            <div class="filterHeader"><span class="FilterNameHeading">Sections</span>
                                 <span style="margin-left:10px"><asp:TextBox ID="tbSearch_Section" CssClass="input-field" runat="server" /> <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn-primary" OnClick="ButtonSearchSection_Click" /></span></div>
                            <section>
                            <asp:GridView ID="GridViewSection" runat="server" Width="300px" AutoGenerateColumns="False" PagerStyle-CssClass="pgr" CssClass="Grid"                              GridLines="None" AllowPaging="True" AllowSorting="True"                                                OnPageIndexChanging="GridViewSection_PageIndexChanging" ForeColor="Black" OnRowDataBound="GridViewSection_RowDataBound" ShowFooter="True"  >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <%--<asp:CheckBox ID="CheckAll" runat="server" />--%>
                                            <input style="margin-left:6px" id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 'GridViewSection');" 
                                            runat="server" type="checkbox" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox style="margin-left:6px"  ID="CheckOne" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="SectionID" HeaderText="ID" />
                                        <asp:BoundField DataField="SectionName" HeaderText="Name" />
                    
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black" Wrap="False" />
                                <HeaderStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black" />
                                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev" Mode="NextPreviousFirstLast" />
                                <PagerStyle BackColor="White" ForeColor="#0094FF" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </section>
                        </div>
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

