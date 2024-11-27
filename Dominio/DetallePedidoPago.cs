using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetallePedidoPago
    {

        public int IdDetallePedido { get; set; }  // ID del detalle del pedido
        public int IdPedido { get; set; }  // Clave foránea que hace referencia al pedido
        public int IdProducto { get; set; }  // ID del producto
        public string Nombre { get; set; }  // Nombre del producto
        public int Cantidad { get; set; }  // Cantidad del producto
        public float PrecioUnitario { get; set; }  // Precio unitario del producto
        public float Subtotal => Cantidad * PrecioUnitario;  // Subtotal calculado automáticamente

        public DetallePedidoPago(int idPedido, int idProducto, string nombre, int cantidad, float precioUnitario)
        {
            IdPedido = idPedido;
            IdProducto = idProducto;
            Nombre = nombre;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }

    }
}
