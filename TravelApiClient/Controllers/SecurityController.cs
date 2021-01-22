using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelApiClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        return RedirectToAction("Index","Home")
    }

  }
}