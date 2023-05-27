<%@ Page Title="Domestic | Generate Truck Slip (IN)" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticTruckIn.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic | Generate Truck Slip (IN)</title>
       
</head>
    <style>
        .scrolling-table-container{
            max-height:200px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>Generate Truck Slip (IN)
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
 
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

    


                                         
<div class="col-md-9 pull-md-left main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
                
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Slip No</b>
    <asp:TextBox ID="txtslipno" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>
   
</div>
</div>
    <div class="col-md-2" style="display:none">
    <asp:TextBox runat="server" ID="txtSlipNoPrint"></asp:TextBox>
    </div>
  
 <div class="col-md-4  col-xs-12" ">                                      
<div class="form-group text-label">
<b >Slip Date</b>
<asp:TextBox ID="txtslipdate"  placeholder="dd-mm-yyyy " TextMode="Datetimelocal"  runat="server"   BackColor="White" Class="form-control text-label"></asp:TextBox>

</div>                        
</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
       <div class="col-sm-3 col-xs-12" >
<div class="form-group text-label">
<b >JO No</b>
    <asp:TextBox ID="txtJONo" Style="text-transform: uppercase;" AutoPostBack="true" onkeypress="return ValidateNumber();" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="JO No"></asp:TextBox>   
</div>
</div>
                                        </ContentTemplate></asp:UpdatePanel>
    <div class="col-sm-1 col-xs-6">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="LinkButton2" ControlStyle-CssClass='btn btn-primary btn-sm'  runat="server"
OnClientClick="return OpenNOCList();"><i class=" fa fa-search" aria-hidden="true"></i></asp:LinkButton>  
</div>                                  
</div> 
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        

 <div class="row">
     <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Entry Type</b>
<asp:DropDownList ID="ddlSlipType"  Style="text-transform: uppercase;"  runat="server" class="form-control text-label">
    <asp:ListItem Value="0">--Select--</asp:ListItem>
    <asp:ListItem Value="3">BTT</asp:ListItem>
    <asp:ListItem Value="2">Delivery</asp:ListItem>
<asp:ListItem Value="1">Receipt</asp:ListItem>
    <asp:ListItem Value="4">Rejected Cargo</asp:ListItem>
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

<b>Vehicle No</b>
<asp:TextBox ID="txtVehilceNo" Style="text-transform:uppercase"  class="form-control text-label"  placeholder=""
runat="server"   ></asp:TextBox>
</div>
                       </div>
 
     <div class="col-sm-3 col-xs-12">
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
           <asp:Panel runat="server" Enabled="True">
 <div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Party Name</b>
<asp:DropDownList ID="ddlparty"  Style="text-transform: uppercase;"  runat="server" class="form-control">
</asp:DropDownList>
</div>
</div>
     <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Transporter Name</b>
<asp:DropDownList ID="ddlTransporterName"  Style="text-transform: uppercase;"  runat="server" class="form-control">
</asp:DropDownList>
</div>
</div>
    </div>
</asp:Panel>
    <div class="row">
        <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Driver Name</b>
<asp:TextBox ID="txtdriver" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Driver Name"
runat="server"   ></asp:TextBox>
</div>
</div>

             <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >License No</b>
<asp:TextBox ID="txtlicense" Style="text-transform:uppercase" class="form-control text-label"  placeholder="License No"
runat="server"   ></asp:TextBox>
</div>
</div>



    </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
                 <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Contact Number</b>
<asp:TextBox ID="txtcontact" Style="text-transform:uppercase" class="form-control text-label" onkeypress="return ValidatePhoneNo()"  placeholder="Contact Number"
runat="server"   ></asp:TextBox>
</div>
</div>
          <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >LR No</b>
<asp:TextBox ID="txtlrNO" Style="text-transform:uppercase" class="form-control text-label"  placeholder=" LR No"
runat="server"   ></asp:TextBox>
</div>
</div>

        <div class="col-md-4  col-xs-12" ">                                      
<div class="form-group text-label">
<b >LR Date</b>
<asp:TextBox ID="txtlrdate"  placeholder="dd-mm-yyyy " TextMode="Datetimelocal"  runat="server"   BackColor="White" Class="form-control text-label"></asp:TextBox>

</div>                        
</div>


    </div>
    <div class="row">
             <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Batch No</b>
<asp:TextBox ID="txtBatchNo" Style="text-transform:uppercase"    class="form-control text-label"  placeholder="Batch No"
runat="server"   ></asp:TextBox>
</div>
</div>
          <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Rake No</b>
<asp:TextBox ID="txtRakeNo"  Style="text-transform:uppercase" class="form-control text-label"  placeholder="Rake No"
runat="server"   ></asp:TextBox>
</div>
</div>
         <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Pkgs</b>
<asp:TextBox ID="txtPkgs" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Pkgs"
runat="server"   ></asp:TextBox>
</div>
</div>
                <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Weight</b>
<asp:TextBox ID="txtWeight" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Weight"
runat="server"   ></asp:TextBox>
</div>
</div>
          <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Invoice No</b>
<asp:TextBox ID="txtInvoiceNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Invoice No"
runat="server"   ></asp:TextBox>
</div>
</div>
          <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Commodity</b>
<asp:DropDownList ID="ddlCommodity"  Style="text-transform: uppercase;"  runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
            <div class="col-sm-1" style="padding-right:5px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnadd" class="btn btn-primary btn btn-sm outline " runat="server"    
Text="Add"  OnClientClick="return ValidationAdd()" OnClick="btnadd_Click"/>
</div>
                                              
</div> 
    </div>
    <div class="row">
      
    </div>
    <div class="row">
   
<div class="col-lg-12 text-label "  style="padding-right:40px;">
<div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"> 
<Columns>

                        <asp:TemplateField Visible="true">
                             <ItemTemplate>
                                 <asp:LinkButton ID="lnkdelete" ControlStyle-CssClass='btn btn-danger btn-xs outline'   OnClick="lnkdelete_Click" 
                                 CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "BatchNo")%>'  runat="server"
                                 > <i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton>
                             </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Batch No" >
                             <ItemTemplate>
                                 <asp:Label id="lblBatchno" runat="server" Text='<%# Eval("BatchNo")%>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Rake No" >
                             <ItemTemplate>
                                 <asp:Label id="lblRakeNo" runat="server" Text='<%# Eval("RakeNo")%>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Batch Pkgs" >
                             <ItemTemplate>
                                 <asp:Label id="lblBatchPkgs" runat="server" Text='<%# Eval("BatchPkgs")%>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Batch Weight" >
                             <ItemTemplate>
                                 <asp:Label id="lblBatchWeight" runat="server" Text='<%# Eval("BatchWeight")%>'></asp:Label>
                             </ItemTemplate>
                            </asp:TemplateField>
          <asp:TemplateField HeaderText="Commodity" >
                             <ItemTemplate>
                                 <asp:Label id="lblCommodity" runat="server" Text='<%# Eval("Commodity")%>'></asp:Label>
                                 <asp:Label id="lblCommodityID" Visible="false" runat="server" Text='<%# Eval("CommodityID")%>'></asp:Label>
                             </ItemTemplate>
                            </asp:TemplateField>
                         <asp:TemplateField HeaderText="InvoiceNo" >
                             <ItemTemplate>
                                 <asp:Label id="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo")%>'></asp:Label>
                             </ItemTemplate>
                            </asp:TemplateField>
  
                                                          
 
</Columns>

</asp:GridView>
</div>
</div>
</div>
   <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
<asp:Button ID="btnNOCList" runat="server" Text="Call Button Item Click" style="display:none" OnClick="btnNOCList_Click"  />
   
                              


     </ContentTemplate>
             </asp:UpdatePanel>
       <div class="row">
           <div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b >Remarks</b>
<asp:TextBox ID="txtremarks" Style="text-transform:uppercase" class="form-control text-label" Rows="2" placeholder="Remarks"  
TextMode="MultiLine"   runat="server"   ></asp:TextBox>
</div>
</div>
</div>
 
    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="btnSave_Click"  
Text="Save"  OnClientClick="return ValidationSave()" />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:8px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="TruckSlipGenerate.aspx" id="btnclear" runat="server" class="btn btn-primary btn btn-sm outline ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
<%--<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="TruckSlipSummary.aspx" target="_blank"><b style="color:blue">Click here to view Truck Slip summary</b> </a>
</div>
</div>--%>
                         
</div>

                               
</asp:Panel>
                        
</div>
</div>



<asp:Label ID="lblID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblName" Visible="false" runat="server" Text=""></asp:Label>
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
<a href="DomesticTruckIn.aspx" class="btn btn-danger ">No</a>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>             
</fieldset>

</div>

        <div class="col-md-3 pull-md-right sidebar" style="padding-top:0px;">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title" style="padding-bottom: 0px !important; ">
<i class="fa fa-cube"></i>&nbsp;Recent Slips  
 
</h3>
</div>   
<div class="panel-body" Style="text-transform:uppercase;">
<asp:Repeater ID="rptnoLIst" runat="server">
<ItemTemplate>
<div  >
                             
                            
<strong><asp:Label runat="server"  Text='<%#Eval("Vehicle_no")%>' ID="lblNONumber" style="text-transform:uppercase;"></asp:Label></strong>                                                    
<br /> <asp:Label runat="server" Text='<%#Eval("AddDate")%>' Id="lblDate" style="text-transform:uppercase;"></asp:Label>
<br/>Generated By: &nbsp;<asp:Label runat="server" Text='<%#Eval("Users")%>' Id="lbluser" style="text-transform:uppercase;"></asp:Label>
<br />
<asp:Label runat="server" >-----------------------------------------</asp:Label>
</div>
</ItemTemplate>
</asp:Repeater>
</div>
</div>
                           
</div>
        
                     
</div>
                 
 
</div>
       
         
</div>
   <script type="text/javascript">
       var popup;
       function OpenSlipPrint() {
                  
           var txtSlipNoPrint = document.getElementById('<%= txtSlipNoPrint.ClientID%>').value;
           
           var url = "../Report_Domestic/TruckSlipDomesticPrint.aspx?SlipNo=" + txtSlipNoPrint
           
    window.open(url);

}

</script>
    <script type="text/javascript">
        var popup;
        function OpenNOCList() {

            var popup;
            var url = "JOListforTruckIn.aspx"
            //window.open(url);
            popup = window.open(url, "Popup", "width=800,height=550");
            popup.focus();
       }

</script>
    <script type="text/javascript">
        function OpenContainerList() {

            var txtJONo = document.getElementById('<%= txtJONo.ClientID%>').value;

    var blResult = Boolean;
    blResult = true;

    if (txtJONo == "") {
        document.getElementById('<%= txtJONo.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    //alert('hi')
    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    else
    {
        var popup;
        var url = "ContainerListForTruckIn.aspx?NOCNo=" + txtNOCNo
        //window.open(url);
        popup = window.open(url, "Popup", "width=800,height=550");
        popup.focus();
    }
    return blResult;
}
</script>
<script type="text/javascript">
function ValidationSave() {
                 
     
    var txtVehilceNo = document.getElementById('<%= txtVehilceNo.ClientID%>').value;          
    var ddlSlipType = document.getElementById('<%= ddlSlipType.ClientID%>').value;             
    var ddlparty = document.getElementById('<%= ddlparty.ClientID%>').value; 
    var txtJONo = document.getElementById('<%= txtJONo.ClientID%>').value;          
    var ddlFromLocation = document.getElementById('<%= ddlFromLocation.ClientID%>').value;
    var ddlToLocation = document.getElementById('<%= ddlToLocation.ClientID%>').value;

var blResult = Boolean;
blResult = true;
 

<%--if (txtJONo == "") {
    document.getElementById('<%= txtJONo.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}--%>

  <%--  if (txtVehilceNo == "") {
        document.getElementById('<%= txtVehilceNo.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
    --%>

    if (ddlSlipType == 0) {
        document.getElementById('<%= ddlSlipType.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }

    if (ddlparty == 0) {
        document.getElementById('<%= ddlparty.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
    if (ddlFromLocation == 0) {
        document.getElementById('<%= ddlFromLocation.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (ddlToLocation == 0) {
        document.getElementById('<%= ddlToLocation.ClientID%>').style.borderColor = "red";
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
      function ValidatePhoneNo() {
          //alert('hii')
          if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 43 || event.keyCode == 32 || event.keyCode == 40 || event.keyCode == 41)
              return event.returnValue;
          return event.returnValue = '';
      }
      function ValidateNumber() {
          //alert('hii')
          if (event.keyCode > 47 && event.keyCode < 58)
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
