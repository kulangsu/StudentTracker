<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/Instructor.master" AutoEventWireup="true" CodeBehind="Homework.aspx.cs" Inherits="StudentTracker.Instructor.Homework" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
    <asp:Label ID="Lbl_pageTitle" runat="server" Text="Homework List"></asp:Label><br /><br< /></h1>
    
    <asp:GridView ID="GriveViewAssignmentList" runat="server" AllowPaging="True" CellPadding="8" GridLines="Horizontal" HorizontalAlign="Center" AutoGenerateColumns="False" OnSelectedIndexChanged="GriveViewAssignmentList_SelectedIndexChanged" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60" HeaderStyle-HorizontalAlign="Center">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="ckBx_AssignmentList" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="AssignmentName" HeaderText="Assignment Name" ItemStyle-Width="200" />
            <asp:CommandField ShowEditButton="true" /> 
        </Columns>
   
       </asp:GridView>

      <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StudentTracker_DBConnectionString %>" SelectCommand="SELECT [AssignmentName] FROM [Assignment]"></asp:SqlDataSource>

      <asp:label id="MessageLabel" forecolor="Red" runat="server"/>

    <br />
    <br />

    <asp:Button ID="btnAddHmw" runat="server" Text="Add Homework" OnClick="btnAddHmw_Click" CssClass="btn btn-primary" />
    &nbsp;&nbsp;&nbsp;&nbsp;
   <asp:Button ID="btnRemoveHmw" runat="server" Text="Remove Homework" onClick="btnRemoveHmw_Click" CausesValidation="False" CssClass="btn btn-primary"  />
    <br />
    <br />
</asp:Content>
