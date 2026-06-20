using coneccion;
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
            

            public void Agregar(int idUser, int idArticulo)
            {
                try
                {
                using (SqlConnection con = new SqlConnection(Conexion.Cadena))
                using (SqlCommand cmd = new SqlCommand(@"
                        IF NOT EXISTS (
                            SELECT 1 FROM FAVORITOS WHERE IdUser = @u AND IdArticulo = @a
                        )
                        BEGIN
                            INSERT INTO FAVORITOS (IdUser, IdArticulo) VALUES (@u, @a)
                        END", con))
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

            public bool Existe(int idUser, int idArticulo)
            {
                try
                {
                using (SqlConnection con = new SqlConnection(Conexion.Cadena))
                using (SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(1) FROM FAVORITOS WHERE IdUser=@u AND IdArticulo=@a", con))
                    {
                        cmd.Parameters.Add("@u", SqlDbType.Int).Value = idUser;
                        cmd.Parameters.Add("@a", SqlDbType.Int).Value = idArticulo;

                        con.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en FavoritoDAL.Existe", ex);
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

                using (SqlConnection con = new SqlConnection(Conexion.Cadena))
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
                using (SqlConnection con = new SqlConnection(Conexion.Cadena))
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
