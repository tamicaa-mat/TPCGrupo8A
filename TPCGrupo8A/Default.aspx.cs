using Datos;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TPCGrupo8A
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                
                if (Request.QueryString["categoriaId"] != null)
                {
                    int categoriaId = int.Parse(Request.QueryString["categoriaId"]);
                    CargarProductosPorCategoria(categoriaId);
                }
                else if (Request.QueryString["marcaId"] != null)
                {
                    int marcaId = int.Parse(Request.QueryString["marcaId"]);
                    CargarProductosPorMarca(marcaId);
                }


            }
        }

       

        private void CargarProductosPorCategoria(int categoriaId)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT p.IdProducto, p.Nombre, p.Descripcion, p.Precio, i.ImagenUrl" +
                    " FROM Productos p" +
                    " LEFT JOIN Imagenes i ON p.IdProducto = i.IdProducto" +
                    " WHERE p.IdCategoria = @IdCategoria");

                datos.SetearParametro("@IdCategoria", categoriaId);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto
                    {
                        ID = (int)datos.Lector["IdProducto"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Precio = (float)(decimal)datos.Lector["Precio"],
                        Imagen = new Imagen()
                    };

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        producto.Imagen.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    }
                    else
                    {
                        producto.Imagen.ImagenUrl = "https://path/to/default/image.jpg";
                    }

                    listaProductos.Add(producto);
                }

                rptProductos.DataSource = listaProductos;
                rptProductos.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar productos por categoría", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void CargarProductosPorMarca(int marcaId)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT p.IdProducto, p.Nombre, p.Descripcion, p.Precio, i.ImagenUrl" +
                    " FROM Productos p" +
                    " LEFT JOIN Imagenes i ON p.IdProducto = i.IdProducto" +
                    " WHERE p.IdMarca = @IdMarca");

                datos.SetearParametro("@IdMarca", marcaId);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto
                    {
                        ID = (int)datos.Lector["IdProducto"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Precio = (float)(decimal)datos.Lector["Precio"],
                        Imagen = new Imagen()
                    };

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        producto.Imagen.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    }
                    else
                    {
                        producto.Imagen.ImagenUrl = "https://path/to/default/image.jpg";
                    }

                    listaProductos.Add(producto);
                }

                rptProductos.DataSource = listaProductos;
                rptProductos.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar productos por marca", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }























    }
}
