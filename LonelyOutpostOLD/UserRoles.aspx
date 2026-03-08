<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRoles.aspx.cs" Inherits="LonelyOutpost.Admin.UserRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h2>User Role Management</h2>
    
    <h3>Manage Roles By User</h3>
    
    <p>
        <asp:Label ID="ActionStatus" CssClass="important" runat="server" Text=""></asp:Label>
    </p>
    
    <p>
    <asp:DropDownList   ID="UserList"
                        AutoPostBack="true" 
                        DataTextField="UserName"
                        DataValueField="UserName"
                        runat="server" 
                        onselectedindexchanged="UserList_SelectedIndexChanged">
    </asp:DropDownList>
    </p>

        
    <asp:Repeater ID="UsersRoleList" runat="server">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>

        <ItemTemplate>
                <tr>
        <td align="left">
            <asp:CheckBox ID="RoleCheckBox" AutoPostBack="true" Text='<%# Container.DataItem %>' OnCheckedChanged="RoleCheckBox_CheckChanged" runat="server" />
            <br />
        
        </td>
        <tr>
        </ItemTemplate>
        <FooterTemplate></Table></FooterTemplate>
        
    </asp:Repeater>
    
    <h3>Manage Users By Role</h3>

    <p>
    <b>Select a Role:</b>
        <asp:DropDownList ID="RoleList" AutoPostBack="true" runat="server" 
            onselectedindexchanged="RoleList_SelectedIndexChanged">
        </asp:DropDownList>
    </p>
    <p>
        <asp:GridView ID="RolesUserList" AutoGenerateColumns = "False" 
            EmptyDataText="No users belong to this role." runat="server" 
            onrowdeleting="RolesUserList_RowDeleting">
            <Columns>
                <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
                <asp:TemplateField HeaderText="Users">
                <ItemTemplate>
                    <asp:Label ID="UserNameLabel" runat="server" Text="<%# Container.DataItem %>"></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </p>
    <br />
    <p>
        <asp:LinkButton ID="LinkButton1" PostBackUrl="~/Admin/Default.aspx" runat="server">Click here to return to the Admin menu</asp:LinkButton>
    </p>

</asp:Content>
