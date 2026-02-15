using coneccion;
using conexion;
using Dominio;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

                // ✅ Hashear antes de validar (porque en DB guardás hash)
                string passHash = HashPasswordSha256(passPlano);

                UsuarioDAL dal = new UsuarioDAL();
                Usuario user = dal.Login(email, passHash);

                if (user == null)
                {
                    lblMsg.Text = "Email o contraseña incorrectos.";
                    return;
                }

                // Guardar sesión (elegí UNA clave y usala en todo el proyecto)
                Session["usuario"] = user;

                // Redirigir según rol
                if (user.Admin)
                    Response.Redirect("AdminProductos.aspx", false);
                else
                    Response.Redirect("Home.aspx", false);
            }
            catch (Exception ex)
            {
                // Si ya no querés usar Session["Error"], podés sacarlo.
                Session["Error"] = ex.ToString();
                Response.Redirect("Error.aspx", false);
            }
        }

        private string HashPasswordSha256(string plain)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(plain);
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}

