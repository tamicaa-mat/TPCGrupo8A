using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Direccion
    {
        public Cliente Cliente { get; set; }
        public string CodigoPostal { get; set; } 
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }
    }
}
