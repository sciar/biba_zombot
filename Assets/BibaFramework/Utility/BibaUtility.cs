using System;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Net.NetworkInformation;

namespace BibaFramework.Utility
{
    public static class BibaUtility
    {
		public static bool CheckForInternetConnection()
		{
			return NetworkInterface.GetIsNetworkAvailable ();
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

