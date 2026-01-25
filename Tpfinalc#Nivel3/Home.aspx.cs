using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using conexion;

namespace Tpfinalc_Nivel3
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMarcas();
                CargarCategorias();
                CargarArticulos();
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarArticulos();
        }

        private void CargarArticulos()
        {
            ArticuloDAL dal = new ArticuloDAL();

            repArticulos.DataSource = dal.Listar(
                txtBuscar.Text,
                ddlMarca.SelectedValue,
                ddlCategoria.SelectedValue
            );

            repArticulos.DataBind();
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