<%@ Page Title="Domestic |Container Hold Entry" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DomesticContainerHoldEntry.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic |Container Hold Entry</title>
       
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
<i class="glyphicon glyphicon-transfer"></i>Container Hold Entry
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
Container Hold Entry
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
 
    <div class="row">
              
     <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtcontainer" Style="text-transform:uppercase" MaxLength="11" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>
    
        <div class="col-sm-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnShow" class="btn btn-primary btn-sm outline " runat="server"   OnClick="btnShow_Click"
Text="Show"  OnClientClick="return ValidationShow()"    />
</div>
                                              
                                      
</div>

         <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Jo No</b>
<asp:TextBox ID="txtjoNo" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Jo No"
runat="server"   ></asp:TextBox>
</div>
                       </div>
   
          <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Size</b>
<asp:TextBox ID="txtSize" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Size"
runat="server"   ></asp:TextBox>
</div>
                       </div>

    

         
        </div>

    <div class="row">
         
             <div class="col-md-6 col-xs-12" >
            <div class="form-group text-label">

<b>Line Name</b>
<asp:TextBox ID="txtlineName" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Line Name"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        
        
          <div class="col-md-4 col-xs-12" >
            <div class="form-group text-label">

<b>Hold Reason</b>
<asp:DropDownList ID="ddlholdreason"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>
      
       
        </div>

    <div class="row">

                               
<div class="col-md-3  col-xs-12">                                      
<div class="form-group text-label">
<b>In Date</b>                                         
<asp:TextBox ID="txtInDate"  placeholder="yyyy-mm-dd "   runat="server" ReadOnly="true"   Class="form-control text-label"></asp:TextBox>

</div>
</div>

         <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >Hold Date</b>
                    <asp:TextBox ID="txtHoldDate" placeholder="dd-MM-yyyy" ReadOnly="true" TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        

                    </div>
      </div>
    <div class="row">
        <div class="col-md-6 col-xs-12" >
            <div class="form-group text-label">

<b>Remarks</b>
<asp:TextBox ID="txtremarks" Style="text-transform:uppercase" TextMode="MultiLine" class="form-control text-label"  placeholder="Remarks"
runat="server"   ></asp:TextBox>
</div>
                       </div>
    </div>
    <div class="row">
<div class="col-sm-5 col-xs-4">
<div class="form-group" style="padding-top:8px">
<asp:Button ID="btnputonhold" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnputonhold_Click"
Text="Put On Hold" OnClientClick="return ValidationSavep()"   />
    <asp:Button ID="btnclearfrom" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnclearfrom_Click"  
Text="Clear From Hold" OnClientClick="return ValidationSaveH()"   />
    <a href="DomesticContainerHoldEntry.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
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
        

<asp:BoundField DataField="JO No" HeaderText="JO No"></asp:BoundField>
 <asp:BoundField DataField="Container No" HeaderText="Container No"></asp:BoundField>                                                 
<asp:BoundField DataField="Hold Reason" HeaderText="Hold Reason"></asp:BoundField>
   

     


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
                   
<a href="DomesticContainerHoldEntry.aspx" class="btn btn-info btn-block">OK</a>
                                
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
        function ValidationShow() {


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
   <script type="text/javascript">
       function ValidationSavep() {
            
     
    var txtremarks = document.getElementById('<%= txtremarks.ClientID%>').value;

var blResult = Boolean;
blResult = true;
              
 

if (txtremarks == "") {
document.getElementById('<%= txtremarks.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
    }
          
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>

    <script type="text/javascript">
        function ValidationSaveH() {


            var txtremarks = document.getElementById('<%= txtremarks.ClientID%>').value;

           var blResult = Boolean;
           blResult = true;



           if (txtremarks == "") {
               document.getElementById('<%= txtremarks.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;
}

    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    return blResult;
}
</script>
   
</asp:Content>
