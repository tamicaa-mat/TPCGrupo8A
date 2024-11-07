using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> listar(int idProducto)
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdImagen, IdProducto, ImagenUrl FROM Imagenes WHERE IdProducto = @IdProducto");
                datos.SetearParametro("@IdProducto", idProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen imagen = new Imagen();
                    imagen.Id = (int)datos.Lector["IdImagen"];
                    imagen.IdProducto = (int)datos.Lector["IdProducto"];
                    imagen.ImagenUrl= datos.Lector["ImagenUrl"].ToString();

                    lista.Add(imagen);
                }
                return lista;
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
        public void agregar(Imagen nuevaImagen)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Imagenes(IdProducto, ImagenUrl) VALUES (@IdProducto, @ImagenUrl)");
                datos.SetearParametro("@idProducto", nuevaImagen.IdProducto);
                datos.SetearParametro("@ImagenUrl", nuevaImagen.ImagenUrl);
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
                datos.setearConsulta("DELETE FROM Imagenes WHERE IdImagen = @IdImagen");
                datos.SetearParametro("@IdImagen", id);
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
        //public List<Imagen> imagenesxProducto(int idProducto)
        //{
        //    List<Imagen> imagenes = new List<Imagen>();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("SELECT IdImagen, IdProducto, ImagenUrl FROM Imagenes WHERE IdProducto = @IdProducto");
        //        datos.SetearParametro("@IdProducto", idProducto);
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Imagen imagen = new Imagen();
        //            imagen.Id = (int)datos.Lector["IdImagen"];
        //            imagen.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
        //            imagenes.Add(imagen);
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
        //    return imagenes;
        //}


        //public List<Imagen> imagenesxArticulo(int idArticulo)
        //{
        //    List<Imagen> imagenes = new List<Imagen>();
        //    AccesoDatos datos = new AccesoDatos();

        //    try
        //    {
        //        datos.setearConsulta("SELECT IdProducto, ImagenUrl FROM IMAGENES WHERE IdProducto = @IdProducto");
        //        datos.SetearParametro("@IdProducto", idArticulo);
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Imagen imagen = new Imagen();
        //            // Asegúrate de que el campo que estás leyendo existe en la consulta
        //            imagen.Id = (int)datos.Lector["IdProducto"]; // Cambiado de "IdImagen" a "IdProducto"
        //            imagen.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
        //            imagenes.Add(imagen);
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
        //    return imagenes;
        //}



        public List<Imagen> imagenesxProducto(int idProducto)
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdImagen, IdProducto, ImagenUrl FROM Imagenes WHERE IdProducto = @IdProducto");
                datos.SetearParametro("@IdProducto", idProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen imagen = new Imagen();
                    imagen.Id = (int)datos.Lector["IdImagen"];
                    imagen.IdProducto = (int)datos.Lector["IdProducto"];
                    imagen.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    imagenes.Add(imagen);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
            return imagenes;
        }


























    }
}
