<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="Bookstore.AddBook" %>

<%@ Import Namespace="Bookstore.Models" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="~/css/GeneralStyles.css" />
</head>
<body>
    
    
    <form id="addForm" runat="server">
        <div class="center-screen" style="max-width: 300px">
            <h1>Enter book info</h1>
            <div id="validationSummary" style="color:red;" runat="server">

            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Title:" />
                <asp:TextBox runat="server" CssClass="form-control text-input" ID="titleBox" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="About:" />
                <asp:TextBox runat="server" CssClass="form-control text-input" ID="aboutBox" />
            </div>
            <div id="authorDiv" runat="server" class="input-group mb-3 mt-3">
                <div class="input-group-prepend">
                    <asp:Label CssClass="input-group-text" runat="server" Text="Author:" />
                </div>
                <select name="authorSelector" class="custom-select">
                    <asp:Repeater ID="authorsRepeater" ItemType="Author" runat="server">
                        <ItemTemplate>
                            <option value="<%# Item.ID %>"><%# Item %></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </select>
                <button type="button" class="btn btn-outline-warning" data-toggle="modal" data-target="#newAuthorView">
                    +
                </button>
            </div>
            <div id="genreDiv" runat="server" class="input-group mb-3 mt-3">
                <div class="input-group-prepend">
                    <asp:Label CssClass="input-group-text" runat="server" Text="Genre:" />
                </div>
                <select name="genreSelector" class="custom-select">
                    <asp:Repeater ID="genreRepeater" ItemType="Genre" runat="server">
                        <ItemTemplate>
                            <option value="<%# Item.ID %>"><%# Item %></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </select>
                <button type="button" class="btn btn-outline-warning" data-toggle="modal" data-target="#newGenreView">
                    +
                </button>
            </div>
            <asp:Button runat="server" ID="addBookButton" CssClass="btn btn-outline-success" Text="Submit" OnClick="addBookButton_Click" />
            <asp:Button runat="server" ID="backButton" CssClass="btn btn-outline-secondary" Text="Back" OnClick="backButton_Click" />
        </div>
        <div class="modal fade" id="newAuthorView" runat="server" tabindex="-1" role="dialog" aria-labelledby="authorLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="authorLabel">New author</h4>
                    </div>
                    <div class="modal-body">
                        <div class="center-screen" style="min-height: 100px;">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Name:" />
                                <asp:TextBox runat="server" ID="authorName" CssClass="form-control text-input"/>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" Text="Surname:" />
                                <asp:TextBox runat="server" ID="authorSurname" CssClass="form-control text-input"/>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div>
                            <asp:Button runat="server" ID="addAuthor" CssClass="btn btn-outline-success" Text="Add" OnClick="addAuthorButton_Click" />
                        </div>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="newGenreView" runat="server" tabindex="-1" role="dialog" aria-labelledby="genreLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="genreLabel">New genre</h4>
                    </div>
                    <div class="modal-body">
                        <div class="center-screen" style="min-height: 100px;">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Name:" />
                                <asp:TextBox runat="server" ID="genreName" CssClass="form-control text-input" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div>
                            <asp:Button runat="server" ID="addGenre" CssClass="btn btn-outline-success" Text="Add" OnClick="addGenreButton_Click" />
                        </div>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        
    </form>
</body>
</html>
