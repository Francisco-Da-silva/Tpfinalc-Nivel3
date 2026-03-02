using conexion;
using conexion.conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tpfinalc_Nivel3
{
    public partial class ResetPassword : System.Web.UI.Page
    {

        private string Token => Request.QueryString["token"];

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (string.IsNullOrWhiteSpace(Token))
                    {
                        lblMsg.Text = "Token inválido.";
                        pnlForm.Visible = false;
                        return;
                    }

                    var dal = new UsuarioDAL();
                    bool ok = dal.TokenValido(Token);

                    pnlForm.Visible = ok;
                    if (!ok) lblMsg.Text = "El link es inválido o ya venció.";
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("~/Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                string p1 = (txtPass.Text ?? "").Trim();
                string p2 = (txtPass2.Text ?? "").Trim();

                if (p1 == "" || p2 == "")
                {
                    lblMsg.Text = "Completá los campos.";
                    return;
                }

                if (p1 != p2)
                {
                    lblMsg.Text = "Las contraseñas no coinciden.";
                    return;
                }

                if (p1.Length > 20)
                {
                    lblMsg.Text = "Máximo 20 caracteres (por la DB).";
                    return;
                }

                new UsuarioDAL().ConfirmarReset(Token, p1);

                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("~/Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

    }
}