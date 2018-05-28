using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SZPNUW.Base
{
    public static class SecurityService
    {
        public static string GetSHA256Hash(string input)
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
