using Dominio;
using System;
using System.Net.Http;
using System.Web;

namespace conexion
{
    public static class Seguridad
    {
        private static void NoCache()
        {
            var r = HttpContext.Current.Response;

            r.Cache.SetCacheability(HttpCacheability.NoCache);
            r.Cache.SetNoStore();
            r.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            r.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        }

        /// <summary>
        /// Valida sesión Admin. Si no cumple, redirige a Login/Home.
        /// Retorna bool para que la página haga "return".
        /// </summary>
        public static bool ValidarAdmin()
        {
            NoCache();

            var context = HttpContext.Current;
            var user = context.Session["Usuario"] as Usuario;

            if (user == null)
            {
                context.Response.Redirect("~/Login.aspx", false);
                context.ApplicationInstance.CompleteRequest();
                return false;
            }

            if (!user.Admin)
            {
                context.Response.Redirect("~/Home.aspx", false);
                context.ApplicationInstance.CompleteRequest();
                return false;
            }

            return true;
        }
    }
}
