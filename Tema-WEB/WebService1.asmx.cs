using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Tema_WEB
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private SqlConnection connection = new SqlConnection();

        [WebMethod]
        public void AddProduct(string productId, string name, string price, string quantity)
        {
            connection.ConnectionString = @"data source=DESKTOP-8FI7P38\SQLEXPRESS;initial catalog=LogisticDB;integrated security=True;encrypt=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework";
            connection.Open();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Products (ProductID, Name, Price, Qty) VALUES (@id, @name, @price, @qty)",
                connection);

            cmd.Parameters.AddWithValue("@id", productId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@qty", quantity);

            cmd.ExecuteNonQuery();
            connection.Close();
        }
        [WebMethod]
        public void UpdateProduct(string productId, string name, string price, string quantity)
        {
            connection.ConnectionString = @"data source=DESKTOP-8FI7P38\SQLEXPRESS;initial catalog=LogisticDB;integrated security=True;encrypt=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework";
            connection.Open();

            SqlCommand cmd = new SqlCommand(
                "UPDATE Products SET Name = @name, Price = @price, Qty = @qty WHERE ProductID = @id",
                connection);

            cmd.Parameters.AddWithValue("@id", productId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@qty", quantity);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        [WebMethod]
        public List<string> GetProductIds()
        {
            List<string> ids = new List<string>();
            connection.ConnectionString = @"data source=DESKTOP-8FI7P38\SQLEXPRESS;initial catalog=LogisticDB;integrated security=True;encrypt=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework";
            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT ProductID FROM Products", connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ids.Add(reader["ProductID"].ToString());
            }

            connection.Close();
            return ids;
        }
        [WebMethod]
        public string DeleteProduct(string productID)
        {
            connection.ConnectionString = @"data source=DESKTOP-8FI7P38\SQLEXPRESS;initial catalog=LogisticDB;integrated security=True;encrypt=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework";
            connection.Open();
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = "DELETE FROM Products WHERE ProductID = @id";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", productID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            return "Produsul a fost șters cu succes!";
                        else
                            return "Nu s-a găsit produsul cu ID-ul specificat.";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return "Eroare SQL: " + sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Eroare: " + ex.Message;
            }
        }
        [WebMethod]
        public List<string> GetProductInfo( string productID)
        {
            connection.ConnectionString = @"data source=DESKTOP-8FI7P38\SQLEXPRESS;initial catalog=LogisticDB;integrated security=True;encrypt=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework";
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE ProductID LIKE "+ productID + ";",connection);
            SqlDataReader reader = cmd.ExecuteReader();

            List<string> productInfo = new List<string>();
            while (reader.Read())
            {
                productInfo.Add(reader["Name"].ToString());
                productInfo.Add(reader["Price"].ToString());
                productInfo.Add(reader["Qty"].ToString());
            }
            connection.Close();
            return productInfo;
        }

    }
  
}

