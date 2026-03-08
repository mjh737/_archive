<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LonelyOutpost.Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h4><span>The Admin Pages</span></h4>

<ul class="blocklist">
    <li><asp:LinkButton ID="LinkButton1" PostBackUrl="~/Admin/AddRemUsers.aspx" runat="server">Add/Remove Users</asp:LinkButton></li>
    <li><asp:LinkButton ID="LinkButton2" PostBackUrl="~/Admin/AddRemRoles.aspx" runat="server">Add/Remove Roles</asp:LinkButton></li>
    <li><asp:LinkButton ID="LinkButton3" PostBackUrl="~/Admin/UserRoles.aspx" runat="server">Add/Remove Users from Roles</asp:LinkButton></li>
</ul>

</asp:Content>
