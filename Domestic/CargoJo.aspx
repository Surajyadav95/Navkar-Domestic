<%@ Page Title="Domestic | Cargo Jo" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="CargoJo.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic | Cargo JO</title>
       
</head>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i> Cargo JO
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
Cargo JO
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
 
  <div class="row">
<div class="col-sm-1 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >JO No</b>
<asp:TextBox ID="txtJONo" Style="text-transform: uppercase;" ReadOnly  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >JO Date</b>
<asp:TextBox ID="txtJODate" Style="text-transform:uppercase" class="form-control text-label" TextMode="DateTimeLocal"
runat="server"   ></asp:TextBox>
</div>
</div>
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Format</b>
<asp:DropDownList ID="ddlFormat" Style="text-transform: uppercase;" runat="server" class="form-control text-label"   OnSelectedIndexChanged="ddlFormat_SelectedIndexChanged" AutoPostBack="true" >
    <asp:ListItem Value="General">General</asp:ListItem>
    <asp:ListItem Value="System">System</asp:ListItem>
     <asp:ListItem Value="Reliance">Reliance</asp:ListItem>
</asp:DropDownList>
</div>
</div>
   

</div>   


    
    <div class="row">
    <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Customer</b>
<asp:DropDownList ID="ddlCustomer" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>

            <div class="col-md-4 col-xs-12">
    <div class="form-group text-label">
    <%--<span class="required">*</span>--%>
<b  >Commodity</b>
<asp:DropDownList ID="ddlCommodity" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
        </div>
</div>     
    
<div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >From</b>
<asp:DropDownList ID="ddlFrom" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
</asp:DropDownList>
</div>
</div>
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<%--<span class="required">*</span>--%>
<b>To</b>
<asp:DropDownList ID="ddlto" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
</asp:DropDownList>
</div>
</div>
</div>

<div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Weight</b>
<asp:TextBox ID="txtweight" Style="text-transform:uppercase" class="form-control text-label"  placeholder="weight"
runat="server"   ></asp:TextBox>
</div>
</div>
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >PKGS</b>
<asp:TextBox ID="txtPKGS" Style="text-transform:uppercase" class="form-control text-label"  placeholder="PKGS"
runat="server"   ></asp:TextBox>
</div>
</div>  
    <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<%--<span class="required">*</span>--%>
<b>Location</b>
<asp:DropDownList ID="ddlLocation" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
</asp:DropDownList>
</div>
</div>       
</div>
     <div class="row">
         <asp:Label ID="Lblfile" runat="server" style="display:none" Text="" ForeColor="red"></asp:Label>

 <div class="col-sm-3 col-xs-12 text-label" style="padding-top:25px">
     <label runat="server"  style="width:0px; margin-left:10px;" >
            <a style="display:block">
 <asp:FileUpload ID="FileUpload1" AllowMultiple="false"  runat="server" ClientIDMode="Static" /></a>
                                       </label>
       </div>
        
    <div class="col-sm-1 col-xs-12" style="padding-top:21px">
     <asp:Button ID="btnUpload" class="btn btn-success btn-sm outline"  Text="Import" runat="server"  onclick="btnUpload_Click"   />  
     <asp:postbacktrigger controlid="btnUpload" xmlns:asp="#unknown"  />
        <b><asp:Label runat="server" ID="lblfilename" Text=""></asp:Label></b>
        </div>

    <div class="col-sm-1 col-xs-12" style="padding-top:21px;">
<div class="form-group"  >
<asp:LinkButton runat="server" ID="lnkDownloadExcel"  OnClick="lnkDownloadExcel_Click"  CssClass="btn btn-info btn-sm" ToolTip="Download Template"><i class="fa fa-download"></i></asp:LinkButton>
</div>
    </div>  

    <asp:HiddenField ID="hffile" runat="server" value="" />
     <asp:HiddenField ID="hdExist" runat="server" value="0" />


         <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >RR No</b>
<asp:TextBox ID="txtRRNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="RR No"
runat="server"   ></asp:TextBox>
</div>
</div>  
     </div>
    <br />
    <div class="row">
<div class="col-lg-12 col-xs-12 text-label " id="divGenSys" runat="server">
<div class="table-responsive">
<asp:GridView ID="grdOutDets" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover  "
AutoGenerateColumns="false" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
    <asp:BoundField DataField="SrNo" HeaderText="Sr No" />
    <asp:TemplateField HeaderText="Mode" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblMode" runat="server" text='<%#Eval("Mode")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Destination" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDestination" runat="server" text='<%#Eval("Destination")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblLocation" runat="server" text='<%#Eval("Location")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Batch No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblBatchNo" runat="server" text='<%#Eval("BatchNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Do No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDoNo" runat="server" text='<%#Eval("DoNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="DO Quantity" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDoQty" runat="server" text='<%#Eval("DoQty")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Thickness" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblThickness" runat="server" text='<%#Eval("Thickness")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Width/Diameter" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblWidth" runat="server" text='<%#Eval("Width")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Transport Zone" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblTransportZone" runat="server" text='<%#Eval("TransportZone")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="SR Grade/Pipe Grade" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSRPipeGrade" runat="server" text='<%#Eval("SRPipeGrade")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="P From" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblPFrom" runat="server" text='<%#Eval("PFrom")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Region" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblRegion" runat="server" text='<%#Eval("Region")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Allocation Zone" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblAllocationZone" runat="server" text='<%#Eval("AllocationZone")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Material Descriptions" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblMaterial" runat="server" text='<%#Eval("Material")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Net Weight" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblNetWt" runat="server" text='<%#Eval("NetWt")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Gross Weight" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblWeight" runat="server" text='<%#Eval("Weight")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Del Age" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDelAge" runat="server" text='<%#Eval("DelAge")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblRemarks" runat="server" text='<%#Eval("Remarks")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
</Columns>
</asp:GridView>
</div>
</div>

         
</div>
         <div class="row">
             <div class="col-lg-12 col-xs-12 text-label " id="divReliance"  style="display:none"  runat="server">
<div class="table-responsive">
<asp:GridView ID="grdRelianceDets" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover  "
AutoGenerateColumns="false" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
<asp:BoundField DataField="SrNo" HeaderText="Sr No" />
    <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblContainerNo" runat="server" text='<%#Eval("ContainerNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="ContainerType" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblContainerType" runat="server" text='<%#Eval("ContainerType")%>'></asp:Label>
            <asp:Label ID="lblContainerTypeID" Visible="false" runat="server" text='<%#Eval("ContainerTypeID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="No of Bags" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblNoofBags" runat="server" text='<%#Eval("NoofBags")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Wt" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblWt" runat="server" text='<%#Eval("Wt")%>'></asp:Label>
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
       <asp:Label ID="lblDESTINATION" runat="server" text='<%#Eval("DESTINATION")%>'></asp:Label>
              <asp:Label ID="lblLocationID" Visible="false" runat="server" text='<%#Eval("LocationID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Ship To Party Name" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSHIPTOPARTYNAME" runat="server" text='<%#Eval("SHIPTOPARTYNAME")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Tax Invoice" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblTAXINVOICENO" runat="server" text='<%#Eval("TAXINVOICENO")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="W-Way Bill Details" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblEWAYBILLDETAILS" runat="server" text='<%#Eval("EWAYBILLDETAILS")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Valid Up To" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblVALIDUPTO" runat="server" text='<%#Eval("VALIDUPTO")%>'></asp:Label>
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
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline " runat="server"   OnClick="btnSave_Click"
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
                   
<a href="CargoJo.aspx" class="btn btn-info btn-block">OK</a>
                                
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
   
 <script>
     function Test() {
         debugger;
         var Format = document.getElementById('<%= ddlFormat.ClientID%>').value;
         if (Format == "General" || Format == "System") {
       
             $("#divGenSys").show();
             $("#divReliance").hide();
         }
         if (Format == "Reliance") {
             $("#divGenSys").hide();
             $("#divReliance").show();
             
         }
     }
 </script>

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
