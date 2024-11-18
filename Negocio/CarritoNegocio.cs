using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CarritoNegocio
    {
        public List<DetallePedido> ListarCarritoCliente(int idCliente)
        {
            List<DetallePedido> listaPedido = new List<DetallePedido>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT C.IdCarrito, DC.IdProducto, DC.Cantidad, DC.Precio, P.Nombre
                                       FROM Carrito C
                                       INNER JOIN DetallesCarrito DC ON C.IdCarrito = DC.IdCarrito
                                       INNER JOIN Productos P ON DC.IdProducto = P.IdProducto
                                       WHERE C.IdCliente = @IdCliente");
                datos.SetearParametro("@IdCliente", idCliente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetallePedido detallePedido = new DetallePedido();
                    detallePedido.Producto = new Producto();
                    detallePedido.Producto.ID = (int)datos.Lector["IdProducto"];
                    detallePedido.Producto.Nombre = datos.Lector["Nombre"].ToString();

                    detallePedido.Cantidad = (int)datos.Lector["Cantidad"];
                    detallePedido.PrecioUnitario = (float)(decimal)datos.Lector["Precio"];

                    listaPedido.Add(detallePedido);
                }
                return listaPedido;
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
        //public List<DetallePedido> ObtenerDetallesCarritoPorIds(List<int> idsProductos)
        //{
        //    List<DetallePedido> lista = new List<DetallePedido>();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        string query = @"SELECT P.IdProducto, P.Nombre, P.Precio, I.ImagenUrl
        //                 FROM Productos P
        //                 LEFT JOIN (SELECT IdProducto, ImagenUrl
        //                                 FROM Imagenes
        //                                 WHERE IdImagen IN (SELECT MIN(IdImagen)
        //                                     FROM Imagenes
        //                                     GROUP BY IdProducto)) I ON P.IdProducto = I.IdProducto
        //                 WHERE P.IdProducto IN (" + string.Join(",", idsProductos) + ")";
        //        datos.setearConsulta(query);
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            DetallePedido detalle = new DetallePedido();
        //            detalle.Producto = new Producto
        //            {
        //                ID = (int)datos.Lector["IdProducto"],
        //                Nombre = datos.Lector["Nombre"].ToString()
        //            };
        //            detalle.PrecioUnitario = (float)(decimal)datos.Lector["Precio"];
        //            detalle.Cantidad = 1; // Por ahora asume una cantidad fija de uno

        //            lista.Add(detalle);
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

        //    return lista;
        //}

    }
}
