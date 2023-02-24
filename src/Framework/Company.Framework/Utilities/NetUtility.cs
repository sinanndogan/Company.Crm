using System.Net;

namespace Company.Framework.Utilities
{
    public class NetUtility
    {
        public static string GetPublicIP()
        {
            string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            var externalIp = IPAddress.Parse(externalIpString);
            return externalIp.ToString();
        }
    }
}
