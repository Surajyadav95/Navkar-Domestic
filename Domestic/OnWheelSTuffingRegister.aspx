<%@ Page Title="Domestic |Loading Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="OnWheelSTuffingRegister.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Domestic | Loading Summary</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Loading Summary 
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
 
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
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
Category 
<asp:DropDownList ID="ddlCategory"   runat="server"  class="form-control text-label">
<asp:ListItem Value="0">All</asp:ListItem>
<asp:ListItem Value="1">Wagon No</asp:ListItem>
<asp:ListItem Value="2">Container No</asp:ListItem>
</asp:DropDownList>
</div>

</div>
    <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Search Text</b>
<asp:TextBox ID="txtSearchText" Style="text-transform:uppercase" MaxLength="11" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
                       </div>
 

<div class="col-sm-1" style="padding-left:10px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnSave_Click" 
Text="Search"/>
</div>
                                              
                                      
</div>
                                               
</div>
 
                     
<div class="row">


<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:40px;">
<div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="true" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="left"/> 
<Columns>
                      

</Columns>

</asp:GridView>
</div>
</div>
</div>
  <div class="col-md-2 col-xs-12 pull-right " style="padding-top: 20px">
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
                   
<a href="LoadingSummary.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
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
