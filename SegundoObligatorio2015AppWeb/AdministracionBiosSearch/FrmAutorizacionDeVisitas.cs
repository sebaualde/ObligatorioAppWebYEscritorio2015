using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AdministracionBiosSearch.ServicioObligatorio;
using System.Configuration;
using System.Messaging;

namespace AdministracionBiosSearch
{
    public partial class FrmAutorizacionDeVisitas : Form
    {
        private MessageQueue _colaVisitas;
        private List<Empresa> _listaVisitas;
        private Empresa _objVisita;

        public FrmAutorizacionDeVisitas()
        {
            InitializeComponent();
            CargarCola();
        }

        private void FrmAutorizacionDeVisitas_Load(object sender, EventArgs e)
        {
            try
            {
                _colaVisitas.BeginReceive(new TimeSpan(1, 0, 0, 0));

                _colaVisitas.ReceiveCompleted += new ReceiveCompletedEventHandler(Recepcion);

                gvColaVisitas.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                btnActualizar.Enabled = false;
                MessageBox.Show(ex.Message, "Error en MSMQ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Recepcion(object sender, ReceiveCompletedEventArgs args)
        {
            try
            {

                System.Messaging.Message _unMensaje = _colaVisitas.EndReceive(args.AsyncResult);

                Empresa _unaEmpresa = (Empresa)_unMensaje.Body;

                _listaVisitas.Add(_unaEmpresa);

                _colaVisitas.BeginReceive(new TimeSpan(1, 0, 0, 0));

            }
            catch {}

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                gvColaVisitas.DataSource = null;
                gvColaVisitas.DataSource = _listaVisitas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en MSMQ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmAutorizacionDeVisitas_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_listaVisitas.Count > 0)
                {
                    DialogResult _respuesta = MessageBox.Show("Hay Visitas sin autorizar. \nSi sale del formulario dichas visitas se perderan. \nEsta seguro que desea salir de todos modos?", "PERDIDA INMINENTE DE DATOS", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                    if (_respuesta == System.Windows.Forms.DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }

                _colaVisitas.Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void gvColaVisitas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _objVisita = _listaVisitas[e.RowIndex];

                dtpFechaVisita.Value = _objVisita.Visitas.Last().FechaYHora;
                lblCliente.Text = _objVisita.Visitas.Last().Cliente.Nombre;
                lblEmpresa.Text = _objVisita.Nombre;

                btnAgregar.Enabled = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                _objVisita.Visitas.Last().FechaYHora = DateTime.Now;

                if (rbAceptar.Checked)
                {
                    _objVisita.Visitas.Last().VisitaAceptada = true;

                    new ServicioObligatorio.ServicioObligatorio().NuevaVisita(_objVisita);

                    lblMensaje.Text = "Visita agregada Correctamente";
                }
                else
                {
                    _objVisita.Visitas.Last().VisitaAceptada = false;

                    new ServicioObligatorio.ServicioObligatorio().NuevaVisita(_objVisita);

                    lblMensaje.Text = "Visita agregada pero rechazada";
                }

                _listaVisitas.Remove(_objVisita);
                gvColaVisitas.DataSource = null;
                gvColaVisitas.DataSource = _listaVisitas;

                btnAgregar.Enabled = false;

                dtpFechaVisita.Value = DateTime.Now;
                lblCliente.Text = "Datos del Cliente";
                lblEmpresa.Text = "Datos de la Empresa";
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
                lblMensaje.Text = ex.Message;
            }
        }

        private void CargarCola()
        {
            string _Direccion = ConfigurationManager.AppSettings["ColaVisita"];
            _colaVisitas = new MessageQueue(_Direccion);
            _colaVisitas.MessageReadPropertyFilter.SetAll();

            ((XmlMessageFormatter)_colaVisitas.Formatter).TargetTypes = new Type[] { typeof(Empresa) };

            _listaVisitas = new List<Empresa>();

            _objVisita = null;

            btnAgregar.Enabled = false;

            dtpFechaVisita.Value = DateTime.Now;
            lblCliente.Text = "Datos del Cliente";
            lblEmpresa.Text = "Datos de la Empresa";
        }

    }
}
