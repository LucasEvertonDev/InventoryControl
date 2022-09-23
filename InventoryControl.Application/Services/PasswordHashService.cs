using InventoryControl.Application.Interfaces;
using InventoryControl.Models.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace InventoryControl.Application.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public static string PASSWORD_HASH_KEY = "TBMP#MDUYVS@K@542KLÇPISVQHWWMWJWJt55NNNEK";

        public HashSalt EncryptPassword(string password)
        {
            var salt = Encoding.ASCII.GetBytes(PASSWORD_HASH_KEY);

            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return new HashSalt { Hash = encryptedPassw, Salt = salt };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="storedPassword"></param>
        /// <returns></returns>
        public bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            var salt = Encoding.ASCII.GetBytes(PASSWORD_HASH_KEY);

            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return encryptedPassw == storedPassword;
        }

        /// <summary>
        /// 
        /// </summary>
        private void GenerateSalt()
        {
            byte[] salt = new byte[128 / 8]; // Generate a 128-bit salt using a secure PRNG
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
        }
    }
}
