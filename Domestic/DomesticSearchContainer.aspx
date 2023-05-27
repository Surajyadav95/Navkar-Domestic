<%@ Page Title="Domestic |Domestic Search Container" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticSearchContainer.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic |Domestic Search Container</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>Domestic Search Container
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
Domestic Search Container
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtcontainerNo"   Style="text-transform: uppercase;" OnTextChanged="txtcontainerNo_TextChanged" AutoPostBack="true"   runat="server" class="form-control text-label" placeholder="Container No">
                                            
</asp:TextBox> 
</div>
</div>
<div class="col-sm-1 col-xs-6">
                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return OpenItembond();"> 
                                 
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>

<asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
                              
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Jo No</b>
<asp:TextBox ID="txtJoNo"   Style="text-transform: uppercase;" ReadOnly="true"  runat="server" class="form-control text-label" >
                                            
</asp:TextBox> 
</div>
</div>
     <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Size</b>
<asp:TextBox ID="txtSize" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"  placeholder="Size"
runat="server"   ></asp:TextBox>
</div>
                       </div>

         <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Type</b>
<asp:TextBox ID="txttype" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="type"
runat="server"   ></asp:TextBox>
</div>
                       </div>

</div>

    <div class="row">
         
    </div>
<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Arrival Date</b>
<asp:TextBox ID="txtArrivalDate" placeholder="yyyy-mm-dd " runat="server" ReadOnly="true" class="form-control text-label"
></asp:TextBox>
</div>
</div>
                                 
                               
<div class="col-md-3  col-xs-12">                                      
<div class="form-group text-label">
<b>Jo Date</b>                                         
<asp:TextBox ID="txtJoDate"  placeholder="yyyy-mm-dd "   runat="server" ReadOnly="true"   Class="form-control text-label"></asp:TextBox>

</div>
</div>
                                  
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >BE No</b>
<asp:TextBox ID="txtbeno"   Style="text-transform: uppercase;"  runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Be No">                                           
</asp:TextBox> 
</div>
</div>
<div class="col-md-3  col-xs-12">                                      
<div class="form-group text-label">
<b>BE Date</b>                                         
<asp:TextBox ID="txtbedate"  placeholder="yyyy-mm-dd "   runat="server" ReadOnly="true"   Class="form-control text-label"></asp:TextBox>

</div>
</div>
</div>

    <div class="row">

    </div>
<div class="row">
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b>Importer Name</b>
<asp:textbox ID="txtImporter" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Importer Name " >
</asp:textbox>   
</div>
</div>
                                      
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b> CHA Name</b>
<asp:textbox ID="txtCha" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Cha Name" >
</asp:textbox>   
</div>
</div>
</div>

    <div class="row">
        <div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b> Customer Name</b>
<asp:textbox ID="txtCustomer" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Customer Name" >
</asp:textbox>   
</div>
</div>

         <div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b> Line Name</b>
<asp:textbox ID="txtLineName" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Line Name" >
</asp:textbox>   
</div>
</div>
    </div>
<div class="row">
    <div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b>Transporter</b>
<asp:textbox ID="txttransporter" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Transporter" >
</asp:textbox>   
</div>

</div>
<div class="col-sm-6  col-xs-12" >
<div class="form-group text-label">
<b>Cargo Description</b>
<asp:textbox ID="txtCargo" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Cargo Descrption" >
</asp:textbox>   
</div>
</div>
</div>

</asp:Panel>
                                             
<div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="panel panel-default" style=" padding: 10px; margin: 10px;">
<div id="Tabs" role="tabpanel">
<!-- Nav tabs -->
<ul class="nav nav-tabs" role="tablist" >
<li class="active"><a href="#LoadedIn" aria-controls="Loaded In" role="tab" data-toggle="tab">
Loaded In</a></li>
<li><a href="#EmptyIn" aria-controls="Empty In" role="tab" data-toggle="tab">Empty In</a></li>

<li><a href="#EmprtOut" aria-controls="Emprt Out" role="tab" data-toggle="tab">Emprt Out</a></li>

<li><a href="#GatePass" aria-controls="Gate Pass" role="tab" data-toggle="tab">Gate Pass</a></li>

<li><a href="#TruckOut" aria-controls="Truck Out" role="tab" data-toggle="tab">Truck Out</a></li>

    <li><a href="#Destuff" aria-controls="Destuff" role="tab" data-toggle="tab">Destuff</a></li>

    <li><a href="#LoadedSheet" aria-controls="Loaded Sheet" role="tab" data-toggle="tab">Loaded Sheet</a></li>

</ul>
<!-- Tab panes -->
<div class="tab-content" style="padding-top: 20px">
<div role="tabpanel" class="tab-pane active" id="LoadedIn">
<div class="row">
<asp:UpdatePanel ID="up_grid1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdLoadedIn" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                  

</Columns>
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="EmptyIn">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdEmptyIn" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>
 
</Columns>

</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="EmprtOut">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdEmprtOut" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                

</Columns>
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>

<div role="tabpanel" class="tab-pane" id="GatePass">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdGatePass" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                 

</Columns>
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>

<div role="tabpanel" class="tab-pane" id="TruckOut">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdTruckOut" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                
   
</Columns>
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>

 <div role="tabpanel" class="tab-pane" id="Destuff">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdDestuff" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                
   
</Columns>
</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>

    <div role="tabpanel" class="tab-pane" id="LoadedSheet">
<div class="row">
<asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
<ContentTemplate>
                              
<div class="row">
<div class="col-lg-12 col-xs-12 text-label "  style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-left:22px;margin-right:0px;">
<asp:GridView ID="grdLoadedSheet" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
<Columns>                                                
   
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
</div>

<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Seal No</b>
<asp:TextBox ID="txtSealNo"   Style="text-transform: uppercase;"  runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Seal No">                                           
</asp:TextBox> 
</div>
</div>

<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Driver Code</b>
<asp:TextBox ID="txtDriverCode"   Style="text-transform: uppercase;"  runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Driver COde">                                           
</asp:TextBox> 
</div>
</div>

<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Trailer No</b>
<asp:TextBox ID="txttrailerNo"   Style="text-transform: uppercase;"  runat="server" ReadOnly="true"   class="form-control text-label" placeholder="Trailer No">                                           
</asp:TextBox> 
</div>
</div>


</div>
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
                   
<a href="#" class="btn btn-info btn-block">OK</a>
                                
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
var popup;
//alert('hi')
function OpenItembond() {
//alert('hi')
    var url = "ContainerSearch.aspx"
//window.open(url);
popup = window.open(url, "Popup", "width=800,height=550");
popup.focus();
}
</script>
</asp:Content>
