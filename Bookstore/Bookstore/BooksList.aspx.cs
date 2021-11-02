using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace Bookstore
{
    public partial class BooksList : System.Web.UI.Page
    {
        private readonly string _connectionString = WebConfigurationManager.ConnectionStrings["BookstoreDB"].ToString();
        private int PageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetBooksPageWise(1);
            }
        }

        private void GetBooksPageWise(int pageIndex)
        {
            List<Book> booksList = new List<Book>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetBooksPageWise_Proc", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", PageSize);
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    con.Open();
                    var reader = cmd.ExecuteReader();
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

                    booksRepeater.DataSource = booksList;
                    booksRepeater.DataBind();
                    reader.Close();
                    con.Close();
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    this.PopulatePager(recordCount, pageIndex);
                }
            }
        }
        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)((decimal)recordCount / (decimal)PageSize);
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                pages.Add(new ListItem("<<", "1", currentPage > 1));
                if (currentPage != 1)
                {
                    pages.Add(new ListItem("Previous", (currentPage - 1).ToString()));
                }
                if (pageCount < 4)
                {
                    for (int i = 1; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                }
                else if (currentPage < 4)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                }
                else if (currentPage > pageCount - 4)
                {
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                    for (int i = currentPage - 1; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                }
                else
                {
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                    for (int i = currentPage - 2; i <= currentPage + 2; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                }
                if (currentPage != pageCount)
                {
                    pages.Add(new ListItem("next", (currentPage + 1).ToString()));
                }
                pages.Add(new ListItem(">>", pageCount.ToString(), currentPage < pageCount));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.GetBooksPageWise(pageIndex);
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

        protected void newBookButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBook.aspx");
        }

        protected void backButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}