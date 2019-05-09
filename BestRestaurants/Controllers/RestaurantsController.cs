using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System.Linq;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Controllers
{
  public class RestaurantsController : Controller
  {

      [HttpGet("/restaurants")]
      public ActionResult Index()
      {
          List<Restaurant> allRestaurants = Restaurant.GetAll();
          return View(allRestaurants);
      }

      [HttpGet("/cuisines/{id}/restaurants/new")]
      public ActionResult New(int id)
      {
          return View(id);
      }

      [HttpPost("/cuisines/{id}/restaurants")]
      public ActionResult Create(string name, string address, int id)
      {
          Restaurant myRestaurant = new Restaurant(name, address, id);
          myRestaurant.Save();
          return RedirectToAction("Show", "Cuisines", id);
      }

      [HttpGet("/cuisines/{cuisineId}/restaurants/{restaurantId}")]
      public ActionResult Show(int restaurantId, int cuisineId)
      {
          Restaurant myRestaurant = Restaurant.Find(restaurantId);
          return View(myRestaurant);
      }

      [HttpGet("/cuisines/{cuisineId}/restaurants/{restaurantId}/edit")]
        public ActionResult Edit(int restaurantId, int cuisineId)
        {
        Restaurant editRestaurant = Restaurant.Find(restaurantId);
       
        return View(editRestaurant);
        }

        [ActionName("Edit"), HttpPost("/cuisines/{cuisineId}/restaurants/{restaurantId}/edit")]
        public ActionResult Update(int restaurantId, int cuisineId, string name, string address)
        {
        Console.WriteLine("you clicked edit");
        Restaurant thisRestaurant = Restaurant.Find(restaurantId);
        thisRestaurant.EditName(name);
        thisRestaurant.EditAddress(address);
        return RedirectToAction("Show", thisRestaurant);
        }


        [ActionName("Destroy"), HttpPost("/cuisines/{cuisineId}/restaurants/{restaurantId}/delete")]
        public ActionResult Destroy(int restaurantId)
        {
        Restaurant deleteRestaurant = Restaurant.Find(restaurantId);
        List<Review> deleteReviews = deleteRestaurant.GetReviews();
        foreach(Review review in deleteReviews)
        {
            review.Delete();
        }

        deleteRestaurant.Delete();
        return RedirectToAction("Index");
        }

  }

}