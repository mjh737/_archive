<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCategories.aspx.cs" Inherits="LonelyOutpost.Private.Calories.ManageCategories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<p>
  <h3>Manage Categories</h3>
  <asp:Label ID="ActionStatus" ForeColor="Red" runat="server" Text=""></asp:Label><br />
</p>


<b>Create a New Category: </b>
<asp:TextBox ID="CategoryName" runat="server"></asp:TextBox>
<asp:Button ID="CreateCategoryButton" runat="server" Text="Create Category"  onclick="CreateCategoryButton_Click" />
<br /><br />
<asp:GridView ID="CategoryList" runat="server" AutoGenerateColumns="false" 
        onrowdeleting="CategoryList_RowDeleting">
      <Columns>
      <asp:CommandField DeleteText="Delete Category" ShowDeleteButton="True"/>      
        <asp:TemplateField HeaderText="Category">
              <ItemTemplate>
                    <asp:Label runat="server" ID="CategoryLabel" Text='<%# Eval("CategoryName") %>' />
              </ItemTemplate>      
        </asp:TemplateField>
      </Columns>
</asp:GridView>

</asp:Content>
