<%@ Page Title="Upload Student Grade" Language="C#" MasterPageFile="~/Instructor/InstructorClass.master" AutoEventWireup="true" CodeBehind="UploadGrade.aspx.cs" Inherits="StudentTracker.Instructor.UploadGrade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div class="form-horizontal">
            <div class="row">
                <div class="col-md-offset-3 col-md-9">
                    <h2><%: Title %></h2>
                    <h2><span style="font-size: 24px; color: #ff6a00" class="glyphicon glyphicon-book"></span>&nbsp
                        <asp:Label ID="ClassName" runat="server"></asp:Label></h2>
                    <h4>Browse Student Grade for this class to upload</h4>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-3 col-md-9">
                <asp:ValidationSummary runat="server" CssClass="text-danger" />
                <h4 class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </h4>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="StudentGradeFile" CssClass="col-md-3 control-label">Browse File</asp:Label>
                <div class="col-md-4">
                    <asp:FileUpload ID="StudentGradeFile" runat="server" CssClass="btn btn-default" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-3 col-md-6">
                    <asp:Button ID="btnUploadGrade" runat="server" OnClick="UploadGrade_Click" Text="Upload Grade" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="panel panel-default col-md-12">
                <table class="table">
                    <tr>
                        <th class="col-md-12"><h4>Current Enrolled Student</h4></th>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvCurrentStudentEnroll" runat="server" CssClass="table" AutoGenerateColumns="False" PageSize="100">
                                <Columns>
                                    <asp:BoundField DataField="SID" HeaderText="SID" SortExpression="SID" />
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                                    <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" HtmlEncode="False" HtmlEncodeFormatString="False" ItemStyle-Width="50%" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" HtmlEncodeFormatString="False" HtmlEncode="False" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <h4 class="text-danger">No Student Enroll In This Class.</h4>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                        <!--
                        <td>
                            <asp:GridView ID="gvGradeUploadStudent" runat="server" CssClass="table" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="Message" HeaderText="Grade Upload Message" SortExpression="Message" HtmlEncode="False" HtmlEncodeFormatString="False" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <h4 class="text-danger">Grade Upload Not Yet Submit.</h4>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                        <td>
                            <asp:GridView ID="gvGradeUploadStatus" runat="server" CssClass="table" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" HtmlEncode="False" HtmlEncodeFormatString="False" />
                                </Columns>
                                <EmptyDataTemplate>                                    
                                    <h4 class="text-danger">Not Upload</h4>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                        -->
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
