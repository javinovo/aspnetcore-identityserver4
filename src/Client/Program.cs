using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args) =>
            MainAsync(args).GetAwaiter().GetResult();

        const string Usage = "You must pass either 'client' or 'user' as parameter";

        static async Task MainAsync(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(Usage);
                return;
            }

            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            // request token
            TokenResponse tokenResponse = null;

            switch (args[0].ToLower())
            {
                case "client":
                    tokenResponse = await GetClientCredentialTokenAsync(disco);
                    break;
                case "user":
                    tokenResponse = await GetResourceOwnerCredentialTokenAsync(disco);
                    break;
                default:
                    Console.WriteLine(Usage);
                    return;
            }

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine("Token:");
            Console.WriteLine(tokenResponse.Json);

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/api/identity");
            if (!response.IsSuccessStatusCode)
                Console.WriteLine($"Unsuccessful: {response.StatusCode}");
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response content:");
                Console.WriteLine(JArray.Parse(content));
            }
        }

        /// <summary>
        /// Obtains a token representing the whole client
        /// </summary>
        static async Task<TokenResponse> GetClientCredentialTokenAsync(DiscoveryResponse discovery) =>

            await new TokenClient(discovery.TokenEndpoint, "client", "secret")
                .RequestClientCredentialsAsync(scope: "api1");

        /// <summary>
        /// Obtains a token representing a particular user
        /// </summary>
        static async Task<TokenResponse> GetResourceOwnerCredentialTokenAsync(DiscoveryResponse discovery) =>

            await new TokenClient(discovery.TokenEndpoint, "ro.client", "secret")
                .RequestResourceOwnerPasswordAsync("alice", "password", scope: "api1");
    }
}
