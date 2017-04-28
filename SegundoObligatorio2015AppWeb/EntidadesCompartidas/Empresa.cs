using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public class Empresa
    {
        private string _rut;
        private string _nombre;
        private string _direccion;
        private List<Telefono> _telefonos;
        private Ciudad _ciudad;
        private Categoria _categoria;
        private List<Visita> _visitas;

        //atributos para mostrar en aplicacion web los datos de la visita en data grid view en el FrmAutorizacionDeVisitas
        private string _fechaUltimaVisita;
        private string _NombreUltimaVisita;
        
        public string Rut
        {
            get { return _rut; }
            set
            {
                if (((string)value).Length != 12)
                {
                    throw new Exception("El Rut debe tener 12 caracteres");
                }
                try
                {
                    Convert.ToInt64(value);
                }
                catch
                {
                    throw new Exception("Rut Inválido: Sólo se aceptan números");
                }

                if (Convert.ToInt64(value) < 0)
                {
                    throw new Exception("El RUT no puede ser un número negativo");
                }

                _rut = value;
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("El nombre no puede estar vacío");
                }

                if (value.Length > 25)
                {
                    throw new Exception("El nombre no puede superar los 25 caracteres");
                }

                _nombre = value;
            }
        }

        public string Direccion
        {
            get { return _direccion; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("La dirección no puede quedar en blanco");
                }

                if (value.Length > 50)
                {
                    throw new Exception("La dirección no puede tener más de 50 caracteres");
                }

                    _direccion = value;
            }
        }

        public List<Telefono> Telefonos
        {
            get { return _telefonos; }
            set { _telefonos = value; }
        }

        public Ciudad Ciudad
        {
            get { return _ciudad; }
            set
            {
                if (value == null)
                {
                    throw new Exception("Ingrese un ciudad válida");
                }

                _ciudad = value;
            }
        }

        public Categoria Categoria
        {
            get { return _categoria;}
            set
            {
                if (value == null)
                {
                    throw new Exception("Ingrese una categoría válida");
                }
                _categoria = value;
            }
        }

        public List<Visita> Visitas
        {
            get { return _visitas; }
            set
            {
                if (value == null)
                {
                    throw new Exception("La lista de visitas es nula");
                }
                _visitas = value;
            }
        }

        public int CantidadVisitas
        {
            get
            {
                return _visitas.Count;
            }
            set { }
        }
        
        public string FechaUltimaVisita
        {
            get { return _fechaUltimaVisita; }
            set { _fechaUltimaVisita = value; }
        }

        public string NombreUltimaVisita
        {
            get { return _NombreUltimaVisita; }
            set { _NombreUltimaVisita = value; }
        }

        public Empresa(string rut, string nombre, string direccion, List<Telefono> telefonos, Ciudad ciudad, Categoria categoria, List<Visita> visitas)
        {
            Rut = rut;
            Nombre = nombre;
            Direccion = direccion;
            Telefonos = telefonos;
            Ciudad = ciudad;
            Categoria = categoria;
            Visitas = visitas;
        }

        public Empresa()
        {
        }
    }
}
