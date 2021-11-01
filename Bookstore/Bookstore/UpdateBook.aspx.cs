using Bookstore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bookstore
{
    public partial class UpdateBook : System.Web.UI.Page
    {
        private static readonly string _connectionString = WebConfigurationManager.ConnectionStrings["BookstoreDB"].ToString();
        public Book CurrentBook { get; set; } = new Book();

        [WebMethod]
        public static void UpdateBookMethod(int ID, string Title, string About, int AuthorID, int GenreID)
        {
            using (SqlConnection connection =
                    new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdateBook_Proc", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("BookID", ID));
                        command.Parameters.Add(new SqlParameter("Title", Title));
                        command.Parameters.Add(new SqlParameter("About", About));
                        command.Parameters.Add(new SqlParameter("AuthorID", AuthorID));
                        command.Parameters.Add(new SqlParameter("GenreID", GenreID));
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

        public IEnumerable<Author> GetAuthors()
        {
            List<Author> authors = new List<Author>();
            using (SqlConnection connection =
                    new SqlConnection(_connectionString))
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
                            Author author = new Author
                            {
                                ID = Convert.ToInt32(reader["ID"].ToString()),
                                Name = reader["Name"].ToString(),
                                Surname = reader["Surname"].ToString()
                            };
                            authors.Add(author);
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return authors;
        }

        public IEnumerable<Genre> GetGenres()
        {
            List<Genre> genres = new List<Genre>();
            using (SqlConnection connection =
                    new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetGenres_Proc", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Genre author = new Genre
                            {
                                ID = Convert.ToInt32(reader["ID"].ToString()),
                                Name = reader["Name"].ToString()
                            };
                            genres.Add(author);
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return genres;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetBookByID_Proc", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("ID", Request.QueryString["id"]));
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CurrentBook.ID = Convert.ToInt32(reader["ID"].ToString());
                            CurrentBook.Title = reader["Title"].ToString();
                            CurrentBook.About = reader["About"].ToString();
                            CurrentBook.Author = new Author
                            {
                                ID = Convert.ToInt32(reader["AuthorID"].ToString()),
                                Name = reader["AuthorName"].ToString(),
                                Surname = reader["AuthorSurname"].ToString()
                            };
                            CurrentBook.Genre = new Genre
                            {
                                ID = Convert.ToInt32(reader["GenreID"].ToString()),
                                Name = reader["GenreName"].ToString(),
                            };
                        }
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