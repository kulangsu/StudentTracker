<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/Instructor.master" AutoEventWireup="true" CodeBehind="Homework.aspx.cs" Inherits="StudentTracker.Instructor.Homework" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
    <asp:Label ID="Lbl_pageTitle" runat="server" Text="Homework List"></asp:Label><br /><br< /></h1>
    
    <asp:GridView ID="GriveViewAssignmentList" runat="server" >
      
   
       </asp:GridView>

      <asp:label id="MessageLabel" forecolor="Red" runat="server"/>

    <br />
    <br />

    <asp:Button ID="btnAddHmw" runat="server" Text="Add Homework" OnClick="btnAddHmw_Click" CssClass="btn btn-primary" />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
</asp:Content>
