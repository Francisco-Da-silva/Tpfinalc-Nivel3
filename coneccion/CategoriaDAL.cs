using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conexion
{
    public class CategoriaDAL
    {
        private string connectionString =
"Server=.\\SQLEXPRESS;Database=CATALOGO_WEB_DB;Trusted_Connection=True;TrustServerCertificate=True";

        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            string query = "SELECT Id, Descripcion FROM CATEGORIAS ORDER BY Descripcion";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Categoria
                        {
                            Id = (int)dr["Id"],
                            Descripcion = dr["Descripcion"]?.ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }
}