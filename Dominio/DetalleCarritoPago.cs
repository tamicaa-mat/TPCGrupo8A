using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleCarritoPago
    {
        public int IdProducto { get; set; }  // ID del producto
        public string Nombre { get; set; }  // Nombre del producto
        public int Cantidad { get; set; }  // Cantidad seleccionada en el carrito
        public float PrecioUnitario { get; set; }  // Precio unitario del producto
        public float Subtotal => Cantidad * PrecioUnitario;  // Subtotal calculado automáticamente

        public DetalleCarritoPago(int idProducto, string nombre, int cantidad, float precioUnitario)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
    }
}
