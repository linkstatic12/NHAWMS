<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WMS.UIReports.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <style type="text/css">

        #GridView1 td span {
           padding:5px 8px;
        }
        table th {
            padding:10px;
        }
    </style>
    <form id="form1" runat="server">
    <div id="divPrint">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="EmpID" DataSourceID="LinqDataSource1" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="EmpID" HeaderText="EmpID" InsertVisible="False" ReadOnly="True" SortExpression="EmpID" />
                <asp:BoundField DataField="EmpName" HeaderText="EmpName" SortExpression="EmpName" />
                <asp:BoundField DataField="CardNo" HeaderText="CardNo" SortExpression="CardNo" />
                <asp:BoundField DataField="NicNo" HeaderText="NicNo" SortExpression="NicNo" />
                <asp:BoundField DataField="FatherName" HeaderText="FatherName" SortExpression="FatherName" />
                <asp:BoundField DataField="BirthDate" HeaderText="BirthDate" SortExpression="BirthDate" />
                <asp:BoundField DataField="HomeAdd" HeaderText="HomeAdd" SortExpression="HomeAdd" />
                <asp:CheckBoxField DataField="ProcessAtt" HeaderText="ProcessAtt" SortExpression="ProcessAtt" />
                <asp:CheckBoxField DataField="ProcessIn" HeaderText="ProcessIn" SortExpression="ProcessIn" />
                <asp:CheckBoxField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="PhoneNo" HeaderText="PhoneNo" SortExpression="PhoneNo" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="CellNo" HeaderText="CellNo" SortExpression="CellNo" />
                <asp:CheckBoxField DataField="FlagFP" HeaderText="FlagFP" SortExpression="FlagFP" />
                <asp:CheckBoxField DataField="FlagFace" HeaderText="FlagFace" SortExpression="FlagFace" />
                <asp:CheckBoxField DataField="FlagCard" HeaderText="FlagCard" SortExpression="FlagCard" />
                <asp:CheckBoxField DataField="Presence" HeaderText="Presence" SortExpression="Presence" />
                <asp:BoundField DataField="LastEntryDateTime" HeaderText="LastEntryDateTime" SortExpression="LastEntryDateTime" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="WMS.DataAccess.ReportsDataContext" EntityTypeName="" TableName="Emps">
        </asp:LinqDataSource>
    </div>
    </form>
</body>
</html>
