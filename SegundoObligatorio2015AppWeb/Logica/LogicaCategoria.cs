using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaCategoria : ILogicaCategoria
    {
        private static LogicaCategoria _instancia = null;
        private LogicaCategoria() { }
        public static LogicaCategoria GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaCategoria();

            return _instancia;
        }

        public void AltaCategoria(Categoria pCategoria)
        {
            IPersistenciaCategoria FCategoria = FabricaPersistencia.GetPersistenciaCategoria();
            FCategoria.AltaCategoria(pCategoria);        
        }

        public void BajaCategoria(Categoria pCategoria)
        {
            IPersistenciaCategoria FCategoria = FabricaPersistencia.GetPersistenciaCategoria();
            FCategoria.BajaCategoria(pCategoria);
        }

        public void ModificarCategoria(Categoria pCategoria)
        {
            IPersistenciaCategoria FCategoria = FabricaPersistencia.GetPersistenciaCategoria();
            FCategoria.ModificarCategoria(pCategoria);
        }

        public Categoria BuscarCategoria(string pId)
        {
            IPersistenciaCategoria FCategoria = FabricaPersistencia.GetPersistenciaCategoria();
            return (FCategoria.BuscarCategoria(pId));
        }

        public List<Categoria> ListarCategorias()
        {
            IPersistenciaCategoria FCategoria = FabricaPersistencia.GetPersistenciaCategoria();
            return (FCategoria.ListarCategorias());
        }
    }
}
