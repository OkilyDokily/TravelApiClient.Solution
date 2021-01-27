using RestSharp;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace TravelApiClient.Models
{
  public class ApiHelper
  {

    public static async Task<string> GetAll(string country, string city, string option)
    {
      RestClient client = new RestClient("http://localhost:5004/api");
      RestRequest request = new RestRequest($"reviews?country={country}&city={city}&option={option}", Method.GET);
      IRestResponse response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }

    public static async Task<string> Get(int id)
    {
      RestClient client = new RestClient("http://localhost:5004/api");
      RestRequest request = new RestRequest($"reviews/{id}", Method.GET);
      IRestResponse response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }

    public static async Task<string> GetRandom()
    {
      RestClient client = new RestClient("http://localhost:5004/api");
      RestRequest request = new RestRequest($"reviews/random", Method.GET);
      IRestResponse response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }

    public static async Task<string> GetPopular(string option)
    {
      RestClient client = new RestClient("http://localhost:5004/api");
      RestRequest request = new RestRequest($"reviews/popular?option={option}", Method.GET);
      IRestResponse response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }

    public static async Task Post(string newReview, Microsoft.AspNetCore.Http.HttpContext context)
    {
      RestClient client = new RestClient("http://localhost:5004/api");
      RestRequest request = new RestRequest($"reviews", Method.POST);
      request.AddHeader("Content-Type", "application/json");
   
      string cookie = context.Request.Cookies["CookieKeyJWT"];
      request.AddHeader("Authorization", "Bearer " + cookie);
      request.AddJsonBody(newReview);
      IRestResponse response = await client.ExecuteTaskAsync(request);
    }

    public static async Task Put(int id, string newReview,Microsoft.AspNetCore.Http.HttpContext context)
    {
      RestClient client = new RestClient("http://localhost:5004/api");
      RestRequest request = new RestRequest($"reviews/{id}", Method.PUT);
      request.AddHeader("Content-Type", "application/json");
      string cookie = context.Request.Cookies["CookieKeyJWT"];
      request.AddHeader("Authorization", "Bearer " + cookie);
      request.AddJsonBody(newReview);
      IRestResponse response = await client.ExecuteTaskAsync(request);
    }

    public static async Task Delete(int id, Microsoft.AspNetCore.Http.HttpContext context)
    {
      RestClient client = new RestClient("http://localhost:5004/api");
      RestRequest request = new RestRequest($"reviews/{id}", Method.DELETE);
      request.AddHeader("Content-Type", "application/json");
      string cookie = context.Request.Cookies["CookieKeyJWT"];
      request.AddHeader("Authorization", "Bearer " + cookie);
      IRestResponse response = await client.ExecuteTaskAsync(request);
    }
  }
}