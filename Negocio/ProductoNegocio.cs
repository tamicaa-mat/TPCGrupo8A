using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT  P.IdProducto, P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock, I.ImagenUrl, M.IdMarca, M.Nombre AS Marca, C.IdCategoria, C.Nombre AS Categoria
                                     FROM Productos P
                                     LEFT JOIN  (SELECT IdProducto, ImagenUrl
                                                FROM Imagenes 
                                                WHERE IdImagen IN (SELECT MIN(IdImagen) 
                                                FROM Imagenes 
                                                GROUP BY IdProducto)) I 
                                    ON P.IdProducto = I.IdProducto
                                    JOIN Marcas M ON P.IdMarca = M.IdMarca
                                    JOIN Categorias C ON P.IdCategoria = C.IdCategoria
                                    WHERE Estado = 1;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.ID = (int)datos.Lector["IdProducto"];
                    aux.Codigo = datos.Lector["Codigo"].ToString();
                    aux.Nombre = datos.Lector["Nombre"].ToString();
                    aux.Descripcion = datos.Lector["Descripcion"].ToString();
                    aux.Marca = new Marca((int)datos.Lector["IdMarca"], datos.Lector["Marca"].ToString());
                    aux.Categoria = new Categoria((int)datos.Lector["IdCategoria"], datos.Lector["Categoria"].ToString());

                    if (aux.Imagenes == null)
                        aux.Imagenes = new List<Imagen>();

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.Imagenes.Add(new Imagen(datos.Lector["ImagenUrl"].ToString()));
                    }
                    else
                    {
                        aux.Imagenes.Add(new Imagen("https://media.istockphoto.com/id/1128826884/es/vector/ning%C3%BAn-s%C3%ADmbolo-de-vector-de-imagen-falta-icono-disponible-no-hay-galer%C3%ADa-para-este-momento.jpg?s=612x612&w=0&k=20&c=9vnjI4XI3XQC0VHfuDePO7vNJE7WDM8uzQmZJ1SnQgk="));
                    }
                    aux.Precio = (float)(decimal)datos.Lector["Precio"];
                    lista.Add(aux);
                }
                return lista;
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
        public void agregar(Producto productoNuevo)
        {
            agregarProducto(productoNuevo);
            //productoNuevo.ID = idProducto;
            int idProducto = Convert.ToInt32(ObtenerIdProducto(productoNuevo.ID));
            foreach (var imagen in productoNuevo.Imagenes)
            {
                agregarImagenUrl(idProducto, imagen.ImagenUrl);
            }
        }
        public void agregarProducto(Producto productoNuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Productos (Codigo, Nombre, Descripcion, IdCategoria, IdMarca, Stock, Precio) " +
                                     "VALUES (@Codigo, @Nombre, @Descripcion, @IdCategoria, @IdMarca, @Stock, @Precio);");
                datos.SetearParametro("@Codigo", productoNuevo.Codigo);
                datos.SetearParametro("@Nombre", productoNuevo.Nombre);
                datos.SetearParametro("@Descripcion", productoNuevo.Descripcion);
                datos.SetearParametro("@IdCategoria", productoNuevo.Categoria.ID);
                datos.SetearParametro("@IdMarca", productoNuevo.Marca.ID);
                datos.SetearParametro("@Stock", productoNuevo.Stock);
                datos.SetearParametro("@Precio", productoNuevo.Precio);
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
        public void agregarImagenUrl(int idProducto, string Imagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Imagenes (IdProducto, ImagenUrl) " +
                                     "VALUES (@IdProducto, @ImagenUrl)");
                datos.SetearParametro("@IdProducto", idProducto);
                datos.SetearParametro("@ImagenUrl", Imagen);
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
        public void editar(Producto producto)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Productos SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, " +
                                     "Precio  = @Precio, Stock = @Stock, IdMarca = @IdMarca, IdCategoria = @IdCategoria " +
                                     "WHERE IdProducto = @IdProducto");
                datos.SetearParametro("@Codigo", producto.Codigo);
                datos.SetearParametro("@Nombre", producto.Nombre);
                datos.SetearParametro("@Descripcion", producto.Descripcion);
                datos.SetearParametro("@Precio", producto.Precio);
                datos.SetearParametro("@Stock", producto.Stock);
                datos.SetearParametro("@IdMarca", producto.Marca.ID);
                datos.SetearParametro("@IdCategoria", producto.Categoria.ID);
                datos.SetearParametro("@IdProducto", producto.ID);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR al editar la Producto:" + ex.Message, ex);
            }
        }
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("EXEC EliminacionLogicaProducto @idProducto = @idProducto"); //eliminación logica
                datos.SetearParametro("@idProducto", id);
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
        public bool ExisteNombreProducto(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            bool existeNombre = false;

            try
            {
                datos.setearConsulta("SELECT Nombre FROM Productos WHERE UPPER(Nombre) = UPPER(@Nombre)"); //Usar UPPER para que no haya diferencia entre mayus y minus Reme = REME
                datos.SetearParametro("@Nombre", nombre);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    existeNombre = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR al agregar producto", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
            return existeNombre;
        }
        //public Producto ObtenerIdProducto(int id)
        //{
        //    AccesoDatos datos = new AccesoDatos();
        //    Producto producto = null;
        //    datos.setearConsulta("SELECT P.IdProducto, P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock, " +
        //                         "M.IdMarca, M.Nombre AS Marca, C.IdCategoria, C.Nombre AS Categoria " +
        //                         "FROM Productos P " +
        //                         "LEFT JOIN Marcas M ON P.IdMarca = M.IdMarca " +
        //                         "LEFT JOIN Categorias C ON P.IdCategoria = C.IdCategoria " +
        //                         "WHERE P.IdProducto = @IdProducto");

        //    datos.SetearParametro("@IdProducto", id);
        //    datos.ejecutarLectura();

        //    if (datos.Lector.Read())
        //    {
        //        producto = new Producto();
        //        producto.ID = (int)datos.Lector["IdProducto"];
        //        producto.Codigo = datos.Lector["Codigo"].ToString();
        //        producto.Nombre = datos.Lector["Nombre"].ToString();
        //        producto.Descripcion = datos.Lector["Descripcion"].ToString();
        //        producto.Precio = (float)(decimal)datos.Lector["Precio"];
        //        producto.Stock = (int)datos.Lector["Stock"];

        //        if (datos.Lector["IdMarca"] != DBNull.Value)
        //        {
        //            producto.Marca = new Marca();
        //            producto.Marca.ID = (int)datos.Lector["IdMarca"];
        //            producto.Marca.Nombre = datos.Lector["Marca"].ToString();
        //        }
        //        if (datos.Lector["IdCategoria"] != DBNull.Value)
        //        {
        //            producto.Categoria = new Categoria();
        //            producto.Categoria.ID = (int)datos.Lector["IdCategoria"];
        //            producto.Categoria.Nombre = datos.Lector["Categoria"].ToString();
        //        }
        //    }
        //    return producto;
        //}

//        public Producto PrimerArticulo()
//        {
//            AccesoDatos datos = new AccesoDatos();

//            Producto producto = null;
//            try
//            {
//                datos.setearConsulta("SELECT P.IdProducto, P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock, " +
//                                "M.IdMarca, M.Nombre AS Marca, C.IdCategoria, C.Nombre AS Categoria " +
//                                "FROM Productos P " +
//                                "LEFT JOIN Marcas M ON P.IdMarca = M.IdMarca " +
//                                "LEFT JOIN Categorias C ON P.IdCategoria = C.IdCategoria " +
//                                "WHERE P.IdProducto = @IdProducto");

//                datos.SetearParametro("@IdProducto", id);
//                datos.ejecutarLectura();

//                if (datos.Lector.Read())
//                {
//                    producto = new Producto();
//                    producto.ID = (int)datos.Lector["IdProducto"];
//                    producto.Codigo = datos.Lector["Codigo"].ToString();
//                    producto.Nombre = datos.Lector["Nombre"].ToString();
//                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
//                    producto.Precio = (float)(decimal)datos.Lector["Precio"];
//                    producto.Stock = (int)datos.Lector["Stock"];

//                    if (datos.Lector["IdMarca"] != DBNull.Value)
//                    {
//                        producto.Marca = new Marca();
//                        producto.Marca.ID = (int)datos.Lector["IdMarca"];
//                        producto.Marca.Nombre = datos.Lector["Marca"].ToString();
//                    }
//                    if (datos.Lector["IdCategoria"] != DBNull.Value)
//                    {
//                        producto.Categoria = new Categoria();
//                        producto.Categoria.ID = (int)datos.Lector["IdCategoria"];
//                        producto.Categoria.Nombre = datos.Lector["Categoria"].ToString();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                if (producto == null)
//                {
//                    throw new Exception("El producto no se encontró.", ex);
//
//            datos.setearConsulta(@"SELECT TOP 1 A.Nombre, A.Descripcion, 
//                             MIN(I.ImagenUrl) AS ImagenUrl, 
//                             A.Precio
//                             FROM Productos A
//                             LEFT JOIN IMAGENES I ON A.IdProducto = I.IdProducto
//                             GROUP BY A.Nombre, A.Descripcion, A.Precio");
//            datos.ejecutarLectura();

//            if (datos.Lector.Read())
//            {
//                Producto aux = new Producto();
//                aux.Nombre = datos.Lector["Nombre"].ToString();
//                aux.Descripcion = datos.Lector["Descripcion"].ToString();

               
//                if (aux.Imagenes == null)
//                    aux.Imagenes = new List<Imagen>();

//                if (!(datos.Lector["ImagenUrl"] is DBNull))
//                {
//                    aux.Imagenes.Add(new Imagen(datos.Lector["ImagenUrl"].ToString()));
//                }
//                else
//                {
//                    aux.Imagenes.Add(new Imagen("https://media.istockphoto.com/id/1128826884/es/vector/ning%C3%BAn-s%C3%ADmbolo-de-vector-de-imagen-falta-icono-disponible-no-hay-galer%C3%ADa-para-este-momento.jpg?s=612x612&w=0&k=20&c=9vnjI4XI3XQC0VHfuDePO7vNJE7WDM8uzQmZJ1SnQgk="));
//
//                }

//                aux.Precio = (float)(decimal)datos.Lector["Precio"];
//                return aux;
//            }
//            return null;
//        }

        public Producto ObtenerArticuloId(int idArticulo)
        {
            Producto articulo = null;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta((@"SELECT A.IdProducto, A.Nombre, A.Descripcion, A.Precio 
                               FROM Productos A 
                               WHERE A.IdProducto = @Id"));
                datos.SetearParametro("@Id", idArticulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Producto art = new Producto();
                    art.ID= (int)datos.Lector["IdProducto"];
                    art.Nombre = datos.Lector["Nombre"].ToString();
                    art.Descripcion = datos.Lector["Descripcion"].ToString();
                    art.Precio = (float)(decimal)datos.Lector["Precio"];
                    articulo = art;
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
            return articulo;
        }

        public Producto ObtenerIdProducto(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Producto producto = null;
            try
            {
                datos.setearConsulta("SELECT P.IdProducto, P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock, " +
                                     "M.IdMarca, M.Nombre AS Marca, C.IdCategoria, C.Nombre AS Categoria " +
                                     "FROM Productos P " +
                                     "LEFT JOIN Marcas M ON P.IdMarca = M.IdMarca " +
                                     "LEFT JOIN Categorias C ON P.IdCategoria = C.IdCategoria " +
                                     "WHERE P.IdProducto = @IdProducto");
                datos.SetearParametro("@IdProducto", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    producto = new Producto
                    {
                        ID = (int)datos.Lector["IdProducto"],
                        Codigo = datos.Lector["Codigo"].ToString(),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Precio = (float)(decimal)datos.Lector["Precio"],
                        Stock = (int)datos.Lector["Stock"]
                    };

                    if (datos.Lector["IdMarca"] != DBNull.Value)
                    {
                        producto.Marca = new Marca
                        {
                            ID = (int)datos.Lector["IdMarca"],
                            Nombre = datos.Lector["Marca"].ToString()
                        };
                    }

                    if (datos.Lector["IdCategoria"] != DBNull.Value)
                    {
                        producto.Categoria = new Categoria
                        {
                            ID = (int)datos.Lector["IdCategoria"],
                            Nombre = datos.Lector["Categoria"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el producto: " + ex.Message);
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
            return producto;
        }







    }
}
