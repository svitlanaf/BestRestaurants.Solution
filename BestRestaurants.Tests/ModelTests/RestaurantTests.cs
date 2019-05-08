using Microsoft.VisualStudio.TestTools.UnitTesting;
using BestRestaurants.Models;
using System.Collections.Generic;
using System;

namespace BestRestaurants.Tests
{
    [TestClass]
    public class RestaurantTests : IDisposable
    {
    public void Dispose()
    {
      Restaurant.ClearAll();
    //   Review.ClearAll();
    }

    public RestaurantTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
    }

    // [TestMethod]
    // {
        
    // }
    }
}