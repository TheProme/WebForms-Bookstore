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
    public partial class BooksList : System.Web.UI.Page
    {
        private readonly string _connectionString = WebConfigurationManager.ConnectionStrings["BookstoreDB"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Page_Prerender(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void LoadBooks()
        {
            List<Book> booksList = new List<Book>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetBooks_Proc", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var book = new Book
                            {
                                ID = Convert.ToInt32(reader["ID"].ToString()),
                                Title = reader["Title"].ToString(),
                                About = reader["About"].ToString(),
                                Author = new Author
                                {
                                    ID = Convert.ToInt32(reader["AuthorID"].ToString()),
                                    Name = reader["AuthorName"].ToString(),
                                    Surname = reader["AuthorSurname"].ToString()
                                },
                                Genre = new Genre
                                {
                                    ID = Convert.ToInt32(reader["GenreID"].ToString()),
                                    Name = reader["GenreName"].ToString(),
                                }
                            };
                            booksList.Add(book);
                        }
                        connection.Close();

                        booksRepeater.DataSource = booksList;
                        booksRepeater.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }
        }

        protected void newBookButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBook.aspx");
        }

        protected void backButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void booksRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;

            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect($"UpdateBook.aspx?id={e.CommandArgument}");
                    break;

                default:
                    break;
            }
        }
    }
}