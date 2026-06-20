using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using conexion;

namespace Tpfinalc_Nivel3
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarMarcas();
                    CargarCategorias();
                    CargarArticulos();
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarArticulos();
        }

        private void CargarArticulos()
        {
            try
            {
                ArticuloDAL dal = new ArticuloDAL();
                var lista = dal.Listar(
                    txtBuscar.Text,
                    ddlMarca.SelectedValue,
                    ddlCategoria.SelectedValue
                );

                repArticulos.DataSource = lista;
                repArticulos.DataBind();

                pnlSinResultados.Visible = (lista.Count == 0);

                if (lista.Count == 0)
                {
                    string marcaTxt = ddlMarca.SelectedItem?.Text ?? "esa marca";
                    string catTxt = ddlCategoria.SelectedItem?.Text ?? "esa categoria";

                    if (ddlMarca.SelectedValue != "0" && ddlCategoria.SelectedValue != "0")
                        lblSinResultados.Text = $"No hay articulos de <b>{marcaTxt}</b> en <b>{catTxt}</b> en este momento.";
                    else if (ddlMarca.SelectedValue != "0")
                        lblSinResultados.Text = $"No hay articulos de la marca <b>{marcaTxt}</b> en este momento.";
                    else if (ddlCategoria.SelectedValue != "0")
                        lblSinResultados.Text = $"No hay articulos de la categoria <b>{catTxt}</b> en este momento.";
                    else
                        lblSinResultados.Text = "No hay articulos para mostrar en este momento.";
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarMarcas()
        {
            ddlMarca.DataSource = new MarcaDAL().Listar();
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("Todas", "0"));
        }

        private void CargarCategorias()
        {
            ddlCategoria.DataSource = new CategoriaDAL().Listar();
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Todas", "0"));
        }

        protected string GetImagenProducto(object imagenUrl)
        {
            string url = imagenUrl?.ToString();
            return string.IsNullOrWhiteSpace(url) ? ResolveUrl("~/img/no-image.png") : url.Trim();
        }
    }
}
