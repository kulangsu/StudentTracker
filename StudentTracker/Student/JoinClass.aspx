<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="JoinClass.aspx.cs" Inherits="StudentTracker.Student.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h1>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbStudentTracker %>" SelectCommand="SELECT [ID], [Name] FROM [Courses]"></asp:SqlDataSource>
    </h1>
    <h1>You're login as Student account.</h1>
<h3>You&#39;re in Student Content view page</h3>
    <h3>
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
    </h3>
    
</asp:Content>
