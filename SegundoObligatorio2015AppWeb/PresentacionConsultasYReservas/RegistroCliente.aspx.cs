using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServicioObligatorio;

public partial class RegistroCliente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Cliente)Session["ClienteRegistrado"] is Cliente)
            Response.Redirect("~/Default.aspx");
        btnBuscar.Enabled = true;
        btnAgregar.Enabled = false;
        
        this.DesactivarCampos();
    }

    #region Botones

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            int _ci = Convert.ToInt32(txtCI.Text.Trim());
            Usuario _usuario = new ServicioObligatorio.ServicioObligatorio().BuscarUsuario(_ci);

            if (_ci.ToString().Length != 8)
                throw new Exception("La cedula debe tener 8 caracteres");

            if ((Convert.ToInt32(txtCI.Text)) < 0)
                throw new Exception("La cedula no puede ser negativa");

            if (_usuario == null)
            {
                this.ActivarCamposyBtns();
                lblMensaje.CssClass = "mensajeinfo";
                lblMensaje.Text= "No se encontro ningun cliente con CI: " + _ci + " Desea agreagarlo?";
            }
            else
            {
                this.DesactivarCampos();
                throw new Exception("CI ya asignada a un usuario");
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

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            ServicioObligatorio.ServicioObligatorio _miServicio = new ServicioObligatorio.ServicioObligatorio();

            int ci = Convert.ToInt32(txtCI.Text.Trim());
            string nombre = txtNombre.Text;
            string usuario = txtUsuario.Text;
            string contrasenia = txtContrasenia.Text;
            int edad = Convert.ToInt32(txtEdad.Text.Trim());

            _miServicio.BuscarUsuario(ci);

            Cliente cliente = new Cliente();
            cliente.CI = ci;
            cliente.Nombre = nombre;
            cliente.NombreUsuario = usuario;
            cliente.Contrasenia = contrasenia;
            cliente.Edad = edad;

            _miServicio.AltaUsuario(cliente);

            lblMensaje.Text = "Se agrego el cliente correctamente.";

            this.LimpiarTxt();
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
            this.ActivarCamposyBtns();

            EstiloError();
            lblMensaje.Text = "¡Error! " + ex.Message;
        }

    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtCI.Enabled = true;
        this.LimpiarTxt();
    }

    #endregion

    #region Mantenimiento

    protected void LimpiarTxt()
    {
        txtCI.Text = "";
        txtContrasenia.Text = "";
        txtNombre.Text = "";
        txtUsuario.Text = "";
        txtEdad.Text = "";
    }

    protected void DesactivarCampos()
    {
        txtCI.Enabled = true;
        txtContrasenia.Enabled = false;
        txtNombre.Enabled = false;
        txtUsuario.Enabled = false;
        txtEdad.Enabled = false;
    }

    protected void ActivarCamposyBtns()
    {
        btnBuscar.Enabled = false;
        btnAgregar.Enabled = true;

        txtCI.Enabled = false;
        txtContrasenia.Enabled = true;
        txtNombre.Enabled = true;
        txtUsuario.Enabled = true;
        txtEdad.Enabled = true;
    }

    protected void EstiloError()
    {
        lblMensaje.ForeColor = System.Drawing.Color.Red;
        lblMensaje.CssClass = "mensajeerror";
    }

    #endregion

}