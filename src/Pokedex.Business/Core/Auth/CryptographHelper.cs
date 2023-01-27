using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Core.Auth
{
    public static class CryptographHelper
    {
        public static string EncryptToMD5(string text)
        {
            var hash = MD5.HashData(Encoding.UTF8.GetBytes(text));
            return hash.ToHexadecimal();
        }

        private static string ToHexadecimal(this byte[] bytes)
        {
            return string.Join(string.Empty, bytes.Select(b => b.ToString("x2")));
        }
    }
}
