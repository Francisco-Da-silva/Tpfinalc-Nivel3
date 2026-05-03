using conexion.conexion;
using Service;
using System;
using System.Configuration;
using System.Web.UI;

namespace Tpfinalc_Nivel3
{
    public partial class RecuperarContraseña : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = (txtEmail.Text ?? "").Trim();

                if (string.IsNullOrWhiteSpace(email))
                {
                    lblMsg.CssClass = "text-danger";
                    lblMsg.Text = "Ingresa tu email.";
                    return;
                }

                int minutos = int.Parse(ConfigurationManager.AppSettings["RESET_TOKEN_MINUTOS"]);

                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Si el email existe, te enviamos un link para restablecer tu contrasena.";

                string token;
                try
                {
                    token = new UsuarioDAL().CrearTokenReset(email, minutos);
                }
                catch
                {
                    return;
                }

                string url = Request.Url.GetLeftPart(UriPartial.Authority)
                             + ResolveUrl("~/ResetPassword.aspx?token=" + Server.UrlEncode(token));

                EmailService mail = new EmailService();
                mail.ArmarCorreoResetPassword(email, url);
                mail.Enviar();
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "No se pudo enviar el email de recuperacion. Verifica la configuracion SMTP.";
                System.Diagnostics.Trace.TraceError("Error reset password email: " + ex);
            }
        }
    }
}
