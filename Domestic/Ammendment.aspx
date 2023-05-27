<%@ Page Title="Domestic | Admin Amendments" Language="VB" MasterPageFile="~/Domestic/User.Master" AutoEventWireup="false" CodeFile="Ammendment.aspx.vb" Inherits="RA_asd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!--Page Title-->
<!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
<form id="form1" >
<div class="pageheader">
<h3>
<i class="fa fa-home"></i>Admin Amendments
</h3>
<div class="breadcrumb-wrapper">
 
<ol class="breadcrumb">
 
</ol>
</div>
</div>
<!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
<!--End page title-->
<!--Page content-->
<!--===================================================-->
<div id="page-content">
<!--Widget-4 -->
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
 
<div class="row">

<div class="col-md-4 col-xs-6 text-label" runat="server" id="divCancelNOC" style="width:280px;vertical-align:middle;">            
<div class="col-md-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(231, 84, 90);margin-right:32px;padding:initial;border-color:rgb(231, 84, 90);">
                                 
<div class="col-md-9 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
<a href="ModifyCntrInDate.aspx" style="color:white;" onclick='return pop("ModifyCntrInDate.aspx",900,550,70,300);'> Modify Gate In Date
</a>
                               
<br /><br />
</div>             
<br />           
</div>
           
</div> 
    
<div class="col-md-4 col-xs-6 text-label" runat="server" id="divCancelBondIN" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(254, 106, 0);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
                                 
<div class="col-md-9 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
 
<a href="ModifyStuffingDate.aspx"  style="color:white" onclick='return pop("ModifyStuffingDate.aspx",900,550,70,300);'> Modify Destuff Date
</a>                               
<br /><br />
</div>
            
<br />
            
</div>
           
</div> 
<div class="col-md-4 col-xs-6 text-label" runat="server" id="divCancelBondEx" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(127, 12, 217);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
                                 

<div class="col-md-9 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
 
<a href="ModifyLoadedDate.aspx"  style="color:white" onclick='return pop("ModifyLoadedDate.aspx",900,550,70,300);'> Modify Loaded Date
</a>                               
<br /><br />
</div>
      
<br />
            
</div>
           
</div> 
 
    
 

</div>
<div class="row">
</div>
<div class="row">
</div>
        
</div>
<!--===================================================-->
<!--End page content-->
<script src="../js/jQuery.min.js" type="text/javascript"></script>
<script type="text/javascript">
$(document).ready(function () {
$('[data-toggle="tooltip"]').tooltip();
});

       
</script>
</form>
</asp:Content>

