using System.Security.Cryptography;
using System.Text;

using System;


namespace IDENTITY_SERVICE.Services
{
    public class SecurityService
    {
        internal void GenerateFirst()
        {
            using (SHA512Managed sha1 = new SHA512Managed())
            {
                var hashSh1 = sha1.ComputeHash(Encoding.UTF8.GetBytes("root"));

                var sb = new StringBuilder(hashSh1.Length * 2);

                foreach (byte b in hashSh1)
                {
                    sb.Append(b.ToString("X2").ToLower());
                }

                Console.WriteLine(sb.ToString());
            }
        }

        internal void GetWithSalt(string original, string salt)
        {
            if (original.Length > salt.Length)
                salt = salt + new string('*', original.Length - salt.Length);
            else
                original = original + new string('*', salt.Length - original.Length);

            var result = original.ToCharArray();

            for (int i = 0; i < result.Length; i++)
            {
                if (i % 2 == 0)
                    result[i] = salt[i];
            }

            Console.WriteLine("ORIGA" + original);
            Console.WriteLine("SALT" + salt);
            Console.WriteLine(new string(result));
        }
    }
}