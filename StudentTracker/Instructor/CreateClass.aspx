<%@ Page Title="Create New Class" Language="C#" MasterPageFile="~/Instructor/Instructor.master" AutoEventWireup="true" CodeBehind="CreateClass.aspx.cs" Inherits="StudentTracker.Instructor.CreateClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div class="row">
            <div class="col-md-offset-3 col-md-8">
                <h2><%: Title %></h2>
                <h4>Create new class each quarter. Select quarter year then enter class name. 
                    If you QuarterYear not found in the dropdown list, it need to create first.</h4>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="row">
            <div class="col-md-offset-3 col-md-8">
                <asp:ValidationSummary runat="server" CssClass="text-danger" />
                <h4 class="text-danger">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </h4>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="selectQuarterYear" CssClass="col-md-3 control-label">Quarter Year</asp:Label>
                <div class="col-md-4">
                    <asp:DropDownList ID="selectQuarterYear" runat="server" CssClass="form-control" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ClassName" CssClass="col-md-3 control-label">Class Name</asp:Label>
                <div class="col-md-9">
                    <asp:TextBox runat="server" ID="ClassName" CssClass="form-control cap_first_letter" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ClassName"
                        CssClass="text-danger" ErrorMessage="Class Name field is required." Display="Dynamic" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-3 col-md-6">
                    <asp:Button ID="btnClassCreate" runat="server" OnClick="CreateClass_Click" Text="Save Class" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <h4 class="text-danger">
                    <asp:Literal runat="server" ID="ClassListMessage" />
                </h4>
            </div>
            <div class="col-md-offset-3 col-md-7">
                <h4>Courses list below created is assign to you.</h4>
                <asp:GridView ID="GridView1" runat="server" CssClass="table" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlInstructtorCurrentClass">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" Visible="False" />
                        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                        <asp:BoundField DataField="Quarter" HeaderText="Quarter" SortExpression="Quarter" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="QuarterYearID" HeaderText="QuarterYearID" SortExpression="QuarterYearID" Visible="False" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlInstructtorCurrentClass" runat="server" ConnectionString="<%$ ConnectionStrings:dbStudentTracker %>" SelectCommand="SELECT UsersCourses.ID, UsersCourses.UserId, Courses.Name, QuarterYears.Quarter, QuarterYears.Year, Courses.QuarterYearID FROM UsersCourses INNER JOIN Courses ON UsersCourses.CourseId = Courses.ID INNER JOIN QuarterYears ON Courses.QuarterYearID = QuarterYears.ID WHERE ([QuarterYearID] = @QuarterYearID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="selectQuarterYear" Name="QuarterYearID" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
            <div class="col-md-offset-3 col-md-7">
                <h4>All Courses list below created for current selected quarter.</h4>
                <asp:GridView ID="GridViewClassList" runat="server" CssClass="table" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlClassListAdded">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="Class ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Name" HeaderText="Course Name" SortExpression="Name" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlClassListAdded" runat="server" ConnectionString="<%$ ConnectionStrings:dbStudentTracker %>" SelectCommand="SELECT [ID], [Name] FROM [Courses] WHERE ([QuarterYearID] = @QuarterYearID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="selectQuarterYear" Name="QuarterYearID" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
