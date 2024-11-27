using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PedidoPago
    {
        public int IdPedido { get; set; }  // ID del pedido
        public string EmailCliente { get; set; }  // Correo electrónico del cliente
        public DateTime FechaPedido { get; set; }  // Fecha del pedido
        public float TotalPedido { get; set; }  // Total del pedido
        public List<DetallePedidoPago> Detalles { get; set; }  // Lista de detalles del pedido

        // Constructor sin total
        public PedidoPago(int idPedido, string emailCliente, DateTime fechaPedido, float totalPedido)
        {
            IdPedido = idPedido;
            EmailCliente = emailCliente;
            FechaPedido = fechaPedido;
            TotalPedido = totalPedido;
            Detalles = new List<DetallePedidoPago>();
        }

        // Método para agregar un detalle de pedido
        public void AgregarDetalle(DetallePedidoPago detalle)
        {
            Detalles.Add(detalle);
        }

        // Método para calcular el total del pedido
        public void CalcularTotal()
        {
            TotalPedido = Detalles.Sum(d => d.Subtotal);
        }
    }

}
