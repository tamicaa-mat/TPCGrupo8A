using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {
        public int ID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Categoria Categoria { get; set; }
        public Marca Marca { get; set; }
        public float Precio { get; set; }
        public List<Imagen> Imagenes { get; set; }
        public string Color { get; set; }
        public string Talle { get; set; }
    }
}
