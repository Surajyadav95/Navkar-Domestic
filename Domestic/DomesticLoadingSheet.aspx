<%@ Page Title="Domestic | Loading Sheet Entry" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticLoadingSheet.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic |Restuff Entry</title>
       
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
<i class="glyphicon glyphicon-transfer"></i>Loading Sheet Entry
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
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true">
 <div class="row">
         <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Loading Sheet ID</b>
<asp:TextBox ID="txtLoadingSheetID" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>

              <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >Loading Sheet Date </b>
                    <asp:TextBox ID="txtLoadingSheetDate" placeholder="dd-MM-yyyy" ReadOnly="true" TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>

     <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Loading Type</b>
<asp:DropDownList ID="ddlLoadingType"   Style="text-transform: uppercase;" OnSelectedIndexChanged="ddlLoadingType_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
        
      
    </div>
   <div class="row">
          <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Container No</b>
<asp:TextBox ID="txtcontainerNo" ReadOnly="true" Style="text-transform:uppercase" MaxLength="11" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
                       </div>

<asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click1" />
        
<div class="col-sm-1 col-xs-6">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return gstsearch();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>
        
          <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Jo No</b>
<asp:TextBox ID="txtjono" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"  placeholder="Jo No"
runat="server"   ></asp:TextBox>
</div>
                       </div>
       <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >Arrival Date </b>
                    <asp:TextBox ID="txtArrivalDate" placeholder="dd-MM-yyyy" ReadOnly="true"   TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>
       </div>
    <div class="row">
         <div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b  >Customer Name</b>
<asp:TextBox ID="txtcustomer" Style="text-transform:uppercase"   ReadOnly="true" class="form-control text-label"  placeholder="Customer Name"
runat="server"   ></asp:TextBox>
</div>
</div>
<div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Pkgs</b>
<asp:TextBox ID="txtpkgs" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Pkgs"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Weight</b>
<asp:TextBox ID="txtWeight" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Weight"
runat="server"   ></asp:TextBox>
</div>
                       </div>

        <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Balance Pkgs</b>
<asp:TextBox ID="txtBalancePkgs" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Balance Pkgs"
runat="server"   ></asp:TextBox>
</div>
                       </div>
            <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Balance Weight</b>
<asp:TextBox ID="txtbalanceWeight" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Balance Weight"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        </div>
        <div class="row">
               <div class="col-md-3 col-xs-12" style="display: none;" id="divStuffedl" runat="server" >
            <div class="form-group text-label">
                <b><asp:Label ID="lblcriteria" Visible="false" runat="server" Text=""></asp:Label></b>
 <b>Stuffed Truck No</b>
<asp:TextBox ID="txtTruckNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Stuffed Truck No"
runat="server"   ></asp:TextBox>
</div>
                       </div>

             <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
<ContentTemplate>
              <div class="col-md-3 col-xs-12 pull-left" style="display: none;" id="divStuffed" runat="server" >
            <div class="form-group text-label">

<b>Stuffed Container No</b>
<asp:TextBox ID="txtStuffedContainerNo" Style="text-transform:uppercase" OnTextChanged="txtStuffedContainerNo_TextChanged" AutoPostBack="true"  class="form-control text-label"  placeholder="Stuffed Container No"
runat="server"   ></asp:TextBox>
</div>
                       </div>
    <div class="col-md-1 col-xs-12 pull-left" style="display: none;" id="divSize" runat="server" >
            <div class="form-group text-label">

<b>Size</b>
<asp:TextBox ID="txtSize" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Size"
runat="server"   ></asp:TextBox>
</div>
                       </div>
    </ContentTemplate>
                 </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
 
            <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Stuffed Qty</b>
<asp:TextBox ID="txtStuffedQty" Style="text-transform:uppercase" OnTextChanged="txtStuffedQty_TextChanged" AutoPostBack="true"  class="form-control text-label"  placeholder="Stuffing Qty"
runat="server"   ></asp:TextBox>
</div>
</div>
    </ContentTemplate>
                </asp:UpdatePanel>

              <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
<ContentTemplate>
            <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Stuffed Wt</b>
<asp:TextBox ID="txtStuffedWt" Style="text-transform:uppercase" OnTextChanged="txtStuffedWt_TextChanged" AutoPostBack="true" class="form-control text-label"  placeholder="Stuffed Wt"
runat="server"   ></asp:TextBox>
</div>
                       </div>
    </ContentTemplate>
                  </asp:UpdatePanel>



              <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Seal No</b>
<asp:TextBox ID="txtSealNo" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Seal No"
runat="server"   ></asp:TextBox>
</div>
                       </div>

             <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Release Area</b>
<asp:TextBox ID="txtArea" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Area"
runat="server"   ></asp:TextBox>
</div>
                       </div>
            </div>
    <div class="row">
         <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Receiver Name</b>
<asp:TextBox ID="txtReceiverName" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Receiver Name"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Location</b>
<asp:DropDownList ID="ddlLocation"   Style="text-transform: uppercase;"  runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>

         <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>No.Of Labour</b>
<asp:TextBox ID="txtNoOflabour" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="No.Of Labour"
runat="server"   ></asp:TextBox>
</div>
                       </div>
          <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Equipment Type</b>
<asp:DropDownList ID="ddlEquipment"   Style="text-transform: uppercase;"  runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>

    </div>
    <div class="row">
        <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Vendor Name</b>
<asp:DropDownList ID="ddlvendorName"   Style="text-transform: uppercase;"  runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
<ContentTemplate>
     

             <div class="col-md-1 col-xs-12" style="display:none" >
            <div class="form-group text-label">

<b>CBM</b>
<asp:TextBox ID="txtcbm" Style="text-transform:uppercase" OnTextChanged="txtcbm_TextChanged" AutoPostBack="true"  class="form-control text-label"  placeholder="CBM"
runat="server"   ></asp:TextBox>
</div>
                       </div>

             <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Area</b>
<asp:TextBox ID="txtareacbm" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Area"
runat="server"   ></asp:TextBox>
</div>
                       </div>
    </ContentTemplate>
                 </asp:UpdatePanel>

          <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Vehicle Type</b>
<asp:DropDownList ID="ddlVehicletype"   Style="text-transform: uppercase;"  runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
             <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b  >Trailer No</b>
<asp:TextBox ID="txttrailerno" Style="text-transform:uppercase" OnTextChanged="txttrailerno_TextChanged"   AutoPostBack="true"   class="form-control text-label"  placeholder="Trailer No"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label" style="padding-top:20px">


<asp:TextBox ID="txttrailer" Style="text-transform:uppercase" ReadOnly="true"  class="form-control text-label"  placeholder=""
runat="server"   ></asp:TextBox>
</div>
                       </div>
    </div>
            <div class="row">
              <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Cargo Desc</b>
<asp:TextBox ID="txtCargoDesc" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Cargo Desc"
runat="server"   ></asp:TextBox>
</div>
                       </div>
            <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Reference Jo No</b>
<asp:TextBox ID="txtReferenceNo" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Reference Jo No"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        
    <div class="col-sm-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnAdd" class="btn btn-primary btn-sm outline " runat="server" 
Text="Add"  OnClientClick="return ValidationAdd()" OnClick="btnAdd_Click"  />
</div>                                                                                  
</div>
 </div>

  
     </asp:Panel>
 
      </div>



<asp:Label ID="lblJoNo" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblDEntryID" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblSEntryID" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblCHAID" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblSLID" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblCUSTOMERID" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblremarks" Visible="false" runat="server" Text=""></asp:Label>

    
    <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
 
 

<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive  ">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!"  ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

  <asp:TemplateField>
<ItemTemplate>
                                                               
                                                            
<asp:LinkButton ID="lnkDelete"  ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Delete" OnClick="lnkDelete_Click"                                                  
    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ContainerNo")%>' runat="server"  
    ></asp:LinkButton>

   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="100px" />
</asp:TemplateField>
         <asp:TemplateField HeaderText="Container No" >
                             <ItemTemplate>
                                 <asp:Label id="lblcontainerno" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
     <asp:TemplateField HeaderText="Size" >
                             <ItemTemplate>
                                 <asp:Label id="lblSize" runat="server" Text='<%# Eval("Size")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
      <asp:TemplateField HeaderText="Truck No" >
                             <ItemTemplate>
                                 <asp:Label id="lbltruckNo" runat="server" Text='<%# Eval("TruckNo")%>'></asp:Label>
                                 
                             </ItemTemplate>
                            
                        </asp:TemplateField>

     <asp:TemplateField HeaderText="Stuffed Qty" >
                             <ItemTemplate>
                                 <asp:Label id="lblStuffQty" runat="server" Text='<%# Eval("StuffQty")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

      <asp:TemplateField HeaderText="Stuffed Wt" >
                             <ItemTemplate>
                                 <asp:Label id="lblStuffWt" runat="server" Text='<%# Eval("StuffWt")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

    
     <asp:TemplateField HeaderText="Cargo Description" >
                             <ItemTemplate>
                                 <asp:Label id="lblCargoDesc" runat="server" Text='<%# Eval("Cargo_Desc")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

     <asp:TemplateField HeaderText="Seal No" >
                             <ItemTemplate>
                                 <asp:Label id="lblSealNo" runat="server" Text='<%# Eval("Seal_No")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>

     <asp:TemplateField HeaderText="Release Area" >
                             <ItemTemplate>
                                 <asp:Label id="lblArea" runat="server" Text='<%# Eval("Area")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>

      <asp:TemplateField HeaderText="Receiver  Name" >
                             <ItemTemplate>
                                 <asp:Label id="lblReceiver" runat="server" Text='<%# Eval("Receiver")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>
       <asp:TemplateField HeaderText="Location" >
                             <ItemTemplate>
                                 <asp:Label id="lblLocation" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                                 <asp:Label id="lblLocationID" Visible="false" runat="server" Text='<%# Eval("LocationID")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>

       <asp:TemplateField HeaderText="No.Of Labour" >
                             <ItemTemplate>
                                 <asp:Label id="lblNoOfLabour" runat="server" Text='<%# Eval("NoOfLabour")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>

    <asp:TemplateField HeaderText="Equipment Type" >
                             <ItemTemplate>
                                 <asp:Label id="lblEquipment" runat="server" Text='<%# Eval("Equipment")%>'></asp:Label>
                                 <asp:Label id="lblEquipmentID" Visible="false" runat="server" Text='<%# Eval("EquipmentID")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>

        <asp:TemplateField HeaderText="Vendor" >
                             <ItemTemplate>
                                 <asp:Label id="lblVendor" runat="server" Text='<%# Eval("Vendor")%>'></asp:Label>
                                 <asp:Label id="lblVendorID" Visible="false" runat="server" Text='<%# Eval("VendorID")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>

      <asp:TemplateField HeaderText="Area" >
                             <ItemTemplate>
                                 <asp:Label id="lblAreacbm" runat="server" Text='<%# Eval("Areacbm")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>

      <asp:TemplateField HeaderText="Vehicle Type" >
                             <ItemTemplate>
                                 <asp:Label id="lblVehicle" runat="server" Text='<%# Eval("Vehicle")%>'></asp:Label>
                                  <asp:Label id="lblVehicleID" Visible="false" runat="server" Text='<%# Eval("VehicleID")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>

          <asp:TemplateField HeaderText="Reference JO No" >
                             <ItemTemplate>
                                 <asp:Label id="lblReferenceJONo" runat="server" Text='<%# Eval("Reference_No")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>
     <asp:TemplateField HeaderText="Trailer No" >
                             <ItemTemplate>
                                 <asp:Label id="lblTrailerNo" runat="server" Text='<%# Eval("LoadingTrailer")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>

</Columns>

</asp:GridView>
</div>
</div>


</div>
       </ContentTemplate>
</asp:UpdatePanel>

    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:8px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server" 
Text="Save" OnClientClick="return ValidationSav()" OnClick="btnSave_Click"  />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:0px;">
<div class="form-group" style="padding-top:8px">
                           
<a href="DomesticLoadingSheet.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a> 
                              
</div>                                            
                                      
</div>

        <div class="col-sm-5 pull-right" style="padding-top:15px;">
<div class="form-group">
<a href="EmptyInSummary.aspx" target="_blank"><b style="color:blue"></b> </a>
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
    <button class="btn btn-info btn-block" id="SaveOk" data-dismiss="modal" runat="server" onserverclick="SaveOk_ServerClick">OK</button>               
<%--<a href="TruckSlipGenerate.aspx" class="btn btn-info btn-block">OK</a>--%>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
     <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel7" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
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
<a href="DomesticLoadingSheet.aspx" class="btn btn-danger ">No</a>
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

             var txtLoadingSheetID = document.getElementById('<%= txtLoadingSheetID.ClientID%>').value;

             var url = "../Report_Domestic/LoadingSheetPrint.aspx?SheetNo=" + txtLoadingSheetID

           window.open(url);

       }

</script>
    <script type="text/javascript">
        function ValidationAdd() {

            var ddlLoadingType = document.getElementById('<%= ddlLoadingType.ClientID%>').value;
            var txtTruckNo = document.getElementById('<%= txtTruckNo.ClientID%>').value;
            var txtStuffedContainerNo = document.getElementById('<%= txtStuffedContainerNo.ClientID%>').value;
            var txtStuffedQty = document.getElementById('<%= txtStuffedQty.ClientID%>').value;
            var txtStuffedWt = document.getElementById('<%= txtStuffedWt.ClientID%>').value;
            var txtcontainerNo = document.getElementById('<%= txtcontainerNo.ClientID%>').value;


            var txtArea = document.getElementById('<%= txtArea.ClientID%>').value;
            var blResult = Boolean;
            blResult = true;
            if (ddlLoadingType == 0) {
                document.getElementById('<%= ddlLoadingType.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtcontainerNo == "") {
                document.getElementById('<%= txtcontainerNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlLoadingType == 1) {
                if (txtStuffedContainerNo == "") {
                    document.getElementById('<%= txtStuffedContainerNo.ClientID%>').style.borderColor = "red";
                    blResult = blResult && false;
                }
            }
            if (ddlLoadingType == 2) {
                if (txtTruckNo == "") {
                    document.getElementById('<%= txtTruckNo.ClientID%>').style.borderColor = "red";
                    blResult = blResult && false;
                }
            }
            if (txtStuffedQty == "") {
                document.getElementById('<%= txtStuffedQty.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtStuffedWt == "") {
                document.getElementById('<%= txtStuffedWt.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }

            if (txtArea == "") {
                document.getElementById('<%= txtArea.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
        }
    </script>
     
        <script type="text/javascript">
            function ValidationShow() {
                
                var ddlLoadingType = document.getElementById('<%= ddlLoadingType.ClientID%>').value;
                
                var txtcontainerNo = document.getElementById('<%= txtcontainerNo.ClientID%>').value;
                
           

            var blResult = Boolean;
            blResult = true;


        
            if (ddlLoadingType == 0) {
                document.getElementById('<%= ddlLoadingType.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
       
    }

                if (txtcontainerNo == "") {
        document.getElementById('<%= txtcontainerNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }


                

            //alert('hi')
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
        }
</script>
    <script type="text/javascript">
        function ValidationSav() {
            var ddlLoadingType = document.getElementById('<%= ddlLoadingType.ClientID%>').value;
            var txtcontainerNo = document.getElementById('<%= txtcontainerNo.ClientID%>').value;

            var blResult = Boolean;
            blResult = true;
            if (ddlLoadingType == 0) {
                document.getElementById('<%= ddlLoadingType.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtcontainerNo == "") {
                document.getElementById('<%= txtcontainerNo.ClientID%>').style.borderColor = "red";
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

               var url = "ContainerListforLoading.aspx"

               popup = window.open(url, "Popup", "width=710,height=555");
               popup.focus();

           }
</script>
</asp:Content>
