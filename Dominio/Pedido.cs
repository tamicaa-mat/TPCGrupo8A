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
        public DateTime Fecha { get; set; }
        public Cliente Cliente { get; set; }  // Asociación con la clase Cliente
        public DetallePedido Detalle { get; set; }  // Asociado con clase DetallePedido
        public EstadoPedido Estado {  get; set; }
        public string MetodoPago { get; set; }// hay que definir como queda esto

    }
}
