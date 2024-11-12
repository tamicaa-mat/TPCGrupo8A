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
            try
            {
                // Cargar todas las categorías (sin filtrar por estado)
                GVCategorias.DataSource = categoriaNegocio.listar();
                GVCategorias.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
            var siteMaster = (SiteMaster)this.Master;
            if(siteMaster != null)
            {
                siteMaster.CargarCategorias();
                siteMaster.CargarMarcas();
            }
        }
        protected void GVCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtener el valor de Estado de la categoría actual
                var estado = DataBinder.Eval(e.Row.DataItem, "Estado");

               
                if (estado != null && !(bool)estado) 
                {
                   
                    var btnHabilitar = (Button)e.Row.Cells[3].Controls[0];
                    btnHabilitar.Visible = true; // Mostrar el botón "Habilitar"
                }
                else
                {
                    
                    var btnHabilitar = (Button)e.Row.Cells[3].Controls[0];
                    btnHabilitar.Visible = false;
                }
            }
        }
        protected void btnGuardarCategoria_OnClick(object sender, EventArgs e)
        {
            try
            {
                lblExito.Visible = false;
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
                lblExito.Text = "CATEGORIA AGREGADA ÉXITOSAMENTE";
                lblExito.Visible = true;
                lblExito.ForeColor = System.Drawing.Color.Green;
                
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
                lblExito.Visible = false;
                lblMensaje.Visible = false;
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
        protected void btnEliminar_OnClick(object sender, EventArgs e)
        {
            try
            {
                lblExito.Visible = false;
                lblMensaje.Visible = false;
                if (string.IsNullOrEmpty(hdnCategoriaId.Value))
                {
                    lblMensaje.Text = "Por favor, selecciona una categoría para eliminar.";
                    lblMensaje.Visible = true;
                    return;
                }
                int idCategoria = Convert.ToInt32(hdnCategoriaId.Value); // Obtener el ID con el HiddenField
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                categoriaNegocio.eliminar(idCategoria); // Eliminar la categoría 
                CargarCategorias(); // Recargar el GridView
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GVCategorias_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVCategorias.Rows[rowIndex];
                int categoriaId = Convert.ToInt32(GVCategorias.DataKeys[rowIndex].Value);

                hdnCategoriaId.Value = categoriaId.ToString(); // Asigna el ID seleccionado al HiddenField
                txtNombreCategoriaEditar.Text = row.Cells[1].Text; // Muestra el nombre de la categoría seleccionada
            }
            else if (e.CommandName == "Habilitar")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVCategorias.Rows[rowIndex];
                int categoriaId = Convert.ToInt32(GVCategorias.DataKeys[rowIndex].Value);

               
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                var categoria = categoriaNegocio.ObtenerIdCategoria(categoriaId);
                if (categoria != null && !categoria.Estado)
                {
                    categoria.Estado = true;
                    categoriaNegocio.editar(categoria); 
                    CargarCategorias(); 
                }
            }
            var siteMaster = (SiteMaster)this.Master;
            if (siteMaster != null)
            {
                siteMaster.CargarCategorias();
                siteMaster.CargarMarcas();
            }
        }
    }
}