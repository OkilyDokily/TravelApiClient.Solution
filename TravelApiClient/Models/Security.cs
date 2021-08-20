using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TravelApiClient.Models
{
  public class Security
  {
    public async static Task<JObject> Login(string username, string password)
    {
      string result = await SecurityApiHelper.Login(username, password);
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      return jsonResponse;
    }

    public async static Task Register(string username, string password, string passwordmatch)
    {
      await SecurityApiHelper.Register(username, password, passwordmatch);
    }
  }
}