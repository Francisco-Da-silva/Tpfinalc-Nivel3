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
    public partial class Detalle : System.Web.UI.Page
    {
        private int IdArticulo
        {
            get
            {
                int.TryParse(Request.QueryString["id"], out int id);
                return id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["id"] == null)
                        throw new Exception("Producto no especificado.");

                    int id = int.Parse(Request.QueryString["id"]);
                    CargarDetalle(id);
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }


        private void CargarDetalle(int id)
        {
            try
            {
                ArticuloDAL dal = new ArticuloDAL();
                Articulo art = dal.ObtenerPorId(id);

                if (art == null)
                    throw new Exception("El producto no existe.");

                pnlDetalle.Visible = true;

                lblNombre.Text = art.Nombre;
                lblPrecio.Text = art.Precio.ToString("N2");
                lblCodigo.Text = art.Codigo;
                lblDescripcion.Text = art.Descripcion;

                imgProducto.Src = string.IsNullOrWhiteSpace(art.ImagenUrl)
                    ? ResolveUrl("~/img/no-image.png")
                    : art.ImagenUrl.Trim();

                lblMarca.Text = art.MarcaDescripcion;
                lblCategoria.Text = art.CategoriaDescripcion;

                ConfigurarFavorito(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar el detalle del producto.", ex);
            }
        }

        private void ConfigurarFavorito(int idArticulo)
        {
            Usuario user = Session["Usuario"] as Usuario;
            bool logueado = user != null;

            btnAgregarFavorito.Visible = logueado;
            lblFavoritoMsg.Text = logueado
                ? ""
                : "Inicia sesion para agregar este producto a favoritos.";
            lblFavoritoMsg.CssClass = logueado ? "d-block mt-3" : "d-block mt-3 text-muted";

            if (!logueado)
                return;

            bool yaEsFavorito = new FavoritoDAL().Existe(user.Id, idArticulo);
            btnAgregarFavorito.Enabled = !yaEsFavorito;
            btnAgregarFavorito.Text = yaEsFavorito ? "Ya esta en favoritos" : "Agregar a favoritos";
        }

        protected void btnAgregarFavorito_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = Session["Usuario"] as Usuario;
                if (user == null)
                {
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                if (IdArticulo <= 0)
                    throw new Exception("Producto no especificado.");

                FavoritoDAL dal = new FavoritoDAL();
                dal.Agregar(user.Id, IdArticulo);

                btnAgregarFavorito.Enabled = false;
                btnAgregarFavorito.Text = "Ya esta en favoritos";
                lblFavoritoMsg.CssClass = "d-block mt-3 text-success";
                lblFavoritoMsg.Text = "Producto agregado a tus favoritos.";
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

