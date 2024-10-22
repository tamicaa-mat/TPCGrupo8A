using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("select idMarca, Nombre from Marcas");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();

                    marca.ID = (int)datos.Lector["idMarca"];
                    marca.Nombre = datos.Lector["Nombre"].ToString();

                    lista.Add(marca);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las marcas", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Categoria nuevaMarca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Marcas (Descripcion) VALUES (@Descripcion)");
                datos.SetearParametro("@Descripcion", nuevaMarca.Nombre);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Categorias WHERE id = @id");
                datos.SetearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

