<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/Instructor.master" AutoEventWireup="true" CodeBehind="EditHomework.aspx.cs" Inherits="StudentTracker.Instructor.EditHomework" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Label ID="lblAddHmw" runat="server" Text="Edit Homework Page"></asp:Label><br /><br />
    <asp:Label ID="lblHmwName" runat="server" Text="Homework Name:"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtHmwName" runat="server" Width="195px"></asp:TextBox><br />
    <asp:Label ID="lblActStartDate" runat="server" Text="Activiation Start Date:"></asp:Label>
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" />
        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <WeekendDayStyle BackColor="#FFFFCC" />
    </asp:Calendar>
    <asp:Label ID="lblActStopDate" runat="server" Text="Activation Stop Date:"></asp:Label>
    &nbsp;<br />
    <asp:Calendar ID="Calendar2" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
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
        <asp:Label ID="lblStatus" runat="server" Text="Status:" Font-Bold="true"></asp:Label>
        <asp:RadioButton ID="rdActivate" runat="server" Text="Activate" Width="101px" style="margin-left: 33px" OnCheckedChanged="rdActivate_CheckedChanged"/>
        <asp:RadioButton ID="rdDeactivate" runat="server" Text="Deactivate" OnCheckedChanged="rdDeactivate_CheckedChanged"/>
        <br />
    <asp:Button ID="btnAddHmw" runat="server" Text="Save" OnClick="btnAddHm_Click" Width="73px"/>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click"/>
</asp:Content>

