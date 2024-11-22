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
    public partial class DetalleProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idArticulo = 0;
                if (Session["ID"] != null)
                {
                    idArticulo = int.Parse(Session["ID"].ToString());
                }
                else
                {
                    AccesoDatos datos = new AccesoDatos();
                    ProductoNegocio articuloNegocio = new ProductoNegocio();
                    //Producto articulo = articuloNegocio.PrimerArticulo();

                    //if (articulo != null)
                    //    idArticulo = articulo.ID;
                    //else
                    //{
                    //    //FALTAAAAAAAAAAA pantalla de "Error no se encontro producto para mostrar" 
                    //}
                }
                cargaImagenes(idArticulo);
                cargaInformacionArticulo(idArticulo);
                cargaOtrosArticulos(idArticulo);
            }
        }
        protected void cargaImagenes(int idArticulo)
        {
            ImagenNegocio ImagenNegocio = new ImagenNegocio();
            List<Imagen> imagenes = ImagenNegocio.imagenesxProducto(idArticulo);

            if (imagenes == null || imagenes.Count == 0)
            {
                Imagen imagenRota = new Imagen();
                imagenRota.ImagenUrl = "https://media.istockphoto.com/id/1128826884/es/vector/ning%C3%BAn-s%C3%ADmbolo-de-vector-de-imagen-falta-icono-disponible-no-hay-galer%C3%ADa-para-este-momento.jpg?s=612x612&w=0&k=20&c=9vnjI4XI3XQC0VHfuDePO7vNJE7WDM8uzQmZJ1SnQgk=";
                imagenes.Add(imagenRota);
            }
            for (int x = 0; x < imagenes.Count; x++)
            {
                imagenes[x].Estado = (x == 0);
            }
            RepeaterImagenes.DataSource = imagenes;
            RepeaterImagenes.DataBind();
        }
        protected void cargaInformacionArticulo(int idArticulo)
        {
            ProductoNegocio articuloNegocio = new ProductoNegocio();
            Producto articulo = articuloNegocio.ObtenerArticuloId(idArticulo);

            try
            {
                if (articulo != null)
                {
                    nombreArticulo.InnerText = articulo.Nombre.ToString();
                    descripcionArticulo.InnerText = articulo.Descripcion.ToString();
                    precioArticulo.InnerText = "$" + articulo.Precio.ToString();
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cargaOtrosArticulos(int idArticuloSeleccionado)
        {
            ProductoNegocio articuloNegocio = new ProductoNegocio();
            List<Producto> listaArticulos = articuloNegocio.Listar(); // Usamos tu método "listar" para obtener todos los artículos

            
            List<Producto> otrosArticulos = listaArticulos.Where(a => a.ID != idArticuloSeleccionado).ToList();
            foreach (var articulo in otrosArticulos)
            {
                articulo.Imagenes = new ImagenNegocio().imagenesxProducto(articulo.ID);

                // Si no tiene imágenes, asigna una lista vacía
                if (articulo.Imagenes == null || articulo.Imagenes.Count == 0)
                {
                    articulo.Imagenes = new List<Imagen>
        {
            new Imagen { ImagenUrl = "\"https://media.istockphoto.com/id/1128826884/es/vector/ning%C3%BAn-s%C3%ADmbolo-de-vector-de-imagen-falta-icono-disponible-no-hay-galer%C3%ADa-para-este-momento.jpg?s=612x612&w=0&k=20&c=9vnjI4XI3XQC0VHfuDePO7vNJE7WDM8uzQmZJ1SnQgk=\"" } // Imagen por defecto
        };
                }
            }

            RepeaterLista.DataSource = otrosArticulos;
            RepeaterLista.DataBind();
        }
        protected void BtnVer_OnClick(object sender, EventArgs e)
        {
            string btn = ((Button)sender).CommandArgument;
            int idSeleccionado = int.Parse(btn);
            cargaInformacionArticulo(idSeleccionado);
            cargaOtrosArticulos(idSeleccionado);
            Session["ID"] = idSeleccionado;
            Response.Redirect("DetalleProducto.aspx", false);
        }
        //protected void BtnAgregarCarrito_Click(object sender, EventArgs e)
        //{
        //    if (Session["ID"] != null)
        //    {
        //        int idProducto = Convert.ToInt32(Session["ID"]);
        //        List<int> carrito;
        //        if (Session["Carrito"] != null)
        //            carrito = (List<int>)Session["Carrito"];
        //        else
        //            carrito = new List<int>();

        //        carrito.Add(idProducto);
        //        Session["Carrito"] = carrito;
        //        string MensajeScript = "alert('Producto agregado correctamente al carrito');";
        //        ClientScript.RegisterStartupScript(this.GetType(), "ProductoAgregado", MensajeScript, true);
        //        Response.Redirect("CarritoPago.aspx", false);
        //    }


        //}

        protected void BtnAgregarCarrito_Click(object sender, EventArgs e)
        {
            ProductoNegocio prod = new ProductoNegocio();

            if (Session["ID"] != null)
            {
                int idProducto = Convert.ToInt32(Session["ID"]);

                // Verificar stock
                int stock = prod.VerificarStock(idProducto);

                if (stock>0) // Si hay stock disponible
                {
                    List<int> carrito;

                    if (Session["Carrito"] != null)
                        carrito = (List<int>)Session["Carrito"];
                    else
                        carrito = new List<int>();

                    carrito.Add(idProducto);
                    Session["Carrito"] = carrito;

                    // Mensaje de éxito
                    string MensajeScript = "alert('Producto agregado correctamente al carrito');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ProductoAgregado", MensajeScript, true);
                    Response.Redirect("CarritoPago.aspx", false);

                  

                }
                else
                {
                    // Mensaje de error por falta de stock
                    string MensajeScript = "alert('No hay suficiente stock para agregar este producto al carrito');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SinStock", MensajeScript, true);
                    // Response.Redirect("Default.aspx", false);
                }
            }
        }




    }
}