<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="StudentTracker.Student.EditProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
         <h1>
         <asp:Label ID="Lbl_pageTitle" runat="server" Text="Edit Profile"></asp:Label><br /><br< />       
         </h1>
    <asp:Label ID="lblSid" runat="server" Font-Size="Large" Text="SID:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:TextBox ID="TextBox1" runat="server" Width="200px"></asp:TextBox>
         <br />
    <asp:Label ID="lblFirstName" runat="server" Font-Size="Large"  Text="First Name:"></asp:Label>
         <asp:TextBox ID="TextBox2" runat="server" Width="200px"></asp:TextBox>
         <br />
    <asp:Label ID="lblLastName" runat="server" Font-Size="Large"  Text="Last Name:"></asp:Label>
         <asp:TextBox ID="TextBox3" runat="server" Width="200px"></asp:TextBox>
         <br />
         <br />
         <asp:Button ID="btnChange" runat="server" Text="Change" Font-Size="Large" OnClick="btnChange_Click" Width="89px" />
         <asp:Button ID="btnReset" runat="server" Text="Reset" Font-Size="Large" onClick="btnReset_Click" Width="89px" />
         <br />
         <br />
</asp:Content>
