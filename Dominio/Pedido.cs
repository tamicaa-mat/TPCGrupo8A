using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedido
    {
        public int ID { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime Fecha { get; set; }
        public Cliente Cliente { get; set; }  
        public List<DetallePedido> Detalles { get; set; } 
        public EstadoPedido Estado {  get; set; } 
        public MetodoPago MetodoPago { get; set; }
    }
}
