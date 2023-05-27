Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.IO
Imports ClosedXML.Excel
Imports System.Globalization

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim TariffID, TariffIDView As String
    Dim ed As New clsEncodeDecode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserID") Is Nothing Then
        '    Session("UserID") = Request.Cookies("UserIDPRE_Bond").Value
        '    'Session("Dept") = Request.Cookies("Dept").Value
        '    Session("UserNamePRE_Bond") = Request.Cookies("UserNamePRE_Bond").Value
        '    ''Session("PROFILEURL") = Request.Cookies("PROFILEURL").Value
        '    'Session("Location") = Request.Cookies("Location").Value
        '    ''Session("LOcationId") = Request.Cookies("LOcationId").Value
        '    'Session("ID") = Response.Cookies("ID").Value
        '    'Session("CompID") = Response.Cookies("CompID").Value
        '    'Session("Workyear") = Response.Cookies("Workyear").Value
        'End If
        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("Delete from Temp_Loaded_InD Where UserID=" & Session("UserID") & "")
            txtGateInDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            sub_CreateTable()
            sub_CreateTable1()

        End If

    End Sub
     
    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function
    Private Sub sub_CreateTable()
        Dim dtDepoContainer As New DataTable

        dtDepoContainer.Columns.Clear()

        Session("table_DepoContainer") = ""

        dtDepoContainer.Columns.Add("SrNo")
        dtDepoContainer.Columns.Add("Mode")
        dtDepoContainer.Columns.Add("DoNo")
        dtDepoContainer.Columns.Add("DoQty")
        dtDepoContainer.Columns.Add("Destination")
        dtDepoContainer.Columns.Add("BatchNo")
        dtDepoContainer.Columns.Add("Location")
        dtDepoContainer.Columns.Add("PFrom")
        dtDepoContainer.Columns.Add("TransportZone")
        dtDepoContainer.Columns.Add("Region")
        dtDepoContainer.Columns.Add("AllocationZone")
        dtDepoContainer.Columns.Add("SRPipeGrade")
        dtDepoContainer.Columns.Add("Thickness")
        dtDepoContainer.Columns.Add("Width")
        dtDepoContainer.Columns.Add("Weight")
        dtDepoContainer.Columns.Add("NetWt")
        dtDepoContainer.Columns.Add("Material")
        dtDepoContainer.Columns.Add("DelAge")
        dtDepoContainer.Columns.Add("Remarks")

        Dim dtRow2 As DataRow = dtDepoContainer.NewRow
 
        Session("table_DepoContainer") = dtDepoContainer

    End Sub

    Private Sub sub_CreateTable1()
        Dim dtRelianceContainer As New DataTable

        dtRelianceContainer.Columns.Clear()

        Session("table_RelianceContainer") = ""

        dtRelianceContainer.Columns.Add("SrNo")
        dtRelianceContainer.Columns.Add("ContainerNo")
        dtRelianceContainer.Columns.Add("ContainerType")
        dtRelianceContainer.Columns.Add("NoofBags")
        dtRelianceContainer.Columns.Add("Wt")
        dtRelianceContainer.Columns.Add("PRODUCTGRADE")
        dtRelianceContainer.Columns.Add("DESTINATION")
        dtRelianceContainer.Columns.Add("SHIPTOPARTYNAME")
        dtRelianceContainer.Columns.Add("TAXINVOICENO")
        dtRelianceContainer.Columns.Add("EWAYBILLDETAILS")
        dtRelianceContainer.Columns.Add("VALIDUPTO")
        dtRelianceContainer.Columns.Add("Size")
        dtRelianceContainer.Columns.Add("Seal")
        dtRelianceContainer.Columns.Add("Grade")
        dtRelianceContainer.Columns.Add("ContainerTypeID")
        dtRelianceContainer.Columns.Add("LocationID")
        Dim dtRow2 As DataRow = dtRelianceContainer.NewRow

        grdRelianceDets.DataSource = Nothing
        grdRelianceDets.DataSource = dtRelianceContainer
        grdRelianceDets.DataBind()
        Session("table_RelianceContainer") = dtRelianceContainer

    End Sub
  
    Protected Sub lnksearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_RRNo_LoadingSeach'" & Trim(txtRRNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                grdRelianceDets.DataSource = Nothing
                grdRelianceDets.DataSource = dt
                grdRelianceDets.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim dblGateInNo As Double = 0
        strSql = ""
        strSql += "select isnull(max(gatein_no),0)+1 gatein_no from Domestic_IN"
        dt = db.sub_GetDatatable(strSql)
        If dt.Rows.Count > 0 Then
            dblGateInNo = Val(dt.Rows(0)("gatein_no"))
        End If
        For Each row In grdRelianceDets.Rows
            strSql = ""
            strSql += "USP_INSERT_DOMESTIC_IN_By_rail " & dblGateInNo & ",'" & Convert.ToDateTime(Trim(txtGateInDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(CType(row.FindControl("lblJONO"), Label).Text & "") & "',"
            strSql += "'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & "0" & "','" & "0" & "',"
            strSql += "'" & "" & "','" & "" & "','" & "" & "',"
            strSql += "'" & "" & "','" & Session("UserId_DomCFS") & "','" & Val(CType(row.FindControl("lblNoofBags"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblWt"), Label).Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
        Next



        lblSession.Text = "Record saved successfully"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        UpdatePanel3.Update()
    End Sub
End Class
