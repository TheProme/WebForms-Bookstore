<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BooksList.aspx.cs" Inherits="Bookstore.BooksList" %>
<%@ Import Namespace="Bookstore.Models" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
</head>
<body>
    <form runat="server">
        <div>
            <h1>Books list</h1>
            <table class="table table-bordered">
                <asp:Repeater ID="booksRepeater" ItemType="Book" runat="server" OnItemCommand="booksRepeater_ItemCommand">
                    <HeaderTemplate>
                        <tr>
                            <th>Title</th>
                            <th>About</th>
                            <th>Genre</th>
                            <th>Author</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#: Item.Title %></td>
                            <td><%#: Item.About %></td>
                            <td><%#: Item.Genre %></td>
                            <td><%#: Item.Author %></td>
                            <td><asp:Button runat="server" ID="editButton" CssClass="btn btn-outline-warning" Text="Edit" CommandArgument='<%# Eval("ID") %>' CommandName="Edit"/></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div>
            <asp:Button runat="server" ID="newBookButton" CssClass="btn btn-outline-secondary" Text="New" OnClick="newBookButton_Click" />
            <asp:Button runat="server" ID="backButton" CssClass="btn btn-outline-secondary" Text="Back" OnClick="backButton_Click" />
        </div>
    </form>
</body>
</html>
