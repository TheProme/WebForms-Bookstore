using Bookstore.Helpers;
using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace Bookstore
{
    public partial class AddBook : System.Web.UI.Page
    {
        private readonly string _connectionString = WebConfigurationManager.ConnectionStrings["BookstoreDB"].ToString();
        private List<Author> _authors = DatabaseWorker.GetAuthors().ToList();
        private List<Genre> _genres = DatabaseWorker.GetGenres().ToList();

        private void BindAuthors()
        {
            authorsRepeater.DataSource = _authors;
            authorsRepeater.DataBind();
        }
        private void BindGenres()
        {
            genreRepeater.DataSource = _genres;
            genreRepeater.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindAuthors();
            BindGenres();
        }
        protected void addAuthorButton_Click(object sender, EventArgs e)
        {
            Author author = new Author();
            author.Name = authorName.Text;
            author.Surname = authorSurname.Text;
            if (CheckValidation(author))
            {
                DatabaseWorker.AddAuthor(author.Name, author.Surname);
                BindAuthors();
            }
            else
            {
                validationSummary.InnerHtml = ShowValidationErrors(_validationResults);
            }
        }


        protected void addGenreButton_Click(object sender, EventArgs e)
        {
            Genre genre = new Genre();
            genre.Name = genreName.Text;
            if (CheckValidation(genre))
            {
                DatabaseWorker.AddGenre(genre.Name);
                BindGenres();
            }
            else
            {
                validationSummary.InnerHtml = ShowValidationErrors(_validationResults);
            }
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
            if (CheckValidation(book))
            {
                DatabaseWorker.AddBook(book.Title, book.About, book.Author.ID, book.Genre.ID);
                Response.Redirect("BooksList.aspx");
            }
            else
            {
                validationSummary.InnerHtml = ShowValidationErrors(_validationResults);
            }
        }

        private string ShowValidationErrors(IEnumerable<ValidationResult> results)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div><ul>");
            foreach (var validationResult in results)
            {
                sb.Append($"<li>{validationResult.ErrorMessage}</li>");
            }
            sb.Append("</ul></div>");
            return sb.ToString();
        }

        private List<ValidationResult> _validationResults = new List<ValidationResult>();
        private bool CheckValidation(object type)
        {
            var context = new ValidationContext(type);
            return Validator.TryValidateObject(type, context, _validationResults, true);
        }

        protected void backButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("BooksList.aspx");
        }


    }
}