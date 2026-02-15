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
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarFavoritos();
        }

        private void CargarFavoritos()
        {
            var usuario = (Usuario)Session["Usuario"];

            FavoritoDAL dal = new FavoritoDAL();
            repFavoritos.DataSource = dal.ListarPorUsuario(usuario.Id);
            repFavoritos.DataBind();
        }

        protected void btnQuitar_Command(object sender, CommandEventArgs e)
        {
            var usuario = (Usuario)Session["Usuario"];

            int idArticulo = int.Parse(e.CommandArgument.ToString());

            FavoritoDAL dal = new FavoritoDAL();
            dal.Eliminar(usuario.Id, idArticulo);

            CargarFavoritos();
        }
    }
}