<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StudentTracker._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <asp:Image runat="server" ImageUrl="~/Images/logo_grey.png" />
        <p class="lead">Welcome to BIT286 web application project.</p>
        <p><a href="http://faculty.cascadia.edu/mpanitz/Courses/BIT286/" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Students</h2>
            <p>
                Some introduction about this site for student.
            </p>
            <p>
                <a class="btn btn-default" href="#">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Instructor</h2>
            <p>
                Some instroduction for instructor site.
            </p>
            <p>
                <a class="btn btn-default" href="#">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Student Tracker</h2>
            <p>
                Some introduction about this Student Tracker site.
            </p>
            <p>
                <a class="btn btn-default" href="#">Learn more &raquo;</a>
            </p>
        </div>
    </div>
</asp:Content>
