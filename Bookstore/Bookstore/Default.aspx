<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bookstore.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Author:</h1>
            <p>
                <asp:Label ID="nameLabel" runat="server" />
                <asp:Label ID="surnameLabel" runat="server" />
            </p>
        </div>
        <div>
            <asp:Button runat="server" ID="testButton" Text="Get author" OnClick="testButton_Click" />
        </div>
    </form>
</body>
</html>
