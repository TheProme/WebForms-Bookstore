using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            authorsRepeater.DataSource = GetAuthors();
            authorsRepeater.DataBind();
            genreRepeater.DataSource = GetGenres();
            genreRepeater.DataBind();
        }

        public IEnumerable<Author> GetAuthors()
        {
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

        //TODO: Add validation check
        protected void addAuthorButton_Click(object sender, EventArgs e)
        {
            string authorName = Request.Form["authorName"];
            string authorSurname = Request.Form["authorSurname"];
            if(!String.IsNullOrEmpty(authorName) && !String.IsNullOrEmpty(authorSurname))
            {
                using (SqlConnection connection =
                       new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("AddAuthor_Proc", connection))
                    {
                        try
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("Name", authorName));
                            command.Parameters.Add(new SqlParameter("Surname", authorSurname));
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
                authorSelector.Visible = true;
                newAuthorView.Visible = false;
            }
        }

        protected void newAuthorButton_Click(object sender, EventArgs e)
        {
            authorSelector.Visible = false;
            newAuthorView.Visible = true;
        }


        //TODO: Add validation check
        protected void addGenreButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection =
                    new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("AddGenre_Proc", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("Name", Request.Form["genreName"]));
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
            genresSelector.Visible = true;
            newGenreView.Visible = false;
        }

        protected void newGenreButton_Click(object sender, EventArgs e)
        {
            genresSelector.Visible = false;
            newGenreView.Visible = true;
        }

        protected void addBookButton_Click(object sender, EventArgs e)
        {
            if(this.IsValid)
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("NO");
            }
        }
    }
}