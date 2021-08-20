using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelApiClient.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace TravelApiClient.Controllers
{
  public class SecurityController : Controller
  {
    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(string username, string password)
    {
      JObject jwtToken = await Security.Login(username, password);
      Response.Cookies.Delete("CookieKeyJWT");

      Response.Cookies.Append(
        "CookieKeyJWT",
        jwtToken.GetValue("token").ToString(),
        new CookieOptions
        {
          IsEssential = true,
          HttpOnly = true
        });
      return RedirectToAction("Index", "Reviews");
    }

    public ActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register(string username, string password, string passwordmatch)
    {
      await Security.Register(username, password, passwordmatch);
      return RedirectToAction("Login");
    }

  }
}