using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class FabricaLogica
    {
        public static ILogicaCategoria GetLogicaCategoria()
        {
            return (LogicaCategoria.GetInstancia());
        }

        public static ILogicaCiudad GetLogicaCiudad()
        {
            return (LogicaCiudad.GetInstancia());
        }

        public static ILogicaEmpresa GetLogicaEmpresa()
        {
            return (LogicaEmpresa.GetInstancia());
        }

        public static ILogicaUsuario GetLogicaUsuario()
        {
            return (LogicaUsuario.GetInstancia());
        }
    }
}
