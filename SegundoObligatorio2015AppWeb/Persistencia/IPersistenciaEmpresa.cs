using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaEmpresa
    {
        void Agregar(Empresa empresa);
        void Eliminar(Empresa empresa);
        void Modificar(Empresa empresa);
        Empresa Buscar(string rut);
        void NuevaVisita(Empresa empresa);
        List<Empresa> Listar();
        List<Empresa> ListarXCiudadYCategoria(Ciudad pCiudad, Categoria pCategoria);
    }
}
