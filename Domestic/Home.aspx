<%@ Page Title="Domestic | Home" Language="VB" MasterPageFile="~/Domestic/User.Master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="RA_asd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!--Page Title-->
<!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
<form id="form1" >
    <style>
        .center{
            text-align:center
        }
    </style>
<div class="pageheader">
<h3>
<i class="fa fa-home"></i>Home
</h3>
<div class="breadcrumb-wrapper">
<span class="label">You are here:</span>
<ol class="breadcrumb">
<li><a href="Home.aspx">Home </a></li>
<li class="active">Home </li>
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
<%-- <asp:Repeater runat="server" ID="rptDashboard">
     <ItemTemplate>
         </ItemTemplate>
 </asp:Repeater>--%>
    
<div class="row">
    <a href="NOCPendencyRegister.aspx" runat="server">
<div class="col-sm-4 col-xs-6 text-label" title="Click here to view NOC Pending register" style="width:280px">
                  
<div class="col-md-12 col-xs-12 panel " style="height:100px;display: inline-block;background-color:#b8d443;margin-right:32px;padding:initial;border-color:#b8d443;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:#b8d443"  >
</div>--%>   
    
<asp:label class="pull-right" runat="server" id="lblreg" style="color:white;font-size:x-large;margin-top:2px;margin-right:8px" >0</asp:label>
<div class="col-md-12 col-xs-12 pull-left" style="margin-top:-29px;text-align:left;color:white;font-size:large" >
JO-Pendency 
                               
<br /><br />
</div>
               
<br />
            
</div>
           
</div> </a>
    <a href="BondInRegister.aspx" runat="server">
<div class="col-sm-4 col-xs-6 text-label" title="Click here to view Bond In register" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:100px;display: inline-block;background-color:#3489b7;margin-right:32px;padding:initial;border-color:#3489b7;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:rgb(217, 111, 9)"></div>--%>
                                 
<asp:label class="pull-right" runat="server" id="lblbondIn" style="color:white;font-size:x-large;margin-top:2px;margin-right:8px" >0</asp:label>
<div class="col-md-12 col-xs-12 pull-left" style="margin-top:-28px;text-align:left;color:white;font-size:large" >
Loaded-IN
                               
<br /><br />
</div>
            
<br />
            
</div>
           
</div> </a>
    <a href="BondExRegister.aspx" runat="server">
<div class="col-sm-4 col-xs-6 text-label" title="Click here to view Bond Ex register" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:100px;display: inline-block;background-color:#e12980;margin-right:32px;padding:initial;border-color:#e12980;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:rgb(98, 8, 166)"></div>--%>
                                 
<asp:label class="pull-right" runat="server" id="lblbondex" style="color:white;font-size:x-large;margin-top:2px;margin-right:8px" >0</asp:label>

<div class="col-md-12 col-xs-12 pull-left" style="margin-top:-28px;text-align:left;color:white;font-size:large" >
Empty In
                               
<br /><br />
</div>
      
<br />
            
</div>
           
</div> </a>
    <a href="LiveBondRegister.aspx" runat="server">
<div class="col-sm-4 col-xs-6 text-label" title="Click here to view Live Bonds register" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:100px;display: inline-block;background-color:#f19f21;margin-right:32px;padding:initial;border-color:#f19f21;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:rgb(84, 181, 224)"></div>--%>
                                 
<asp:label class="pull-right" runat="server" id="lbllive" style="color:white;font-size:x-large;margin-top:-px;margin-right:8px" >0</asp:label><br />
<div class="col-md-12 col-xs-12 pull-left" style="margin-top:-28px;text-align:left;color:white;font-size:large" >
Loaded Out
<br />

<br /><br />
</div>
     
<br />
            
</div>
           
</div> </a>
<%--</div>
    <div class="row">--%>
    <a href="ExpiredBondRegister.aspx" runat="server">
        <div class="col-sm-4 col-xs-6 text-label" title="Click here to view Live Bonds register" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:100px;display: inline-block;background-color:#e77926;margin-right:32px;padding:initial;border-color:#e77926;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:rgb(0, 165, 213)"></div>--%>
                                 
<asp:label class="pull-right" runat="server" id="lblEx" style="color:white;font-size:x-large;margin-top:-0px;margin-right:12px" >0</asp:label>

<div class="col-md-12 col-xs-12 pull-left" style="margin-top:-28px;text-align:left;color:white;font-size:large" >
Loaded Inventory
                               
<br /><br />
</div>
      
<br />
            
</div>
           
</div></a>
    <a href="#" runat="server">
    <div class="col-sm-5 col-xs-6 text-label" title="Click here to view Cargo Inventory" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:100px;display: inline-block;background-color:#b8d443;margin-right:32px;padding:initial;border-color:#b8d443;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:rgb(81, 156, 65)"></div>--%>
                                 

<div class="col-md-12 col-xs-12 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
 Empty Inventory
    
     <%--<asp:label class="pull-right" runat="server" id="lblopenbond" ToolTip="Last 30 days" style="color:white;font-size:x-large;margin-top:-px;margin-right:100px;"></asp:label>--%> <br />                                     

     <asp:label class="pull-right" runat="server" id="lblopen" style="color:white;font-size:large;margin-right:-px;"  >0</asp:label> 
    <br />
     <asp:label class="pull-right" runat="server" id="lblopensqft" style="color:white;font-size:large;margin-right:-px;margin-top:5px"  >0</asp:label>    
       
<br /><br />
</div>
                
<br />
            
</div>
           
</div></a>

        
    <a href="#" runat="server">
        <div class="col-sm-5 col-xs-6 text-label" title="Click here to view Cargo Inventory" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:100px;display: inline-block;background-color:#3489b7;margin-right:32px;padding:initial;border-color:#3489b7;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:rgb(182, 35, 79)"></div>--%>                               
<div class="col-md-12 col-xs-12 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Domestic Containers
     
     <%--<asp:label class="pull-right" runat="server" id="lblclosebond" ToolTip="Last 30 days" style="color:white;font-size:x-large;margin-top:-px;margin-right:100px;"></asp:label>--%>  <br />          
                     
     <asp:label class="pull-right" runat="server" id="lblclosed" style="color:white;font-size:large;margin-right:-px;"  >0</asp:label>
    <br />
     <asp:label class="pull-right" runat="server" id="lblclosedsqft" style="color:white;font-size:large;margin-right:-px;margin-top:5px"  >0</asp:label>                             
                                 
<br /><br />
</div>
       
<br />
            
</div>
           
</div></a>
        <div class="col-sm-5 col-xs-6 text-label" style="width:280px;display:none">
                  
<div class="col-md-12 col-xs-12 panel " style="color:white;font-size:small;height:100px;display: inline-block;background-color:#e12980;padding:initial;border-color:#e12980;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:rgb(182, 35, 79)"></div>--%>                               
<%--<div class="col-md-12 col-xs-12 pull-left" style="color:white;font-size:large" ></div>--%>    
<asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="false" style="margin-top:10px;margin-left:10px" BorderColor="#e12980" ID="grdbalvalduty">
    <Columns>
        <asp:BoundField HeaderText="Insurance" DataField="Name" ControlStyle-BorderStyle="None" ItemStyle-Width="60px" />
<asp:TemplateField HeaderText="Balance" HeaderStyle-CssClass="center">
<ItemTemplate>
    <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="center" Width="90px"  />
</asp:TemplateField>
        <%--<asp:BoundField HeaderText="Balance" DataField="Balance" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px" />--%>
        <asp:BoundField HeaderText="Expiry Date" DataField="Expiry Date" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px" />

    </Columns>
</asp:GridView>  

       
<br />
            
</div>
           
</div>
    <a href="Generatedinvoice.aspx" runat="server">
    <div class="col-sm-4 col-xs-6 text-label" title="Click here to view List of Pending Docs" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:100px;display: inline-block;background-color:#f19f21;margin-right:32px;padding:initial;border-color:#f19f21;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:rgb(217, 111, 9)"></div>--%>
                                 
<asp:label class="pull-right" runat="server" id="lblPendingDocs" style="color:white;font-size:x-large;margin-top:2px;margin-right:8px" >0</asp:label>
<div class="col-md-12 col-xs-12 pull-left" style="margin-top:-28px;text-align:left;color:white;font-size:large" >
Domestic Available Containers
                               
<br /><br />
</div>
            
<br />
            
</div>
           
</div> </a>
    <a href="#" runat="server">
    <div class="col-sm-5 col-xs-6 text-label" title="Click here to view Cargo Inventory" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:100px;display: inline-block;background-color:#b8d443;margin-right:32px;padding:initial;border-color:#b8d443;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:rgb(81, 156, 65)"></div>--%>
                                 

<div class="col-md-12 col-xs-12 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Destuff
    
     <%--<asp:label class="pull-right" runat="server" id="lblopenbond" ToolTip="Last 30 days" style="color:white;font-size:x-large;margin-top:-px;margin-right:100px;"></asp:label>--%> <br />                                     

     <asp:label class="pull-right" runat="server" id="Label1" style="color:white;font-size:large;margin-right:-px;"  >0</asp:label> 
    <br />
     <%--<asp:label class="pull-right" runat="server" id="Label2" style="color:white;font-size:large;margin-right:-px;margin-top:5px"  >0</asp:label>--%>    
       
<br /><br />
</div>
                
<br />
            
</div>
           
</div></a>
    <a href="#" runat="server">
    <div class="col-sm-5 col-xs-6 text-label" title="Click here to view Cargo Inventory" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:100px;display: inline-block;background-color:#e77926;margin-right:32px;padding:initial;border-color:#e77926;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:rgb(81, 156, 65)"></div>--%>
                                 

<div class="col-md-12 col-xs-12 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Total Dispatch Cargo
    
     <%--<asp:label class="pull-right" runat="server" id="lblopenbond" ToolTip="Last 30 days" style="color:white;font-size:x-large;margin-top:-px;margin-right:100px;"></asp:label>--%> <br />                                     

     <asp:label class="pull-right" runat="server" id="Label2" style="color:white;font-size:large;margin-right:-px;"  >0</asp:label> 
    <br />
     <%--<asp:label class="pull-right" runat="server" id="Label2" style="color:white;font-size:large;margin-right:-px;margin-top:5px"  >0</asp:label>--%>    
       
<br /><br />
</div>
                
<br />
            
</div>
           
</div></a>
    <a href="#" runat="server">
    <div class="col-sm-5 col-xs-6 text-label" title="Click here to view Cargo Inventory" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:100px;display: inline-block;background-color:#3489b7;margin-right:32px;padding:initial;border-color:#3489b7;">
<%--<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:100px; background-color:rgb(81, 156, 65)"></div>--%>
                                 

<div class="col-md-12 col-xs-12 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Total balance Cargo
    
     <%--<asp:label class="pull-right" runat="server" id="lblopenbond" ToolTip="Last 30 days" style="color:white;font-size:x-large;margin-top:-px;margin-right:100px;"></asp:label>--%> <br />                                     

     <asp:label class="pull-right" runat="server" id="Label3" style="color:white;font-size:large;margin-right:-px;"  >0</asp:label> 
    <br />
     <%--<asp:label class="pull-right" runat="server" id="Label2" style="color:white;font-size:large;margin-right:-px;margin-top:5px"  >0</asp:label>--%>    
       
<br /><br />
</div>
                
<br />
            
</div>
           
</div></a>
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

