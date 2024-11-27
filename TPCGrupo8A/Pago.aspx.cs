using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
               
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorSesion", "alert('No se encontró al usuario en la sesión.');", true);
                    return;
                }

                string email = usuario.Email;

               
                List<int> idProductos = (List<int>)Session["Carrito"];
                if (idProductos == null || idProductos.Count == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "CarritoVacio", "alert('El carrito está vacío o no se encontró en la sesión.');", true);
                    return;
                }

                
                if (!validarCajasTexto())
                {
                    //Response.Redirect("Error.aspx",false);
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorValidacion", "alert('Hay errores en el formulario. Por favor, corríjalos e intente nuevamente.');", true);
                    return;
                }

                // Si los campos son válidos, se actualizan los datos del usuario
                usuario.Nombre = Request.Form["Nombre"];
                usuario.Apellido = Request.Form["Apellido"];
                usuario.Email = Request.Form["Email"];
                usuario.Contrasenia = Request.Form["Contrasenia"];
                usuario.TipoUsuario = 0;

                // Obtener los detalles del carrito
                List<DetallePedido> carritoDetalles = new CarritoNegocio().DetallesCarritoIds(idProductos);

                PedidoNegocio pedidoNegocio = new PedidoNegocio();

                // Registrar el pedido
                pedidoNegocio.RegistroPedido(carritoDetalles, email);

                // Mostrar un mensaje de éxito
                ClientScript.RegisterStartupScript(this.GetType(), "PedidoRegistrado", "alert('Compra finalizada con éxito.');", true);
                Response.Redirect("~/Perfil.aspx");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error: {ex.Message}");
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "alert('Ocurrió un error al finalizar la compra: " + ex.Message + "');", true);
            }
        }






        protected void btnVolver2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CarritoPago.aspx", false);

        }

        //validaciones cajas de txt pago 27/11

        public bool validarCajasTexto()
        {
            string mensajeErrorNombre = "";
            string mensajeErrorApellido = "";
            string mensajeErrorTelefono = "";
            string mensajeErrorDireccion = "";
            bool hayErroresNombre = false;
            bool hayErroresApellido = false;
            bool hayErroresTelefono = false;
            bool hayErroresDireccion = false;

            // Validación del campo NOMBRE
            string nombre = Request.Form["nombre"].Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                mensajeErrorNombre = "El campo NOMBRE no puede estar vacío.";
                hayErroresNombre = true;
            }
            else if (!esSoloLetras(nombre))
            {
                mensajeErrorNombre = "NOMBRE debe contener solo LETRAS.";
                hayErroresNombre = true;
            }

            // Validación del campo APELLIDO
            string apellido = Request.Form["apellido"].Trim();
            if (string.IsNullOrEmpty(apellido))
            {
                mensajeErrorApellido = "El campo APELLIDO no puede estar vacío.";
                hayErroresApellido = true;
            }
            else if (!esSoloLetras(apellido))
            {
                mensajeErrorApellido = "APELLIDO debe contener solo LETRAS.";
                hayErroresApellido = true;
            }

            // Validación del campo TELEFONO
            string telefono = Request.Form["telefono"].Trim();
            if (string.IsNullOrEmpty(telefono))
            {
                mensajeErrorTelefono = "El campo TELEFONO no puede estar vacío.";
                hayErroresTelefono = true;
            }
            else if (!esNumero(telefono))
            {
                mensajeErrorTelefono = "TELEFONO debe contener solo NUMEROS.";
                hayErroresTelefono = true;
            }

            // Validación del campo DIRECCION
            string direccion = Request.Form["direccion"].Trim();
            if (string.IsNullOrEmpty(direccion))
            {
                mensajeErrorDireccion = "El campo DIRECCION no puede estar vacío.";
                hayErroresDireccion = true;
            }
           

            // Mostrar mensajes de error y retornar false si hay errores
            if (hayErroresNombre)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorNombre", $"alert('{mensajeErrorNombre}');", true);
                return false;
            }

            if (hayErroresApellido)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorApellido", $"alert('{mensajeErrorApellido}');", true);
                return false;
            }

            if (hayErroresTelefono)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorTelefono", $"alert('{mensajeErrorTelefono}');", true);
                return false;
            }

            if (hayErroresDireccion)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorDireccion", $"alert('{mensajeErrorDireccion}');", true);
                return false;
            }

            hayErroresNombre = false;
            hayErroresApellido = false;
            hayErroresTelefono = false;
            hayErroresDireccion = false;
            return true;
        }

        // Función para validar si el texto contiene solo letras
        bool esSoloLetras(string texto)
        {
            return texto.All(c => char.IsLetter(c)); // Verifica si todos los caracteres son letras
        }

        // Función para validar si es un número
        bool esNumero(string texto)
        {
            long numero;
            return long.TryParse(texto, out numero);
        }









    }






}