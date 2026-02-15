using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tpfinalc_Nivel3
{
    public partial class Error : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Mostrar mensaje si existe
                if (!IsPostBack)
                {
                    string msg = Session["Error"] as string;
                    lblError.Text = string.IsNullOrWhiteSpace(msg)
                        ? "Ocurrió un error inesperado."
                        : msg;

                    // opcional: limpiar para que no quede “pegado”
                    Session["Error"] = null;
                }
            }
            catch
            {
                // Si incluso Error.aspx falla, no hagas redirect para evitar loop
            }
        }
    }
}