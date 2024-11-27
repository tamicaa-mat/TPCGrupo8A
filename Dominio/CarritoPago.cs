using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CarritoPago
    {

        public int IdCarrito { get; set; }  // Identificador del carrito
        public string EmailCliente { get; set; }  // Correo electrónico del cliente
        public List<DetalleCarritoPago> Detalles { get; set; }  // Lista de detalles del carrito (productos en el carrito)
        public float Total { get; set; }  // Total calculado del carrito
        public DateTime FechaCreacion { get; set; }  // Fecha de creación del carrito

        public CarritoPago()
        {
            Detalles = new List<DetalleCarritoPago>();
            FechaCreacion = DateTime.Now;
        }

        // Método para calcular el total del carrito
        public void CalcularTotal()
        {
            Total = Detalles.Sum(d => d.Subtotal);
        }

    }

}

