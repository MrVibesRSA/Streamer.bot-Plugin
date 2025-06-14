using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrVibesRSA.StreamerbotPlugin.Utilities
{
    public static class EncryptionHelper
    {
        public static string GenerateAuthenticationString(string password, string salt, string challenge)
        {
            // Check for empty inputs
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(challenge))
            {
                // Debug.LogError("Password, Salt, or Challenge is empty.");
                return string.Empty;
            }

            // Step 1: Concatenate password and salt
            string passwordSaltConcat = password + salt;

            // Step 2: Generate SHA-256 binary hash of the result
            byte[] passwordSaltHashByte = SHA256Hash(passwordSaltConcat);

            // Step 3: Base64 encode the SHA-256 hash
            string base64Secret = Convert.ToBase64String(passwordSaltHashByte);

            // Step 4: Concatenate the base64_secret with the challenge
            string secretChallengeConcat = base64Secret + challenge;

            // Step 5: Generate SHA-256 hash of this result
            byte[] finalHashByte = SHA256Hash(secretChallengeConcat);
            return Convert.ToBase64String(finalHashByte);
        }

        // SHA-256 method for hashing
        private static byte[] SHA256Hash(string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }
    }
}
