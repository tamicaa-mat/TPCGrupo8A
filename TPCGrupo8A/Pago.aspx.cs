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
                if (Session["Carrito"] != null)
                {
                    List<int> idProductos = (List<int>)Session["Carrito"];

                    if (idProductos.Count > 0)
                    {

                        List<DetallePedido> carrito = new CarritoNegocio().DetallesCarritoIds(idProductos);


                        RepeaterCarrito.DataSource = carrito;
                        RepeaterCarrito.DataBind();



                        float totalCarrito = 0;
                        foreach (var detalle in carrito)
                        {

                            {
                                totalCarrito += detalle.Cantidad * detalle.PrecioUnitario;

                            }
                        }


                      totalCarritoLabel.Text = "$" + totalCarrito.ToString("F2");

                    }
                    else
                    {
                        totalCarritoLabel.Text = "Carrito vacío";
                    }
                }
            }




        }
           

        protected void ActualizarTotal(object sender, EventArgs e)//--Función para actualizar el total del carrito tomando por repeater el precio del producto y la cantidad
        {
            float totalCarrito = 0;

            foreach (RepeaterItem item in RepeaterCarrito.Items)
            {
                TextBox txtCantidad = (TextBox)item.FindControl("txtCantidad");
                Label lblPrecio = (Label)item.FindControl("lblPrecio");

                if (txtCantidad != null && lblPrecio != null)
                {
                    if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0) //--si la cant es valida prosigue
                    {
                        string precioTexto = lblPrecio.Text.Replace("Precio: $", "").Trim();
                        if (float.TryParse(precioTexto, out float precioUnitario))
                        {
                            totalCarrito += precioUnitario * cantidad;
                        }
                        else
                        {
                            lblPrecio.Text = "Precio erroneo";
                            return;
                        }
                    }
                    else
                    {
                        txtCantidad.Text = "1";
                    }
                }
                else
                {
                    Response.Redirect("Error.aspx", false);
                }
            }
            totalCarritoLabel.Text = $"Total: ${totalCarrito:F2}";//--Actualiza el total de todo el carrito
        }



        protected void btn_finalizarCompra(object sender, EventArgs e)
        {
          
        
            try
            {
                // Verificar si el usuario está en la sesión
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorSesion", "alert('No se encontró al usuario en la sesión.');", true);
                    return;
                }

                string email = usuario.Email;

                // Verificar si el carrito existe y tiene productos
                List<int> idProductos = (List<int>)Session["Carrito"];
                if (idProductos == null || idProductos.Count == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "CarritoVacio", "alert('El carrito está vacío o no se encontró en la sesión.');", true);
                    return;
                }

                // Aquí va el resto de la lógica para procesar la compra
                List<DetallePedido> carritoDetalles = new CarritoNegocio().DetallesCarritoIds(idProductos);

                PedidoNegocio pedidoNegocio = new PedidoNegocio();

                // Registrar el pedido
                pedidoNegocio.RegistroPedido(carritoDetalles, email);

                // Mostrar un mensaje de éxito o redirigir a otra página
                ClientScript.RegisterStartupScript(this.GetType(), "PedidoRegistrado", "alert('Compra finalizada con éxito.');", true);
                 Response.Redirect("~/Perfil.aspx");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error: {ex.Message}");
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "alert('Ocurrió un error al finalizar la compra. " + ex.Message + "');", true);
            }
        


        }
        protected void btnVolver2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CarritoPago.aspx", false);

        }















    }






}