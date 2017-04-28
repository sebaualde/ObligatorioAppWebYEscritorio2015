using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaTelefono
    {
        internal static void Agregar(Telefono telefono, Empresa empresa, SqlTransaction transaccion)
        {
            SqlCommand cmdAgregarTelefono = new SqlCommand("AgregarTelefono", transaccion.Connection);
            cmdAgregarTelefono.CommandType = CommandType.StoredProcedure;

            cmdAgregarTelefono.Parameters.AddWithValue("@rut", empresa.Rut);
            cmdAgregarTelefono.Parameters.AddWithValue("@telefono", telefono.Numero);

            SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;

            cmdAgregarTelefono.Parameters.Add(retorno);

            try
            {
                cmdAgregarTelefono.Transaction = transaccion;

                int filasAfectadas = cmdAgregarTelefono.ExecuteNonQuery();

                if (filasAfectadas < 1)
                {
                    int valorRetorno= Convert.ToInt32(retorno.Value);

                    if (valorRetorno == 1)
                    {
                        throw new Exception(String.Format("El teléfono {0} ya está registrado para la empresa {1}", telefono.Numero,empresa.Rut));
                    }
                    else if (valorRetorno == 2)
                    {
                        throw new Exception(String.Format("No existe la empresa con RUT {0}", empresa.Rut));
                    }
                    else
                    {
                        throw new Exception("No se pudo agregar el teléfono");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        internal static void EliminarTelefonosDeEmpresa(string empresa, SqlTransaction transaccion)
        {
            SqlCommand cmdEliminarTelsEmpresa = new SqlCommand("EliminarTelefonosDeEmpresa", transaccion.Connection);
            cmdEliminarTelsEmpresa.CommandType = CommandType.StoredProcedure;

            cmdEliminarTelsEmpresa.Parameters.AddWithValue("@rut", empresa);

            try
            {
                cmdEliminarTelsEmpresa.Transaction = transaccion;

                cmdEliminarTelsEmpresa.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        internal static List<Telefono> TelefonosDeEmpresa(string rutEmpresa)
        {
            SqlConnection conexion = null;
            SqlDataReader drTelefonos = null;

            try
            {
                conexion= new SqlConnection(Conexion.Cnn);
                SqlCommand cmdTelefonosDeEmpresa = new SqlCommand("TelefonosDeEmpresa", conexion);
                cmdTelefonosDeEmpresa.CommandType = CommandType.StoredProcedure;

                cmdTelefonosDeEmpresa.Parameters.AddWithValue("@rut", rutEmpresa);

                conexion.Open();

                drTelefonos = cmdTelefonosDeEmpresa.ExecuteReader();

                List<Telefono> telefonos = new List<Telefono>();

                if (drTelefonos.HasRows)
                {
                    while (drTelefonos.Read())
                    {
                        Telefono telefono = new Telefono((string)drTelefonos["Telefono"]);

                        telefonos.Add(telefono);
                    }
                }

                return telefonos;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drTelefonos != null)
                {
                    drTelefonos.Close();
                }

                if (conexion != null)
                {
                    conexion.Close();
                }
            }

        }
       
    }
}
