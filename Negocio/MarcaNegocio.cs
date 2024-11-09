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

                datos.setearConsulta("select idMarca, Estado, Nombre from Marcas");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.Estado = (bool)datos.Lector["Estado"];
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
                datos.setearConsulta("INSERT INTO Marcas (Nombre, Estado) VALUES (@Nombre,'1')");
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
                datos.setearConsulta("UPDATE Marcas SET Nombre = @Nombre, Estado = @Estado WHERE IdMarca = @IdMarca");
                datos.SetearParametro("@Nombre", marca.Nombre);
                datos.SetearParametro("@IdMarca", marca.ID);
                datos.SetearParametro("@Estado", marca.Estado);

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
                // Llama al procedimiento almacenado en lugar de la sentencia DELETE
                datos.setearConsulta("EXEC SP_EliminacionLogicaMarcas @IDMARCA = @IdMarca");
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
       
    }
}

