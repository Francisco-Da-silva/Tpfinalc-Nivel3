using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Service
{

    public class EmailService
    {

        
        private readonly SmtpClient server;
        private MailMessage email;

        public EmailService()
        {


            // TLS 1.2 para .NET Framework
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            server = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["SMTP_HOST"],
                Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]),
                EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTP_SSL"]),
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 20000,
                Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["SMTP_USER"],
                    ConfigurationManager.AppSettings["SMTP_PASS"]
                )
            };
        }

        public void ArmarCorreoResetPassword(string destino, string url)
        {
            email = new MailMessage
            {
                From = new MailAddress(
                    ConfigurationManager.AppSettings["EMAIL_FROM"],
                    ConfigurationManager.AppSettings["EMAIL_FROM_NAME"]
                ),
                Subject = "Recuperar contraseña",
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8,
                Body = $@"
<h2>Recuperación de contraseña</h2>
<p>Hacé clic en el siguiente enlace:</p>
<p><a href='{url}'>Restablecer contraseña</a></p>
<p>Si no solicitaste esto, ignorá este email.</p>"
            };

            email.To.Add(destino);
        }

        public void Enviar()
        {
            try
            {
                if (email == null)
                    throw new InvalidOperationException("No se armó el correo antes de enviar.");

                server.Send(email);
            }
            catch (SmtpException ex)
            {
                string inner = ex.InnerException?.Message ?? "";
                throw new Exception($"SMTP StatusCode: {ex.StatusCode} | {ex.Message} | Inner: {inner}", ex);
            }
            catch (Exception ex)
            {
                string inner = ex.InnerException?.Message ?? "";
                throw new Exception($"SMTP General: {ex.Message} | Inner: {inner}", ex);
            }
        }
    }
}