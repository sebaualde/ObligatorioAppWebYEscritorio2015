using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdministracionBiosSearch
{
    public partial class FrmInicio : Form
    {
        public FrmInicio()
        {
            InitializeComponent();
        }

        private void cronos_Tick(object sender, EventArgs e)
        {

            cronos.Enabled = false;
            this.Hide();

            FrmLogueo _unForm = new FrmLogueo();
            _unForm.ShowDialog();

            this.Close();
        }


    }
}
