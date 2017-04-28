using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using EntidadesCompartidas;

namespace Persistencia
{
    internal class PersistenciaCategoria : IPersistenciaCategoria
    {
        private static PersistenciaCategoria _insatncia = null;
        private PersistenciaCategoria() { }
        public static PersistenciaCategoria GetInstancia()
        {
            if (_insatncia == null)
                _insatncia = new PersistenciaCategoria();

            return _insatncia;
        }

        public void AltaCategoria(Categoria pCategoria)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdAltaCategoria = new SqlCommand("AltaCategoria", _conexion);
            cmdAltaCategoria.CommandType = CommandType.StoredProcedure;

            cmdAltaCategoria.Parameters.AddWithValue("@id", pCategoria.Identificador);
            cmdAltaCategoria.Parameters.AddWithValue("@nombre", pCategoria.Nombre);
            cmdAltaCategoria.Parameters.AddWithValue("@descripcion", pCategoria.Descripcion);

            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdAltaCategoria.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();
                cmdAltaCategoria.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("Ya existe la categoria que intenta agregar.");
                else if((int)_valorRetorno.Value == -2)
                    throw new Exception("Ya existe una categoria con el mismo nombre en la base de datos.");
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conexion.Close();
            }

        }

        public void BajaCategoria(Categoria pCategoria)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdBajaCategoria = new SqlCommand("BajaCategoria", _conexion);
            cmdBajaCategoria.CommandType = CommandType.StoredProcedure;

            cmdBajaCategoria.Parameters.AddWithValue("@id", pCategoria.Identificador);

            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdBajaCategoria.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();
                cmdBajaCategoria.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("No existe la categoria.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conexion.Close();
            }
        }

        public void ModificarCategoria(Categoria pCategoria)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdModificarCategoria = new SqlCommand("ModificarCategoria", _conexion);
            cmdModificarCategoria.CommandType = CommandType.StoredProcedure;

            cmdModificarCategoria.Parameters.AddWithValue("@id", pCategoria.Identificador);
            cmdModificarCategoria.Parameters.AddWithValue("@nombre", pCategoria.Nombre);
            cmdModificarCategoria.Parameters.AddWithValue("@descripcion", pCategoria.Descripcion);

            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdModificarCategoria.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();
                cmdModificarCategoria.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("No existe la categoria que intenta modificar.");
                else if ((int)_valorRetorno.Value == -2)
                    throw new Exception("Ya existe ese nombre de categoria en la base de datos.");
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conexion.Close();
            }

        }

        //buscar categoria para abm
        public Categoria BuscarCategoria(string pId)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drCategoria;

            SqlCommand cmdBuscarCategoria = new SqlCommand("BuscarCategoria", _conexion);
            cmdBuscarCategoria.CommandType = CommandType.StoredProcedure;

            cmdBuscarCategoria.Parameters.AddWithValue("@id", pId);

            Categoria _categoria = null;

            try
            {
                _conexion.Open();
                drCategoria = cmdBuscarCategoria.ExecuteReader();

                if (drCategoria.HasRows)
                {
                    while(drCategoria.Read())                   
                        _categoria = new Categoria((string)drCategoria["Id"], (string)drCategoria["Nombre"], (string)drCategoria["Descripcion"]);
                }

                drCategoria.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conexion.Close();
            }

            return _categoria;
        }

        //BuscarCategoriaSinFiltro para cargar datos de la empresa 
        internal Categoria BuscarCategoriaSinFiltro(string pId)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drCategoria;

            SqlCommand cmdBuscarCategoria = new SqlCommand("BuscarCategoriaSinFiltro", _conexion);
            cmdBuscarCategoria.CommandType = CommandType.StoredProcedure;

            cmdBuscarCategoria.Parameters.AddWithValue("@id", pId);

            Categoria _categoria = null;

            try
            {
                _conexion.Open();
                drCategoria = cmdBuscarCategoria.ExecuteReader();

                if (drCategoria.HasRows)
                {
                    while (drCategoria.Read())
                        _categoria = new Categoria((string)drCategoria["Id"], (string)drCategoria["Nombre"], (string)drCategoria["Descripcion"]);
                }

                drCategoria.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conexion.Close();
            }

            return _categoria;
        }

        public List<Categoria> ListarCategorias()
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drCategorias;

            List<Categoria> _categorias = new List<Categoria>();

            SqlCommand cmdListarCategorias = new SqlCommand("ListarCategorias", _conexion);
            cmdListarCategorias.CommandType = CommandType.StoredProcedure;

            try
            {
                _conexion.Open();
                drCategorias = cmdListarCategorias.ExecuteReader();

                if (drCategorias.HasRows)
                {
                    while (drCategorias.Read())
                    {
                        Categoria _pCategoria = new Categoria((string)drCategorias["Id"], (string)drCategorias["Nombre"], (string)drCategorias["Descripcion"]);
                        _categorias.Add(_pCategoria);
                    }
                }

                drCategorias.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conexion.Close();
            }

            return _categorias;
        }
      
    }
}
