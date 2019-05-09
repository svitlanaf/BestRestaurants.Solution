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

    [HttpGet("/cuisines/{cuisineId}/restaurants/{restaurantId}/reviews/{reviewId}/edit")]
        public ActionResult Edit(int reviewId, int restaurantId, int cuisineId)
        {
        Review editReview = Review.Find(reviewId);

        return View(editReview);
        }

        [ActionName("Edit"), HttpPost("/cuisines/{cuisineId}/restaurants/{restaurantId}/reviews/{reviewId}/edit/")]
        public ActionResult Update(int reviewId, int restaurantId, int cuisineId, string reviewText)
        {
        Review thisReview = Review.Find(reviewId);
        thisReview.Edit(reviewText);
        return RedirectToAction("Show", "Restaurants", restaurantId);
        }

  }
}