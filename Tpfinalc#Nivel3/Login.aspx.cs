using conexion;
using Dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;

namespace Tpfinalc_Nivel3
{
    public partial class Login : Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = (txtEmail.Text ?? "").Trim();
                string pass = (txtPass.Text ?? "").Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
                {
                    lblMsg.Text = "Completá email y contraseña.";
                    return;
                }

                UsuarioDAL dal = new UsuarioDAL();
                Usuario user = dal.Login(email, pass);

                if (user == null)
                {
                    lblMsg.Text = "Email o contraseña incorrectos.";
                    return;
                }

                // Guardar sesión
                Session["Usuario"] = user;

                // Redirigir según rol
                if (user.Admin)
                    Response.Redirect("AdminProductos.aspx", false);
                else
                    Response.Redirect("Home.aspx", false);
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }


        //protected void btnEnviar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string email = (txtEmail.Text ?? "").Trim();

        //        if (string.IsNullOrWhiteSpace(email))
        //        {
        //            lblMsg.CssClass = "text-danger";
        //            lblMsg.Text = "Ingresá tu email.";
        //            return;
        //        }

        //        int minutos = int.Parse(ConfigurationManager.AppSettings["RESET_TOKEN_MINUTOS"]);

        //        // ⚠️ Mensaje genérico SIEMPRE (seguridad: no revelar si existe o no)
        //        lblMsg.CssClass = "text-success";
        //        lblMsg.Text = "Si el email existe, te enviamos un link para restablecer tu contraseña.";

        //        // 1) Crear token (si el mail NO existe, el SP puede lanzar error)
        //        //    Para que no se note, lo manejamos sin mostrar el error al usuario.
        //        string token;
        //        try
        //        {
        //            token = new UsuarioDAL().CrearTokenReset(email, minutos);
        //        }
        //        catch
        //        {
        //            // No hacemos nada a propósito: mismo mensaje para todos
        //            return;
        //        }

        //        // 2) Armar link absoluto
        //        string url = Request.Url.GetLeftPart(UriPartial.Authority)
        //                     + ResolveUrl("~/ResetPassword.aspx?token=" + token);

        //        // 3) Enviar email
        //        EmailService mail = new EmailService();
        //        mail.ArmarCorreoResetPassword(email, url);
        //        mail.Enviar();

        //        // ✅ Debug opcional (si querés verlo solo en localhost)
        //        bool esLocal = Request.IsLocal;
        //        if (esLocal)
        //        {
        //            lnkDebug.Text = "Link de reseteo (debug)";
        //            lnkDebug.NavigateUrl = url;
        //            lnkDebug.Visible = true;
        //        }
        //        else
        //        {
        //            lnkDebug.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["Error"] = ex.Message;
        //        Response.Redirect("~/Error.aspx", false);
        //        Context.ApplicationInstance.CompleteRequest();
        //    }
        //}

    }
}
