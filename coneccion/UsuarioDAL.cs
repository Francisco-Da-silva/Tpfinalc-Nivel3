using Dominio;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace conexion
{
    public class UsuarioDAL
    {
        private readonly string connectionString =
            "Server=.\\SQLEXPRESS;Database=CATALOGO_WEB_DB;Trusted_Connection=True;TrustServerCertificate=True";

        public Usuario Login(string email, string pass)
        {
            try
            {
                const string query = @"
                    SELECT Id, email, pass, nombre, apellido, urlImagenPerfil, admin
                    FROM USERS
                    WHERE email = @email AND pass = @pass
                ";

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = (email ?? "").Trim();
                    cmd.Parameters.Add("@pass", SqlDbType.VarChar, 20).Value = (pass ?? "").Trim();

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read())
                            return null;

                        return new Usuario
                        {
                            Id = (int)dr["Id"],
                            Email = dr["email"]?.ToString(),
                            Pass = dr["pass"]?.ToString(),
                            Nombre = dr["nombre"] == DBNull.Value ? null : dr["nombre"].ToString(),
                            Apellido = dr["apellido"] == DBNull.Value ? null : dr["apellido"].ToString(),
                            UrlImagenPerfil = dr["urlImagenPerfil"] == DBNull.Value ? null : dr["urlImagenPerfil"].ToString(),
                            Admin = dr["admin"] != DBNull.Value && Convert.ToBoolean(dr["admin"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en UsuarioDAL.Login", ex);
            }
        }

        public void Registrar(Usuario u)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_RegistrarUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (u.Email ?? "").Trim();
                    cmd.Parameters.Add("@Pass", SqlDbType.VarChar, 20).Value = (u.Pass ?? "").Trim();

                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value =
                        string.IsNullOrWhiteSpace(u.Nombre) ? (object)DBNull.Value : u.Nombre.Trim();

                    cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value =
                        string.IsNullOrWhiteSpace(u.Apellido) ? (object)DBNull.Value : u.Apellido.Trim();

                    cmd.Parameters.Add("@UrlImagenPerfil", SqlDbType.VarChar, 500).Value =
                        string.IsNullOrWhiteSpace(u.UrlImagenPerfil) ? (object)DBNull.Value : u.UrlImagenPerfil.Trim();

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en UsuarioDAL.Registrar (SP_RegistrarUsuario)", ex);
            }
        }



        public void ActualizarPerfil(Usuario user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("SP_ActualizarPerfil", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value =
                        (object)user.Nombre ?? DBNull.Value;
                    cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value =
                        (object)user.Apellido ?? DBNull.Value;
                    cmd.Parameters.Add("@UrlImagenPerfil", SqlDbType.VarChar, 500).Value =
                        (object)user.UrlImagenPerfil ?? DBNull.Value;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar perfil", ex);
            }
        }
        public string CrearTokenReset(string email, int minutosVigencia)
        {
            try
            {
                // Generar bytes aleatorios seguros
                byte[] bytes = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(bytes);
                }

                // Token URL-safe
                string token = Convert.ToBase64String(bytes)
                    .Replace("+", "-")
                    .Replace("/", "_")
                    .Replace("=", "");

                byte[] tokenHash = Sha256(token);
                DateTime expira = DateTime.Now.AddMinutes(minutosVigencia);

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_Reset_CreateToken", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (email ?? "").Trim();

                    var pHash = cmd.Parameters.Add("@TokenHash", SqlDbType.VarBinary, 32);
                    pHash.Value = tokenHash;

                    cmd.Parameters.Add("@Expira", SqlDbType.DateTime).Value = expira;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return token;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en UsuarioDAL.CrearTokenReset", ex);
            }
        }

        public bool TokenValido(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    return false;

                byte[] tokenHash = Sha256(token);

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_Reset_ValidarToken", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pHash = cmd.Parameters.Add("@TokenHash", SqlDbType.VarBinary, 32);
                    pHash.Value = tokenHash;

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        return dr.Read();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en UsuarioDAL.TokenValido", ex);
            }
        }

        public void ConfirmarReset(string token, string nuevaPass)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    throw new Exception("Token inválido.");

                byte[] tokenHash = Sha256(token);

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_Reset_Confirmar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pHash = cmd.Parameters.Add("@TokenHash", SqlDbType.VarBinary, 32);
                    pHash.Value = tokenHash;

                    cmd.Parameters.Add("@NuevaPass", SqlDbType.VarChar, 20).Value = (nuevaPass ?? "").Trim();

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en UsuarioDAL.ConfirmarReset", ex);
            }
        }

        private static byte[] Sha256(string input)
        {
            using (var sha = SHA256.Create())
                return sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        }
    }
}
