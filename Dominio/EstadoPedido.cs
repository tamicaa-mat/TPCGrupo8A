using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EstadoPedido
    {
        public int ID { get; set; }
        public Pedido Pedido { get; set; } // ASOCIADO CON EL NUMERO DE PEDIDO
        public string nombre { get; set; }
    }
}
