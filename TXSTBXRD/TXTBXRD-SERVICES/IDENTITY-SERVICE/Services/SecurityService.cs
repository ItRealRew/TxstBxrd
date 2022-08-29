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


            Console.WriteLine("ORIGA " + original);
            Console.WriteLine("SALT  " + salt);
            Console.WriteLine("      " + result.ToString());
            return "Ок";
        }

        internal string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[80];
            chars =
            "abcdefghijklmnopqrstuvwxyz*&!#1234567890-=|:ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
                result.Append(chars[b % (chars.Length)]);
                
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