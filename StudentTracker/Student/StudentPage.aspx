<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentPage.aspx.cs" Inherits="StudentTracker.Student.StudentPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

         <h1>
         <asp:Label ID="Lbl_pageTitle" runat="server" Text="Student Homepage"></asp:Label><br /><br< />       
         </h1>

          <h2><%: Title %><span style="font-size: 24px; color: #ff6a00" class="glyphicon glyphicon-book"></span>&nbsp
                <asp:Label ID="Welcome" runat="server"></asp:Label>
             </h2>
                 <asp:Button ID="btnEditProfile" runat="server" Text="Edit Profile" Font-Size="Larger" OnClick="btnEditProfile_Click" Width="148px" />
                 <br />
                 <asp:Button ID="btnChangePswd" runat="server"  Text="Change Password" Font-Size="Larger"  OnClick="btnChangePswd_Click" Width="216px" />

         <br />
         <br />

    <div class="col-md-offset-2 col-md-9">
            <h4>Courses You have Joined
                </h4>
            <asp:GridView ID="GridViewStudentClassList" runat="server" CssClass="table" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="CourseName" HeaderText="Course Name" SortExpression="CourseName" />
                    <asp:BoundField DataField="Quarter" HeaderText="Quarter" SortExpression="Quarter" />
                    <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                </Columns>
            </asp:GridView>
        </div>

                <h3>Join A Class</h3>
            <h4>
                <asp:Literal runat="server" ID="ErrorMessage" />
            </h4>
            <p>
                <asp:DropDownList ID="drpDwn_Join" runat="server" DataTextField="Name" DataValueField="ID" Font-Size="Larger" Height="25px" Width="300px">
                </asp:DropDownList>&nbsp;&nbsp;<asp:Button ID="btnSelect" runat="server" Font-Size="Larger" OnClick="bntSelect_Click" Text="Select" />
                &nbsp;
            </p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>


</asp:Content>
