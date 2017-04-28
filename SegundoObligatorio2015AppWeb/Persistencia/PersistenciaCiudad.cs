using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using EntidadesCompartidas;

namespace Persistencia
{
    internal class PersistenciaCiudad : IPersistenciaCiudad
    {
        private static PersistenciaCiudad _instancia = null;
        private PersistenciaCiudad() { }
        public static PersistenciaCiudad GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaCiudad();

            return _instancia;
        }

        public void AltaCiudad(Ciudad pCiudad)
        { 
            SqlConnection _conexion = new SqlConnection (Conexion.Cnn);
    
            SqlCommand cmdAltaCiudad = new SqlCommand("AltaCiudad", _conexion);
            cmdAltaCiudad.CommandType = CommandType.StoredProcedure;

            cmdAltaCiudad.Parameters.AddWithValue("@codigoDepto", pCiudad.CodDepto);
            cmdAltaCiudad.Parameters.AddWithValue("@nombre", pCiudad.Nombre);

            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdAltaCiudad.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();

                cmdAltaCiudad.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("Ya existe la ciudad que intenta agregar a este departamento.");
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

        public void BajaCiudad(Ciudad pCiudad)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdBajaCiudad = new SqlCommand("BajaCiudad", _conexion);
            cmdBajaCiudad.CommandType = CommandType.StoredProcedure;

            cmdBajaCiudad.Parameters.AddWithValue("@codigoDepto", pCiudad.CodDepto);
            cmdBajaCiudad.Parameters.AddWithValue("@nombre", pCiudad.Nombre);

            try
            {
                _conexion.Open();
                cmdBajaCiudad.ExecuteNonQuery();
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

        //BuscarCiudad para ab
        public Ciudad BuscarCiudad(string pCodDepto, string pNombre)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drCiudad;

            SqlCommand cmdBuscarCiudad = new SqlCommand("BuscarCiudad", _conexion);
            cmdBuscarCiudad.CommandType = CommandType.StoredProcedure;

            cmdBuscarCiudad.Parameters.AddWithValue("@codigoDepto", pCodDepto);
            cmdBuscarCiudad.Parameters.AddWithValue("@nombre", pNombre);

            Ciudad _ciudad = null;

            try
            {
                _conexion.Open();
                drCiudad = cmdBuscarCiudad.ExecuteReader();

                if (drCiudad.HasRows)
                {
                    drCiudad.Read();
                    _ciudad = new Ciudad((string)drCiudad["CodigoDepto"], (string)drCiudad["Nombre"]);
                }

                drCiudad.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conexion.Close();
            }

            return _ciudad;
        }

        //BuscarCiudadSinFiltro para cargar datos de la empresa 
        internal Ciudad BuscarCiudadSinFiltro(string pCodDepto, string pNombre)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drCiudad;

            SqlCommand cmdBuscarCiudad = new SqlCommand("BuscarCiudadSinFiltro", _conexion);
            cmdBuscarCiudad.CommandType = CommandType.StoredProcedure;

            cmdBuscarCiudad.Parameters.AddWithValue("@codigoDepto", pCodDepto);
            cmdBuscarCiudad.Parameters.AddWithValue("@nombre", pNombre);

            Ciudad _ciudad = null;

            try
            {
                _conexion.Open();
                drCiudad = cmdBuscarCiudad.ExecuteReader();

                if (drCiudad.HasRows)
                {
                    drCiudad.Read();
                    _ciudad = new Ciudad((string)drCiudad["CodigoDepto"], (string)drCiudad["Nombre"]);
                }

                drCiudad.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conexion.Close();
            }

            return _ciudad;
        }

        public List<Ciudad> ListarCiudades(string pDepartamento)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drCiudades;

            SqlCommand cmdListarCiudades = new SqlCommand("ListarCiudades", _conexion);
            cmdListarCiudades.CommandType = CommandType.StoredProcedure;

            cmdListarCiudades.Parameters.AddWithValue("@CodigoDepto", pDepartamento);

            List<Ciudad> _ciudades = new List<Ciudad>();

            try
            {
                _conexion.Open();
                drCiudades = cmdListarCiudades.ExecuteReader();

                while (drCiudades.Read())
                {
                    Ciudad _ciudad = new Ciudad((string)drCiudades["CodigoDepto"], (string)drCiudades["Nombre"]);
                    _ciudades.Add(_ciudad);
                }

                drCiudades.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conexion.Close();
            }

            return _ciudades;
        }

    }
}
