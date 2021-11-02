<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateBook.aspx.cs" Inherits="Bookstore.UpdateBook" %>
<%@ Import Namespace="Bookstore.Helpers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <link rel="stylesheet" href="~/css/GeneralStyles.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
</head>
<body>
    <form id="mainForm" runat="server">
        <div class="center-screen" style="max-width: 300px">
            <h1>Update info</h1>
            <div class="form-group">
                <label for="titleInput">Title:</label>
                <input class="form-control text-input" type="text" name="titleInput" value="<%= CurrentBook.Title %>" />
            </div>
            <div class="form-group">
                <label for="aboutInput">About:</label>
                <input class="form-control text-input" type="text" name="aboutInput" value="<%= CurrentBook.About %>" />
            </div>
            <div class="input-group mb-3 mt-3">
                <div class="input-group-prepend">
                    <label class="input-group-text" for="authorSelect">Author:</label>
                </div>
                <select id="authorSelect" class="custom-select">
                    <% 
                        foreach (var item in DatabaseWorker.GetAuthors())
                        {
                            string htmlString;
                            if (CurrentBook.Author?.ID == item.ID)
                            {
                                htmlString = String.Format($"<option selected value='{item.ID}'>{item}</option>");
                            }
                            else
                            {
                                htmlString = String.Format($"<option value='{item.ID}'>{item}</option>");
                            }
                            Response.Write(htmlString);
                        }
                    %>
                </select>
            </div>
            <div class="input-group mb-3">
                <div class="input-group-prepend">
                    <label class="input-group-text" for="genreSelect">Genre:</label>
                </div>
                <select id="genreSelect" class="custom-select">
                    <% 
                        foreach (var item in DatabaseWorker.GetGenres())
                        {
                            string htmlString;
                            if (CurrentBook.Genre?.ID == item.ID)
                            {
                                htmlString = String.Format($"<option selected value='{item.ID}'>{item}</option>");
                            }
                            else
                            {
                                htmlString = String.Format($"<option value='{item.ID}'>{item}</option>");
                            }
                            Response.Write(htmlString);
                        }
                    %>
                </select>
            </div>

            <input type="button" value="Update" class="btn btn-width100 btn-outline-success m-3" onclick="submitFunc()" />
            <input type="button" value="Back" class="btn btn-width100 btn-outline-secondary m-3" onclick="backFunc()" />
        </div>
        <script type="text/javascript">
            var submitFunc = function () {
                var DTO =
                {
                    ID: <%= CurrentBook.ID %>,
                    Title: $('input[name="titleInput"]').val(),
                    About: $('input[name="aboutInput"]').val(),
                    AuthorID: $("#authorSelect").val(),
                    GenreID: $("#genreSelect").val()
                };
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "UpdateBook.aspx/UpdateBookMethod",
                    data: JSON.stringify(DTO),
                    datatype: "json",
                    success: function () {
                        window.location.replace("BooksList.aspx");
                    },
                    error: function () {
                        alert('Failed');
                    }
                });
            }

            var backFunc = function () {
                window.location.replace("BooksList.aspx");
            }
        </script>
    </form>
</body>
</html>
