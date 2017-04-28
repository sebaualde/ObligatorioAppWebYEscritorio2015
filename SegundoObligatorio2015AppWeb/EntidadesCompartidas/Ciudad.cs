using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public class Ciudad
    {
        private string _codDepto;   
        private string _nombre; 

        public string CodDepto
        {
            get { return _codDepto; }
            set 
            {
                if (value == "X")
                    throw new Exception("Seleccione el departamento de la ciudad que desea buscar.");

                if (!(char.IsLetter(value[0])) || value.Length != 1)
                    throw new Exception("El identificador debe ser una letra.");

                string _letrasPosibles = "abcdefghijklmnopqrs";

                if (_letrasPosibles.Contains(value.ToLower()))
                     _codDepto = value.Trim().ToUpper();
                else
                    throw new Exception("El identificador debe contener una letra de la A a la S.");
                    
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set 
            {
                if (value.Length > 20 || value.Trim() == string.Empty)
                    throw new Exception("El Nombre no puede quedar vacio ni tener mas de 20 caracteres.");

                _nombre = value.Trim().ToUpper(); 
            }
        }

        public Ciudad(string pCodDepto, string pNombre)
        {
            CodDepto = pCodDepto;
            Nombre = pNombre;
        }

        public Ciudad()
        {
        }
    }
}
