<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultaIndividualEmpresa.aspx.cs" Inherits="ConsultaIndividualEmpresa" %>

<%@ Register Assembly="Controles" Namespace="Controles" TagPrefix="cc1" %>

<%--<%@ Register Assembly="Controles" Namespace="Controles" TagPrefix="cc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">   
    <div>
        <center>
            <h1>Detalles de la empresa</h1>
   
            <cc1:DatosEmpresa ID="DatosEmpresa1" runat="server" />

        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <br />
        </center>
    </div>
</asp:Content>

