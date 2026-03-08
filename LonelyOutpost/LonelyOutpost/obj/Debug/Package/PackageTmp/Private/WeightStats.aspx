<%@ Page Title="" Language="C#" MasterPageFile="~/lonelyOutpost.Master" AutoEventWireup="true" CodeBehind="WeightStats.aspx.cs" Inherits="lonelyOutpost.Private.WeightStats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:TemplateField HeaderText="Date" SortExpression="Day">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetDay(Eval("Day")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Kgs" SortExpression="Weight">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# String.Format("{0:N1}", Eval("Weight")) %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetStats" TypeName="lonelyOutpost.LonelyOutpostDAL"></asp:ObjectDataSource>
</asp:Content>
