using RestSharp;
using System.Threading.Tasks;

namespace TravelApiClient.Models
{
  public class SecurityApiHelper
  {
    public static async Task<string> Login(string username, string password)
    {
      RestClient client = new RestClient("http://localhost:5004/api");
      RestRequest request = new RestRequest($"security/login", Method.POST);
      request.AddJsonBody(new{Name = username, Password = password});
      request.AddHeader("Content-Type", "application/json");
      IRestResponse response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }

    public static async Task Register(string username, string password,string passwordmatch)
    {
      RestClient client = new RestClient("http://localhost:5004/api");
      RestRequest request = new RestRequest($"security/register", Method.POST);
      request.AddJsonBody(new { Name = username, Password = password,ConfirmPassword = passwordmatch });
      request.AddHeader("Content-Type", "application/json");
      IRestResponse response = await client.ExecuteTaskAsync(request);
    }
  }
}