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
                    <asp:DropDownList ID="selectQuarterYear" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="selectQuarterYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ClassName" CssClass="col-md-3 control-label">Class Name</asp:Label>
                <div class="col-md-2">
                    <asp:DropDownList ID="CourseArea" runat="server" CssClass="form-control" AutoPostBack="True" DataSourceID="SqlCoursePrefixList" DataTextField="PrefixName" DataValueField="PrefixID" OnSelectedIndexChanged="CourseArea_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlCoursePrefixList" runat="server" ConnectionString="<%$ ConnectionStrings:dbStudentTracker %>" SelectCommand="SELECT * FROM [CoursePrefixs] ORDER BY [PrefixName]"></asp:SqlDataSource>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="CourseNumber" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="ClassName" CssClass="form-control cap_first_letter" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ClassName"
                        CssClass="text-danger" ErrorMessage="Class Name field is required." Display="Dynamic" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-3 col-md-4">
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
            <h4>Courses List Link To: <asp:Literal runat="server" ID="FullName" /></h4>
            <asp:GridView ID="GridViewInstructorClassList" runat="server" CssClass="table" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="CourseID" HeaderText="Course ID" InsertVisible="False" ReadOnly="True" SortExpression="CourseID" />
                    <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                    <asp:BoundField DataField="Quarter" HeaderText="Quarter" SortExpression="Quarter" />
                    <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="CourseName" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-offset-3 col-md-7">
            <h4>All Courses list below created for current selected quarter.</h4>
            <asp:GridView ID="GridViewClassList" runat="server" CssClass="table" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlClassListAdded">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="Course ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
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

</asp:Content>
