using conexion;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Tpfinalc_Nivel3
{
    public partial class RecuperarContraseña : System.Web.UI.Page
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
                    lblMsg.Text = "Ingresá tu email.";
                    return;
                }

                int minutos = int.Parse(ConfigurationManager.AppSettings["RESET_TOKEN_MINUTOS"]);

                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Si el email existe, te enviamos un link para restablecer tu contraseña.";

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
                             + ResolveUrl("~/ResetPassword.aspx?token=" + token);

                EmailService mail = new EmailService();
                mail.ArmarCorreoResetPassword(email, url);
                mail.Enviar();

                bool esLocal = Request.IsLocal;
                if (esLocal)
                {
                    lnkDebug.Text = "Link de reseteo (debug)";
                    lnkDebug.NavigateUrl = url;
                    lnkDebug.Visible = true;
                }
                else
                {
                    lnkDebug.Visible = false;
                }
            }
            catch (Exception exMail)
            {

                //Session["Error"] = ex.ToString();
                //Session["Error"] = ex.Message;
                //Response.Redirect("~/Error.aspx", false);
                //Context.ApplicationInstance.CompleteRequest();
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = exMail.Message;      // ahora va a decir "Error SMTP: Authentication failed..." etc.
                Session["Error"] = exMail.ToString();
            }
        }
    }
}
   