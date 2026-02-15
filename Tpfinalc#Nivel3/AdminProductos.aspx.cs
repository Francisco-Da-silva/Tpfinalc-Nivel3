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
                    return;

                if (!IsPostBack)
                {
                    // tu lógica normal
                    CargarMarcas();
                    CargarCategorias();
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("~/Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try { CargarGrilla(); }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("../Error.aspx", false);
            }
        }

        private void CargarGrilla()
        {
            try
            {
                ArticuloDAL dal = new ArticuloDAL();
                gvArticulos.DataSource = dal.AdminListar(txtBuscar.Text, ddlMarca.SelectedValue, ddlCategoria.SelectedValue);
                gvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar grilla de productos.", ex);
            }
        }

        protected void gvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    new ArticuloDAL().AdminEliminar(id);
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("../Error.aspx", false);
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