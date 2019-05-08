using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System.Linq;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Controllers
{
  public class CuisinesController : Controller
  {

      [HttpGet("/cuisines")]
      public ActionResult Index()
      {
          List<Cuisine> allCuisines = Cuisine.GetAll();
          return View(allCuisines);
      }

      [HttpGet("/cuisines/new")]
      public ActionResult New()
      {
          return View();
      }

      [HttpPost("/cuisines/create")]
      public ActionResult Create(string name, string description)
      {
          Cuisine myCuisine = new Cuisine(name, description);
          myCuisine.Save();
          return RedirectToAction("Index");
      }

  }

}