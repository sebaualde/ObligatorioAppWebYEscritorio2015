using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencia
{
    internal class PersistenciaCliente : IPersistenciaCliente
    {
        private static PersistenciaCliente _instancia = null;
        private PersistenciaCliente() { }

        public static PersistenciaCliente GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaCliente();

            return _instancia;
        }
        
        public Cliente LogueoCliente(string pNombreUsuario, string pContrasenia)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drCliente;

            SqlCommand cmdLogueoCliente = new SqlCommand("LogueoCliente", _conexion);
            cmdLogueoCliente.CommandType = CommandType.StoredProcedure;

            cmdLogueoCliente.Parameters.AddWithValue("@usuario", pNombreUsuario);
            cmdLogueoCliente.Parameters.AddWithValue("@contrasenia", pContrasenia);

            Cliente _cliente = null;

            try
            {
                _conexion.Open();
                drCliente = cmdLogueoCliente.ExecuteReader();

                if (drCliente.HasRows)
                {
                    drCliente.Read();
                    _cliente = new Cliente((int)drCliente["CI"], (string)drCliente["Nombre"], (string)drCliente["Usuario"], (string)drCliente["Contrasenia"], (int)drCliente["Edad"]);
                }

                drCliente.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("Error! " + ex.Message);
            }

            finally
            {
                _conexion.Close();
            }

            return _cliente;
        }

        public Cliente BuscarCliente(int pci)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drCliente;

            SqlCommand cmdBuscarCliente = new SqlCommand("BuscarCliente", _conexion);
            cmdBuscarCliente.CommandType = CommandType.StoredProcedure;

            cmdBuscarCliente.Parameters.AddWithValue("@ci", pci);

            Cliente _cliente = null;

            try
            {
                _conexion.Open();
                drCliente = cmdBuscarCliente.ExecuteReader();

                if (drCliente.HasRows)
                {
                     drCliente.Read();
                    _cliente = new Cliente((int)drCliente["CI"], (string)drCliente["Nombre"], (string)drCliente["Usuario"], (string)drCliente["Contrasenia"], (int)drCliente["Edad"]);
                }
                drCliente.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error! " + ex.Message);
            }
            finally
            {
                _conexion.Close();
            }

            return _cliente;
        }

        public void AltaCliente(Cliente pCliente)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdAltaCliente = new SqlCommand("AltaCliente", _conexion);
            cmdAltaCliente.CommandType = CommandType.StoredProcedure;

            cmdAltaCliente.Parameters.AddWithValue("@ci", pCliente.CI);
            cmdAltaCliente.Parameters.AddWithValue("@nombre", pCliente.Nombre);
            cmdAltaCliente.Parameters.AddWithValue("@usuario", pCliente.NombreUsuario);
            cmdAltaCliente.Parameters.AddWithValue("@contrasenia", pCliente.Contrasenia);
            cmdAltaCliente.Parameters.AddWithValue("@edad", pCliente.Edad);


            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdAltaCliente.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();

                cmdAltaCliente.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("CI ya existente.");

                if ((int)_valorRetorno.Value == -2)
                    throw new Exception("Usuario ya exitente.");

                if ((int)_valorRetorno.Value == -3)
                    throw new Exception("Error al intentar agregar usuario.");

                if ((int)_valorRetorno.Value == -4)
                    throw new Exception("Error al intentar agregar cliente.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error! " + ex.Message);
            }
            finally
            {
                _conexion.Close();
            }
        }
    }
}
