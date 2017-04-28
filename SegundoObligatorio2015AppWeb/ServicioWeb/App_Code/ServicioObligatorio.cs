using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using EntidadesCompartidas;
using Logica;
using System.Xml;
using System.Web.Services.Protocols;

/// <summary>
/// Descripción breve de MiServicio
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class ServicioObligatorio : System.Web.Services.WebService 
{

   public ServicioObligatorio() { }

        #region OperacionDeErroresParaSoap

        private void GeneroSoapException(Exception ex)
        {
            XmlDocument _undoc = new System.Xml.XmlDocument();
            XmlNode _NodoError = _undoc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);

            XmlNode _NodoDetalle = _undoc.CreateNode(XmlNodeType.Element, "Error", "");
            _NodoDetalle.InnerText = ex.Message;
            _NodoError.AppendChild(_NodoDetalle);

            SoapException _MiEx = new SoapException("Error WS", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, _NodoError);
            throw _MiEx;
        }

        #endregion

        #region LogicaCategoria
            [WebMethod]
            public void AltaCategoria(Categoria pCategoria)
            {
                try
                {
                    FabricaLogica.GetLogicaCategoria().AltaCategoria(pCategoria);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public void BajaCategoria(Categoria pCategoria)
            {
                try
                {
                    FabricaLogica.GetLogicaCategoria().BajaCategoria(pCategoria);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public void ModificarCategoria(Categoria pCategoria)
            {
                try
                {
                    FabricaLogica.GetLogicaCategoria().ModificarCategoria(pCategoria);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public Categoria BuscarCategoria(string pId)
            {
                Categoria _categ = null;
                try
                {
                    _categ = FabricaLogica.GetLogicaCategoria().BuscarCategoria(pId);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }

                return _categ;
            }

            [WebMethod]
            public List<Categoria> ListarCategoria()
            {
                List<Categoria> _Categorias = null;
                try
                {
                    _Categorias = FabricaLogica.GetLogicaCategoria().ListarCategorias();
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }

                return _Categorias;
            }

        #endregion

        #region LogicaCiudad

            [WebMethod]
            public void AltaCiudad(Ciudad pCiudad)
            {
                try
                {
                    FabricaLogica.GetLogicaCiudad().AltaCiudad(pCiudad);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public void BajaCiudad(Ciudad pCiudad)
            {
                try
                {
                    FabricaLogica.GetLogicaCiudad().BajaCiudad(pCiudad);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public Ciudad BuscarCiudad(string pCodDepto, string pNombre)
            {
                Ciudad _unaCiudad = null;
                try
                {
                    _unaCiudad = FabricaLogica.GetLogicaCiudad().BuscarCiudad(pCodDepto, pNombre);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }

                return _unaCiudad;
            }

            [WebMethod]
            public List<Ciudad> ListarCiudad(string pDepartamento)
            {
                List<Ciudad> _Ciudades = null;
                try
                {
                    _Ciudades = FabricaLogica.GetLogicaCiudad().ListarCiudades(pDepartamento);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }

                return _Ciudades;
            }

        #endregion

        #region LogicaUsuario

            [WebMethod]
            public Usuario LogueoUsuario(string pNombreUsuario, string pContrasenia)
            {
                Usuario _unUsuario = null;
                try
                {
                   _unUsuario = FabricaLogica.GetLogicaUsuario().LogueoUsuario(pNombreUsuario, pContrasenia);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }

                return _unUsuario;
            }

            [WebMethod]
            public void AltaUsuario(Usuario pUsuario)
            {
                try
                {
                    FabricaLogica.GetLogicaUsuario().AltaUsuario(pUsuario);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public void BajaUsuario(Usuario pUsuario)
            {
                try
                {
                    FabricaLogica.GetLogicaUsuario().BajaUsuario(pUsuario);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public void ModificarUsuario(Usuario pUsuario)
            {
                try
                {
                    FabricaLogica.GetLogicaUsuario().ModificarUsuario(pUsuario);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public Usuario BuscarUsuario(int pCi)
            {
                Usuario _unUsuario = null;
                try
                {
                    _unUsuario = FabricaLogica.GetLogicaUsuario().BuscarUsuario(pCi);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }

                return _unUsuario;
            }
        #endregion

        #region LogicaEmpresa

            [WebMethod]
            public void AltaEmpresa(Empresa pEmpresa)
            {
                try
                {
                    FabricaLogica.GetLogicaEmpresa().Agregar(pEmpresa);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public void BajaEmpresa(Empresa pEmpresa)
            {
                try
                {
                    FabricaLogica.GetLogicaEmpresa().Eliminar(pEmpresa);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public void ModificarEmpresa(Empresa pEmpresa)
            {
                try
                {
                    FabricaLogica.GetLogicaEmpresa().Modificar(pEmpresa);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public Empresa BuscarEmpresa(string pRut)
            {
                Empresa _unaEmpresa = null;

                try
                {
                    _unaEmpresa = FabricaLogica.GetLogicaEmpresa().Buscar(pRut);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }

                return _unaEmpresa;
            }

            [WebMethod]
            public List<Empresa> ListarEmpresa()
            {
                List<Empresa> _ListaEmpresas = null;

                try
                {
                    _ListaEmpresas = FabricaLogica.GetLogicaEmpresa().Listar();
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }

                return _ListaEmpresas;
            }

            [WebMethod]
            public List<Empresa> ListarEmpresasXCiudadYCategoria(Ciudad pCiudad, Categoria pCategoria)
            {
                List<Empresa> _ListaEmpresas = null;

                try
                {
                    _ListaEmpresas = FabricaLogica.GetLogicaEmpresa().ListarXCiudadYCategoria(pCiudad, pCategoria);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }

                return _ListaEmpresas;
            }


        #endregion

        #region LogicaVisita

            [WebMethod]
            public void NuevaVisita(Empresa pEmpresa)
            {
                try
                {
                    FabricaLogica.GetLogicaEmpresa().NuevaVisita(pEmpresa);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }
            }

            [WebMethod]
            public XmlDocument ListadoVisitasXml(Cliente pCliente)
            {
                XmlDocument _Documento = null;

                try
                {
                    _Documento = FabricaLogica.GetLogicaEmpresa().ListadoVisitasXml(pCliente);
                }
                catch (Exception ex)
                {
                    this.GeneroSoapException(ex);
                }

                return _Documento;
            }

        #endregion

        #region SolucionSer

            //necesario para q serialize los dos tipos de cuentas hacia el WS
            [WebMethod]
            public void ParaPoderSerializar(Administrador unAdmin, Cliente unCliente) { }

            //necesario para q serialize el tipo de cuenta no usado directamente desde el WS
            [WebMethod]
            public Administrador ParaPoderSerializarAdmin()
            {
                return new Administrador();
            }

            #endregion
      
    }
