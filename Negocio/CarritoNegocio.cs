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
        public List<DetallePedido> DetallesCarritoIds(List<int> idProductos)
        {
            List<DetallePedido> lista = new List<DetallePedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta(@"SELECT P.IdProducto, P.Nombre, P.Precio, I.ImagenUrl
                                     FROM Productos P
                                     LEFT JOIN (SELECT IdProducto, ImagenUrl
                                     FROM Imagenes
                                     WHERE IdImagen IN (SELECT MIN(IdImagen)
                                     FROM Imagenes
                                     GROUP BY IdProducto)) I ON P.IdProducto = I.IdProducto");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int productoId = (int)datos.Lector["IdProducto"];

                    bool estaEnCarrito = false;

                    foreach (int id in idProductos)
                    {
                        if (id == productoId)
                        {
                            estaEnCarrito = true;
                            break;
                        }
                    }

                    if (estaEnCarrito)
                    {
                        DetallePedido detallePedido = new DetallePedido();
                        detallePedido.Producto = new Producto();
                        detallePedido.Producto.ID = productoId;
                        detallePedido.Producto.Nombre = datos.Lector["Nombre"].ToString();
                        detallePedido.PrecioUnitario = (float)(decimal)datos.Lector["Precio"];
                        detallePedido.Cantidad = 1; 

                        lista.Add(detallePedido);
                    }
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
            return lista;
        }
    }
}
