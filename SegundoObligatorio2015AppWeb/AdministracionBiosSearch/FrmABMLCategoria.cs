using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AdministracionBiosSearch.ServicioObligatorio;
using System.Xml;
using System.Web.Services.Protocols;

namespace AdministracionBiosSearch
{
    public partial class FrmABMLCategoria : Form
    {
        private Categoria _unaCategoria;

        public FrmABMLCategoria()
        {
            InitializeComponent();
            txtId.Focus();
        }
   
        #region Botones

        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                if (_unaCategoria == null)
                    throw new Exception("No hay una categoria en memoria para poder crearla.");

                _unaCategoria.Nombre = txtNombre.Text.Trim();
                _unaCategoria.Descripcion = txtDescripcion.Text;

                new ServicioObligatorio.ServicioObligatorio().AltaCategoria(_unaCategoria);

                DesactivarBotones();
                LimpiarCampos();
                lblError.Text = "¡Categoria agregada con éxito!";
            
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
               
                lblError.Text = ex.Message;
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (_unaCategoria == null)
                    throw new Exception("No hay una categoria en memoria para poder eliminarla.");

                DialogResult resultado = MessageBox.Show("¿Esta seguro que desea Eliminar esta Categoria?", "Advertencia!!", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    new ServicioObligatorio.ServicioObligatorio().BajaCategoria(_unaCategoria);

                    DesactivarBotones();
                    LimpiarCampos();
                    CargarGV();
                    lblError.Text = "¡Categoria Eliminada con éxito!";
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

                lblError.Text = ex.Message;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_unaCategoria == null)
                    throw new Exception("No hay una categoria en memoria para poder modificarla.");

                _unaCategoria.Nombre = txtNombre.Text;
                _unaCategoria.Descripcion = txtDescripcion.Text;

                DialogResult resultado = MessageBox.Show("¿Esta seguro que desea Modificar esta ciudad?", "Advertencia!!", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    new ServicioObligatorio.ServicioObligatorio().ModificarCategoria(_unaCategoria);
                    DesactivarBotones();
                    LimpiarCampos();
                    CargarGV();
                    lblError.Text = "¡Categoria Modificada con éxito!";
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

                lblError.Text = ex.Message;
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            txtId.Enabled = true;
            txtId.Focus();
            epCategoria.Clear();
            CargarGV();
        }

        #endregion

        #region Mantenimiento

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string _idCategoria = txtId.Text.Trim();
            try
            {
                if (_idCategoria.Length != 3 || !(char.IsLetter(_idCategoria[0])) || !(char.IsLetter(_idCategoria[1])) || !(char.IsLetter(_idCategoria[2])))
                    throw new Exception("El identificador de la categoria debe tener 3 letras.");
                else
                    epCategoria.Clear();

                Categoria c = new ServicioObligatorio.ServicioObligatorio().BuscarCategoria(txtId.Text);

                if (c == null)
                {
                    btnAlta.Enabled = true;
                    txtId.Enabled = false;

                    lblError.Text = "No se encontro ninguna categoria con el Id: " + _idCategoria + " (si desea puede agregarla).";

                    _unaCategoria = new Categoria();
                    _unaCategoria.Identificador = txtId.Text;
                }
                else
                {
                    _unaCategoria = c;

                    txtId.Text = c.Identificador;
                    txtNombre.Text = c.Nombre;
                    txtDescripcion.Text = c.Descripcion;

                    txtId.Enabled = false;
                    btnBaja.Enabled = true;
                    btnModificar.Enabled = true;
                    lblError.Text = "¡Categoria encontrada!";
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
                epCategoria.SetError(txtId, ex.Message);
                e.Cancel = true;
                lblError.Text = ex.Message;
            }

        }

        private void CargarGV()
        {
            dgvCategorias.DataSource = new ServicioObligatorio.ServicioObligatorio().ListarCategoria();
            lblError.Text = "";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            DesactivarBotones();
            LimpiarCampos();
            _unaCategoria = null;
            lblError.Text = "";
            epCategoria.Clear();
            dgvCategorias.DataSource = null;
        }

        private void DesactivarBotones()
        {
            btnAlta.Enabled = false;
            btnBaja.Enabled = false;
            btnModificar.Enabled = false;
            txtId.Enabled = true;
            txtId.Focus();
        }

        private void LimpiarCampos()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        #endregion
    }
}
