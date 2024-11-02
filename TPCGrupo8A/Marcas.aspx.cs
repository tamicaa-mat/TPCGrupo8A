using Datos;
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
    public partial class Marcas : System.Web.UI.Page
    {
        AccesoDatos datos = new AccesoDatos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMarcas();

                //Usuario usuario = Session["usuario"] as Usuario;
                //if (usuario == null || usuario.TipoUsuario != TipoUsuario.Administrador)
                //{
                //    Response.Redirect("~/Default.aspx", false);
                //}  // lo comento para que podamos acceder a las pantallas sin logearnos, ya q solo el admin puede acceder a estas pantallas
            }
        }
        private void CargarMarcas()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            //List<marca> marca= marcaNegocio.listar();
            try
            {
                GVMarca.DataSource = marcaNegocio.listar();
                GVMarca.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void GVMarca_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);//obtengo el indice de la fila selleccionada
                GridViewRow row = GVMarca.Rows[index];

                int idMarca = Convert.ToInt32(GVMarca.DataKeys[index].Value);
                hdnMarcaId.Value = idMarca.ToString();//guarda el id para luego utilizarlo 

                MarcaNegocio marcaNegocio = new MarcaNegocio();
                Marca marca = marcaNegocio.ObtenerIdMarca(idMarca);//obtiene el ID 

                if (marca != null)
                {
                    txtNombreMarcaEditar.Text = marca.Nombre;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModalEditar", "$('#modalEditar').modal('show');", true);//abre el modal
            }
        }
        protected void btnGuardarMarca_OnClick(object sender, EventArgs e)
        {
            try
            {
                Marca nuevaMarca = new Marca();
                nuevaMarca.Nombre = txtNombreMarca.Text;

                MarcaNegocio marcaNegocio = new MarcaNegocio();
                if (marcaNegocio.ExisteNombreMarca(nuevaMarca.Nombre))
                {
                    lblMensaje.Text = "La marca: " + nuevaMarca.Nombre + " ya existe";
                    lblMensaje.Visible = true;
                    return;
                    throw new Exception("La marca: " + nuevaMarca.Nombre + " ya existe");
                }
                hdnMarcaId.Value = "";
                marcaNegocio.agregar(nuevaMarca);
                CargarMarcas();

                txtNombreMarca.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "HideModalAgregar", "var modalAgregar = " +
                    "bootstrap.Modal.getInstance(document.getElementById('modalAgregar')); if(modalAgregar) { modalAgregar.hide(); }", true);//Cierra el modal de Agregar
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR no se pudo agregar la marca", ex);
            }
        }
        protected void btnEditarMarca_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdnMarcaId.Value))
                {
                    lblMensaje.Text = "Por favor, selecciona una marca para editar.";
                    lblMensaje.Visible = true;
                    return;
                }
                int idMarca = Convert.ToInt32(hdnMarcaId.Value);

                string nombreMarca = txtNombreMarcaEditar.Text.ToString();
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                if (marcaNegocio.ExisteNombreMarca(nombreMarca))
                {
                    lblMensaje.Text = "La marca: " + nombreMarca + " ya existe";
                    lblMensaje.Visible = true;
                    return;
                    throw new Exception("La marca: " + nombreMarca + " ya existe");
                }
                Marca marca = new Marca();
                marca.ID = idMarca;
                marca.Nombre = nombreMarca;

                marcaNegocio.editar(marca);
                CargarMarcas();
                ScriptManager.RegisterStartupScript(this, GetType(), "HideModalEditar", "$('#modalEditar').modal('hide');", true); // Cerrar el modal de Editar
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR no se pudo agregar la Marca", ex);
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hdnMarcaId.Value))
                {
                    lblMensaje.Text = "Por favor, selecciona una marca para eliminar.";
                    lblMensaje.Visible = true;
                    return;
                }
                int idMarca = Convert.ToInt32(hdnMarcaId.Value);//Tengo el ID con el HiddenField
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                marcaNegocio.eliminar(idMarca);
                CargarMarcas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}