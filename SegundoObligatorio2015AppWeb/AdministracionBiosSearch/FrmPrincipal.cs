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
    public partial class FrmPrincipal : Form
    {
        private Administrador _AdminLogueado;

        public FrmPrincipal(Administrador pAdmin)
        {
            InitializeComponent();
            _AdminLogueado = pAdmin;
        }

        private void aBMEmpresasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEmpresas _unForm = new FrmEmpresas();
            _unForm.ShowDialog();
        }

        private void aBMAdministrativosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmABMAdministradores _unForm = new FrmABMAdministradores(_AdminLogueado);
            _unForm.ShowDialog();
        }

        private void aBLCiudadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmABLCiudades _unForm = new FrmABLCiudades();
            _unForm.ShowDialog();
        }

        private void aBMLCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmABMLCategoria _unForm = new FrmABMLCategoria();
            _unForm.ShowDialog();
        }

        private void autorizacionDeVisitasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAutorizacionDeVisitas _unForm = new FrmAutorizacionDeVisitas();
            _unForm.ShowDialog();
        }

        private void listadoGeneralDeEmpresasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_AdminLogueado.VeListado)
            {
                DialogResult resultado = MessageBox.Show("No tienes permiso para ver listados", "No permitido", MessageBoxButtons.OK);
            }
            else
            {
                FrmListadoGeneralEmpresas _unForm = new FrmListadoGeneralEmpresas(_AdminLogueado);
                _unForm.ShowDialog();
            }
        }

    }
}
