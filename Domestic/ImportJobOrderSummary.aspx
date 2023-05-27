<%@ Page Title="Domestic |Job Order Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticJobOrderSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Domestic | Job Order Summary </title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Job Order Summary  
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
    <div class="col-sm-6 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" >To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional"><ContentTemplate>
<div class="col-md-2 col-xs-12" >
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem> 
<asp:ListItem Value="1">Customer</asp:ListItem>
    <asp:ListItem Value="2">Importer</asp:ListItem>
    <asp:ListItem Value="3">CHA</asp:ListItem>
    <asp:ListItem Value="4">IGM No</asp:ListItem>
    <asp:ListItem Value="5">Container No</asp:ListItem>
    <asp:ListItem Value="6">BE No</asp:ListItem>
<asp:ListItem Value="7">Jo-Ref No</asp:ListItem>

</asp:DropDownList>                                               
</div>
</div>
    <div class="col-md-4 col-xs-12" id="divCust" runat="server" style="display:none">
<div class="form-group text-label">
<b  >Customer Name</b>
<asp:DropDownList ID="ddlCustomerName" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
</asp:DropDownList>
</div>
</div>
         <div class="col-md-4 col-xs-12" id="divimporter" runat="server" style="display:none">
<div class="form-group text-label">
<b  >Importer</b>
<asp:DropDownList ID="ddlImporter" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
</asp:DropDownList>
</div>
</div>
         <div class="col-md-4 col-xs-12" id="divCHA" runat="server" style="display:none">
<div class="form-group text-label">
<b  >CHA</b>
<asp:DropDownList ID="ddlcha" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
</asp:DropDownList>
</div>
</div>
<div class="col-md-3 col-xs-12" id="divSearch" runat="server" style="display:none">
<div class="form-group text-label">
<b >IGM No</b>
<asp:TextBox ID="txtigmno" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 

        <div class="col-md-3 col-xs-12" id="divContainer" runat="server" style="display:none">
<div class="form-group text-label">
<b >Container No</b>
<asp:TextBox ID="txtContainer" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>

         <div class="col-md-3 col-xs-12" id="divBeno" runat="server" style="display:none">
<div class="form-group text-label">
<b >BE No</b>
<asp:TextBox ID="txtbeno" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>

         <div class="col-md-3 col-xs-12" id="divJoNo" runat="server" style="display:none">
<div class="form-group text-label">
<b >JO-Ref No</b>
<asp:TextBox ID="txtJono" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>



        </ContentTemplate>
    </asp:UpdatePanel>
<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>                                                                                  
</div>
</div>
    
<div class="row">
<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

<asp:TemplateField>
<ItemTemplate>
<a  href='<%# "ImportJobOrder.aspx?JONoView=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "JO NO")).ToString())%>' target="_blank"
Class='btn btn-success btn-xs outline' 
>View</a>
<a  href='<%# "ImportJobOrder.aspx?JONoEdit=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "JO NO")).ToString())%>' target="_blank"
Class='btn btn-info btn-xs outline' 
>Edit</a>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="150px" />
</asp:TemplateField>

<asp:BoundField DataField="Jo No" HeaderText="JO No"></asp:BoundField>                                                   
<asp:BoundField DataField="JO DATE" HeaderText="JO Date"></asp:BoundField>
<asp:BoundField DataField="IGM NO" HeaderText="IGM No"></asp:BoundField>
<asp:BoundField DataField="ITEM NO" HeaderText="Item No"></asp:BoundField>
<asp:BoundField DataField="Customer" HeaderText="Customer"></asp:BoundField>
<asp:BoundField DataField="CHA Name" HeaderText="CHA Name"></asp:BoundField>
<asp:BoundField DataField="Importer Name" HeaderText="Importer"></asp:BoundField>
<asp:BoundField DataField="BE NO" HeaderText="BE No"></asp:BoundField>
<asp:BoundField DataField="BE DATE" HeaderText="BE Date"></asp:BoundField>
<asp:BoundField DataField="BE ADDRESS" HeaderText="BE Address"></asp:BoundField>
<asp:BoundField DataField="PO NO" HeaderText="PO No"></asp:BoundField>    
<asp:BoundField DataField="LOT NO" HeaderText="Lot No"></asp:BoundField>    
<asp:BoundField DataField="REF JO NO" HeaderText="Ref JO No"></asp:BoundField> 
<%--<asp:BoundField DataField="REMARKS" HeaderText="Remarks"></asp:BoundField>--%> 
    <asp:BoundField DataField="CONTAINER NO" HeaderText="Container No"></asp:BoundField>  
    <asp:BoundField DataField="SIZE" HeaderText="SIZE"></asp:BoundField>     
    <asp:BoundField DataField="Cargo type" HeaderText="Cargo Type"></asp:BoundField> 
    <asp:BoundField DataField="Location" HeaderText="From"></asp:BoundField> 
    <asp:BoundField DataField="Location" HeaderText="To"></asp:BoundField> 
    <asp:BoundField DataField="PKGS" HeaderText="PKGS"></asp:BoundField> 
    <asp:BoundField DataField="WEIGHT" HeaderText="WEIGHT"></asp:BoundField> 
    <asp:BoundField DataField="CLASS" HeaderText="CLASS"></asp:BoundField>  
    <asp:BoundField DataField="UNNO" HeaderText="UN NO"></asp:BoundField>  
    <asp:BoundField DataField="DOVALID DATE" HeaderText="Do ValiD Date"></asp:BoundField>  
    <asp:BoundField DataField="CARGO DESCRIPTION" HeaderText="Cargo Description"></asp:BoundField>  
    <asp:BoundField DataField="User Name" HeaderText="Added By"></asp:BoundField>  
    <asp:BoundField DataField="REMARKS" HeaderText="Remarks"></asp:BoundField>                                          

</Columns>
</asp:GridView>
</div>
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
