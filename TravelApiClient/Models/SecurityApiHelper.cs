using RestSharp;
using System.Threading.Tasks;
using System;

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
  }
}