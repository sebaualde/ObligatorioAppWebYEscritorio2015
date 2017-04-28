<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultaDeVisitas.aspx.cs" Inherits="ConsultaDeVisitas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
<h1>Consulta de visitas del Cliente</h1>
    <p>
        <table align="center" style="width: 327px">
            <tr>
                <td style="width: 346px">
                    <asp:DropDownList ID="ddTipoFiltro" runat="server">
                        <asp:ListItem>Ninguno</asp:ListItem>
                        <asp:ListItem Value="Fecha">Por Fecha</asp:ListItem>
                        <asp:ListItem Value="Empresa">Por Empresa</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtBusqueda" runat="server"></asp:TextBox>
                </td>
                <td style="width: 362px">
                    <asp:ImageButton ID="ibtnAplicarFiltro" runat="server" 
                        ImageUrl="~/img/filtrar.png" ToolTip="Filtrar" 
                        onclick="ibtnAplicarFiltro_Click" />
                </td>
                 <td style="width: 362px">
                    <asp:ImageButton ID="ibtnResetFiltro" runat="server" 
                         ImageUrl="~/img/quitarFiltros.png" ToolTip="Resetear Filtro" 
                         onclick="ibtnResetFiltro_Click" />
                </td>
            </tr>
        </table>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="lblError" runat="server" EnableViewState="False"></asp:Label>
        &nbsp;</p>
    <p align="center">
        <asp:Xml ID="xmlConsulta" runat="server" 
            TransformSource="~/App_Data/Visitas.xslt"></asp:Xml>
    </p>
    <p>&nbsp;</p>
</asp:Content>

