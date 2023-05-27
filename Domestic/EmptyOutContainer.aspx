<%@ Page Title="Domestic |Empty Out Container" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EmptyOutContainer.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Domestic |Empty Out Container</title>
       
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
<i class="glyphicon glyphicon-transfer"></i>Empty Out Container
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
 Empty Out Container
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
 <div class="row">

         <div class="col-md-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Entry ID</b>
<asp:TextBox ID="txtentryID" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
 <div class="col-md-2" style="display:none">
    <asp:TextBox runat="server" ID="txtSlipNoPrint"></asp:TextBox>
    </div>

       <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >Gate Out Date & Time</b>
                    <asp:TextBox ID="txtGateOutDate" placeholder="dd-MM-yyyy" TextMode="DateTimeLocal"  runat="server" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>
</div>
    <div class="row">
            
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
     <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b  >Trailer No</b>
<asp:TextBox ID="txttrailerno" Style="text-transform:uppercase" OnTextChanged="txttrailerno_TextChanged" AutoPostBack="true"   class="form-control text-label"  placeholder="Trailer No"
runat="server"   ></asp:TextBox>
</div>
</div>
    
   <asp:Panel ID="Panel1" runat="server" Enabled="false"> 

           <div class="col-md-2 col-xs-12 " id="divtrai" runat="server"  >
            <div class="form-group text-label"  style="padding-top:20px;">


<asp:TextBox ID="txttrailerNo1" Style="text-transform:uppercase"  class="form-control text-label"  placeholder=""
runat="server"   ></asp:TextBox>
</div>
                       </div>
       </asp:Panel>

         <div class="col-md-2 col-xs-12" id="divtrailer" runat="server" style="display:none" >
            <div class="form-group text-label">

<b>Trailer No</b>
<asp:TextBox ID="txttrailer" Style="text-transform:uppercase"  class="form-control text-label"  placeholder=""
runat="server"   ></asp:TextBox>
</div>
                       </div>
          <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Transporter</b>
<asp:DropDownList ID="ddltransporter"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>

    <div class="col-md-2 col-xs-12" >
            <div class="form-group text-label">

<b>Driver Code</b>
<asp:TextBox ID="txtDriverCode" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Driver Code"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        
          <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Source</b>
<asp:DropDownList ID="ddlSource"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
                       </div>

    <div class="col-md-3 col-xs-12"  >
            <div class="form-group text-label">

<b>Booking No</b>
<asp:TextBox ID="txtBookingNo" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Booking No"
runat="server"   ></asp:TextBox>
</div>
                       </div>
      
    </ContentTemplate>
            </asp:UpdatePanel>
          <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
<ContentTemplate>
          <div class="col-md-3 col-xs-12" >
            <div class="form-group text-label">

<b>Container No</b>
<asp:TextBox ID="txtcontainerNo" Style="text-transform:uppercase"  MaxLength="11"  OnTextChanged="txtcontainerNo_TextChanged" AutoPostBack="true" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        <asp:Panel ID="Panel3" runat="server" Enabled="false"> 
            
         <div class="col-md-2 col-xs-12"  >
            <div class="form-group text-label">

<b>Jo No</b>
<asp:TextBox ID="txtjono" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Jo No"
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
         <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b  >Type</b>
<asp:DropDownList ID="ddltype"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>
            </asp:Panel>
         <div class="row">
             <div class="col-md-3 col-xs-12"  >
            <div class="form-group text-label">

<b>Gross Weight</b>
<asp:TextBox ID="txtGrossWeight" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Gross Weight"
runat="server"   ></asp:TextBox>
</div>
                       </div>
             <div class="col-md-3 col-xs-12"  >
            <div class="form-group text-label">

<b>Tare Weight</b>
<asp:TextBox ID="txttareWeight" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Tare Weight"
runat="server"   ></asp:TextBox>
</div>
                       </div>
         </div>
            
         <div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b  >Customer Name</b>
<asp:DropDownList ID="ddlcustomer"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>
</div>
</div>

       
       
       
    <asp:Panel ID="Panel4" runat="server" Enabled="false">
         <div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b  >Shipping Line</b>
<asp:DropDownList ID="ddlshippingline"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
  

</asp:DropDownList>
</div>
</div>
       </asp:Panel>
    </ContentTemplate>
              </asp:UpdatePanel>
             </div>

    <div class="row">
        <div class="col-md-6 col-xs-12"  >
            <div class="form-group text-label">

<b>Remarks</b>
<asp:TextBox ID="txtRemarks" Style="text-transform:uppercase"    class="form-control text-label"  placeholder="Remarks"
runat="server"   ></asp:TextBox>
</div>
                       </div>
    <div class="col-sm-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnAdd" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnAdd_Click"
Text="Add"  OnClientClick="return ValidationAdd()"  />
</div>
                                              
                                      
</div>
 </div>

  
     
 
      </div>


<asp:Label ID="Lbloutno" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblVendorName" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblDriverNo" Visible="false" runat="server" Text=""></asp:Label>

    
    <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
 
 

<div class="row">
<div class=" col-md-9 col-lg-9 text-label "  style="padding-right:50px;">
<div class="table-responsive  ">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!"  ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
         <asp:TemplateField HeaderText="Container No" >
                             <ItemTemplate>
                                 <asp:Label id="lblcontainerno" runat="server" Text='<%# Eval("ContainerNo")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

      <asp:TemplateField HeaderText="Size" >
                             <ItemTemplate>
                                 <asp:Label id="lblsize" runat="server" Text='<%# Eval("Size")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>

     <asp:TemplateField HeaderText="Container Type" >
                             <ItemTemplate>
                                 <asp:Label id="lblcontype" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                                 <asp:Label id="lblcontypeID" Visible="false" runat="server" Text='<%# Eval("TypeID")%>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>
    <asp:TemplateField HeaderText="Gross Weight" >
                             <ItemTemplate>
                                 <asp:Label id="lblGrossWeight" runat="server" Text='<%# Eval("GrossWeight")%>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>
    <asp:TemplateField HeaderText="Tare Weight" >
                             <ItemTemplate>
                                 <asp:Label id="lblTareWeight" runat="server" Text='<%# Eval("TareWeight")%>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>

</Columns>

</asp:GridView>
</div>
</div>


</div>
       </ContentTemplate>
</asp:UpdatePanel>

    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:8px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline " runat="server"  OnClick="btnSave_Click"
Text="Save" OnClientClick="return ValidationSav()"   />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:0px;">
<div class="form-group" style="padding-top:8px">
                           
<a href="EmptyOutContainer.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a> 
                              
</div>                                            
                                      
</div>

        <div class="col-sm-5 pull-right" style="padding-top:15px;">
<div class="form-group">
<a href="EmptyInSummary.aspx" target="_blank"><b style="color:blue"></b> </a>
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
                   

                                <button class="btn btn-info btn-block" id="SaveOk" data-dismiss="modal" runat="server" onserverclick="SaveOk_ServerClick">OK</button>
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
<asp:Label ID="lblPrintQue"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btnprint" data-dismiss="modal" runat="server" onclick="OpenSlipPrint()"  aria-hidden="true">
Yes  
</button>
<a href="EmptyOutContainer.aspx" class="btn btn-danger ">No</a>
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
        var popup;
        function OpenSlipPrint() {

            var txtSlipNoPrint = document.getElementById('<%= txtSlipNoPrint.ClientID%>').value;

            var url = "../Report_Domestic/DomesticEmptyOutPrint.aspx?SlipNo=" + txtSlipNoPrint

            window.open(url);

        }

</script>
    <script type="text/javascript">
        function ValidationAdd() {

            var txtcontainerNo = document.getElementById('<%= txtcontainerNo.ClientID%>').value;
           

    var blResult = Boolean;
    blResult = true;



    if (txtcontainerNo == "") {
        document.getElementById('<%= txtcontainerNo.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}

     


    //alert('hi')
    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    return blResult;
}
</script>
        <script type="text/javascript">
            function ValidationSav() {

                var txtdrivercode = document.getElementById('<%= txtdrivercode.ClientID%>').value;

                var ddltransporter = document.getElementById('<%= ddltransporter.ClientID%>').value;
                var ddlSource = document.getElementById('<%= ddlSource.ClientID%>').value;

                //var ddltype = document.getElementById('<%= ddltype.ClientID%>').value;

                var blResult = Boolean;
                blResult = true;



                <%--if (txtdrivercode == "") {
                    document.getElementById('<%= txtdrivercode.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }--%>

            if (ddltransporter == 0) {
                document.getElementById('<%= ddltransporter.ClientID%>').style.borderColor = "red";
                    blResult = blResult && false;

                }

                if (ddlSource == 0) {
                    document.getElementById('<%= ddlSource.ClientID%>').style.borderColor = "red";
                    blResult = blResult && false;

                }


                //alert('hi')
                if (blResult == false) {
                    alert('Please fill the required fields!');
                }
                return blResult;
            }
</script>
    
   
</asp:Content>
