﻿<%@ Page Title="Domestic | Modify Container Out Date" Language="VB" EnableEventValidation="false" MasterPageFile="~/Domestic/PopUp.master" AutoEventWireup="false" CodeFile="ModifyLoadedDate.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
window.opener.document.getElementById("MainContent_btnIndentItem").click();
self.close();
}
</script>
<div class="container" style="background-color: white">

<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<h1>Modify Container Out Date<small class="pull-left" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>
<div class="row ">
 
<div class="col-sm-4 col-xs-5 ">
<div class="form-group text-label">
<b  >Loading Sheet No</b>
<asp:TextBox ID="txtLoadedID" Style="text-transform:uppercase;" class="form-control text-label"  placeholder="Loading Sheet No"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-2 col-xs-4 pull-left">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnsearch" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnsearch_Click"  
    OnClientClick="return ValidationSave()" Text="Show"  />
</div>                                                                                   
</div>
    <div class="col-sm-4 col-xs-5 ">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtContainerno" Style="text-transform:uppercase;" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>
</div>

    <div class="row">
         <div class="col-sm-4 col-xs-12">                                      
<div class="form-group text-label">
<b>Loading Sheet Date</b>                                         
<asp:TextBox ID="txtStuffdate"  placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
</div>
</div>
    </div>

    <div class="row">
<div class="col-sm-4 col-xs-5">
<div class="form-group text-label">
<b  >Stuffed Qty</b>
<asp:TextBox ID="txtStuffedPkgs" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Stuffed Qty"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>


    <div class="row">
<div class="col-sm-4 col-xs-6">
<div class="form-group text-label">
<b  >Stuffed Wt</b>
<asp:TextBox ID="txtStuffedWeight" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Stuffed Wt"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>
 
 


<div class="row">
<div class="col-sm-4 col-xs-5">
<div class="form-group text-label">
<b  >Seal No</b>
<asp:TextBox ID="txtDCANo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Seal No"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>
 

  

 

    <div class="row" style="display:none">
<div class="col-sm-6 col-xs-6">
<div class="form-group text-label">
<b  >Remarks</b>
<asp:TextBox ID="txtRemarks" class="form-control text-label" TextMode="MultiLine"  placeholder="Remarks"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>

    <asp:Label ID="lblEntryID" Visible="false" runat="server" Text=""></asp:Label>

<div class="row">
<div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnUpdate" class="btn btn-primary btn-sm outline  " runat="server" OnClick="btnUpdate_Click"  
Text="Update"  OnClientClick="return ValidationSave()"  />
</div>                               
</div>
<div class="col-sm-1" style="padding-left:26px;">
<div class="form-group" style="padding-top:18px">                          
<a href="ModifyLoadedDate.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a>                               
</div>                                                                               
</div>
</div>

<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
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
                   
<a href="ModifyLoadedDate.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
                
</div>
</div>
     
    <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
 
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblquoteApprove"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btncancelyes" data-dismiss="modal" runat="server"  onserverclick="btncancelyes_ServerClick"  aria-hidden="true">
Yes 
</button>
<a href="ModifyLoadedDate.aspx" class="btn btn-danger ">No</a>
</div>
</div>
 
</div>
</div>             

</div>
</div>
</div>
<script type="text/javascript">
function ValidationSave() {
                 
    var txtLoadedID = document.getElementById('<%= txtLoadedID.ClientID%>').value;

    var txtContainerno = document.getElementById('<%= txtContainerno.ClientID%>').value;
                   

var blResult = Boolean;
blResult = true;
 

<%--if (txtContainerno == "") {
document.getElementById('<%= txtLoadedID.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}--%>
    if (txtLoadedID == "") {
        document.getElementById('<%= txtLoadedID.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>
</asp:Content>


