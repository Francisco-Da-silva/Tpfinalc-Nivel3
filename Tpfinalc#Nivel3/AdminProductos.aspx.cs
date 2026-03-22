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
    public partial class AdminProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Seguridad.ValidarAdmin())
                {
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                if (!IsPostBack)
                {
                    CargarMarcas();
                    CargarCategorias();
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("~/Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("~/Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void CargarGrilla()
        {
            try
            {
                ArticuloDAL dal = new ArticuloDAL();

                gvArticulos.DataSource = dal.AdminListar(
                    txtBuscar.Text.Trim(),
                    ddlMarca.SelectedValue,
                    ddlCategoria.SelectedValue
                );

                gvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar la grilla de productos.", ex);
            }
        }

        protected void gvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Editar")
                {
                    Response.Redirect("FormularioArticulo.aspx?id=" + id, false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                if (e.CommandName == "Eliminar")
                {
                    ArticuloDAL dal = new ArticuloDAL();
                    dal.AdminEliminar(id);

                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("~/Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
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
    }
}