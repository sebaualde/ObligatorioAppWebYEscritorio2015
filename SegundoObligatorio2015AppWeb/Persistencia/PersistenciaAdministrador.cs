using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencia
{
    internal class PersistenciaAdministrador: IPersistenciaAdministrador
    {
        private static PersistenciaAdministrador _instancia = null;
        private PersistenciaAdministrador() { }
        public static PersistenciaAdministrador GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaAdministrador();

            return _instancia;
        }

        public Administrador LogueoAdmin(string pNombreUsuario, string pContrasenia)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drAdmin;

            SqlCommand cmdLogueoAdmin = new SqlCommand("LogueoAdmin", _conexion);
            cmdLogueoAdmin.CommandType = CommandType.StoredProcedure;

            cmdLogueoAdmin.Parameters.AddWithValue("@usuario", pNombreUsuario);
            cmdLogueoAdmin.Parameters.AddWithValue("@contrasenia", pContrasenia);

            Administrador _administrador = null;
            
            try
            {
                _conexion.Open();
                drAdmin = cmdLogueoAdmin.ExecuteReader();

                if (drAdmin.HasRows)
                {
                    drAdmin.Read();
                    _administrador = new Administrador((int)drAdmin["CI"], (string)drAdmin["Nombre"], (string)drAdmin["Usuario"], (string)drAdmin["Contrasenia"], (bool)drAdmin["VeListados"]);
                }

                drAdmin.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _conexion.Close();
            }

            return _administrador;
        }

        public Administrador BuscarAdministrador(int pci)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drAdministrador;

            SqlCommand cmdBuscarAdministrador = new SqlCommand("BuscarAdministrador", _conexion);
            cmdBuscarAdministrador.CommandType = CommandType.StoredProcedure;

            cmdBuscarAdministrador.Parameters.AddWithValue("@ci", pci);

            Administrador _administrador = null;

            try
            {
                _conexion.Open();
                drAdministrador = cmdBuscarAdministrador.ExecuteReader();

                if (drAdministrador.HasRows)
                {
                    drAdministrador.Read();
                    _administrador = new Administrador((int)drAdministrador["CI"], (string)drAdministrador["Nombre"], (string)drAdministrador["Usuario"], (string)drAdministrador["Contrasenia"], (bool)drAdministrador["VeListados"]);
                }

                drAdministrador.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conexion.Close();
            }

            return _administrador;
        }

        public void AltaAdministrador(Administrador pAdministrador)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdAltaAdministrador = new SqlCommand("AltaAdministrador", _conexion);
            cmdAltaAdministrador.CommandType = CommandType.StoredProcedure;

            cmdAltaAdministrador.Parameters.AddWithValue("@ci", pAdministrador.CI);
            cmdAltaAdministrador.Parameters.AddWithValue("@nombre", pAdministrador.Nombre);
            cmdAltaAdministrador.Parameters.AddWithValue("@usuario", pAdministrador.NombreUsuario);
            cmdAltaAdministrador.Parameters.AddWithValue("@contrasenia", pAdministrador.Contrasenia);
            cmdAltaAdministrador.Parameters.AddWithValue("@veListado", pAdministrador.VeListado);


            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdAltaAdministrador.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();

                cmdAltaAdministrador.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("CI ya existente.");

                if ((int)_valorRetorno.Value == -2)
                    throw new Exception("Usuario ya exitente.");

                if ((int)_valorRetorno.Value == -3)
                    throw new Exception("Error al intentar agregar un usuario.");

                if ((int)_valorRetorno.Value == -4)
                    throw new Exception("Error al intentar agregar un administrador.");
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

        public void BajaAdministrador(Administrador pAdministrador)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdBajaAdministrador = new SqlCommand("BajaAdministrador", _conexion);
            cmdBajaAdministrador.CommandType = CommandType.StoredProcedure;

            cmdBajaAdministrador.Parameters.AddWithValue("@ci", pAdministrador.CI);

            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdBajaAdministrador.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();

                cmdBajaAdministrador.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("CI no existente.");

                if ((int)_valorRetorno.Value == -2)
                    throw new Exception("Error al intentar eliminar administrador.");
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

        public void ModificarAdministrador(Administrador pAdministrador)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdModificarAdministrador = new SqlCommand("ModificarAdministrador", _conexion);
            cmdModificarAdministrador.CommandType = CommandType.StoredProcedure;

            cmdModificarAdministrador.Parameters.AddWithValue("@ci", pAdministrador.CI);
            cmdModificarAdministrador.Parameters.AddWithValue("@nombre", pAdministrador.Nombre);
            cmdModificarAdministrador.Parameters.AddWithValue("@usuario", pAdministrador.NombreUsuario);
            cmdModificarAdministrador.Parameters.AddWithValue("@contrasenia", pAdministrador.Contrasenia);
            cmdModificarAdministrador.Parameters.AddWithValue("@veListado", pAdministrador.VeListado);


            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdModificarAdministrador.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();
                cmdModificarAdministrador.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("CI no existente");

                if ((int)_valorRetorno.Value == -2)
                    throw new Exception("Usuario ya existente");

                if ((int)_valorRetorno.Value == -3)
                    throw new Exception("Error al modificar administrador");

                if ((int)_valorRetorno.Value == -4)
                    throw new Exception("Error al modificar usuario");

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
