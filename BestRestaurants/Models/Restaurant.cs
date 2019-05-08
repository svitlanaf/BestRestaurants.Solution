using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
  public class Restaurant
  {
      private string _name;
      private int _id;
      private int _cuisineId;
      private string _address;
  
        public Restaurant(string name, string address, int cuisineId, int id = 0)
        {
            _name = name;
            _address = address;
            _cuisineId = cuisineId;
            _id = id;
        }

        public string GetName()
        {
        return _name;
        }

        public string GetAddress()
        {
        return _address;
        }

        public int GetId()
        {
        return _id;
        }

        public int GetCuisineId()
        {
        return _cuisineId;
        }

         public static List<Restaurant> GetAll()

        {
        List<Restaurant> allRestaurants = new List<Restaurant> { };
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM restaurants;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int restaurantId = rdr.GetInt32(0);
            int restaurantCuisineId = rdr.GetInt32(1);
            string restaurantName = rdr.GetString(2);
            string restaurantAddress = rdr.GetString(3);
            
            Restaurant newRestaurant = new Restaurant(restaurantName, restaurantAddress, restaurantId, restaurantCuisineId);
            allRestaurants.Add(newRestaurant);
        }
        conn.Close();
        if (conn != null)
            {
                conn.Dispose();
            }
        return allRestaurants;
        }

        public List<Review> GetReviews()
    {
        List<Review> allReviews = new List<Review>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM reviews WHERE restaurant_id = @thisId;";
        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = _id;
        cmd.Parameters.Add(thisId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int reviewId = rdr.GetInt32(0);
            int reviewRestaurantId = rdr.GetInt32(1);
            string reviewText = rdr.GetString(2);
            
            Review newReview = new Review(reviewText, reviewRestaurantId, reviewId);
            allReviews.Add(newReview);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allReviews;
    }



        public static void ClearAll()
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM restaurants;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
            {
            conn.Dispose();
            }
        }

        public static Restaurant Find(int id)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM restaurants WHERE id = (@searchId);";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int restaurantId = 0;
        string restaurantName = "";
        string restaurantAddress = "";
        int restaurantCuisineId = 0;
        while(rdr.Read())
        {
            restaurantId = rdr.GetInt32(0);
            restaurantCuisineId = rdr.GetInt32(1);
            restaurantName = rdr.GetString(2);
            restaurantAddress = rdr.GetString(3);
        }
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantAddress, restaurantCuisineId, restaurantId);
        conn.Close();
        if (conn != null)
            {
                conn.Dispose();
            }
        return newRestaurant;
        }

        public override bool Equals(System.Object otherRestaurant)
        {
        if (!(otherRestaurant is Restaurant))
            {
                return false;
            }
        else
            {
                Restaurant newRestaurant = (Restaurant) otherRestaurant;
                bool idEquality = this.GetId() == newRestaurant.GetId();
                bool nameEquality = this.GetName() == newRestaurant.GetName();
                bool addressEquality = this.GetAddress() == newRestaurant.GetAddress();
                bool cuisineIdEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
                return (idEquality && nameEquality && addressEquality && cuisineIdEquality);
            }
        }

        public void Save()
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO restaurants (name, address, cuisine_id) VALUES (@name, @address, @cuisine_id);";
        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@name";
        name.Value = this._name;
        cmd.Parameters.Add(name);
        MySqlParameter address = new MySqlParameter();
        address.ParameterName = "@address";
        address.Value = this._address;
        cmd.Parameters.Add(address);
        MySqlParameter cuisineId = new MySqlParameter();
        cuisineId.ParameterName = "@cuisine_id";
        cuisineId.Value = this._cuisineId;
        cmd.Parameters.Add(cuisineId);
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
        cmd.CommandText = @"UPDATE restaurants SET name = @newName WHERE id = @searchId;";
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

        public void EditAddress(string newAddress)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE restaurants SET address = @newAddress WHERE id = @searchId;";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
        MySqlParameter address = new MySqlParameter();
        address.ParameterName = "@newAddress";
        address.Value = newAddress;
        cmd.Parameters.Add(address);
        cmd.ExecuteNonQuery();
        _address = newAddress;
        conn.Close();
        if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}