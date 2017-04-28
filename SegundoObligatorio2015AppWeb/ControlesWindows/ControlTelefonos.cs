using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ControlesWindows.ServicioObligatorio;

namespace ControlesWindows
{
    public partial class ControlTelefonos : UserControl
    {
        private List<string> telefonos = new List<string>();

        public List<string> Telefonos
        {
            get { return telefonos; }
            set
            {
                telefonos = value;
                ActualizarNumeros();
            }
        }

        public ControlTelefonos()
        {
            InitializeComponent();
            ActualizarNumeros();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string numero = txtNumero.Text.Trim();

                foreach (string tel in telefonos)
                {
                    if (tel == numero)
                    {
                        throw new Exception("Ya ingresaste este número");
                    }
                }

                try
                {
                    Convert.ToInt64(numero);
                }
                catch
                {
                    throw new Exception("Ingrese un número válido");
                }

                if (Convert.ToInt64(numero) < 0)
                {
                    throw new Exception("Ingrese un número válido");
                }

                Telefono telefono = new Telefono();
                telefono.Numero = numero;

                Telefonos.Add(telefono.Numero);

                ActualizarNumeros();

                txtNumero.Text = "";
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
                if (listBoxTelefonos.SelectedIndex != -1)
                {
                    int indice = listBoxTelefonos.SelectedIndex;

                    telefonos.RemoveAt(indice);

                    ActualizarNumeros();
                }
                else
                {
                    throw new Exception("Debe seleccionar un número de la lista");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void ActualizarNumeros()
        {
            listBoxTelefonos.DataSource = null;
            listBoxTelefonos.Items.Clear();
            lblMensaje.Text = "";
            listBoxTelefonos.DataSource = telefonos;
        }

        public void Limpiar()
        {
            listBoxTelefonos.DataSource = null;
            listBoxTelefonos.Items.Clear();
            lblMensaje.Text = "";
            telefonos = new List<string>();
            txtNumero.Text = "";
        }
    }
}