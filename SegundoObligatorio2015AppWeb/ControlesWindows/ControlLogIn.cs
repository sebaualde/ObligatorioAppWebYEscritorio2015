using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ControlesWindows.ServicioObligatorio;
namespace ControlesWindows
{
    public partial class ControlLogIn : ContainerControl
    {
        private TextBox _txtUsuario;
        private Label _lblUsuario;
        private TextBox _txtContrasenia;
        private Label _lblContrasenia;
        private Button _btnLogueo;

        public ControlLogIn()
        {
            _txtUsuario = new TextBox();
            _txtContrasenia = new TextBox();
            _lblUsuario = new Label();
            _lblContrasenia = new Label();
            _btnLogueo = new Button();

            _txtUsuario = new TextBox();
            _txtUsuario.Location = new System.Drawing.Point(78, 19);
            _txtUsuario.Name = "txtUsuario";
            _lblUsuario.Text = "Hola";
            Controls.Add(_txtUsuario);

            _lblUsuario = new Label();
            _lblUsuario.Location = new System.Drawing.Point(4, 22);
            _lblUsuario.Name = "lblUsuario";
            _lblUsuario.Text = "Usuario: ";
            Controls.Add(_lblUsuario);

            _txtContrasenia = new TextBox();
            _txtContrasenia.Location = new System.Drawing.Point(78, 46);
            _txtContrasenia.Name = "txtContrasenia";
            _txtContrasenia.PasswordChar = Convert.ToChar("●");
            Controls.Add(_txtContrasenia);

            _lblContrasenia = new Label();
            _lblContrasenia.Location = new System.Drawing.Point(4, 50);
            _lblContrasenia.Name = "lblContrasenia";
            _lblContrasenia.Text = "Contraseña: ";
            Controls.Add(_lblContrasenia);
            
            _btnLogueo = new Button();
            _btnLogueo.Location = new System.Drawing.Point(104, 76);
            _btnLogueo.Name = "btnLogueo";
            _btnLogueo.Text = "Loguearse";
            Controls.Add(_btnLogueo);
            _btnLogueo.Click += new EventHandler(_btnLogueo_Click);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public string NombreUsuario
        {
            get { return _txtUsuario.Text.Trim(); }
        }

        public string Contraseña
        {
            get { return _txtContrasenia.Text.Trim(); }
        }

        public event EventHandler AutenticarUsuario;

        private void _btnLogueo_Click(object sender, EventArgs e)
        {
            AutenticarUsuario(this, new EventArgs());
        }
    }
}
