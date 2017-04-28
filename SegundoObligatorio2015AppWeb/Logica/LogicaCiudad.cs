using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaCiudad : ILogicaCiudad
    {
        private static LogicaCiudad _instancia = null;
        private LogicaCiudad() { }
        public static LogicaCiudad GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaCiudad();

            return _instancia;
        }

        public void AltaCiudad(Ciudad pCiudad)
        {
            IPersistenciaCiudad FCiudad = FabricaPersistencia.GetPersistenciaCiudad();
            FCiudad.AltaCiudad(pCiudad);
        }

        public void BajaCiudad(Ciudad pCiudad)
        {
            IPersistenciaCiudad FCiudad = FabricaPersistencia.GetPersistenciaCiudad();
            FCiudad.BajaCiudad(pCiudad);
        }

        public Ciudad BuscarCiudad(string pCodDepto, string pNombre)
        {
            IPersistenciaCiudad FCiudad = FabricaPersistencia.GetPersistenciaCiudad();
            return (FCiudad.BuscarCiudad(pCodDepto, pNombre));
        }

        public List<Ciudad> ListarCiudades(string pDepartamento)
        {
            IPersistenciaCiudad FCiudad = FabricaPersistencia.GetPersistenciaCiudad();
            return (FCiudad.ListarCiudades(pDepartamento));
        }
    }
}
