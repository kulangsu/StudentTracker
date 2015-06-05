<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/InstructorClass.master" AutoEventWireup="true" CodeBehind="AddHomework.aspx.cs" Inherits="StudentTracker.Instructor.AddHomework" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %>
                    <asp:Literal ID="CourseName" runat="server"></asp:Literal>
                </h2>
    <asp:Label ID="AddActionLabel" runat="server" Visible="False"></asp:Label><br /><br />
    <asp:Label ID="lblHmwName" runat="server" Text="Homework Name:"></asp:Label>&nbsp;<asp:TextBox ID="txtHmwName" runat="server" Width="195px"></asp:TextBox><br />
    <asp:Label ID="lblHmwPoints" runat="server" Text="Homework Points:"></asp:Label><asp:TextBox ID="TxtHmwPoints" runat="server" Width="195px"></asp:TextBox>
    <br />
    <asp:Label ID="lblHmwType" runat="server" Text="Homework Type:"></asp:Label>
    &nbsp; <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>
     
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;
<br /><br />
    <asp:Label ID="lblActStartDate" runat="server" Text="Activiation Start Date:"></asp:Label>
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" OnSelectionChanged="Calendar1_SelectionChanged">
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" />
        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <WeekendDayStyle BackColor="#FFFFCC" />
    </asp:Calendar>
    <br />


    End Date:
     <asp:RadioButton ID="RB_End" Checked="true" GroupName="DateDiff" runat="server"/>
    <asp:Label ID="LabelEndDate" runat="server"
        Text=""></asp:Label>
    <br />

    <asp:label ID="LabelMessage" runat="server"
        Text="Message"></asp:label>
    <br />

    <asp:Button ID="btnAddHmw" runat="server" Text="Add Homework" OnClick="btnAddHm_Click"/>
    &nbsp;&nbsp;&nbsp;&nbsp;
   <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CausesValidation="False" /><br /><br />
    
</asp:Content>
