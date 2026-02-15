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

                imgProducto.Src = art.ImagenUrl;

                lblMarca.Text = art.MarcaDescripcion;
                lblCategoria.Text = art.CategoriaDescripcion;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar el detalle del producto.", ex);
            }
        }

    }

    }