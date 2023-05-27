

Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.IO

Partial Class Report_Estimation_Default
    Inherits System.Web.UI.Page
    Dim db As New dbOperation_Domestic

    Dim strSql, strSql1 As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9 As DataTable
    ' Dim db As New dbOperation
    Dim ds, ds1, ds2, ds3 As DataSet
    Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing

    Dim streamids As String() = Nothing

    Dim mimeType As String = Nothing

    Dim encoding As String = Nothing

    Dim extension As String = Nothing

    Dim deviceInfo As String

    Dim bytes As Byte()

    Dim lr As New Microsoft.Reporting.WebForms.LocalReport
    Private Sub PrintData(ByVal strTINo As String)
        Try
            btnExport_Click()
        Catch ex As Exception
            MsgBox("Error in procedure: " & System.Reflection.MethodBase.GetCurrentMethod.Name & vbCrLf & ex.Message.ToString)
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtvaldate.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy 08:00")
            LoadReport()

            If Not (Request.QueryString("GPNO") = "") Then
                LoadReport()
            End If
        End If
    End Sub
    Protected Sub btnExport_Click()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim sw As New StringWriter()
        Dim hw As New HtmlTextWriter(sw)
        '  pnlPerson.RenderControl(hw)
        Dim sr As New StringReader(sw.ToString())

        Response.Write(hw)
        Response.End()
    End Sub
    Private Sub LoadReport()
        Try
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Domestic/DomesticGateLogo.rdlc")

            Dim GPNO As String = Request.QueryString("GPNO")

            'GPNO = "1"
            ds = db.sub_GetDataSets("usp_gatePass_print_new " & GPNO & "")

            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(1))
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)

            Dim GPDate As String = Trim(ds.Tables(0).Rows(0)("GPDate") & "")
            Dim TransName As String = Trim(ds.Tables(0).Rows(0)("TransName") & "")
            Dim AGName As String = Trim(ds.Tables(0).Rows(0)("AGName") & "")
            Dim Location As String = Trim(ds.Tables(0).Rows(0)("Location") & "")
            Dim Remarks As String = Trim(ds.Tables(0).Rows(0)("Remarks") & "")

            Dim LRNO As String = Trim(ds.Tables(0).Rows(0)("LR_No") & "")
            Dim LRDATE As String = Trim(ds.Tables(0).Rows(0)("LR_DATE") & "")

            Dim con_Name As String = Trim(ds.Tables(2).Rows(0)("con_Name") & "")
            Dim AddressI As String = Trim(ds.Tables(2).Rows(0)("AddressI") & "")
            Dim AddressII As String = Trim(ds.Tables(2).Rows(0)("AddressII") & "")

            Dim p1 As New ReportParameter("GPNO", GPNO)
            Dim p2 As New ReportParameter("GPDate", GPDate)
            Dim p3 As New ReportParameter("TransName", TransName)
            Dim p4 As New ReportParameter("AGName", AGName)
            Dim p5 As New ReportParameter("Location", Location)

            Dim p6 As New ReportParameter("con_Name", con_Name)
            Dim p7 As New ReportParameter("AddressI", AddressI)
            Dim p8 As New ReportParameter("AddressII", AddressII)
            Dim p9 As New ReportParameter("REMARKS", Remarks)

            Dim p10 As New ReportParameter("LRNO", LRNO)
            Dim p11 As New ReportParameter("LRDATE", LRDATE)

            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11})

            deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>"

            bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo, mimeType, encoding, extension, streamids, warnings)

            Response.ClearContent()

            Response.ClearHeaders()

            Response.ContentType = "application/pdf"

            Response.BinaryWrite(bytes)

            Response.Flush()

            Response.Close()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub


End Class
