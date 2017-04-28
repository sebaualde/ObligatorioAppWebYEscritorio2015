using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public class Telefono
    {
        string _numero;

        public string Numero
        {
            get { return _numero; }
            set
            {
                if (value.Trim().Length > 10)
                {
                    throw new Exception("El teléfono no puede tener más de 10 dígitos");
                }

                if (value.Trim().Length == 0)
                {
                    throw new Exception("El teléfono no puede estar vacío");
                }

                try
                {
                    Convert.ToInt64(value);
                }
                catch
                {
                    throw new Exception("El teléfono sólo puede contener números (sin puntos ni guiones)");
                }

                if(Convert.ToInt64(value)<0)
                {
                    throw new Exception("El teléfono no puede ser un número negativo");
                }

                _numero = value;
            }
        }

        public Telefono(string numero)
        {
            Numero = numero;
        }

        public Telefono()
        {
        }
    }
}
