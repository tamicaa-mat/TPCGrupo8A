using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Datos
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        private SqlTransaction transaccion;
        public SqlDataReader Lector
        {
            get { return lector; }
        }
        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=TPCGRUPO8A; integrated security=true");
            comando = new SqlCommand();
        }

        // Método para iniciar una transacción
        public void iniciarTransaccion()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();

            transaccion = conexion.BeginTransaction();
            comando.Transaction = transaccion; // Asocia la transacción al comando
        }

        // Método para confirmar la transacción
        public void commitTransaccion()
        {
            transaccion?.Commit();
            transaccion = null;
        }

        // Método para revertir la transacción
        public void rollbackTransaccion()
        {
            transaccion?.Rollback();
            transaccion = null;
        }



        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ejecutarLectura2()
        {
            comando.Connection = conexion;
            DataTable tabla = new DataTable();

            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
                tabla.Load(lector); // Cargar los datos del lector en un DataTable
                return tabla;       // Devolver el DataTable con los resultados
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la lectura: " + ex.Message);
            }
            finally
            {
                if (lector != null)
                    lector.Close(); // Asegurarse de cerrar el lector
                conexion.Close();   // Cerrar la conexión
            }
        }
        public void ejecutarAccion()
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object ejecutarScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return comando.ExecuteScalar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
        //public void cerrarConexion()
        //{
        //    if (lector != null)
        //        lector.Close();
        //    conexion.Close();
        //}
        public void cerrarConexion()
        {
            if (lector != null && !lector.IsClosed)
            {
                lector.Close(); // Cierra el lector si está abierto
            }

            if (conexion != null && conexion.State == ConnectionState.Open)
            {
                conexion.Close(); // Cierra la conexión si está abierta
            }
        }



        public void SetearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
        public int ObtenerIdProducto(string codigo) //Permite obtener el nuevo codigo id de articulo
        {
            int id = 0;
            try
            {
                setearConsulta("SELECT IdProducto FROM Productos WHERE Codigo = @Codigo");
                SetearParametro("@Codigo", codigo);
                ejecutarLectura();
                if (Lector.Read())
                {
                    id = (int)(Lector["IdProducto"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cerrarConexion();
            }
            return id;
        }

        public int ejecutarAccion2()
        {
            try
            {
                // Abre la conexión si no está abierta
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }

                // Ejecuta la consulta y devuelve el número de filas afectadas
                return comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al ejecutar la consulta: {ex.Message}");
                throw; // Propaga la excepción si es necesario
            }
            finally
            {
                // Asegúrate de cerrar la conexión
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }

                // Limpiar los parámetros para evitar problemas de referencia
                comando.Parameters.Clear();
            }
        }


    }

}







    //..................................................validar.........................................................

//public bool ExisteCodigoArticulo(string codigo)
//{
//    AccesoDatos datos = new AccesoDatos();
//    bool existe = false;

//    try
//    {

//        datos.setearConsulta("SELECT Codigo FROM Productos WHERE Codigo = @codigo");
//        datos.comando.Parameters.Clear();
//        datos.comando.Parameters.AddWithValue("@codigo", codigo);

//        datos.ejecutarLectura();


//        if (datos.Lector.Read())
//        {
//            existe = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//    finally
//    {
//        datos.cerrarConexion();
//    }

//    return existe;
//}

//public bool ExisteNombreMarca(string nombreMarca)
//{
//    AccesoDatos datos = new AccesoDatos();
//    bool existeNombre = false;

//    try
//    {

//        datos.setearConsulta("SELECT Descripcion FROM MARCAS WHERE Descripcion = @descripcion");
//        datos.comando.Parameters.Clear();
//        datos.comando.Parameters.AddWithValue("@descripcion", nombreMarca);

//        datos.ejecutarLectura();


//        if (datos.Lector.Read())
//        {
//            existeNombre = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//    finally
//    {
//        datos.cerrarConexion();
//    }

//    return existeNombre;


//}

//public bool ExisteIDmarca(int codMarca)
//{
//    AccesoDatos datos = new AccesoDatos();
//    bool existeIDmarca = false;

//    try
//    {

//        datos.setearConsulta("SELECT Id FROM MARCAS WHERE Id = @Id");
//        datos.comando.Parameters.Clear();
//        datos.comando.Parameters.AddWithValue("@Id", codMarca);

//        datos.ejecutarLectura();


//        if (datos.Lector.Read())
//        {
//            existeIDmarca = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//    finally
//    {
//        datos.cerrarConexion();
//    }

//    return existeIDmarca;


//}

//public bool ExisteNombreCategoria(string nombreCAT)
//{
//    AccesoDatos datos = new AccesoDatos();
//    bool existeNombreCat = false;

//    try
//    {

//        datos.setearConsulta("SELECT Descripcion FROM CATEGORIAS WHERE Descripcion = @descripcion");
//        datos.comando.Parameters.Clear();
//        datos.comando.Parameters.AddWithValue("@descripcion", nombreCAT);

//        datos.ejecutarLectura();


//        if (datos.Lector.Read())
//        {
//            existeNombreCat = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//    finally
//    {
//        datos.cerrarConexion();
//    }

//    return existeNombreCat;




//}
//public bool ExisteIDcategoria(int codCate)
//{

//    AccesoDatos datos = new AccesoDatos();
//    bool existeIDcategoria = false;

//    try
//    {

//        datos.setearConsulta("SELECT Id FROM CATEGORIAS WHERE Id = @Id");
//        datos.comando.Parameters.Clear();
//        datos.comando.Parameters.AddWithValue("@Id", codCate);

//        datos.ejecutarLectura();


//        if (datos.Lector.Read())
//        {
//            existeIDcategoria = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//    finally
//    {
//        datos.cerrarConexion();
//    }

//    return existeIDcategoria;

//}

