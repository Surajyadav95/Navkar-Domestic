<%@ Page Title="Domestic |Own Container Master" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="OwnContainerMaster.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic |Own Container Master</title>
       
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
<i class="glyphicon glyphicon-transfer"></i>Own Container Master
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
 Own Container Master
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
 <div class="row">

         <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >ID</b>
<asp:TextBox ID="txtvendorID" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
 
</div>
    <div class="row">
              
     <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtcontainer" Style="text-transform:uppercase" MaxLength="11" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>
    
   
           <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Size</b>
<asp:DropDownList ID="ddlSize"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>

          <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Type</b>
<asp:DropDownList ID="ddltype"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
        </div>

    <div class="row">
          <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Condition</b>
<asp:DropDownList ID="ddlcondtion"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
      
          <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Status</b>
<asp:DropDownList ID="ddlstatus"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
        
          <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Location</b>
<asp:DropDownList ID="ddllocation"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
         <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >ISO Code</b>
<asp:DropDownList ID="ddlisocode"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>

       
        </div>

    <div class="row">
          <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Tare Wt</b>
<asp:TextBox ID="txtTarewt" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Tare Wt"
runat="server"   ></asp:TextBox>
</div>
</div>
         <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Permissible Wt</b>
<asp:TextBox ID="txtPermissiblewt" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Permissible Wt"
runat="server"   ></asp:TextBox>
</div>
</div>

         <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Own/Lease</b>
<asp:DropDownList ID="ddlOwnedBy"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
        <asp:ListItem Value="0">--select--</asp:ListItem> 
<asp:ListItem Value="OWN">OWN</asp:ListItem>
<asp:ListItem Value="Lease">Lease</asp:ListItem>

</asp:DropDownList>
</div>
</div>
 <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >Purchase Date</b>
                    <asp:TextBox ID="txtPurchaseDate" placeholder="dd-MM-yyyy" TextMode="Date"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>
 </div>

    <div class="row">
         <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Leaser Name</b>
<asp:TextBox ID="txtLeaserName" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Leaser Name"
runat="server"   ></asp:TextBox>
</div>
</div>
          <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Lease From</b>
<asp:TextBox ID="txtleasefrom" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Lease From"
runat="server"   ></asp:TextBox>
</div>
</div>

          <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Lease Till</b>
<asp:TextBox ID="txtleaseTill" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Lease Till"
runat="server"   ></asp:TextBox>
</div>
</div>

        </div>
    <div class="row">


<div class="col-md-6 col-xs-12">
<div class="form-group text-label" style="padding-top: 10px;">
<asp:CheckBox ID="chkisActive" runat="server" Checked="true" />
<asp:HiddenField ID="hdlocation" runat="server" Value="0" />
<asp:Label ID="IsActiveLabel" runat="server" AssociatedControlID="chkisActive" CssClass="inline">Is Active?</asp:Label>
</div>
</div>
</div>
 
      </div>


<asp:Label ID="LblVendorID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblVendorName" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblDriverNo" Visible="false" runat="server" Text=""></asp:Label>

    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:8px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnSave_Click"
Text="Save" OnClientClick="return ValidationSave()"   />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:0px;">
<div class="form-group" style="padding-top:8px">
                           
<a href="OwnContainerMaster.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a> 
                              
</div>                                            
                                      
</div>
 </div>
    <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
 
     <div class="row">
        <div class="col-sm-4 col-xs-12 ">
<div class="form-group text-label">
<b  >Search</b>
<asp:TextBox ID="txtsearche" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>

        <div class="col-sm-1 col-xs-2 pull-left">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnsearch" class="btn btn-primary btn-sm outline  " runat="server" OnClick="btnsearch_Click"  
Text="Search"  />
</div>
                                              
                                      
</div>
                         
</div>

   


<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!"  ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
        

<asp:BoundField DataField="ContainerNo" HeaderText="Container No"></asp:BoundField>
 <asp:BoundField DataField="Size" HeaderText="Size"></asp:BoundField>                                                 
<asp:BoundField DataField="ContainerType" HeaderText="Type"></asp:BoundField>
    <asp:BoundField DataField="Name" HeaderText="Condition"></asp:BoundField>
    <asp:BoundField DataField="Location" HeaderText="Location"></asp:BoundField>
    <asp:BoundField DataField="UserName" HeaderText="Added By"></asp:BoundField>
    <%--<asp:BoundField DataField="statusdesc" HeaderText="Location"></asp:BoundField>--%>

     


</Columns>

</asp:GridView>
</div>
</div>


</div>
       </ContentTemplate>
</asp:UpdatePanel>
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
                   
<a href="OwnContainerMaster.aspx" class="btn btn-info btn-block">OK</a>
                                
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
</script>
   
</asp:Content>
