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

      [HttpGet("/cuisines/{id}")]
      public ActionResult Show(int id)
      {
          Cuisine myCuisine = Cuisine.Find(id);
          return View(myCuisine);
      }

        [HttpGet("/cuisines/{id}/edit")]
        public ActionResult Edit(int id)
        {
        Cuisine editCuisine = Cuisine.Find(id);
        return View(editCuisine);
        }

        [ActionName("Edit"), HttpPost("/cuisines/{id}/edit")]
        public ActionResult Update(int id, string name, string description)
        {
        Cuisine thisCuisine = Cuisine.Find(id);
        thisCuisine.EditName(name);
        thisCuisine.EditDescription(description);
        return RedirectToAction("Show", thisCuisine);
        }

  }

}