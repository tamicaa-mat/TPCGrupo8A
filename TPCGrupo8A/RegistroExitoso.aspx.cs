using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCGrupo8A
{
    public partial class RegistroExitoso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string nombreCliente = Request.QueryString["nombre"];
                if (!string.IsNullOrEmpty(nombreCliente))
                {
                    MensajeExito.Text = "¡Gracias, " + nombreCliente + "! Tu registro ha sido completado con éxito.";
                }
            }
        }
    }
}