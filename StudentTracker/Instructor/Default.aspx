<%@ Page Title="Instructor Homepage" Language="C#" MasterPageFile="~/Instructor/Instructor.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StudentTracker.Instructor.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .classModifyDiv {
            display: none;
        }
    </style>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {

        });
    </script>
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
                            <div class="currentCourseList" id="Course<%#Eval("CourseID") %>">
                                <div class="row">
                                    <div class="col-md-9">
                                        <div style="width: 100%; float: left; font-weight: bold;"><a href="#"><%#Eval("Year") %> - <%#Eval("Quarter") %></a></div>
                                    </div>
                                    <div class="col-md-1"><a href="#" title="Modify This Class"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></a></div>
                                    <div class="col-md-1"><a href="#" title="Delete This Class"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span></a></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-11">
                                        <div style="width: 100%; float: left; font-weight: bold;"><a href="#"><%#Eval("CourseName") %></a></div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 2px;">
                                    <div class="col-md-6">
                                        <div class="input-group input-group-sm">
                                            <span class="input-group-addon">00</span>
                                            <div class="form-control">Enrollment</div>
                                        </div>                                        
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group input-group-sm">
                                            <span class="input-group-addon">00</span>
                                            <div class="form-control">Assignments</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 2px;">
                                    <div class="col-md-6">
                                        <div class="input-group input-group-sm">
                                            <span class="input-group-addon">00</span>
                                            <div class="form-control">Projects</div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group input-group-sm">
                                            <span class="input-group-addon">00</span>
                                            <div class="form-control">Exam/Final</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 2px;">
                                    <div class="col-md-6">
                                        <div class="input-group input-group-sm">
                                            <span class="input-group-addon">00</span>
                                            <div class="form-control">In Class Exercise</div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group input-group-sm">
                                            <span class="input-group-addon">00</span>
                                            <div class="form-control">Extra Credit</div>
                                        </div>
                                    </div>
                                </div>
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
