using System;
using System.Web.UI;

namespace Tpfinalc_Nivel3
{
    public partial class ResetManual : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RedirigirARecuperacionPorMail();
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            RedirigirARecuperacionPorMail();
        }

        private void RedirigirARecuperacionPorMail()
        {
            Response.Redirect("~/RecuperarContrasena.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
