<%@ Page Title="Domestic | Train Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="TrainSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Domestic |  Train Summary .</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Train Summary  
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
                 
<asp:UpdatePanel ID="updatepanel2" runat="server" UpdateMode="Conditional"> 
<ContentTemplate>
                                    
                                                
<div class="row">
<div class="col-md-4 col-xs-12" >
<div class="form-group text-label">
<b >Train No</b>
<asp:TextBox ID="txtTrainNo" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Enter Train No"
runat="server"></asp:TextBox>
</div>
</div> 

<div class="col-sm-3" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server"
OnClick="btnSave_Click" Text="Search" />

    <asp:Button ID="btnCancel" class="btn btn-danger " runat="server"
OnClick="lnkcancel_Click" Text="Cancel" />
</div>
                                              
                                      
</div>
                                               
</div>
</ContentTemplate>
</asp:UpdatePanel>

                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
   
<asp:BoundField DataField="Train No" HeaderText="Train No"></asp:BoundField>
<asp:BoundField DataField="Wagon No" HeaderText="Wagon No"></asp:BoundField>
<asp:BoundField DataField="Wagon Type" HeaderText="Wagon Type" ></asp:BoundField>
<asp:BoundField DataField="Capacity" HeaderText="Capacity" ></asp:BoundField>
<asp:BoundField DataField="Prepared By" HeaderText="Prepared By"></asp:BoundField>
<asp:BoundField DataField="RR No" HeaderText="RR No"></asp:BoundField>

</Columns>
</asp:GridView>
</div>
</div>
</div>
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
