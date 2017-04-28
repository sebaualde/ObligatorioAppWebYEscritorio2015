using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServicioObligatorio;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Session["ClienteRegistrado"] is Cliente))
        {
            Menu1.Visible = true;
            Menu2.Visible = false;
        }
        else
        {
            Menu2.Visible = true;
            Menu1.Visible = false;
        }

        if (!IsPostBack)
        {
            LimpiarRepiter();
            LimpiarCategorias();
            LimpiarCiudades();
        }
    }

    #region Carga de Menus

    protected void menDepartamentos_MenuItemClick(object sender, MenuEventArgs e)
    {
        try
        {

            if (menDepartamentos.SelectedValue != "Departamentos")
            {
                List<Ciudad> _Ciudades = (new ServicioObligatorio.ServicioObligatorio().ListarCiudad(menDepartamentos.SelectedValue)).ToList();

                if (_Ciudades.Count != 0)
                {

                    lblError.Text = "";
                    lblError.CssClass = "";
                    LimpiarRepiter();
                    LimpiarCategorias();
                    menCiudades.Enabled = true;

                    menCiudades.Items.Clear();
                    MenuItem _NodoRaiz = new MenuItem("Ciudades");
                    menCiudades.Items.Add(_NodoRaiz);

                    foreach (Ciudad c in _Ciudades)
                    {
                        MenuItem _mHijo = new MenuItem(c.Nombre);
                        _NodoRaiz.ChildItems.Add(_mHijo);
                    }
                }
                else
                {
                    LimpiarRepiter();
                    LimpiarCategorias();
                    LimpiarCiudades();
                    EstiloError();
                    lblError.Text = "No existen Ciudades registradas en el Departamento de " + menDepartamentos.SelectedItem.Text;
                }
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

    protected void menCiudades_MenuItemClick(object sender, MenuEventArgs e)
    {
        try
        {
            //si en el menu no se selecciono el titulo entramos a la busqueda de categorias
            if (menCiudades.SelectedValue != "Ciudades")
            {
                ServicioObligatorio.ServicioObligatorio _unServicio = new ServicioObligatorio.ServicioObligatorio();

                Ciudad _ciudad = _unServicio.BuscarCiudad(menDepartamentos.SelectedValue, menCiudades.SelectedValue);
                Session["BuscarCiudad"] = _ciudad;
                List<Categoria> _Categorias = (_unServicio.ListarCategoria()).ToList(); //.ListarXCiudad(_ciudad);

                //si el listado de categorias no esta vacio entramos
                if (_Categorias.Count != 0)
                {
                    lblError.Text = "";
                    lblError.CssClass = "";
                    menCategorias.Enabled = true;

                    menCategorias.Items.Clear();
                    MenuItem _NodoRaiz = new MenuItem("Categorias");
                    menCategorias.Items.Add(_NodoRaiz);

                    foreach (Categoria c in _Categorias)
                    {
                        MenuItem _mHijo = new MenuItem(c.Nombre, c.Identificador);
                        _NodoRaiz.ChildItems.Add(_mHijo);
                    }
                }
                else
                {
                    LimpiarRepiter();
                    LimpiarCategorias();
                    EstiloError();
                    lblError.Text = "No existen categorias en la ciudad de " + menCiudades.SelectedItem.Text;
                }
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

    protected void menCategorias_MenuItemClick(object sender, MenuEventArgs e)
    {
        try
        {
            ServicioObligatorio.ServicioObligatorio _unServicio = new ServicioObligatorio.ServicioObligatorio();

            if (menCategorias.SelectedValue != "Categorias")
            {
                lblTitulo.Text = "Departamento: " + menDepartamentos.SelectedItem.Text + " -- Ciudad: " + menCiudades.SelectedValue;

                Ciudad _ciudad = (Ciudad)Session["BuscarCiudad"];

                Categoria _categoria = _unServicio.BuscarCategoria(menCategorias.SelectedValue);

                List<Empresa> _empresas = (_unServicio.ListarEmpresasXCiudadYCategoria(_ciudad, _categoria)).ToList();

                if (_empresas.Count == 0)
                {
                    lblError.Text = "No se encontraron empresas en la categoria " + _categoria.Nombre + ".";
                    this.LimpiarRepiter();
                }
                else
                {
                    lblError.Text = "";
                    rpEmpresas.DataSource = _empresas;
                    rpEmpresas.DataBind();
                }
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

    #endregion

    //ENVIO DE EMPRESA A PAGINA DETALLES
    protected void rpEmpresas_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            Empresa _empresa = new ServicioObligatorio.ServicioObligatorio().BuscarEmpresa(((TextBox)(e.Item.Controls[1])).Text);

            Session["DetalleEmpresa"] = _empresa;
            Response.Redirect("~/ConsultaIndividualEmpresa.aspx");
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

    //DESHABILITACION DE DETALLES EN REPITER
    protected void rpEmpresas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (!(Session["ClienteRegistrado"] is Cliente))
        {
            Button b = e.Item.FindControl("btnSeleccionar") as Button;
            b.Visible = false;

            EstiloError();
            lblError.Text = "Para ver los detalles de las empresas debe ser un usuario registrado.";
        }
    }

    //DESLOGUEO DE CLIENTE
    protected void Menu2_MenuItemClick(object sender, MenuEventArgs e)
    {
        if (e.Item.Value == "Desloguearse")
        {
            Session.Remove("ClienteRegistrado");
            Response.Redirect("~/Default.Aspx");
        }
    }

    #region Mantenimiento

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        lblError.CssClass = "";
        LimpiarRepiter();
        LimpiarCategorias();
        LimpiarCiudades();
    }

    protected void LimpiarCiudades()
    {
        menCiudades.Items.Clear();
        MenuItem _NodoRaiz = new MenuItem("Ciudades");
        menCiudades.Items.Add(_NodoRaiz);
        menCiudades.Enabled = false;
    }

    protected void LimpiarCategorias()
    {
        menCategorias.Items.Clear();
        MenuItem _NodoRaizc = new MenuItem("Categorias");
        menCategorias.Items.Add(_NodoRaizc);
        menCategorias.Enabled = false;
    }

    protected void LimpiarRepiter()
    {
        lblTitulo.Text = "";
        rpEmpresas.DataSource = "";
        rpEmpresas.DataBind();
    }

    protected void EstiloError()
    {
        lblError.ForeColor = System.Drawing.Color.Red;
        lblError.CssClass = "mensajeerror";
    }

    #endregion

}