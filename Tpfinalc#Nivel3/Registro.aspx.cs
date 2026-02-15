using conexion;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tpfinalc_Nivel3
{
    public partial class Registro : System.Web.UI.Page
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
                    Pass = pass,
                    Nombre = (txtNombre.Text ?? "").Trim(),
                    Apellido = (txtApellido.Text ?? "").Trim(),
                    UrlImagenPerfil = (txtImg.Text ?? "").Trim(),
                    Admin = false
                };

                new UsuarioDAL().Registrar(nuevo);

                // Opcional: loguearlo automáticamente
                Session["Usuario"] = new UsuarioDAL().Login(email, pass);

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