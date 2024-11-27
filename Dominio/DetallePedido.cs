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
        public int idProducto { get; set; }
        public int Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public string ImagenUrl { get; set; }
        public List<Imagen> Imagenes { get; set; } = new List<Imagen>();
        public string Nombre { get; set; }

        public int  Stock { get; set; }
        public float Total
        {
            get
            {
                // Verificar si PrecioUnitario o Cantidad son válidos
                if (PrecioUnitario > 0 && Cantidad > 0)
                {
                    return PrecioUnitario * Cantidad;
                }
                return 0; // O el valor predeterminado que consideres
            }
            set { }
        }

        public List<DetallePedido> detallePedidos { get; set; }
        public DetallePedido() { }
        //public DetallePedido(Producto producto, int cantidad, float precioUnitario, string imagenUrl,float total)
        //{
        //    Producto = producto;

        //    Cantidad = cantidad;
        //    PrecioUnitario = precioUnitario;
        //    ImagenUrl = imagenUrl;
        //    Total = total;
        //}
    }
}
