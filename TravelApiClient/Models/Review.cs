using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TravelApiClient.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
namespace TravelApiClient.Models
{
  public class Review
  {
    public int ReviewId { get; set; }
    [Range(0, 5, ErrorMessage = "rating must be between 0 and 5")]
    [Required]
    public Rating? Rating { get; set; }
    public string UserName { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string City { get; set; }

    public static async Task<List<Review>> GetReviews(string country, string city, string option)
    {
      string result = await ApiHelper.GetAll(country, city, option);

      JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
      List<Review> ReviewList = JsonConvert.DeserializeObject<List<Review>>(jsonResponse.ToString());

      return ReviewList;
    }

    public static async Task<List<string>> Popular(string option)
    {
      string result = await ApiHelper.GetPopular(option);
      JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
      List<string> stringList = JsonConvert.DeserializeObject<List<string>>(jsonResponse.ToString());
      return stringList;
    }

    public static async Task<string> GetRandom()
    {
      string result = await ApiHelper.GetRandom();
      string deserializedString = JsonConvert.DeserializeObject<string>(result);
      return deserializedString;
    }

    public async static Task<Review> GetDetails(int id)
    {
      string result = await ApiHelper.Get(id);

      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      Review Review = JsonConvert.DeserializeObject<Review>(jsonResponse.ToString());

      return Review;
    }

    public async static Task Post(Review Review, Microsoft.AspNetCore.Http.HttpContext context)
    {
      string jsonReview = JsonConvert.SerializeObject(Review);
      await ApiHelper.Post(jsonReview, context);
    }

    public static async Task Delete(int id, Microsoft.AspNetCore.Http.HttpContext context)
    {
      await ApiHelper.Delete(id, context);
    }

    public static async Task Put(Review Review, Microsoft.AspNetCore.Http.HttpContext context)
    {
      string jsonReview = JsonConvert.SerializeObject(Review);
      await ApiHelper.Put(Review.ReviewId, jsonReview, context);
    }
  }
  public enum Rating
  {
    ZERO, ONE, TWO, THREE, FOUR, FIVE
  }


}