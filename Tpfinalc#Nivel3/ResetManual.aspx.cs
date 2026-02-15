using conexion;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Tpfinalc_Nivel3
{
    public partial class ResetManual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string email = Request.QueryString["email"];
                if (!string.IsNullOrWhiteSpace(email))
                    txtEmail.Text = email.Trim();
            }
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";

                string email = (txtEmail.Text ?? "").Trim();
                string codigo = (txtCodigo.Text ?? "").Trim();
                string pass1 = (txtPass1.Text ?? "");
                string pass2 = (txtPass2.Text ?? "");

                // 1) Validaciones básicas
                if (string.IsNullOrWhiteSpace(email))
                    throw new Exception("Ingresá tu email.");

                if (string.IsNullOrWhiteSpace(codigo))
                    throw new Exception("Ingresá el código de soporte.");

                if (string.IsNullOrWhiteSpace(pass1) || string.IsNullOrWhiteSpace(pass2))
                    throw new Exception("Completá la nueva contraseña en ambos campos.");

                if (pass1 != pass2)
                    throw new Exception("Las contraseñas no coinciden.");

                if (pass1.Length < 6)
                    throw new Exception("La contraseña debe tener al menos 6 caracteres.");

                // 2) Validar código de soporte (web.config)
                string codigoSoporte = ConfigurationManager.AppSettings["RESET_MANUAL_CODE"];

                if (string.IsNullOrWhiteSpace(codigoSoporte))
                    throw new Exception("No está configurado el código de soporte en el servidor.");

                if (!SecureEquals(codigo, codigoSoporte))
                    throw new Exception("Código de soporte inválido.");

                // 3) Cambiar contraseña
                string passHash = HashPasswordSha256(pass1);

                var dal = new UsuarioDAL();
                bool ok = dal.CambiarPasswordPorEmail(email, passHash);

                if (!ok)
                    throw new Exception("No se pudo cambiar la contraseña. Verificá el email.");

                lblMsg.CssClass = "alert alert-success d-block";
                lblMsg.Text = "Contraseña actualizada correctamente. Ya podés iniciar sesión.";
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "alert alert-danger d-block";
                lblMsg.Text = ex.Message;
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

        private bool SecureEquals(string a, string b)
        {
            if (a == null || b == null) return false;

            byte[] ba = Encoding.UTF8.GetBytes(a);
            byte[] bb = Encoding.UTF8.GetBytes(b);

            if (ba.Length != bb.Length) return false;

            int diff = 0;
            for (int i = 0; i < ba.Length; i++)
                diff |= ba[i] ^ bb[i];

            return diff == 0;
        }
    }
}