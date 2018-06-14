using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PowerbiAvailableTrialUsage
{
    public class Powerbi
    {
        public async Task<string> TryGetAvailableFeaturesAsync()
        {
            string result = "";
            string aadtoken = await TokenProvider.GetTokenCredentialsAsync();

            WebRequest request = WebRequest.Create($"https://api.powerbi.com/v1.0/myorg/availableFeatures(featureName='embedTrial')");

            request.Method = "GET";
            request.ContentLength = 0;
            request.Headers.Add("Authorization", $"Bearer {aadtoken}");

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync()))
                {
                    try
                    {
                        using (var reader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8))
                            result = reader.ReadToEnd();
                    }
                    finally
                    {
                        response.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}