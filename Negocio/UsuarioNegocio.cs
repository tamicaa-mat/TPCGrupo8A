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
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdUsuario, Apellido, Nombre, Email,  TipoUsuario " +
                "FROM Usuarios " +
                "WHERE Email = @Email AND Contraseña = @contrasenia");
                datos.SetearParametro("@Email", usuario.Email);
                datos.SetearParametro("@contrasenia", usuario.Contrasenia);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    usuario.ID = (int)datos.Lector["IdUsuario"];
                    usuario.TipoUsuario = (int)(datos.Lector["TipoUsuario"]) == 1 ? TipoUsuario.Administrador : TipoUsuario.Cliente;
                    usuario.Apellido = datos.Lector["Apellido"].ToString();
                    usuario.Nombre = datos.Lector["Nombre"].ToString();
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
                datos.cerrarConexion();
            }
        }
    }
}
