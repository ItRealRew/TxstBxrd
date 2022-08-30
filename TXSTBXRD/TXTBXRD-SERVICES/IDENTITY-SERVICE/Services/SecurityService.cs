using System.Security.Cryptography;
using System.Text;
using System;


namespace IDENTITY_SERVICE.Services
{
    public class SecurityService
    {
        internal string GetSecurePassword(string original, string salt, int repeatSalt)
        {
            StringBuilder result = new StringBuilder(128 + (salt.Length));
            result.Append(original);
            result.Append(salt);

            result = Hash(result);

            while (repeatSalt != 0)
            {
                repeatSalt--;
                result.Append(salt);
                result = Hash(result);
            }

            return result.ToString();
        }

        internal string GetUniqueKey(int maxSize, string alphabet)
        {
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
                result.Append(alphabet[b % (alphabet.Length)]);

            return result.ToString();
        }

        private StringBuilder Hash(StringBuilder wordHashing)
        {
            using (SHA512Managed security = new SHA512Managed())
            {
                byte[] hashed = security.ComputeHash(Encoding.UTF8.GetBytes(wordHashing.ToString()));

                StringBuilder result = new StringBuilder(hashed.Length * 2);

                foreach (byte b in hashed)
                    result.Append(b.ToString("X2").ToLower());

                return result;
            }
        }
    }
}