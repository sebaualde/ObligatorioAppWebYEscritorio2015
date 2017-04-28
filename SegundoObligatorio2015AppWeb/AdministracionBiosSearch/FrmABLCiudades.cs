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
    public partial class FrmABLCiudades : Form
    {
        private Ciudad _unaCiudad;
        
        public FrmABLCiudades()
        {
            InitializeComponent();
            Limpiar();
            lblError.Text = "";
        }

        #region Botones
        //----------------------------Botones--------------------------------------
        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                if (_unaCiudad == null)
                    throw new Exception("No hay una ciudad en memoria para poder crearla.");

                new ServicioObligatorio.ServicioObligatorio().AltaCiudad(_unaCiudad);

                Limpiar();
                lblError.Text = "¡Ciudad agregada con éxito!";

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
                if (_unaCiudad == null)
                    throw new Exception("No hay una ciudad en memoria para poder eliminarla.");

                DialogResult resultado = MessageBox.Show("¿Esta seguro que desea Eliminar esta ciudad?", "Advertencia!!", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    new ServicioObligatorio.ServicioObligatorio().BajaCiudad(_unaCiudad);

                    Limpiar();
                    lblError.Text = "¡Ciudad eliminada con éxito!";
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

        #endregion

        #region Mantenimiento
        //realizo la busqueda al seleccionar un departamento ya que son necesario los dos campos para buscar una ciudad
        private void cbDepartamentos_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbDepartamentos.SelectedIndex == 0)
                {                    
                    dgvCiudades.Columns.Clear();
                    throw new Exception("Seleccione un departamento.");
                }

                string _codDep = CodigoDepto(cbDepartamentos.SelectedItem.ToString());

                CargarGV(_codDep);
     
                if (txtNombreCiudad.Text == string.Empty)
                    throw new Exception("Para agregar una ciudad coloque su nombre y seleccione un departamento.");

                Ciudad _CiudadBuscada = new ServicioObligatorio.ServicioObligatorio().BuscarCiudad(_codDep, txtNombreCiudad.Text);

                if (_CiudadBuscada == null)
                {
                    btnAlta.Enabled = true;
                    txtNombreCiudad.Enabled = false;
                    cbDepartamentos.Enabled = false;

                    _unaCiudad = new Ciudad();
                    _unaCiudad.CodDepto = CodigoDepto(cbDepartamentos.SelectedItem.ToString());
                    _unaCiudad.Nombre = txtNombreCiudad.Text;

                    lblError.Text = "No se encontro ninguna ciudad con el nombre '" + txtNombreCiudad.Text + "', puede agregarla si lo desea.";
                }
                else
                {
                    btnBaja.Enabled = true;
                    _unaCiudad = _CiudadBuscada;

                    txtNombreCiudad.Enabled = false;
                    cbDepartamentos.Enabled = false;

                    lblError.Text = "Ya existe la ciudad,  si deseea puede eliminarla";
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

        private void dgvCiudades_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCiudades.SelectedRows.Count > 0)
            {
                lblError.Text = "Puede eliminar la ciudad si lo desea.";

                txtNombreCiudad.Text = dgvCiudades.SelectedRows[0].Cells[0].Value.ToString();

                _unaCiudad = new Ciudad();
                _unaCiudad.Nombre = txtNombreCiudad.Text;

                _unaCiudad.CodDepto = CodigoDepto(cbDepartamentos.SelectedItem.ToString());

                txtNombreCiudad.Enabled = false;
                cbDepartamentos.Enabled = false;

                btnBaja.Enabled = true;
            }
        }

        private void CargarGV(string pCodDepto)
        {

            dgvCiudades.Columns.Clear();

            dgvCiudades.ColumnCount = 1;
            dgvCiudades.Columns[0].Name = "Nombre";

            List<Ciudad> _ciudadesBuscadas = new List<Ciudad>(new ServicioObligatorio.ServicioObligatorio().ListarCiudad(pCodDepto));

            foreach (Ciudad c in _ciudadesBuscadas)
            {
                dgvCiudades.Rows.Add(c.Nombre);
            }

            lblError.Text = "";

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            lblError.Text = "";
        }

        private void Limpiar()
        {
            txtNombreCiudad.Text = "";
            txtNombreCiudad.Enabled = true;
            cbDepartamentos.Enabled = true;

            btnAlta.Enabled = false;
            btnBaja.Enabled = false;

            cbDepartamentos.SelectedIndex = 0;

            _unaCiudad = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        private string CodigoDepto(string pNombreDepto)
        {
            switch (pNombreDepto)
            {
                case "Canelones":
                    return "A";
                case "Maldonado":
                    return "B";
                case "Rocha":
                    return "C";
                case "Treinta y Tres":
                    return "D";
                case "Cerro Largo":
                    return "E";
                case "Rivera":
                    return "F";
                case "Artigas":
                    return "G";
                case "Salto":
                    return "H";
                case "Paysandú":
                    return "I";
                case "Río Negro":
                    return "J";
                case "Soriano":
                    return "K";
                case "Colonia":
                    return "L";
                case "San José":
                    return "M";
                case "Flores":
                    return "N";
                case "Florida":
                    return "O";
                case "Lavalleja":
                    return "P";
                case "Durazno":
                    return "Q";
                case "Tacuarembó":
                    return "R";
                case "Montevideo":
                    return "S";
                default:
                    return "X";
            }
        }

        #endregion

    }
}
