<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaNativaCRM.aspx.cs" Inherits="PruebasWeb.ConsultaNativaCRM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server"
            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <RowStyle BackColor="White" ForeColor="#003399" /> 
            <Columns>
                <asp:BoundField DataField="CuentaID" HeaderText="CuentaID" ReadOnly="True" SortExpression="CuentaID" Visible ="false" /> 
                <asp:BoundField DataField="Nombre" HeaderText="Nombre de cuenta" ReadOnly="True" SortExpression="Nombre" /> 
                <asp:BoundField DataField="DireccionCiudad" HeaderText="Ciudad" ReadOnly="True" SortExpression="DireccionCiudad" /> 
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
