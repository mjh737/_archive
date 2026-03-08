<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRemUsers.aspx.cs" Inherits="LonelyOutpost.Admin.AddRemUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Users</h3>

        <asp:GridView ID="UserAccounts" AutoGenerateColumns="False" runat="server" 
        HorizontalAlign="Center" onrowdeleting="RolesUserList_RowDeleting">
        <Columns>
            <asp:CommandField DeleteText="Delete" ShowDeleteButton="True" />
            <asp:HyperLinkField DataNavigateUrlFields="UserName" 
                DataNavigateUrlFormatString="UserInformation.aspx?user={0}" Text="Manage" />
            <asp:TemplateField HeaderText="UserName">
                <ItemTemplate>
                    <asp:Label ID="UserNameLabel" runat="server" Text="<%# Container.DataItem %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsLockedOut" HeaderText="LockedOut?" />
            <asp:CheckBoxField DataField="IsOnline" HeaderText="IsOnline?" />
            <asp:BoundField DataField="Comment" HeaderText="Comment" />
        </Columns>
    </asp:GridView>

    <p>&nbsp;</p>
        <p>Add User</p>
    <table>
    <tr>
        <td><asp:Label ID="Label1" runat="server" Text="Username: "></asp:Label></td>
        <td><asp:TextBox ID="NewUserTextBox" runat="server"></asp:TextBox>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:Label ID="Label2" runat="server" Text="Password: "></asp:Label></td>
        <td><asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>&nbsp;</td>
    </tr>
    <tr>
        <td></td>
        <td><asp:Button ID="NewUserButton" runat="server" Text="Add" onclick="NewUserButton_Click" /></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label></td>
    </tr>
    </table>

    <br /><br />
    <asp:LinkButton ID="LinkButton1" PostBackUrl="~/Admin/Default.aspx" runat="server">Click here to return to the Admin menu</asp:LinkButton>

</asp:Content>
