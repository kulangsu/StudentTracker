<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="StudentClass.aspx.cs" Inherits="StudentTracker.Student.StudentClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

             <h1>
         <asp:Label ID="Lbl_pageTitle" runat="server" Text="Student Class"></asp:Label>&nbsp;&nbsp;&nbsp;
                 
                 <br /><br< />       
            </h1>
            <h2>
            <asp:Label ID="Label1" runat="server" Text="Select Homework:" Font-Size="Large"></asp:Label>
                 <asp:DropDownList ID="drpDwnSelect" runat="server" Font-Size="Medium" DataTextField="Assignment" DataValueField="ID"  Height="25px" Width="211px" OnSelectedIndexChanged="drpDwnSelect_SelectedIndexChanged">
             </asp:DropDownList></h2>
             <p>
                 <asp:TextBox ID="TextBox1" runat="server" Width="155px" Height="20px"></asp:TextBox>
&nbsp;&nbsp;
                 <asp:Button ID="btnBrowse" runat="server" Text="Browse" Width="156px" Height="30px" Font-Size="Larger" OnClick="btnBrowse_Click" />
                 <p>
                     &nbsp;<p>
                 <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="156px" Height="30px" Font-Size="Larger" OnClick="btnSubmit_Click" />

             <p>
                 &nbsp;<p>
                 &nbsp;<asp:GridView ID="Assignments" runat="server" AutoGenerateColumns="False" CssClass="table" OnSelectedIndexChanged="Assingments_SelectedIndexChanged" PageSize="100">
                 <Columns>
                     <asp:BoundField DataField="AssignmentName" HeaderText="Assignment Name"  />
                     <asp:BoundField DataField="Grade" HeaderText="Grade" />
                     <asp:BoundField DataField="Attendance" HeaderText="Attendance"  />
                     <asp:BoundField DataField="GrandTotal" HeaderText="Grand Total" />
                     <asp:BoundField DataField="Updated" HeaderText="Updated" />
                 </Columns>
                 <EmptyDataTemplate>
                     <h4 class="text-danger">No Student Enroll In This Class.</h4>
                 </EmptyDataTemplate>
             </asp:GridView>
             <p>
                 &nbsp;</asp:Content>