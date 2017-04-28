using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaCliente
    {
        Cliente LogueoCliente(string pNombreUsuario, string pContrasenia);
        Cliente BuscarCliente(int ci);
        void AltaCliente(Cliente pCliente);
    }
}
