using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public abstract class Usuario
    {
        private int _ci;
        private string _nombre;
        private string _nombreUsuario;
        private string _contrasenia;

        public Int32 CI
        {
            get { return _ci; }
            set
            {
                if (value.ToString().Length != 8)
                    throw new Exception("La cedula debe tener 8 caracteres");

                if (value < 0)
                    throw new Exception("La cedula no puede ser negativa");
                _ci = value;
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (value.Length > 30 || value.Trim() == string.Empty)
                    throw new Exception("El Nombre no puede quedar vacio ni tener mas de 30 caracteres.");

                _nombre = value.Trim();
            }
        }

        public string NombreUsuario
        {
            get { return _nombreUsuario; }
            set
            {
                if (value.Length > 30 || value.Trim() == string.Empty)
                    throw new Exception("El Nombre de usuario no puede quedar vacio ni tener mas de 30 caracteres.");

                _nombreUsuario = value.Trim();
            }
        }

        public string Contrasenia
        {
            get { return _contrasenia; }
            set
            {
                if (value.Length != 5)
                    throw new Exception("La contrasenia debe ser de 5 caracteres");

                _contrasenia = value.Trim();
            }
        }

        public Usuario(int pCI, string pNombre, string pNombreUsuario, string pContrasenia)
        {
            CI = pCI;
            Nombre = pNombre;
            NombreUsuario = pNombreUsuario;
            Contrasenia = pContrasenia;
        }

        public Usuario()
        {
        }
    }
}
