using RestSharp;
using System.Threading.Tasks;

namespace TravelApiClient.Models
{
    public class SecurityApiHelper
    {
        public static async Task<string> Login(string username, string password)
        {
        RestClient client = new RestClient("http://localhost:5004/api");
        RestRequest request = new RestRequest($"reviews?Username={username}&password={password}", Method.POST);
        request.AddHeader("Content-Type", "application/json");
        IRestResponse response = await client.ExecuteTaskAsync(request);
        return response.Content;
        }
    }
}