<%@ Page Title="Domestic |Destuff Entry" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DestuffEntry.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic |Destuff Entry</title>
       
</head>
       <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:250px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i>Destuff Entry
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
<div class="col-md-3 pull-md-left sidebar" style="padding-top:12px;">
 
                           
</div>
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Destuff Entry
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
 <div class="row">

         <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Entry ID</b>
<asp:TextBox ID="txtvendorID" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
 
      <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >Entry Date</b>
                    <asp:TextBox ID="txtGateInDate" placeholder="dd-MM-yyyy" ReadOnly="true" TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>
</div>
     
    <div class="row">
         <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Search By</b>
<asp:DropDownList ID="ddlSearchby"   Style="text-transform: uppercase;"  OnSelectedIndexChanged="ddlSearchby_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control text-label">
    <asp:ListItem Value="0">--Select--</asp:ListItem> 
<asp:ListItem Value="Container No">Container No</asp:ListItem>
    <asp:ListItem Value="IGM-Item No">IGM-Item No</asp:ListItem>
</asp:DropDownList>
</div>
</div>
        
             <div class="col-md-3 col-xs-12" id="divContainer" runat="server" style="display:none">
<div class="form-group text-label">
<b >Container No</b>
<asp:TextBox ID="txtContainer" Style="text-transform:uppercase" MaxLength="11" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>

</div>

        <div class="col-md-3 col-xs-12" id="divSearch" runat="server" style="display:none">
<div class="form-group text-label">
<b >IGM No</b>
<asp:TextBox ID="txtigmno" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 

         <div class="col-md-3 col-xs-12" id="divItem" runat="server" style="display:none">
<div class="form-group text-label">
<b >Item No</b>
<asp:TextBox ID="txtItemNo" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 
          <div class="col-sm-1 col-xs-6" id="divDomestic" runat="server" style="display:none">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="LinkButton2" ControlStyle-CssClass='btn btn-primary btn-sm'  runat="server"
OnClientClick="return OpenNOCList();"><i class=" fa fa-search" aria-hidden="true"></i></asp:LinkButton>  
</div>                                  
</div>
         <div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnsearch" class="btn btn-primary btn-sm outline " runat="server"

Text="Search" OnClick="btnsearch_Click" />
</div>                                                                                  
</div>
        </div>

     
 
<asp:Button ID="btnDomestic" runat="server" Text="Call Button Item Click" OnClick="btnDomestic_Click" style="display:none"  />
   
                              


 
    


     
 
      </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div class="row">
                    <div class="col-lg-12 col-xs-12 text-label " >
                        <asp:Label ID="Label1" Visible="false" runat="server" Text=""></asp:Label>
                    <div class="table-responsive scrolling-table-container1">
                    <asp:GridView ID="grdinvoices" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                    AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnRowDataBound="grdinvoices_RowDataBound"  PageSize="6">
                    <Columns>
                       
                        <asp:TemplateField HeaderText="Jo No" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblJoNo" runat="server" Text='<%# Eval("JO NO")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                       
                         <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblContainerNo" runat="server" Text='<%# Eval("CONTAINERNO")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblSize" runat="server" Text='<%# Eval("SIZE")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cargo Type" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblCargoType" runat="server" Text='<%# Eval("Cargotype")%>'></asp:Label>
                                 <asp:Label id="lblCargoTypeID" Visible="false" runat="server" Text='<%# Eval("CARGOTYPEID")%>'></asp:Label>
                                 
                             </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Weight" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblWeight" runat="server" Text='<%# Eval("WEIGHT")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="PKGS" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblPKGS" runat="server" Text='<%# Eval("PKGS")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Weight" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                 <asp:TextBox runat="server" OnTextChanged="txtWeight_TextChanged" AutoPostBack="true" onkeypress="return ValidateQty();" ID="txtWeight"  Width="100px" Class="form-control" Text='<%# Eval("Destuff Weight")%>'></asp:TextBox>
                             </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />    
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Destuff PKGS" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                 <asp:TextBox runat="server" onkeypress="return ValidateQty();" ID="txtDestuffPkgs" OnTextChanged="txtDestuffPkgs_TextChanged" AutoPostBack="true"  Width="100px" Class="form-control" Text='<%# Eval("Destuf PKGS")%>'></asp:TextBox>
                             </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />    
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Short PKGS" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                 <asp:TextBox runat="server"  onkeypress="return ValidateQty();" ID="txtshortpkgs" OnTextChanged="txtshortpkgs_TextChanged" AutoPostBack="true"  Width="100px" Class="form-control" Text='<%# Eval("Short PKGS")%>'></asp:TextBox>
                             </ItemTemplate> 
                             <ItemStyle HorizontalAlign="Right" />                           
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Excess PKGS" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                 <asp:TextBox runat="server" onkeypress="return ValidateQty();" ID="txtExcesspkgs" OnTextChanged="txtExcesspkgs_TextChanged" AutoPostBack="true"  Width="100px" Class="form-control" Text='<%# Eval("Excess PKGS")%>'></asp:TextBox>
                             </ItemTemplate> 
                             <ItemStyle HorizontalAlign="Right" />                           
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Equipment" HeaderStyle-CssClass="header-center">
    <ItemTemplate>        
        <asp:Panel runat="server" Enabled="True">
        <asp:Label ID="lblEquipment" runat="server" text='<%#Eval("EQUIPMENT")%>'   Visible = "false" />
        <asp:DropDownList ID="ddlEquipment" runat="server" class="form-control " width="200px" >
        </asp:DropDownList>
            </asp:Panel>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
                           <asp:TemplateField HeaderText="No Of Labour" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                 <asp:TextBox runat="server" ID="txtNoOfLabour"   Width="80px" Class="form-control" Text='<%# Eval("No Of Labour")%>'></asp:TextBox>
                             </ItemTemplate> 
                             <ItemStyle HorizontalAlign="Right" />                           
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                 <asp:TextBox runat="server" ID="txtRemarks"  TextMode="MultiLine" Width="150px" Class="form-control" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                             </ItemTemplate> 
                             <ItemStyle HorizontalAlign="Right" />                           
                        </asp:TemplateField>
                    </Columns>

                    </asp:GridView>
                    </div>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
<asp:Label ID="LblVendorID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblVendorName" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblDriverNo" Visible="false" runat="server" Text=""></asp:Label>
    <div class="row">
         <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Vendor Name</b>
<asp:DropDownList ID="ddlvendor"   Style="text-transform: uppercase;"   runat="server" class="form-control text-label">
 
</asp:DropDownList>
</div>
</div>
    </div>
    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:8px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " OnClick="btnSave_Click" runat="server"  
Text="Save" OnClientClick="return ValidationSave()"   />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:0px;">
<div class="form-group" style="padding-top:8px">
                           
<a href="DestuffEntry.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a> 
                              
</div>                                            
                                      
</div>
 </div>
     
    </asp:Panel>                        
</div>
</div>



<asp:Label ID="lblcode" Visible="false" runat="server" Text=""></asp:Label>
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
                   
<a href="DestuffEntry.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
               
</fieldset>

</div>
</div>
                               
</div>
                           
                          
                     
                       
                       
</div>
                 
              
</div>
       
         
</div>
    <script type="text/javascript">
        function ValidateQty() {
            //alert('hii')
            if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 46)
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
   <%--<script type="text/javascript">
function ValidationSave() {
            
     
    var txtcontainer = document.getElementById('<%= txtcontainer.ClientID%>').value;

var blResult = Boolean;
blResult = true;
              
 

if (txtcontainer == "") {
document.getElementById('<%= txtcontainer.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
    }
          
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>--%>

    <script type="text/javascript">
        var popup;
        function gstsearch() {

            var url = "LoadedSearch.aspx"

            popup = window.open(url, "Popup", "width=710,height=555");
            popup.focus();

        }
</script>
   
    <script type="text/javascript">
        var popup;
        function OpenNOCList() {

            var popup;
            var url = "DomesticDestuffSearch.aspx"
            //window.open(url);
            popup = window.open(url, "Popup", "width=800,height=550");
            popup.focus();
        }

</script>
</asp:Content>
