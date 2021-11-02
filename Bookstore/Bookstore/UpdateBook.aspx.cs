using Bookstore.Helpers;
using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Services;

namespace Bookstore
{
    public partial class UpdateBook : System.Web.UI.Page
    {
        private static readonly string _connectionString = WebConfigurationManager.ConnectionStrings["BookstoreDB"].ToString();
        public Book CurrentBook { get; set; } = new Book();

        [WebMethod]
        public static void UpdateBookMethod(int ID, string Title, string About, int AuthorID, int GenreID)
        {
            DatabaseWorker.UpdateBook(ID, Title, About, AuthorID, GenreID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentBook = DatabaseWorker.GetBookByID(Request.QueryString["id"]);
        }
    }
}