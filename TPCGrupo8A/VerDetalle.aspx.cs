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
    public partial class VerDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idPedido = Convert.ToInt32(Request.QueryString["idPedido"]);
            CargarDetallePedido(idPedido);
        }


        private void CargarDetallePedido(int idPedido)
        {
            try
            {
                PedidoNegocio pedidoNegocio = new PedidoNegocio();
                List<DetallePedido> detalles = pedidoNegocio.ObtenerDetallesPedido(idPedido);

                if (detalles.Count > 0)
                {
                    string html = "<h3>Detalles del Pedido</h3><ul>";

                    foreach (var detalle in detalles)
                    {
                        html += $"<li>Producto: {detalle.Nombre} || Cantidad: {detalle.Cantidad} || Precio: {detalle.PrecioUnitario:C} || Total: {detalle.Total:C}</li>";
                    }

                    html += "</ul>";

                    // Mostrar los detalles en la página
                    detallePedido.InnerHtml = html;
                }
                else
                {
                    detallePedido.InnerHtml = "<p>No se encontraron detalles para este pedido.</p>";
                }
            }
            catch (Exception ex)
            {
                detallePedido.InnerHtml = $"<p>Error al cargar los detalles: {ex.Message}</p>";
            }
        }
    }
}