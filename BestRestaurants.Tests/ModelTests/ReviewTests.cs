using Microsoft.VisualStudio.TestTools.UnitTesting;
using BestRestaurants.Models;
using System.Collections.Generic;
using System;

namespace BestRestaurants.Tests
{
    [TestClass]
    public class ReviewTests : IDisposable
    {
    public void Dispose()
    {
      Review.ClearAll();
    }

    public ReviewTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
    }

    // [TestMethod]
    // {
        
    // }
    }
}