using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tpfinalc_Nivel3
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Evita navbar "viejo" por cache
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetRevalidation(System.Web.HttpCacheRevalidation.AllCaches);

                var user = Session["Usuario"] as Dominio.Usuario;
                bool logueado = user != null;
                bool admin = logueado && user.Admin;

                liMiPerfil.Visible = logueado;
                liFavoritos.Visible = logueado;
                liRegistro.Visible = !logueado;

                liLogin.Visible = !logueado;
                liLogout.Visible = logueado;

                liAdminProductos.Visible = admin;
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
