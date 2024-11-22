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
        public int tipoUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (!(usuario == null))
            {
                tipoUsuario = (int)usuario.TipoUsuario;
            }
            if (!IsPostBack) 
            {
                CargarProductos();
                //CargarProductosAleatorios();
                if (Request.QueryString["categoriaId"] != null)
                {
                    int categoriaId = int.Parse(Request.QueryString["categoriaId"]);
                    CargarProductosPorCategoria(categoriaId);
                }
                else if (Request.QueryString["marcaId"] != null)
                {
                    int marcaId = int.Parse(Request.QueryString["marcaId"]);
                    CargarProductosPorMarca(marcaId);///
                }
            }
        }
        private void CargarProductos()
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Producto> listaArticulos = productoNegocio.Listar();
            rptProductos.DataSource = listaArticulos;
            rptProductos.DataBind();
        }
        private void CargarProductosPorCategoria(int categoriaId)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT P.IdProducto, P.Nombre, P.Descripcion, P.Precio, I.ImagenUrl
                                     FROM Productos P
                                     LEFT JOIN (SELECT IdProducto, ImagenUrl
                                         FROM Imagenes
                                         WHERE IdImagen IN (SELECT MIN(IdImagen)
                                             FROM Imagenes
                                             GROUP BY IdProducto)) I ON P.IdProducto = I.IdProducto
                                     WHERE P.IdCategoria = @IdCategoria AND P.Estado = 1;");

                datos.SetearParametro("@IdCategoria", categoriaId);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto
                    {
                        ID = (int)datos.Lector["IdProducto"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Precio = (float)(decimal)datos.Lector["Precio"]
                    };
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        producto.Imagenes.Add(new Imagen(datos.Lector["ImagenUrl"].ToString()));
                    }
                    else
                    {
                        producto.Imagenes.Add(new Imagen("https://path/to/default/image.jpg"));
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
                datos.setearConsulta(@"SELECT P.IdProducto, P.Nombre, P.Descripcion, P.Precio, I.ImagenUrl
                                    FROM Productos P
                                    LEFT JOIN (SELECT IdProducto, ImagenUrl
                                        FROM Imagenes
                                        WHERE IdImagen IN (SELECT MIN(IdImagen)
                                            FROM Imagenes
                                            GROUP BY IdProducto)) I ON P.IdProducto = I.IdProducto
                                    WHERE P.IdMarca = @IdMarca AND P.Estado = 1;");

                datos.SetearParametro("@IdMarca", marcaId);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto
                    {
                        ID = (int)datos.Lector["IdProducto"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Precio = (float)(decimal)datos.Lector["Precio"]
                    };
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        producto.Imagenes.Add(new Imagen(datos.Lector["ImagenUrl"].ToString()));
                    }
                    else
                    {
                        producto.Imagenes.Add(new Imagen("https://path/to/default/image.jpg"));
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
        /*____________ ABM ____________*/
        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ProductoNegocio productoNegocio = new ProductoNegocio();
                int idProducto = Convert.ToInt32(e.CommandArgument);
                productoNegocio.eliminar(idProducto);
                CargarProductos();
                SiteMaster siteMaster = (SiteMaster)this.Master;
                siteMaster.CargarCategorias(); 
                siteMaster.CargarMarcas();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnEditar_Command(object obj, CommandEventArgs e)
        {
            try
            {
                if(e.CommandName != null)
                {
                    int idProducto = Convert.ToInt32(e.CommandArgument);
                    Session["ID"] = idProducto;
                    Response.Redirect("FormularioProductosAM.aspx", false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            string btn = ((LinkButton)sender).CommandArgument;
            int idSeleccionado = int.Parse(btn);
            Session["ID"] = idSeleccionado;
            Response.Redirect("DetalleProducto.aspx", false);
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            EmailServices emailServices = new EmailServices();

            // Usa el correo de contacto como remitente y el correo destino es el fijo
            string correoContacto = txtCorreoContacto.Text;
            string nombre = txtNombreContacto.Text;
            string mensaje = $"Mensaje de {nombre}\nCelular: {txtCelularContacto.Text}\n\nMensaje:\n{txtMensajeContacto.Text}";

            // Envía el correo al destinatario fijo
            emailServices.armarCorreo("pruebaproyectocuatrimestral@gmail.com", "Contacto", mensaje, correoContacto);
            try
            {
                emailServices.enviarEmail();
                // Mensaje de éxito
                lblMensaje.Text = "¡Tu mensaje ha sido enviado con éxito!";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception)
            {
                // Mensaje de error
                lblMensaje.Text = "Hubo un error al enviar tu mensaje. Por favor intenta de nuevo.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
            lblMensaje.Visible = true;
        }


    }
}
