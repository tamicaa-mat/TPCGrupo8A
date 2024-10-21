using Dominio;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{

    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("select idCategoria, Nombre from Categoria");

                datos.setearConsulta("select IdCategoria, Nombre from Categoria");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();

                    categoria.ID = (int)datos.Lector["idCategoria"];

                    categoria.ID = (int)datos.Lector["IdCategoria"];

                    categoria.Nombre = datos.Lector["Nombre"].ToString();

                    lista.Add(categoria);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las categorías", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        /// agregar o eliminar categorias de la bd hay que modificarlo porque las columnas de categorias cambiaron en la bd
        public void agregar(Categoria nuevaCategoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO CATEGORIAS (Descripcion) VALUES (@Descripcion)");
                datos.SetearParametro("@Descripcion", nuevaCategoria.Nombre);
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
                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE id = @id");
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