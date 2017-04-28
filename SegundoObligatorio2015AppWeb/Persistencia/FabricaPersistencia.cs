using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia
{
    public class FabricaPersistencia
    {
        public static IPersistenciaCiudad GetPersistenciaCiudad()
        {
            return (PersistenciaCiudad.GetInstancia());
        }

        public static IPersistenciaCategoria GetPersistenciaCategoria()
        {
            return (PersistenciaCategoria.GetInstancia());
        }

        public static IPersistenciaCliente GetPersistenciaCliente()
        {
            return (PersistenciaCliente.GetInstancia());
        }

        public static IPersistenciaAdministrador GetPersistenciaAdministrador()
        {
            return (PersistenciaAdministrador.GetInstancia());
        }

        public static IPersistenciaEmpresa GetPersistenciaEmpresa()
        {
            return (PersistenciaEmpresa.GetInstancia());
        }
    }
}
