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

        public void agregar(Marca nuevaMarca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Marcas (Nombre) VALUES (@Nombre)");
                datos.SetearParametro("@Nombre", nuevaMarca.Nombre);
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
        public void editar(Marca marca)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Marcas SET Nombre = @Nombre WHERE IdMarca = @IdMarca");
                datos.SetearParametro("@Nombre", marca.Nombre);
                datos.SetearParametro("@IdMarca", marca.ID);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR al editar la marca:" + ex.Message, ex);
            }
        }
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Marcas WHERE IdMarca = @IdMarca");
                datos.SetearParametro("@IdMarca", id);
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
        public bool ExisteNombreMarca(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            bool existeNombre = false;

            try
            {
                datos.setearConsulta("SELECT Nombre FROM Marcas WHERE UPPER(Nombre) = UPPER(@Nombre)");
                datos.SetearParametro("@Nombre", nombre);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    existeNombre = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR al agregar marca", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
            return existeNombre;
        }
        public Marca ObtenerIdMarca(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Marca marca= null;
            datos.setearConsulta("SELECT IdMarca, Nombre FROM Marcas WHERE IdMarca = @id");
            datos.SetearParametro("@Id", id);
            datos.ejecutarLectura();

            if (datos.Lector.Read())
            {
                marca = new Marca();
                marca.ID = (int)datos.Lector["IdMarca"];
                marca.Nombre = datos.Lector["Nombre"].ToString();
            }
            return marca;
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

