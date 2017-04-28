using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServicioObligatorio;
using System.Xml;
using System.Xml.XPath;

public partial class ConsultaDeVisitas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Session["ClienteRegistrado"] is Cliente))
        {
            Session["MensajeError"] = "¡ERROR! Debe ser ub Cliente Registrado para acceder a esta pagina.";
            Response.Redirect("~/Logueo.aspx");
        }

        try
        {
            if (!IsPostBack)
            {
                XmlNode _MiDoc = new ServicioObligatorio.ServicioObligatorio().ListadoVisitasXml((Cliente)Session["ClienteRegistrado"]);

                XmlDocument _documento = new XmlDocument();
                _documento.LoadXml(_MiDoc.OuterXml);
                Session["DocumentoXml"] = _documento;

                xmlConsulta.DocumentContent = _documento.OuterXml;
            }
        }
        catch (System.Web.Services.Protocols.SoapException ex)
        {
            if (ex.Detail.InnerText == "")
                lblError.Text = "¡Error en Web Service!";
            else
                lblError.Text = ex.Detail.InnerText;
        }
        catch (Exception ex)
        {
            EstiloError();
            lblError.Text = ex.Message;
        }
    }

    protected void ibtnAplicarFiltro_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            XmlDocument _documento = (XmlDocument)Session["DocumentoXml"];

            XmlDocument _DocumentoFiltrado = new XmlDocument();
            _DocumentoFiltrado.LoadXml("<?xml version='1.0' encoding='utf-8' ?> <Raiz> </Raiz>");
            XmlNode _raiz = _DocumentoFiltrado.DocumentElement;

            if (ddTipoFiltro.SelectedValue == "Fecha")
            {
                string _fechaBuscada = (Convert.ToDateTime(txtBusqueda.Text)).ToShortDateString();

                XPathNavigator _Navegador = _documento.CreateNavigator();
                XPathNodeIterator _Resultado = _Navegador.Select("Raiz/Visita[Fecha = '" + _fechaBuscada + "']");

                Filtrar("Fecha", _DocumentoFiltrado, _Resultado, _raiz);
            }
            else if (ddTipoFiltro.SelectedValue == "Empresa")
            {
                string _NombreBuscado = txtBusqueda.Text.Trim();

                if (_NombreBuscado == string.Empty)
                    throw new Exception("Debe proporcionar un nombre de empresa para filtrar.");

                XPathNavigator _Navegador = _documento.CreateNavigator();
                XPathNodeIterator _Resultado = _Navegador.Select("Raiz/Visita[NomEmpresa = '" + _NombreBuscado + "']");

                Filtrar("Empresa", _DocumentoFiltrado, _Resultado, _raiz);
            }
            else
            {
                xmlConsulta.DocumentContent = _documento.OuterXml;
            }
        }
        catch (System.Web.Services.Protocols.SoapException ex)
        {
            if (ex.Detail.InnerText == "")
                lblError.Text = "¡Error en Web Service!";
            else
                lblError.Text = ex.Detail.InnerText;
        }
        catch (Exception ex)
        {
            EstiloError();
            lblError.Text = ex.Message;
        }
    }

    protected void ibtnResetFiltro_Click(object sender, ImageClickEventArgs e)
    {
        ddTipoFiltro.SelectedValue = "Ninguno";
        txtBusqueda.Text = "";
        lblError.Text = "";
    }

    protected void Filtrar(string pTipoBusqueda, XmlDocument pDocumentoFiltrado, XPathNodeIterator pResultado, XmlNode pRaiz)
    {
        try
        {
            if (pResultado.Count > 0)
            {
                while (pResultado.MoveNext())
                {
                    XmlElement _Nodo = pDocumentoFiltrado.CreateElement("Visita");

                    XmlElement _Fecha = pDocumentoFiltrado.CreateElement("Fecha");
                    _Fecha.InnerText = pResultado.Current.SelectSingleNode("Fecha").ToString();
                    _Nodo.AppendChild(_Fecha);

                    XmlElement _NomEmpresa = pDocumentoFiltrado.CreateElement("NomEmpresa");
                    _NomEmpresa.InnerText = pResultado.Current.SelectSingleNode("NomEmpresa").ToString();
                    _Nodo.AppendChild(_NomEmpresa);

                    pRaiz.AppendChild(_Nodo);
                }
                xmlConsulta.DocumentContent = pDocumentoFiltrado.OuterXml;
            }
            else
            {
                if (pTipoBusqueda == "Fecha")
                    lblError.Text = "No tiene visitas en la fecha: " + txtBusqueda.Text;
                else
                    lblError.Text = "No tiene visitas en la Empresa: " + txtBusqueda.Text;
            }
        }
        catch (Exception ex)
        {
            EstiloError();
            lblError.Text = ex.Message;
        }
    }

    protected void EstiloError()
    {
        lblError.ForeColor = System.Drawing.Color.Red;
        lblError.CssClass = "mensajeerror";
    }

}