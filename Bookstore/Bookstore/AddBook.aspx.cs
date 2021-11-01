using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bookstore.Models;

namespace Bookstore
{
    public partial class AddBook : System.Web.UI.Page
    {
        private readonly string _connectionString = WebConfigurationManager.ConnectionStrings["BookstoreDB"].ToString();
        private List<Author> _authors = new List<Author>();
        private List<Genre> _genres = new List<Genre>();

        private void BindAuthors()
        {
            authorsRepeater.DataSource = GetAuthors();
            authorsRepeater.DataBind();
        }
        private void BindGenres()
        {
            genreRepeater.DataSource = GetGenres();
            genreRepeater.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindAuthors();
            BindGenres();
        }

        public IEnumerable<Author> GetAuthors()
        {
            _authors.Clear();
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
                        while(reader.Read())
                        {
                            Author author = new Author
                            {
                                ID = Convert.ToInt32(reader["ID"].ToString()),
                                Name = reader["Name"].ToString(),
                                Surname = reader["Surname"].ToString()
                            };
                            _authors.Add(author);
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return _authors;
        }

        public IEnumerable<Genre> GetGenres()
        {
            _genres.Clear();
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
                            _genres.Add(author);
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return _genres;
        }
        protected void addAuthorButton_Click(object sender, EventArgs e)
        {
            Author author = new Author();
            author.Name = authorName.Text;
            author.Surname = authorSurname.Text;
            if (CheckValidation(author))
            {
                using (SqlConnection connection =
                       new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("AddAuthor_Proc", connection))
                    {
                        try
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("Name", author.Name));
                            command.Parameters.Add(new SqlParameter("Surname", author.Surname));
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
                BindAuthors();
            }
            else
            {
                Response.Write(ShowValidationErrors(_validationResults));
            }
            newAuthorView.Visible = false;
        }


        protected void addGenreButton_Click(object sender, EventArgs e)
        {
            Genre genre = new Genre();
            genre.Name = genreName.Text;
            if(CheckValidation(genre))
            {
                using (SqlConnection connection =
                    new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("AddGenre_Proc", connection))
                    {
                        try
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("Name", genre.Name));
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
                BindGenres();
            }
            else
            {
                Response.Write(ShowValidationErrors(_validationResults));
            }
            newGenreView.Visible = false;
        }

        protected void addBookButton_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.Title = titleBox.Text;
            book.About = aboutBox.Text;
            var author = _authors.Where(a => a.ID == Convert.ToInt32(Request.Form["authorSelector"])).FirstOrDefault();
            var genre = _genres.Where(g => g.ID == Convert.ToInt32(Request.Form["genreSelector"])).FirstOrDefault();
            book.Author = author;
            book.Genre = genre;
            if(CheckValidation(book))
            {
                using (SqlConnection connection =
                    new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("AddBook_Proc", connection))
                    {
                        try
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("Title", book.Title));
                            command.Parameters.Add(new SqlParameter("About", book.About));
                            command.Parameters.Add(new SqlParameter("AuthorID", book.Author.ID));
                            command.Parameters.Add(new SqlParameter("GenreID", book.Genre.ID));
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
                Response.Redirect("Default.aspx");
            }
            else
            {
                Response.Write(ShowValidationErrors(_validationResults));
            }
        }

        private string ShowValidationErrors(IEnumerable<ValidationResult> results)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div><ul class='validationList'>");
            foreach (var validationResult in results)
            {
                sb.Append($"<li>{validationResult.ErrorMessage}</li>");
            }
            sb.Append("</ul><div>");
            return sb.ToString();
        }

        private List<ValidationResult> _validationResults = new List<ValidationResult>();
        private bool CheckValidation(object type)
        {
            var context = new ValidationContext(type);
            return Validator.TryValidateObject(type, context, _validationResults, true);
        }


    }
}