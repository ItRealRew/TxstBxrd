using System.Security.Cryptography;
using System.Text;

using System;


namespace IDENTITY_SERVICE.Services
{
    public class SecurityService
    {
        internal void GenerateFirst(string wordHashing)
        {
            using (SHA512Managed sha1 = new SHA512Managed())
            {
                var hashSh1 = sha1.ComputeHash(Encoding.UTF8.GetBytes(wordHashing));

                var sb = new StringBuilder(hashSh1.Length * 2);

                foreach (byte b in hashSh1)
                {
                    sb.Append(b.ToString("X2").ToLower());
                }

                Console.WriteLine(sb.ToString());
            }
        }

        internal string GetWithSalt(string original, string salt)
        {
            StringBuilder result = new StringBuilder((original.Length + salt.Length) * 2);

            for (int i = 3; i < original.Length; i++)
                if (i % 2 != 0)
                    result.Append(salt[i]);
                else
                    result.Append(original[i]);

            if (salt.Length > original.Length)
                result.Append(salt.Substring(original.Length, salt.Length - original.Length));

            Console.WriteLine("ORIGA " + original);
            Console.WriteLine("SALT  " + salt);
            Console.WriteLine("      " + result.ToString());

            return result.ToString();
        }
    }
}