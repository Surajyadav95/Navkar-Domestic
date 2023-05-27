<%@ Page Title="Domestic | List of Invoice Final" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ListofInvoiceDetailsFinal.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Domestic | List of Invoice Final .</title>
</head>
    <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:500px;
            overflow:auto
        }
        .nav-tabs>li.active>a, .nav-tabs>li.active>a:focus, .nav-tabs>li.active>a:hover{
            background-color:orange
        }
    </style>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>List of Invoice
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
<div class="panel-body" >
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
                           
                                                
<div class="row">
<div class="col-md-6 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy"  runat="server" TextMode="DateTimeLocal" Class="  form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy"  runat="server" TextMode="DateTimeLocal" Class="  form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
     <div class="col-md-2 col-xs-12" style="display:none;"  id="divInvoiceno" runat="server">
<div class="form-group text-label">
<b >Invoice No</b>
<asp:TextBox ID="txtAssessno" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search Assess No"
runat="server"   ></asp:TextBox>
</div>
</div>

    <div class="col-md-3 col-xs-12" style="display:none;"  id="divWorkYear" runat="server">
<div class="form-group text-label">
<b >Work Year</b>
<asp:TextBox ID="TxtWorkYear" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search Work Year"
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
Customer
<asp:DropDownList ID="ddlCustomer" runat="server" class="form-control text-label">

</asp:DropDownList>
                                               
</div>
</div>
    <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
Invoice No
<asp:TextBox runat="server" ID="txtInvoiceSearch" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                               
</div>
</div>
<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
                                              
                                       
</div>
    <div class="col-md-1  col-xs- 12">
<div class="form-group pull-left" style="padding-top:20px" >
<asp:Button ID="btnGeneratePDF" runat="server" 
class="btn btn-purple btn-sm outline" Text="Generate PDF"  OnClientClick="return confirm('Are you sure to Generate PDF?')" OnClick="btnGeneratePDF_Click" ></asp:Button>
</div>              
</div>                                           
</div>


                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="grdcontainer" />
    </Triggers>
<ContentTemplate>

<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="true" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true"  >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

<asp:TemplateField>
<ItemTemplate>
                                                          
<a  href='<%# "../Report_Domestic/DomesticInvoicePrintFinal.aspx?AssessNo=" & (DataBinder.Eval(Container.DataItem, "ASSESSNO")).ToString() & "&WorkYear=" & (DataBinder.Eval(Container.DataItem, "WorkYear")).ToString()%>' target="_blank"
Class='btn btn-primary btn-xs outline' 
>Print</a>
<asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Cancel" OnClick="lnkCancel_Click"
    OnClientClick="return confirm('Are you sure to Cancel?')"
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "InvoiceNo")%>' runat="server"></asp:LinkButton>                                          
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="150px" />
</asp:TemplateField>
   
<%--<asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No"></asp:BoundField>
<asp:BoundField DataField="AssessDate" HeaderText="Date"></asp:BoundField>
<asp:BoundField DataField="AGNAME" HeaderText="Customer"></asp:BoundField>
<asp:BoundField DataField="GrandTotal" HeaderText="Grand Total"></asp:BoundField>--%>
    <asp:TemplateField HeaderText="Invoice No" Visible="false" HeaderStyle-CssClass="text-center">
<ItemTemplate>

<asp:Label runat="server" ID="lblassessNo" Visible="false" Text='<%#Eval("ASSESSNO")%>'></asp:Label>
   
</ItemTemplate>

<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
    <asp:TemplateField Visible="false" HeaderText="Work Year"   HeaderStyle-CssClass="text-center">
<ItemTemplate>
<asp:Label runat="server" ID="lblworkyear" Visible="false" Text='<%#Eval("WorkYear")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
</Columns>

</asp:GridView>
</div>
</div>
</div>
        <asp:Panel ID="pnlPerson" Visible="false" runat="server" font-family="Segoe UI">
        <rsweb:ReportViewer ID="ReportViewer1" Width="1000px" Height="600px" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Depo\Report_Epic\HandlingStoragePrint.rdlc" >
        </LocalReport>
        </rsweb:ReportViewer>
        </asp:Panel> 
                     
</ContentTemplate>
</asp:UpdatePanel>
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
     <div class="modal fade control-label" id="myModalforupdate2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="Button1" />
    </Triggers>
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblsession"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info btn-block" id="Button1" data-dismiss="modal" runat="server" onserverclick="btnSave_Click"  aria-hidden="true">
Ok 
</button>

</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
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
     <script>
          function OpenCancelInvoice() {
              var txtAssessno = document.getElementById('<%= txtAssessno.ClientID%>').value;
             var TxtWorkYear = document.getElementById('<%= TxtWorkYear.ClientID%>').value;

             var url = "InvoiceCancel.aspx?AssessNo=" + txtAssessno + "&WorkYear=" + TxtWorkYear
             popup = window.open(url, "Popup", "top=100,left=400,width=700,height=215");
             popup.focus();
         }
    </script> 
</asp:Content>
