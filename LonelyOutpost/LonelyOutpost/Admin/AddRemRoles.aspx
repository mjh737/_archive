<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRemRoles.aspx.cs" Inherits="LonelyOutpost.Admin.AddRemRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Roles</h3>

    <asp:GridView ID="RoleList" runat="server" AutoGenerateColumns="False" 
            onrowdeleting="RoleList_RowDeleting">
        <Columns>
            <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
            <asp:TemplateField HeaderText="Role">
                <ItemTemplate>
                    <asp:Label runat="server" ID="RoleNameLabel" Text='<%# Container.DataItem %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <asp:TextBox ID="RoleName" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You must enter a value" ControlToValidate="RoleName"></asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="CreateRoleButton" runat="server" Text="Create Role" 
        onclick="CreateRoleButton_Click" />
    <br /><br />
    <p>
        <asp:LinkButton ID="LinkButton1" CausesValidation="false" PostBackUrl="~/Admin/Default.aspx" runat="server">Click here to return to the Admin menu.</asp:LinkButton>
    </p>

</asp:Content>
