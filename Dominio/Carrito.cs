using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Carrito
    {
        public int ID { get; set; }
        public Usuario Usuario { get; set; }
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
        public float Total
        {
            get
            {
                float total = 0;
                foreach (var detalle in Detalles)
                {
                    total += detalle.Total;
                }
                return total;
            }
        }
    }
}
