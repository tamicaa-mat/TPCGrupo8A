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
                if (Session["Carrito"] != null)
                {
                    List<int> idProductos = (List<int>)Session["Carrito"];

                    if (idProductos.Count > 0)
                    {
                        // Obtener los detalles de los productos usando los IDs
                        List<DetallePedido> carrito = new CarritoNegocio().DetallesCarritoIds(idProductos);

                        // Asignar el carrito al Repeater
                        RepeaterCarrito.DataSource = carrito;
                        RepeaterCarrito.DataBind();

                        // Calcular el total
                        float totalCarrito = 0;
                        foreach (var detalle in carrito)
                        {
                            totalCarrito += detalle.Cantidad * detalle.PrecioUnitario;
                        }

                        // Mostrar el total
                        totalCarritoLabel.Text = "$" + totalCarrito.ToString("F2");
                    }
                    else
                    {
                        totalCarritoLabel.Text = "Carrito vacío";
                    }
                }
            }
        }
    }
}
