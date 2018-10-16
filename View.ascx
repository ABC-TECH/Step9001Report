<%@ Control language="C#" Inherits="DotNetNuke.Modules.Step9001Report.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<style type="text/css">
    .beta table, .beta tr, .beta td, .beta th
    {
        border: 1px solid #e3e3e3;
        vertical-align: middle; /*border-spacing: 10px;
             border-collapse: separate;*/
    }
    
    .beta td
    {
        font-weight: normal;
        vertical-align: middle;
        max-width: 250px;
        overflow-wrap: break-word;
    }
    
    .beta th
    {
        color: #fff;
        font-weight: normal;
        vertical-align: middle;
        text-align: center;
    }
    
    .centerTD
    {
        text-align: center;
    }
    
    .rightTD
    {
        text-align: right;
    }
    
    .toupper
    {
        text-transform: uppercase;
    }
    
    .paginationS table, .paginationS tr, .paginationS td, .paginationS th
    {
        border: none;
        margin-top: 15px;
        padding: 3px;
        vertical-align: middle;
        background-color: #fff;
    }
    
    .paginationS
    {
        border: 1px solid white !important;
    }
    
    .paginationS td, .paginationS tr
    {
        border: none;
        text-decoration: none;
        white-space: nowrap;
    }
    
    .paginationS a, .paginationS a:hover, .paginationS a:visited
    {
        font-weight: bold;
        color: #565656;
        text-decoration: none;
        white-space: nowrap;
    }
    .paginationS span
    {
        color: Red;
        font-weight: bold;
        border: 1px solid #808080;
        text-decoration: none;
        white-space: nowrap;
        padding: 2px 4px 2px 4px;
        background-color: #fff;
    }
    
    th.sortasc a
    {
        display: block;
        padding: 0 0 0 8px;
        line-height: 12px;
        background: url(<%= ResolveUrl("~/DesktopModules/ABC_STEP_9002_Report/img/asc.png")%>) no-repeat;
    }
    
    th.sortdesc a
    {
        display: block;
        padding: 0 0 0 8px;
        line-height: 12px;
        background: url(<%= ResolveUrl("~/DesktopModules/ABC_STEP_9002_Report/img/desc.png")%>) no-repeat;
    }
    
    th.sortdefault a
    {
        display: block;
        padding: 0 0 0 8px;
        line-height: 12px;
    }
    
    .navSTEP, ul.navSTEP
    {
        list-style-type: none;
        margin: 0;
        padding: 0;
        float: left;
        padding-bottom: 10px;
    }
    
    .nav-itemSTEP, li.nav-itemSTEP a
    {
        display: inline !important;
        width: 60px;
        padding-right: 20px;
    }
    
    .navLegend, ul.navLegend
    {
        margin: 0;
        padding: 0;
        float: left;
        padding-bottom: 10px;
    }
    
    .nav-itemLegend, li.nav-itemLegend
    {
        display: inline !important;
        width: 60px;
        margin-right: 20px;
    }
    
    .inc
    {
        padding: 5px;
        background: url(<%= ResolveUrl("~/DesktopModules/ABC_STEP_9002_Report/img/incomplete.png")%>);
    }
    
    .notsubmitted
    {
        padding: 5px;
        background: url(<%= ResolveUrl("~/DesktopModules/ABC_STEP_9002_Report/img/notsubmitted.png")%>);
    }
    
    .notpaid
    {
        padding: 5px;
        background: url(<%= ResolveUrl("~/DesktopModules/ABC_STEP_9002_Report/img/notpaid.png")%>);
    }
    
    .loading
    {
        position: fixed;
        background: url('../../../images/loading.gif') 50% 50% no-repeat;
        height: 100%;
        width: 100%;
        left: 0px;
        top: 0px;
        z-index: 999999;
        opacity: 1;
    }
    
    /*need to break the page skin width*/
    .skin_width
    {
        width: 100%;
        max-width: 100% !important;
        min-width: 767px;
        margin: 0 auto;
        position: relative;
    }
</style>
<div id="navsection" style="clear: both; position: relative;">
    <ul class="navSTEP">
        <li class="nav-itemSTEP">Application Year: &nbsp;&nbsp;
            <asp:DropDownList ID="ddlApplicationYear" runat="server" AutoPostBack="False">
            </asp:DropDownList>
        </li>
        <li class="nav-itemSTEP">Chapter: &nbsp;&nbsp;
            <asp:DropDownList ID="ddlChapter" runat="server" AutoPostBack="False">
          
            </asp:DropDownList>
        </li>
              <li class="nav-itemSTEP">Program: &nbsp;&nbsp;
            <asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="False">
              <asp:ListItem Text="Step" Value = "Step" Selected ="True"></asp:ListItem>
            <asp:ListItem Text="STEP for Suppliers" Value = "STEP for Suppliers"></asp:ListItem>
             <asp:ListItem Text="First STEP" Value="First STEP"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="nav-itemSTEP">
            <asp:ImageButton ID="imgbExportExcel" runat="server" AlternateText="Export to Excel"
                ToolTip="Export to Excel" ImageUrl="~/DesktopModules/ABC_STEP_9002_Report/img/excel_export.png"
                OnClick="ExportToExcel_Click" Width="15px" /></li>
    </ul>
</div>
