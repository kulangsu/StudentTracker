<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/Instructor.master" AutoEventWireup="true" CodeBehind="Homework.aspx.cs" Inherits="StudentTracker.Instructor.Homework" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblHmwList" runat="server" Text="Homework List"></asp:Label><br /><br< /> 
    <asp:CheckBox ID="cbHmwList" runat="server" AutoPostBack="True" OnCheckedChanged="cbHmwList_CheckedChanged"/> 
    <asp:Button ID="btnEdit" runat="server" Text="Edit"  OnClick="btnEdit_Click" Width="83px" Height="24px"/><br /><br />
    <asp:Button ID="btnAddHmw" runat="server" Text="Add Homework" OnClick="btnAddHmw_Click"/>
    &nbsp;&nbsp;&nbsp;&nbsp;
   <asp:Button ID="btnRemoveHmw" runat="server" Text="Remove Homework" onClick="btnRemoveHmw_Click" CausesValidation="False" />
</asp:Content>
