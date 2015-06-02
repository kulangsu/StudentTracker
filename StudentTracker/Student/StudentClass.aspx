<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="StudentClass.aspx.cs" Inherits="StudentTracker.Student.StudentClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        <asp:Label ID="Lbl_pageTitle" runat="server" Text="Student Class"></asp:Label>&nbsp;&nbsp;&nbsp;
                    
    </h1>


    <h4>
        <asp:Literal runat="server" ID="ErrorMessage" />
    </h4>
    <asp:Label ID="Label1" runat="server" Text="Select Homework:" Font-Size="Large"></asp:Label>

    &nbsp;&nbsp;
             <asp:DropDownList ID="drpDwn_Assignment" runat="server" DataTextField="Name" DataValueField="ID" Font-Size="Larger" Height="25px" Width="300px" OnSelectedIndexChanged="drpDwn_Assignment_SelectedIndexChanged">
             </asp:DropDownList>
    <br />

    <p>
        <asp:FileUpload ID="FileUpload1" runat="server" />
    <p>

        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="156px" Height="30px" Font-Size="Larger" OnClick="btnSubmit_Click" />

    <p>
    <p>
        &nbsp;<asp:GridView ID="Assignments" runat="server" AutoGenerateColumns="False" CssClass="table" PageSize="100">
            <Columns>
                <asp:BoundField DataField="AssignmentName" HeaderText="Assignment Name" />
                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                <asp:BoundField DataField="Attendance" HeaderText="Attendance" />
                <asp:BoundField DataField="GrandTotal" HeaderText="Grand Total" />
                <asp:BoundField DataField="Updated" HeaderText="Updated" />
            </Columns>
            <EmptyDataTemplate>
                <h4 class="text-danger">No Student Enroll In This Class.</h4>
            </EmptyDataTemplate>
        </asp:GridView>
        <p>
            &nbsp;
</asp:Content>
