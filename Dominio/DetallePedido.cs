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
        public string ImagenUrl { get; set; }
        public List<Imagen> Imagenes { get; set; } = new List<Imagen>();
        public string Nombre { get; set; }

        public int  Stock { get; set; }
        public float Total
        {
            get
            {
                return (float)PrecioUnitario * (float)Cantidad;
            }
        }
        public DetallePedido() { }
        public DetallePedido(Producto produco, int cantidad, float precioUnitario, string imagenUrl) 
        { 
            Producto = produco;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            ImagenUrl = imagenUrl;
        }
    }
}
