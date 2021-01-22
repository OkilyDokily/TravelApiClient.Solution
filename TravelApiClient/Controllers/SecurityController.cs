using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelApiClient.Models;
using Newtonsoft.Json;
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

      HttpContext.Response.Cookies.Append(
        "CookieKeyJWT",
        jwtToken.GetValue("token").ToString(),
        new CookieOptions
        {
          IsEssential = true,
          HttpOnly = true
        });
      return RedirectToAction("Index", "Home");
    }

  }
}