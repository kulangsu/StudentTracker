<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="JoinClass.aspx.cs" Inherits="StudentTracker.Student.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h1>You're logged in under a Student account.</h1>
    <h3>Please select a class from the drop down list</h3>
    <h4>
        <asp:Literal runat="server" ID="ErrorMessage" />
    </h4>
    <p>
        <asp:DropDownList ID="drpDwn_Join" runat="server" DataTextField="Name" DataValueField="ID" Font-Size="Larger" Height="25px" Width="300px">
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_Join" runat="server" Font-Size="Larger" OnClick="btn_Join_Click" Text="Join" />
    </p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>



</asp:Content>
