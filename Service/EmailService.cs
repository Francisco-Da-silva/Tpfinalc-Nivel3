using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmailService
    {
        private readonly SmtpClient server;
        private MailMessage email;

        public EmailService()
        {
            server = new SmtpClient(
                ConfigurationManager.AppSettings["SMTP_HOST"],
                int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"])
            )
            {
                Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["SMTP_USER"],
                    ConfigurationManager.AppSettings["SMTP_PASS"]
                ),
                EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTP_SSL"])
            };
        }

        public void ArmarCorreo(string emailDestino, string asunto, string cuerpoHtml)
        {
            email = new MailMessage
            {
                From = new MailAddress(
                    ConfigurationManager.AppSettings["EMAIL_FROM"],
                    ConfigurationManager.AppSettings["EMAIL_FROM_NAME"]
                ),
                Subject = asunto,
                Body = cuerpoHtml,
                IsBodyHtml = true
            };

            email.To.Add(emailDestino);
        }

        public void ArmarCorreoResetPassword(string emailDestino, string resetLink)
        {
            int minutos = int.Parse(ConfigurationManager.AppSettings["RESET_TOKEN_MINUTOS"]);

            string asunto = "Recuperación de contraseña - Catálogo Web";

            string cuerpo = $@"
                <h2>Recuperación de contraseña</h2>
                <p>Recibimos una solicitud para restablecer tu contraseña.</p>
                <p>Hacé click en el siguiente enlace:</p>
                <p><a href='{resetLink}' target='_blank'>Restablecer contraseña</a></p>
                <p>Este link vence en <strong>{minutos} minutos</strong>.</p>
                <hr />
                <p>Si no solicitaste este cambio, ignorá este correo.</p>
            ";

            ArmarCorreo(emailDestino, asunto, cuerpo);
        }

        public void Enviar()
        {
            try
            {
                if (email == null)
                    throw new InvalidOperationException("No hay email armado.");

                server.Send(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el email.", ex);
            }
            finally
            {
                email?.Dispose();
            }
        }

    }
}