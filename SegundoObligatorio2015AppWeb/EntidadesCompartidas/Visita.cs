using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public class Visita
    {
        private Cliente _cliente;
        private DateTime _fechayHora;
        private Boolean _visitaAceptada;

        public Cliente Cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("El cliente no puede ser nulo");
                }

                _cliente = value;
            }
        }

        public string NombreCliente
        {
            get
            {
                return _cliente.Nombre;
            }

            set { }
        }

        public DateTime FechaYHora
        {
            get
            {
                return _fechayHora;
            }
            set { }
        }

        public Boolean VisitaAceptada
        {
            get { return _visitaAceptada; }
            set { _visitaAceptada = value; }
        }

        public Visita(Cliente cliente, DateTime fecha, Boolean pVisitaAceptada)
        {
            Cliente = cliente;
            _fechayHora = fecha;
            VisitaAceptada = pVisitaAceptada;
        }

        public Visita()
        {
        }
    }
}
