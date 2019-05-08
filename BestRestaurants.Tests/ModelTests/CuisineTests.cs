using Microsoft.VisualStudio.TestTools.UnitTesting;
using BestRestaurants.Models;
using System.Collections.Generic;
using System;

namespace BestRestaurants.Tests
{
    [TestClass]
    public class CuisineTest : IDisposable
    {
    public void Dispose()
    {
      Cuisine.ClearAll();
    //   Restaurant.ClearAll();
    }

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

    [TestMethod]
    public void GetName_ReturnsDueDate_String()
    {
      string description = "Very spicy.";
      string name = "Indian";
      Cuisine newCuisine = new Cuisine(name, description);
      string result = newCuisine.GetName();
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_CuisineList()
    {
      List<Cuisine> newList = new List<Cuisine> { };
      List<Cuisine> result = Cuisine.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    // [TestMethod] =>>> !!!!!!!!!!
    // public void GetAll_ReturnsAllCategoryObjects_CategoryList()
    // {
    //   //Arrange
    //   string name01 = "Work";
    //   string name02 = "School";
    //   Category newCategory1 = new Category(name01);
    //   newCategory1.Save();
    //   Category newCategory2 = new Category(name02);
    //   newCategory2.Save();
    //   List<Category> newList = new List<Category> { newCategory1, newCategory2 };

    //   //Act
    //   List<Category> result = Category.GetAll();

    //   //Assert
    //   CollectionAssert.AreEqual(newList, result);
    // }

    // [TestMethod]
    // public void GetItems_ReturnsEmptyItemList_ItemList()
    // {
    //   //Arrange
    //   string name = "Work";
    //   Category newCategory = new Category(name);
    //   List<Item> newList = new List<Item> { };

    //   //Act
    //   List<Item> result = newCategory.GetItems();

    //   //Assert
    //   CollectionAssert.AreEqual(newList, result);
    // }

    // [TestMethod]
    // public void GetItems_RetrievesAllItemsWithCategory_ItemList()
    // {
    //   //Arrange, Act
    //   Category testCategory = new Category("Household chores");
    //   testCategory.Save();
    //   Item firstItem = new Item("Mow the lawn", testCategory.GetId());
    //   firstItem.Save();
    //   Item secondItem = new Item("Do the dishes", testCategory.GetId());
    //   secondItem.Save();
    //   List<Item> testItemList = new List<Item> {firstItem, secondItem};
    //   List<Item> resultItemList = testCategory.GetItems();

    //   //Assert
    //   CollectionAssert.AreEqual(testItemList, resultItemList);
    // }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Cuisine()
    {
      Cuisine firstCuisine = new Cuisine("", "Very spicy.");
      Cuisine secondCuisine = new Cuisine("", "Very spicy.");
      Assert.AreEqual(firstCuisine, secondCuisine);
    }

     [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Cuisine()
    {
      Cuisine firstCuisine = new Cuisine("Indian", "");
      Cuisine secondCuisine = new Cuisine("Indian", "");
      Assert.AreEqual(firstCuisine, secondCuisine);
    }

     [TestMethod]
    public void Find_ReturnsCuisineInDatabase_Cuisine()
    {
      Cuisine testCuisine = new Cuisine("Indian", "Very spicy.");
      testCuisine.Save();
      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());
      Assert.AreEqual(testCuisine, foundCuisine);
    }

    [TestMethod]
    public void GetAll_CuisinesEmptyAtFirst_List()
    {
      int result = Cuisine.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesCuisineToDatabase_CuisineList()
    {
      Cuisine testCuisine = new Cuisine("Indian", "Very spicy.");
      testCuisine.Save();
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};
      CollectionAssert.AreEqual(testList, result);
    }

     [TestMethod]
    public void Save_DatabaseAssignsIdToCuisine_Id()
    {
      Cuisine testCuisine = new Cuisine("Indian", "Very spicy.");
      testCuisine.Save();
      Cuisine savedCuisine = Cuisine.GetAll()[0];
      int result = savedCuisine.GetId();
      int testId = testCuisine.GetId();
      Assert.AreEqual(testId, result);
    }


    }    
}