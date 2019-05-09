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

    public List<Restaurant> GetRestaurants()
    {
        List<Restaurant> allRestaurants = new List<Restaurant>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM restaurants WHERE cuisine_id = @thisId;";
        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = _id;
        cmd.Parameters.Add(thisId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int restaurantId = rdr.GetInt32(0);
            int restaurantCuisineId = rdr.GetInt32(1);
            string restaurantName = rdr.GetString(2);
            string restaurantAddress = rdr.GetString(3);
            
            Restaurant newRestaurant = new Restaurant(restaurantName, restaurantAddress, restaurantCuisineId, restaurantId);
            allRestaurants.Add(newRestaurant);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allRestaurants;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisines;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }

     public static Cuisine Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisines WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int cuisineId = 0;
      string cuisineName = "";
      string cuisineDescription = "";
      while (rdr.Read())
      {
        cuisineId = rdr.GetInt32(0);
        cuisineName = rdr.GetString(1);
        cuisineDescription = rdr.GetString(2);
      }
      Cuisine foundCuisine= new Cuisine(cuisineName, cuisineDescription, cuisineId);
       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
      return foundCuisine;
    }


    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = (this.GetId() == newCuisine.GetId());
        bool descriptionEquality = (this.GetDescription() == newCuisine.GetDescription());
        bool dueDateEquality = (this.GetName() == newCuisine.GetName());
        return (idEquality && descriptionEquality && dueDateEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cuisines (description, name) VALUES (@CuisineDescription, @CuisineName);";
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@CuisineDescription";
      description.Value = this._description;
      cmd.Parameters.Add(description);
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@CuisineName";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
    }

    public void EditName(string newName)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE cuisines SET name = @newName WHERE id = @searchId;";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@newName";
        name.Value = newName;
        cmd.Parameters.Add(name);
        cmd.ExecuteNonQuery();
        _name = newName;
        conn.Close();
        if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void EditDescription(string newDescription)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE cuisines SET description = @newDescription WHERE id = @searchId;";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
        MySqlParameter description = new MySqlParameter();
        description.ParameterName = "@newDescription";
        description.Value = newDescription;
        cmd.Parameters.Add(description);
        cmd.ExecuteNonQuery();
        _description = newDescription;
        conn.Close();
        if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("DELETE FROM cuisines WHERE id = @CuisineId;", conn);
        MySqlParameter cuisineIdParameter = new MySqlParameter();
        cuisineIdParameter.ParameterName = "@CuisineId";
        cuisineIdParameter.Value = _id;
        cmd.Parameters.Add(cuisineIdParameter);
        cmd.ExecuteNonQuery();
        if (conn != null)
        {
          conn.Close();
        }
      }


  }
}