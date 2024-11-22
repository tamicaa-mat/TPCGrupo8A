using Datos;
using Dominio;
using Negocio;
using System;
using System.Collections.Concurrent;
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
            if(tipoUsuario != 1)
            {
                Response.Redirect("Default.aspx", false);
            }
            if (!IsPostBack)
            {
                CargarMarcas();
                CargarCategorias();
                if (Session["ID"] != null)
                {
                    try
                    {
                        int idProducto = (int)Session["ID"];
                        CargarProductos(idProducto);
                        CargarImagenes(idProducto);
                        pnlImagenes.Visible = true; //si hay id del producto podra agregar y ver las imgs
                    }
                    catch (FormatException)
                    {
                        pnlImagenes.Visible = false;
                    }
                }
                else
                {
                    pnlImagenes.Visible = false;
                }
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
            var siteMaster = (SiteMaster)this.Master;
            if (siteMaster != null)
            {
                siteMaster.CargarCategorias();
                siteMaster.CargarMarcas();
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
            var siteMaster = (SiteMaster)this.Master;
            if (siteMaster != null)
            {
                siteMaster.CargarCategorias();
                siteMaster.CargarMarcas();
            }
        }
        public void CargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> categorias = categoriaNegocio.listar();

            ddlCategorias.Items.Clear();
            ddlCategorias.Items.Add(new ListItem("Selecciona una Categóría", ""));

            foreach (Categoria categoria in categorias)
            {
                ddlCategorias.Items.Add(new ListItem(categoria.Nombre, categoria.ID.ToString()));
            }
            var siteMaster = (SiteMaster)this.Master;
            if (siteMaster != null)
            {
                siteMaster.CargarCategorias();
                siteMaster.CargarMarcas();
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
            var siteMaster = (SiteMaster)this.Master;
            if (siteMaster != null)
            {
                siteMaster.CargarCategorias();
                siteMaster.CargarMarcas();
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto();
            ProductoNegocio productoNegocio = new ProductoNegocio();

            lblExito.Visible = false;
            lblErrorStock.Visible = false;
            lblErrorPrecio.Visible = false;
            lblErrorCodigo.Visible = false;
            lblErrorMarca.Visible = false;
            lblErrorCategoria.Visible = false;

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                lblErrorCodigo.Text = "El código no puede quedar vacío";
                lblErrorCodigo.Visible = true;
                return;
            }

            if (string.IsNullOrEmpty(txtPrecio.Text)) {
                lblErrorPrecio.Text = "El precio no puede quedar vacío";
                lblErrorPrecio.Visible = true;
                return;
            }

            
            if (string.IsNullOrEmpty(txtStock.Text))
            {
                lblErrorStock.Text = "El stock no puede quedar vacío";
                lblErrorStock.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMarca.Text))
            {
                lblErrorMarca.Text = "Debe seleccionar una marca";
                lblErrorMarca.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCategoria.Text))
            {
                lblErrorCategoria.Text = "Debe seleccionar una categoría";
                lblErrorCategoria.Visible = true;
                return;
            }

            producto.Marca = new Marca();
            if (ddlMarcas != null)
            {
                producto.Marca.ID = int.Parse(ddlMarcas.SelectedValue);
            }
            producto.Categoria = new Categoria();
            if (ddlCategorias != null)
            {
                producto.Categoria.ID = int.Parse(ddlCategorias.SelectedValue);
            }

            producto.Codigo = txtCodigo.Text;
            producto.Nombre = txtNombre.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.Precio = (float)decimal.Parse(txtPrecio.Text);
            producto.Stock = int.Parse(txtStock.Text);
            

            if (!VerificarSiExiste(producto))
            {
                return;
            }

            lblExito.Text = "ARTICULO AGREGADO ÉXITOSAMENTE";
            lblExito.Visible = true;
            lblExito.ForeColor = System.Drawing.Color.Green;

            if (Session["ID"] != null)
            {
                producto.ID = (int)Session["ID"];
                productoNegocio.editar(producto);
                pnlImagenes.Visible = true;
                Session["ID"] = "";
            }
            else
            {
                productoNegocio.agregar(producto);
                Session["ID"] = (int)productoNegocio.ObtenerUltimoIdProducto();
                pnlImagenes.Visible = true;
            }
            var siteMaster = (SiteMaster)this.Master;
            if (siteMaster != null)
            {
                siteMaster.CargarCategorias();
                siteMaster.CargarMarcas();
            }


        }
        private void CargarProductos(int idProducto)
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            Producto producto = productoNegocio.ObtenerIdProducto(idProducto);

            if (producto != null)
            {
                if(Session["ID"] != null)
                {
                    txtCodigo.Enabled = false;

                }
                else
                {
                    txtCodigo.Text = producto.Codigo != null ? producto.Codigo.ToString() : "";

                }
                txtNombre.Text = producto.Nombre;
                txtDescripcion.Text = producto.Descripcion;
                txtPrecio.Text = producto.Precio.ToString();
                txtStock.Text = producto.Stock.ToString();
                if (producto.Marca != null)
                    ddlMarcas.SelectedValue = producto.Marca.ID.ToString();
                if (producto.Categoria != null)
                    ddlCategorias.SelectedValue = producto.Categoria.ID.ToString();
            }
            var siteMaster = (SiteMaster)this.Master;
            if (siteMaster != null)
            {
                siteMaster.CargarCategorias();
                siteMaster.CargarMarcas();
            }
        }
        private void CargarImagenes(int idProducto)
        {
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            List<Imagen> imagenes = imagenNegocio.imagenesxProducto(idProducto);

            if (imagenes != null && imagenes.Count > 0)
            {
                rptImagenes.DataSource = imagenes;
                rptImagenes.DataBind();
            }
            var siteMaster = (SiteMaster)this.Master;
            if (siteMaster != null)
            {
                siteMaster.CargarCategorias();
                siteMaster.CargarMarcas();
            }
        }
        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (Session["ID"] != null)
            {
                int idProducto = (int)Session["ID"];
                string imagenUrl = txtImagenUrl.Text;
                if (!string.IsNullOrEmpty(imagenUrl))
                {
                    Imagen imagenNueva = new Imagen();
                    imagenNueva.IdProducto = idProducto;
                    imagenNueva.ImagenUrl = imagenUrl;

                    ImagenNegocio imagenNegocio = new ImagenNegocio();
                    imagenNegocio.agregar(imagenNueva);

                    txtImagenUrl.Text = "";
                    CargarImagenes(idProducto);
                }
            }
            var siteMaster = (SiteMaster)this.Master;
            if (siteMaster != null)
            {
                siteMaster.CargarCategorias();
                siteMaster.CargarMarcas();
            }
            //Session["ID"] = null;
        }
        protected void btnEliminarImagen_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ImagenNegocio imagenNegocio = new ImagenNegocio();
                int idImagen = Convert.ToInt32(e.CommandArgument);
                imagenNegocio.eliminar(idImagen);
                int idProducto = (int)Session["ID"];
                CargarImagenes(idProducto);
            }
            catch (Exception)
            {

                throw;
            }
            var siteMaster = (SiteMaster)this.Master;
            if (siteMaster != null)
            {
                siteMaster.CargarCategorias();
                siteMaster.CargarMarcas();
            }
        }

        public bool VerificarSiExiste(Producto producto)
        {

            lblErrorCodigo.Visible = false;
            lblErrorStock.Visible = false;
            lblErrorPrecio.Visible = false;
            ProductoNegocio productoNegocio = new ProductoNegocio();

            // Verificar si el código ya existe en la base de datos
            if (productoNegocio.ObtenerArticuloPorCodigo(producto.Codigo) != null)
            {
                lblErrorCodigo.Text = "El código ya existe.";
                lblErrorCodigo.Visible = true;
                return false;
            }

            if (producto.Stock <= 0)
            {
                lblErrorStock.Text = "El stock no puede ser negativo.";
                lblErrorStock.Visible = true;
                return false;
            }

            if (producto.Precio <= 0)
                {
                    lblErrorPrecio.Text = "El precio no puede ser negativo. ";
                    lblErrorPrecio.Visible = true;

                    return false;
            }
            
           
                return true;
            }
        }
    }
