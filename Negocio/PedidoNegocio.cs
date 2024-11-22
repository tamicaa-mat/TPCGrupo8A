using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;

namespace Negocio
{
    public class PedidoNegocio
    {
        //public void RegistroPedido(List<Producto> productosCarrito, string email)
        //{
        //    AccesoDatos datos = null; // Declarar fuera del try

        //    try
        //    {
        //        datos = new AccesoDatos(); // Inicializar dentro del try
        //        UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        //        // Obtener el idUsuario asociado al email
        //        int idUsuario = usuarioNegocio.ObtenerIdUsuarioPorEmail(email, datos);

        //        // Calcular el monto total del pedido
        //        float montoTotal =0;

        //        foreach (Producto producto in productosCarrito)
        //        {
        //            montoTotal += producto.Precio * producto.Cantidad; // Precio por cantidad
        //        }

        //        // Insertar el pedido en la tabla Pedidos
        //        datos.setearConsulta("INSERT INTO Pedidos (IdUsuario, MontoTotal, Estado) " +
        //                             "OUTPUT INSERTED.IdPedido " + // Obtener el Id del pedido generado
        //                             "VALUES (@IdUsuario, @MontoTotal, @Estado)");

        //        datos.SetearParametro("@IdUsuario", idUsuario);
        //        datos.SetearParametro("@MontoTotal", montoTotal);
        //        datos.SetearParametro("@Estado", "Pendiente"); // Estado inicial del pedido

        //        datos.ejecutarAccion();

        //       // Response.Redirect("RegistroExitoso.aspx?nombre=Usuario", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }
        //    finally
        //    {
        //        if (datos != null)
        //        {
        //            datos.cerrarConexion(); // Asegurarse de cerrar la conexión
        //        }
        //    }
        //}


        public void RegistroPedido(List<Producto> productosCarrito, string email)
        {
            AccesoDatos datos = null; // Declarar fuera del try

            try
            {
                datos = new AccesoDatos(); // Inicializar dentro del try
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

                // Obtener el idUsuario asociado al email
                int idUsuario = usuarioNegocio.ObtenerIdUsuarioPorEmail(email);

                // Calcular el monto total del pedido
                float montoTotal = 0;

                Console.WriteLine("---- Inicio del cálculo de Monto Total ----");
                foreach (Producto producto in productosCarrito)
                {
                    Console.WriteLine($"Producto: {producto.Nombre}, Precio: {producto.Precio}, Cantidad: {producto.Cantidad}");

                    if (producto.Precio > 0 && producto.Cantidad > 0)
                    {
                        montoTotal += producto.Precio * producto.Cantidad; // Precio por cantidad
                    }
                    else
                    {
                        Console.WriteLine($"[ADVERTENCIA] Producto con valores inválidos: Precio={producto.Precio}, Cantidad={producto.Cantidad}");
                    }
                }
                Console.WriteLine($"Monto Total Calculado: {montoTotal}");
                Console.WriteLine("---- Fin del cálculo de Monto Total ----");

                // Insertar el pedido en la tabla Pedidos
                datos.setearConsulta("INSERT INTO Pedidos (IdUsuario, MontoTotal, Estado) " +
                                     "OUTPUT INSERTED.IdPedido " + // Obtener el Id del pedido generado
                                     "VALUES (@IdUsuario, @MontoTotal, @Estado)");

                datos.SetearParametro("@IdUsuario", idUsuario);
                datos.SetearParametro("@MontoTotal", montoTotal);
                datos.SetearParametro("@Estado", "Pendiente"); // Estado inicial del pedido

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (datos != null)
                {
                    datos.cerrarConexion(); // Asegurarse de cerrar la conexión
                }
            }
        }








    }
}
