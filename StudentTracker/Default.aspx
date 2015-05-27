<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StudentTracker._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <asp:Image runat="server" ImageUrl="~/Images/logo_grey.png" />
        <p class="lead">Welcome to the BIT286 web application project.</p>
        <p><a href="http://faculty.cascadia.edu/mpanitz/Courses/BIT286/" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Students</h2>
            <p>
                Upload your assignments as soon as you are ready.  Get feedback quickly.
            </p>
            <p>
                <a class="btn btn-default" href="About.aspx">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Instructors</h2>
            <p>
                Access all student submissions with the click of a button.  Upload grades with ease.
            </p>
            <p>
                <a class="btn btn-default" href="About.aspx">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Student Tracker</h2>
            <p>
                Keep track of assignments and grades all in one place!
            </p>
            <p>
                <a class="btn btn-default" href="About.aspx">Learn more &raquo;</a>
            </p>
        </div>
    </div>
</asp:Content>
