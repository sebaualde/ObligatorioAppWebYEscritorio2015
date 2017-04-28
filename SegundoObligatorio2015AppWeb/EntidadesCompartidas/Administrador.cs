using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]

    public class Administrador : Usuario
    {
        private bool _veListado;

        public bool VeListado
        {
            get { return _veListado; }
            set
            {
                _veListado = value;
            }
        }

        public Administrador(int pCI, string pNombre, string pNombreUsuario, string pContrasenia, bool pVeListado)
            : base(pCI, pNombre, pNombreUsuario, pContrasenia)
        {
            VeListado = pVeListado;
        }

        public Administrador()
        {
        }
    }
}
