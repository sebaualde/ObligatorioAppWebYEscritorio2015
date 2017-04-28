using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;
using System.Xml;

namespace Logica
{
    internal class LogicaEmpresa : ILogicaEmpresa
    {
        private static LogicaEmpresa _instancia = null;
        private LogicaEmpresa() { }
        public static LogicaEmpresa GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaEmpresa();
            }

            return _instancia;
        }

        public void Agregar(Empresa empresa)
        {
            FabricaPersistencia.GetPersistenciaEmpresa().Agregar(empresa);
        }

        public void Eliminar(Empresa empresa)
        {
            FabricaPersistencia.GetPersistenciaEmpresa().Eliminar(empresa);
        }

        public void Modificar(Empresa empresa)
        {
            FabricaPersistencia.GetPersistenciaEmpresa().Modificar(empresa);
        }

        public Empresa Buscar(string rut)
        {
            return FabricaPersistencia.GetPersistenciaEmpresa().Buscar(rut);
        }
        
        public void NuevaVisita(Empresa empresa)
        {
            FabricaPersistencia.GetPersistenciaEmpresa().NuevaVisita(empresa);
        }

        public List<Empresa> Listar()
        {
            return FabricaPersistencia.GetPersistenciaEmpresa().Listar();
        }

        public XmlDocument ListadoVisitasXml(Cliente pCliente)
        {
            XmlDocument _Documento = null;

            List<Empresa> _empresas = FabricaPersistencia.GetPersistenciaEmpresa().Listar();

            _Documento = new XmlDocument();
            _Documento.LoadXml("<?xml version='1.0' encoding='utf-8' ?> <Raiz> </Raiz>");
            XmlNode _raiz = _Documento.DocumentElement;

            foreach (Empresa e in _empresas)
            {
                foreach (Visita v in e.Visitas)
                {
                    if (v.Cliente.CI == pCliente.CI)
                    {
                        XmlNode _Nodo = _Documento.CreateElement("Visita");

                        XmlNode _Fecha = _Documento.CreateElement("Fecha");
                        _Fecha.InnerText = v.FechaYHora.ToShortDateString();
                        _Nodo.AppendChild(_Fecha);

                        XmlNode _NomEmpresa = _Documento.CreateElement("NomEmpresa");
                        _NomEmpresa.InnerText = e.Nombre.ToString();
                        _Nodo.AppendChild(_NomEmpresa);

                        _raiz.AppendChild(_Nodo);
                    }
                }
            }

            return _Documento;
        }

        public List<Empresa> ListarXCiudadYCategoria(Ciudad pCiudad, Categoria pCategoria)
        {
            return FabricaPersistencia.GetPersistenciaEmpresa().ListarXCiudadYCategoria(pCiudad, pCategoria);
        }
    }
}
