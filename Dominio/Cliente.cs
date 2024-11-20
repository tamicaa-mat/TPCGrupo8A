using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Cliente:Usuario
    {
       // public int IDCLIENTE { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public string Direccion {  get; set; }

        public string Telefono { get; set; }
      

        //public List<Direccion> Direcciones { get; set; }
        //public Cliente()
        //{
        //    Direcciones = new List<Direccion>();
        //    Pedidos = new List<Pedido>();
        //}

        //// para agregar direcciones
        //public void AgregarDireccion(Direccion direccion)
        //{
        //    if (direccion != null)
        //    {
        //        Direcciones.Add(direccion);
        //    }
        //}

        //  para agregar un pedidos
        public void AgregarPedido(Pedido pedido)
        {
            if (pedido != null)
            {
                Pedidos.Add(pedido);
            }
        }




    }
}
