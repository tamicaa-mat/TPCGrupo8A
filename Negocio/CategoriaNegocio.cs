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
            AccesoDatos datos = new AccesoDatos();
            List<Categoria> lista = new List<Categoria>();
            try
            {
                datos.setearConsulta("select IdCategoria, Nombre ,Estado from Categorias");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Estado = (bool)datos.Lector["Estado"];
                    categoria.ID = (int)datos.Lector["IdCategoria"];
                    categoria.Nombre = datos.Lector["Nombre"].ToString();

                    if (categoria.Estado == true)
                    {

                    lista.Add(categoria);

                    }
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
        public void agregar(Categoria nuevaCategoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Categorias (Nombre, Estado) VALUES (@Nombre,'1')");
                datos.SetearParametro("@Nombre", nuevaCategoria.Nombre);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la categoría. Verifique los datos e intente nuevamente.", ex);
           
            }
            finally

            {
                datos.cerrarConexion();
            }
        }



        public void editar(Categoria categoria)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Categorias SET Nombre = @Nombre WHERE IdCategoria = @IdCategoria");
                datos.SetearParametro("@Nombre", categoria.Nombre);
                datos.SetearParametro("@IdCategoria", categoria.ID);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR al editar la categoria:" + ex.Message, ex);
            }
        }
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
              
                datos.setearConsulta("EXEC SP_EliminacionLogicaCategorias @IDCATEGORIA");
                datos.SetearParametro("@IDCATEGORIA", id);
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

        public bool ExisteNombreCategoria(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            bool existeNombre = false;

            try
            {
                datos.setearConsulta("SELECT Nombre FROM Categorias WHERE UPPER(Nombre) = UPPER(@Nombre)");
                datos.SetearParametro("@Nombre", nombre);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    existeNombre = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR al agregar categoría", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
            return existeNombre;
        }
        public Categoria ObtenerIdCategoria(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Categoria categoria = null;
            datos.setearConsulta("SELECT IdCategoria, Nombre FROM Categorias WHERE IdCategoria = @id");
            datos.SetearParametro("@Id", id);
            datos.ejecutarLectura();

            if (datos.Lector.Read())
            {
                categoria = new Categoria();
                categoria.ID = (int)datos.Lector["IdCategoria"];
                categoria.Nombre = datos.Lector["Nombre"].ToString();
            }
            return categoria;
        }
    }
}