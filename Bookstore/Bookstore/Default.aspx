<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bookstore.Default" %>
<%@ Import Namespace="Bookstore.Models" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <asp:Repeater ID="AuthorsRepeater" ItemType="Author" runat="server">
                    <HeaderTemplate>
                        <tr>
                            <th>Name</th>
                            <th>Surname</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#: Item.Name %></td>
                            <td><%#: Item.Surname %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div>
            <h1>Author:</h1>
            <p>
                <asp:Label ID="nameLabel" runat="server" />
                <asp:Label ID="surnameLabel" runat="server" />
            </p>
            <div>
                <asp:Button runat="server" ID="getAuthorButton" Text="Get author" OnClick="getAuthorButton_Click" />
            </div>
        </div>
        <div>
            <h1>New Author:</h1>
            <p>
                <asp:TextBox ID="newAuthorName" runat="server" />
                <asp:TextBox ID="newAuthorSurname" runat="server" />
            </p>
            <div>
                <asp:Button runat="server" ID="addAuthorButton" Text="Add author" OnClick="addAuthorButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>
