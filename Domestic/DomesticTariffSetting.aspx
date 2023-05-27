<%@ Page Title="Domestic | Tariff Settings " Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticTariffSetting.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<style type="text/css">
.center1 {
text-align: center;
}
</style>
<head>
<title>Domestic | Bond Tariff Settings</title>
</head>
<div class="page-container">
<div class="pageheader">

<h3>

<i class="glyphicon glyphicon-transfer"></i>Domestic Tariff Settings 
</h3>

</div>

<div id="page-content">



<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="Button1" />
        <asp:PostBackTrigger ControlID="btnsave" />

    </Triggers>
<ContentTemplate>
<div class="page-container">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">

<div class="col-md-12 pull-left main-content">
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">Domestic Tariff Setting
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>

<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true">
<div class="row">
    <asp:Panel ID="Panel" runat="server" Enabled="false">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b>Tariff ID</b>
<asp:DropDownList ID="ddltraiff" AutoPostBack="true" OnSelectedIndexChanged="ddltraiff_SelectedIndexChanged" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem>
</asp:DropDownList>
</div>
</div>
        </asp:Panel>
    <div class="col-sm-1 col-xs-6">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="LinkButton1" ControlStyle-CssClass='btn btn-primary btn-sm'  runat="server"
OnClientClick="return OpenNOCList();"><i class=" fa fa-search" aria-hidden="true"></i></asp:LinkButton>  
</div>                                  
</div> 
       <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b>Tariff Desc</b>
<asp:TextBox runat="server" ReadOnly="true" ID="txtTariffDesc" CssClass="form-control"></asp:TextBox>
</div>
</div>                                             
</div>

<div class="row">
<div class="col-md-4 col-xs-12">
<b>Invoice Type</b>
<asp:DropDownList ID="ddlbond" AutoPostBack="true" OnSelectedIndexChanged="ddltraiff_SelectedIndexChanged" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="1">General</asp:ListItem>
<asp:ListItem Value="2">Other</asp:ListItem>
<asp:ListItem Value="3">Transport</asp:ListItem>
    <asp:ListItem Value="4">General 1</asp:ListItem>
    <asp:ListItem Value="5">Import</asp:ListItem>
</asp:DropDownList>
</div>
    <div class="col-md-3 col-xs-12">
<b>Criteria</b>
<asp:DropDownList ID="ddlCriteria" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
<asp:ListItem Value="Cargo">Cargo</asp:ListItem>
<asp:ListItem Value="Coil">Coil</asp:ListItem>
</asp:DropDownList>
</div>
    <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Loaded/Destuff</b>
<asp:DropDownList ID="ddlLorD"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">   
    <asp:ListItem Value="0">--Select--</asp:ListItem>  
    <asp:ListItem Value="L">Loaded</asp:ListItem>
    <asp:ListItem Value="D">Destuff</asp:ListItem>
</asp:DropDownList> 
</div>
</div>
</div>
<div class="row">
<div class="col-md-2  col-xs-12">                                      
<div class="form-group date text-label">
<b>Effective From</b>                                           
<asp:TextBox ID="txtfrom" ReadOnly="true"  placeholder="dd-MM-yyyy"    runat="server"   Class="   form-control text-label"></asp:TextBox>

</div>
</div>
 

<div class="col-md-2  col-xs-12" ">                                      
<div class="form-group date text-label">
<b >Effective UpTo</b>
<asp:TextBox ID="txtUpTo"  placeholder="dd-MM-yyyy" ReadOnly="true"  runat="server"   Class="  form-control text-label"></asp:TextBox>

</div>                        
</div>
                                                   

</div>
<div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Account Head</b>
<asp:DropDownList ID="ddlAccount"   Style="text-transform: uppercase;" runat="server"    class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem> 
</asp:DropDownList> 
</div>
</div>
<div class="col-sm-1 col-xs-12">
<div class="form-group text-label">
<b >Size</b>
<asp:DropDownList  ID="ddlsize" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">   
<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="20">20</asp:ListItem>
<asp:ListItem Value="40">40</asp:ListItem>
<asp:ListItem Value="45">45</asp:ListItem>
                                         
</asp:DropDownList>
</div>
</div>

<div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Charges Based On</b>
<asp:DropDownList  ID="ddlCharges" AutoPostBack="true" OnSelectedIndexChanged="ddlCharges_SelectedIndexChanged" Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 
<asp:ListItem Value="0">--Select--</asp:ListItem>
</asp:DropDownList>
</div>
</div>
    <div class="col-sm-3 col-xs-12" id="divLocation" style="display:none" runat="server">
<div class="form-group text-label">
<b >Location</b>
<asp:DropDownList  ID="ddlLocation" Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 

</asp:DropDownList>
</div>
</div>
        <div class="col-sm-2 col-xs-12" id="divSlab" runat="server" style="display:none">
<div class="form-group text-label">
<b >Slab ID</b>
<asp:DropDownList  ID="ddlSlab" Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddlSlab_SelectedIndexChanged" runat="server" class="form-control text-label"> 
</asp:DropDownList>
</div>
</div>  
    <div class="col-sm-1" style="display:none">
        <asp:Button ID="btnSlab" runat="server" OnClick="btnSlab_Click" />
    </div>
    
     <div class="col-sm-1" id="divSlabAdd" runat="server" style="display:none">
                                     <div class="form-group " style="padding-top:20px;" >
                                         <asp:LinkButton class="btn btn-warning btn-sm outline" ID="lnkAddSlab" OnClientClick="return OpenSlabEntry()" runat="server">Add Slabs</asp:LinkButton>
                                                                                 
                                     </div>
                                 </div>  
<div class="col-md-2 col-xs-12" id="divFixedAmount" runat="server" style="display:none">
<div class="form-group text-label">
<b  >Fixed Amount</b>
<asp:TextBox ID="txtfixed" Style="text-transform:uppercase" class="form-control text-label" placeholder="Fixed Amount"  
runat="server"   ></asp:TextBox>
</div>
</div>
                                          
</div>

<div class="row">
<div class="col-md-1 col-xs-12">
                                                        
<div class="form-group text-label" style="padding-top: 0px;">
<asp:CheckBox ID="chkIstax" runat="server"  Checked="true"  />
<asp:HiddenField ID="hdltax" runat="server" Value="0" />
<asp:Label ID="TaxLabel" runat="server" AssociatedControlID="chkIstax" CssClass="inline"></asp:Label>
<b>Is Tax?</b>
</div>
</div>
<div class="col-md-2 col-xs-12" style="width:150px">
<div class="form-group text-label" style="padding-top: 2px;">
<asp:CheckBox ID="ChkOnDelivered" runat="server"   />
<asp:HiddenField ID="hdlDeliver" runat="server" Value="0" />
<asp:Label ID="OnLabel" runat="server" AssociatedControlID="ChkOnDelivered" CssClass="inline"></asp:Label>
<b>On Delivered</b>
</div>
</div>
<div class="col-md-2 col-xs-12" style="width:150px">
                                                        
<div class="form-group text-label" style="padding-top: 0px;">
<asp:CheckBox ID="chkconsarea" runat="server"  />
<asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
<asp:Label ID="Label1" runat="server" AssociatedControlID="chkconsarea" CssClass="inline"></asp:Label>
<b>Consider Area?</b>
</div>
</div>
<div class="col-sm-1">
<div class="form-group" style="padding-top: 0px">
<asp:Button ID="Button1" class="btn btn-info " runat="server" OnClick="Button1_Click"  
Text="Add" OnClientClick="return Validationsearch()"  />
</div>


</div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div class="col-sm-5 text-label" runat="server" id="divSlabDets" style="display:none">
        <div class="table-responsive scrolling-table-container1">
<asp:GridView ID="grdSlabDets" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<Columns>
    <asp:BoundField DataField="slabID" HeaderText="Slab ID" />    
    <asp:BoundField DataField="slabon" HeaderText="Slab On" />
    <asp:BoundField DataField="fromslab" HeaderText="From" />
    <asp:BoundField DataField="toslab" HeaderText="To" />
    <asp:BoundField DataField="value" HeaderText="Value" />                                               
</Columns>
</asp:GridView>
</div>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>

<div class="row">
    <asp:Button ID="btnNOCList" runat="server" Text="Call Button Click" style="display:none" OnClick="btnNOCList_Click" />
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

<asp:TemplateField>
<ItemTemplate>                                                     
                                                            
<asp:LinkButton ID="lnkDelete"  ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Delete"                                                         
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AutoTemp")%>' runat="server"  OnClick="lnkDelete_Click" 
></asp:LinkButton>

   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="90px" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Tariff ID" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lbltariffID" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tariffID")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Invoice Type" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblInvoicetype" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "bondtype")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="From" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lbleffectivefrom" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "effectivefrom")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Up To" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lbleffectiveupto" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "effectiveupto")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Account Head" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblAccountHead" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AccountHead")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Size" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblsize" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "size")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Charges" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblchargesBased" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "chargesBased")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

    <asp:TemplateField HeaderText="Slab ID" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblSlabID" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SlabId")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

    <asp:TemplateField HeaderText="Location" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblLocation" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LocationName")%>'>'></asp:Label>
<asp:Label ID="lblLocationID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Location")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
<asp:TemplateField HeaderText="Fixed Amount" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblFixedAmount" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FixedAmount")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Is Tax" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblIsTax" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IsTax")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Delivered" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblOnDelivered" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OnDelivered")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
<asp:TemplateField HeaderText="Consider Area" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblconsarea" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ConsARea")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
                  <asp:TemplateField HeaderText="Criteria" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblCriteria" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Criteria")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>    
    <asp:TemplateField HeaderText="Loaded/Destuff" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblLorD" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LorD")%>'>'></asp:Label>
    <asp:Label ID="lblLorD1" Visible="false" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LorD1")%>'>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>                            
</Columns>

</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Panel>
</div>
</div>


<div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top: 15px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"
Text="Save" OnClientClick="return Validationseve()" />
</div>


</div>

<div class="col-sm-1" style="padding-left: 14px;">
<div class="form-group" style="padding-top: 15px">

<a href="DomesticTariffSetting.aspx" id="btnclear" runat="server" class="btn btn-primary ">Clear
</a>

</div>

        
</div>


</div>
<asp:Label ID="lblcode" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblagentname" Visible="false" runat="server" Text=""></asp:Label>

    <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<span><i runat="server" id="I1" class="fa fa-5x fa-check-circle-o text-success"></i></span>
<br />
<h4 class="modal-title">

<asp:Label ID="Label2" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label></h4>
</center>
</div>
<div class="modal-footer">

<button class="btn btn-info " id="btnYes" data-dismiss="modal" runat="server" onserverclick="btnYes_ServerClick" aria-hidden="true">
Yes 
</button>
    <button class="btn btn-info " id="btnNo" data-dismiss="modal" runat="server" onserverclick="btnNo_ServerClick" aria-hidden="true">
No 
</button>
</div>
</div>

</ContentTemplate>

</asp:UpdatePanel>
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

<a href="DomesticTariffSetting.aspx" class="btn btn-info btn-block">OK</a>

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

</ContentTemplate>
</asp:UpdatePanel>
</div>


</div>
    <script type="text/javascript">
        var popup;
        function OpenNOCList() {

            var url = "TariffSearch.aspx"

            popup = window.open(url, "Popup", "width=710,height=555");
            popup.focus();

        }
</script>
<script type="text/javascript">
    function Validationseve() {
        document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
        document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary disabled");
    }
</script>     
    
<script type="text/javascript">
function Validationsearch() {
var ddlAccount = document.getElementById('<%= ddlAccount.ClientID%>').value;
<%--var ddlsize = document.getElementById('<%= ddlsize.ClientID%>').value;--%>
var ddlCharges = document.getElementById('<%= ddlCharges.ClientID%>').value;
var txtfixed = document.getElementById('<%= txtfixed.ClientID%>').value;
    var ddltraiff = document.getElementById('<%= ddltraiff.ClientID%>').value;
    var ddlLorD = document.getElementById('<%= ddlLorD.ClientID%>').value;
    var ddlbond = document.getElementById('<%= ddlbond.ClientID%>').value;
    document.getElementById('<%= Button1.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= Button1.ClientID%>').setAttribute("class", "btn btn-info disabled");
var blResult = Boolean;
blResult = true;

if (ddlAccount == 0) {
document.getElementById('<%= ddlAccount.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

 
if (ddlCharges == 0) {
document.getElementById('<%= ddlCharges.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
    if ((ddlcharges!=11)||(ddlCharges!=12)){
if (txtfixed == "") {
document.getElementById('<%= txtfixed.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
    }
if (ddltraiff == 0) {
document.getElementById('<%= ddltraiff.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
    if (ddlbond == 0) {
        document.getElementById('<%= ddlbond.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    else {
        if (ddlbond == 5) {
            if (ddlLorD == 0) {
                document.getElementById('<%= ddlLorD.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
        }
    }
if (blResult == false) {
    alert('Please fill the required fields!');
    document.getElementById('<%= Button1.ClientID%>').value = "Add";
    document.getElementById('<%= Button1.ClientID%>').setAttribute("class", "btn btn-info");
}
return blResult;
}
</script>
    <script>
        function OpenSlabEntry() {
            var url = "SlabEntry.aspx"
            popup = window.open(url, "Popup", "top=100,left=400,width=700,height=550");
            popup.focus();
        }
    </script>
</asp:Content>
