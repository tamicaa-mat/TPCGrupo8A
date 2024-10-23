using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCGrupo8A
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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