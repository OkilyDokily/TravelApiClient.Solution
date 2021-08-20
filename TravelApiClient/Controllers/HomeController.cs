using Microsoft.AspNetCore.Mvc;


namespace TravelApiClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            System.Console.WriteLine("Heelllo " + typeof(Controller).Assembly.GetName().Version.ToString());
            return View();
        }
    }
}
