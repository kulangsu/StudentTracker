<%@ Page Title="Instructor Homepage" Language="C#" MasterPageFile="~/Instructor/Instructor.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StudentTracker.Instructor.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div class="row">
            <div class="col-md-8">
                <h2><%: Title %> :: School Year
                    <asp:Label ID="schoolYear" runat="server"></asp:Label></h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:ListView ID="CourseListView" runat="server">
                    <EmptyDataTemplate>
                        <ul class="nav nav-tabs">
                            <li role="presentation"><a href="#">Previous Quarter</a></li>
                            <li role="presentation" id="lblWinter" runat="server" class="active"><a href="#"><span class="glyphicon glyphicon-inbox"></span>&nbsp Current Quarter</a></li>
                            <li role="presentation"><a href="#">Next Quarter</a></li>
                        </ul>
                        <table class="border_lbr" style="width: 100%;">
                            <tr>
                                <th style="text-align: center;">
                                    <h2>No Class From Current Quarter.</h2>
                                </th>
                            </tr>
                        </table>

                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <ul class="nav nav-tabs">
                            <li role="presentation"><a href="#">Previous Quarter</a></li>
                            <li role="presentation" id="lblWinter" runat="server" class="active"><a href="#"><span class="glyphicon glyphicon-inbox"></span>&nbsp Current Quarter</a></li>
                            <li role="presentation"><a href="#">Next Quarter</a></li>
                        </ul>
                        <table class="border_lbr">
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>

                    <ItemTemplate>
                        <div class="col-md-6">
                            <div class="currentCourseList">
                                <a href="#">
                                    <div style="width: 100%; float: left"><%#Eval("Year") %> - <%#Eval("Quarter") %></div>
                                    <div style="width: 100%; float: left"><%#Eval("CourseName") %></div>
                                    <div style="width: 45%; float: left" title="Student Enrolled In This Class">Enrollment: (00)</div>
                                    <div style="width: 45%; float: right" title="Number of Assignment">Assignment: (00)</div>
                                    <div style="width: 45%; float: left" title="Number of Project">Projects: (00)</div>
                                    <div style="width: 45%; float: right" title="Midterm & Final Exam">Midtern/Final: (00)</div>
                                    <div style="width: 45%; float: left" title="In Class Exercise">ICE: (00)</div>
                                    <div style="width: 45%; float: right" title="Available Extra Credit">Extra Credit: (00)</div>
                                </a>
                            </div>
                        </div>
                    </ItemTemplate>


                </asp:ListView>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <h4>Select Quarter to show list of classes: <%: DateTime.Now.Year %></h4>
                <asp:Label runat="server" AssociatedControlID="qrtWinter">Winter</asp:Label>
                <asp:CheckBox ID="qrtWinter" runat="server" />
                <asp:Label runat="server" AssociatedControlID="qrtSpring">Spring</asp:Label>
                <asp:CheckBox ID="qrtSpring" runat="server" />
                <asp:Label runat="server" AssociatedControlID="qrtSummer">Summer</asp:Label>
                <asp:CheckBox ID="qrtSummer" runat="server" />
                <asp:Label runat="server" AssociatedControlID="qrtFall">Fall</asp:Label>
                <asp:CheckBox ID="qrtFall" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <h4>Classes list below not yet assign to instructore</h4>
                <asp:GridView ID="GridViewClassesList" runat="server" CssClass="table">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
