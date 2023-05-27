<%@ Page Title="Domestic | Generate Invoice" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="GenerateInvoice.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <head>
<title>Domestic | Generate Invoice</title>       
</head>
     <style>
        .scrolling-table-container{
            height:210px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>  Generate Invoice
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>


<div class="panel-body">

<div class="row">
                                         
<div class="col-md-8 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
           
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Invoice No</b>
<asp:TextBox ID="txtInvoiceNo" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
    <div class="col-sm-4 col-xs-12" style="display:none">
<asp:TextBox runat="server" ID="txtassessno" ></asp:TextBox>
<asp:TextBox runat="server" ID="txtworkyear" ></asp:TextBox>
        <asp:TextBox runat="server" ID="txtInvoiceType" ></asp:TextBox>
</div>
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Invoice Date</b>
<asp:TextBox ID="txtInvoiceDate" Style="text-transform:uppercase" class="form-control text-label" TextMode="DateTimeLocal"
runat="server"   ></asp:TextBox>
    <asp:Label runat="server" ID="lblValidDate" Visible="false"></asp:Label>
</div>
</div>
    <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Valid Upto Date</b>
<asp:TextBox ID="txtValidUptoDate" Style="text-transform:uppercase" class="form-control text-label" TextMode="DateTimeLocal"
runat="server"   ></asp:TextBox>
</div>
</div>
    </div>
    <div class="row">
    <div class="col-md-3 col-xs-12">
<b>Invoice Type</b>
<asp:DropDownList ID="ddlInvoiceType" Style="text-transform: uppercase;" runat="server" onchange="return InvoiceTypeChange();" class="form-control text-label">
<asp:ListItem Value="General">General</asp:ListItem>
    <asp:ListItem Value="General 1">General 1</asp:ListItem>
    <asp:ListItem Value="Import">Import</asp:ListItem>
<asp:ListItem Value="Transport">Transport</asp:ListItem>
</asp:DropDownList>
</div>
    <div class="col-md-3 col-xs-12">
<b>Criteria</b>
<asp:DropDownList ID="ddlCriteria" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
<asp:ListItem Value="Cargo">Cargo</asp:ListItem>
<asp:ListItem Value="Coil">Coil</asp:ListItem>
</asp:DropDownList>
</div>
</div>
    <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnGSTSearch_Click" />
    <div class="row">
<div class="col-sm-5 col-xs-12">
<div class="form-group text-label">
<b >GST In Number</b>
<asp:TextBox  ID="txtgstin" AutoPostBack="false" MaxLength="16" Style="text-transform: uppercase;" placeholder="GST Number" runat="server" class="form-control text-label">                                      
</asp:TextBox>
<asp:Label runat="server" ID="lblpartyid" Visible="false"></asp:Label>
</div>
</div>

<div class="col-sm-1 col-xs-6">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return gstsearch();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>
<div class="col-sm-6 col-xs-12" style="padding-top:20px">
<div class="form-group text-label">
<asp:TextBox  ID="txtgstname" ReadOnly="true" AutoPostBack="false" placeholder="GST Name" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
<asp:Label runat="server" Visible="false" Id="lblstatecode"></asp:Label>
</div>

    <div class="row" id="divIGMItem" runat="server" style="display:none">
    <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >IGM No</b>
<asp:TextBox ID="txtIGMNo" Style="text-transform:uppercase" class="form-control text-label" runat="server" ></asp:TextBox>
</div>
</div>
    <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Item No</b>
<asp:TextBox ID="txtItemNo" Style="text-transform:uppercase" class="form-control text-label" runat="server" ></asp:TextBox>
</div>
</div>
    <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtContainerNo" Style="text-transform:uppercase" class="form-control text-label" runat="server" ></asp:TextBox>
</div>
</div>
</div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional"><ContentTemplate>
<div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Customer</b>
<asp:DropDownList ID="ddlCustomer" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>
    <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >RR No</b>
<asp:TextBox ID="txtRefNo" Style="text-transform:uppercase" OnTextChanged="txtRefNo_TextChanged" AutoPostBack="true" class="form-control text-label" runat="server" ></asp:TextBox>
</div>
</div>

               <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Location</b>
 
    <asp:DropDownList ID="ddllocation"  Style="text-transform: uppercase;"   runat="server"  class="form-control text-label">
                    
</asp:DropDownList> 
</div>
</div>
</div>

            <div class="row">
                      <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Commodity</b>
 
    <asp:DropDownList ID="ddlCommodity"  Style="text-transform: uppercase;" OnTextChanged="ddlCommodity_TextChanged" AutoPostBack="true" runat="server"  class="form-control text-label">
                    
</asp:DropDownList> 
</div>
</div>

       <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Tax</b>
 
    <asp:DropDownList ID="ddltxtTax"  Style="text-transform: uppercase;"  runat="server"  class="form-control text-label">
                    
</asp:DropDownList> 
</div>
</div>
            </div>

</ContentTemplate></asp:UpdatePanel>
<div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >From</b>
<asp:TextBox ID="txtFromDate" Style="text-transform:uppercase" class="form-control text-label" TextMode="DateTimeLocal"
runat="server"   ></asp:TextBox>
</div>
</div>
          <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >To</b>
<asp:TextBox ID="txtToDate" Style="text-transform:uppercase" class="form-control text-label" TextMode="DateTimeLocal"
runat="server"   ></asp:TextBox>
</div>
</div>                       

     <div class="col-sm-1" style="padding-right:5px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnShow" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="btnShow_Click"    
Text="Show"  OnClientClick="return ValidationAdd()"/>
    
</div>                                              
</div>  
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional"><ContentTemplate>
             <div class="col-sm-3 col-xs-12">
        <div class="form-group">
            <asp:CheckBox runat="server" ID="chkSelectAll" AutoPostBack="true" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged" />
        </div>
    </div>
<div class="row">
<div class="col-lg-12 col-xs-12 text-label">
    <div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;" runat="server" id="divRegularGrid">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"> 
<Columns>
    
<asp:TemplateField>
<ItemTemplate>

<asp:CheckBox ID="chkright" Text="" Checked="true" runat="server" AutoPostBack="true" OnCheckedChanged="chkright_CheckedChanged" />
<asp:Label id="lblLoadedID" Visible="false" runat="server" Text='<%# Eval("Loaded_ID")%>'></asp:Label>

</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

    <asp:BoundField HeaderText="Sr No" DataField="SRNo" />
<asp:TemplateField HeaderText="Container No" Visible="false" >
<ItemTemplate>
<asp:Label id="lblContainerNo" runat="server" Text='<%# Eval("Loaded_Container_No")%>'></asp:Label>

</ItemTemplate>                            
</asp:TemplateField>
 <asp:TemplateField HeaderText="Size" Visible="false" >
<ItemTemplate>
<asp:Label id="lblSize" runat="server" Text='<%# Eval("size")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>    
<asp:TemplateField HeaderText="Truck No" Visible="false" >
<ItemTemplate>
<asp:Label id="lblTruckNo" runat="server" Text='<%# Eval("Loaded_Truck_No")%>'></asp:Label>
<%--<asp:Label id="lblItemCode" Visible="false" runat="server" Text='<%# Eval("ITEM_CODE")%>'></asp:Label>--%>
</ItemTemplate>                            
</asp:TemplateField>
    <asp:TemplateField HeaderText="Gate In Date" Visible="false" >
<ItemTemplate>
<asp:Label id="lblStuffingDate" runat="server" Text='<%# Eval("Stuffed_Date")%>'></asp:Label>
<%--<asp:Label id="lblItemCode" Visible="false" runat="server" Text='<%# Eval("ITEM_CODE")%>'></asp:Label>--%>
</ItemTemplate>                            
</asp:TemplateField>
    <asp:TemplateField HeaderText="Loading Date" >
<ItemTemplate>
<asp:Label id="lblLoadingDate" runat="server" Text='<%# Eval("Loaded_Date")%>'></asp:Label>
<%--<asp:Label id="lblItemCode" Visible="false" runat="server" Text='<%# Eval("ITEM_CODE")%>'></asp:Label>--%>
</ItemTemplate>                            
</asp:TemplateField>
<asp:TemplateField HeaderText="Total Days" Visible="false">
<ItemTemplate>
<asp:Label id="lblTotalDays" runat="server" Text='<%# Eval("Days")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>

<asp:TemplateField HeaderText="Loaded Pkgs" >
<ItemTemplate>
<asp:Label id="lblPkgs" runat="server" Text='<%# Eval("Loaded_Qty")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>

<asp:TemplateField HeaderText="Weight" >
<ItemTemplate>
<asp:Label id="lblWeight" runat="server" Text='<%# Eval("Loaded_Wt")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>

<asp:TemplateField HeaderText="Area" Visible="false" >
<ItemTemplate>
<asp:Label id="lblArea" runat="server" Text='<%# Eval("Area_cbm")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>

    <asp:TemplateField HeaderText="Trailer No" Visible="false" >
<ItemTemplate>
<asp:Label id="lblTrailerNo" runat="server" Text='<%# Eval("LoadingTrailer")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>
    <asp:TemplateField HeaderText="Commodity" >
<ItemTemplate>
<asp:Label id="lblCommodity" runat="server" Text='<%# Eval("Commodity")%>'></asp:Label>
<%--<asp:Label id="lblCommodityID" runat="server" Visible="false" Text='<%# Eval("CommodityID")%>'></asp:Label>--%>
</ItemTemplate>                            
</asp:TemplateField>
        <asp:TemplateField HeaderText="Location">
<ItemTemplate>
<asp:Label id="lblLocation" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
<asp:Label id="lblLocationID" runat="server" Visible="false" Text='<%# Eval("LocationID")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>
</Columns>

</asp:GridView>
</div>
<div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;display:none" runat="server" id="divImportGrid">
<asp:GridView ID="grdImport" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"> 
<Columns>
    
<asp:TemplateField>
<ItemTemplate>
<asp:CheckBox ID="chkright" Text="" Checked="true" runat="server" AutoPostBack="true" OnCheckedChanged="chkright_CheckedChanged" />
<asp:Label id="lblLoadedID" Visible="false" runat="server" Text='<%# Eval("Loaded_ID")%>'></asp:Label>

</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>
    <asp:BoundField HeaderText="Sr No" DataField="SRNo" />
<asp:TemplateField HeaderText="Container No" >
<ItemTemplate>
<asp:Label id="lblContainerNo" runat="server" Text='<%# Eval("Loaded_Container_No")%>'></asp:Label>

</ItemTemplate>                            
</asp:TemplateField>
 <asp:TemplateField HeaderText="Size" >
<ItemTemplate>
<asp:Label id="lblSize" runat="server" Text='<%# Eval("size")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>    
<asp:TemplateField HeaderText="Truck No" >
<ItemTemplate>
<asp:Label id="lblTruckNo" runat="server" Text='<%# Eval("Loaded_Truck_No")%>'></asp:Label>
<%--<asp:Label id="lblItemCode" Visible="false" runat="server" Text='<%# Eval("ITEM_CODE")%>'></asp:Label>--%>
</ItemTemplate>                            
</asp:TemplateField>
    <asp:TemplateField HeaderText="Gate In Date" >
<ItemTemplate>
<asp:Label id="lblStuffingDate" runat="server" Text='<%# Eval("Stuffed_Date")%>'></asp:Label>
<%--<asp:Label id="lblItemCode" Visible="false" runat="server" Text='<%# Eval("ITEM_CODE")%>'></asp:Label>--%>
</ItemTemplate>                            
</asp:TemplateField>
    <asp:TemplateField HeaderText="Loading Date" Visible="false" >
<ItemTemplate>
<asp:Label id="lblLoadingDate" runat="server" Text='<%# Eval("Loaded_Date")%>'></asp:Label>
<%--<asp:Label id="lblItemCode" Visible="false" runat="server" Text='<%# Eval("ITEM_CODE")%>'></asp:Label>--%>
</ItemTemplate>                            
</asp:TemplateField>
<asp:TemplateField HeaderText="Total Days" Visible="false">
<ItemTemplate>
<asp:Label id="lblTotalDays" runat="server" Text='<%# Eval("Days")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>

<asp:TemplateField HeaderText="Loaded Pkgs" >
<ItemTemplate>
<asp:Label id="lblPkgs" runat="server" Text='<%# Eval("Loaded_Qty")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>

<asp:TemplateField HeaderText="Weight" >
<ItemTemplate>
<asp:Label id="lblWeight" runat="server" Text='<%# Eval("Loaded_Wt")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>

<asp:TemplateField HeaderText="Area" Visible="false" >
<ItemTemplate>
<asp:Label id="lblArea" runat="server" Text='<%# Eval("Area_cbm")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>

    <asp:TemplateField HeaderText="Trailer No" Visible="false" >
<ItemTemplate>
<asp:Label id="lblTrailerNo" runat="server" Text='<%# Eval("LoadingTrailer")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>
    <asp:TemplateField HeaderText="Commodity" Visible="false" >
<ItemTemplate>
<asp:Label id="lblCommodity" runat="server" Text='<%# Eval("Commodity")%>'></asp:Label>
<%--<asp:Label id="lblCommodityID" runat="server" Visible="false" Text='<%# Eval("CommodityID")%>'></asp:Label>--%>
</ItemTemplate>                            
</asp:TemplateField>
        <asp:TemplateField HeaderText="Location" Visible="false">
<ItemTemplate>
<asp:Label id="lblLocation" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
<asp:Label id="lblLocationID" runat="server" Visible="false" Text='<%# Eval("LocationID")%>'></asp:Label>
</ItemTemplate>                            
</asp:TemplateField>
</Columns>
</asp:GridView>
</div>
</div>    
    </div>  
         </ContentTemplate></asp:UpdatePanel>  
    <div class="row">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional"><ContentTemplate>
        <div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b>Tariff  </b>
<asp:DropDownList  ID="ddlTariff" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                      
</asp:DropDownList>
</div>
</div>
            </ContentTemplate></asp:UpdatePanel>
        
        <div class="col-lg-2 text-label" style="padding-top:20px" >
<asp:Button ID="btncal" data-layout="center" data-type="confirm" OnClick="btncal_Click" 
class="btn btn-success btn-sm outline" runat="server" Text="Calculate"  OnClientClick=" return ValidationCalcu()" />
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Tax</b>
<asp:DropDownList ID="ddlTax"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                     
</asp:DropDownList> 
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Loaded/Destuff</b>
<asp:DropDownList ID="ddlLorD"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">     
    <asp:ListItem Value="0">--Select--</asp:ListItem> 
    <asp:ListItem Value="L">Loaded</asp:ListItem>
    <asp:ListItem Value="D">Destuff</asp:ListItem>
</asp:DropDownList> 
</div>
</div>
    </div> 

    <div class="row">
       <div class="col-md-8 col-xs-12">
<div class="form-group text-label">
<b  >In Ward</b>
<asp:TextBox ID="txtInWard" TextMode="MultiLine" Rows="2" Style="text-transform:uppercase" class="form-control text-label"  placeholder="In Ward"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Rake No</b>
<asp:TextBox ID="txtRakeNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Rake No"
runat="server"   ></asp:TextBox>
</div>
</div>
 </div>
            <div class="row">
       <div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b  >Out Ward</b>
<asp:TextBox ID="txtOutWard" TextMode="MultiLine" Rows="2" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Out Ward"
runat="server"   ></asp:TextBox>
</div>
</div>
 </div>

<div class="row">
<div class="col-sm-12 col-xs-12">
<div class="form-group text-label">
<b >Remarks</b>
<asp:TextBox ID="txtRemarks" Style="text-transform:uppercase" placeholder="Remarks" TextMode="MultiLine" Rows="2" class="form-control text-label"    
runat="server"   ></asp:TextBox>
</div>
</div>
</div>       
</asp:Panel>
                        
</div>
</div>

             
 </fieldset>                     

</div>
    <div class="col-md-4 col-xs-12 pull-md-right sidebar" >

<div menuitemname="Client Details" class="panel panel-sidebar" style="height:730px">
   
<div class="panel-body">
<asp:UpdatePanel ID="upModalSave1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
<ContentTemplate>

                                         
<div class="row text-label">
&nbsp;&nbsp; <b><asp:Label ID="lblname" runat="server" ForeColor="Blue" text="Charges to be collected"></asp:Label></b>
<div class="text-label pull-right" style="padding-right:5px">
<b><asp:Label ID="lblchargescount" Visible="false" runat="server" ForeColor="Blue" text="Count:"></asp:Label></b>
<asp:Label ID="LBLNO"  runat="server" ForeColor="Black" text=""></asp:Label>
</div>
                              
<br /><br />
<div class="col-lg-12 text-label">
<div class="table-responsive scrolling-table-container1" style="margin-left:-5px;margin-right:-5px;height:430px;">
<asp:GridView ID="rptIndentLIst" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
<Columns>
<asp:TemplateField Visible="false">
<ItemTemplate>
<asp:HiddenField ID="hfEntryid" runat="server" Value='<%#Eval("accountid")%>' />
<asp:Label ID="lblaccntid" runat="server" text='<%#Eval("accountid")%>'></asp:Label>
<asp:CheckBox id="chkindent"  runat="server" Visible="false"/>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left"  />
</asp:TemplateField>
     <asp:TemplateField HeaderText="Account Name" HeaderStyle-CssClass="header-center">
<ItemTemplate>
<asp:Label ID="lblLoadingNo" runat="server" Visible="false" text='<%#Eval("LoadingID")%>'></asp:Label>
<asp:Label ID="lblaccntname" runat="server" text='<%#Eval("accountname")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>                        
                       <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="header-center">
<ItemTemplate>
<asp:Label ID="lblContainerNoforAmount" runat="server"  text='<%#Eval("ContainerNo")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
</asp:TemplateField> 
    <asp:TemplateField HeaderText="Truck No" HeaderStyle-CssClass="header-center">
<ItemTemplate>
<asp:Label ID="lblTruckNoforAmount" runat="server" text='<%#Eval("TruckNo")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>      
<asp:TemplateField HeaderText="Net Amount" HeaderStyle-CssClass="header-center">
<ItemTemplate>
<asp:Label ID="lblIndentNo1" runat="server" Visible="false" text='<%#Eval("amount")%>'></asp:Label>
<asp:Label ID="lblntamnt" runat="server" text='<%#Eval("amount")%>'></asp:Label>
<asp:Label ID="lblQty" runat="server" Visible="false" text='<%#Eval("Qty")%>'></asp:Label>
<asp:Label ID="lblWeight" runat="server" Visible="false" text='<%#Eval("Weight")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="Right"  />
</asp:TemplateField>
</Columns>
</asp:GridView>
</div>
<div id="divtblWOTOtal" runat="server" style="display:none;">                                         
<table forecolor="Black" class="table table-striped table-bordered table-hover" style="border-top:5px solid #7bc144;margin-left:-5px;">
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b ">Net Total</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblPercentage" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblTotal" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Discount</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="Label1" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lbldisc" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">
       
<%--<td style ="width:50%;text-align:left"><b >CGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblCgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblCGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">
       
<%--<td style ="width:50%;text-align:left"><b >SGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblSgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblSGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">
       
<%--<td style ="width:50%;text-align:left"><b >IGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblIgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblIGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Grand Total</b></td>
<%--<td style ="width:20%;text-align:right"></td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblAllTotal" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
</table>
</div>
 
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>  
                        
</div>
                      
</div>
<div class="row" style="padding-top:14px;">
          
<div class="col-lg-12" style="margin-left:6px;">
<a href="GenerateInvoice.aspx" class="btn btn-primary btn-sm outline pull-left"
runat="server">Clear</a>
                                 
<asp:Button ID="btnSave" data-layout="center" Visible="true"  data-type="success" ValidationGroup="Groupsubmit" class="btn btn-success btn-sm outline pull-right "
runat="server" Text="Save"  OnClientClick="return ValidationSave()" OnClick="btnSave_Click"  />
</div>
<br /><br /><br />
<div class="col-lg-12" style="margin-left:6px;">
<div class="form-group ">
<a href="ListofInvoiceDetails.aspx" target="_blank"><b style="color:blue">Click here to view Assessment Summary</b> </a>
</div>
</div>
      <br /><br />
    <div class="col-lg-12" style="margin-left:6px;">
<div class="form-group ">
<a href="AdditionalCharges.aspx" target="_blank"><b style="color:blue">Click here to add Additional Charges</b> </a>
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
                   
<button class="btn btn-info btn-block" id="saveQuoOK" data-dismiss="modal" runat="server" onserverclick="saveQuoOK_ServerClick" aria-hidden="true">
OK 
</button>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
        <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblquoteApprove"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btnprint" data-dismiss="modal" runat="server" onclick="OpenWOPrint()"  aria-hidden="true">
Yes 
</button>
<a href="GenerateInvoice.aspx" class="btn btn-danger ">No</a>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>                       
</div>
                           
                          
                     
                       
                       
</div>

</div>
       
         
</div>
   <script type="text/javascript">
       var popup;
       function OpenWOPrint() {
           var txtassessno = document.getElementById('<%= txtassessno.ClientID%>').value;
           var txtworkyear = document.getElementById('<%= txtworkyear.ClientID%>').value;
           var txtInvoiceType = document.getElementById('<%= txtInvoiceType.ClientID%>').value;

           var url = "../Report_Domestic/DomesticInvoicePrint.aspx?AssessNo=" + txtassessno + "&WorkYear=" + txtworkyear + "&InvoiceType=" + txtInvoiceType
    window.open(url);

}

</script>
<script type="text/javascript">
function ValidationSave() {
                 
    var ddlCustomer = document.getElementById('<%= ddlCustomer.ClientID%>').value;
    var txtFromDate = document.getElementById('<%= txtFromDate.ClientID%>').value;
    var txtToDate = document.getElementById('<%= txtToDate.ClientID%>').value;
    var ddlTariff = document.getElementById('<%= ddlTariff.ClientID%>').value;
    var txtgstname = document.getElementById('<%= txtgstname.ClientID%>').value;
                   
    document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline pull-right disabled");   

var blResult = Boolean;
blResult = true;
                   
if (txtgstname == "") {
document.getElementById('<%= txtgstin.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}
    if (ddlCustomer == 0) {
        document.getElementById('<%= ddlCustomer.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
    if (txtFromDate == "") {
        document.getElementById('<%= txtFromDate.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
    if (txtToDate == "") {
        document.getElementById('<%= txtToDate.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
    if (ddlTariff == 0) {
        document.getElementById('<%= ddlTariff.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
//alert('hi')
if (blResult == false) {
    alert('Please fill the required fields!');
    document.getElementById('<%= btnSave.ClientID%>').value = "Save";
    document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline pull-right");
}
return blResult;
}
</script>
       <script type="text/javascript">
           var popup;
           function gstsearch() {
               var url = "GSTPartySearch.aspx"

               popup = window.open(url, "Popup", "width=710,height=555");
               popup.focus();
           }
</script>
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
    <script type="text/javascript">
        function ValidationAdd() {
            var ddlCustomer = document.getElementById('<%= ddlCustomer.ClientID%>').value;
            var txtFromDate = document.getElementById('<%= txtFromDate.ClientID%>').value;
            var txtToDate = document.getElementById('<%= txtToDate.ClientID%>').value;
            var ddlInvoiceType = document.getElementById('<%= ddlInvoiceType.ClientID%>').value;
            var txtIGMNo = document.getElementById('<%= txtIGMNo.ClientID%>').value;
            var txtItemNo = document.getElementById('<%= txtItemNo.ClientID%>').value;
            var ddllocation = document.getElementById('<%= ddllocation.ClientID%>').value;

            document.getElementById('<%= btnShow.ClientID%>').value = "Please Wait...";
            document.getElementById('<%= btnShow.ClientID%>').setAttribute("class", "btn btn-primary btn btn-sm outline disabled");
    var blResult = Boolean;
    blResult = true;

    if (ddlInvoiceType == "Import") {
        if (txtIGMNo == "") {
            document.getElementById('<%= txtIGMNo.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }
        if (txtItemNo == "") {
            document.getElementById('<%= txtItemNo.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;
        }
    }
    else {
        if (ddlCustomer == 0) {
            document.getElementById('<%= ddlCustomer.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }
    }
    
            if (txtFromDate == "") {
        document.getElementById('<%= txtFromDate.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;
}
            if (txtToDate == "") {
                document.getElementById('<%= txtToDate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }

            if (ddllocation == "") {
                document.getElementById('<%= txtToDate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }
    //alert('hi')
    if (blResult == false) {
        alert('Please fill the required fields!');
        document.getElementById('<%= btnShow.ClientID%>').value = "Show";
        document.getElementById('<%= btnShow.ClientID%>').setAttribute("class", "btn btn-primary btn btn-sm outline");
}
    return blResult;
        }
        function ValidationCalcu() {
            var txtgstin = document.getElementById('<%= txtgstin.ClientID%>').value;
            var ddlTariff = document.getElementById('<%= ddlTariff.ClientID%>').value;
            var ddlInvoiceType = document.getElementById('<%= ddlInvoiceType.ClientID%>').value;
            var ddlLorD = document.getElementById('<%= ddlLorD.ClientID%>').value;

            document.getElementById('<%= btncal.ClientID%>').value = "Please Wait...";
            document.getElementById('<%= btncal.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline disabled");
            var blResult = Boolean;
            blResult = true;

            if (ddlTariff == 0) {
                document.getElementById('<%= ddlTariff.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }
            if (txtgstin == "") {
                document.getElementById('<%= txtgstin.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlInvoiceType == 0) {
                document.getElementById('<%= ddlInvoiceType.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            else {
                if (ddlInvoiceType == "Import") {
                    if (ddlLorD == 0) {
                        document.getElementById('<%= ddlLorD.ClientID%>').style.borderColor = "red";
                        blResult = blResult && false;
                    }
                }
            }
            //alert('hi')
            if (blResult == false) {
                alert('Please fill the required fields!');
                document.getElementById('<%= btncal.ClientID%>').value = "Calculate";
                document.getElementById('<%= btncal.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline");
            }
            return blResult;
        }
        function InvoiceTypeChange() {
            var ddlInvoiceType = document.getElementById('<%= ddlInvoiceType.ClientID%>').value;

            if (ddlInvoiceType == "Import") {
                document.getElementById('<%= divImportGrid.ClientID%>').style.display = "block";
                document.getElementById('<%= divRegularGrid.ClientID%>').style.display = "none";
                document.getElementById('<%= divIGMItem.ClientID%>').style.display = "block";
                document.getElementById('<%= txtIGMNo.ClientID%>').value = "";
                document.getElementById('<%= txtItemNo.ClientID%>').value = "";
                document.getElementById('<%= txtContainerNo.ClientID%>').value = "";
            }
            
            else {
                document.getElementById('<%= divImportGrid.ClientID%>').style.display = "none";
                document.getElementById('<%= divRegularGrid.ClientID%>').style.display = "block";
                document.getElementById('<%= divIGMItem.ClientID%>').style.display = "none";
                document.getElementById('<%= txtIGMNo.ClientID%>').value = "";
                document.getElementById('<%= txtItemNo.ClientID%>').value = "";
                document.getElementById('<%= txtContainerNo.ClientID%>').value = "";
            }
        }
</script>
</asp:Content>
