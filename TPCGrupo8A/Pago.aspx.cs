using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TPCGrupo8A
{
    public partial class Pago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Seguridad.SesionActiva(Session["Usuario"])))
            {
                //Redirigir al inicio de sesión
                Response.Redirect("IniciarSesion.aspx", false);
            }


            if (!IsPostBack)
            {
                // Verificar si hay datos en la sesión
                if (Session["Carrito"] != null)
                {
                    // Obtener el carrito desde la sesión
                    var carrito = (Carrito)Session["Carrito"];

                    // Enlazar los detalles del carrito al Repeater
                    RepeaterCarrito.DataSource = carrito.Detalles;
                    RepeaterCarrito.DataBind();

                    // Calcular y mostrar el total
                    float total = carrito.Detalles.Sum(d => d.Total);  // Suponiendo que 'Total' ya está calculado en el objeto 'Detalle'
                    lblTotal.Text = $"${total:F2}";
                }
                else
                {
                    // Mostrar un mensaje si no hay carrito
                   // lblError.Text = "No hay productos en el carrito.";
                   // lblError.Visible = true;
                }
            }
        }

        protected void btn_finalizarCompra(object sender, EventArgs e)
        {
            try
            {
                // Obtener el carrito de la sesión
                if (Session["Carrito"] != null)
                {
                    Carrito carrito = (Carrito)Session["Carrito"]; // Asegúrate de que la sesión contiene un objeto de tipo Carrito
                                                                   //List<Producto> productosCarrito = carrito.Detalles.Select(d => d.Producto).ToList();
                    List<Producto> productosCarrito = new List<Producto>();
                    // Obtener el email del usuario desde la sesión
                    Usuario usuario = (Usuario)Session["Usuario"]; // Suponiendo que el usuario está almacenado en la sesión
                    string email = usuario.Email;

                    // Crear una instancia de la clase que contiene el método RegistroPedido
                    PedidoNegocio pedidoNegocio = new PedidoNegocio();

                    // Registrar el pedido
                    pedidoNegocio.RegistroPedido(productosCarrito, email);

                    // Mostrar un mensaje de éxito o redirigir a otra página
                    ClientScript.RegisterStartupScript(this.GetType(), "PedidoRegistrado", "alert('Compra finalizada con éxito.');", true);
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    // Manejo si el carrito está vacío
                    ClientScript.RegisterStartupScript(this.GetType(), "CarritoVacio", "alert('El carrito está vacío.');", true);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error: {ex.Message}");
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "alert('Ocurrió un error al finalizar la compra.');", true);
            }



        }

    }
}