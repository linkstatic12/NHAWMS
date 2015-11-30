<%@ Page Title="" Language="C#" MasterPageFile="~/ReportingEngine.Master" AutoEventWireup="true" CodeBehind="Graphs.aspx.cs" Inherits="WMS.Reports.Graph.Graphs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <article class="row">
        <aside class="col-md-3">
            <asp:GridView ID="GridViewLocation" runat="server" Width="300px" AutoGenerateColumns="False" CssClass="Grid" GridLines="None"  AllowSorting="True" ForeColor="Black"  Height="300px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <%--<asp:CheckBox ID="CheckAll" runat="server" />--%>
                            <input style="margin-left:6px" id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 'GridViewLocation');" 
                            runat="server" type="checkbox" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox style="margin-left:6px"  ID="CheckOne" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                        <asp:BoundField DataField="LocID" HeaderText="ID" />
                        <asp:BoundField DataField="LocName" HeaderText="Name" />

                    
                </Columns>
            </asp:GridView>
        </aside>
        <section class="col-md-9">
            <%-- Daily Counts --%>
            <section class="row">
                <div class="row">
                    <div class="col-md-6"><h3>Daily Attendance Count</h3></div>
                    <div class="col-md-6">Date: <input type="date" name="Dailydate" id="Dailydate"/></div>
                </div>
                 
            </section>
            <%-- Graphs Counts --%>
            <section class="row">

            </section>
        </section>
    </article>
</asp:Content>
