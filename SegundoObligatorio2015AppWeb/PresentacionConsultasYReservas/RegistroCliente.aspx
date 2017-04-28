<%@ Page Title="Registro Cliente" Language="C#" AutoEventWireup="true" CodeFile="RegistroCliente.aspx.cs" Inherits="RegistroCliente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="estilos.css" title="estilos" rel="stylesheet" type="text/css"/>

    <style type="text/css">
        .style1
        {
            width: 120px;
            height: 25px;
        }
        .style2
        {
            height: 25px;
        }
    </style>

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
             Orientation="Horizontal" StaticSubMenuIndent="16px" Width="100%">
             <Items>
                 <asp:MenuItem NavigateUrl="~/Default.aspx" Text="Inicio" Value="Nuevo elemento">
                 </asp:MenuItem>
             </Items>
         </asp:Menu>
     </div>
    
        <div id="contenido">
         <h1>Registro de Usuarios</h1>
        <table align="center">
            <tr>
                <td>
                    CI:</td>
                <td>
                    <asp:TextBox ID="txtCI" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" onclick="btnBuscar_Click" 
                        Text="Buscar" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Nombre:</td>
                <td class="style2">
                    <asp:TextBox ID="txtNombre" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="style1">
                    </td>
            </tr>
            <tr>
                <td>
                    Usuario:</td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Contraseña:</td>
                <td>
                    <asp:TextBox ID="txtContrasenia" runat="server" Width="200px" 
                        TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    Edad:</td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtEdad" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" 
                        Text="Registrar" />
                </td>
                <td>
                    <asp:Button ID="btnLimpiar" runat="server" onclick="btnLimpiar_Click" 
                        Text="Limpiar" />
                </td>
            </tr>
            <tr>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
                    <asp:Label ID="lblMensaje" runat="server" EnableViewState="False"></asp:Label>
                <br />
            <br />
    <br />
        </div>
        
    </div>
   <div id="pie"><center>Obligatorio -&nbsp; Diesño de Apps Web en C#&nbsp;<br />
            Darío Stramil - Marcelo Mesa - Sebastián Ualde</center></div> 
            </div>
    </form>
</center>
</body>
</html>
