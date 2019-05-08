using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
  public class Cuisine
  {
      private int _id;
      private string _name;
      private string _description;

      public Cuisine(string name, string description, int id = 0)
      {
          _description = description;
          _name = name;
          _id = id;
      }

    public string GetDescription()
    {
      return _description;
    }

    public string GetName()
    {
      return _name;
    }

     public int GetId()
    {
      return _id;
    }

    
    public static List<Cuisine> GetAll()

    {
      List<Cuisine> allCuisines = new List<Cuisine> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisines;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int cuisineId = rdr.GetInt32(0);
        string cuisineName = rdr.GetString(1);
        string cuisineDescription = rdr.GetString(2);
        
        Cuisine newCuisine = new Cuisine(cuisineName, cuisineDescription, cuisineId);
        allCuisines.Add(newCuisine);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCuisines;
    }
  }
}