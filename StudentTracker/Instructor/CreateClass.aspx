<%@ Page Title="Create New Class" Language="C#" MasterPageFile="~/Instructor/Instructor.master" AutoEventWireup="true" CodeBehind="CreateClass.aspx.cs" Inherits="StudentTracker.Instructor.CreateClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div class="row">
            <div class="col-md-offset-4 col-md-8">
                <h2><%: Title %></h2>
                <p>Create new class each quarter. Select quarter year then enter class name.</p>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="row">
            <div class="col-md-offset-4 col-md-8">
                <asp:ValidationSummary runat="server" CssClass="text-danger" />
                <h4 class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </h4>
            </div>
            <div class="row">
                <div class="col-md-offset-4 col-md-6">
                    <asp:GridView ID="GridViewClassList" runat="server" CssClass="table">
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="selectQuarterYear" CssClass="col-md-4 control-label">Quarter Year</asp:Label>
                <div class="col-md-4">
                    <asp:DropDownList ID="selectQuarterYear" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ClassName" CssClass="col-md-4 control-label">Class Name</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="ClassName" CssClass="form-control cap_first_letter" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ClassName"
                        CssClass="text-danger" ErrorMessage="Class Name field is required." Display="Dynamic" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-4 col-md-6">
                    <asp:Button ID="btnClassCreate" runat="server" OnClick="CreateClass_Click" Text="Save Class" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
