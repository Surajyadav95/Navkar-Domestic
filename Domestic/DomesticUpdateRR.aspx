﻿<%@ Page Title="Domestic" Language="VB" EnableEventValidation="false" MasterPageFile="PopUp.master" AutoEventWireup="false" CodeFile="DomesticUpdateRR.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
window.opener.document.getElementById("ContentPlaceHolder1_btnSlab").click();
    self.close();
}
</script>
    <style>
        .text-center{
            text-align:center
        }
        .scrolling-table-container{
            height:200px;
            overflow:auto
        }
    </style>
<div class="container" style="background-color: white;margin-top:-60px">    
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<h1>Update RR<small class="pull-right" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>                  
</div>
</div>                                 
<div class="row">

    <div class="col-sm-4 col-xs-4">
<div class="form-group text-label">
<b >Train No</b>
<asp:DropDownList  ID="ddlTrainNo" Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 
</asp:DropDownList>
</div>
</div> 
    <div class="col-sm-4 col-xs-4">
<div class="form-group text-label">
<b>RR No</b>
<asp:TextBox ID="txtRRNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Enter RR No"
runat="server"   ></asp:TextBox>
</div>
</div>
            <div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnAdd" class="btn btn-primary btn-sm" runat="server" OnClientClick="return ValidationAdd()" OnClick="btnAdd_Click"  
Text="Add"  />
</div>                                                                                  
</div> 
</div>  



          

<%--<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>--%>

<div class="row">                  

<div class="col-md-12 col-xs-12 text-label ">
<div class="table-responsive scrolling-table-container" style="margin-right:22px">
<asp:GridView ID="grdRRUpdate" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">

<Columns>
<asp:TemplateField>
<ItemTemplate>
    <asp:LinkButton ID="lnkDelete" ControlStyle-CssClass='btn btn-danger btn-sm outline' OnClick="lnkDelete_Click"                                                             
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SrNo")%>' runat="server" 
                                                            ><i class="fa fa-remove" aria-hidden="true"></i></asp:LinkButton>
<asp:Label runat="server" ID="lblAutoID" Text='<%#Eval("SrNo")%>' Visible="false"></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" Width="60px" />
</asp:TemplateField>
                                     
<asp:BoundField DataField="TrainNo" HeaderText="Train No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="RRNo" HeaderText="RR No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>

</Columns>

</asp:GridView>
</div>
</div>
</div>                        
        <div class="row">
            <div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm" runat="server" OnClick="btnSave_Click"  
Text="Save"  />
</div>                                                                                  
</div> 
        </div>     
</div>
    <div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-xs">
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

<button class="btn btn-info " ID="Button1" data-dismiss="modal" runat="server" aria-hidden="true">
                                 Ok 
                             </button>

</div>
</div>

</ContentTemplate>

</asp:UpdatePanel>
</div>
</div>
</div>
</div>
    <script type="text/javascript">
        function ValidationAdd() {
            var ddlTrainNo = document.getElementById('<%= ddlTrainNo.ClientID%>').value;
            var txtRRNo = document.getElementById('<%= txtRRNo.ClientID%>').value;

            var blResult = Boolean;
            blResult = true;

            if (ddlTrainNo == 0) {
                document.getElementById('<%= ddlTrainNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtRRNo == "") {
                document.getElementById('<%= txtRRNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
        }
    </script>
</asp:Content>


