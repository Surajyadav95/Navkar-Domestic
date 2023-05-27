<%@ Page Title="Domestic | Coil In Wagon wise" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticCoilInWagonWise.aspx.vb" Inherits="Summary_BCYMovement" culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic | Coil In Wagon wise</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> Coil In Wagon wise
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    </ContentTemplate>
</asp:UpdatePanel>--%>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
    <div class="col-sm-3 col-xs-12">
<div class="form-group text-label" style="text-decoration-color:black">
<b >Gate In Date</b>
<asp:TextBox ID="txtGateInDate" Style="text-transform: uppercase" TextMode="DateTimeLocal" class="form-control text-label form-cascade-control"
runat="server"></asp:TextBox>     
</div>
</div>
     <asp:Label ID="Lblfile" runat="server" style="display:none" Text="" ForeColor="red"></asp:Label>

 <div class="col-sm-3 col-xs-12 text-label" style="padding-top:25px">
     <label runat="server"  style="width:0px; margin-left:10px;" >
            <a style="display:block">
 <asp:FileUpload ID="FileUpload1" AllowMultiple="false"  runat="server" ClientIDMode="Static" /></a>
                                       </label>
       </div>
        
    <div class="col-sm-1 col-xs-12" style="padding-top:21px">
     <asp:Button ID="btnUpload" class="btn btn-success btn-sm outline"  Text="Import" runat="server" OnClientClick="return ClassChange()" onclick="btnUpload_Click"    />  
     <asp:postbacktrigger controlid="btnUpload" xmlns:asp="#unknown"  />
        <b><asp:Label runat="server" ID="lblfilename" Text=""></asp:Label></b>
        </div>

    <div class="col-sm-1 col-xs-12" style="padding-top:21px">
<div class="form-group"  >
<asp:LinkButton runat="server" ID="lnkDownloadExcel" OnClick="lnkDownloadExcel_Click" CssClass="btn btn-info btn-sm" ToolTip="Download Template"><i class="fa fa-download"></i></asp:LinkButton>
</div>
    </div>  

    <asp:HiddenField ID="hffile" runat="server" value="" />
     <asp:HiddenField ID="hdExist" runat="server" value="0" />
       
</div>

<div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdOutDets" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover  "
AutoGenerateColumns="false" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
    <asp:TemplateField HeaderText="Train No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblTrainNo" runat="server" text='<%#Eval("TrainNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Wagon No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblWagonNo" runat="server" text='<%#Eval("WagonNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Source Location" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSource" runat="server" text='<%#Eval("Source")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
      <asp:TemplateField HeaderText="To Location" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblToLocation" runat="server" text='<%#Eval("ToLocation")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Batch No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblBatchNo" runat="server" text='<%#Eval("BatchNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Batch Weight(Kgs)" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblBatchWeight" runat="server" text='<%#Eval("BatchWeight")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Shipment No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblShipmentNo" runat="server" text='<%#Eval("ShipmentNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Delivery No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDeliveryNo" runat="server" text='<%#Eval("DeliveryNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblCustomer" runat="server" text='<%#Eval("Customer")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Product" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblProduct" runat="server" text='<%#Eval("Product")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Commercial Invoice No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblComInvNo" runat="server" text='<%#Eval("ComInvNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="ODN No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblODNNo" runat="server" text='<%#Eval("ODNNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="FNR" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblFNR" runat="server" text='<%#Eval("FNR")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="E-Way Bill No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblEWayBillNo" runat="server" text='<%#Eval("EWayBillNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Validity" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblValidity" runat="server" text='<%#Eval("Validity")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

     <asp:TemplateField HeaderText="Party name" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblPartyname" runat="server" text='<%#Eval("Partyname")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Width" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblWidth" runat="server" text='<%#Eval("Width")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Thickness" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblThickness" runat="server" text='<%#Eval("Thickness")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
</Columns>
</asp:GridView>
</div>
</div>
</div>    
      
</asp:Panel>
                        
</div>
</div>


<div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="btnSave_Click"  
Text="Save"  OnClientClick="return ValidationSave()" />
</div>                                                                             
</div>
                       
<div class="col-sm-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="DomesticCoilInWagonWise.aspx" id="btnclear" runat="server" class="btn btn-primary btn btn-sm outline ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
    <div class="col-sm-4"></div>
        <div class="col-lg-6 pull-right" style="margin-left:6px;">
<div class="form-group ">
<a href="DomesticCoilInSummary.aspx" target="_blank"><b style="color:blue">Click here to view Coil In Summary</b> </a>
</div>
</div>                 
</div>                    
                    
                   
                         
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
                   
<a href="DomesticCoilInWagonWise.aspx" class="btn btn-info btn-block">OK</a>
                                
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
function ValidationSave() {
    document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary btn btn-sm outline disabled");    
    
    var txtTrainNo = document.getElementById('<%= txtGateInDate.ClientID%>').value;

var blResult = Boolean;
blResult = true;
      
if (txtGateInDate == "") {
    document.getElementById('<%= txtGateInDate.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;
}

    if (blResult == false) {
        document.getElementById('<%= btnSave.ClientID%>').value = "Save";
        document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary btn btn-sm outline");  
        alert('Please fill the required fields!');
}
return blResult;
}
</script>
    <script>
        function ClassChange() {

            document.getElementById('<%= btnUpload.ClientID%>').value = "Please Wait...";
            document.getElementById('<%= btnUpload.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline disabled");

        }
    </script>
<script type="text/javascript">
function ValidateQty() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 46)
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
