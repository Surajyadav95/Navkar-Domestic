<%@ Page Title="Domestic | Commodity Master" Language="VB" EnableEventValidation="false" MasterPageFile="~/Domestic/PopUp.master" AutoEventWireup="false" CodeFile="CommodityMaster.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
window.opener.document.getElementById("MainContent_btnIndentItem").click();
self.close();
}
</script>
<div class="container" style="background-color: white">

<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<h1>Commodity Master  <small class="pull-right" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>

<div class="row">
     <div class="col-sm-4 col-xs-10" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >ID </b>
<asp:TextBox ID="txtid" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>

<div class="col-sm-10 col-xs-8">
<div class="form-group text-label">
<b  >Commodity Name</b>
<asp:TextBox ID="txtCommodityname" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Commodity Name"
runat="server"   ></asp:TextBox>
</div>
</div>

             
<div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"   
Text="Save"  OnClientClick="return ValidationSave()"  />
</div>
                                              
                                      
</div>
</div>

<div class="row" style="display:none">
<div class="col-sm-4 col-xs-4 pull-right">
<div class="form-group text-label" style="padding-top:10px;">
<asp:CheckBox ID="chkisActive" runat="server" Checked="true" />
<asp:hiddenfield ID="hdlocation" runat="server" Value="0" />
<asp:Label ID="IsActiveLabel" runat="server" AssociatedControlID="chkisActive" CssClass="inline"> Is Active?</asp:Label>
</div>
</div>
</div>

<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lbllocation" Visible="false" runat="server" Text=""></asp:Label>
    <div class="row ">

<div class="col-sm-5 col-xs-5 ">
<div class="form-group text-label">
<b  >Search</b>
<asp:TextBox ID="txtsearch" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-2 col-xs-2">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnsearch" class="btn btn-primary " runat="server" OnClick="btnsearch_Click"  
Text="Search"  />
</div>
                                              
                                      
</div>
</div>
<div class="row">
                    

<div class="row">
<div class="col-lg-6 text-label " style="padding-right: 50px;">
<div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="4">
<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />
<Columns>
 
 
                                                 
                                                 
<asp:BoundField DataField="ID"   HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Commodity"   HeaderText="Commodity"></asp:BoundField>

</Columns>

</asp:GridView>
</div>
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
                   
<a href="CommodityMaster.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
                
</div>
</div>
                 

</div>
</div>
</div>
<script type="text/javascript">
function ValidationSave() {
                 
    var txtCommodityname = document.getElementById('<%= txtCommodityname.ClientID%>').value;
                   

var blResult = Boolean;
blResult = true;
 

if (txtCommodityname == "") {
document.getElementById('<%= txtCommodityname.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
 
//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>
</asp:Content>


