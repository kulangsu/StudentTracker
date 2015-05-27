<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/InstructorClass.master" AutoEventWireup="true" CodeBehind="Homework.aspx.cs" Inherits="StudentTracker.Instructor.Homework" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
    <asp:Label ID="Lbl_pageTitle" runat="server" Text="Homework List"></asp:Label><br /><br< /></h2>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbStudentTracker %>" 
            DeleteCommand="DELETE FROM Assignments
            WHERE AssignmentID = @AssignmentID 
            "
        UpdateCommand="Update assignments set assignmentName=@assignmentName,DueDate=@DueDate,MaxPoint=@MaxPoint where AssignmentID=@assignmentID"
         SelectCommand="Select * from Assignments as a inner join AssignmentGroups as ag on a.AssignmentGroupID=ag.AssignmentGroupID where courseID=@CourseID">
        <UpdateParameters>
            <asp:Parameter Name="AssignmentID" />
            <asp:Parameter Name="assignmentName" />
            <asp:Parameter Name="DueDate" />
            <asp:Parameter Name="MaxPoint" />
        </UpdateParameters>    
        <DeleteParameters>
                <asp:Parameter Name="AssignmentID" />                
            </DeleteParameters>  
        <SelectParameters>
                <asp:QueryStringParameter Name="CourseID" QueryStringField="CourseID" Type="Int32" />
            </SelectParameters>         
        </asp:SqlDataSource>
    <asp:GridView ID="GriveViewAssignmentList"  DataKeyNames="AssignmentID" DataSourceID="SqlDataSource1" runat="server" CssClass="table" AutoGenerateColumns="False" AllowPaging="True" >
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
             <asp:BoundField DataField="AssignmentID" HeaderText="AssignmentID" ReadOnly="True" Visible="false" />
            
            <asp:BoundField DataField="AssignmentGroupName" HeaderText="Assignment Group" ReadOnly="True"/>
            <asp:TemplateField HeaderText="Assignment Name">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("AssignmentName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("AssignmentName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Points Possible">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("MaxPoint") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("MaxPoint") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Due Date">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("DueDate") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("DueDate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
      
   
       </asp:GridView>

      <asp:label id="MessageLabel" forecolor="Red" runat="server"/>

    <br />
    <br />

    <asp:Button ID="btnAddHmw" runat="server" Text="Add Homework" OnClick="btnAddHmw_Click" CssClass="btn btn-primary" />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
</asp:Content>
