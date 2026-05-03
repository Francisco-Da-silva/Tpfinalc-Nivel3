using conexion;
using conexion.conexion;
using Dominio;
using System;
using System.Web.UI;

namespace Tpfinalc_Nivel3
{
    public partial class Login : Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = (txtEmail.Text ?? "").Trim();
                string passPlano = (txtPass.Text ?? "").Trim();

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(passPlano))
                {
                    lblMsg.Text = "Completá email y contraseña.";
                    return;
                }

                string passHash = PasswordHelper.HashPasswordSha256(passPlano);

                UsuarioDAL dal = new UsuarioDAL();
                Usuario user = dal.Login(email, passHash);

                if (user == null)
                {
                    lblMsg.Text = "Email o contraseña incorrectos.";
                    return;
                }

                Session["Usuario"] = user;

                if (user.Admin)
                    Response.Redirect("AdminProductos.aspx", false);
                else
                    Response.Redirect("Home.aspx", false);

                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}
