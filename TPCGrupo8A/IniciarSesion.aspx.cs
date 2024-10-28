using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCGrupo8A
{
    public partial class IniciarSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnButtonIngresarOnClick(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            try
            {
                Usuario usuario = new Usuario(txtemail.Text, txtpassword.Text, false);
                if (usuarioNegocio.IniciarSesion(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("~/Default.aspx", false);
                    
                }
                else
                {
                    Session.Add("Error", "user o pass incorrectos");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}