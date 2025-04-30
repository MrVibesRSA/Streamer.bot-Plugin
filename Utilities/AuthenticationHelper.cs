using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace MrVibesRSA.StreamerbotPlugin.Utilities
{
    public static class AuthenticationHelper
    {
        /// <summary>
        /// Generates a Streamer.bot compatible authentication string
        /// </summary>
        public static string GenerateAuthenticationString(string password, string salt, string challenge)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(challenge))
            {
                return string.Empty;
            }

            string passwordSaltConcat = password + salt;
            byte[] passwordSaltHashByte = SHA256Hash(passwordSaltConcat);
            string base64Secret = Convert.ToBase64String(passwordSaltHashByte);
            string secretChallengeConcat = base64Secret + challenge;
            byte[] finalHashByte = SHA256Hash(secretChallengeConcat);

            return Convert.ToBase64String(finalHashByte);
        }

        private static byte[] SHA256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }
    }
}