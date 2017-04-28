using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaUsuario
    {
        Usuario LogueoUsuario(string nombreUsuario, string contrasenia);
        void AltaUsuario(Usuario usuario);
        void BajaUsuario(Usuario usuario);
        void ModificarUsuario(Usuario usuario);
        Usuario BuscarUsuario(int ci); 
    }
}
