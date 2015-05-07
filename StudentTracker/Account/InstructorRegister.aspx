<%@ Page Title="Instructor Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InstructorRegister.aspx.cs" Inherits="StudentTracker.Account.RegisterInstructor" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <div class="form-horizontal">
        <h4>Create a new account</h4>
        <hr />
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="SID" CssClass="col-md-5 control-label">Employee ID</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="SID" CssClass="form-control" placeholder="Enter 9-digits Employee ID" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="SID"
                            CssClass="text-danger" ErrorMessage="Employee ID is required." Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                            ControlToValidate="SID"
                            ValidationExpression="^(\d{9})$"
                            Display="Dynamic"
                            ErrorMessage="Enter a valid 9-digits Employee ID"
                            EnableClientScript="True"
                            runat="server" CssClass="text-danger" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="col-md-5 control-label">First Name</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="FirstName" CssClass="form-control cap_first_letter" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                            CssClass="text-danger" ErrorMessage="First Name field is required." Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-5 control-label">Last Name</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="LastName" CssClass="form-control cap_first_letter" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                            CssClass="text-danger" ErrorMessage="Last Name field is required." Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="City" CssClass="col-md-5 control-label">City</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="City" CssClass="form-control cap_first_letter" placeholder="City field is optional" />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-5 control-label">Email</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" placeholder="Required valid email for confirmation" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                            CssClass="text-danger" ErrorMessage="Email field is required." Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-5 control-label">Password</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                            CssClass="text-danger" ErrorMessage="Password field is required." Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-5 control-label">Confirm password</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                        <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                            CssClass="text-danger" Display="Dynamic" ErrorMessage="Password and Confirmation Password do not match." />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-5 col-md-6">
                        <asp:Button ID="InstructorRegister" runat="server" OnClick="CreateInstructorUser_Click" Text="Register" CssClass="btn btn-primary" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClientClick="this.form.reset();return false;" CssClass="btn btn-primary" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:ValidationSummary runat="server" CssClass="text-danger" />
                    <p class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
