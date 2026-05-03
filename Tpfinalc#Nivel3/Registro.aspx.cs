using conexion;
using conexion.conexion;
using Dominio;
using System;
using System.Web.UI;

namespace Tpfinalc_Nivel3
{
    public partial class Registro : Page
    {
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = (txtEmail.Text ?? "").Trim();
                string pass = (txtPass.Text ?? "").Trim();
                string pass2 = (txtPass2.Text ?? "").Trim();

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
                {
                    lblMsg.Text = "Email y contraseña son obligatorios.";
                    return;
                }

                if (pass != pass2)
                {
                    lblMsg.Text = "Las contraseñas no coinciden.";
                    return;
                }

                if (pass.Length > 20)
                {
                    lblMsg.Text = "La contraseña no puede superar 20 caracteres (por la DB).";
                    return;
                }

                Usuario nuevo = new Usuario
                {
                    Email = email,
                    Pass = PasswordHelper.HashPasswordSha256(pass),
                    Nombre = (txtNombre.Text ?? "").Trim(),
                    Apellido = (txtApellido.Text ?? "").Trim(),
                    UrlImagenPerfil = (txtImg.Text ?? "").Trim(),
                    Admin = false
                };

                UsuarioDAL dal = new UsuarioDAL();
                dal.Registrar(nuevo);

                Session["Usuario"] = dal.Login(email, nuevo.Pass);

                Response.Redirect("~/Home.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}
