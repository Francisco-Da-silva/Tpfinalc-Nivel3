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

    public class MarcaDAL
    {
        private string connectionString =
            "Server=.\\SQLEXPRESS;Database=CATALOGO_WEB_DB;Trusted_Connection=True;TrustServerCertificate=True";

        public List<Marca> Listar()
        {
            try
            {
                List<Marca> lista = new List<Marca>();

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_ListarMarcas", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Marca
                            {
                                Id = (int)dr["Id"],
                                Descripcion = dr["Descripcion"]?.ToString()
                            });
                        }
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en MarcaDAL.Listar (SP_ListarMarcas)", ex);
            }
        }
    }
}