using conexion;
using Dominio;

using System;

namespace Tpfinalc_Nivel3
{
    public partial class ProductoForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarMarcas();
                    CargarCategorias();

                    if (Request.QueryString["id"] != null)
                    {
                        lblTitulo.Text = "Editar Producto";
                        int id = int.Parse(Request.QueryString["id"]);
                        CargarArticulo(id);
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void CargarArticulo(int id)
        {
            ArticuloDAL dal = new ArticuloDAL();
            Articulo art = dal.ObtenerPorId(id);

            txtCodigo.Text = art.Codigo;
            txtNombre.Text = art.Nombre;
            txtDescripcion.Text = art.Descripcion;
            txtPrecio.Text = art.Precio.ToString();
            txtImagenUrl.Text = art.ImagenUrl;

            ddlMarca.SelectedValue = art.IdMarca.ToString();
            ddlCategoria.SelectedValue = art.IdCategoria.ToString();

            imgProducto.ImageUrl = art.ImagenUrl;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo art = new Articulo();

                art.Codigo = txtCodigo.Text;
                art.Nombre = txtNombre.Text;
                art.Descripcion = txtDescripcion.Text;
                art.Precio = decimal.Parse(txtPrecio.Text);
                art.IdMarca = int.Parse(ddlMarca.SelectedValue);
                art.IdCategoria = int.Parse(ddlCategoria.SelectedValue);
                art.ImagenUrl = txtImagenUrl.Text;

                ArticuloDAL dal = new ArticuloDAL();

                if (Request.QueryString["id"] != null)
                {
                    art.Id = int.Parse(Request.QueryString["id"]);
                    dal.AdminModificar(art);
                }
                else
                {
                    dal.AdminAgregar(art);
                }

                Response.Redirect("AdminProductos.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgProducto.ImageUrl = txtImagenUrl.Text;
        }

        private void CargarMarcas()
        {
            MarcaDAL dal = new MarcaDAL();
            ddlMarca.DataSource = dal.Listar();
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
        }

        private void CargarCategorias()
        {
            CategoriaDAL dal = new CategoriaDAL();
            ddlCategoria.DataSource = dal.Listar();
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
        }
    }
}