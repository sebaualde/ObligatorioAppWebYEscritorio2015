using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Xml;

namespace Logica
{
    public interface ILogicaEmpresa
    {
        void Agregar(Empresa empresa);
        void Eliminar(Empresa empresa);
        void Modificar(Empresa empresa);
        Empresa Buscar(string rut);
        void NuevaVisita(Empresa empresa);
        List<Empresa> Listar();
        XmlDocument ListadoVisitasXml(Cliente pCliente);
        List<Empresa> ListarXCiudadYCategoria(Ciudad pCiudad, Categoria pCategoria);
    }
}
