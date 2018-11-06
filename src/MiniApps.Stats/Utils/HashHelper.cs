using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiniApps.Stats.Utils
{
    public static class HashHelper
    {
        public static string GetMD5(string value, string formatter = "x2")
        {
            if(string.IsNullOrEmpty(value))
            {
                return null;
            }

            using(var md5 = MD5.Create())
            {
                byte[] hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

                StringBuilder builder = new StringBuilder();

                foreach(var byteValue in hashedBytes)
                {
                    builder.Append(byteValue.ToString(formatter));
                }
                return builder.ToString();
            }
        }
    }
}
