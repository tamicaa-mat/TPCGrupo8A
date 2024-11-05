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
    public partial class FormularioProductosAM : System.Web.UI.Page
    {
        public int tipoUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (!(usuario == null))
            {
                tipoUsuario = (int)usuario.TipoUsuario;
            }
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarMarcas();
            }
        }
        public void CargarMarcas()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            List<Marca> marcas = marcaNegocio.listar();

            ddlMarcas.Items.Clear();
            ddlMarcas.Items.Add(new ListItem("Selecciona una marca", ""));

            foreach (Marca marca in marcas)
            {
                ddlMarcas.Items.Add(new ListItem(marca.Nombre, marca.ID.ToString()));
            }
        }
        protected void ddlMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlMarcas.SelectedValue))
            {
                txtMarca.Text = ddlMarcas.SelectedItem.Text;
            }
            else
            {
                txtMarca.Text = "";
            }
        }
        public void CargarCategorias()
        {
            CategoriaNegocio categoriaNegocio= new CategoriaNegocio();
            List<Categoria> categorias = categoriaNegocio.listar();

            ddlCategorias.Items.Clear();
            ddlCategorias.Items.Add(new ListItem("Selecciona una Categóría", ""));

            foreach (Categoria categoria in categorias)
            {
                ddlCategorias.Items.Add(new ListItem(categoria.Nombre, categoria.ID.ToString()));
            }
        }
        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCategorias.SelectedValue))
            {
                txtCategoria.Text = ddlCategorias.SelectedItem.Text;
            }
            else
            {
                txtCategoria.Text = "";
            }
        }

        // En proceso!, para poder agregar las imagenes primero debe exitir el producto
        //protected void btnAgregarImagen_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(txtImagenUrl.Text))
        //    {
        //        Imagen imagen = new Imagen();
        //        imagen.ImagenUrl = txtImagenUrl.Text; 

        //    }
        //}
        //private void CargarImagenes()
        //{
        //    ImagenNegocio imagenNegocio = new ImagenNegocio();
        //    Imagen imagenes = imagenNegocio.listar(); Necesita antes conocer el producto
        //}
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto(); 
            ProductoNegocio productoNegocio = new ProductoNegocio();

            producto.Codigo = txtCodigo.Text;
            producto.Nombre = txtNombre.Text;
            producto.Descripcion = txtDescripcion.Text;
            
            producto.Marca = new Marca();
            if(ddlMarcas != null)
            {
                producto.Marca.ID = int.Parse(ddlMarcas.SelectedValue);
            }
            producto.Categoria = new Categoria();
            if(ddlCategorias!= null)
            {
                producto.Categoria.ID = int.Parse(ddlCategorias.SelectedValue);
            }
            producto.Precio = (float)decimal.Parse(txtPrecio.Text);
            //producto.Imagenes = txtImagenUrl.Text;
            producto.Stock = int.Parse(txtStock.Text);

            if (producto.ID != 0)
            {
                productoNegocio.editar(producto);
            }
            else
            {
                productoNegocio.agregar(producto);
            }
        }
    }
}