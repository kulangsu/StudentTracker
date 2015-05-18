<%@ Page Title="Upload Student Grade" Language="C#" MasterPageFile="~/Instructor/InstructorClass.master" AutoEventWireup="true" CodeBehind="UploadGrade.aspx.cs" Inherits="StudentTracker.Instructor.UploadGrade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div class="form-horizontal">
            <div class="row">
                <div class="col-md-offset-3 col-md-9">
                    <h2> <%: Title %></h2>
                    <h2><span style="font-size: 24px; color: #ff6a00" class="glyphicon glyphicon-book"></span>&nbsp <asp:Label ID="ClassName" runat="server"></asp:Label></h2>
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
    </div>
</asp:Content>
