<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="LonelyOutpost.Private.Calories.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h2>Categories</h2><br />
<div>

<p>
    <asp:Label ID="ActionStatus" ForeColor="Red" runat="server" Text=""></asp:Label><br />
<b>Select a Category: </b><asp:DropDownList ID="CategoryList" runat="server" 
        AutoPostBack="True" DataTextField="CategoryName" DataValueField="CategoryID" 
        onselectedindexchanged="CategoryList_SelectedIndexChanged"></asp:DropDownList>
</p>
    <p>
        <asp:Repeater ID="FoodItemsList" runat="server">
            <ItemTemplate>
                <asp:CheckBox runat="server" ID="cbFoodItem" AutoPostBack="true" OnCheckedChanged="FoodItemCheckBox_CheckChanged" Text='<%# Eval("FoodName") %>' /><br />
            </ItemTemplate>
        </asp:Repeater>
    </p>
</div>
</asp:Content>
