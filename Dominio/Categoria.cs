
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public Categoria() { }
        public Categoria(int id, string nombre)
        {
            ID = id;
            Nombre = nombre;
        }

        public Categoria(string nombre)
        {
            Nombre = nombre;
        }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
