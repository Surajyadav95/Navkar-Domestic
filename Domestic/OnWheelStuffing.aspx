<%@ Page Title="Domestic | On Wheel Stuffing" Language="VB" MasterPageFile="User.master" AutoEventWireup="false" Culture="en-GB"
CodeFile="OnWheelStuffing.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic | On Wheel Stuffing</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> On Wheel Stuffing
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
    <div class="col-sm-3 col-xs-12" style="display:none">
<div class="form-group text-label" style="text-decoration-color:black">
<b >Train No</b>
<asp:TextBox ID="txtTrainNo" Style="text-transform: uppercase" MaxLength="20" class="form-control text-label form-cascade-control"
runat="server"></asp:TextBox>     
</div>
</div>
    <div class="col-sm-3 col-xs-12">
<div class="form-group text-label" style="text-decoration-color:black">
<b >Location</b>
<asp:DropDownList ID="ddlLocation" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                      
</asp:DropDownList>   
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
    <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblContainerNo" runat="server" text='<%#Eval("ContainerNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Stuff Pkgs" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblStuffPkgs" runat="server" text='<%#Eval("StuffPkgs")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Stuff Weight" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblStuffWeight" runat="server" text='<%#Eval("StuffWeight")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Stuff Date" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblStuffDate" runat="server" text='<%#Eval("StuffDate")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
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
    <asp:TemplateField HeaderText="Invoice No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblInvoiceNo" runat="server" text='<%#Eval("InvoiceNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Do No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDoNo" runat="server" text='<%#Eval("DONo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Seal No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSealNo" runat="server" text='<%#Eval("SealNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Remark" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblRemark" runat="server" text='<%#Eval("Remark")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblLocation" runat="server" text='<%#Eval("Location")%>'></asp:Label>
 
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Scrap Pks" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblScrapPks" runat="server" text='<%#Eval("ScrapPks")%>'></asp:Label>
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
                           
<a href="OnWheelStuffing.aspx" id="btnclear" runat="server" class="btn btn-primary btn btn-sm outline ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
    <div class="col-sm-4"></div>
        <div class="col-lg-6 pull-right" style="margin-left:6px;display:none">
<div class="form-group ">
<a href="TrainSummary.aspx" target="_blank"><b style="color:blue">Click here to view Train Summary</b> </a>
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
                   
<a href="OnWheelStuffing.aspx" class="btn btn-info btn-block">OK</a>
                                
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
    
    var ddlLocation = document.getElementById('<%= ddlLocation.ClientID%>').value;

var blResult = Boolean;
blResult = true;
      
if (ddlLocation == 0) {
    document.getElementById('<%= ddlLocation.ClientID%>').style.borderColor = "red";
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
