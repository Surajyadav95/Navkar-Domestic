

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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Domestic/DomesticLoadedInPrint.rdlc")

            Dim GateInNo As String = Request.QueryString("GateInNo")

            'GPNO = "1"
            ds = db.sub_GetDataSets("USP_DOMESTIC_LOADED_IN_PRINT " & GateInNo & "")

            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(2))
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)

            Dim InDate_Time As String = Trim(ds.Tables(0).Rows(0)("InDate_Time") & "")

            Dim ContainerNo As String = Trim(ds.Tables(0).Rows(0)("TrailerNo") & "")

            Dim JoNo As String = Trim(ds.Tables(0).Rows(0)("JoNo") & "")
            Dim AGName As String = Trim(ds.Tables(0).Rows(0)("AGName") & "")

            Dim REMARKS As String = Trim(ds.Tables(0).Rows(0)("REMARKS") & "")

            Dim Commodity As String = Trim(ds.Tables(0).Rows(0)("Commodity") & "")
            Dim Location As String = Trim(ds.Tables(0).Rows(0)("Location") & "")



            Dim CON_NAME As String = Trim(ds.Tables(1).Rows(0)("CON_NAME") & "")
            Dim CON_NAMEI As String = Trim(ds.Tables(1).Rows(0)("CON_NAMEI") & "")
            Dim ADDRESSI As String = Trim(ds.Tables(1).Rows(0)("ADDRESSI") & "")
            Dim ADDRESSII As String = Trim(ds.Tables(1).Rows(0)("ADDRESSII") & "")
            Dim ADDRESSIII As String = Trim(ds.Tables(1).Rows(0)("ADDRESSIII") & "")
            Dim ADDRESSIV As String = Trim(ds.Tables(1).Rows(0)("ADDRESSIV") & "")
            Dim CON_DETS As String = Trim(ds.Tables(1).Rows(0)("CON_DETS") & "")
            Dim CON_FOR As String = Trim(ds.Tables(1).Rows(0)("CON_FOR") & "")

            Dim p1 As New ReportParameter("GateInNo", GateInNo)
            Dim p2 As New ReportParameter("InDate_Time", InDate_Time)

            Dim p4 As New ReportParameter("ContainerNo", ContainerNo)

            Dim p7 As New ReportParameter("JoNo", JoNo)
            Dim p8 As New ReportParameter("AGName", AGName)


            Dim p11 As New ReportParameter("REMARKS", REMARKS)
            Dim p12 As New ReportParameter("con_Name", CON_NAME)
            Dim p13 As New ReportParameter("AddressI", ADDRESSI)

            Dim p14 As New ReportParameter("CON_NAMEI", CON_NAMEI)
            Dim p15 As New ReportParameter("ADDRESSII", ADDRESSII)
            Dim p16 As New ReportParameter("ADDRESSIII", ADDRESSIII)
            Dim p17 As New ReportParameter("ADDRESSIV", ADDRESSIV)
            Dim p18 As New ReportParameter("CON_DETS", CON_DETS)
            Dim p19 As New ReportParameter("CON_FOR", CON_FOR)

            Dim p20 As New ReportParameter("Commodity", Commodity)
            Dim p21 As New ReportParameter("Location", Location)

            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p4, p7, p8, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21})

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
