using System.Text.Json;
using System.Text;
using System;
using RestSharp;


namespace TravelApiClient.Models
{
  public class Payload
  {
    public int exp { get; set; }
    public string iss { get; set; }
    public string aud { get; set; }


    public static Payload GetPayloadObject(Microsoft.AspNetCore.Http.HttpContext context)
    {
      string cookieValueFromReq = context.Request.Cookies["CookieKeyJWT"];
      string payload = cookieValueFromReq.Split(".")[1];
      string jsonstring = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
      return JsonSerializer.Deserialize<Payload>(jsonstring);
    }

    public static DateTime UnixTimestampToDateTime(double unixTime)
    {
      DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
      long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
      return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
    }

    public static (bool, bool, string) GetValues(Microsoft.AspNetCore.Http.HttpContext context)
    {
      try
      {
        Payload cookieValueFromReq = Payload.GetPayloadObject(context);
        DateTime date = DateTime.Now;
        DateTime dateJWT = Payload.UnixTimestampToDateTime(cookieValueFromReq.exp);
        string userName = cookieValueFromReq.aud;
        bool expired = date > dateJWT;

        return (true, expired, userName);
      }
      catch
      {
        return (false, true, "");
      }
    }
  }
}