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
                CargarProductosAleatorios();
            }
        }

        // obtener productos aleatorios para la pag default
        public List<Producto> ObtenerProductosAleatorios(int cantidad)
        {
            List<Producto> productos = new List<Producto>();
           
            string query = @"
        SELECT TOP(@Cantidad) p.*, i.ImagenUrl 
        FROM Productos p
        LEFT JOIN Imagenes i ON p.IdProducto = i.IdProducto 
        ORDER BY NEWID();"; 

            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.setearConsulta(query);
                accesoDatos.SetearParametro("@Cantidad", cantidad);
                accesoDatos.ejecutarLectura(); 

                while (accesoDatos.Lector.Read())
                {
                    Producto producto = new Producto
                    {
                        ID = Convert.ToInt32(accesoDatos.Lector["IdProducto"]),
                        Nombre = accesoDatos.Lector["Nombre"].ToString(),
                        Imagen = accesoDatos.Lector["ImagenUrl"] != DBNull.Value
                                 ? new Imagen { ImagenUrl = accesoDatos.Lector["ImagenUrl"].ToString() }
                                 : null
                    };
                    productos.Add(producto);
                }
            }
            catch (Exception ex)
            {
               
                throw; 
            }
            finally
            {
                accesoDatos.cerrarConexion(); 
            }

            return productos;
        }


        private void CargarProductosAleatorios()
        {
            var productos = ObtenerProductosAleatorios(3); 
            rptProductos.DataSource = productos;
            rptProductos.DataBind();
        }

    }
}
