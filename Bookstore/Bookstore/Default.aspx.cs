using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bookstore
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly string _connectionString = WebConfigurationManager.ConnectionStrings["BookstoreDB"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void getAuthorButton_Click(object sender, EventArgs e)
        {
            List<Author> authorsList = new List<Author>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAuthors_Proc", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var existingAuthor = new Author 
                            { 
                                ID = Convert.ToInt32(reader["ID"].ToString()), 
                                Name = reader["Name"].ToString(), 
                                Surname = reader["Surname"].ToString() 
                            };

                            if(!authorsList.Contains(existingAuthor))
                            {
                                authorsList.Add(existingAuthor);
                            }
                        }
                        connection.Close();

                        AuthorsRepeater.DataSource = authorsList;
                        AuthorsRepeater.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                    
            }
        }

        protected void addAuthorButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(newAuthorName.Text) && !String.IsNullOrEmpty(newAuthorSurname.Text))
            {
                using (SqlConnection connection =
                    new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("AddAuthor_Proc", connection))
                    {
                        try
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("Name", newAuthorName.Text));
                            command.Parameters.Add(new SqlParameter("Surname", newAuthorSurname.Text));
                            connection.Open();
                            var reader = command.ExecuteNonQuery();
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                } 
            }
        }
    }
}