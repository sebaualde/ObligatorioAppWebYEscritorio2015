using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServicioObligatorio;

public partial class Logueo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoginCLi_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
            string _nombreUsuario = LoginCLi.UserName.Trim();
            string _contrasenia = LoginCLi.Password.Trim();

            if (_contrasenia.Length > 5)
                throw new Exception("La contraseña no puede contener mas de 5 caracteres");

            Usuario _usuario = new ServicioObligatorio.ServicioObligatorio().LogueoUsuario(_nombreUsuario, _contrasenia);

            if (_usuario == null || !(_usuario is Cliente))
                LoginCLi.FailureText = "Nombre de usuario o contraseña incorrecta";
            else
            {
                Session["ClienteRegistrado"] = _usuario;
                Response.Redirect("~/Default.aspx");
            }
        }
        catch (System.Web.Services.Protocols.SoapException ex)
        {
            if (ex.Detail.InnerText == "")
                LoginCLi.FailureText = "¡Error en Web Service!";
            else
                LoginCLi.FailureText = ex.Detail.InnerText;
        }
        catch (Exception ex)
        {
            LoginCLi.FailureText = ex.Message;
        }
    }
}