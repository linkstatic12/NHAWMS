<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexReport.aspx.cs" Inherits="WMS.UIReports.IndexReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBoxSearch" BackColor="Yellow" runat="server"/> 
        <asp:Button ID="ButtonSearch" runat="server" Text="Search" OnClick="ButtonSearch_Click" />
        <br />
        <br />

        <asp:GridView ID="GridViewReport" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="EmpName" HeaderText="Name" />
                 <asp:BoundField DataField="FatherName" HeaderText="Father" />
                 <asp:BoundField DataField="NicNo" HeaderText="NIC" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
