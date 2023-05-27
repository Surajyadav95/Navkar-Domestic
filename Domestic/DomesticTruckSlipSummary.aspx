﻿<%@ Page Title="Domestic |Truck Slip Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticTruckSlipSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Domestic | Truck Slip Summary</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Truck Slip Summary 
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
                     
<div class="panel">
<div class="panel-body">
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
                 

                                    
                                                
<div class="row">
    <div class="col-md-5  col-xs-12" style="width: 400px;">
<div class="form-group date text-label">
Date                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" Style="width: 150px;" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 40px;">To</div>
<asp:TextBox ID="txttoDate" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-4 col-xs-12" >
<div class="form-group text-label">
<b >Search</b>
<asp:TextBox ID="txtsearch" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 

<div class="col-sm-1" style="padding-left:10px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
                                              
                                      
</div>
                                               
</div>
 

                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:40px;">
<div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

 <asp:TemplateField>
<ItemTemplate>
    <a  href='<%# "../Report_Domestic/TruckSlipDomesticPrint.aspx?SlipNo=" & (DataBinder.Eval(Container.DataItem, "SLIP_NO")).ToString()%>'target="_blank" 
Class='btn btn-primary btn-xs outline' 
>Print</a>
     <asp:LinkButton ID="lnkCancel" ControlStyle-CssClass="btn btn-danger btn-xs outline"  Text="Cancel" OnClick="lnkCancel_Click" Visible="true"
                                                            OnClientClick="return confirm('Are you sure to Cancel this Truck?')"
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SLIP_NO")%>' runat="server"
                                                            ></asp:LinkButton>
    </ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="150px" />
</asp:TemplateField>


                                                    
<asp:BoundField DataField="SLIP_NO" HeaderText="Slip No"></asp:BoundField>
<asp:BoundField DataField="Slipdate" HeaderText="Slip Date"></asp:BoundField>
<asp:BoundField DataField="JO_NO" HeaderText="JO No"></asp:BoundField>
<asp:BoundField DataField="SLIP_TYPE" HeaderText="Slip Type"></asp:BoundField>
<asp:BoundField DataField="VEHICLE_NO" HeaderText="Vehicle No"></asp:BoundField>
 <asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name"></asp:BoundField>      
      <asp:BoundField DataField="DRIVER_NAME" HeaderText="Driver Name"></asp:BoundField>      
 <asp:BoundField DataField="LICENSE_NO" HeaderText="License No"></asp:BoundField>      
 <asp:BoundField DataField="CONTACT_NO" HeaderText="Contact Number"></asp:BoundField>      
        <asp:BoundField DataField="LR_NO" HeaderText="LR No"></asp:BoundField>      
     <asp:BoundField DataField="LRDATE" HeaderText="LR Date"></asp:BoundField>      

     <asp:BoundField DataField="Batch No" HeaderText="Batch No"></asp:BoundField>    
     <asp:BoundField DataField="Rake No" HeaderText="Rake No"></asp:BoundField>    
     <asp:BoundField DataField="Batch Pkgs" HeaderText="Batch Pkgs"></asp:BoundField>    
     <asp:BoundField DataField="Batch Weight" HeaderText="Batch Weight"></asp:BoundField>   
      <asp:BoundField DataField="Invoice No" HeaderText="Invoice No"></asp:BoundField>     
     <%--<asp:BoundField DataField="STATUS" HeaderText="STATUS"></asp:BoundField>--%>                                      
<%--<asp:BoundField DataField="CONTAINER_NO" HeaderText="Container No"></asp:BoundField>
<asp:BoundField DataField="size1" HeaderText="Size "></asp:BoundField>
    <asp:BoundField DataField="ContainerType" HeaderText="Container Type"></asp:BoundField>--%>
</Columns>

</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
    <div class="col-md-2 col-xs-12 " style="padding-top: 20px">
                                <div class="form-group  " >
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>
</div>


</div>
</div>   
                                 
                               
</div>
</div>
                          
                     
                       
                       
</div>
                 

</div>
         
</div>
<%-- <script type="text/javascript">
function checkRadioBtn(id) {
var gv = document.getElementById('<%=grdcontainer.ClientID%>');

for (var i = 1; i < gv.rows.length; i++) {
var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

// Check if the id not same
if (radioBtn[0].id != id.id) {
radioBtn[0].checked = false;
}
}
}
</script>--%>
<%--  <script type="text/javascript">
 
function BondExPrint() {
            
var NOCNo1= document.getElementById('<%= txtNOCNo.ClientID%>').value;
             
var url = "../Report_Bond/BondEx_logo_print.aspx?NOCNo=" + NOCNo1;
//alert("hi")
                
window.open(url);

}


</script>--%>
</asp:Content>