<%@ Page Title="" Language="C#" MasterPageFile="~/lonelyOutpost.Master" AutoEventWireup="true" CodeBehind="Weight.aspx.cs" Inherits="lonelyOutpost.Private.Weight" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br /><asp:TextBox ID="WeightTextBox" runat="server"></asp:TextBox>
    <asp:DropDownList ID="TimeList" runat="server">
        <asp:ListItem>00:00</asp:ListItem>
        <asp:ListItem>01:00</asp:ListItem>
        <asp:ListItem>02:00</asp:ListItem>
        <asp:ListItem>03:00</asp:ListItem>
        <asp:ListItem>04:00</asp:ListItem>
        <asp:ListItem>05:00</asp:ListItem>
        <asp:ListItem>06:00</asp:ListItem>
        <asp:ListItem>07:00</asp:ListItem>
        <asp:ListItem>08:00</asp:ListItem>
        <asp:ListItem>09:00</asp:ListItem>
        <asp:ListItem>10:00</asp:ListItem>
        <asp:ListItem>11:00</asp:ListItem>
        <asp:ListItem>12:00</asp:ListItem>
        <asp:ListItem>13:00</asp:ListItem>
        <asp:ListItem>14:00</asp:ListItem>
        <asp:ListItem>15:00</asp:ListItem>
        <asp:ListItem>16:00</asp:ListItem>
        <asp:ListItem Selected="True">17:00</asp:ListItem>
        <asp:ListItem>18:00</asp:ListItem>
        <asp:ListItem>19:00</asp:ListItem>
        <asp:ListItem>20:00</asp:ListItem>
        <asp:ListItem>21:00</asp:ListItem>
        <asp:ListItem>22:00</asp:ListItem>
        <asp:ListItem>23:00</asp:ListItem>
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="WeightButton" runat="server" Text="Enter Today's Weight" onclick="WeightButton_Click" />
&nbsp;&nbsp;&nbsp;
    <asp:Label ID="NotificationLabel" runat="server" Visible="false" Text="Weight Recorded!"></asp:Label>
&nbsp;<br />
    <br />
    <asp:LinkButton ID="LinkButton1" PostBackUrl="~/Private/WeightStats.aspx" runat="server">Stats Page</asp:LinkButton>
&nbsp;
</asp:Content>
