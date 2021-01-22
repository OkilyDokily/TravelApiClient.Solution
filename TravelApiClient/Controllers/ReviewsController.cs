using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelApiClient.Models;

namespace TravelApiClient.Controllers
{
  public class ReviewsController : Controller
  {
      public async Task<ActionResult> Index()
      {
        List<Review> reviews = await Review.GetReviews();
        return View(reviews);
      }

    public async Task<ActionResult> Details(int id)
    {
      Review review = await Review.GetDetails(id);
      return View(review);
    }
  }

}