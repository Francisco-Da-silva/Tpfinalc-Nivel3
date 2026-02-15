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
            // prueba de usuarios seccion
            //var user = Session["Usuario"] as Dominio.Usuario;
            //Response.Write("Usuario en sesión: " + (user == null ? "NULL" : user.Email)); 

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

                    // notificacion de sin resultados
                    pnlSinResultados.Visible = (lista.Count == 0);

                    if (lista.Count == 0)
                    {
                        string marcaTxt = ddlMarca.SelectedItem?.Text ?? "esa marca";
                        string catTxt = ddlCategoria.SelectedItem?.Text ?? "esa categoría";

                        if (ddlMarca.SelectedValue != "0" && ddlCategoria.SelectedValue != "0")
                            lblSinResultados.Text = $"No hay artículos de <b>{marcaTxt}</b> en <b>{catTxt}</b> en este momento.";
                        else if (ddlMarca.SelectedValue != "0")
                            lblSinResultados.Text = $"No hay artículos de la marca <b>{marcaTxt}</b> en este momento.";
                        else if (ddlCategoria.SelectedValue != "0")
                            lblSinResultados.Text = $"No hay artículos de la categoría <b>{catTxt}</b> en este momento.";
                        else
                            lblSinResultados.Text = "No hay artículos para mostrar en este momento.";
                    }
                }
                catch (Exception ex)
                {
                    Session["Error"] = ex.Message;
                    Response.Redirect("Error.aspx", false);
                }
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