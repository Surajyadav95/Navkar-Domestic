<%@ Page Title="Domestic | De-Stuff Entry" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticDestuffEntry.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic | De-Stuff Entry</title>
       
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
<i class="glyphicon glyphicon-transfer"></i>De-Stuff Entry
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
      <asp:Panel ID="Panel2" runat="server" Enabled="true">       
<div class="panel-body">                      
 <div class="row">

         <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >De-Stuffing ID</b>
<asp:TextBox ID="txtstuffingID" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
 

    
              <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >De-Stuffing Date </b>
                    <asp:TextBox ID="txtGateInDate" placeholder="dd-MM-yyyy" TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>

     <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>De-Stuffing Type</b>
<asp:DropDownList ID="ddlStuffingType"   Style="text-transform: uppercase;" OnSelectedIndexChanged="ddlStuffingType_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control text-label">
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

       <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
        
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
       <div class="col-sm-3 col-xs-6" style="display:none">                                      
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

<b>Manifiest Pkgs</b>
<asp:TextBox ID="txtpkgs" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Pkgs"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Manifiest Weight</b>
<asp:TextBox ID="txtWeight" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Weight"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Balance Pkgs</b>
<asp:TextBox ID="txtBalPkgs" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Pkgs"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Balance Weight</b>
<asp:TextBox ID="txtBalWeight" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Weight"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
     </ContentTemplate>
                 </asp:UpdatePanel>
             <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                 <Triggers>
                     <asp:PostBackTrigger ControlID="btnAdd" />
                 </Triggers>
<ContentTemplate>
        <div class="row">
               <div class="col-md-2 col-xs-12" style="display: none;" id="divStuffedl" runat="server" >
            <div class="form-group text-label">
                <b><asp:Label ID="lblcriteria" Visible="false" runat="server" Text=""></asp:Label></b>
 <b>Ware House</b>
<asp:DropDownList ID="ddllocation"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>

              <div class="col-md-2 col-xs-12 pull-left" style="display: none;" id="divStuffed" runat="server" >
            <div class="form-group text-label">

<b>Stuffed Container No</b>
<asp:TextBox ID="txtStuffedContainerNo" Style="text-transform:uppercase" OnTextChanged="txtStuffedContainerNo_TextChanged" AutoPostBack="true"  class="form-control text-label"  placeholder="Stuffed Container No"
runat="server"   ></asp:TextBox>
</div>
                       </div>


            <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Stuffed Pkgs</b>
<asp:TextBox ID="txtStuffedQty" Style="text-transform:uppercase" OnTextChanged="txtStuffedQty_TextChanged" AutoPostBack="true" onkeypress="return ValidateNo();"  class="form-control text-label"  placeholder="Stuffing Qty"
runat="server"   ></asp:TextBox>
</div>
                       </div>
    
            <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Stuffed Wt</b>
<asp:TextBox ID="txtStuffedWt" Style="text-transform:uppercase" OnTextChanged="txtStuffedWt_TextChanged" AutoPostBack="true" onkeypress="return ValidateQty();" class="form-control text-label"  placeholder="Stuffed Wt"
runat="server"   ></asp:TextBox>
</div>
                       </div>
 
            <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Short Pkgs</b>
<asp:TextBox ID="txtShortPkgs" Style="text-transform:uppercase" OnTextChanged="txtShortPkgs_TextChanged" AutoPostBack="true" onkeypress="return ValidateNo();" class="form-control text-label"  placeholder="Short Pkgs"
runat="server"   ></asp:TextBox>
</div>
                       </div>
       
            <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Excess Pkgs</b>
<asp:TextBox ID="txtExcessPkgs" Style="text-transform:uppercase" onkeypress="return ValidateNo();" class="form-control text-label"  placeholder="Excess Pkgs"
runat="server"   ></asp:TextBox>
</div>
                       </div>

            </div>
            <div class="row">
            <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">
<b>DCA/Jo No</b>
<asp:TextBox ID="txtDCAJoNo" Style="text-transform:uppercase"   class="form-control text-label"  placeholder="DCA/Jo No"
runat="server"   ></asp:TextBox>
</div>
                       </div>

            <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>IGM No</b>
<asp:TextBox ID="txtIgmNo" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="IGM No"
runat="server"   ></asp:TextBox>
</div>
                       </div>

             <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Item No</b>
<asp:TextBox ID="txtItemNo" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Item No"
runat="server"   ></asp:TextBox>
</div>
                       </div>

                
              <div class="col-md-2 col-xs-12" style="display:none">
            <div class="form-group text-label">

<b>Seal No</b>
<asp:TextBox ID="txtSealNo" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Seal No"
runat="server"   ></asp:TextBox>
</div>
                       </div>
              <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Cargo Desc</b>
<asp:TextBox ID="txtCargoDesc" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Cargo Desc"
runat="server"   ></asp:TextBox>
</div>
                       </div>

                <div class="col-md-1 col-xs-12" style="display:none" >
            <div class="form-group text-label">

<b>CBM</b>
<asp:TextBox ID="txtcbm" Style="text-transform:uppercase" onkeypress="return ValidateQty();" OnTextChanged="txtcbm_TextChanged" AutoPostBack="true"  class="form-control text-label"  placeholder="CBM"
runat="server"   ></asp:TextBox>
</div>
                       </div>
             <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">
<b>Area (Sq.Ft.)</b>
<asp:TextBox ID="txtArea" Style="text-transform:uppercase" onkeypress="return ValidateQty();"  class="form-control text-label"  placeholder="Area"
runat="server"   ></asp:TextBox>
</div>
                       </div>
                <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">
<b>No of Labours</b>
<asp:TextBox ID="txtNoLabours" Style="text-transform:uppercase" onkeypress="return ValidateNo();" class="form-control text-label"  placeholder="No of Labours"
runat="server"   ></asp:TextBox>
</div>
                       </div>
     <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Vendor Name</b>
<asp:DropDownList ID="ddlVendorName"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>

                     <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Equipment Type</b>
<asp:DropDownList ID="ddlEquipmentType"   Style="text-transform: uppercase;"  runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
    <div class="col-sm-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnAdd" class="btn btn-primary btn-sm outline " runat="server" 
Text="Add"  OnClientClick="return ValidationAdd()" OnClick="btnAdd_Click"  />
</div>
                                              
                                      
</div>
 </div>
              </ContentTemplate>
                  </asp:UpdatePanel>
  
     
 
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
                                                               
                                                            
<asp:LinkButton ID="lnkDelete"  ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Delete"  OnClick="lnkDelete_Click"                                                  
    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ContainerNo")%>' runat="server"  
    ></asp:LinkButton>

   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="100px" />
</asp:TemplateField>
         <asp:TemplateField HeaderText="Container No" >
                             <ItemTemplate>
                                 <asp:Label id="lblcontainerno" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
                                 <asp:Label id="lblSEntryID" Visible="false" runat="server" Text='<%# Eval("SEntryID")%>'></asp:Label>

                             </ItemTemplate>                            
                        </asp:TemplateField>

      <asp:TemplateField HeaderText="Ware House" >
                             <ItemTemplate>
                                 <asp:Label id="lblWareHouse" runat="server" Text='<%# Eval("WareHouse")%>'></asp:Label>
                                 <asp:Label id="lblWareHouseID" Visible="false" runat="server" Text='<%# Eval("WareHouse_ID")%>'></asp:Label>
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
    <asp:TemplateField HeaderText="Short Pkgs" >
                             <ItemTemplate>
                                 <asp:Label id="lblShortPkgs" runat="server" Text='<%# Eval("ShortPkgs")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
    <asp:TemplateField HeaderText="Excess Pkgs" >
                             <ItemTemplate>
                                 <asp:Label id="lblExcessPkgs" runat="server" Text='<%# Eval("ExcessPkgs")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
     <asp:TemplateField HeaderText="DCA/Jo No" >
                             <ItemTemplate>
                                 <asp:Label id="lblDCANo" runat="server" Text='<%# Eval("DCA_No")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

     <asp:TemplateField HeaderText="IGM No" >
                             <ItemTemplate>
                                 <asp:Label id="lblIGMNo" runat="server" Text='<%# Eval("IGM_No")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>


     <asp:TemplateField HeaderText="Item No" >
                             <ItemTemplate>
                                 <asp:Label id="lblItemNo" runat="server" Text='<%# Eval("Item_No")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

         <asp:TemplateField HeaderText="Seal No" >
                             <ItemTemplate>
                                 <asp:Label id="lblSealNo" runat="server" Text='<%# Eval("Seal_No")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>
    
     <asp:TemplateField HeaderText="Cargo Description" >
                             <ItemTemplate>
                                 <asp:Label id="lblCargoDesc" runat="server" Text='<%# Eval("Cargo_Desc")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

     <asp:TemplateField HeaderText="Area" >
                             <ItemTemplate>
                                 <asp:Label id="lblArea" runat="server" Text='<%# Eval("Area")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>
          <asp:TemplateField HeaderText="No of Labours" >
                             <ItemTemplate>
                                 <asp:Label id="lblNoofLabours" runat="server" Text='<%# Eval("NoLabours")%>'></asp:Label>
                             </ItemTemplate>
                            

                        </asp:TemplateField>
         <asp:TemplateField HeaderText="Vendor" >
                             <ItemTemplate>
                                 <asp:Label id="lblVendorName" runat="server" Text='<%# Eval("VendorName")%>'></asp:Label>
                                 <asp:Label id="lblVendorID" Visible="false" runat="server" Text='<%# Eval("VendorID")%>'></asp:Label>

                             </ItemTemplate>
                            

                        </asp:TemplateField>
         <asp:TemplateField HeaderText="Equipment Type" >
                             <ItemTemplate>
                                 <asp:Label id="lblEquipmentType" runat="server" Text='<%# Eval("EquipmentType")%>'></asp:Label>
                                 <asp:Label id="lblEquipmentTypeID" Visible="false" runat="server" Text='<%# Eval("EquipmentTypeID")%>'></asp:Label>

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
                           
<a href="DomesticDestuffEntry.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
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
                   
<a href="DomesticDestuffEntry.aspx" class="btn btn-info btn-block">OK</a>
                                
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
            function ValidationShow() {
                
                var ddlStuffingType = document.getElementById('<%= ddlStuffingType.ClientID%>').value;
                
                var txtcontainerNo = document.getElementById('<%= txtcontainerNo.ClientID%>').value;
                
           

            var blResult = Boolean;
            blResult = true;


        
            if (ddlStuffingType == 0) {
                document.getElementById('<%= ddlStuffingType.ClientID%>').style.borderColor = "red";
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
         var popup;
         function gstsearch() {

             var url = "ContainerListforDestuffing.aspx"

             popup = window.open(url, "Popup", "width=710,height=555");
             popup.focus();

         }
</script>
    <script type="text/javascript">
        function ValidateQty() {
            //alert('hii')
            if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 46)
                return event.returnValue;
            return event.returnValue = '';
        }
        function ValidateNo() {
            //alert('hii')
            if ((event.keyCode > 47 && event.keyCode < 58))
                return event.returnValue;
            return event.returnValue = '';
        }
        </script>
</asp:Content>
