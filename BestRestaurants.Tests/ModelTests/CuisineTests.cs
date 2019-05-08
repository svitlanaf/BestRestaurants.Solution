using Microsoft.VisualStudio.TestTools.UnitTesting;
using BestRestaurants.Models;
using System.Collections.Generic;
using System;

namespace BestRestaurants.Tests
{
    [TestClass]
    public class CuisineTest//: IDisposable
    {
    // public void Dispose()
    // {
    //   Cuisine.ClearAll();
    // }

    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
    }

    [TestMethod]
    public void CuisineConstructor_CreatesInstanceOfCuisine_Cuisine()
    {
      string description = "Very spicy.";
      string name = "Indian";
      Cuisine newCuisine = new Cuisine(name, description);
      Assert.AreEqual(typeof(Cuisine), newCuisine.GetType());
    }

    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      string description = "Very spicy.";
      string name = "Indian";
      Cuisine newCuisine = new Cuisine(name, description);
      string result = newCuisine.GetDescription();
      Assert.AreEqual(description, result);
    }
    }
}