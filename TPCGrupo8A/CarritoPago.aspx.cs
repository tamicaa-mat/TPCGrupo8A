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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdCliente"] != null)
                {
                    int idCliente = (int)Session["IdCliente"];
                    //List<int> carritoIds = (List<int>)Session["Carrito"];
                    CarritoNegocio negocio = new CarritoNegocio();
                    List<DetallePedido> detallesCarrito = negocio.ListarCarritoCliente(idCliente);

                    RepeaterCarrito.DataSource = detallesCarrito;
                    RepeaterCarrito.DataBind();

                    float totalCarrito = detallesCarrito.Sum(detalle => detalle.Cantidad * detalle.PrecioUnitario);
                    totalCarritoLabel.Text = "$" + totalCarrito.ToString("F2");
                }
            }
        }
        
    }
}