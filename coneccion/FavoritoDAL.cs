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
        public class FavoritoDAL
        {
            private string connectionString =
                "Server=.\\SQLEXPRESS;Database=CATALOGO_WEB_DB;Trusted_Connection=True;TrustServerCertificate=True";

            public void Agregar(int idUser, int idArticulo)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO FAVORITOS (IdUser, IdArticulo) VALUES (@u, @a)", con))
                    {
                        cmd.Parameters.Add("@u", SqlDbType.Int).Value = idUser;
                        cmd.Parameters.Add("@a", SqlDbType.Int).Value = idArticulo;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en FavoritoDAL.Agregar", ex);
                }
            }

            public List<Articulo> ListarPorUsuario(int idUser)
            {
                try
                {
                    List<Articulo> lista = new List<Articulo>();

                    string query = @"
                    SELECT A.Id, A.Nombre, A.ImagenUrl, A.Precio
                    FROM ARTICULOS A
                    INNER JOIN FAVORITOS F ON A.Id = F.IdArticulo
                    WHERE F.IdUser = @idUser";

                    using (SqlConnection con = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@idUser", SqlDbType.Int).Value = idUser;

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new Articulo
                                {
                                    Id = (int)dr["Id"],
                                    Nombre = dr["Nombre"].ToString(),
                                    ImagenUrl = dr["ImagenUrl"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"])
                                });
                            }
                        }
                    }

                    return lista;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en FavoritoDAL.ListarPorUsuario", ex);
                }
            }

            public void Eliminar(int idUser, int idArticulo)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(
                        "DELETE FROM FAVORITOS WHERE IdUser=@u AND IdArticulo=@a", con))
                    {
                        cmd.Parameters.Add("@u", SqlDbType.Int).Value = idUser;
                        cmd.Parameters.Add("@a", SqlDbType.Int).Value = idArticulo;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en FavoritoDAL.Eliminar", ex);
                }
            }
        }
    }
