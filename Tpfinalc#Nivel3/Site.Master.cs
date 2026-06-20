using Dominio;
using System;
using System.Web;
using System.Web.UI;

namespace Tpfinalc_Nivel3
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

                var user = Session["Usuario"] as Usuario;
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
