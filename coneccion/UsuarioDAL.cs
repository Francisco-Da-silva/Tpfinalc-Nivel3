using coneccion;
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
        public Usuario Login(string email, string passHash)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT Id, email, nombre, apellido, urlImagenPerfil, admin FROM dbo.USERS WHERE email=@Email AND pass=@Pass", cn))
            {
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (email ?? "").Trim();
                cmd.Parameters.Add("@Pass", SqlDbType.VarChar, 200).Value = (passHash ?? "").Trim(); // ajustá el tamaño según tu DB

                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return null;

                    return new Usuario
                    {
                        Id = dr["Id"] == DBNull.Value ? 0 : (int)dr["Id"],
                        Email = dr["email"]?.ToString(),
                        Nombre = dr["nombre"] as string,
                        Apellido = dr["apellido"] as string,
                        UrlImagenPerfil = dr["urlImagenPerfil"] as string,
                        Admin = dr["admin"] != DBNull.Value && (bool)dr["admin"]
                    };
                }
            }
        }

        public void Registrar(Usuario u)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.Cadena))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_RegistrarUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (u.Email ?? "").Trim();
                    cmd.Parameters.Add("@Pass", SqlDbType.VarChar, 200).Value = (u.Pass ?? "").Trim(); // ajustá tamaño

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
                using (SqlConnection con = new SqlConnection(Conexion.Cadena))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_ActualizarPerfil", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;

                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value =
                        string.IsNullOrWhiteSpace(user.Nombre) ? (object)DBNull.Value : user.Nombre.Trim();

                    cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value =
                        string.IsNullOrWhiteSpace(user.Apellido) ? (object)DBNull.Value : user.Apellido.Trim();

                    cmd.Parameters.Add("@UrlImagenPerfil", SqlDbType.VarChar, 500).Value =
                        string.IsNullOrWhiteSpace(user.UrlImagenPerfil) ? (object)DBNull.Value : user.UrlImagenPerfil.Trim();

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
                byte[] bytes = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(bytes);
                }

                string token = Convert.ToBase64String(bytes)
                    .Replace("+", "-")
                    .Replace("/", "_")
                    .Replace("=", "");

                byte[] tokenHash = Sha256(token);
                DateTime expira = DateTime.Now.AddMinutes(minutosVigencia);

                using (SqlConnection con = new SqlConnection(Conexion.Cadena))
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

                using (SqlConnection con = new SqlConnection(Conexion.Cadena))
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

                using (SqlConnection con = new SqlConnection(Conexion.Cadena))
                using (SqlCommand cmd = new SqlCommand("dbo.SP_Reset_Confirmar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pHash = cmd.Parameters.Add("@TokenHash", SqlDbType.VarBinary, 32);
                    pHash.Value = tokenHash;

                    cmd.Parameters.Add("@NuevaPass", SqlDbType.VarChar, 200).Value = (nuevaPass ?? "").Trim(); // ajustá tamaño

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en UsuarioDAL.ConfirmarReset", ex);
            }
        }

        public bool CambiarPasswordPorEmail(string email, string passHash)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE dbo.USERS SET pass = @Pass WHERE email = @Email", cn))
            {
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (email ?? "").Trim();
                cmd.Parameters.Add("@Pass", SqlDbType.VarChar, 200).Value = (passHash ?? "").Trim(); // ajustá tamaño

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private static byte[] Sha256(string input)
        {
            using (var sha = SHA256.Create())
                return sha.ComputeHash(Encoding.UTF8.GetBytes(input ?? ""));
        }
    }
}