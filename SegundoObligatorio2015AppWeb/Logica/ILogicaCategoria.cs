using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaCategoria
    {
        void AltaCategoria(Categoria pCategoria);
        void BajaCategoria(Categoria pCategoria);
        void ModificarCategoria(Categoria pCategoria);
        Categoria BuscarCategoria(string pId);
        List<Categoria> ListarCategorias();
    }
}
