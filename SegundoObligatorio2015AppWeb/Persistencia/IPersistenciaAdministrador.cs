using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
   public interface IPersistenciaAdministrador
    {
        Administrador LogueoAdmin(string pNombreUsuario, string pContrasenia);
        Administrador BuscarAdministrador(int ci);
        void AltaAdministrador(Administrador pAdministrador);
        void BajaAdministrador(Administrador pAdministrador);
        void ModificarAdministrador(Administrador pAdministrador);
    }
}
