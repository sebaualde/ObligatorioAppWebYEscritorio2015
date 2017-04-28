using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using AdministracionBiosSearch.ServicioObligatorio;

namespace AdministracionBiosSearch
{
    public partial class FrmListadoGeneralEmpresas : Form
    {
        private List<Empresa> _empresas;

        public FrmListadoGeneralEmpresas(Administrador admin)
        {
            InitializeComponent();

            _empresas = new ServicioObligatorio.ServicioObligatorio().ListarEmpresa().ToList();

            //Cargo las categorías
            ArrayList categorias = new ArrayList();
            var cualquierCategoria = new { Identificador = "0", Nombre = "CUALQUIERA" };
            categorias.Add(cualquierCategoria);

            foreach (Categoria C in new ServicioObligatorio.ServicioObligatorio().ListarCategoria())
            {
                var unaCategoria = new { Identificador = C.Identificador, Nombre = C.Nombre };
                categorias.Add(unaCategoria);
            }        

            ddlCategorias.DataSource = categorias;
            ddlCategorias.ValueMember = "Identificador";
            ddlCategorias.DisplayMember = "Nombre";

            Filtrar();
        }

        private void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Filtrar();
            }
            catch(Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void Filtrar()
        {
            string categoria = ddlCategorias.SelectedValue.ToString();
            int visitas;

            if (!string.IsNullOrEmpty(txtVisitas.Text.Trim()))
            {
                try
                {
                    visitas = Convert.ToInt32(txtVisitas.Text.Trim());

                    if (visitas < 0)
                    {
                        throw new Exception("La cantidad de visitas no puede ser negativa");
                    }
                }
                catch (FormatException)
                {
                    throw new Exception("Ingrese un número entero válido");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                visitas = -1;
            }


            if (visitas == -1 && ddlCategorias.SelectedValue.ToString() != "0") //filtro sólo por categoría
            {
                var resultado = from emp in _empresas
                                where emp.Categoria.Identificador == ddlCategorias.SelectedValue.ToString()
                                select emp;

                CargarGrilla(resultado.ToList());
            
            }

            if (visitas != -1 && ddlCategorias.SelectedValue.ToString() == "0") //FILTRO SÓLO POR CANTIDAD DE VISITAS
            {

                var resultado = from emp in _empresas
                                where emp.CantidadVisitas == visitas
                                select emp;

                CargarGrilla(resultado.ToList());
            }

            if (visitas != -1 && ddlCategorias.SelectedValue.ToString() != "0") //aplico ambos filtros a la vez
            {

                var resultado = from emp in _empresas
                                where emp.CantidadVisitas == visitas && emp.Categoria.Identificador== ddlCategorias.SelectedValue.ToString()
                                select emp;

                CargarGrilla(resultado.ToList());
            }

            if (visitas == -1 && ddlCategorias.SelectedValue.ToString() == "0") //quito el filtro cuando no hay valores en los campos
            {
                CargarGrilla(_empresas);
            }
        }

        private void CargarGrilla(List<Empresa> empresas)
        {
            ArrayList datos = new ArrayList();

            foreach (Empresa E in empresas)
            {
                var emp = new {Rut=E.Rut, Nombre=E.Nombre, Direccion= String.Format("{0}, {1}", E.Direccion, E.Ciudad.Nombre), Categoría = E.Categoria.Nombre, VIsitas = E.CantidadVisitas};
                datos.Add(emp);
            }

            grillaEmpresas.DataSource = datos;
        }

        private void txtVisitas_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Filtrar();
            }
            catch(Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnQuitalFiltros_Click(object sender, EventArgs e)
        {
            try
            {
                txtVisitas.Text = "";
                ddlCategorias.SelectedValue = "0";
                Filtrar();
            }
            catch(Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

    }
}