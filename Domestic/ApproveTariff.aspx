<%@ Page Title="Domestic |Approve Tariff" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ApproveTariff.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Domestic |Approve Tariff</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Approve Tariff Details  
</h3>
           
</div>
     
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body">
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
                 
<asp:UpdatePanel ID="updatepanel2" runat="server" UpdateMode="Conditional"> 
<ContentTemplate>
                                    
                                                
<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b>Tariff ID</b>
<asp:DropDownList ID="ddltraiff"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem>  
</asp:DropDownList>
</div>
</div>

<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b>Invoice Type</b>
<asp:DropDownList ID="ddlbondType"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="General">General</asp:ListItem>
<asp:ListItem Value="Other">Other</asp:ListItem>
<asp:ListItem Value="Transport">Transport</asp:ListItem>
    <asp:ListItem Value="General1">General 1</asp:ListItem>
    <asp:ListItem Value="Import">Import</asp:ListItem>
</asp:DropDownList>
</div>
</div>

<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSearch" class="btn btn-primary " runat="server"
OnClick="btnSearch_Click" 
Text="Show"     />
</div>                                                                                   
</div>

<div class="col-sm-1 col-xs-12" style="display:none;">
                                     
<div class="form-group" style="padding-top:20px">
<asp:LinkButton ID="lnkUpdate" ControlStyle-CssClass='btn btn-info'  runat="server"
OnClientClick="return Container()" >  
<i class=" fa fa-check"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>
                                               
</div>


</ContentTemplate>
</asp:UpdatePanel>
                     
<div class="row">
    <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div class="col-sm-3 col-xs-12">
        <div class="form-group">
            <asp:CheckBox runat="server" ID="chkSelectAll" AutoPostBack="true" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged" />
        </div>
    </div>


<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:5    px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
<asp:TemplateField Visible="false">
<ItemTemplate>
                                                         
<a  href='<%# "BondTraiffSetting.aspx?tariffIDView=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "tariffID ")).ToString())%>' target="_blank"
Class='btn btn-success btn-xs outline' 
>View</a>
                  <asp:Label runat="server" ID="lblentryid" Visible="false" Text='<%#Eval("entryID")%>'></asp:Label>                                       
</ItemTemplate>
 
</asp:TemplateField>
<asp:TemplateField HeaderText="Select">
<ItemTemplate>

<asp:CheckBox ID="chkright" Text=""  Checked='<%#Eval("Iscancel")%>' runat="server" AutoPostBack="true" OnCheckedChanged="chkright_CheckedChanged" />
</ItemTemplate>
                                                       
</asp:TemplateField>
                                                
<asp:BoundField DataField="tariffID" HeaderText="Tariff ID"></asp:BoundField>
<asp:BoundField DataField="TariffDescription" HeaderText="Tariff Desc"></asp:BoundField>

<asp:BoundField DataField="bondtype" HeaderText="Bond Type"></asp:BoundField>
<asp:BoundField DataField="accountID" HeaderText="Account Name"></asp:BoundField>
<asp:BoundField DataField="size" HeaderText="Size"></asp:BoundField>
<asp:BoundField DataField="SORF" HeaderText="SorF"></asp:BoundField>
<asp:BoundField DataField="FIXEDAMT" HeaderText="Fixed Amount"></asp:BoundField>
    <asp:BoundField DataField="Criteria" HeaderText="Criteria"></asp:BoundField>
</Columns>

</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>

<div class="col-sm-2 pull-right" style="padding-right:0px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btncancel" class="btn btn-primary " runat="server" OnClick="btncancel_Click"
Text="Approve Tariff"/>
</div>
                                              
                                      
</div>


</div>
</div>   
                                 
                               
</div>
</div>
                          
                     
                       
                       
</div>
                 
</ContentTemplate>
</asp:UpdatePanel>
</div>
<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<span><i runat="server" id="I3" class="fa fa-5x fa-check-circle-o text-success"></i></span>
<br />
<h4 class="modal-title">

<asp:Label ID="lblSession" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label></h4>
</center>
</div>
<div class="modal-footer">
<button data-dismiss="modal" class="btn btn-info btn-block">OK</button>
</div>
</div>

    </ContentTemplate></asp:UpdatePanel>                  
</div>
</div>
</div>
  
</asp:Content>
