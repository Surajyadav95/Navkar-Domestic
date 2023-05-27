<%@ Page Title="Domestic | Gate In By Rail" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="GateINRRWise.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic | Gate In By Rail</title>
       
</head>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i> Gate In By Rail
</h3>           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
 
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Gate In By Rail
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
 <div class="row">
<div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >ID</b>
<asp:TextBox ID="txtvendorID" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
  <div class="col-md-2" style="display:none">
    <asp:TextBox runat="server" ID="txtSlipNoPrint"></asp:TextBox>
    </div>
      <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >Gate In Date & Time</b>
                    <asp:TextBox ID="txtGateInDate" placeholder="dd-MM-yyyy" TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>
</div>

    <div class="row">
         <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >RR No</b>
<asp:TextBox ID="txtRRNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="RR No"
runat="server"   ></asp:TextBox>
</div>
</div>     
 

        <div class="col-sm-1 col-xs-6">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary' OnClick="lnksearch_Click"  runat="server">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>
    </div>
 
    
    <br />
    
         <div class="row">
             <div class="col-lg-12 col-xs-12 text-label " >
<div class="table-responsive">
<asp:GridView ID="grdRelianceDets" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover  "
AutoGenerateColumns="false" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
    <asp:TemplateField HeaderText="Jo No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblJONO" runat="server" text='<%#Eval("JONO")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
 
    <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblContainerNo" runat="server" text='<%#Eval("ContainerNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="ContainerType" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblContainerType" runat="server" text='<%#Eval("ContainerType")%>'></asp:Label>
            <asp:Label ID="lblContainerTypeID" Visible="false" runat="server" text='<%#Eval("CONTAINERTYPEID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="No of Bags" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblNoofBags" runat="server" text='<%#Eval("PKGS")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Wt" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblWt" runat="server" text='<%#Eval("WEIGHT")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Product Grade" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblPRODUCTGRADE" runat="server" text='<%#Eval("PRODUCTGRADE")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Destination" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDESTINATION" runat="server" text='<%#Eval("Location")%>'></asp:Label>
              <asp:Label ID="lblLocationID" Visible="false" runat="server" text='<%#Eval("FROMLOCATION")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Ship To Party Name" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSHIPTOPARTYNAME" runat="server" text='<%#Eval("ShipTo_PartyName")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Tax Invoice" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblTAXINVOICENO" runat="server" text='<%#Eval("TaxInvoice")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="W-Way Bill Details" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblEWAYBILLDETAILS" runat="server" text='<%#Eval("E_WayBillDetails")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Valid Up To" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblVALIDUPTO" runat="server" text='<%#Eval("ValidUpTo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSize" runat="server" text='<%#Eval("Size")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Seal" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSeal" runat="server" text='<%#Eval("Seal")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Grade" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblGrade" runat="server" text='<%#Eval("Grade")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
     
</Columns>
</asp:GridView>
</div>
</div>
         </div>      
</asp:Panel>
     <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline " runat="server"  OnClick="btnSave_Click"   
Text="Save"  OnClientClick="return ValidationSave()" />
</div>                                                                             
</div>
                       
<div class="col-sm-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="CargoJo.aspx" id="btnclear" runat="server" class="btn btn-primary btn btn-sm outline ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
     
                          
</div>                    
</div>
</div>


 
<asp:Label ID="lblcode" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblagentname" Visible="false" runat="server" Text=""></asp:Label>
             
                      
                    
                   
                         
<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
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
                   
<a href="GateINRRWise.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
               
</fieldset>

</div>
</div>
                               
</div>
                           
                          
                     
                       
                       
</div>
                 
              
</div>
       
         
</div>
   
 

<script type="text/javascript">

$(document).ready(function () {

//alert('hi')
$('.dummy').datepicker({
format: 'yyyy-mm-dd',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,



})

});

</script>  

<script type="text/javascript">

$(document).ready(function () {

//alert('hi')
$('.dummy2').datepicker({
format: 'yyyy-mm-dd',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,



})

});

</script> 
    
  
          

<script type="text/javascript">
function ValidateQty() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58))
return event.returnValue;
return event.returnValue = '';
}

function checkEmail(str) {
var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

if (reg.test(emailField.value) == false) {
alert('Invalid Email Address');
return false;
}

return true;
}

function CheckTelephone(tel) {

if (tel.length < 7)
alert("Invalid Telephone number.")
}

function CheckMobile(mob) {
if (mob.length < 10)
alert("Invalid Mobile number.");

}
</script>

<script type="text/javascript">
function ValidatePhoneNo() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 43 || event.keyCode == 32 || event.keyCode == 40 || event.keyCode == 41)
return event.returnValue;
return event.returnValue = '';
}

function checkEmail(str) {
var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

if (reg.test(emailField.value) == false) {
alert('Invalid Email Address');
return false;
}

return true;
}

function CheckTelephone(tel) {

if (tel.length < 7)
alert("Invalid Telephone number.")
}

function CheckMobile(mob) {
if (mob.length < 10)
alert("Invalid Mobile number.");

}
</script>
</asp:Content>
