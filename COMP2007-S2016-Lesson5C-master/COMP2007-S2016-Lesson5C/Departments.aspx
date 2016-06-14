<%@ Page Title="Departments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="university1.Departments" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Departments List</h1>
                <a href="DepartmentDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> ADD Department</a>

                <label for="PageSizeDropDownList">Records per page: </label>
                <asp:DropDownList ID="PageSizeDropDownList" runat="server" AutoPostBack="true" 
                    CssClass="btn btn-default btn-sm dropdown-toggle" OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                    <asp:ListItem Text="3" Value="3" />
                    <asp:ListItem Text="5" Value="5" />
                    <asp:ListItem Text="All" Value="10000" />
                </asp:DropDownList>

                <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover" 
                    ID="DepartmentGridView" AutoGenerateColumns="false" DataKeyNames="DepartmentID" 
                    AllowPaging="true" PageSize="3" OnPageIndexChanging="DepartmentGridView_PageIndexChanging"
                    AllowSorting="true" OnSorting="DepartmentGridView_Sorting" OnRowDataBound="DepartmentGridView_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="DepartmentID" HeaderText="Department ID" Visible="true" SortExpression="DepartmentID" />
                        <asp:BoundField DataField="Name" HeaderText="Department Name" Visible="true" SortExpression="Name"/>
                        <asp:BoundField DataField="Budget" HeaderText="Budget" Visible="true" SortExpression="Budget"
                            DataFormatString="{0:C}" />
                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" NavigateUrl="~/DepartmentDetails.aspx.cs"
                            DataNavigateUrlFields="DepartmentID" DataNavigateUrlFormatString="DepartmentDetails.aspx?DepartmentID={0}" 
                            ControlStyle-CssClass="btn btn-primary btn-sm"/>
                          <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete" ShowDeleteButton="true"
                            ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                     </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
