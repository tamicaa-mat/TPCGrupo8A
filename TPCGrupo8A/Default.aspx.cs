using Datos;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TPCGrupo8A
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
            }

        }


        private void CargarCategorias()
        {
           
            CategoriaNegocio cateNegocio = new CategoriaNegocio();
            AccesoDatos accesoDatos = new AccesoDatos();

            
            List<Categoria> categorias = cateNegocio.listar();

            foreach (var categoria in categorias)
            {
             
                var li = new HtmlGenericControl("li");
                


                var a = new HtmlGenericControl("a");
                a.Attributes["class"] = "dropdown-item";
                a.Attributes["href"] = "#";  
                a.InnerText = categoria.Nombre;  

                // Agregar el <a> dentro del <li>
                li.Controls.Add(a);

                // Agregar el <li> a la lista del menú (control <ul>)
                ulCategorias.Controls.Add(li);  // 'ulCategorias' es el ID del <ul> en el HTML
            }
        }
    }



























}














































