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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var user = Session["Usuario"] as Usuario;

                if (user == null)
                {
                    pnlInvitado.Visible = true;
                    pnlPerfil.Visible = false;
                    return;
                }

                pnlInvitado.Visible = false;
                pnlPerfil.Visible = true;

                if (!IsPostBack)
                {
                    txtNombre.Text = user.Nombre;
                    txtApellido.Text = user.Apellido;
                    txtImagen.Text = user.UrlImagenPerfil;

                    imgPerfil.Src = string.IsNullOrWhiteSpace(user.UrlImagenPerfil)
                        ? ResolveUrl("~/img/no-image.png")
                        : user.UrlImagenPerfil;
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("~/Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var user = Session["Usuario"] as Usuario;
                if (user == null)
                {
                    // Si se expiró la sesión entre medio
                    pnlInvitado.Visible = true;
                    pnlPerfil.Visible = false;
                    return;
                }

                user.Nombre = (txtNombre.Text ?? "").Trim();
                user.Apellido = (txtApellido.Text ?? "").Trim();
                user.UrlImagenPerfil = (txtImagen.Text ?? "").Trim();

                new UsuarioDAL().ActualizarPerfil(user);

                // actualizar sesión
                Session["Usuario"] = user;

                Response.Redirect("~/MiPerfil.aspx", false);
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