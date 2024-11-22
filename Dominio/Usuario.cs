using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dominio
{
    public enum TipoUsuario
    {
        Cliente = 0,
        Administrador = 1,
    }

    public class Usuario
    {

        public int idUsuario { get; set; }  // Aquí lo defines
        public int ID { get; set; }
        //public string DNI { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public string Direccion {  get; set; }
        public string Telefono { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
       
        public Usuario() 
        { 
        
        }
        public Usuario(string email,string contrasenia, string Direccion, string apellido, string nombre, DateTime fechaNacimiento, bool administrador) 
        {
            Email = email;
            Apellido = apellido;
            Nombre = nombre;
            FechaNacimiento = fechaNacimiento;
            Contrasenia = contrasenia;
            TipoUsuario = TipoUsuario.Cliente;
        }
    }
}
