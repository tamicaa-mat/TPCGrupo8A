using Datos;
using Dominio;
using Microsoft.SqlServer.Server;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCGrupo8A
{
    public partial class CarritoPago : System.Web.UI.Page
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

        protected void btnSeleccionarOtro_Click(object sender, EventArgs e)
        {

            Response.Redirect("Default.aspx", false);
        }


        //protected void btnConfirmarPago_Click(object sender, EventArgs e)
        //{
        //    // Lista para guardar los productos seleccionados
        //    List<Producto> productosCarrito = new List<Producto>();
        //    string email = txtEmail.Text;

        //    foreach (RepeaterItem item in RepeaterCarrito.Items)
        //    {
        //        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
        //        {
        //            // Encontrar los controles en el Repeater
        //            Label lblNombre = item.FindControl("lblNombre") as Label;
        //            Debug.WriteLine("NOMBRE " + lblNombre);
        //            Label lblPrecio = item.FindControl("lblPrecio") as Label;
        //            Debug.WriteLine("PRECIO " + lblPrecio);
        //            TextBox txtCantidad = item.FindControl("Cantidad") as TextBox;
        //            Debug.WriteLine("CANTIDAD " + txtCantidad);

        //            if (txtCantidad != null)
        //            {
        //                string cantidadLimpia = txtCantidad.Text.Trim();
        //                int cantidad;
        //                if (int.TryParse(cantidadLimpia, out cantidad) && cantidad > 0)
        //                {
        //                    Producto producto = new Producto
        //                    {
        //                        Nombre = lblNombre.Text,
        //                        Precio = float.Parse(lblPrecio.Text.Replace("$", "").Replace(",", "").Trim()),
        //                        Cantidad = cantidad
        //                    };

        //                    productosCarrito.Add(producto);

        //                    Response.Redirect("Pago.aspx", false);
        //                }
        //                else
        //                {

        //                }
        //            }
        //        }

        //    }

        //    try
        //    {
        //        UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        //        ClienteNegocio clienteNegocio = new ClienteNegocio();

        //        int idUsuario = usuarioNegocio.ObtenerIdUsuarioPorEmail(email);

        //        clienteNegocio.RegistrarComoCliente(
        //            idUsuario,
        //            txtApellido.Text,
        //            txtNombre.Text,
        //            txtDireccion.Text,
        //            txtTelefono.Text
        //        );

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    PedidoNegocio pedidoNegocio = new PedidoNegocio();
        //    pedidoNegocio.RegistroPedido(productosCarrito, email);
        //}

        //protected void btnConfirmarPago_Click(object sender, EventArgs e)
        //{
        //    Carrito carrito = new Carrito();
        //    // Crear una lista tipada para el carrito

        //    List<Producto> productosCarrito = new List<Producto>();
        //    Producto producto=new Producto();

        //    foreach (RepeaterItem item in RepeaterCarrito.Items)
        //    {
        //        // Buscar controles dentro del Repeater
        //        var lblNombre = (Label)item.FindControl("lblNombre");
        //        var txtCantidad = (TextBox)item.FindControl("txtCantidad");
        //        var lblPrecio = (Label)item.FindControl("lblPrecio");

        //        // Extraer los valores
        //        string nombre = lblNombre.Text;
        //        int cantidad = int.Parse(txtCantidad.Text);
        //        float precio = float.Parse(lblPrecio.Text.Replace("Precio: $", "").Trim());

        //        producto.Nombre = nombre;
        //        producto.Cantidad = cantidad;
        //        producto.Precio = precio;


        //        // Agregar a la lista
        //        productosCarrito.Add(producto);

        //    }

        //    Session["Carrito"] = carrito;
        //    // Guardar la lista tipada en la sesión

        //    // Redirigir a la página de pago
        //    Response.Redirect("Pago.aspx");



        //}
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

        protected void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            // Crear un objeto carrito
            Carrito carrito = new Carrito();

            // Crear los detalles del carrito
            foreach (RepeaterItem item in RepeaterCarrito.Items)
            {
                // Buscar controles dentro del Repeater
                var lblNombre = (Label)item.FindControl("lblNombre");
                var txtCantidad = (TextBox)item.FindControl("txtCantidad");
                var lblPrecio = (Label)item.FindControl("lblPrecio");

                // Extraer los valores
                string nombre = lblNombre.Text;
                int cantidad = int.Parse(txtCantidad.Text);
                float precioUnitario = float.Parse(lblPrecio.Text.Replace("Precio: $", "").Trim());

                // Crear un nuevo DetallePedido
                DetallePedido detalle = new DetallePedido
                {
                    Nombre = nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = precioUnitario,
                };

                // Agregar el detalle al carrito
                carrito.Detalles.Add(detalle);
            }

            // Guardar el carrito en la sesión
            Session["Carrito"] = carrito;

            // Redirigir a la página de pago
            Response.Redirect("Pago.aspx");
        }







        // PARA LAS CAJAS DE cliente VALIDACIONES

        bool esNumero(string texto)
            {
                long numero;
                return long.TryParse(texto, out numero);
            }
            // validacion formato email
            private bool esEmailValido(string email)
            {
                string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, patronEmail);
            }


            //public bool ValidarDatosUsuario()
            //{
            //    string mensajeErrorNombre = "";
            //    string mensajeErrorApellido = "";
            //    string mensajeErrorEmail = "";
            //    string mensajeErrorTelefono = "";
            //    bool hayErrores = false;

            //    // Validación campo APELLIDO
            //    if (string.IsNullOrEmpty(txtApellido.Text))
            //    {
            //        mensajeErrorApellido = "El campo APELLIDO no puede estar vacío.";
            //        hayErrores = true;
            //    }
            //    else if (esNumero(txtApellido.Text))
            //    {
            //        mensajeErrorApellido = "APELLIDO debe contener solo LETRAS.";
            //        hayErrores = true;
            //    }

            //    // Validación campo NOMBRE
            //    if (string.IsNullOrEmpty(txtNombre.Text))
            //    {
            //        mensajeErrorNombre = "El campo NOMBRE no puede estar vacío.";
            //        hayErrores = true;
            //    }
            //    else if (esNumero(txtNombre.Text))
            //    {
            //        mensajeErrorNombre = "NOMBRE debe contener solo LETRAS.";
            //        hayErrores = true;
            //    }

            //    // Validación campo EMAIL
            //    if (string.IsNullOrEmpty(txtEmail.Text))
            //    {
            //        mensajeErrorEmail = "El campo EMAIL no puede estar vacío.";
            //        hayErrores = true;
            //    }
            //    else if (!esEmailValido(txtEmail.Text))
            //    {
            //        mensajeErrorEmail = "El campo EMAIL no es válido.";
            //        hayErrores = true;
            //    }

            //    // Validación campo TELEFONO
            //    if (string.IsNullOrEmpty(txtTelefono.Text))
            //    {
            //        mensajeErrorTelefono = "El campo TELEFONO no puede estar vacío.";
            //        hayErrores = true;
            //    }
            //    else if (!(esNumero(txtTelefono.Text)))
            //    {
            //        mensajeErrorTelefono = "El campo TELEFONO debe contener solo NÚMEROS.";
            //        hayErrores = true;
            //    }

            //    if (hayErrores)
            //    {
            //        if (!string.IsNullOrEmpty(mensajeErrorNombre))
            //            txtNombre.Attributes["placeholder"] = mensajeErrorNombre;
            //        if (!string.IsNullOrEmpty(mensajeErrorApellido))
            //            txtApellido.Attributes["placeholder"] = mensajeErrorApellido;
            //        if (!string.IsNullOrEmpty(mensajeErrorEmail))
            //            txtEmail.Attributes["placeholder"] = mensajeErrorEmail;
            //        if (!string.IsNullOrEmpty(mensajeErrorTelefono))
            //            txtTelefono.Attributes["placeholder"] = mensajeErrorTelefono;

            //        txtNombre.ForeColor = System.Drawing.Color.Red;
            //        txtApellido.ForeColor = System.Drawing.Color.Red;
            //        txtEmail.ForeColor = System.Drawing.Color.Red;
            //        txtTelefono.ForeColor = System.Drawing.Color.Red;

            //        return false;
            //    }

            //    // Si no hay errores
            //    return true;
            //}


        



    }




}
