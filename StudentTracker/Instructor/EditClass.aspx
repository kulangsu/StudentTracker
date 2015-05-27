<%@ Page Title="Edit Class for " Language="C#" MasterPageFile="~/Instructor/Instructor.master" AutoEventWireup="true" CodeBehind="EditClass.aspx.cs" Inherits="StudentTracker.Instructor.EditClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style> .ui-autocomplete { font-size:11px; text-align:left; } </style>
       <script lang="javascript" type="text/javascript">
           $(document).ready(function () {
               //auto completed textbox when type
               $('#<%=ClassName.ClientID%>').autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: "../AjaxController.aspx/GetCourseList",
                            data: "{ 'pre':'" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return { value: item }
                                }))
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus);
                            }
                        });
                    }
                });
            });
    </script>
    <div class="form-horizontal">
        <div class="row">
            <div class="col-md-offset-3 col-md-8">
                <h2><%: Title %>
                    <asp:Literal ID="CourseName" runat="server"></asp:Literal>
                </h2>
               
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
                    <asp:DropDownList ID="selectQuarterYear" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="selectQuarterYear_SelectedIndexChanged" >
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ClassName" CssClass="col-md-3 control-label">Class Name</asp:Label>
                <div class="col-md-1">
                    <asp:DropDownList ID="CourseArea" runat="server" CssClass="form-control" AutoPostBack="True"  OnSelectedIndexChanged="CourseArea_SelectedIndexChanged" Width="75px">
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:DropDownList ID="CourseNumber" runat="server" CssClass="form-control" AutoPostBack="True" Width="75px"></asp:DropDownList>
                </div>
                <div class="col-md-2">                    
                    <asp:DropDownList ID="CourseSection" runat="server" CssClass="form-control" AutoPostBack="True" Width="100px" DataSourceID="SqlCourseSectionList" DataTextField="Section" DataValueField="Section"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="ClassName" CssClass="form-control cap_first_letter textboxAuto" />                     
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ClassName"
                        CssClass="text-danger" ErrorMessage="Class Name field is required." Display="Dynamic" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-3 col-md-4">
                <asp:Button ID="btnClassUpdate" runat="server" OnClick="UpdateClass_Click" Text="Update Class" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
<%--    <DIV CLASS="ROW">
        <DIV CLASS="COL-MD-OFFSET-3 COL-MD-6">
            <H4 CLASS="TEXT-DANGER">
                &NBSP;</H4>
        </DIV>
        <DIV CLASS="COL-MD-OFFSET-2 COL-MD-9">
            <H4>COURSES LINK TO:
                <ASP:LITERAL RUNAT="SERVER" ID="FULLNAME" /></H4>
        </DIV>
        <DIV CLASS="COL-MD-OFFSET-2 COL-MD-9">
            <H4>ALL COURSES BELOW LINK TO OTHER INSTRUCTORS:</H4>
        </DIV>
    </DIV>--%>

</asp:Content>
