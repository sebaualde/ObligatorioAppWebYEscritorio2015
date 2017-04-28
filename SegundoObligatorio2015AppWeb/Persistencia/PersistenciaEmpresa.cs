using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using EntidadesCompartidas;

namespace Persistencia
{
    internal class PersistenciaEmpresa : IPersistenciaEmpresa
    {
        private static PersistenciaEmpresa _instancia = null;
        private PersistenciaEmpresa() { }
        public static PersistenciaEmpresa GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new PersistenciaEmpresa();
            }
            return _instancia; 
        }

        public void Agregar(Empresa empresa)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdAgregarEmpresa = new SqlCommand("AgregarEmpresa", conexion);
            cmdAgregarEmpresa.CommandType = CommandType.StoredProcedure;

            cmdAgregarEmpresa.Parameters.AddWithValue("@rut", empresa.Rut);
            cmdAgregarEmpresa.Parameters.AddWithValue("@nombre", empresa.Nombre);
            cmdAgregarEmpresa.Parameters.AddWithValue("@direccion", empresa.Direccion);
            cmdAgregarEmpresa.Parameters.AddWithValue("@categoria", empresa.Categoria.Identificador);
            cmdAgregarEmpresa.Parameters.AddWithValue("@departamento", empresa.Ciudad.CodDepto);
            cmdAgregarEmpresa.Parameters.AddWithValue("@nombreCiudad", empresa.Ciudad.Nombre);

            SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            cmdAgregarEmpresa.Parameters.Add(retorno);

            SqlTransaction transaccion = null;

            try
            {
                conexion.Open();

                transaccion = conexion.BeginTransaction();

                cmdAgregarEmpresa.Transaction = transaccion;

                int filasAfectadas = cmdAgregarEmpresa.ExecuteNonQuery();

                if (filasAfectadas < 1)
                {
                    switch ((int)retorno.Value)
                    {
                        case 1:
                            throw new Exception("Ya existe una empresa con rut " + empresa.Rut);
                        case 2:
                            throw new Exception("No existe una categoría con ID " + empresa.Categoria.Identificador);
                        case 3:
                            throw new Exception("La ciudad no es correcta");
                        default:
                            throw new Exception("Error no especificado");
                    }
                }

                //persisto los teléfonos de la empresa

                foreach (Telefono telefono in empresa.Telefonos)
                {
                    PersistenciaTelefono.Agregar(telefono, empresa, transaccion);
                }

                transaccion.Commit();
            }
            catch(Exception ex)
            {
                transaccion.Rollback();

                throw new Exception("Error de DB: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        public void Eliminar(Empresa empresa)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdEliminarEmpresa = new SqlCommand("EliminarEmpresa", conexion);
                cmdEliminarEmpresa.CommandType = CommandType.StoredProcedure;

                cmdEliminarEmpresa.Parameters.AddWithValue("@rut", empresa.Rut);

                SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;
                cmdEliminarEmpresa.Parameters.Add(retorno);

                conexion.Open();

                int filasAfectadas = cmdEliminarEmpresa.ExecuteNonQuery();

                if (filasAfectadas < 1)
                {
                    switch ((int)retorno.Value)
                    {
                        case 1:
                            throw new Exception("No se pudo borrar los teléfonos de la empresa");
                        case 2:
                            throw new Exception("No se pudo borrar las visitas de la empresa");
                        default:
                            throw new Exception("No se pudo eliminar la empresa");
                    }
                }

            }

            catch (Exception ex)
            {
                throw new Exception("Error de base de datos: " + ex.Message);
            }

            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public void Modificar(Empresa empresa)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdModificarEmpresa = new SqlCommand("ModificarEmpresa", conexion);
            cmdModificarEmpresa.CommandType = CommandType.StoredProcedure;
             
            cmdModificarEmpresa.Parameters.AddWithValue("@rut", empresa.Rut);
            cmdModificarEmpresa.Parameters.AddWithValue("@nombre", empresa.Nombre);
            cmdModificarEmpresa.Parameters.AddWithValue("@direccion", empresa.Direccion);
            cmdModificarEmpresa.Parameters.AddWithValue("@categoria", empresa.Categoria.Identificador);
            cmdModificarEmpresa.Parameters.AddWithValue("@departamento", empresa.Ciudad.CodDepto);
            cmdModificarEmpresa.Parameters.AddWithValue("@nombreCiudad", empresa.Ciudad.Nombre);

            SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;

            cmdModificarEmpresa.Parameters.Add(retorno);
            
            SqlTransaction transaccion = null;

            try
            {
                conexion.Open();

                transaccion = conexion.BeginTransaction();

                cmdModificarEmpresa.Transaction = transaccion;

                //modifico la empresa 

                int filasAfectadas = cmdModificarEmpresa.ExecuteNonQuery();

                if (filasAfectadas < 1)
                {
                    switch ((int)retorno.Value)
                    {
                        case 1:
                            throw new Exception("No existe una empresa con rut " + empresa.Rut);
                        case 2:
                            throw new Exception("No existe una categoría con ID " + empresa.Categoria.Identificador);
                        case 3:
                            throw new Exception("La ciudad no es correcta");
                        default:
                            throw new Exception("Error no especificado");
                    }
                }

                //Elimino la lista anterior de teléfonos de la empresa
                PersistenciaTelefono.EliminarTelefonosDeEmpresa(empresa.Rut, transaccion);

                //agrego la nueva lista de teléfonos
                foreach(Telefono telefono in empresa.Telefonos)
                {
                    PersistenciaTelefono.Agregar(telefono, empresa, transaccion);
                }

                transaccion.Commit();
            }

            catch(Exception ex)
            {
                transaccion.Rollback();

                throw new Exception("Error de base de datos: " + ex.Message);
            }

            finally
            {
                conexion.Close();
            }
        }

        public Empresa Buscar(string rut) 
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpresa = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdBuscarEmpresa = new SqlCommand("BuscarEmpresa", conexion);
                cmdBuscarEmpresa.CommandType = CommandType.StoredProcedure;

                cmdBuscarEmpresa.Parameters.AddWithValue("@rut", rut);

                conexion.Open();

                drEmpresa = cmdBuscarEmpresa.ExecuteReader();

                Empresa empresa = null;

                if (drEmpresa.HasRows)
                {
                    List<Visita> visitas = this.ListaVisitas(rut);
                    List<Telefono> tels= PersistenciaTelefono.TelefonosDeEmpresa(rut);
                    drEmpresa.Read();
                    Ciudad ciudad = PersistenciaCiudad.GetInstancia().BuscarCiudadSinFiltro((string)drEmpresa["Departamento"], (string)drEmpresa["NombreCiudad"]);
                    Categoria categoria = PersistenciaCategoria.GetInstancia().BuscarCategoriaSinFiltro((string)drEmpresa["Categoria"]);
                    empresa = new Empresa(rut, (string)drEmpresa["Nombre"], (string)drEmpresa["Direccion"],tels, ciudad, categoria, visitas);
                }

                return empresa;
            }

            catch(Exception ex)
            {
                throw new Exception("Error de DB: " + ex.Message);
            }

            finally
            {
                if (drEmpresa != null)
                {
                    drEmpresa.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }

        }

        internal List<Visita> ListaVisitas(string rutEmpresa)
        {
            SqlConnection conexion = null;
            SqlDataReader drVisitas = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdListarVisitas = new SqlCommand("VisitasDeEmpresa", conexion);
                cmdListarVisitas.CommandType = CommandType.StoredProcedure;

                cmdListarVisitas.Parameters.AddWithValue("@rut", rutEmpresa);

                conexion.Open();

                drVisitas = cmdListarVisitas.ExecuteReader();

                List<Visita> visitas = new List<Visita>();

                if (drVisitas.HasRows)
                {
                    while (drVisitas.Read())
                    {
                        Cliente cliente = PersistenciaCliente.GetInstancia().BuscarCliente(Convert.ToInt32(drVisitas["CICliente"]));
                        Visita visita = new Visita(cliente, (DateTime)drVisitas["FechaHora"], Convert.ToBoolean(drVisitas["VisitaAceptada"]));

                        visitas.Add(visita);
                    }
                }

                return visitas;
            }

            catch(Exception ex)
            {
                throw new Exception("Error de DB: " + ex.Message);
            }

            finally
            {
                if (drVisitas != null)
                {
                    drVisitas.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }

        }

        public void NuevaVisita(Empresa empresa)
        {
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdNuevaVisita = new SqlCommand("NuevaVisita", conexion);
                cmdNuevaVisita.CommandType = CommandType.StoredProcedure;

                cmdNuevaVisita.Parameters.AddWithValue("@ciCliente", empresa.Visitas.Last().Cliente.CI);
                cmdNuevaVisita.Parameters.AddWithValue("@rutEmpresa", empresa.Rut);
                cmdNuevaVisita.Parameters.AddWithValue("@VisitaAceptada", empresa.Visitas.Last().VisitaAceptada);

                SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;

                cmdNuevaVisita.Parameters.Add(retorno);
                
                conexion.Open();

                int filasAfectadas = cmdNuevaVisita.ExecuteNonQuery();

                if (filasAfectadas < 1)
                {
                    int valorRetorno = Convert.ToInt32(retorno.Value);

                    switch (valorRetorno)
                    {
                        case 1:
                            throw new Exception("Cliente no válido");
                        case 2:
                            throw new Exception("Empresa no válida");
                        default:
                            throw new Exception("Error no especificado al visitar la empresa");
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Error de base de datos: " + ex.Message);
            }

            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public List<Empresa> Listar()
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpresas = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdListarEmpresas = new SqlCommand("ListarEmpresas", conexion);
                cmdListarEmpresas.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                drEmpresas = cmdListarEmpresas.ExecuteReader();

                List<Empresa> empresas = new List<Empresa>();

                if (drEmpresas.HasRows)
                {
                    while (drEmpresas.Read())
                    {
                        Categoria categoria = PersistenciaCategoria.GetInstancia().BuscarCategoriaSinFiltro((string)drEmpresas["Categoria"]);
                        Ciudad ciudad = PersistenciaCiudad.GetInstancia().BuscarCiudadSinFiltro((string)drEmpresas["Departamento"], (string)drEmpresas["NombreCiudad"]);
                        Empresa emp = new Empresa((string)drEmpresas["Rut"], (string)drEmpresas["Nombre"], (string)drEmpresas["Direccion"], PersistenciaTelefono.TelefonosDeEmpresa((string)drEmpresas["Rut"]), ciudad, categoria, ListaVisitas((string)drEmpresas["Rut"]));

                        empresas.Add(emp);
                    }
                }

                return empresas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error de BD: " + ex.Message);
            }
            finally
            {
                if (drEmpresas != null)
                {
                    drEmpresas.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }

        }

        public List<Empresa> ListarXCiudadYCategoria(Ciudad pCiudad, Categoria pCategoria)
        {
            SqlConnection conexion = null;
            SqlDataReader drEmpresas = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdListarXCiudadYCategoria = new SqlCommand("ListarEmpresasXCiudadYCategoria", conexion);
                cmdListarXCiudadYCategoria.CommandType = CommandType.StoredProcedure;

                cmdListarXCiudadYCategoria.Parameters.AddWithValue("@ciudad", pCiudad.Nombre);
                cmdListarXCiudadYCategoria.Parameters.AddWithValue("@categoria", pCategoria.Identificador);

                conexion.Open();

                drEmpresas = cmdListarXCiudadYCategoria.ExecuteReader();

                List<Empresa> empresas = new List<Empresa>();

                if (drEmpresas.HasRows)
                {
                    while (drEmpresas.Read())
                    {
                        Empresa emp = new Empresa((string)drEmpresas["Rut"], (string)drEmpresas["Nombre"], (string)drEmpresas["Direccion"], PersistenciaTelefono.TelefonosDeEmpresa((string)drEmpresas["Rut"]), pCiudad, pCategoria, ListaVisitas((string)drEmpresas["Rut"]));

                        empresas.Add(emp);
                    }
                }

                return empresas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error de BD: " + ex.Message);
            }
            finally
            {
                if (drEmpresas != null)
                {
                    drEmpresas.Close();
                }
                if (conexion != null)
                {
                    conexion.Close();
                }
            }

        }
    }
}
