<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnauthorisedAccess.aspx.cs" Inherits="LonelyOutpost.UnauthorisedAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
    
        You have attempted to access a page which you are not currently authorised to access.<br /><br /> Contact Matt if you 
        think you ought to be!<br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx"><< Return to login page</asp:HyperLink>
    
    </p>
</asp:Content>
