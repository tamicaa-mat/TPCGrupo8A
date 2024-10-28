using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

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

        protected void btnRegistrar_On_Click(object sender, EventArgs e)
        {
            try
            {

                if (validarCajasTexto())
                {
                    Usuario usuario = new Usuario();
                    usuario.Apellido = txtApellido.Text.ToString();
                    usuario.Nombre = txtNombre.Text.ToString();
                    usuario.Email = txtEmailRegistro.Text.ToString();
                    usuario.Contrasenia = txtPasswordRegistro.Text.ToString();
                    usuario.TipoUsuario = 0;

                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();


                    usuarioNegocio.RegistroUsuario(usuario);
                    Response.Redirect("RegistroExitoso.aspx?nombre=" + Server.UrlEncode(usuario.Nombre), false);

                }
                else
                {
                    return;
                   
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        // PARA LAS CAJAS DE REGISTRO VALIDACIONES

        bool esNumero(string texto)
        {
            long numero;
            return long.TryParse(texto, out numero);
        }
        // validacion formato email
        private bool esEmailValido(string email)
        {
            string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patronEmail);
        }
        //validacion para que la contraseña tenga al menos 5 caracteres ,entre ellos al menos un numero
        private bool esPasswordValida(string password)
        {
            return password.Length >= 5 &&
                   password.Any(char.IsLetter) &&
                   password.Any(char.IsDigit);
        }


        public bool validarCajasTexto()
        {
            string mensajeErrorNombre = "";
            string mensajeErrorApellido = "";
            string mensajeErrorEmail = "";
            string mensajeErrorContrasenia = "";
            bool hayErrores = false;

            // Validación campo APELLIDO
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                mensajeErrorApellido = "El campo APELLIDO no puede estar vacío.";
                hayErrores = true;
            }
           else if (esNumero(txtApellido.Text))
            {
                mensajeErrorApellido = "APELLIDO debe contener solo LETRAS.";
                hayErrores = true;
            }

            // Validación campo NOMBRE
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                mensajeErrorNombre = "El campo NOMBRE no puede estar vacío.";
                hayErrores = true;
            }
            else if (esNumero(txtNombre.Text))
            {
                mensajeErrorNombre = "NOMBRE debe contener solo LETRAS.";
                hayErrores = true;
            }

            // Validación campo EMAIL
            if (string.IsNullOrEmpty(txtEmailRegistro.Text))
            {
                mensajeErrorEmail = "El campo EMAIL no puede estar vacío.";
                hayErrores = true;
            }
            else if (!esEmailValido(txtEmailRegistro.Text))
            {
                mensajeErrorEmail = "El campo EMAIL no es válido.";
                hayErrores = true;
            }

            // Validación del campo CONTRASEÑA
            if (string.IsNullOrEmpty(txtPasswordRegistro.Text))
            {
                mensajeErrorContrasenia = "El campo PASSWORD no puede estar vacío.";
                hayErrores = true;
            }
            else if (!esPasswordValida(txtPasswordRegistro.Text))
            {
                mensajeErrorContrasenia = "La contraseña debe tener al menos 5 caracteres y contener letras y números.";
                hayErrores = true;
            }

           
            if (hayErrores)
            {
               
                if (!string.IsNullOrEmpty(mensajeErrorNombre))
                    txtNombre.Attributes["placeholder"] = mensajeErrorNombre;
                if (!string.IsNullOrEmpty(mensajeErrorApellido))
                    txtApellido.Attributes["placeholder"] = mensajeErrorApellido;
                if (!string.IsNullOrEmpty(mensajeErrorEmail))
                    txtEmailRegistro.Attributes["placeholder"] = mensajeErrorEmail;
                if (!string.IsNullOrEmpty(mensajeErrorContrasenia))
                    txtPasswordRegistro.Attributes["placeholder"] = mensajeErrorContrasenia;

               
                txtNombre.ForeColor = System.Drawing.Color.Red;
                txtApellido.ForeColor = System.Drawing.Color.Red;
                txtEmailRegistro.ForeColor = System.Drawing.Color.Red;
                txtPasswordRegistro.ForeColor = System.Drawing.Color.Red;

                return false; 
            }

            // Si no hay errores true
            return true;
        }








    }
}