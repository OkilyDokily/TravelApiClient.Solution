using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelApiClient.Models;

namespace TravelApiClient.Controllers
{
    public class ReviewsController : Controller
    {
        public async Task<ActionResult> Index(string country, string city, string option)
        {
            try
            {
                Payload cookieValueFromReq = Payload.GetPayloadObject(HttpContext);
                DateTime date = DateTime.Now;
                DateTime dateJWT = Payload.UnixTimestampToDateTime(cookieValueFromReq.exp);
                List<Review> reviews = await Review.GetReviews(country, city, option);
                ViewBag.expired = date > dateJWT;

                return View(reviews);
            }
            catch
            {
                List<Review> reviews = await Review.GetReviews(country, city, option);
                return View(reviews);
            }
        }

        public async Task<ActionResult> Popular(string option)
        {
            List<string> strings = await Review.Popular(option);
            return View(strings);
        }

        public async Task<ActionResult> Random()
        {
            string result = await Review.GetRandom();
            ViewBag.result = result;
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {

            try
            {
                Payload cookieValueFromReq = Payload.GetPayloadObject(HttpContext);
                DateTime date = DateTime.Now;
                DateTime dateJWT = Payload.UnixTimestampToDateTime(cookieValueFromReq.exp);

                Review review = await Review.GetDetails(id);
                ViewBag.expired = date > dateJWT;
                ViewBag.userName = cookieValueFromReq.aud;
                return View(review);
            }
            catch
            {
                Review review = await Review.GetDetails(id);
                return View(review);
            }
        }

        public ActionResult Create()
        {
            try
            {
                Payload cookieValueFromReq = Payload.GetPayloadObject(HttpContext);
                DateTime date = DateTime.Now;
                DateTime dateJWT = Payload.UnixTimestampToDateTime(cookieValueFromReq.exp);
                ViewBag.expired = date > dateJWT;
                return View();
            }
            catch
            {
                return View();
            }

        }

        [HttpPost]
        public async Task<ActionResult> Create(Review review)
        {
            await Review.Post(review, HttpContext);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Payload cookieValueFromReq = Payload.GetPayloadObject(HttpContext);
                DateTime date = DateTime.Now;
                DateTime dateJWT = Payload.UnixTimestampToDateTime(cookieValueFromReq.exp);

                Review review = await Review.GetDetails(id);
                ViewBag.expired = date > dateJWT;
                ViewBag.userName = cookieValueFromReq.aud;
                return View(review);
            }
            catch
            {
                Review review = await Review.GetDetails(id);
                return View(review);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Review review)
        {
            await Review.Put(review, HttpContext);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await Review.Delete(id, HttpContext);
            return RedirectToAction("Index");
        }
    }
}