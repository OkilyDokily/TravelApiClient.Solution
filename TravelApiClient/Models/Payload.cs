using System.Text.Json;
using System.Text;
using System;

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
            string jsonstring = Encoding.UTF8.GetString(Decode(payload));
            return JsonSerializer.Deserialize<Payload>(jsonstring);
        }

        public static byte[] Decode(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: output += "=="; break; // Two pad chars
                case 3: output += "="; break; // One pad char
                default: throw new System.ArgumentOutOfRangeException("input", "Illegal base64url string!");
            }
            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return converted;
            //Thank you Kalten from stackoverflow for this code https://stackoverflow.com/questions/60404612/parse-jwt-token-to-get-the-payload-content-only-without-external-library-in-c-sh
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