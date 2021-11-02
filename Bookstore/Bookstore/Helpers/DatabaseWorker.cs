using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;

namespace Bookstore.Helpers
{
    public static class DatabaseWorker
    {
        private static readonly string _connectionString = WebConfigurationManager.ConnectionStrings["BookstoreDB"].ToString();
        public static IEnumerable<Author> GetAuthors()
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

        public static IEnumerable<Genre> GetGenres()
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

        public static void UpdateBook(int ID, string Title, string About, int AuthorID, int GenreID)
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
        public static Book GetBookByID(string ID)
        {
            Book requestedBook = new Book();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetBookByID_Proc", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("ID", ID));
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            requestedBook.ID = Convert.ToInt32(reader["ID"].ToString());
                            requestedBook.Title = reader["Title"].ToString();
                            requestedBook.About = reader["About"].ToString();
                            requestedBook.Author = new Author
                            {
                                ID = Convert.ToInt32(reader["AuthorID"].ToString()),
                                Name = reader["AuthorName"].ToString(),
                                Surname = reader["AuthorSurname"].ToString()
                            };
                            requestedBook.Genre = new Genre
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
            return requestedBook;
        }



        public static void AddBook(string Title, string About, int authorID, int genreID)
        {
            using (SqlConnection connection =
                    new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("AddBook_Proc", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("Title", Title));
                        command.Parameters.Add(new SqlParameter("About", About));
                        command.Parameters.Add(new SqlParameter("AuthorID", authorID));
                        command.Parameters.Add(new SqlParameter("GenreID", genreID));
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

        public static void AddGenre(string name)
        {
            using (SqlConnection connection =
                    new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("AddGenre_Proc", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("Name", name));
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

        public static void AddAuthor(string name, string surname)
        {
            using (SqlConnection connection =
                       new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("AddAuthor_Proc", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("Name", name));
                        command.Parameters.Add(new SqlParameter("Surname", surname));
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