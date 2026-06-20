using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Tpfinalc_Nivel3
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            LimpiarCambioVistaMobile();
        }

        private void LimpiarCambioVistaMobile()
        {
            string rawUrl = Request.RawUrl ?? "";

            ExpirarCookie("__FriendlyUrls_SwitchViews");
            ExpirarCookie("__FriendlyUrls_SwitchView");

            if (rawUrl.IndexOf("__FriendlyUrls_SwitchView", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Response.Redirect("~/Home.aspx", false);
                CompleteRequest();
            }
        }

        private void ExpirarCookie(string nombre)
        {
            if (Request.Cookies[nombre] == null)
                return;

            var cookie = new HttpCookie(nombre)
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                Value = ""
            };

            Response.Cookies.Add(cookie);
        }
    }
}
