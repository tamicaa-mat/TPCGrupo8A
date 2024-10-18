using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Stock
    {
        public string Estado { get; set; }
        public int Cantidad {  get; set; }
        public Producto Producto { get; set; }
    }
}
