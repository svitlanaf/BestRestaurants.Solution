using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
  public class Review
  {
      private string _reviewText;
      private int _id;
      private int _restaurantId;
    //   private int _cuisineId;
  
        public Review(string reviewText,  int restaurantId, int id = 0) //int cuisineId,
        {
            _reviewText = reviewText;
            _restaurantId = restaurantId;
            // _cuisineId = cuisineId;
            _id = id;
        }

        public string GetReviewText()
            {
            return _reviewText;
            }


        public int GetId()
            {
            return _id;
            }

        public int GetRestaurantId()
            {
            return _restaurantId;
            }

        // public int GetCuisineId()
        // {
        // return _cuisineId;
        // }

        public static void ClearAll()
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM reviews;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
            {
            conn.Dispose();
            }
        }

        public static List<Review> GetAll()

        {
        List<Review> allReviews = new List<Review> { };
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM reviews;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int reviewId = rdr.GetInt32(0);
            int restaurantId = rdr.GetInt32(1);
            string reviewText = rdr.GetString(2);
            
            Review newReview = new Review(reviewText, reviewId, restaurantId);
            allReviews.Add(newReview);
        }
        conn.Close();
        if (conn != null)
            {
                conn.Dispose();
            }
        return allReviews;
        }

        public static Review Find(int id)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM reviews WHERE id = (@searchId);";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int reviewId = 0;
        string reviewText = "";
        int restaurantId = 0;
        while(rdr.Read())
        {
            reviewId = rdr.GetInt32(0);
            restaurantId = rdr.GetInt32(1);
            reviewText = rdr.GetString(2);
        }
        Review newReview = new Review(reviewText, reviewId, restaurantId);
        conn.Close();
        if (conn != null)
            {
                conn.Dispose();
            }
        return newReview;
        }


         public override bool Equals(System.Object otherReview)
        {
        if (!(otherReview is Review))
            {
                return false;
            }
        else
            {
                Review newReview = (Review) otherReview;
                bool idEquality = this.GetId() == newReview.GetId();
                bool reviewTextEquality = this.GetReviewText() == newReview.GetReviewText();
                bool restauraantIdEquality = this.GetRestaurantId() == newReview.GetRestaurantId();
                return (idEquality && reviewTextEquality  && restauraantIdEquality);
            }
        }

        public void Save()
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO reviews (review_text, restaurant_id) VALUES (@review_text, @restaurant_id);";
        MySqlParameter reviewText = new MySqlParameter();
        reviewText.ParameterName = "@review_text";
        reviewText.Value = this._reviewText;
        cmd.Parameters.Add(reviewText);
        MySqlParameter restaurantId = new MySqlParameter();
        restaurantId.ParameterName = "@restaurant_id";
        restaurantId.Value = this._restaurantId;
        cmd.Parameters.Add(restaurantId);
        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string newReviewText)
        {
            Console.WriteLine("EDITING");

        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE reviews SET review_text = @newReviewText WHERE id = @searchId;";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
        MySqlParameter reviewText = new MySqlParameter();
        reviewText.ParameterName = "@newReviewText";
        reviewText.Value = newReviewText;
        cmd.Parameters.Add(reviewText);
        cmd.ExecuteNonQuery();
        _reviewText = newReviewText;
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
        MySqlCommand cmd = new MySqlCommand("DELETE FROM reviews WHERE id = @ReviewId;", conn);
        MySqlParameter reviewParameter = new MySqlParameter();
        reviewParameter.ParameterName = "@ReviewId";
        reviewParameter.Value = _id;
        cmd.Parameters.Add(reviewParameter);
        cmd.ExecuteNonQuery();
        if (conn != null)
            {
            conn.Close();
            }
      }
  }
}