<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/InstructorClass.master" AutoEventWireup="true" CodeBehind="ClassHomepage.aspx.cs" Inherits="StudentTracker.Instructor.ClassHomepage" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label ID="Lbl_pageTitle" runat="server" Text="Label"></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbStudentTracker %>" 
            DeleteCommand="DELETE FROM UsersCourses
            WHERE UserId = @UserId 
            AND CourseID = @Class" SelectCommand="classList_Proc" SelectCommandType="StoredProcedure">
            <DeleteParameters>
                <asp:Parameter Name="UserId" />
                <asp:QueryStringParameter DbType="Int32" Name="Class" QueryStringField="field1" />
            </DeleteParameters>
            <SelectParameters>
                <asp:QueryStringParameter Name="param2" QueryStringField="field1" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </h1>
    <asp:GridView ID="GrdView_Students"  CssClass="table"  runat="server" AllowPaging="True" CellPadding="8" HorizontalAlign="Center" AutoGenerateColumns="False" Width="752px" DataSourceID="SqlDataSource1" DataKeyNames="UserId">
        <Columns>
            <asp:CommandField ShowDeleteButton="True"   />
            <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" ReadOnly="True" Visible="False" />
            <asp:BoundField DataField="SID" HeaderText="SID" SortExpression="SID" />
            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name" />
        </Columns>
    </asp:Gridview>
</asp:Content>