using System.Security.Cryptography;
using System.Text;


namespace Security
{
    public class CriptoSevice
    {
        public string GetSecurePassword(string original, string salt, int repeatSalt)
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

        public string GetUniqueKey(int maxSize, string alphabet)
        {
            byte[] data = new byte[1];
            using (var crypto =  RandomNumberGenerator.Create())
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
            using (SHA512 security = SHA512.Create())
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