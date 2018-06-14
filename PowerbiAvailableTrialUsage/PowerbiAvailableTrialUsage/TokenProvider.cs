using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PowerbiAvailableTrialUsage
{
    public static class TokenProvider
    {
        public static async Task<string> GetTokenCredentialsAsync()
        {
            string token = "";

            using (HttpClient client = new HttpClient())
            {
                var tokenEndpoint = $"https://login.windows.net/{TokenSettings.TenantId}/oauth2/token";
                var accept = "application/json";

                client.DefaultRequestHeaders.Add("Accept", accept);
                string postBody = null;

                postBody = $"resource=https%3A%2F%2Fanalysis.windows.net/powerbi/api&client_id={TokenSettings.ClientId}&grant_type=password&username={TokenSettings.Username}&password={TokenSettings.Password}&scope=openid";

                var tokenResult = await client.PostAsync(tokenEndpoint, new StringContent(postBody, Encoding.UTF8, "application/x-www-form-urlencoded"));
                tokenResult.EnsureSuccessStatusCode();

                var tokenData = await tokenResult.Content.ReadAsStringAsync();

                JObject parsedTokenData = JObject.Parse(tokenData);

                token = parsedTokenData["access_token"].Value<string>();
            }
            return token;
        }
    }
}