﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;
using System.Data.SqlClient;

namespace Negocio
{
    public class PedidoNegocio
    {

        public void RegistroDetallePedido(int idPedido, List<DetallePedido> detalleCarrito)
        {
            // Inicializar las variables fuera del foreach
            int idProducto = 0;
            int cantidad = 0;
            float precioUnitario = 0;

            AccesoDatos datos = null;

            try
            {

                foreach (var productoC in detalleCarrito)
                {
                    datos = new AccesoDatos(); // Crear la conexión

                    if (productoC.PrecioUnitario > 0 && productoC.Cantidad > 0)
                    {
                        // Asignar las variables con los valores del producto
                        idProducto = productoC.Producto.ID;
                        cantidad = productoC.Cantidad;
                        precioUnitario = productoC.PrecioUnitario;


                        string consulta = @"INSERT INTO DetallePedido (IdPedido, IdProducto, Cantidad, Preciounitario) 
                                    VALUES (@IdPedido, @IdProducto, @Cantidad, @Preciounitario);";
                        datos.setearConsulta(consulta);

                        datos.SetearParametro("@IdPedido", idPedido);
                        datos.SetearParametro("@IdProducto", idProducto);
                        datos.SetearParametro("@Cantidad", cantidad);
                        datos.SetearParametro("@Preciounitario", precioUnitario);

                        // Ejecutar la acción para insertar cada detalle
                        datos.ejecutarAccion();
                        Console.WriteLine($"Detalle de pedido para producto ID {idProducto} insertado correctamente.");
                    }
                    else
                    {

                        Console.WriteLine($"[ADVERTENCIA] Producto con valores inválidos: Precio={productoC.PrecioUnitario}, Cantidad={productoC.Cantidad}");
                    }

                    datos.cerrarConexion();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al registrar los detalles del pedido: {ex.Message}");
                throw;
            }
            finally
            {

                datos?.cerrarConexion();
            }
        }


        public void RegistroPedido(List<DetallePedido> carritoDetalles, string email)
        {
            AccesoDatos datos = null;


            try
            {
                datos = new AccesoDatos();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

                float totalCarrito = 0;


                int idUsuario = usuarioNegocio.ObtenerIdUsuarioPorEmail(email);


                foreach (var productoC in carritoDetalles)
                {
                    if (productoC.PrecioUnitario > 0 && productoC.Cantidad > 0)
                    {
                        totalCarrito += productoC.Cantidad * productoC.PrecioUnitario;
                    }
                    else
                    {
                        Console.WriteLine($"[ADVERTENCIA] Producto con valores inválidos: Precio={productoC.PrecioUnitario}, Cantidad={productoC.Cantidad}");
                    }
                }


                datos.setearConsulta(@"INSERT INTO Pedidos (IdUsuario, MontoTotal, Estado) 
                        VALUES (@IdUsuario, @MontoTotal, @Estado);
                        SELECT SCOPE_IDENTITY();");
                datos.SetearParametro("@IdUsuario", idUsuario);
                datos.SetearParametro("@MontoTotal", totalCarrito);
                datos.SetearParametro("@Estado", "Pendiente");
                datos.ejecutarAccion();
                datos.cerrarConexion();
                int idPedido = Convert.ToInt32(datos.ejecutarScalar());


                RegistroDetallePedido(idPedido, carritoDetalles);

                Console.WriteLine("Pedido registrado exitosamente.");


            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al registrar el pedido: {ex.Message}");
                throw;
            }
            finally
            {

                datos?.cerrarConexion();
            }
        }

        public DataTable ObtenerPedidos(int idUsuario, TipoUsuario tipoUsuario, string estadoFiltro = "")
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


        public List<DetallePedido> ObtenerDetallesPedido(int idPedido)
        {
            List<DetallePedido> detallesPedido = new List<DetallePedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consulta para obtener los detalles del pedido
                string consulta = "SELECT dp.IdProducto, dp.Cantidad, dp.PrecioUnitario, p.Nombre FROM DetallePedido dp INNER JOIN Productos p ON dp.IdProducto = p.IdProducto"
           + " WHERE dp.IdPedido = @IdPedido";

                datos.setearConsulta(consulta);
                datos.SetearParametro("@IdPedido", idPedido);

                // Ejecutar la consulta y obtener los resultados
                DataTable dt = datos.ejecutarLectura2();

                foreach (DataRow row in dt.Rows)
                {
                    DetallePedido detalle = new DetallePedido
                    {
                        idProducto = Convert.ToInt32(row["IdProducto"]),
                        Cantidad = Convert.ToInt32(row["Cantidad"]),
                        PrecioUnitario = Convert.ToSingle(row["PrecioUnitario"]),
                        Nombre = row["Nombre"].ToString(),
                      
                    };

                    detallesPedido.Add(detalle);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los detalles del pedido: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return detallesPedido;
        }


    }
}
