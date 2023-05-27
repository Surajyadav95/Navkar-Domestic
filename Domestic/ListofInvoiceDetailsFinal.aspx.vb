Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports System.Configuration

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt10 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing

    Dim streamids As String() = Nothing

    Dim mimeType As String = Nothing

    Dim encoding As String = Nothing

    Dim extension As String = Nothing

    Dim deviceInfo As String

    Dim bytes As Byte()
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Filldropdown()
            If Not (Request.QueryString("AssessNo") = "" Or (Request.QueryString("WorkYear") = "")) Then
                btnGeneratePDF_Click(sender, e)
            End If
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT00:00")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            btnSave_Click(sender, e)
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_DOMESTIC_INVOICE_FILLdROPDOWN"
            ds = db.sub_GetDataSets(strSql)
            ddlCustomer.DataSource = ds.Tables(0)
            ddlCustomer.DataTextField = "AGName"
            ddlCustomer.DataValueField = "AGID"
            ddlCustomer.DataBind()
            ddlCustomer.Items.Insert(0, New ListItem("All", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql += " USP_DOMESTIC_INVOICE_DETAILS '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlCustomer.SelectedValue) & "','" & Trim(txtInvoiceSearch.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If
            strSql = ""
            strSql += " USP_DOMESTIC_INVOICE_DETAILS '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlCustomer.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            strSql = ""
            strSql += "Select * from Con_Details_Domestic"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Invoice Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(12)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Range("A5:" & Excelno & "5").Merge()
                        .Range("A6:" & Excelno & "6").Merge()
                        .Range("A7:" & Excelno & "7").Merge()
                        .Range("A8:" & Excelno & "8").Merge()
                        .Range("A9:" & Excelno & "9").Merge()
                        .Range("A10:" & Excelno & "10").Merge()
                        .Range("A11:" & Excelno & "11").Merge()
                        .Range("A12:" & Excelno & "12").Merge()

                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(2, 1).Value = Trim(dt10.Rows(0)("con_NameI") & "")
                        .Cell(3, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")
                        .Cell(4, 1).Value = Trim(dt10.Rows(0)("AddressII") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(6).Height = 20
                        .Row(10).Height = 20
                        .Cell(10, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(10, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(11, 1).Value = "Invoice Summary"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(2, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(4, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(5, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(5, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(6, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(6, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(6, 1).Style.Font.FontSize = 17
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Cell(11, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(11, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(11, 1).Style.Font.FontSize = 17
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=InvoiceSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)

                        Response.Flush()
                        Response.End()
                    End Using
                End Using
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No record found!');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnGeneratePDF_Click(sender As Object, e As EventArgs)
        Try

            For Each row In grdcontainer.Rows
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
                If Request.QueryString("InvoiceType") = "Import" Then
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Domestic/DomesticInvoicePrintImport.rdlc")
                Else
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Domestic/DomesticInvoicePrint.rdlc")
                End If


                Dim assessnoprint As String = Request.QueryString("AssessNo")
                Dim workyear As String = Request.QueryString("WorkYear")
                ''assessno = "182"
                ''workyear = "2018-19"
                strSql = ""
                strSql += "USP_INVOICE_PRNT_DETAILS_DOMESTIC '" & Trim(CType(row.FindControl("lblassessNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblworkyear"), Label).Text) & "'"
                            ds = db.sub_GetDataSets(strSql)
                dt = ds.Tables(1)
                Dim Con_name As String = Trim(dt.Rows(0)("con_Name") & "")
                Dim AddressI As String = Trim(dt.Rows(0)("AddressI") & "")
                Dim AddressII As String = Trim(dt.Rows(0)("AddressII") & "")
                Dim BankName As String = Trim(dt.Rows(0)("BankName") & "")
                Dim AccountNo As String = Trim(dt.Rows(0)("AccountNo") & "")
                Dim BranchName As String = Trim(dt.Rows(0)("BranchName") & "")
                Dim IFSCCode As String = Trim(dt.Rows(0)("IFSCCode") & "")
                Dim GSTINComp As String = Trim(dt.Rows(0)("GSTIN") & "")
                Dim CINNo As String = Trim(dt.Rows(0)("CINNo") & "")
                Dim PANNo As String = Trim(dt.Rows(0)("PANNo") & "")
                Dim ConFor As String = Trim(dt.Rows(0)("Con_For") & "")
                Dim NoteI As String = Trim(dt.Rows(0)("NoteI") & "")
                Dim NoteII As String = Trim(dt.Rows(0)("NoteII") & "")
                Dim NoteIII As String = Trim(dt.Rows(0)("NoteIII") & "")
                Dim noteiv As String = Trim(dt.Rows(0)("noteiv") & "")

                dt1 = ds.Tables(0)

                Dim AssessNo As String = Trim(dt1.Rows(0)("AssessNo") & "")
                Dim AssessDate As String = Trim(dt1.Rows(0)("AssessDate") & "")
                Dim GSTNAME As String = Trim(dt1.Rows(0)("GSTName") & "")
                Dim GST_address As String = Trim(dt1.Rows(0)("GST_address") & "")
                Dim GSTIN As String = Trim(dt1.Rows(0)("GSTIN") & "")
                Dim STATE As String = Trim(dt1.Rows(0)("STATE") & "")
                Dim STATECODE As String = Trim(dt1.Rows(0)("STATECODE") & "")
                Dim AGENTNAME As String = Trim(dt1.Rows(0)("AGENTNAME") & "")
                Dim BillPeriod As String = Trim(dt1.Rows(0)("BillPeriod") & "")
                Dim Remarks As String = Trim(dt1.Rows(0)("Remarks") & "")
                Dim CargoDesc As String = Trim(dt1.Rows(0)("CargoDesc") & "")

                Dim UserName As String = Trim(dt1.Rows(0)("UserName") & "")
                Dim Today As String = Trim(dt1.Rows(0)("Today") & "")
                Dim SGST As String = Val(dt1.Rows(0)("sgst"))
                Dim CGST As String = Val(dt1.Rows(0)("cgst"))
                Dim IGST As String = Val(dt1.Rows(0)("igst"))
                Dim NetTotal As String = dt1.Rows(0)("NetTotal")
                Dim GrandTotal As String = dt1.Rows(0)("GrandTotal")
                Dim amtinword As String = dt1.Rows(0)("amtinword")
                Dim CON_DETS As String = dt.Rows(0)("Con_Dets")


                Dim DeliveryType As String = dt1.Rows(0)("DeliveryType")
                Dim TaxNote As String = Trim(dt1.Rows(0)("Note") & "")

                dt2 = ds.Tables(2)
                Dim datasource As New ReportDataSource("DataSet1", dt2)
                dt3 = ds.Tables(3)
                dt4 = ds.Tables(4)

                Dim datasource1 As New ReportDataSource("DataSet2", dt3)
                Dim datasource2 As New ReportDataSource("DataSet3", dt4)


                ReportViewer1.LocalReport.DataSources.Clear()
                ReportViewer1.LocalReport.DataSources.Add(datasource)
                ReportViewer1.LocalReport.DataSources.Add(datasource1)
                ReportViewer1.LocalReport.DataSources.Add(datasource2)


                Dim p1 As New ReportParameter("Con_Name", Con_name)
                Dim p2 As New ReportParameter("AddressI", AddressI)
                Dim p3 As New ReportParameter("AddressII", AddressII)
                Dim p4 As New ReportParameter("BankName", BankName)
                Dim p5 As New ReportParameter("AccountNo", AccountNo)
                Dim p6 As New ReportParameter("BranchName", BranchName)
                Dim p7 As New ReportParameter("IFSCCOde", IFSCCode)
                Dim p8 As New ReportParameter("GSTINComp", GSTINComp)
                Dim p9 As New ReportParameter("CINNo", CINNo)
                Dim p10 As New ReportParameter("PANNo", PANNo)

                Dim p11 As New ReportParameter("AssessNo", AssessNo)
                Dim p12 As New ReportParameter("AssessDate", AssessDate)
                Dim p13 As New ReportParameter("GSTNAME", GSTNAME)
                Dim p14 As New ReportParameter("GST_address", GST_address)
                Dim p15 As New ReportParameter("GSTIN", GSTIN)
                Dim p16 As New ReportParameter("STATE", STATE)
                Dim p17 As New ReportParameter("STATECODE", STATECODE)
                Dim p18 As New ReportParameter("AGENTNAME", AGENTNAME)
                Dim p19 As New ReportParameter("BillPeriod", BillPeriod)
                Dim p20 As New ReportParameter("UserName", UserName)
                Dim p21 As New ReportParameter("Today", Today)
                Dim p22 As New ReportParameter("SGST", SGST)
                Dim p23 As New ReportParameter("CGST", CGST)
                Dim p24 As New ReportParameter("IGST", IGST)
                Dim p25 As New ReportParameter("NetTotal", NetTotal)
                Dim p26 As New ReportParameter("GrandTotal", GrandTotal)
                Dim p27 As New ReportParameter("amtinword", amtinword)
                Dim p28 As New ReportParameter("ConFor", ConFor)

                Dim p29 As New ReportParameter("NoteI", NoteI)
                Dim p30 As New ReportParameter("NoteII", NoteII)

                Dim p31 As New ReportParameter("DeliveryType", DeliveryType)
                Dim p32 As New ReportParameter("NoteIII", NoteIII)

                Dim p33 As New ReportParameter("noteiv", noteiv)
                Dim p34 As New ReportParameter("CON_DETS", CON_DETS)

                Dim p35 As New ReportParameter("Remarks", Remarks)
                Dim p36 As New ReportParameter("CargoDesc", CargoDesc)
                Dim p37 As New ReportParameter("TaxNote", TaxNote)

                Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37})
                ' Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33})

                '    'Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1})
                '    'lblerror.Text = "loaded3"
                'End If
                'Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1})
                'lblError.Text = "loaded3"
                '' Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4})

                'ReportViewer1.LocalReport.Refresh()
                deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>"

                bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo, mimeType, encoding, extension, streamids, warnings)

                Response.ClearContent()

                Response.ClearHeaders()

                Response.ContentType = "application/pdf"

                Response.BinaryWrite(bytes)

                Response.BinaryWrite(bytes)
                File.WriteAllBytes("F:\ICD\Navkar-Domestic\Navkar-Domestic\Domestic_PDF\AssessMentPrint_" & Val(CType(row.FindControl("lblassessNo"), Label).Text) & ".pdf", bytes)
                'Response.Flush()

                'Response.Close()
            Next
            'btnsearch_Click(sender, e)
            'lblsession.Text = "Assessment Cancelled Successfully"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
            'UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim AssessNo As String = lnkcancel.CommandArgument
            Dim WorkYear As String = Trim(CType(row.FindControl("lblworkyear"), Label).Text)
            Dim str As String = ""
            txtAssessno.Text = AssessNo
            TxtWorkYear.Text = WorkYear
            Dim dtACk As New DataTable
            strSql = ""
            strSql = "USP_Validation_ACK_Status '" & AssessNo & "' "
            dtACk = db.sub_GetDatatable(strSql)
            If dtACk.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' " + dtACk.Rows(0)("msg") + ".');", True)
                Exit Sub
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "OpenList", "<script>OpenCancelInvoice(); </script>")
            End If
            'strSql = ""
            'strSql += "UPDATE Domestic_assessM set iscancel=1,cancelledby='" & Session("UserID") & "',CancelledOn=getdate() where InvoiceNo='" & AssessNo & "' and WorkYear='" & WorkYear & "'"
            'db.sub_ExecuteNonQuery(strSql)
            'btnSave_Click(sender, e)
            'lblsession.Text = "Assessment Cancelled Successfully"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
            'UpdatePanel6.Update()
            'lientScript.RegisterStartupScript(Page.GetType(), "OpenList", "<script>OpenCancelInvoice(); </script>")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub
End Class
