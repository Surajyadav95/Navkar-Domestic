<%@ Page Title="Domestic |Domestic Gate Pass" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticGatePass.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic |Domestic Gate Pass</title>
       
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
<i class="glyphicon glyphicon-transfer"></i>Domestic Gate Pass
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
 Domestic Gate Pass
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
 <div class="row">

         <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Gate Pass No</b>
<asp:TextBox ID="txtgatepassno" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
 <div class="col-md-2" style="display:none">
    <asp:TextBox runat="server" ID="txtSlipNoPrint"></asp:TextBox>
    </div>

              <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >Gate Pass Date & Time</b>
                    <asp:TextBox ID="txtgatepassdate" placeholder="dd-MM-yyyy"  TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>

       <div class="col-sm-1 col-xs-6"   >                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="LinkButton2" ControlStyle-CssClass='btn btn-primary btn-sm'  runat="server"
OnClientClick="return OpenNOCList();"><i class=" fa fa-search" aria-hidden="true"></i></asp:LinkButton>  
</div>                                  
</div>
        
     </div>
    
<asp:Button ID="btnDomesticgate" OnClick="btnDomesticgate_Click" runat="server" Text="Call Button Item Click" style="display:none"  />

  
     
 
      </div>


<asp:Label ID="LblVendorID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblLoaded" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblLoadingID" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblDriverNo" Visible="false" runat="server" Text=""></asp:Label>

    
    <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
 
 

<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive  ">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!"  ShowHeaderWhenEmpty="true"   >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
     
     <asp:TemplateField HeaderText="Jo No" Visible="false"  HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblJoNo" Visible="false"  runat="server" Text='<%# Eval("JONO")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                       
                         <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblContainerNo" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblSize" runat="server" Text='<%# Eval("SIZE")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cargo Type" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblCargoType" runat="server" Text='<%# Eval("CargoType")%>'></asp:Label>
                                 <asp:Label id="lblCargoTypeID" Visible="false" runat="server" Text='<%# Eval("CARGOTYPEID")%>'></asp:Label>
                                 
                             </ItemTemplate>                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="PKGS" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblPKGS" runat="server" Text='<%# Eval("PKGS")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>

     <asp:TemplateField HeaderText="WEIGHT" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblStuffed_Wt" runat="server" Text='<%# Eval("WEIGHT")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
    <asp:TemplateField HeaderText="Source" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                  <asp:Label id="lblLocation" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                                     <asp:Label id="lbllocationID" Visible="false" runat="server" Text='<%# Eval("FromID")%>'></asp:Label>
                             </ItemTemplate> 
                             <ItemStyle HorizontalAlign="Right" />                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delivered Qty" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                     <asp:Label id="lblLoaded_Qty" Visible="false" runat="server" Text='<%# Eval("Loaded_Qty")%>'></asp:Label>
                                 <asp:TextBox runat="server" ID="txtLoadedQty" CssClass="form-control" Width="80px" Text='<%# Eval("Loaded_Qty")%>'></asp:TextBox>
                             </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />    
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Delivered Weight" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                    <asp:Label id="lblLoaded_Wt" runat="server" Visible="false" Text='<%# Eval("Loaded_Wt")%>'></asp:Label>
                                 <asp:TextBox runat="server" ID="txtLoadedWeight" CssClass="form-control" Width="80px" Text='<%# Eval("Loaded_Wt")%>'></asp:TextBox>
                             </ItemTemplate> 
                             <ItemStyle HorizontalAlign="Right" />                           
                        </asp:TemplateField>

                      <%--    <asp:TemplateField HeaderText="Excess PKGS" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                 <asp:TextBox runat="server" onkeypress="return ValidateQty();" ID="txtExcesspkgs" OnTextChanged="txtExcesspkgs_TextChanged" AutoPostBack="true"  Width="100px" Class="form-control" Text='<%# Eval("Excess PKGS")%>'></asp:TextBox>
                             </ItemTemplate> 
                             <ItemStyle HorizontalAlign="Right" />                           
                        </asp:TemplateField>--%>

                   

    

                            <asp:TemplateField HeaderText="Vehicle No" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                 <asp:Label id="lblVehicle" Visible="false" runat="server" Text='<%# Eval("Loaded_Truck_No")%>'></asp:Label>
                                 <asp:TextBox runat="server" ID="txtVehicle" CssClass="form-control" Text='<%# Eval("Loaded_Truck_No")%>'></asp:TextBox>

                             </ItemTemplate> 
                             <ItemStyle HorizontalAlign="Right" />                           
                        </asp:TemplateField>

     <asp:TemplateField HeaderText="IGMNO" Visible="false" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblIGMNO" Visible="false" runat="server" Text='<%# Eval("IGMNO")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField> 
    
       <asp:TemplateField HeaderText="IGMNO" Visible="false" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblLoaded_ID" Visible="false" runat="server" Text='<%# Eval("GateIn_No")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>                          
                       

     <asp:TemplateField HeaderText="ITEMNO" Visible="false"  HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblITEMNO" Visible="false" runat="server" Text='<%# Eval("ITEMNO")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>


     <asp:TemplateField HeaderText="IMPORTER" Visible="false"  HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lbIMPORTER" Visible="false" runat="server" Text='<%# Eval("IMPORTER")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>



     <asp:TemplateField HeaderText="CARGODESCRIPTION" Visible="false"  HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblCARGODESCRIPTION" Visible="false" runat="server" Text='<%# Eval("CARGODESCRIPTION")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>


     <asp:TemplateField HeaderText="Seal No" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblSealNo" Visible="false" runat="server" Text='<%# Eval("Seal_No")%>'></asp:Label>
                                 <asp:TextBox id="txtSealNo" runat="server" Text='<%# Eval("Seal_No")%>' CssClass="form-control"></asp:TextBox>

                             </ItemTemplate>                            
                        </asp:TemplateField>

    <asp:TemplateField HeaderText="DO NO" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblDONO"  runat="server" Text='<%# Eval("DONo")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
    <asp:TemplateField HeaderText="Invoice NO" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblInvoiceNo"  runat="server" Text='<%# Eval("InvoiceNo")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
     <asp:TemplateField HeaderText="REF JO NO" Visible="false" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblREFJONO"  runat="server" Text='<%# Eval("REFJONO")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>

     <asp:TemplateField HeaderText="D_Container_No" Visible="false"  HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblDContainerNo" Visible="false" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>



     <asp:TemplateField HeaderText="D_Entry_ID" Visible="false"  HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblDEntryID" Visible="false" runat="server" Text='<%# Eval("JONO")%>'></asp:Label>
                             </ItemTemplate>                            



                        </asp:TemplateField>

    <asp:TemplateField HeaderText="CHA" Visible="false"  HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblCHA" Visible="false" runat="server" Text='<%# Eval("CHA")%>'></asp:Label>
                             </ItemTemplate>                            



                        </asp:TemplateField>


    <asp:TemplateField HeaderText="LINEID" Visible="false"  HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblLINEID" Visible="false" runat="server" Text='<%# Eval("LINEID")%>'></asp:Label>
                             </ItemTemplate>                            



                        </asp:TemplateField>
    <asp:TemplateField HeaderText="CUSTOMERID" Visible="false"  HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblCUSTOMERID" Visible="false" runat="server" Text='<%# Eval("CUSTOMERID")%>'></asp:Label>
                             </ItemTemplate>                            



                        </asp:TemplateField>

    <asp:TemplateField HeaderText="PONO" Visible="false"  HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblPONO" Visible="false" runat="server" Text='<%# Eval("PONO")%>'></asp:Label>
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
         <div class="col-md-6 col-xs-12" >
            <div class="form-group text-label">

<b>Transporter</b>
<asp:DropDownList ID="ddltransporter"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>

            <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>LR No</b>
<asp:TextBox ID="txtLrno" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="LR No"
runat="server"   ></asp:TextBox>
</div>
                       </div>

         <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >LR Date</b>
                    <asp:TextBox ID="txtLrDate" placeholder="dd-MM-yyyy"  TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>


    </div>

    <div class="row">
               <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>BOE No</b>
<asp:TextBox ID="txtboeno" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="BOE No"
runat="server"   ></asp:TextBox>
</div>
                       </div>

         <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >BOE Date</b>
                    <asp:TextBox ID="txtBoeDate" placeholder="dd-MM-yyyy"  TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>
        <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Location</b>
<asp:DropDownList ID="ddlLocation" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
              <div class="col-md-2 col-xs-12" style="display:none" >
            <div class="form-group text-label">

<b>Ref Jo No</b>
<asp:TextBox ID="txtRefJoNo" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Ref Jo No"
runat="server"   ></asp:TextBox>
</div>

                       </div>

        <div class="col-md-4 col-xs-12" style="display:none" >
            <div class="form-group text-label">

<b>Address</b>
<asp:TextBox ID="txtAddress" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Address"
runat="server"   ></asp:TextBox>
</div>
                       </div>
    </div>
    <div class="row">
        <div class="col-md-6 col-xs-12" >
            <div class="form-group text-label">

<b>Remarks</b>
<asp:TextBox ID="txtRemarks" Style="text-transform:uppercase" TextMode="MultiLine"  class="form-control text-label"  placeholder="Remarks"
runat="server"   ></asp:TextBox>
</div>
                       </div>
    </div>
    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:8px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server"   OnClick="btnSave_Click"
Text="Save" OnClientClick="return ValidationSav()"   />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:0px;">
<div class="form-group" style="padding-top:8px">
                           
<a href="DomesticGatePass.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
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
<a href="DomesticGatePass.aspx" class="btn btn-danger ">ON</a>
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
         function OpenSlipPrint() {

             var txtgatepassno = document.getElementById('<%= txtgatepassno.ClientID%>').value;

             var url = "../Report_Domestic/DomesticGatePassPrint.aspx?GPNO=" + txtgatepassno

             window.open(url);

         }

</script>
         
    <script type="text/javascript">
        function ValidationSav() {
            
     
            var ddltransporter = document.getElementById('<%= ddltransporter.ClientID%>').value;
            var ddlLocation = document.getElementById('<%= ddlLocation.ClientID%>').value;

var blResult = Boolean;
blResult = true;
              
 

if (ddltransporter == 0) {
document.getElementById('<%= ddltransporter.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

            if (ddlLocation == 0) {
        document.getElementById('<%= ddlLocation.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
          
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
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
    
    <script type="text/javascript">
        var popup;
        function OpenNOCList() {

            var popup;
            var url = "DomesticGatePassSearch.aspx"
            //window.open(url);
            popup = window.open(url, "Popup", "width=800,height=550");
            popup.focus();
        }

</script>
</asp:Content>
