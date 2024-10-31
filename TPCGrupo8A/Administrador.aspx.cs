using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCGrupo8A
{
    public partial class Administrasdor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = Session["usuario"] as Usuario;
                if (usuario == null || usuario.TipoUsuario != TipoUsuario.Administrador)
                {
                    Response.Redirect("~/Default.aspx", false);
                }
            }
        }
    }
}