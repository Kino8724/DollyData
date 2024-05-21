using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.ApplicationModel;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

namespace DollyData
{
    public static class UpdateHandler
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task CheckForUpdates()
        {
            try
            {
                string OnlineVersion = await GetOnlineVersion();
                string LocalVersion = GetAppVersion();

                Debug.WriteLine("Online Version: ",OnlineVersion);
                Debug.WriteLine("Local Version: ",LocalVersion);

                if (OnlineVersion != LocalVersion)
                {
                    Debug.WriteLine("There are new updates available");
                    DownloadUpdates();
                }
                else
                {
                    Debug.WriteLine("There are no updates available");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("There was an error trying to get the online version number");
                Debug.WriteLine(ex.Message);
            }
        }

        public static async Task<string> GetOnlineVersion()
        {
            try
            {
                string responseBody = await client.GetStringAsync(new Uri("https://rezappautoupdatetest.blob.core.windows.net/updates/version.txt"));
                return responseBody.Trim();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("There was an error trying to get the online version number");
                Debug.WriteLine(ex.Message);
                return ex.Message;
            }

        }
        public static string GetAppVersion()
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }
        public static void DownloadUpdates()
        {
            try
            {
                Debug.WriteLine("Downloaded updates successfully!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("There was an issue downloading updates");
                Debug.WriteLine(ex.Message);
            }


        }
    }
}
