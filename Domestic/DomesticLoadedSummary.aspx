<%@ Page Title="Domestic |Domestic Loaded In Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
    CodeFile="DomesticLoadedSummary.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
        <title>Domestic |Domestic Loaded In Summary</title>
    </head>
    <div class="page-container">
        <div class="pageheader">
            <h3>
                <i class="glyphicon glyphicon-transfer"></i>Domestic Loaded In Summary 
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



                            <div class="col-md-2 col-xs-12">
                                <div class="form-group text-label">
                                    Category 
                                    <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" class="form-control text-label">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                        <asp:ListItem Value="1">Line Name</asp:ListItem>
                                        <asp:ListItem Value="2">Customer Name</asp:ListItem>
                                 

                                    </asp:DropDownList>

                                </div>

                            </div>
                            <div class="col-md-4 col-xs-12" style="display: none;" id="divCHA" runat="server">
                                <div class="form-group text-label">
                                    Line Name
                                    <asp:DropDownList ID="ddlLine" runat="server" Style="text-transform:uppercase" class="form-control text-label">
                                    </asp:DropDownList>

                                </div>
                            </div>

                             
                            <div class="col-md-4 col-xs-12" style="display: none;" id="divNoc" runat="server">
                                <div class="form-group text-label">
                                    <b>Customer Name</b>
                                      <asp:DropDownList ID="ddlCustomer" runat="server" Style="text-transform:uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <asp:Label ID="lblPDCode" Visible="false" runat="server" Text=""></asp:Label>

                            <div class="col-md-1 col-xs- 12">
                                <div class="form-group pull-right" style="padding-top: 20px">
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
                                                    AutoGenerateColumns="true" EmptyDataText="No records found!" DataKeyNames="Container No" ShowHeaderWhenEmpty="true" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="6">
                                                    <PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Left" />

                                                    <Columns>
                                                          <asp:TemplateField>
<ItemTemplate>
    <a  href='<%# "../Report_Domestic/DomesticLoadedInPrint.aspx?GateInNo=" & (DataBinder.Eval(Container.DataItem, "Gate In No")).ToString()%>'target="_blank" 
Class='btn btn-primary btn-xs outline' 
>Print</a>
    <asp:LinkButton ID="lnkCancel" ControlStyle-CssClass="btn btn-danger btn-xs outline"  Text="Cancel" OnClick="lnkCancel_Click" Visible="true"
                                                            OnClientClick="return confirm('Are you sure to Cancel this Container?')"
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Gate In No")%>' runat="server"
                                                            ></asp:LinkButton>
    </ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="150px" />
</asp:TemplateField>
                                                      
                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                            
                        </div>

                         <div class="col-md-2 col-xs-12 pull-right " style="padding-top: 20px">
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
