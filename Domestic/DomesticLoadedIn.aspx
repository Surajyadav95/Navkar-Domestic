<%@ Page Title="Domestic |Domestic Loaded In" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticLoadedIn.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic |Domestic Loaded In</title>
       
</head>
       <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:250px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i>Domestic Loaded In
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
<div class="col-md-3 pull-md-left sidebar" style="padding-top:12px;">
 
                           
</div>
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
 Domestic Loaded In
                
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
      <div class="col-sm-3 col-xs-12">                                      
                    <div class="form-group text-label">
                    <b >Gate In Date & Time</b>
                    <asp:TextBox ID="txtGateInDate" placeholder="dd-MM-yyyy" TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>
     <div class="col-md-2 col-xs-12" >
<div class="form-group text-label">
<b>Category</b>
<asp:DropDownList ID="ddlCategory"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
    <asp:ListItem Value="0">--Select--</asp:ListItem>
    <asp:ListItem Value="I">Import</asp:ListItem>
    <asp:ListItem Value="E">Export</asp:ListItem>
</asp:DropDownList>
</div>
</div>
     
</div>

    <div class="row">
              
     <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtcontainer" Style="text-transform:uppercase" MaxLength="11" AutoPostBack="true" OnTextChanged="txtcontainer_TextChanged" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>
        <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
        


<div class="col-md-1 col-xs-12" >
<div class="form-group text-label">
<b>Size</b>
<asp:DropDownList ID="ddlsize"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>

<div class="col-md-2 col-xs-12" >
<div class="form-group text-label">
<b>Type</b>
<asp:DropDownList ID="ddltype"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>

<div class="col-md-2 col-xs-12" >
<div class="form-group text-label">
<b>Cargo Type</b>
<asp:DropDownList ID="ddlCargoType"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
        <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b  >From Location</b>
<asp:DropDownList ID="ddlFromLocation"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
        <asp:Panel ID="panel3" runat="server" Enabled="false">
        <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b  >To Location</b>
<asp:DropDownList ID="ddlToLocation"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
</asp:Panel>

</div>
    <div class="row">   
        <div class="col-sm-1 col-xs-12">
<div class="form-group text-label">
<b  >Jo No</b>
<asp:TextBox ID="txtjono" Style="text-transform:uppercase" ReadOnly="true" OnTextChanged="txtjono_TextChanged" AutoPostBack="true"   class="form-control text-label"  placeholder="Jo No"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-sm-1 col-xs-6" style="display:none">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return gstsearch();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>
        <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b  >Jo Type</b>
<asp:TextBox ID="txtJOType" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="JO Type"
runat="server"></asp:TextBox>
</div>
</div>        
           <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Manifiest Pkgs</b>
<asp:TextBox ID="txtpkgs" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Pkgs"
runat="server"   ></asp:TextBox>
</div>
                       </div>

         <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Manifiest Weight</b>
<asp:TextBox ID="txtweight" Style="text-transform:uppercase"  ReadOnly="true"  class="form-control text-label"  placeholder="Weight"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        <div class="col-md-2 col-xs-12" style="display:none" >
            <div class="form-group text-label">

<b>Balance Pkgs</b>
<asp:TextBox ID="txtBalPkgs" Style="text-transform:uppercase"  ReadOnly="true"   class="form-control text-label"  placeholder="Balance Pkgs"
runat="server"   ></asp:TextBox>
</div>
                       </div>

         <div class="col-md-2 col-xs-12" style="display:none" >
            <div class="form-group text-label">

<b>Balance Weight</b>
<asp:TextBox ID="txtBalWeight" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"  placeholder="Balance Weight"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        </div>
    <div class="row">
         <div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b  >Shipping Line</b>
<asp:DropDownList ID="ddllinename"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
         <div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b  >Customer</b>
<asp:DropDownList ID="ddlCustomer"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
        </div>
    <div class="row">
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >DO No</b>
<asp:TextBox ID="txtDONo" Style="text-transform:uppercase" class="form-control text-label" placeholder="DO No"
runat="server"></asp:TextBox>
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Invoice No</b>
<asp:TextBox ID="txtInvoiceNo" Style="text-transform:uppercase" class="form-control text-label" placeholder="Invoice No"
runat="server"></asp:TextBox>
</div>
</div>
        <div class="col-sm-3 col-xs-12">                                      
                    <div class="form-group text-label">
                    <b >Invoice Date</b>
                    <asp:TextBox ID="txtInvoiceDate" placeholder="dd-MM-yyyy" TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>
    </div>
<div class="row">
<div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b  >Commodity</b>
<asp:DropDownList ID="ddlCommodity"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
<div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b>Remarks</b>
<asp:TextBox ID="txtremarks" Style="text-transform:uppercase" TextMode="MultiLine"  class="form-control text-label"  placeholder="Remarks"
runat="server"></asp:TextBox>
</div>
</div>
</div>
   <div class="row">              
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
     <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b  >Trailer No</b>
<asp:TextBox ID="txttrailerno" Style="text-transform:uppercase" OnTextChanged="txttrailerno_TextChanged"   AutoPostBack="true"   class="form-control text-label"  placeholder="Trailer No"
runat="server"   ></asp:TextBox>
</div>
</div>
    
   <asp:Panel ID="Panel1" runat="server" Enabled="false"> 

           <div class="col-md-2 col-xs-12 " id="divtrai" runat="server"  >
            <div class="form-group text-label" style="margin-top:20px;" >


<asp:TextBox ID="txttrailerNo1" Style="text-transform:uppercase"  class="form-control text-label"  placeholder=""
runat="server"   ></asp:TextBox>
</div>
                       </div>
       </asp:Panel>

         <div class="col-md-2 col-xs-12" id="divtrailer" runat="server" style="display:none" >
            <div class="form-group text-label">

<b>Trailer No</b>
<asp:TextBox ID="txttrailer" Style="text-transform:uppercase"  class="form-control text-label"  placeholder=""
runat="server"   ></asp:TextBox>
</div>
                       </div>
    <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">
<b>Loaded In Pkgs</b>
<asp:TextBox ID="txtLoadedInPkgs" Style="text-transform:uppercase"  class="form-control text-label"  placeholder=""
runat="server"   ></asp:TextBox>
</div>
    </div>
    <div class="col-md-2 col-xs-12">
            <div class="form-group text-label">

<b>Loaded In Weight</b>
<asp:TextBox ID="txtLoadedInWeight" Style="text-transform:uppercase"  class="form-control text-label"  placeholder=""
runat="server"   ></asp:TextBox>
</div>
                       </div>
    <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b  >Seal No</b>
<asp:TextBox ID="txtseal" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Seal No"
runat="server"   ></asp:TextBox>
</div>
</div>

          <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Condition</b>
<asp:DropDownList ID="ddlcondtion"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
          <div class="col-md-4 col-xs-12" >
            <div class="form-group text-label">

<b>Transporter</b>
<asp:DropDownList ID="ddltransporter"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
        
          <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Driver Name</b>
<asp:TextBox ID="txtdrivercode" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Driver Name"
runat="server"   ></asp:TextBox>
</div>
                       </div>
      
    </ContentTemplate>
            </asp:UpdatePanel>
       <div class="col-sm-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnAdd" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnAdd_Click"
Text="Add"  OnClientClick="return ValidationAdd()"  />
</div>
                                              
                                      
</div>
       
        </div>

    <div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive  ">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!"  ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

        <asp:TemplateField HeaderText="Container No" >
                             <ItemTemplate>
                                 <asp:Label id="lblContainerno" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

      <asp:TemplateField HeaderText="Size" >
                             <ItemTemplate>
                                 <asp:Label id="lblSize" runat="server" Text='<%# Eval("Size")%>'></asp:Label>
                                 <asp:Label id="lblSizeID" Visible="false" runat="server" Text='<%# Eval("SizeID")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

    <asp:TemplateField HeaderText="Type" >
                             <ItemTemplate>
                                 <asp:Label id="lblType" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                                 <asp:Label id="lblTypeID" Visible="false" runat="server" Text='<%# Eval("TypeID")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

        <asp:TemplateField HeaderText="Cargo Type" >
                             <ItemTemplate>
                                 <asp:Label id="lblCargoType" runat="server" Text='<%# Eval("CargoType")%>'></asp:Label>
                                 <asp:Label id="lblCargoTypeID" Visible="false" runat="server" Text='<%# Eval("CargoTypeID")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

      <asp:TemplateField HeaderText="From Location" >
                             <ItemTemplate>
                                 <asp:Label id="lblFromLocation" runat="server" Text='<%# Eval("FromLocation")%>'></asp:Label>
                                 <asp:Label id="lblFromLocationID" Visible="false" runat="server" Text='<%# Eval("FromLocationID")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

     <asp:TemplateField HeaderText="To Location" >
                             <ItemTemplate>
                                 <asp:Label id="lblToLocation" runat="server" Text='<%# Eval("ToLocation")%>'></asp:Label>
                                 <asp:Label id="lblToLocationID" Visible="false" runat="server" Text='<%# Eval("ToLocationID")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

         <asp:TemplateField HeaderText="Trailer No" >
                             <ItemTemplate>
                                 <asp:Label id="lblTrailerNo" runat="server" Text='<%# Eval("TrailerNo")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

      <asp:TemplateField HeaderText="Loaded In Pkgs" >
                             <ItemTemplate>
                                 <asp:Label id="lblLoadedInPkgs" runat="server" Text='<%# Eval("LoadedInPkgs")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

     <asp:TemplateField HeaderText="Loaded In Weight" >
                             <ItemTemplate>
                                 <asp:Label id="lblLoadedInWeight" runat="server" Text='<%# Eval("LoadedInWeight")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

      <asp:TemplateField HeaderText="Seal No" >
                             <ItemTemplate>
                                 <asp:Label id="lblSealNo" runat="server" Text='<%# Eval("SealNo")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

     <asp:TemplateField HeaderText="Condition" >
                             <ItemTemplate>
                                 <asp:Label id="lblCondition" runat="server" Text='<%# Eval("Condition")%>'></asp:Label>
                                 <asp:Label id="lblConditionID" Visible="false" runat="server" Text='<%# Eval("ConditionID")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
      <asp:TemplateField HeaderText="Transporter" >
                             <ItemTemplate>
                                 <asp:Label id="lblTransporter" runat="server" Text='<%# Eval("Transporter")%>'></asp:Label>
                                 <asp:Label id="lblTransporterID" Visible="false" runat="server" Text='<%# Eval("TransporterID")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
     <asp:TemplateField HeaderText="Driver Name" >
                             <ItemTemplate>
                                 <asp:Label id="lblDriverName" runat="server" Text='<%# Eval("DriverName")%>'></asp:Label>                                 
                             </ItemTemplate>
                            
                        </asp:TemplateField>


</Columns>

</asp:GridView>
</div>
</div>


</div>


     
 
      </div>


<asp:Label ID="LblVendorID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblVendorName" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblDriverNo" Visible="false" runat="server" Text=""></asp:Label>

    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:8px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnSave_Click" 
Text="Save" OnClientClick="return ValidationSave()"   />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:0px;">
<div class="form-group" style="padding-top:8px">
                           
<a href="DomesticLoadedIn.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a> 
                              
</div>                                            
                                      
</div>
 </div>
     
    </asp:Panel>                        
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
                   <button class="btn btn-info btn-block" id="SaveOk" data-dismiss="modal" runat="server" onserverclick="SaveOk_ServerClick">OK</button> 

                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
          <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblPrintQue"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btnprint" data-dismiss="modal" runat="server" onclick="OpenSlipPrint()"  aria-hidden="true">
Yes 
</button>
<a href="DomesticLoadedIn.aspx" class="btn btn-danger ">No</a>
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
       

    <script type="text/javascript">
        var popup;
        function OpenSlipPrint() {

            var txtSlipNoPrint = document.getElementById('<%= txtSlipNoPrint.ClientID%>').value;

            var url = "../Report_Domestic/DomesticLoadedInPrint.aspx?GateInNo=" + txtSlipNoPrint

           window.open(url);

       }

</script>
   <script type="text/javascript">
function ValidationSave() {
            
     
    var txtcontainer = document.getElementById('<%= txtcontainer.ClientID%>').value;
    var ddlcondtion = document.getElementById('<%= ddlcondtion.ClientID%>').value;
    var ddltransporter = document.getElementById('<%= ddltransporter.ClientID%>').value;
    var ddlFromLocation = document.getElementById('<%= ddlFromLocation.ClientID%>').value;
    var ddlToLocation = document.getElementById('<%= ddlToLocation.ClientID%>').value;
    var ddlCategory = document.getElementById('<%= ddlCategory.ClientID%>').value;


var blResult = Boolean;
blResult = true;
              
 

<%--if (txtcontainer == "") {
document.getElementById('<%= txtcontainer.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
    if (ddlFromLocation == 0) {
        document.getElementById('<%= ddlFromLocation.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (ddlToLocation == 0) {
        document.getElementById('<%= ddlToLocation.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }--%>
    
   <%-- if (ddlcondtion == 0) {
        document.getElementById('<%= ddlcondtion.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }


    if (ddltransporter == 0) {
        document.getElementById('<%= ddltransporter.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }--%>
    if (ddlCategory == 0) {
        document.getElementById('<%= ddlCategory.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>
    <script>
        function ValidationAdd() {
            var txttrailer = document.getElementById('<%= txttrailer.ClientID%>').value;
            var txttrailerNo1 = document.getElementById('<%= txttrailerNo1.ClientID%>').value;
            var txtLoadedInPkgs = document.getElementById('<%= txtLoadedInPkgs.ClientID%>').value;
            var txtLoadedInWeight = document.getElementById('<%= txtLoadedInWeight.ClientID%>').value;
            var blResult = Boolean;
            blResult = true;

            if ((txttrailer == "") && (txttrailerNo1 == "")) {
                document.getElementById('<%= txttrailerno.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtLoadedInPkgs == "") {
                document.getElementById('<%= txtLoadedInPkgs.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtLoadedInWeight == "") {
                document.getElementById('<%= txtLoadedInWeight.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
        }
    </script>
    <script type="text/javascript">
        var popup;
        function gstsearch() {

            var url = "LoadedSearch.aspx"

            popup = window.open(url, "Popup", "width=710,height=555");
            popup.focus();

        }
</script>
   
</asp:Content>
