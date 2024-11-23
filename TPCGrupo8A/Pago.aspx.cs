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


                        lblTotal.Text = "$" + totalCarrito.ToString("F2");
                    }
                    else
                    {
                        lblTotal.Text = "Carrito vacío";
                    }
                }
            }
        }









    }
}