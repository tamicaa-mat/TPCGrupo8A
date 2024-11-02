using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Dominio;
using Negocio;
using System.Diagnostics;

namespace TPCGrupo8A
{
    public partial class Categorias : System.Web.UI.Page
    {
        AccesoDatos datos = new AccesoDatos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();

                //Usuario usuario = Session["usuario"] as Usuario;
                //if (usuario == null || usuario.TipoUsuario != TipoUsuario.Administrador)
                //{
                //    Response.Redirect("~/Default.aspx", false);
                //}  // lo comento para que podamos acceder a las pantallas sin logearnos, ya q solo el admin puede acceder a estas pantallas
            }
        }
        private void CargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            //List<Categoria> categorias = categoriaNegocio.listar();
            try
            {
                GVCategorias.DataSource = categoriaNegocio.listar();
                GVCategorias.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void GVCategorias_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);//obtengo el indice de la fila selleccionada
                GridViewRow row = GVCategorias.Rows[index];

                int idCategoria = Convert.ToInt32(GVCategorias.DataKeys[index].Value);
                hdnCategoriaId.Value = idCategoria.ToString();//guarda el id para luego utilizarlo 

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                Categoria categoria = categoriaNegocio.ObtenerIdCategoria(idCategoria);//obtiene el ID 

                if (categoria != null)
                {
                    txtNombreCategoriaEditar.Text = categoria.Nombre;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModalEditar", "$('#modalEditar').modal('show');", true);//abre el modal
            }
        }
        protected void btnGuardarCategoria_OnClick(object sender, EventArgs e)
        {
            try
            {
                Categoria nuevaCategoria = new Categoria();
                nuevaCategoria.Nombre = txtNombreCategoria.Text;

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                if (categoriaNegocio.ExisteNombreCategoria(nuevaCategoria.Nombre))
                {
                    lblMensaje.Text = "La categoría: " + nuevaCategoria.Nombre + " ya existe";
                    lblMensaje.Visible = true;
                    return;
                    throw new Exception("La categoría: " + nuevaCategoria.Nombre + " ya existe");
                }
                hdnCategoriaId.Value = "";
                categoriaNegocio.agregar(nuevaCategoria);
                CargarCategorias();

                txtNombreCategoria.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "HideModalAgregar", "var modalAgregar = " +
                    "bootstrap.Modal.getInstance(document.getElementById('modalAgregar')); if(modalAgregar) { modalAgregar.hide(); }", true);//Cierra el modal de Agregar
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR no se pudo agregar la categoría", ex);
            }
        }
        protected void btnEditarCategoria_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdnCategoriaId.Value))
                {
                    lblMensaje.Text = "Por favor, selecciona una categoría para editar.";
                    lblMensaje.Visible = true;
                    return;
                }
                int idCategoria = Convert.ToInt32(hdnCategoriaId.Value);

                string nombreCategoria = txtNombreCategoriaEditar.Text.ToString();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                if (categoriaNegocio.ExisteNombreCategoria(nombreCategoria))
                {
                    lblMensaje.Text = "La categoría: " + nombreCategoria + " ya existe";
                    lblMensaje.Visible = true;
                    return;
                    throw new Exception("La categoría: " + nombreCategoria + " ya existe");
                }
                Categoria categoria = new Categoria();
                categoria.ID = idCategoria;
                categoria.Nombre = nombreCategoria;

                categoriaNegocio.editar(categoria);
                CargarCategorias();
                ScriptManager.RegisterStartupScript(this, GetType(), "HideModalEditar", "$('#modalEditar').modal('hide');", true); // Cerrar el modal de Editar
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR no se pudo agregar la categoría", ex);
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdnCategoriaId.Value))
                {
                    lblMensaje.Text = "Por favor, selecciona una categoría para eliminar.";
                    lblMensaje.Visible = true;
                    return;
                }
                int idCategoria = Convert.ToInt32(hdnCategoriaId.Value);//Tengo el ID con el HiddenField
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                categoriaNegocio.eliminar(idCategoria);
                CargarCategorias();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}