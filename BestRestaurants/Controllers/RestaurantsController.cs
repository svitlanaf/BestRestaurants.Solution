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
      public ActionResult Show(int restaurantId)
      {
          Restaurant myRestaurant = Restaurant.Find(restaurantId);
          return View(myRestaurant);
      }

  }

}