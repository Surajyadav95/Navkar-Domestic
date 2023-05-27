Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim TariffID, TariffIDView As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserIDPRE_Bond") Is Nothing Then
        '    Session("UserIDPRE_Bond") = Request.Cookies("UserIDPRE_Bond").Value
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
            fill_carting_details()
            db.sub_ExecuteNonQuery("Delete from Temp_Container_Search Where UserID=" & Session("UserId_DomCFS") & "")
        End If
    End Sub  
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function

     
    Private Sub fill_carting_details()

        Dim strSql As String
        Dim dt As New DataTable
        strSql = ""
        strSql = " usp_Domestic_grid_search '" & Trim(txtcontainerNo.Text & "") & "'"
        ds = db.sub_GetDataSets(strSql)
        grdLoadedIn.DataSource = ds.Tables(0)
        grdLoadedIn.DataBind()

        grdEmptyIn.DataSource = ds.Tables(1)
        grdEmptyIn.DataBind()

        grdEmprtOut.DataSource = ds.Tables(2)
        grdEmprtOut.DataBind()

        grdGatePass.DataSource = ds.Tables(3)
        grdGatePass.DataBind()

        grdTruckOut.DataSource = ds.Tables(4)
        grdTruckOut.DataBind()

        grdDestuff.DataSource = ds.Tables(5)
        grdDestuff.DataBind()

        grdLoadedSheet.DataSource = ds.Tables(6)
        grdLoadedSheet.DataBind()
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_Container_Search where userid='" & Session("UserId_DomCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows.Count > 0 Then
                    txtcontainerNo.Text = Trim(dt.Rows(0)("ContainerNo") & "")
                Else
                    txtcontainerNo.Text = ""
                    Exit Sub
                End If
                Call txtcontainerNo_TextChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtcontainerNo_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_SEARCH_CONTAINER_SEARCH'" & Trim(txtcontainerNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtJoNo.Text = Trim(dt.Rows(0)("JONO") & "")
                txtSize.Text = Trim(dt.Rows(0)("SIZE") & "")
                txttype.Text = Trim(dt.Rows(0)("TYPE") & "")
                txtArrivalDate.Text = Trim(dt.Rows(0)("ARRIVAL Date") & "")
                txtJoDate.Text = Trim(dt.Rows(0)("JO Date") & "")
                txtbeno.Text = Trim(dt.Rows(0)("BENO") & "")
                txtbedate.Text = Trim(dt.Rows(0)("BE Date") & "")
                txtImporter.Text = Trim(dt.Rows(0)("ImporterName") & "")
                txtCha.Text = Trim(dt.Rows(0)("CHANAME") & "")
                txtCustomer.Text = Trim(dt.Rows(0)("AGNAME") & "")
                txtLineName.Text = Trim(dt.Rows(0)("SLNAME") & "")
                txttransporter.Text = Trim(dt.Rows(0)("TransName") & "")
                txtCargo.Text = (Trim(dt.Rows(0)("CARGODESCRIPTION") & ""))
                txtSealNo.Text = Trim(dt.Rows(0)("SealNo") & "")
                txtDriverCode.Text = Trim(dt.Rows(0)("DriverCode") & "")
                txttrailerNo.Text = Trim(dt.Rows(0)("TrailerNo") & "")
                fill_carting_details()
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
