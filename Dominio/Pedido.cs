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
        public List<Pedido> Pedidos { get; set; }
        public DetallePedido Detalle { get; set; } 
        public EstadoPedido Estado {  get; set; } //
        public string MetodoPago { get; set; }// a coordinar con el vendedor, transferencia o mp

    }
}
