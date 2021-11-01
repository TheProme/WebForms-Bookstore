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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addBookButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBook.aspx");
        }

        protected void booksListButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("BooksList.aspx");
        }
    }
}