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
        //public void RegistroPedido(Usuario usuario, List<Producto> productosCarrito)
        //{
        //    usuario.TipoUsuario = TipoUsuario.Cliente;
        //    Pedido pedido = new Pedido();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        // Obtener el IdUsuario basado en el Email
        //        usuario.idUsuario = ObtenerIdUsuarioPorEmail(usuario.Email, datos);

        //        if (!(usuario.idUsuario > 0))
        //        {
        //            Console.WriteLine("No se encontró un usuario con ese email.");
        //            return;
        //        }

        //        // Calcular el monto total
        //        float montoTotal = 0;
        //        foreach (Producto producto in productosCarrito)
        //        {
        //            montoTotal += producto.Precio * producto.Cantidad;  // Precio * cantidad de cada producto
        //        }

        //        // Configurar la consulta para insertar el pedido
        //        string consulta = "INSERT INTO Pedidos (IdUsuario, Monto, Estado) " +
        //                          "VALUES (@IdUsuario, @Monto, @Estado); SELECT SCOPE_IDENTITY();";  // Obtener el ID del pedido insertado

        //        datos.setearConsulta(consulta);
        //        datos.SetearParametro("@IdUsuario", usuario.idUsuario);
        //        datos.SetearParametro("@Monto", montoTotal);  // Pasar el monto calculado
        //        datos.SetearParametro("@Estado", EstadoPedido.Pendiente);  // Asumiendo que el estado inicial es "Pendiente"

        //        // Ejecutar la consulta de inserción y obtener el ID del pedido insertado
        //        int idPedido = Convert.ToInt32(datos.ejecutarEscalar());  // Ejecutar la consulta y obtener el ID generado

        //        Console.WriteLine($"Pedido insertado correctamente con ID: {idPedido}");

        //        // Insertar los productos en la tabla DetallePedido
        //        foreach (Producto producto in productosCarrito)
        //        {
        //            string consultaDetalle = "INSERT INTO DetallePedido (IdPedido, IdProducto, Cantidad, PrecioUnitario) " +
        //                                     "VALUES (@IdPedido, @IdProducto, @Cantidad, @PrecioUnitario)";

        //            datos.setearConsulta(consultaDetalle);
        //            datos.SetearParametro("@IdPedido", idPedido);  // ID del pedido insertado
        //            datos.SetearParametro("@IdProducto", producto.Id);  // Suponiendo que el producto tiene un ID
        //            datos.SetearParametro("@Cantidad", producto.Cantidad);
        //            datos.SetearParametro("@PrecioUnitario", producto.Precio);

        //            datos.ejecutarAccion();  // Ejecutar la inserción en DetallePedido
        //        }

        //        Console.WriteLine("Detalles del pedido insertados correctamente.");
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Console.WriteLine($"SQL Error: {sqlEx.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error General: {ex.Message}");
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}




        //public int ObtenerIdUsuarioPorEmail(string email, AccesoDatos datos)
        //{
        //    int idUsuario = -1;

        //    try
        //    {
        //        datos.setearConsulta("SELECT IdUsuario FROM Usuarios WHERE Email = @Email");
        //        datos.SetearParametro("@Email", email);

        //        datos.ejecutarLectura();

        //        if (datos.Lector.Read())
        //        {
        //            idUsuario = Convert.ToInt32(datos.Lector["IdUsuario"]);
        //            return idUsuario;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }

        //    return idUsuario;
        //}
        public int ObtenerIdUsuarioPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdUsuario FROM Usuarios WHERE Email = @Email");
                datos.SetearParametro("@Email", email);

                datos.ejecutarLectura();

                if (datos.Lector.HasRows)
                {
                    Console.WriteLine("Hay filas en la consulta.");
                    if (datos.Lector.Read())
                    {
                        int idUsuario = (int)datos.Lector["IdUsuario"];
                        return idUsuario;
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron resultados.");
                }
                throw new Exception("Usuario no encontrado.");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
