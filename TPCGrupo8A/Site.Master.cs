using Datos;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TPCGrupo8A
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarMarcas();
                Usuario usuario = Session["usuario"] as Usuario;
                if (usuario == null)
                {
                    IngresarBoton.Visible = true;
                    linkAdmin.Visible = false;
                    CerrarSesion.Visible = false;
                }
                else
                {
                    IngresarBoton.Visible = false;
                    CerrarSesion.Visible = true;
                    //si el usuario es administrador muestra el link a administrador y si es cliente no
                    linkAdmin.Visible = usuario.TipoUsuario == TipoUsuario.Administrador;
                }
            MiCuentaMenu.Visible = Session["usuario"] != null; 
            }
        }

        protected void CerrarSesion_OnClick(object sender, EventArgs e)
        {
            Session.Abandon();
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                HttpCookie cookie = new HttpCookie("ASP.NET_SessionId");
                cookie.Expires = DateTime.Now;
                Response.Cookies.Add(cookie);
            }
            Response.Redirect("~/Default.aspx", false);
        }
        public void CargarCategorias()
        {
            ulCategorias.Controls.Clear();
            CategoriaNegocio cateNegocio = new CategoriaNegocio();
            AccesoDatos accesoDatos = new AccesoDatos();


            List<Categoria> categorias = cateNegocio.listar();

            foreach (var categoria in categorias)
            {
                var li = new HtmlGenericControl("li");

                var a = new HtmlGenericControl("a");
                a.Attributes["class"] = "dropdown-item";
                a.Attributes["href"] = $"Default.aspx?categoriaId={categoria.ID}";
                a.InnerText = categoria.Nombre;

                // Agregar el <a> dentro del <li>
                li.Controls.Add(a);

                // Agregar el <li> a la lista del menú (control <ul>)
                ulCategorias.Controls.Add(li);  // 'ulCategorias' es el ID del <ul> en el HTML
            }
        }

        public void CargarMarcas()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            AccesoDatos accesoDatos = new AccesoDatos();

            List<Marca> marcas = marcaNegocio.listar();

            foreach (var marca in marcas)
            {
                var li = new HtmlGenericControl("li");

                var a = new HtmlGenericControl("a");
                a.Attributes["class"] = "dropdown-item";
                // Redirigir a la página de productos pasando el ID de la marca
                a.Attributes["href"] = $"Default.aspx?marcaId={marca.ID}";
                a.InnerText = marca.Nombre;

                if (marca.Estado == true)
                {

                    // Agregar el <a> dentro del <li>
                    li.Controls.Add(a);
                    // Agregar el <li> a la lista del menú (control <ul>)
                    ulMarcas.Controls.Add(li);  // 'ulMarcas' es el ID del <ul> en el HTML

                }
             



            }
        }
    }
}