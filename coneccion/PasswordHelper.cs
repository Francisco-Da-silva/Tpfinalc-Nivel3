using System;
using System.Security.Cryptography;
using System.Text;

namespace conexion
{
    public static class PasswordHelper
    {
        public static string HashPasswordSha256(string plain)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(plain ?? "");
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
