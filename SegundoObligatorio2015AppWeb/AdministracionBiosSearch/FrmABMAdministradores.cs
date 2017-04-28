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
    public partial class FrmABMAdministradores : Form
    {
        private Administrador _AdminLogueado = null;
        private Administrador _UsuarioBuscado = null;

        public FrmABMAdministradores(Administrador pAdmin)
        {
            InitializeComponent();
            _AdminLogueado = pAdmin;
            txtCI.Focus();
        }

        #region Botones

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                _UsuarioBuscado = new Administrador();

                _UsuarioBuscado.CI = Convert.ToInt32(txtCI.Text.Trim());
                _UsuarioBuscado.Nombre = txtNombre.Text;
                _UsuarioBuscado.NombreUsuario = txtUsuario.Text;
                if (txtContrasenia.Text.Trim() == txtConfContrasenia.Text.Trim())
                    _UsuarioBuscado.Contrasenia = txtContrasenia.Text;
                else
                    lblMensaje.Text = "Las contraseñas no coinciden";
                _UsuarioBuscado.VeListado = cbListado.Checked;

                new ServicioObligatorio.ServicioObligatorio().AltaUsuario(_UsuarioBuscado);

                DeshabilitarBotones();
                LimpiarCampos();

                lblMensaje.Text = "Se agrego el administrador correctamente.";
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
                if (_UsuarioBuscado == null)
                {
                    DesactivarBotones();
                }
                lblMensaje.Text = "¡Error! " + ex.Message;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                _UsuarioBuscado.Nombre = txtNombre.Text;
                _UsuarioBuscado.NombreUsuario = txtUsuario.Text;
                if (txtContrasenia.Text != string.Empty)
                {
                    if (txtContrasenia.Text.Length != 5)
                        throw new Exception("La contraseña debe contener 5 caracteres");
                    else if (txtContrasenia.Text.Trim() != txtConfContrasenia.Text.Trim())
                        throw new Exception("Las contraseñas no coinciden");
                    else
                        _UsuarioBuscado.Contrasenia = txtContrasenia.Text.Trim();
                }

                if (_AdminLogueado.CI == Convert.ToInt32(txtCI.Text))
                    cbListado.Enabled = false;
                else
                    _UsuarioBuscado.VeListado = cbListado.Checked;

                DialogResult resultado = MessageBox.Show("¿Esta seguro que desea Modificar esta administrador?", "Advertencia!!", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    new ServicioObligatorio.ServicioObligatorio().ModificarUsuario(_UsuarioBuscado);

                    DeshabilitarBotones();
                    LimpiarCampos();

                    lblMensaje.Text = "¡Administrador modificado corectamente!";
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
                txtContrasenia.Enabled = true;
                txtNombre.Enabled = true;
                txtUsuario.Enabled = true;
                if (_AdminLogueado.CI == Convert.ToInt32(txtCI.Text))
                    cbListado.Enabled = false;
                else
                {
                    cbListado.Enabled = true;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;

                    txtCI.Enabled = false;
                    lblMensaje.Text = ex.Message;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_UsuarioBuscado is Administrador)
                {
                    DialogResult resultado = MessageBox.Show("¿Esta seguro que desea Eliminar este Administrador?", "Advertencia!!", MessageBoxButtons.YesNo);

                    if (resultado == DialogResult.Yes)
                    {
                        new ServicioObligatorio.ServicioObligatorio().BajaUsuario(_UsuarioBuscado);
                        DeshabilitarBotones();
                        LimpiarCampos();
                        lblMensaje.Text = "¡Administrador Eliminado exitosamente!";
                    }
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
            DeshabilitarBotones();
            LimpiarCampos();

            lblMensaje.Text = "";

            txtNombre.Enabled = true;
            txtUsuario.Enabled = true;
            txtContrasenia.Enabled = true;
            txtConfContrasenia.Enabled = true;
            cbListado.Enabled = true;
        }

        #endregion

        #region Mantenimiento

        private void txtCI_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtCI.Text.Trim() == string.Empty)
                    throw new Exception("Por favor ingrese una CI");

                int _ci = Convert.ToInt32(txtCI.Text.Trim());

                if (_ci.ToString().Length != 8)
                    throw new Exception("La cedula debe tener 8 caracteres");

                if (_ci < 0)
                    throw new Exception("La cedula no puede ser negativa");

                epError.Clear();
                lblMensaje.Text = "";

                Usuario _usuario = new ServicioObligatorio.ServicioObligatorio().BuscarUsuario(_ci);

                if (_usuario == null)
                {
                    lblMensaje.Text = "No se encontro ningun administrador con CI: " + _ci + " Desea agreagarlo?";
                    btnAgregar.Enabled = true;
                    txtCI.Enabled = false;
                }

                else if (_usuario is Administrador)
                {
                    txtCI.Enabled = false;

                    if (_AdminLogueado.CI == Convert.ToInt32(txtCI.Text))
                        cbListado.Enabled = false;
                    else
                        cbListado.Enabled = true;

                    _UsuarioBuscado = new Administrador();

                    _UsuarioBuscado.CI = _usuario.CI;
                    _UsuarioBuscado.Nombre = _usuario.Nombre;
                    _UsuarioBuscado.NombreUsuario = _usuario.NombreUsuario;
                    _UsuarioBuscado.Contrasenia = _usuario.Contrasenia;
                    _UsuarioBuscado.VeListado = ((Administrador)_usuario).VeListado;

                    txtCI.Text = _usuario.CI.ToString();
                    txtNombre.Text = _usuario.Nombre;
                    txtUsuario.Text = _usuario.NombreUsuario;
                    cbListado.Checked = ((Administrador)_usuario).VeListado;

                    ActivarBotonA();

                    if (_AdminLogueado.CI == Convert.ToInt32(txtCI.Text))
                        btnEliminar.Enabled = false;
                }

                else
                {
                    lblMensaje.Text = "Ya existe un usuario con esa cedula asociada";
                    txtNombre.Enabled = false;
                    txtUsuario.Enabled = false;
                    txtContrasenia.Enabled = false;
                    txtConfContrasenia.Enabled = false;
                    cbListado.Enabled = false;
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
                epError.SetError(txtCI, ex.Message);
                e.Cancel = true;
                lblMensaje.Text = ex.Message;
            }
        }

        private void txtContrasenia_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtContrasenia.Text.Trim().ToString().Length != 5)
                    throw new Exception("La contraseña tiene que tener 5 caracteres!");
                else
                {
                    epError.Clear();
                    lblMensaje.Text = "";
                }
            }

            catch (Exception ex)
            {
                epError.SetError(txtContrasenia, ex.Message);
                e.Cancel = false;
                lblMensaje.Text = ex.Message;
            }
        }

        private void txtConfContrasenia_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtContrasenia.Text != txtConfContrasenia.Text)
                    throw new Exception("Las contraseñas no coinciden!!");
                else
                {
                    epError.Clear();
                    lblMensaje.Text = "";
                }

            }
            catch (Exception ex)
            {
                epError.SetError(txtConfContrasenia, ex.Message);
                e.Cancel = false;
                lblMensaje.Text = ex.Message;
            }
        }

        private void LimpiarCampos()
        {
            txtCI.Enabled = true;
            txtCI.Focus();
            txtCI.Text = "";
            txtNombre.Text = "";
            txtUsuario.Text = "";
            txtContrasenia.Text = "";
            txtConfContrasenia.Text = "";
            cbListado.Checked = false;
        }

        private void ActivarBotonA()
        {
            btnAgregar.Enabled = false;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void DesactivarBotones()
        {
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void DeshabilitarBotones()
        {
            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        #endregion
    }
}
