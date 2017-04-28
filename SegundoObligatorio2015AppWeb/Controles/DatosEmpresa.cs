using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controles
{
    public class DatosEmpresa : WebControl, INamingContainer
    {
        private Label _lblNombre;
        private Label _lblRut;
        private Label _lblCategoría;
        private Label _lblDepto;
        private Label _lblCiudad;
        private Label _lblDireccion;
        private GridView _gvTelefonos;
        private Label _lblVisitas;
        private Panel _panel;
        private Label _lblMensaje = new Label();

        public DatosEmpresa()
        {
            base.CreateChildControls();

            _panel = new Panel();

            _lblNombre = new Label();
            _lblNombre.Height = Unit.Pixel(15);
            _lblNombre.Width = Unit.Percentage(100);
            _panel.Controls.Add(new LiteralControl("<table><tr><td colspan=\"2\"><h2 style=\"text-align: center\">"));
            _panel.Controls.Add(_lblNombre);

            _lblRut = new Label();
            _lblRut.Height = Unit.Pixel(15);
            _lblRut.Width = Unit.Pixel(150);
            _panel.Controls.Add(new LiteralControl("</h2></td></tr><tr><td style=\"text-align: right\">RUT:</td><td>"));
            _panel.Controls.Add(_lblRut);

            _lblCategoría = new Label();
            _lblCategoría.Height = Unit.Pixel(15);
            _lblCategoría.Width = Unit.Pixel(150);
            _panel.Controls.Add(new LiteralControl("</td></tr><tr><td style=\"text-align: right\">Categoría:</td><td>"));
            _panel.Controls.Add(_lblCategoría);

            _lblDepto = new Label();
            _lblDepto.Height = Unit.Pixel(15);
            _lblDepto.Width = Unit.Pixel(150);
            _panel.Controls.Add(new LiteralControl("</td></tr><tr><td style=\"text-align: right\">Departamento:</td><td>"));
            _panel.Controls.Add(_lblDepto);

            _lblCiudad = new Label();
            _lblCiudad.Height = Unit.Pixel(15);
            _lblCiudad.Width = Unit.Pixel(150);
            _panel.Controls.Add(new LiteralControl("</td></tr><tr><td style=\"text-align: right\">Ciudad:</td><td>"));
            _panel.Controls.Add(_lblCiudad);

            _lblDireccion = new Label();
            _lblDireccion.Height = Unit.Pixel(15);
            _lblDireccion.Width = Unit.Pixel(150);
            _panel.Controls.Add(new LiteralControl("</td></tr><tr><td style=\"text-align: right\">Dirección:</td><td>"));
            _panel.Controls.Add(_lblDireccion);

            _gvTelefonos = new GridView();
            _panel.Controls.Add(new LiteralControl("</td></tr><tr><td style=\"text-align: right\">Teléfonos:</td><td>"));
            _panel.Controls.Add(_gvTelefonos);

            _lblVisitas = new Label();
            _lblVisitas.Height = Unit.Pixel(15);
            _lblVisitas.Width = Unit.Pixel(150);
            _panel.Controls.Add(new LiteralControl("</td></tr><tr><td style=\"text-align: right\">Visitas:</td><td>"));
            _panel.Controls.Add(_lblVisitas);

            _panel.Controls.Add(new LiteralControl("</td></tr></table>"));

            this.Controls.Add(_panel);

        }

        public void MostrarEmpresa(string pNombre, string pRut, string pCategoria, string pDepartamento, string pCiudad, string pDireccion, List<string> pTelefonos, string pCantidadvisitas)
        {
            _lblNombre.Text = pNombre;
            _lblRut.Text = pRut;
            _lblCategoría.Text = pCategoria;
            switch (pDepartamento.ToUpper())
            {
                case "A":
                    _lblDepto.Text = "Canelones";
                    break;
                case "B":
                    _lblDepto.Text = "Maldonado";
                    break;
                case "C":
                    _lblDepto.Text = "Rocha";
                    break;
                case "D":
                    _lblDepto.Text = "Treinta y Tres";
                    break;
                case "E":
                    _lblDepto.Text = "Cerro Largo";
                    break;
                case "F":
                    _lblDepto.Text = "Rivera";
                    break;
                case "G":
                    _lblDepto.Text = "Artigas";
                    break;
                case "H":
                    _lblDepto.Text = "Salto";
                    break;
                case "I":
                    _lblDepto.Text = "Paysandú";
                    break;
                case "J":
                    _lblDepto.Text = "Río Negro";
                    break;
                case "K":
                    _lblDepto.Text = "Soriano";
                    break;
                case "L":
                    _lblDepto.Text = "Colonia";
                    break;
                case "M":
                    _lblDepto.Text = "San José";
                    break;
                case "N":
                    _lblDepto.Text = "Flores";
                    break;
                case "O":
                    _lblDepto.Text = "Florida";
                    break;
                case "P":
                    _lblDepto.Text = "Lavalleja";
                    break;
                case "Q":
                    _lblDepto.Text = "Durazno";
                    break;
                case "R":
                    _lblDepto.Text = "Tacuarembó";
                    break;
                case "S":
                    _lblDepto.Text = "Montevideo";
                    break;

            }

            _lblCiudad.Text = pCiudad;
            _lblDireccion.Text = pDireccion;
            _gvTelefonos.DataSource = pTelefonos.ToList();
            _gvTelefonos.DataBind();
            _gvTelefonos.BorderWidth = 0;
            _gvTelefonos.HeaderRow.Visible = false;
            _lblVisitas.Text = pCantidadvisitas;
        }
    }
}
