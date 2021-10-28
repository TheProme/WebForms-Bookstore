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

        protected void testButton_Click(object sender, EventArgs e)
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["BookstoreDB"].ToString();
            string queryString =
            "SELECT * FROM Authors";

            string name = "";
            string srname = "";


            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        name = reader["Name"].ToString();
                        srname = reader["Surname"].ToString();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            nameLabel.Text = name;
            surnameLabel.Text = srname;
        }
    }
}