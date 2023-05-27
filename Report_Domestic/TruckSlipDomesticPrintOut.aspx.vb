

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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Domestic/TruckSlipDomesticPrintOut.rdlc")

           

            Dim SlipNo As String = Request.QueryString("SlipNo")

            'GPNO = "1"
            ds = db.sub_GetDataSets("USP_DOMESTIC_TRUCK_SLIP_PRINT_OUT " & SlipNo & "")

            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(2))


            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)

            Dim SLIPDATE As String = Trim(ds.Tables(0).Rows(0)("SLIP_DATE") & "")
            Dim VEHICLENO As String = Trim(ds.Tables(0).Rows(0)("VEHICLE_NO") & "")
            Dim SLIPTYPE As String = Trim(ds.Tables(0).Rows(0)("SLIP_TYPE") & "")
            Dim PARTYNAME As String = Trim(ds.Tables(0).Rows(0)("PARTY_NAME") & "")
            Dim DRIVERNAME As String = Trim(ds.Tables(0).Rows(0)("DRIVER_NAME") & "")
            Dim REMARKS As String = Trim(ds.Tables(0).Rows(0)("REMARKS") & "")
            Dim JO_NO As String = Trim(ds.Tables(0).Rows(0)("JO_NO") & "")
            Dim LRNO As String = Trim(ds.Tables(0).Rows(0)("LRNO") & "")
            Dim LR_DATE As String = Trim(ds.Tables(0).Rows(0)("LR_DATE") & "")
            'Dim Commodity As String = Trim(ds.Tables(0).Rows(0)("Commodity") & "")
            Dim UserName As String = Trim(ds.Tables(0).Rows(0)("UserName") & "")


            Dim CON_NAME As String = Trim(ds.Tables(1).Rows(0)("CON_NAME") & "")
            Dim CON_NAMEI As String = Trim(ds.Tables(1).Rows(0)("CON_NAMEI") & "")
            Dim ADDRESSI As String = Trim(ds.Tables(1).Rows(0)("ADDRESSI") & "")
            Dim ADDRESSII As String = Trim(ds.Tables(1).Rows(0)("ADDRESSII") & "")
            Dim ADDRESSIII As String = Trim(ds.Tables(1).Rows(0)("ADDRESSIII") & "")
            Dim ADDRESSIV As String = Trim(ds.Tables(1).Rows(0)("ADDRESSIV") & "")
            Dim CON_DETS As String = Trim(ds.Tables(1).Rows(0)("CON_DETS") & "")
            Dim CON_FOR As String = Trim(ds.Tables(1).Rows(0)("CON_FOR") & "")
            Dim FromTo As String = Trim(ds.Tables(0).Rows(0)("From") & "")
            Dim Tos As String = Trim(ds.Tables(0).Rows(0)("To") & "")
            Dim Transporter As String = Trim(ds.Tables(0).Rows(0)("Transporter") & "")
            Dim p1 As New ReportParameter("SlipNo", SlipNo)
            Dim p2 As New ReportParameter("SLIPDATE", SLIPDATE)
            Dim p3 As New ReportParameter("VEHICLENO", VEHICLENO)
            Dim p4 As New ReportParameter("SLIPTYPE", SLIPTYPE)
            Dim p5 As New ReportParameter("PARTYNAME", PARTYNAME)
            Dim p6 As New ReportParameter("DRIVERNAME", DRIVERNAME)
            Dim p7 As New ReportParameter("LRNO", LRNO)
            Dim p8 As New ReportParameter("LR_DATE", LR_DATE)
            'Dim p9 As New ReportParameter("Commodity", Commodity)
            Dim p11 As New ReportParameter("REMARKS", REMARKS)
            Dim p12 As New ReportParameter("con_Name", CON_NAME)
            Dim p13 As New ReportParameter("AddressI", ADDRESSI)

            Dim p14 As New ReportParameter("CON_NAMEI", CON_NAMEI)
            Dim p15 As New ReportParameter("ADDRESSII", ADDRESSII)
            Dim p16 As New ReportParameter("ADDRESSIII", ADDRESSIII)
            Dim p17 As New ReportParameter("ADDRESSIV", ADDRESSIV)
            Dim p18 As New ReportParameter("CON_DETS", CON_DETS)
            Dim p19 As New ReportParameter("CON_FOR", CON_FOR)
            Dim p20 As New ReportParameter("JO_NO", JO_NO)
            Dim p21 As New ReportParameter("FromTo", FromTo)
            Dim p22 As New ReportParameter("Tos", Tos)
            Dim p23 As New ReportParameter("UserName", UserName)
            Dim p24 As New ReportParameter("Transporter", Transporter)

            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24})

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
