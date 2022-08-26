using System.Security.Cryptography;
using System.Text;

using System;


namespace IDENTITY_SERVICE.Services
{
    public class SecurityService
    {
        internal string GenerateFirst(string wordHashing)
        {
            using (SHA512Managed security = new SHA512Managed())
            {
                byte[] hashed = security.ComputeHash(Encoding.UTF8.GetBytes(wordHashing));

                StringBuilder result = new StringBuilder(hashed.Length * 2);

                foreach (byte b in hashed)
                    result.Append(b.ToString("X2").ToLower());

                Console.WriteLine(result.ToString());

                return result.ToString();
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