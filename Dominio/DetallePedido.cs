using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetallePedido
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public Imagen ImagenUrl { get; set; }
        public string Nombre { get; set; }

        public int  Stock { get; set; }
        public float Total
        {
            get
            {
                return PrecioUnitario * Cantidad;
            }
        }
        public DetallePedido() { }
        public DetallePedido(Producto produco, int cantidad, float precioUnitario) 
        { 
            Producto = produco;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
    }
}
