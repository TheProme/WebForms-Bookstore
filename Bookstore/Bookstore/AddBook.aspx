<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="Bookstore.AddBook" %>
<%@ Import Namespace="Bookstore.Models" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="addForm" runat="server">
        <div>
            <h1>Enter book info:</h1>
            <asp:ValidationSummary ID="validationSummary" runat="server" ShowModelStateErrors="true" />
        </div>
        <div>
            <asp:Label runat="server" Text="Title:"/>
            <asp:TextBox runat="server" ID="titleBox" />
        </div>
        <div>
            <asp:Label runat="server" Text="About:"/>
            <asp:TextBox runat="server" ID="aboutBox" />
        </div>
        <!--TODO: Popup window for new author as Form for validation check-->
        <div id="newAuthorView" runat="server" visible="false">
            <div>
                <asp:Label runat="server" Text="Name:" />
                <asp:TextBox runat="server" ID="authorName" />
            </div>
            <div>
                <asp:Label runat="server" Text="Surname:" />
                <asp:TextBox runat="server" ID="authorSurname" />
            </div>
            <div>
                <asp:Button runat="server" ID="addAuthorButton" Text="Add" OnClick="addAuthorButton_Click" />
            </div>
        </div>
        <div id="authorSelector" runat="server">
            <select>
                <asp:Repeater ID="authorsRepeater" ItemType="Author" runat="server">
                    <ItemTemplate>
                        <option><%# Item.Name + " " + Item.Surname %></option>
                    </ItemTemplate>
                </asp:Repeater>
            </select>
            <asp:Button runat="server" ID="newAuthorButton" Text="+" OnClick="newAuthorButton_Click" />
        </div>
        <!--TODO: Popup window for new genre as Form for validation check-->
        <div id="newGenreView" runat="server" visible="false">
            <div>
                <asp:Label runat="server" Text="Name:" />
                <asp:TextBox runat="server" ID="genreName" />
            </div>
            <div>
                <asp:Button runat="server" ID="addGenre" Text="Add" OnClick="addGenreButton_Click" />
            </div>
        </div>
        <div id="genresSelector" runat="server">
            <select>
                <asp:Repeater ID="genreRepeater" ItemType="Genre" runat="server">
                    <ItemTemplate>
                        <option><%# Item.Name %></option>
                    </ItemTemplate>
                </asp:Repeater>
            </select>
            <asp:Button runat="server" ID="newGenre" Text="+" OnClick="newGenreButton_Click" />
        </div>
        <asp:Button runat="server" ID="addBookButton" Text="Submit" OnClick="addBookButton_Click"/>
    </form>
</body>
</html>
