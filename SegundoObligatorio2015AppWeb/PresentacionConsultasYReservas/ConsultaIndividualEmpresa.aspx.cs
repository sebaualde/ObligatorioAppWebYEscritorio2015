using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServicioObligatorio;
using System.Messaging;
using System.Collections;
using System.Configuration;


public partial class ConsultaIndividualEmpresa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!(Session["DetalleEmpresa"] is Empresa) || !(Session["ClienteRegistrado"] is Cliente))
                Response.Redirect("~/Default.aspx");
            else
            {
                
                Empresa empresa = (Empresa)Session["DetalleEmpresa"];

                //---------------------------------------Nueva visita-------------------------------------------
                //creo nueva visita
                Visita _nuevaVisita = new Visita();
                _nuevaVisita.Cliente = (Cliente)Session["ClienteRegistrado"];
                _nuevaVisita.FechaYHora = DateTime.Now;
                _nuevaVisita.VisitaAceptada = false;

                List<string> _telefonos = new List<string>();
                foreach (Telefono t in empresa.Telefonos)
                    _telefonos.Add(t.Numero);

                //se mandan los datos de la empresa al composite
                DatosEmpresa1.MostrarEmpresa(empresa.Nombre, empresa.Rut, empresa.Categoria.Nombre, empresa.Ciudad.CodDepto, empresa.Ciudad.Nombre, empresa.Direccion, _telefonos, Convert.ToString(empresa.Visitas.Length));

                //guardo datos de la nueva vista para enviar a MSMQ
                empresa.Visitas = new Visita[1] { _nuevaVisita };
                empresa.FechaUltimaVisita = Convert.ToString(_nuevaVisita.FechaYHora);
                empresa.NombreUltimaVisita = _nuevaVisita.Cliente.Nombre;

                //-------------------------------Envio de Datos de Nueva Visita a MSMQ---------------------------------

                MessageQueue _ColaDeVisitas = new MessageQueue(ConfigurationManager.AppSettings["ColaMensajes"]);

                _ColaDeVisitas.MessageReadPropertyFilter.SetAll();

                ((XmlMessageFormatter)_ColaDeVisitas.Formatter).TargetTypes = new Type[] { typeof(Empresa) };

                Message _MensajeEnviar = new Message(empresa);

                MessageQueueTransaction _Transaccion = new MessageQueueTransaction();
                _Transaccion.Begin();
                _ColaDeVisitas.Send(_MensajeEnviar, _Transaccion);
                _Transaccion.Commit();

                //---------------------------------------------------------------------------------------------------------

                Session.Remove("DetalleEmpresa");
            }
        }
        catch (System.Web.Services.Protocols.SoapException ex)
        {
            if (ex.Detail.InnerText == "")
                lblMensaje.Text = "¡Error en Web Service!";
            else
                lblMensaje.Text = ex.Detail.InnerText;
        }
        catch (Exception ex)
        {
            lblMensaje.CssClass = "mensajeerror";
            lblMensaje.Text = ex.Message;
        }
    }
}