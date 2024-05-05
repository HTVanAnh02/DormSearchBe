using DormSearchBe.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.Helpers
{
    public class Helper
    {
        public static string KeyString { get; set; } = string.Empty;
        public static string hashPassword(string password)
        {
            if (password != null)
            {
                var saltBytes = Encoding.ASCII.GetBytes(KeyString);
                var passwordBytes = Encoding.ASCII.GetBytes(password);
                var hmac = new HMACSHA256(saltBytes);
                var hash = hmac.ComputeHash(passwordBytes);
                var hexHash = BitConverter.ToString(hash).Replace("-", "");
                return hexHash;
            }
            return "";

        }
        public static bool verifyPassword(string password, string hashedPassword)
        {
            if (password != null)
            {
                string passwordInput = hashPassword(password);
                if (passwordInput == hashedPassword)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public static string GenerateToken(User user)
        {
            return "";
        }
    }
}
