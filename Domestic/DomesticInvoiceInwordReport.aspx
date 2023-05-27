﻿<%@ Page Title="Domestic |Domestic Invoice Inword Report" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
    CodeFile="DomesticInvoiceInwordReport.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Domestic |Domestic Invoice Inword Report</title>
    </head>
    <div class="page-container">
        <div class="pageheader">
            <h3>
                <i class="glyphicon glyphicon-transfer"></i>Domestic Invoice Inword Report
            </h3>

        </div>
        <div id="page-content">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <div class="page-container">
                <asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

                <div class="panel">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-5  col-xs-12" style="width: 400px;">
                                <div class="form-group date text-label">
                                    Date
                                           
                                    <div class="input-group input-append date input-daterange" id="datePicker">
                                        <asp:TextBox ID="txtfromDate" Style="width: 150px;" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                        <div class="input-group-addon text-label" style="width: 40px;">To</div>
                                        <asp:TextBox ID="txttoDate" placeholder="mm-dd-yyyy" runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
                                    </div>

                                </div>


                            </div>

                               <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>

                           
                            <div class="col-md-3 col-xs-12" id="divContainer" runat="server" style="display:none">
            <div class="form-group text-label">

<b>Container No</b>
<asp:TextBox ID="txtcontainerNo" Style="text-transform:uppercase" MaxLength="11" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
                       </div>
        
           <div class="col-md-2 col-xs-12" id="divIgmNo" runat="server" style="display:none">
<div class="form-group text-label">
<b >IGM No</b>
<asp:TextBox ID="txtigmno" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 

         <div class="col-md-2 col-xs-12" id="divItem" runat="server" style="display:none">
<div class="form-group text-label">
<b >Item No</b>
<asp:TextBox ID="txtItemNo" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 
                             
    </ContentTemplate>
                                   </asp:UpdatePanel>
                            <asp:Label ID="lblPDCode" Visible="false" runat="server" Text=""></asp:Label>

                            <div class="col-md-1 col-xs- 12 pull-left">
                                <div class="form-group " style="padding-top: 20px">
                                    <asp:Button ID="btnsearch" runat="server"
                                        class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click"></asp:Button>
                                </div>
                            </div>

                              
                            

                        </div>


                        <div class="row">
                           

                                    <div class="row">
                                        <div class="col-lg-12 text-label " style="padding-left: 0px;">
                                            <div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
                                                <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                                                    AutoGenerateColumns="true" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="6">
                                                    <PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Left" />

                                                    <Columns>
   
                                                      
                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                            
                        </div>

                         <div class="col-md-2 col-xs-12 " style="padding-top: 20px">
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
</asp:Content>
