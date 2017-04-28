using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public class Cliente : Usuario
    {
        private int _edad;

        public int Edad
        {
            get { return _edad; }
            set
            {
                if (value < 1)
                    throw new Exception("La edad no puede ser menor de 1 año.");

                _edad = value;
            }
        }

        public Cliente(Int32 pCI, string pNombre, string pNombreUsuario, string pContrasenia, int pEdad)
            : base(pCI, pNombre, pNombreUsuario, pContrasenia)
        {
            Edad = pEdad;
        }

        public Cliente()
        {
        }
    }
}
