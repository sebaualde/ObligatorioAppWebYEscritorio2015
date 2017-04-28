using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public class Categoria
    {
        private string _identificador;      
        private string _nombre;       
        private string _descripcion;   

        public string Identificador
        {
            get { return _identificador; }
            set 
            {
                if (value.Length != 3 || value.Trim() == string.Empty)
                    throw new Exception("El identificador de la categoria debe tener 3 letras.");

                if (!(char.IsLetter(value[0])) || !(char.IsLetter(value[1])) || !(char.IsLetter(value[2])))
                    throw new Exception("El identificador solo puede contener letras.");
                
                _identificador = value.Trim().ToUpper(); 
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set 
            {
                if (value.Length > 20 || value.Trim() == string.Empty)
                    throw new Exception("El nombre de la categoria no puede quedar vacio ni tener más de 20 caracteres.");

                _nombre = value.Trim().ToUpper(); 
            }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set 
            {
                if (value.Length > 50 || value.Trim() == string.Empty)
                    throw new Exception("La descripcion no puede quedar vacio ni tener más de 50 caracteres.");

                _descripcion = value.Trim(); 
            }
        }

        public Categoria(string pIdentificador, string pNombre, string pDescripcion)
        {
            Identificador = pIdentificador;
            Nombre = pNombre;
            Descripcion = pDescripcion;
        }

        public Categoria()
        {
            Identificador = "nnn";
            Nombre = "n/d";
            Descripcion = "n/d";
        }
    }
}
