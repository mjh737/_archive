<%@ Page Title="" Language="C#" MasterPageFile="~/lonelyOutpost.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="lonelyOutpost.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><asp:Login ID="Login1" runat="server" Width="400px">
        <LayoutTemplate>
            <table cellpadding="1" cellspacing="0" style="border-collapse:collapse; color:White; font-weight: bold; font-size:32px;">
                <tr>
                    <td>
                        <table cellpadding="0" style="width:680px; background-color:#658ad9">
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label ID="Label1" runat="server" Font-Size="26px" Text="Log In"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" width="200px">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="UserName" Width="400px" Height="50px" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                            <td>&nbsp;</td>
                            </tr>
                            
                            <tr>
                                <td align="right" width="200px">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Password" Width="400px" Height="50px" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                            <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="LoginButton" runat="server" Height="30px" BackColor="White" ForeColor="#658ad9" Font-Bold="true" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color:Red;">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>
