using Datos;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCGrupo8A
{
    public partial class CarritoPago : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        if (Session["Carrito"] != null)
        //        {
        //            List<int> idProductos = (List<int>)Session["Carrito"];

        //            if (idProductos.Count > 0)
        //            {
        //                // Obtener los detalles de los productos usando los IDs
        //                List<DetallePedido> carrito = new CarritoNegocio().DetallesCarritoIds(idProductos);

        //                // Asignar el carrito al Repeater
        //                RepeaterCarrito.DataSource = carrito;
        //                RepeaterCarrito.DataBind();

        //                // Calcular el total
        //                float totalCarrito = 0;
        //                foreach (var detalle in carrito)
        //                {
        //                    totalCarrito += detalle.Cantidad * detalle.PrecioUnitario;
        //                }

        //                // Mostrar el total
        //                totalCarritoLabel.Text = "$" + totalCarrito.ToString("F2");
        //            }
        //            else
        //            {
        //                totalCarritoLabel.Text = "Carrito vacío";
        //            }
        //        }
        //    }
        //}
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
                            totalCarrito += detalle.Cantidad * detalle.PrecioUnitario;
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
         
            Response.Redirect("Default.aspx",false);
        }

        protected void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                if (validarDatosCliente())
                {
                   
                    cliente.Nombre = txtNombre.Text;
                    cliente.Apellido = txtApellido.Text;
                    cliente.Direccion = txtDireccion.Text;
                    cliente.Email = txtEmail.Text;
                    cliente.Telefono = txtTelefono.Text;
                    cliente.TipoUsuario = 0;

                  
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    usuarioNegocio.RegistroCliente(cliente);

                    // Redirigir a la página de éxito
                    Response.Redirect("RegistroExitoso.aspx?nombre=" + Server.UrlEncode(cliente.Nombre), false);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error en el proceso de confirmación: {ex.Message}");
               
                // Response.Redirect("Error.aspx");
            }
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


        public bool validarDatosCliente()
        {
            string mensajeErrorNombre = "";
            string mensajeErrorApellido = "";
            string mensajeErrorEmail = "";
            string mensajeErrorTelefono = "";
            bool hayErrores = false;

            // Validación campo APELLIDO
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                mensajeErrorApellido = "El campo APELLIDO no puede estar vacío.";
                hayErrores = true;
            }
            else if (esNumero(txtApellido.Text))
            {
                mensajeErrorApellido = "APELLIDO debe contener solo LETRAS.";
                hayErrores = true;
            }

            // Validación campo NOMBRE
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                mensajeErrorNombre = "El campo NOMBRE no puede estar vacío.";
                hayErrores = true;
            }
            else if (esNumero(txtNombre.Text))
            {
                mensajeErrorNombre = "NOMBRE debe contener solo LETRAS.";
                hayErrores = true;
            }

            // Validación campo EMAIL
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                mensajeErrorEmail = "El campo EMAIL no puede estar vacío.";
                hayErrores = true;
            }
            else if (!esEmailValido(txtEmail.Text))
            {
                mensajeErrorEmail = "El campo EMAIL no es válido.";
                hayErrores = true;
            }

            // Validación campo TELEFONO
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                mensajeErrorTelefono = "El campo TELEFONO no puede estar vacío.";
                hayErrores = true;
            }
            else if (!(esNumero(txtTelefono.Text)))
            {
                mensajeErrorTelefono = "El campo TELEFONO debe contener solo NÚMEROS.";
                hayErrores = true;
            }

            if (hayErrores)
            {
                if (!string.IsNullOrEmpty(mensajeErrorNombre))
                    txtNombre.Attributes["placeholder"] = mensajeErrorNombre;
                if (!string.IsNullOrEmpty(mensajeErrorApellido))
                    txtApellido.Attributes["placeholder"] = mensajeErrorApellido;
                if (!string.IsNullOrEmpty(mensajeErrorEmail))
                    txtEmail.Attributes["placeholder"] = mensajeErrorEmail;
                if (!string.IsNullOrEmpty(mensajeErrorTelefono))
                    txtTelefono.Attributes["placeholder"] = mensajeErrorTelefono;

                txtNombre.ForeColor = System.Drawing.Color.Red;
                txtApellido.ForeColor = System.Drawing.Color.Red;
                txtEmail.ForeColor = System.Drawing.Color.Red;
                txtTelefono.ForeColor = System.Drawing.Color.Red;

                return false;
            }

            // Si no hay errores
            return true;
        }


    }








}
