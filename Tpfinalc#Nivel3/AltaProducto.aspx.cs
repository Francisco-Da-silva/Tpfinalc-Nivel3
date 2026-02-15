using conexion;
using Dominio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tpfinalc_Nivel3
{
    public partial class AltaProducto : System.Web.UI.Page
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
                    
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("~/Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void CargarMarcas()
    {
        try
        {
            ddlMarca.DataSource = new MarcaDAL().Listar();
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
        }
        catch (Exception ex)
        {
            throw new Exception("Error al cargar marcas.", ex);
        }
    }

    private void CargarCategorias()
    {
        try
        {
            ddlCategoria.DataSource = new CategoriaDAL().Listar();
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
        }
        catch (Exception ex)
        {
            throw new Exception("Error al cargar categorías.", ex);
        }
    }

    protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
    {
        try
        {
            imgPreview.Src = string.IsNullOrWhiteSpace(txtImagenUrl.Text)
                ? "../img/no-image.png"
                : txtImagenUrl.Text.Trim();
        }
        catch (Exception ex)
        {
            Session["Error"] = ex.Message;
            Response.Redirect("../Error.aspx", false);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            // Validaciones
            if (txtCodigo.Text.Trim() == "" || txtNombre.Text.Trim() == "")
            {
                lblMsg.Text = "Código y Nombre son obligatorios.";
                return;
            }

            if (ddlMarca.SelectedValue == "0" || ddlCategoria.SelectedValue == "0")
            {
                lblMsg.Text = "Debe seleccionar Marca y Categoría.";
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text.Replace(",", "."),
                NumberStyles.Any, CultureInfo.InvariantCulture, out decimal precio))
            {
                lblMsg.Text = "Precio inválido.";
                return;
            }

            Articulo art = new Articulo
            {
                Codigo = txtCodigo.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                IdMarca = int.Parse(ddlMarca.SelectedValue),
                IdCategoria = int.Parse(ddlCategoria.SelectedValue),
                ImagenUrl = txtImagenUrl.Text.Trim(),
                Precio = precio
            };

            new ArticuloDAL().AdminAgregar(art);
            Response.Redirect("AltaProducto.aspx", false);
        }
        catch (Exception ex)
        {
            Session["Error"] = ex.Message;
            Response.Redirect("../Error.aspx", false);
        }
    }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/AdminProductos.aspx", false);
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("~/Error.aspx", false);
            }
        }

    }
}