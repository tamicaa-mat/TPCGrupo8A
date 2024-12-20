﻿using Dominio;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
                    categoria.Nombre = datos.Lector["Nombre"].ToString();
                    categoria.Estado = (bool)datos.Lector["Estado"];
                    categoria.ID = (int)datos.Lector["IdCategoria"];

                    lista.Add(categoria);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar las categorías: {ex.Message}", ex);
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
                if (string.IsNullOrWhiteSpace(nuevaCategoria.Nombre))
                {
                    throw new Exception("El nombre de la categoría no puede estar vacío.");
                }

                datos.setearConsulta("INSERT INTO Categorias (Nombre, Estado) VALUES (@Nombre,'1')");
                datos.SetearParametro("@Nombre", nuevaCategoria.Nombre);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar la categoría: {ex.Message}", ex);
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
                if (string.IsNullOrWhiteSpace(categoria.Nombre))
                {
                    throw new Exception("El nombre de la categoría no puede estar vacío.");
                }

                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Categorias SET Nombre = @Nombre, Estado = @Estado WHERE IdCategoria = @IdCategoria");
                datos.SetearParametro("@Nombre", categoria.Nombre);
                datos.SetearParametro("@IdCategoria", categoria.ID);
                datos.SetearParametro("@Estado", categoria.Estado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR al editar la categoría: {ex.Message}", ex);
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("EXEC SP_EliminacionLogicaCategorias @IDCATEGORIA= @IdCategoria");
                datos.SetearParametro("@IdCategoria", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la categoría: {ex.Message}", ex);
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
                throw new Exception($"ERROR al verificar la existencia de la categoría: {ex.Message}", ex);
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
            try
            {
                datos.setearConsulta("SELECT IdCategoria, Nombre FROM Categorias WHERE IdCategoria = @id");
                datos.SetearParametro("@Id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    categoria = new Categoria();
                    categoria.ID = (int)datos.Lector["IdCategoria"];
                    categoria.Nombre = datos.Lector["Nombre"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la categoría: {ex.Message}", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
            return categoria;
        }
    }
}