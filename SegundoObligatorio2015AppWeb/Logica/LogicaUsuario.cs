using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaUsuario : ILogicaUsuario
    {
        private static LogicaUsuario _instancia = null;
        private LogicaUsuario() { }
        public static LogicaUsuario GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaUsuario();
            }

            return _instancia;
        }

        public Usuario LogueoUsuario(string nombreUsuario, string contrasenia)
        {
            Usuario unUsu = null;

            unUsu = FabricaPersistencia.GetPersistenciaCliente().LogueoCliente(nombreUsuario, contrasenia);

            if (unUsu == null)
                unUsu = FabricaPersistencia.GetPersistenciaAdministrador().LogueoAdmin(nombreUsuario, contrasenia);
            
                return unUsu;
        }

        public void AltaUsuario(Usuario usuario)
        {
            if (usuario is Administrador)
                FabricaPersistencia.GetPersistenciaAdministrador().AltaAdministrador((Administrador)usuario);

            else if (usuario is Cliente)
                FabricaPersistencia.GetPersistenciaCliente().AltaCliente((Cliente)usuario);

            else
                throw new Exception("El tipo de usuario no es valido");
        }

        public void BajaUsuario(Usuario usuario)
        {
            if (usuario is Administrador)
                FabricaPersistencia.GetPersistenciaAdministrador().BajaAdministrador((Administrador)usuario);
        }

        public void ModificarUsuario(Usuario usuario)
        {
            if (usuario is Administrador)
            FabricaPersistencia.GetPersistenciaAdministrador().ModificarAdministrador((Administrador)usuario);
        }

        public Usuario BuscarUsuario(int ci)
        {
            Usuario unUsu = null;

            unUsu = FabricaPersistencia.GetPersistenciaAdministrador().BuscarAdministrador(ci);
            
            if (unUsu == null)
                unUsu = FabricaPersistencia.GetPersistenciaCliente().BuscarCliente(ci);

            return unUsu;
        }
    }


}
