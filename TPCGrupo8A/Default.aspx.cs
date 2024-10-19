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
            // Instancia de la clase que accede a las categorías
            CategoriaNegocio cateNegocio = new CategoriaNegocio();
            AccesoDatos accesoDatos = new AccesoDatos();

            // Obtener la lista de categorías
            List<Categoria> categorias = cateNegocio.listar();

            // Iterar sobre las categorías y generar los elementos del menú
            foreach (var categoria in categorias)
            {
                // Crear el elemento <li>
                var li = new HtmlGenericControl("li");
                


                // Crear el elemento <a> dentro del <li>
                var a = new HtmlGenericControl("a");
                a.Attributes["class"] = "dropdown-item";
                a.Attributes["href"] = "#";  // Puedes enlazar a una URL específica si lo necesitas
                a.InnerText = categoria.Nombre;  // Nombre de la categoría

                // Agregar el <a> dentro del <li>
                li.Controls.Add(a);

                // Agregar el <li> a la lista del menú (control <ul>)
                ulCategorias.Controls.Add(li);  // 'ulCategorias' es el ID del <ul> en el HTML
            }
        }
    }



























}














































