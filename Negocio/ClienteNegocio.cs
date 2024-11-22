using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ClienteNegocio
    {
        public void RegistrarComoCliente(int idUsuario, string apellido, string nombre, string direccion, string telefono)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("EXEC RegistrarComoCliente @IdUsuario, @Apellido, @Nombre, @Direccion, @Telefono");

                datos.SetearParametro("@IdUsuario", idUsuario);
                datos.SetearParametro("@Apellido", apellido);
                datos.SetearParametro("@Nombre", nombre);
                datos.SetearParametro("@Direccion", direccion);
                datos.SetearParametro("@Telefono", telefono);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el cliente: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
