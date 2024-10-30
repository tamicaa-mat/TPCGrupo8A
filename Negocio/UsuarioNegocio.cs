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
     
        public void RegistroUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Usuarios(Apellido, Nombre, Email, Contraseña, TipoUsuario) " +
                                     "VALUES(@Apellido, @Nombre, @Email, @Contraseña, @TipoUsuario)");

                datos.SetearParametro("@Apellido", usuario.Apellido);
                datos.SetearParametro("@Nombre", usuario.Nombre);
                datos.SetearParametro("@Email", usuario.Email);
                datos.SetearParametro("@Contraseña", usuario.Contrasenia);
                datos.SetearParametro("@TipoUsuario", 0);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


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
