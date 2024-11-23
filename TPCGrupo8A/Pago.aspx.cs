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
                    lblTotal.Text = $"Total: ${carrito.Total:F2}";
                }
                else
                {
                    // Mostrar un mensaje si no hay carrito
                   // lblError.Text = "No hay productos en el carrito.";
                   // lblError.Visible = true;
                }
            }





        }


    }



}