using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool IniciarSesion(Usuario usuario)
        {
            AccesoDatos dato = new AccesoDatos();
            try
            {
                dato.setearConsulta("SELECT IdUsuario, TipoUsuario " +
                "FROM Usuarios " +
                "WHERE Email = @Email AND Contraseña = @contrasenia");
                dato.SetearParametro("@Email", usuario.Email);
                dato.SetearParametro("@contrasenia", usuario.Contrasenia);
                dato.ejecutarLectura();
                while (dato.Lector.Read())
                {
                    usuario.ID = (int)dato.Lector["IdUsuario"];
                    usuario.TipoUsuario = (int)(dato.Lector["TipoUsuario"]) == 1 ? TipoUsuario.Administrador : TipoUsuario.Cliente;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dato.cerrarConexion();
            }
        }
    }
}
