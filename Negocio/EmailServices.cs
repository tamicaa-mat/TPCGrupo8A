using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;


namespace Negocio
{
    public class EmailServices

    {
        private MailMessage email;
        private SmtpClient server;
        public EmailServices()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("pruebaproyectocuatrimestral@gmail.com", "raoa utac kqpj ifrm"); // CREDENCIALES DEL CORREO
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        public void armarCorreo(string emailDestino, string asunto, string cuerpo, string emailRemitente)
        {
            email = new MailMessage();
            email.From = new MailAddress(emailRemitente); // Usa el correo ingresado como remitente
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.Body = cuerpo;
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
