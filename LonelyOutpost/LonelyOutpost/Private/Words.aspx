<%@ Page Title="" Language="C#" MasterPageFile="~/lonelyOutpost.Master" AutoEventWireup="true" CodeBehind="Words.aspx.cs" Inherits="lonelyOutpost.Private.Words" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text='<%# GetWords() %>'></asp:Label>
    <br />
</asp:Content>
