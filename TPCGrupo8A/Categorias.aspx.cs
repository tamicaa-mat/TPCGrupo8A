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
            Categoria nuevaCategoria = new Categoria();
            nuevaCategoria.Nombre = txtNombreCategoria.Text;
            try
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                if (!(categoriaNegocio.ExisteNombreCategoria(nuevaCategoria.Nombre)))
                {
                    categoriaNegocio.agregar(nuevaCategoria);
                    CargarCategorias();
                    lblErrorCategoria.Visible = false;
                }
                else
                {
                    lblErrorCategoria.Text = $"La categoría '{nuevaCategoria.Nombre}' ya existe, agregue otra distinta";
                    lblErrorCategoria.Visible = true;
                }
                    txtNombreCategoria.Text = "";
            }
            catch (Exception ex)                                           
            {
                throw new Exception($"La categoría '{nuevaCategoria.Nombre}' ya existe, agregue otra distinta", ex);
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