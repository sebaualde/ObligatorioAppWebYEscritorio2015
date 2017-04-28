<%@ Page Title="BIOS Serch Clientes" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="estilos.css" title="estilos" rel="stylesheet" type="text/css"/>

</head>
<body>
    <center>
    <form id="form1" runat="server">
    <div>
    <div id="contenedor">
    <div id="cabecera"><img src="img/logo.png"/>
    
    </div>

     <div id="menu">
         <asp:Menu ID="Menu1" runat="server" CssClass="estilomenu" ForeColor="#CCCCCC" 
             Orientation="Horizontal" StaticSubMenuIndent="16px">
             <Items>
                 <asp:MenuItem NavigateUrl="~/Default.aspx" Text="Inicio" Value="Inicio"></asp:MenuItem>
                 <asp:MenuItem Text="Loguearse" Value="Loguearse" NavigateUrl="~/Logueo.aspx"></asp:MenuItem>
                 <asp:MenuItem Text="Registrarse" Value="Registrarse" NavigateUrl="~/RegistroCliente.aspx"></asp:MenuItem>
             </Items>
         </asp:Menu>
         <asp:Menu ID="Menu2" runat="server" CssClass="estilomenu" ForeColor="#CCCCCC" 
             Orientation="Horizontal" StaticSubMenuIndent="16px" 
             onmenuitemclick="Menu2_MenuItemClick">
             <Items>
                 <asp:MenuItem Text="Inicio" Value="Nuevo elemento" NavigateUrl="~/Default.aspx"></asp:MenuItem> 
                 <asp:MenuItem Text="Consulta de Visitas" Value="Consultas" NavigateUrl="~/ConsultaDeVisitas.aspx"></asp:MenuItem>               
                 <asp:MenuItem Text="Desloguearse" Value="Desloguearse"></asp:MenuItem>
             </Items>
         </asp:Menu>
     </div>
    
        <div id="contenido">
           <h1 style="text-align: center">
        Bienvenido a BIOS Search</h1>
            <h1 style="text-align: center">
                    <asp:Button ID="btnLimpiar" runat="server" onclick="btnLimpiar_Click" 
                        Text="Limpiar Fromulario" Width="131px" />
                </h1>
        <table align="center">
            <tr>
                <td>
                    <asp:Menu ID="menDepartamentos" runat="server" BackColor="#E3EAEB" 
                        DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" 
                        ForeColor="#666666" onmenuitemclick="menDepartamentos_MenuItemClick" 
                        StaticSubMenuIndent="10px" Orientation="Horizontal">
                        <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicMenuStyle BackColor="#E3EAEB" />
                        <DynamicSelectedStyle BackColor="#1C5E55" />
                        <Items>
                            <asp:MenuItem Text="Departamentos" Value="Departamentos">
                                <asp:MenuItem Text="Canelones" Value="A"></asp:MenuItem>
                                <asp:MenuItem Text="Maldonado" Value="B"></asp:MenuItem>
                                <asp:MenuItem Text="Rocha" Value="C"></asp:MenuItem>
                                <asp:MenuItem Text="Treinta y Tres" Value="D"></asp:MenuItem>
                                <asp:MenuItem Text="Cerro Largo" Value="E"></asp:MenuItem>
                                <asp:MenuItem Text="Rivera" Value="F"></asp:MenuItem>
                                <asp:MenuItem Text="Artigas" Value="G"></asp:MenuItem>
                                <asp:MenuItem Text="Salto" Value="H"></asp:MenuItem>
                                <asp:MenuItem Text="Paysandú" Value="I"></asp:MenuItem>
                                <asp:MenuItem Text="Río Negro" Value="J"></asp:MenuItem>
                                <asp:MenuItem Text="Soriano" Value="K"></asp:MenuItem>
                                <asp:MenuItem Text="Colonia" Value="L"></asp:MenuItem>
                                <asp:MenuItem Text="San José" Value="M"></asp:MenuItem>
                                <asp:MenuItem Text="Flores" Value="N"></asp:MenuItem>
                                <asp:MenuItem Text="Florida" Value="O"></asp:MenuItem>
                                <asp:MenuItem Text="Lavalleja" Value="P"></asp:MenuItem>
                                <asp:MenuItem Text="Durazno" Value="Q"></asp:MenuItem>
                                <asp:MenuItem Text="Tacuarembó" Value="R"></asp:MenuItem>
                                <asp:MenuItem Text="Montevideo" Value="S"></asp:MenuItem>
                            </asp:MenuItem>
                        </Items>
                        <StaticHoverStyle BackColor="#666666" ForeColor="White" />
                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticSelectedStyle BackColor="#1C5E55" />
                    </asp:Menu>
                </td>
                <td>
                    <asp:Menu ID="menCiudades" runat="server" BackColor="#E3EAEB" 
                        DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" 
                        ForeColor="#666666" onmenuitemclick="menCiudades_MenuItemClick" 
                        StaticSubMenuIndent="10px" Orientation="Horizontal">
                        <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicMenuStyle BackColor="#E3EAEB" />
                        <DynamicSelectedStyle BackColor="#1C5E55" />
                        <StaticHoverStyle BackColor="#666666" ForeColor="White" />
                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticSelectedStyle BackColor="#1C5E55" />
                    </asp:Menu>
                </td>
                <td>
                    <asp:Menu ID="menCategorias" runat="server" BackColor="#E3EAEB" 
                        DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" 
                        ForeColor="#666666" onmenuitemclick="menCategorias_MenuItemClick" 
                        StaticSubMenuIndent="10px" Orientation="Horizontal">
                        <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicMenuStyle BackColor="#E3EAEB" />
                        <DynamicSelectedStyle BackColor="#1C5E55" />
                        <StaticHoverStyle BackColor="#666666" ForeColor="White" />
                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticSelectedStyle BackColor="#1C5E55" />
                    </asp:Menu>
                </td>
            </tr>
            </table>

    <h3 style="text-align: center">
        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
    </h3>

    <div align="center">
       <asp:Repeater ID="rpEmpresas" runat="server" 
            onitemcommand="rpEmpresas_ItemCommand" 
            onitemdatabound="rpEmpresas_ItemDataBound" >
                    <ItemTemplate >
                        <table style="background:#e1e1e1">
                            <tr>
                                <td>
                                    RUT: <asp:TextBox ID="txtRut" Enabled="false" style="text-align:center" runat="Server" Text='<%# Bind("Rut") %>'></asp:TextBox>
                                    <br />
                                </td>
                                <td>
                                    Empresa: <asp:TextBox ID="txtNombre" Enabled="false" style="text-align:center" runat="Server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                                    <br />
                                </td>
                                <td>
                                    <asp:Button ID="btnSeleccionar" runat="server" Visible="true" style="text-align:center" Text="Seleccionar" />                                
                                </td>                        
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
        </div>
    <p style="text-align: center">
        &nbsp;</p>
    <p style="text-align: center">
        <asp:Label ID="lblError" runat="server"></asp:Label>
    </p>
            <p style="text-align: center">
                &nbsp;</p>
            <p style="text-align: center">
                &nbsp;</p>
<p>
        &nbsp;</p>
        </div>
        
    </div>
   <div id="pie"><center>Obligatorio -&nbsp; Diesño de Apps Web en C#&nbsp;&nbsp; Diesño de Apps Web en C#&nbsp;<br />
            Darío Stramil - Marcelo Mesa - Sebastián Ualde</center></div> 
            </div>
    </form>
    </center>
</body>
</html>
