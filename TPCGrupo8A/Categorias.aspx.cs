using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Dominio;
using Negocio;

namespace TPCGrupo8A
{
    public partial class Categorias : System.Web.UI.Page
    {
        AccesoDatos datos;
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
            List<Categoria> categorias = categoriaNegocio.listar();
            try
            {
                RptCategorias.DataSource = categorias;
                RptCategorias.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnGuardarCategoria_OnClick(object sender, EventArgs e)
        {
            Categoria categoria = new Categoria();
            try
            {
                Categoria nuevaCategoria = new Categoria();
                nuevaCategoria.Nombre = txtNombreCategoria.Text;

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                categoriaNegocio.agregar(nuevaCategoria);

                CargarCategorias();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAgregar_OnClick(object sender, EventArgs e)
        {

        }   
        protected void btnEditar_OnClick(Object sender, EventArgs e)
        {

        }
        protected void btnBorrar_OnClick(Object sender, EventArgs e)
        {

        }
    }
}