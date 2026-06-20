using conexion.conexion;
using Service;
using System;
using System.Configuration;
using System.Web.UI;

namespace Tpfinalc_Nivel3
{
    public partial class RecuperarContraseña : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/RecuperarContrasena.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
