using System;
using System.Collections.Generic;
using System.Data;
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




        public DataTable ObtenerPedidos(int idUsuario, TipoUsuario tipoUsuario,  string estadoFiltro = "")
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                string consulta = "SELECT DISTINCT P.IdPedido AS Numero, P.Fecha, P.IdUsuario AS Cliente, P.MontoTotal AS Importe, P.Estado FROM Pedidos P";

                if (tipoUsuario == TipoUsuario.Cliente)
                {
                    consulta += " INNER JOIN Clientes C ON P.IdUsuario = C.IdUsuario WHERE C.IdUsuario = @IdUsuario AND C.TipoUsuario = @TipoUsuario";
                    datos.SetearParametro("@IdUsuario", idUsuario);
                    datos.SetearParametro("@TipoUsuario", (int)tipoUsuario);
                    if (!string.IsNullOrEmpty(estadoFiltro))
                    {
                        consulta += " AND Estado = @Estado";
                        datos.SetearParametro("@Estado", estadoFiltro);
                    }
                }
                else 
                if (tipoUsuario == TipoUsuario.Administrador)
                {
                    if (!string.IsNullOrEmpty(estadoFiltro))
                    {
                        consulta += " WHERE Estado = @Estado";
                        datos.SetearParametro("@Estado", estadoFiltro);
                    }
                }
                datos.setearConsulta(consulta);
                return datos.ejecutarLectura2();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pedidos: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void CambiarEstadoAPedido(int idPedido, string nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
               
                datos.setearConsulta("UPDATE Pedidos SET Estado = @NuevoEstado WHERE IdPedido = @IdPedido");


                datos.SetearParametro("@NuevoEstado", nuevoEstado);
                datos.SetearParametro("@IdPedido", idPedido);

                datos.ejecutarAccion();

               
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el estado del pedido: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión
                datos.cerrarConexion();
            }
        }



    }
}
