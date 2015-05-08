<%@ Page Title="Create Quarter Year" Language="C#" MasterPageFile="~/Instructor/Instructor.master" AutoEventWireup="true" CodeBehind="QuarterYear.aspx.cs" Inherits="StudentTracker.Instructor.QuarterYearClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div class="row">
            <div class="col-md-offset-4 col-md-8">
                <h2><%: Title %></h2>
                <h4>Select School Year and Quarter to create new entry</h4>
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
        </div>
        <div class="row">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="selectYear" CssClass="col-md-4 control-label">School Year</asp:Label>
                <div class="col-md-3">
                    <asp:DropDownList ID="selectYear" runat="server" CssClass="form-control" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="selectQuarter" CssClass="col-md-4 control-label">Quarter</asp:Label>
                <div class="col-md-3">
                    <asp:DropDownList ID="selectQuarter" runat="server" CssClass="form-control">
                        <asp:ListItem>Winter</asp:ListItem>
                        <asp:ListItem>Spring</asp:ListItem>
                        <asp:ListItem>Summer</asp:ListItem>
                        <asp:ListItem>Fall</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-4 col-md-6">
                    <asp:Button ID="btnQuarterYearCreate" runat="server" OnClick="CreateQrtYear_Click" Text="Crate Quarter Year" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-4 col-md-7">
                <h4>QuarterYear list below created for current and next year.</h4>
                <asp:GridView ID="GridViewQuarterYear" runat="server" CssClass="table">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
