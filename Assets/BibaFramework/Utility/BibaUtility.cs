using System;
using System.Net;
using System.Security.Cryptography;
using System.IO;

namespace BibaFramework.Utility
{
    public static class BibaUtility
    {
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static string GetHashString(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return System.Text.Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }
    }
}

