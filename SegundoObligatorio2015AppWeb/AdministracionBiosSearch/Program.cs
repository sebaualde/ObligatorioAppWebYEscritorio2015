using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using AdministracionBiosSearch.ServicioObligatorio;

namespace AdministracionBiosSearch
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
          
        [STAThread]
        static void Main()
        {
            //agrego un administrador para poder desarrollar y no tener que loguearnos
            //al final hay que cambiar en el application.run por el administrador del logueo
            Administrador _administradorDesarrollo = new Administrador();
            _administradorDesarrollo.CI = 12312312;
            _administradorDesarrollo.Nombre = "Nicolas Pedro";
            _administradorDesarrollo.NombreUsuario = "hola";
            _administradorDesarrollo.Contrasenia = "12345";
            _administradorDesarrollo.VeListado = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmInicio());
            Application.Run(new FrmPrincipal(_administradorDesarrollo));
        }
    }
}
