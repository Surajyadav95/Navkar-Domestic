

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
            ''LoadReport()

            If Not (Request.QueryString("SheetNo") = "") Then
                LoadReport()
            End If
        End If
    End Sub
    Protected Sub btnExport_Click()
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf")
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Dim sw As New StringWriter()
        'Dim hw As New HtmlTextWriter(sw)
        ''  pnlPerson.RenderControl(hw)
        'Dim sr As New StringReader(sw.ToString())

        'Response.Write(hw)
        'Response.End()
    End Sub

    
    Private Sub LoadReport()
        Try


            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Domestic/LoadingSheetPrint.rdlc")

            Dim SheetNo As String = Request.QueryString("SheetNo")
            'SheetNo = "1"
            ds = db.sub_GetDataSets("USP_LOADEDING_PRINT " & SheetNo & "")

            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(1))


            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)



            Dim EntryDate As String = Trim(ds.Tables(0).Rows(0)("Loaded_Date") & "")
            Dim DContainerNo As String = Trim(ds.Tables(0).Rows(0)("D_Container_No") & "")
            Dim Indate As String = Trim(ds.Tables(0).Rows(0)("InDate_Time") & "")
            Dim JONO As String = Trim(ds.Tables(0).Rows(0)("JONO") & "")
            Dim Customer As String = Trim(ds.Tables(0).Rows(0)("AGName") & "")
            Dim Qty As String = Trim(ds.Tables(0).Rows(0)("Stuffed_Qty") & "")
            Dim Weight As String = Trim(ds.Tables(0).Rows(0)("Stuffed_Wt") & "")
            Dim ReceiverName As String = Trim(ds.Tables(0).Rows(0)("ReceiverName") & "")
            Dim UserName As String = Trim(ds.Tables(0).Rows(0)("UserName") & "")
            Dim Location As String = Trim(ds.Tables(0).Rows(0)("Location") & "")
 


            Dim con_Name As String = Trim(ds.Tables(2).Rows(0)("con_Name") & "")
            Dim AddressI As String = Trim(ds.Tables(2).Rows(0)("AddressI") & "")
            Dim AddressII As String = Trim(ds.Tables(2).Rows(0)("AddressII") & "")
            Dim Con_For As String = Trim(ds.Tables(2).Rows(0)("Con_For") & "")



            Dim p1 As New ReportParameter("SheetNo", SheetNo)
            Dim p2 As New ReportParameter("DContainerNo", DContainerNo)
            Dim p3 As New ReportParameter("Indate", Indate)
            Dim p4 As New ReportParameter("JONO", JONO)
            Dim p5 As New ReportParameter("Customer", Customer)

            Dim p6 As New ReportParameter("Qty", Qty)
            Dim p7 As New ReportParameter("Weight", Weight)
            Dim p8 As New ReportParameter("ReceiverName", ReceiverName)
            Dim p9 As New ReportParameter("UserName", UserName)


            Dim p10 As New ReportParameter("con_Name", con_Name)
            Dim p11 As New ReportParameter("AddressI", AddressI)
            Dim p12 As New ReportParameter("AddressII", AddressII)
          
            Dim p13 As New ReportParameter("EntryDate", EntryDate)
            Dim p14 As New ReportParameter("Location", Location)




            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14})

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
