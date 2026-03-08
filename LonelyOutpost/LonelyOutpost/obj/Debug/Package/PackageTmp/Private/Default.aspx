<%@ Page Title="" Language="C#" MasterPageFile="~/lonelyOutpost.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="lonelyOutpost.Private.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
<ul>
<li><asp:HyperLink ID="HyperLink1" NavigateUrl="~/Private/Weight.aspx" runat="server">Weight</asp:HyperLink></li>
<li><asp:HyperLink ID="HyperLink2" NavigateUrl="~/Private/Jenni/Default.aspx" runat="server">Jenni</asp:HyperLink></li>
<li><asp:HyperLink ID="HyperLink3" NavigateUrl="~/Private/Words.aspx" runat="server">Import Words</asp:HyperLink></li>
</ul>
<br />
    
</asp:Content>
