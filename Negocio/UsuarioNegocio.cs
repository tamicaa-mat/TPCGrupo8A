using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Data.SqlClient;

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



        // NO ME AGREGA EL CLIENTE A LA BD :(
        public void RegistroCliente(Cliente cliente)
        {
            
            cliente.TipoUsuario = TipoUsuario.Cliente;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Obtener el IdUsuario basado en el Email
                cliente.idUsuario = ObtenerIdUsuarioPorEmail(cliente.Email, datos);

               
                if (!(cliente.idUsuario > 0))
                {
                    Console.WriteLine("No se encontró un usuario con ese email.");
                    return;
                }

                // Configurar la consulta de inserción
                string consulta = "INSERT INTO Clientes (IdUsuario, Nombre, Apellido, Direccion, Email, Telefono, TipoUsuario) " +
                                  "VALUES (@IdUsuario, @Nombre, @Apellido, @Direccion, @Email, @Telefono, @TipoUsuario)";

              
                datos.setearConsulta(consulta);
                datos.SetearParametro("@IdUsuario", cliente.idUsuario); 
                datos.SetearParametro("@Nombre", cliente.Nombre);
                datos.SetearParametro("@Apellido", cliente.Apellido);
                datos.SetearParametro("@Direccion", cliente.Direccion);
                datos.SetearParametro("@Email", cliente.Email);
                datos.SetearParametro("@Telefono", cliente.Telefono);
                datos.SetearParametro("@TipoUsuario", (int)cliente.TipoUsuario); 

               
                datos.ejecutarAccion();
                Console.WriteLine("Cliente insertado correctamente.");
            }
            catch (SqlException sqlEx)
            {
              
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error General: {ex.Message}");
            }
            finally
            {
             
                datos.cerrarConexion();
            }
        }



        private int ObtenerIdUsuarioPorEmail(string email, AccesoDatos datos)
        {
            int idUsuario = -1;

            try
            {
                datos.setearConsulta("SELECT IdUsuario FROM Usuarios WHERE Email = @Email");
                datos.SetearParametro("@Email", email);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    idUsuario = Convert.ToInt32(datos.Lector["IdUsuario"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return idUsuario;
        }

















    }
}
