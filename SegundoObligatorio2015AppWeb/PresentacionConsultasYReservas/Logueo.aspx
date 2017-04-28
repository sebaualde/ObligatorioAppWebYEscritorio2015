<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logueo.aspx.cs" Inherits="Logueo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="estilos.css" title="estilos" rel="stylesheet" type="text/css"/>

</head>
<center>
<body>
    <form id="form1" runat="server">
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
            <br />
        <center>
            <asp:Login ID="LoginCLi" runat="server" BackColor="#F7F7DE" 
                BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" 
                DisplayRememberMe="False" Font-Names="Verdana" Font-Size="10pt" 
                onauthenticate="LoginCLi_Authenticate">
                <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
        </asp:Login></center>
            <br />
    </div>
   <div id="pie"><center>Obligatorio -&nbsp; Diesño de Apps Web en C#&nbsp;<br />
            Darío Stramil - Marcelo Mesa - Sebastián Ualde</center></div> 
            </div>
    </form>
    </center>
</body>
</html>
