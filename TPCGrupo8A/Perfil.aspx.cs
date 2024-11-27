using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Negocio;

namespace TPCGrupo8A
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Session.Add("Error", "Necesitas logearte ");
                Response.Redirect("~/IniciarSesion", false);
            }
            else if (!IsPostBack)
            {
                CargarPerfil();
            }
        }

        //aca para cargar los demas datos en el perfil de usuario
        //protected void CargarPerfil()
        //{
        //    Usuario usuario = (Usuario)Session["usuario"];
        //    TextEmail.Text = usuario.Email;
        //    TextApellido.Text = usuario.Apellido;
        //    TextNombre.Text = usuario.Nombre;
        //    TextDireccion.Text = usuario.Direccion;

        //    //TextFechaNacimiento.Text = usuario.FechaNacimiento.GetValueOrDefault(usuario.FechaNacimient);

        //}

        private void CargarPerfil()
        {
            // Obtener el usuario de la sesión
            Usuario usuario = (Usuario)Session["usuario"];

          
            if (usuario != null)
            {
               
               // Response.Write($"Nombre: {usuario.Nombre}, Apellido: {usuario.Apellido}");
                TextEmail.Text = usuario.Email;
                TextApellido.Text = usuario.Apellido;
                TextNombre.Text = usuario.Nombre;
                TextDireccion.Text = usuario.Direccion;
            }

            else
            {

                Session.Add("Error", "Necesitas logearte ");
                Response.Redirect("~/IniciarSesion", false);
            }
        }





        protected void btnCambiarContraseniaOnClick(object sender, EventArgs e)
        {
            CambiarContrasenia.Visible = !CambiarContrasenia.Visible;
        }
        protected void btnGuardarNuevaContOnClick(object sender, EventArgs e)
        {
            CambiarContrasenia.Visible = !CambiarContrasenia.Visible;
        }
        protected void btnGuardarPerfilOnClick(object sender, EventArgs e)
        {

        }
    }
}