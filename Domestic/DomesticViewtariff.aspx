<%@ Page Title="Domestic | View Tariff" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticViewtariff.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Domestic | Domestic View Tariff</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i> Domestic View Tariff Details  
</h3>
           
</div>
     
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="grdcontainer" />
    </Triggers>
<ContentTemplate>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body">
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
                 
<asp:UpdatePanel ID="updatepanel2" runat="server" UpdateMode="Conditional"> 
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
        <asp:PostBackTrigger ControlID="btnSearch" />

    </Triggers>
<ContentTemplate>
                                    
                                                
<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b>Tariff ID</b>
<asp:DropDownList ID="ddltraiff"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
            <asp:ListItem Value="0">--Select--</asp:ListItem>  
</asp:DropDownList>
</div>
</div>

  
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b>Invoice Type</b>
<asp:DropDownList ID="ddlbondType"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
        <asp:ListItem Value="0">--Select--</asp:ListItem>
    <asp:ListItem Value="General">General</asp:ListItem>
    <asp:ListItem Value="Other">Other</asp:ListItem>
<asp:ListItem Value="Transport">Transport</asp:ListItem>
    <asp:ListItem Value="General1">General 1</asp:ListItem>
    <asp:ListItem Value="Import">Import</asp:ListItem>
</asp:DropDownList>
</div>
</div>

<div class="col-sm-3" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSearch" class="btn btn-primary " runat="server"
OnClick="btnSearch_Click" 
Text="Show" />
    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
</div>                                                                                   
</div>

         <div class="col-md-4 col-xs-12" style="display:none" >
<div class="form-group text-label">
<b >Slab ID</b>
<asp:TextBox ID="txtSlabID" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>                                       
                                               
</div>
</ContentTemplate>
</asp:UpdatePanel>

                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:5px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
                                                
<asp:BoundField DataField="tariffID" HeaderText="Tariff ID"></asp:BoundField>
<asp:BoundField DataField="TariffDescription" HeaderText="Tariff Desc."></asp:BoundField>
<asp:BoundField DataField="bondtype" HeaderText="Bond Type"></asp:BoundField>
<asp:BoundField DataField="AccountName" HeaderText="Account Name"></asp:BoundField>
    <asp:BoundField DataField="ChargesBased" HeaderText="Charges Based On"></asp:BoundField>
 <asp:TemplateField HeaderText="Slab ID">
<ItemTemplate>
    <asp:LinkButton ID="lnkselect" OnClick="lnkselect_Click"  Text='<%#Eval("SlabID")%>'   CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SlabID")%>' runat="server"></asp:LinkButton>
 
</ItemTemplate>
</asp:TemplateField> 
<asp:BoundField DataField="isSTax" HeaderText="Is STax"></asp:BoundField>
<asp:BoundField DataField="fixedamt" HeaderText="Fixed Amount"></asp:BoundField>
<asp:BoundField DataField="effectivefrom" HeaderText="Effective From"></asp:BoundField>
<asp:BoundField DataField="effectiveupto" HeaderText="Effective Upto"></asp:BoundField>
<asp:BoundField DataField="Size" HeaderText="Size"></asp:BoundField>
    <asp:BoundField DataField="Location" HeaderText="Location"></asp:BoundField>
    <asp:BoundField DataField="Criteria" HeaderText="Criteria"></asp:BoundField>
    <asp:BoundField DataField="LorD" HeaderText="Loaded/Destuff"></asp:BoundField>

</Columns>

</asp:GridView>
</div>
</div>
</div>

</div>

                         


</div>
</div>   
                                 
                               
</div>
</div>
                          
                     
                       
                       
</div>
                 
</ContentTemplate>
</asp:UpdatePanel>
</div>
<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
>
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
<asp:Button ID="btntest" runat="server"
class="btn btn-info btn-block" Text="OK" data-dismiss="modal" aria-hidden="true" OnClientClick="populateCalendarTextbox()"></asp:Button>
</div>
</div>

                      
</div>
</div>
</div>
  <script type="text/javascript">
      var popup;
      function OpenSlabID() {
          var txtslab = document.getElementById('<%= txtSlabID.ClientID%>').value;

               var url = "../Domestic/SlabList.aspx?SlabID=" + txtslab
               popup = window.open(url, "Popup", "width=800,height=550");
               popup.focus();
               //window.open(url);

           }

</script>
</asp:Content>
