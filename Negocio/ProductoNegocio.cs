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
        public void agregar(Producto nuevaProducto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Productos (Nombre) VALUES (@Nombre)");
                datos.SetearParametro("@Nombre", nuevaProducto.Nombre);
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
                datos.setearConsulta("UPDATE Productos SET Nombre = @Nombre WHERE IdProducto = @IdProducto");
                datos.SetearParametro("@Nombre", producto.Nombre);
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
                datos.setearConsulta("EXEC EliminacionLogicaProducto @idProducto = @idProducto");
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
                datos.setearConsulta("SELECT Nombre FROM Productos WHERE UPPER(Nombre) = UPPER(@Nombre)");
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
        public Producto ObtenerIdProducto(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Producto producto = null;
            datos.setearConsulta("SELECT IdProducto, Nombre FROM Productos WHERE IdProducto = @id");
            datos.SetearParametro("@Id", id);
            datos.ejecutarLectura();

            if (datos.Lector.Read())
            {
                producto = new Producto();
                producto.ID = (int)datos.Lector["IdProducto"];
                producto.Nombre = datos.Lector["Nombre"].ToString();
            }
            return producto;
        }
    }
}
