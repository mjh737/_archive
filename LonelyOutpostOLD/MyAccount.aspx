<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="LonelyOutpost.Secure.MyAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h4>My Account</h4>

<p>You are logged in as: <asp:LoginName ID="LoginName1" runat="server" /></p>
<p>You are a member of the following groups: 
<asp:Repeater ID="UsersRoleList" runat="server">
    <HeaderTemplate><table></HeaderTemplate>
    <ItemTemplate>
    <tr><td align="left"><asp:Label ID="RoleLabel" Text='<%# Container.DataItem %>' runat="server" /><br /></td><tr>
    </ItemTemplate>
    <FooterTemplate></Table><br /></FooterTemplate>
</asp:Repeater>
</p>

</asp:Content>
