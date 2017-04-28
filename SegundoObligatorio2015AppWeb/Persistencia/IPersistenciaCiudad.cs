using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaCiudad
    {
        void AltaCiudad(Ciudad pCiudad);
        void BajaCiudad(Ciudad pCiudad);
        Ciudad BuscarCiudad(string pCodDepto, string pNombre);
        List<Ciudad> ListarCiudades(string pDepartamento);
    }
}
