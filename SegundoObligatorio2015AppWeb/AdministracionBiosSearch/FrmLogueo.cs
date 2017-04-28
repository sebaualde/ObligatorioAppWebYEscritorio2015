using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AdministracionBiosSearch.ServicioObligatorio;

namespace AdministracionBiosSearch
{
    public partial class FrmLogueo : Form
    {
        public FrmLogueo()
        {
            InitializeComponent();
            controlLogIn1.AutenticarUsuario += new EventHandler(ComprobarUsuario);
        }

        public void ComprobarUsuario(object sender, EventArgs e)
        {
            try
            {
                Usuario _unUsuario = new ServicioObligatorio.ServicioObligatorio().LogueoUsuario(controlLogIn1.NombreUsuario, controlLogIn1.Contraseña);

                if (_unUsuario == null || controlLogIn1.Contraseña.Length != 5)
                    lblMensaje.Text = "Error! Nombre de Usuario o Contraseña Incorrectos";
                else if (_unUsuario is Cliente)
                    lblMensaje.Text = "Los Clientes no tienen autorizacion para usar la aplicación.";
                else
                {
                    Administrador _adminLogueado = (Administrador)_unUsuario;
                    this.Hide();
                    Form _unForm = new FrmPrincipal(_adminLogueado);
                    _unForm.ShowDialog();
                    this.Close();
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
                lblMensaje.Text = ex.Message;
            }
        }

    }
}