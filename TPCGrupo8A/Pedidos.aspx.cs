using Datos;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TPCGrupo8A
{
    public partial class Pedidos : System.Web.UI.Page
    {
        PedidoNegocio Pedido = new PedidoNegocio(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPedidos();
            }



        }

        private void CargarPedidos(string estadoFiltro = "")
        {
            try
            {
                // Obtener pedidos desde la base de datos
                DataTable pedidos = Pedido.ObtenerPedidos(estadoFiltro);

                // Verificar si hay resultados
                if (pedidos.Rows.Count > 0)
                {
                    RepeaterPedidos.DataSource = pedidos;
                    RepeaterPedidos.DataBind();
                }
                else
                {
                    
                    RepeaterPedidos.DataSource = null;
                    RepeaterPedidos.DataBind();
                }
            }
            catch (Exception ex)
            {
                
                RepeaterPedidos.DataSource = null;
                RepeaterPedidos.DataBind();
                Console.WriteLine($"Error al cargar pedidos: {ex.Message}");
            }
        }

        protected void BtnFiltrarPedidos_Click(object sender, EventArgs e)
        {
           
            string estadoSeleccionado = DropDownEstado.SelectedValue;

            
            CargarPedidos(estadoSeleccionado);
        }

        protected void BtnCambiarEstado_Click(object sender, EventArgs e)
        {
            try
            {
               
                Button btn = (Button)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;

                int idPedido = Convert.ToInt32(((Label)item.FindControl("lblNumeroPedido")).Text);
                Console.WriteLine($"IdPedido obtenido: {idPedido}");

                string nuevoEstado = btn.Text.Contains("Entregado") ? "Entregado" : "Pendiente";
                Console.WriteLine($"Estado a aplicar: {nuevoEstado}");

                PedidoNegocio pedidoNegocio = new PedidoNegocio();

                pedidoNegocio.CambiarEstadoAPedido(idPedido, nuevoEstado);

                CargarPedidos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar el estado: {ex.Message}");
            }
        }

    }
}