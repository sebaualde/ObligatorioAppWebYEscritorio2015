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
    public partial class FrmEmpresas : Form
    {
        private Empresa _empresa;

        public FrmEmpresas()
        {
            InitializeComponent();
            txtRut.Focus();
        }

        #region Botones

        private void toolStripbtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string rut = txtRut.Text;
                string nombre = txtNombre.Text;
                string direccion = txtDireccion.Text;

                if (string.IsNullOrEmpty(nombre))
                {
                    throw new Exception("Ingresa un nombre");
                }
                if (string.IsNullOrEmpty(direccion))
                {
                    throw new Exception("Ingresa una dirección");
                }
                if (ddlCiudad.SelectedIndex == -1)
                {
                    throw new Exception("Selecciona una ciudad");
                }

                Categoria categoria = new ServicioObligatorio.ServicioObligatorio().BuscarCategoria(ddlcategoria.SelectedValue.ToString());

                Ciudad ciudad = new ServicioObligatorio.ServicioObligatorio().BuscarCiudad(ddlDepto.SelectedValue.ToString(), ddlCiudad.SelectedValue.ToString());

                List<Telefono> telefonos = new List<Telefono>();

                foreach (string nro in controlTelefonos1.Telefonos)
                {
                    Telefono tel = new Telefono();
                    tel.Numero = nro;
                    telefonos.Add(tel);
                }

                Empresa empresa = new Empresa();
                empresa.Rut = rut;
                empresa.Nombre = nombre;
                empresa.Direccion = direccion;
                empresa.Telefonos = telefonos.ToArray();
                empresa.Ciudad = ciudad;
                empresa.Categoria = categoria;
                empresa.Visitas = new List<Visita>().ToArray();

                new ServicioObligatorio.ServicioObligatorio().AltaEmpresa(empresa);

                Limpiar();

                lblMensaje.Text = "Empresa agregada correctamente.";
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                _empresa.Nombre = txtNombre.Text.Trim();
                _empresa.Direccion = txtDireccion.Text.Trim();

                if (string.IsNullOrEmpty(_empresa.Nombre))
                {
                    throw new Exception("Ingresa un nombre");
                }
                if (string.IsNullOrEmpty(_empresa.Direccion))
                {
                    throw new Exception("Ingresa una dirección");
                }
                if (ddlCiudad.SelectedIndex == -1)
                {
                    throw new Exception("Selecciona una ciudad");
                }

                if (_empresa.Categoria.Identificador != ddlcategoria.SelectedValue.ToString())
                    _empresa.Categoria = new ServicioObligatorio.ServicioObligatorio().BuscarCategoria(ddlcategoria.SelectedValue.ToString());

                if (_empresa.Ciudad.Nombre != ddlCiudad.SelectedValue.ToString() || _empresa.Ciudad.CodDepto != ddlDepto.SelectedValue.ToString())
                    _empresa.Ciudad = new ServicioObligatorio.ServicioObligatorio().BuscarCiudad(ddlDepto.SelectedValue.ToString(), ddlCiudad.SelectedValue.ToString());

                List<Telefono> tels = new List<Telefono>();

                foreach (string nro in controlTelefonos1.Telefonos)
                {
                    Telefono tel = new Telefono();
                    tel.Numero = nro;

                    tels.Add(tel);
                }

                _empresa.Telefonos = tels.ToArray();

                new ServicioObligatorio.ServicioObligatorio().ModificarEmpresa(_empresa);

                Limpiar();
                lblMensaje.Text = "Empresa modificada con éxito";
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado = MessageBox.Show(String.Format("¿Esta seguro que desea eliminar la empresa {0}?", _empresa.Nombre), "Confirmar eliminación", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    new ServicioObligatorio.ServicioObligatorio().BajaEmpresa(_empresa);

                    Limpiar();
                    lblMensaje.Text = "Se eliminó la empresa \"" + _empresa.Nombre + "\"";
                }
                else
                {

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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                Limpiar();
                lblMensaje.Text = "";
                epRut.Clear();
            }
            catch
            {
                lblMensaje.Text = "No se pudo limpiar el formulario.";
            }

        }

        private void ddlDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDepto.SelectedIndex != 0)
                {
                    List<Ciudad> ciudades = new ServicioObligatorio.ServicioObligatorio().ListarCiudad(ddlDepto.SelectedValue.ToString()).ToList();
                    if (ciudades.Count != 0)
                    {
                        ddlCiudad.DataSource = ciudades;
                        ddlCiudad.DisplayMember = "Nombre";
                        ddlCiudad.ValueMember = "Nombre";
                        lblMensaje.Text = "";
                    }
                    else
                    {
                        ddlCiudad.DataSource = null;
                    }
                }
                else
                {
                    ddlCiudad.DataSource = null;
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

        #endregion

        #region Mantenimiento

        private void txtRut_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string rut = txtRut.Text;

                if (rut.Length != 12)
                {
                    throw new Exception("El RUT debe tener 12 caracteres");
                }
                try
                {
                    Convert.ToInt64(rut);
                }
                catch
                {
                    throw new Exception("Formato de RUT no válido");
                }

                if (Convert.ToInt64(rut) < 0)
                {
                    throw new Exception("El Rut no puede ser un número negativo");
                }

                Empresa empresa = new ServicioObligatorio.ServicioObligatorio().BuscarEmpresa(rut);

                if (empresa != null)
                {
                    _empresa = empresa;
                    EstadoControles(true);
                }
                else
                {
                    EstadoControles(false);
                }
                //si llega hasta acá se limpia el ErrorProvider 
                epRut.Clear();
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
                epRut.SetError(txtRut, ex.Message);
                e.Cancel = true;
                lblMensaje.Text = ex.Message;
            }
        }

        private void EstadoControles(bool encontrada)
        {
            if (encontrada)
            {
                btnAgregar.Enabled = false;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                txtRut.Enabled = false;
               

                txtNombre.Text = _empresa.Nombre;
                txtDireccion.Text = _empresa.Direccion;
                txtRut.Text = _empresa.Rut;
                ddlDepto.SelectedValue = _empresa.Ciudad.CodDepto;
                ddlCiudad.DataSource = new ServicioObligatorio.ServicioObligatorio().ListarCiudad(_empresa.Ciudad.CodDepto);
                ddlCiudad.SelectedValue = _empresa.Ciudad.Nombre;
                lblMensaje.Text = "¡Empresa encontrada!";

                List<string> tels = new List<string>();

                foreach (Telefono T in _empresa.Telefonos)
                {
                    tels.Add(T.Numero);
                }

                controlTelefonos1.Telefonos = tels;

                txtNombre.Focus();
                ddlcategoria.SelectedValue = _empresa.Categoria.Identificador;

            }
            else
            {
                btnAgregar.Enabled = true;
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
                txtRut.Enabled = false;
                txtNombre.Enabled = true;
                txtDireccion.Enabled = true;
                ddlDepto.Enabled = true;
                ddlCiudad.Enabled = true;
                ddlcategoria.Enabled = true;
                controlTelefonos1.Enabled = true;

                lblMensaje.Text = "Empresa no encontrada. Puede agregarla.";
                txtNombre.Focus();
            }
        }

        private void Limpiar()
        {
            //desactivo los botones
            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            //reinicio el estado de los campos
            txtRut.Enabled = true;
            txtRut.Text = "";
            txtDireccion.Text = "";
            txtNombre.Text = "";
           
            ddlDepto.SelectedIndex = 0;
            ddlCiudad.DataSource = null;
            
            controlTelefonos1.Limpiar();
            txtRut.Focus();
        }

        private void FrmEmpresas_Load(object sender, EventArgs e)
        {
            //Cargo las categorías
            ddlcategoria.DataSource = new ServicioObligatorio.ServicioObligatorio().ListarCategoria();
            ddlcategoria.ValueMember = "Identificador";
            ddlcategoria.DisplayMember = "Nombre";

            //Cargo los departamentos 
            ddlDepto.DisplayMember = "Text";
            ddlDepto.ValueMember = "Value";

            var departamentos = new[] { 
            new { Text = "Seleccione", Value = "0" }, 
            new { Text = "Artigas", Value = "G" }, 
            new { Text = "Canelones", Value = "A" },
            new { Text = "Cerro Largo", Value = "E" },
            new { Text = "Colonia", Value = "L" },
            new { Text = "Durazno", Value = "Q" },
            new { Text = "Flores", Value = "N" },
            new { Text = "Florida", Value = "O" },
            new { Text = "Lavalleja", Value = "P" },
            new { Text = "Maldonado", Value = "B" },
            new { Text = "Montevideo", Value = "S" },
            new { Text = "Paysandú", Value = "I" },
            new { Text = "Río Negro", Value = "J" },            
            new { Text = "Rivera", Value = "F" },
            new { Text = "Rocha", Value = "C" },
            new { Text = "Salto", Value = "H" },
            new { Text = "San José", Value = "M" },
            new { Text = "Soriano", Value = "K" },
            new { Text = "Tacuarembó", Value = "R" },
            new { Text = "Treinta y Tres", Value = "D" }
            };

            ddlDepto.DataSource = departamentos;
            ddlDepto.SelectedIndex = 0;
        }

        #endregion
    }
}
