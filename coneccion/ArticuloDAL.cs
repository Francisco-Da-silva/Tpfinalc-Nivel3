using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace conexion

{

    public class ArticuloDAL
    {
        private string connectionString =
            "Server=.\\SQLEXPRESS;Database=CATALOGO_WEB_DB;Trusted_Connection=True;TrustServerCertificate=True";

        public List<Articulo> Listar(string texto, string idMarcaStr, string idCategoriaStr)
        {
            try
            {
                List<Articulo> lista = new List<Articulo>();

                int idMarca = int.TryParse(idMarcaStr, out var m) ? m : 0;
                int idCategoria = int.TryParse(idCategoriaStr, out var c) ? c : 0;

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_ListarArticulos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Texto", SqlDbType.VarChar, 50).Value = (texto ?? "").Trim();
                    cmd.Parameters.Add("@IdMarca", SqlDbType.Int).Value = idMarca;
                    cmd.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = idCategoria;

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Articulo
                            {
                                Id = (int)dr["Id"],
                                Codigo = dr["Codigo"]?.ToString(),
                                Nombre = dr["Nombre"]?.ToString(),
                                Descripcion = dr["Descripcion"]?.ToString(),
                                IdMarca = dr["IdMarca"] == DBNull.Value ? 0 : (int)dr["IdMarca"],
                                IdCategoria = dr["IdCategoria"] == DBNull.Value ? 0 : (int)dr["IdCategoria"],
                                ImagenUrl = dr["ImagenUrl"]?.ToString(),
                                Precio = dr["Precio"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Precio"])
                            });
                        }
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ArticuloDAL.Listar (SP_ListarArticulos)", ex);
            }
        }

        public Articulo ObtenerPorId(int id)
{
    try
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("dbo.SP_ArticuloPorId", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            con.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (!dr.Read())
                    return null;
                        return new Articulo
                        {
                            Id = id,
                            Codigo = dr["Codigo"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            ImagenUrl = dr["ImagenUrl"].ToString(),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            IdMarca = dr["IdMarca"] == DBNull.Value ? 0 : (int)dr["IdMarca"],
                            IdCategoria = dr["IdCategoria"] == DBNull.Value ? 0 : (int)dr["IdCategoria"],

                            // textos para mostrar
                            MarcaDescripcion = dr["MarcaDescripcion"].ToString(),
                            CategoriaDescripcion = dr["CategoriaDescripcion"].ToString()
                        };

                    }
                }
    }
    catch (Exception ex)
    {
        throw new Exception("Error en ArticuloDAL.ObtenerPorId", ex);
    }
}


        public List<Articulo> AdminListar(string texto, string idMarcaStr, string idCategoriaStr)
        {
            try
            {
                List<Articulo> lista = new List<Articulo>();
                int idMarca = int.TryParse(idMarcaStr, out var m) ? m : 0;
                int idCategoria = int.TryParse(idCategoriaStr, out var c) ? c : 0;

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_Admin_ListarArticulos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Texto", SqlDbType.VarChar, 50).Value = (texto ?? "").Trim();
                    cmd.Parameters.Add("@IdMarca", SqlDbType.Int).Value = idMarca;
                    cmd.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = idCategoria;

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Articulo
                            {
                                Id = (int)dr["Id"],
                                Codigo = dr["Codigo"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                IdMarca = (int)dr["IdMarca"],
                                IdCategoria = (int)dr["IdCategoria"],
                                ImagenUrl = dr["ImagenUrl"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                MarcaDescripcion = dr["MarcaDescripcion"].ToString(),
                                CategoriaDescripcion = dr["CategoriaDescripcion"].ToString()
                            });
                        }
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ArticuloDAL.AdminListar", ex);
            }
        }

        public Articulo AdminObtenerPorId(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_Admin_ArticuloPorId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read()) return null;

                        return new Articulo
                        {
                            Id = (int)dr["Id"],
                            Codigo = dr["Codigo"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            IdMarca = (int)dr["IdMarca"],
                            IdCategoria = (int)dr["IdCategoria"],
                            ImagenUrl = dr["ImagenUrl"].ToString(),
                            Precio = Convert.ToDecimal(dr["Precio"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ArticuloDAL.AdminObtenerPorId", ex);
            }
        }

        public void AdminAgregar(Articulo a)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_Admin_AgregarArticulo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 50).Value = a.Codigo;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = a.Nombre;
                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 150).Value = a.Descripcion;
                    cmd.Parameters.Add("@IdMarca", SqlDbType.Int).Value = a.IdMarca;
                    cmd.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = a.IdCategoria;
                    cmd.Parameters.Add("@ImagenUrl", SqlDbType.VarChar, 1000).Value = a.ImagenUrl;
                    cmd.Parameters.Add("@Precio", SqlDbType.Money).Value = a.Precio;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ArticuloDAL.AdminAgregar", ex);
            }
        }

        public void AdminModificar(Articulo a)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_Admin_ModificarArticulo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = a.Id;
                    cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 50).Value = a.Codigo;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = a.Nombre;
                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 150).Value = a.Descripcion;
                    cmd.Parameters.Add("@IdMarca", SqlDbType.Int).Value = a.IdMarca;
                    cmd.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = a.IdCategoria;
                    cmd.Parameters.Add("@ImagenUrl", SqlDbType.VarChar, 1000).Value = a.ImagenUrl;
                    cmd.Parameters.Add("@Precio", SqlDbType.Money).Value = a.Precio;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ArticuloDAL.AdminModificar", ex);
            }
        }

        public void AdminEliminar(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_Admin_EliminarArticulo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ArticuloDAL.AdminEliminar", ex);
            }
        }
 

    }
}