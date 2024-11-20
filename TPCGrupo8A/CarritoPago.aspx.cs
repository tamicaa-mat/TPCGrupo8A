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
            // Verificar si el usuario ha iniciado sesión 20/11
            Usuario usuario = Session["Usuario"] != null ?  (Usuario)Session["usuario"] : null ;


            if (!(usuario!= null && usuario.ID != 0))
            {
                // Redirigir al inicio de sesión
                Response.Redirect("IniciarSesion.aspx",false);
            }

            if (!IsPostBack)
            {
                if (Session["Carrito"] != null)
                {
                    List<int> idProductos = (List<int>)Session["Carrito"];

                    if (idProductos.Count > 0)
                    {
                        // obtener los detalles de los productos usando los ID
                        List<DetallePedido> carrito = new CarritoNegocio().DetallesCarritoIds(idProductos);

                        // asignar el carrito al Repeater
                        RepeaterCarrito.DataSource = carrito;
                        RepeaterCarrito.DataBind();

                        // calcular el total
                        float totalCarrito = 0;
                        foreach (var detalle in carrito)
                        {
                            totalCarrito += detalle.Cantidad * detalle.PrecioUnitario;
                        }

                        // mostrar total
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

    }
}
