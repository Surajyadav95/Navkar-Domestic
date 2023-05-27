<%@ Page Title="Domestic" Language="VB" EnableEventValidation="false" MasterPageFile="~/Domestic/PopUp.master" AutoEventWireup="false" CodeFile="TariffSearch.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
    window.opener.document.getElementById("ContentPlaceHolder1_btnNOCList").click();
self.close();
}
</script>
   <style>
       .text-center{
           text-align:center
       }

   </style>
<div class="container" style="background-color: white">   
<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left">
<div class="header-lined">
<h1>Tariff Search<small class="pull-right" style="margin-right: 20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
</div>
</div>
    <div class="row">

<div class="col-md-4 col-xs-6" >
<div class="form-group text-label">
<b >Search</b>
<asp:TextBox ID="txtsearch" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 
<div class="col-xs-1" >
<div class="form-group" style="padding-top: 18px;" >
<asp:Button ID="btnSave" class="btn btn-primary btn-sm" runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
</div>                                             
</div>
<div class="row">
<div class="col-md-12 col-xs-12 text-label ">
<div class="table-responsive scrolling-table-container" >
<asp:GridView ID="grdNOCList" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:LinkButton ID="lnkselect"  ControlStyle-CssClass='btn btn-primary btn-sm'                                                       
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TariffID")%>' runat="server" OnClick="lnkselect_Click"
><i class="fa fa-check"></i></asp:LinkButton>
    <asp:Label runat="server" ID="lblEntryID" Visible="false" Text='<%#Eval("ENTRYID")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="40px" />
</asp:TemplateField>
<asp:BoundField DataField="TariffID" HeaderText="Tariff ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
    <asp:TemplateField HeaderText="Tariff Description">
<ItemTemplate>
<asp:Label runat="server" ID="lblTariffDescription" Text='<%#Eval("TariffDescription")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
<%--<asp:BoundField DataField="TariffDescription" HeaderText="Tariff Description"></asp:BoundField>--%>
<asp:BoundField DataField="Effectivefrom" HeaderText="Effective From"></asp:BoundField>
    <asp:BoundField DataField="EffectiveUpto" HeaderText="Effective Upto"></asp:BoundField>
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


