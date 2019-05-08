using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System.Linq;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Controllers
{
  public class ReviewsController : Controller
  {
      [HttpGet("/reviews")]
      public ActionResult Index()
      {
          List<Review> allReviews = Review.GetAll();
          return View(allReviews);
      }
      

       [HttpGet("/cuisines/{cuisineId}/restaurants/{restaurantId}/reviews/new")]
        public ActionResult New(int cuisineId, int restaurantId)
        {
            int[] ids = new int[] {cuisineId, restaurantId};
            return View(ids);
        }

      [HttpPost("/cuisines/{cuisineId}/restaurants/{restaurantId}/reviews")]
      public ActionResult Create(string reviewText, int restaurantId)
      {
          Review myReview = new Review(reviewText, restaurantId);
          myReview.Save();
          return RedirectToAction("Show", "Restaurants", restaurantId);
      }


  }
}