<%@ Page Title="Domestic" Language="VB" EnableEventValidation="false" MasterPageFile="~/Domestic/PopUp.master" AutoEventWireup="false" CodeFile="PendingTruckSlipsIn.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
    window.opener.document.getElementById("ContentPlaceHolder1_btnTruckOut").click();
self.close();
}
</script>
   <style>
       .text-center{
           text-align:center
       }
       .scrolling-table-container{
           max-height:300px;
           overflow:auto
       }
   </style>
<div class="container" style="background-color: white">
   
<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left">
<div class="header-lined">
<h1>Pending Truck Slips (IN)<small class="pull-right" style="margin-right: 20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
</div>
</div>
<div class="row">
<div class="col-md-8 col-xs-8" >
<div class="form-group text-label">
<b >Search</b>
<asp:TextBox ID="txtsearch" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search by Slip no"
runat="server"   ></asp:TextBox>
</div>
</div> 
<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm" runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
</div>                                             
</div>

<div class="row">
<div class="col-md-10 col-xs-12 text-label " style="padding-right: 60px;">
<div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
<asp:GridView ID="grdTruckSlips" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:LinkButton ID="lnkselect"  ControlStyle-CssClass='btn btn-primary btn-sm'                                                       
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SLIP_NO")%>' runat="server" OnClick="lnkselect_Click"
><i class="fa fa-check"></i></asp:LinkButton>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="40px" />
</asp:TemplateField>
<asp:BoundField DataField="SLIP_NO" HeaderText="Slip In No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="VEHICLE_NO" HeaderText="Vehicle No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center"></asp:BoundField>

</Columns>
</asp:GridView>
</div>
</div>
</div>                       

<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">                        
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
<a href="BondSearch.aspx" class="btn btn-info btn-block">OK</a>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
<%--<script type="text/javascript" >
function callparentfunction() {
window.opener.document.getElementById("ContentPlaceHolder1_btnIndentItem").click();
self.close();
}
</script> --%>   
</asp:Content>


