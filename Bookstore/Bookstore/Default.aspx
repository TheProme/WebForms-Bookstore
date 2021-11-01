<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bookstore.Default" %>
<%@ Import Namespace="Bookstore.Models" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="center-screen">
            <h1>Welcome to library!</h1>
            <div>
                <asp:Button runat="server" ID="booksListButton" Text="View books" CssClass="btn btn-width100 btn-outline-secondary" OnClick="booksListButton_Click" />
                <asp:Button runat="server" ID="addBookButton" Text="Add book" CssClass="btn btn-width100 btn-outline-secondary" OnClick="addBookButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>
