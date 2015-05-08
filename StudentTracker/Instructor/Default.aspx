<%@ Page Title="Instructor Homepage" Language="C#" MasterPageFile="~/Instructor/Instructor.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StudentTracker.Instructor.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div class="row">
            <div class="col-md-8">
                <h2><%: Title %></h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <h4>Select Quarter to show list of classes: <%: DateTime.Now.Year %></h4>
                    <asp:Label runat="server" AssociatedControlID="qrtWinter">Winter</asp:Label>
                    <asp:CheckBox ID="qrtWinter" runat="server" />
                    <asp:Label runat="server" AssociatedControlID="qrtSpring">Spring</asp:Label>
                    <asp:CheckBox ID="qrtSpring" runat="server" />
                    <asp:Label runat="server" AssociatedControlID="qrtSummer">Summer</asp:Label>
                    <asp:CheckBox ID="qrtSummer" runat="server" />
                    <asp:Label runat="server" AssociatedControlID="qrtFall">Fall</asp:Label>
                    <asp:CheckBox ID="qrtFall" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <h4>Classes list below not yet assign to instructore</h4>
                <asp:GridView ID="GridViewClassesList" runat="server" CssClass="table">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
