﻿<%@ Page Title="Domestic | Account Head Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="AccountSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Domestic |  Account Head Summary .</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Account Head Summary  
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
<div class="panel-body" >
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
                 
<asp:UpdatePanel ID="updatepanel2" runat="server" UpdateMode="Conditional"> 
<ContentTemplate>
                                    
                                                
<div class="row">
<div class="col-md-4 col-xs-12" >
<div class="form-group text-label">
<b >Account Name</b>
<asp:TextBox ID="txtAccountN" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Account Name"
runat="server"   ></asp:TextBox>
</div>
</div> 

<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
                                              
                                      
</div>
                                               
</div>
</ContentTemplate>
</asp:UpdatePanel>

                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="7" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

<asp:TemplateField>
<ItemTemplate>
                                                          
<a  href='<%# "AccountMaster.aspx?AccountIDView=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "AccountID")).ToString())%>' target="_blank"
Class='btn btn-success btn-xs outline' 
>View</a>

<a  href='<%# "AccountMaster.aspx?AccountIDEdit=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "AccountID")).ToString())%>' target="_blank"
Class='btn btn-info btn-xs outline' 
>Edit</a>
                                                         
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="150px" />
</asp:TemplateField>
   
<asp:BoundField DataField="AccountID" HeaderText="Account ID"></asp:BoundField>
<asp:BoundField DataField="AccountName" HeaderText="Account Name"></asp:BoundField>
<asp:BoundField DataField="TallyName" HeaderText="Tally Name"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="HSN_Code" HeaderText="HSN Code"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="IsActive" HeaderText="Is Active"></asp:BoundField>
</Columns>

</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>


</div>
</div>
</div>
</div>
                          
                     
                       
                       
</div>
                 
</ContentTemplate>
</asp:UpdatePanel>
</div>
         
</div>
<%-- <script type="text/javascript">
function checkRadioBtn(id) {
var gv = document.getElementById('<%=grdcontainer.ClientID%>');

for (var i = 1; i < gv.rows.length; i++) {
var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

// Check if the id not same
if (radioBtn[0].id != id.id) {
radioBtn[0].checked = false;
}
}
}
</script>--%>
<%--  <script type="text/javascript">
 
function BondExPrint() {
            
var NOCNo1= document.getElementById('<%= txtNOCNo.ClientID%>').value;
             
var url = "../Report_Bond/BondEx_logo_print.aspx?NOCNo=" + NOCNo1;
//alert("hi")
                
window.open(url);

}


</script>--%>
</asp:Content>
