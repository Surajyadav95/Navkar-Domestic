Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt5 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim ID, GodownIDView As String
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
            txtslipdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtlrdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Filldropdown()
            txtJONo.Focus()
            sub_CreateTable()
        End If

    End Sub
    Private Sub sub_CreateTable()
        Dim dtDepoContainer As New DataTable

        dtDepoContainer.Columns.Clear()

        Session("table_DepoContainer") = ""

        dtDepoContainer.Columns.Add("BatchNo")
        dtDepoContainer.Columns.Add("RakeNo")
        dtDepoContainer.Columns.Add("BatchPkgs")
        dtDepoContainer.Columns.Add("BatchWeight")
        dtDepoContainer.Columns.Add("Commodity")
        dtDepoContainer.Columns.Add("CommodityID")
        dtDepoContainer.Columns.Add("InvoiceNo")
        Dim dtRow2 As DataRow = dtDepoContainer.NewRow

        grdcontainer.DataSource = Nothing
        grdcontainer.DataSource = dtDepoContainer
        grdcontainer.DataBind()
        Session("table_DepoContainer") = dtDepoContainer

    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_truck_fill_domestic_slip")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If

            ds = db.sub_GetDataSets("USP_DOMESTIC_TRUCK_FILL")
            If ds.Tables(0).Rows.Count > 0 Then
                ddlparty.DataSource = ds.Tables(0)
                ddlparty.DataTextField = "agentName"
                ddlparty.DataValueField = "agentID"
                ddlparty.DataBind()
                ddlparty.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            ddlFromLocation.DataSource = ds.Tables(1)
            ddlFromLocation.DataTextField = "Location"
            ddlFromLocation.DataValueField = "LocationID"
            ddlFromLocation.DataBind()
            ddlFromLocation.Items.Insert(0, New ListItem("--Select--", 0))

            ddlToLocation.DataSource = ds.Tables(1)
            ddlToLocation.DataTextField = "Location"
            ddlToLocation.DataValueField = "LocationID"
            ddlToLocation.DataBind()
            ddlToLocation.Items.Insert(0, New ListItem("--Select--", 0))
            ddlToLocation.SelectedValue = 292

            ddlTransporterName.DataSource = ds.Tables(2)
            ddlTransporterName.DataTextField = "TransName"
            ddlTransporterName.DataValueField = "TransID"
            ddlTransporterName.DataBind()
            ddlTransporterName.Items.Insert(0, New ListItem("--Select--", 0))
            
            ds = db.sub_GetDataSets("USP_TRUCK_FILL_Domestic")
            ddlCommodity.DataSource = ds.Tables(3)
            ddlCommodity.DataTextField = "Commodity"
            ddlCommodity.DataValueField = "ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Val(ddlFromLocation.SelectedValue) = Val(ddlToLocation.SelectedValue) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Please select from & to location different!');", True)
                ddlFromLocation.SelectedValue = 0
                Exit Sub
            End If
            Dim VehicleNo As String = ""
            If txttrailerNo1.Text <> "" Then
                VehicleNo = txttrailerNo1.Text
            Else
                VehicleNo = txtVehilceNo.Text

            End If
            Dim dblCount As Double = 0
            For Each row In grdcontainer.Rows
             
                dblCount += 1
            Next
     
            'strSql = ""
            'strSql += "USP_DOMESTIC_GATE_IN_OUT_DUPLICATION_VALIDATION '" & Trim(VehicleNo) & "','T'"
            'ds = db.sub_GetDataSets(strSql)
            'If ds.Tables(0).Rows.Count > 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Truck is already in Inventory!');", True)
            '    txttrailerNo1.Text = ""
            '    txttrailerNo1.Focus()
            '    Exit Sub
            'End If
            strSql = "SELECT ISNULL(Max(SLIP_NO),0)+1 as SLIP_NO FROM DOMESTIC_TRUCK_IN "
            dt5 = db.sub_GetDatatable(strSql)
            If dt5.Rows.Count > 0 Then
                txtslipno.Text = dt5.Rows(0)("SLIP_NO")
            Else
                txtslipno.Text = 1
            End If
            For Each row In grdcontainer.Rows
                strSql = ""
                strSql += "USP_INSERT_DOMESTIC_TRUCK_IN'" & Trim(txtslipno.Text & "") & "','" & Convert.ToDateTime(Trim(txtslipdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlSlipType.SelectedItem.Text & "") & "',"
                strSql += "'" & Trim(VehicleNo) & "','" & Trim(ddlparty.SelectedValue & "") & "','" & Trim(txtdriver.Text & "") & "','" & Trim(txtlicense.Text & "") & "',"
                strSql += "'" & Trim(txtcontact.Text & "") & "','" & Trim(txtlrNO.Text & "") & "','" & Convert.ToDateTime(Trim(txtlrdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserId_DomCFS") & "','" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Trim(txtJONo.Text & "") & "',"
                strSql += "'" & Val(ddlFromLocation.SelectedValue & "") & "','" & Val(ddlToLocation.SelectedValue & "") & "','" & Trim(ddlTransporterName.SelectedValue & "") & "','" & Trim(CType(row.FindControl("lblBatchno"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblRakeNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblBatchPkgs"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblBatchWeight"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblCommodityID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblInvoiceNo"), Label).Text & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next
            If dblCount = 0 Then
                strSql = ""
                strSql += "USP_INSERT_DOMESTIC_TRUCK_IN'" & Trim(txtslipno.Text & "") & "','" & Convert.ToDateTime(Trim(txtslipdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlSlipType.SelectedItem.Text & "") & "',"
                strSql += "'" & Trim(VehicleNo) & "','" & Trim(ddlparty.SelectedValue & "") & "','" & Trim(txtdriver.Text & "") & "','" & Trim(txtlicense.Text & "") & "',"
                strSql += "'" & Trim(txtcontact.Text & "") & "','" & Trim(txtlrNO.Text & "") & "','" & Convert.ToDateTime(Trim(txtlrdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserId_DomCFS") & "','" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Trim(txtJONo.Text & "") & "',"
                strSql += "'" & Val(ddlFromLocation.SelectedValue & "") & "','" & Val(ddlToLocation.SelectedValue & "") & "','" & Trim(ddlTransporterName.SelectedValue & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
            End If



            txtSlipNoPrint.Text = dt5.Rows(0)("SLIP_NO")
            Clear(sender, e)
            txtJONo.Text = ""
            lblSession.Text = "Record Saved successfully Slip No " & Val(txtSlipNoPrint.Text) & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub SaveOk_ServerClick(sender As Object, e As EventArgs)
        Try
            lblPrintQue.Text = "Do you wish to print Slip?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Clear(sender As Object, e As EventArgs)
        Try
            txtslipdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtlrdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            ddlSlipType.SelectedValue = 0
            txtVehilceNo.Text = ""
            ddlparty.SelectedValue = 0
            txtdriver.Text = ""
            txtlicense.Text = ""
            txtcontact.Text = ""
            txtlrNO.Text = ""
            txtremarks.Text = ""
            ddlFromLocation.SelectedValue = 0
            'ddlToLocation.SelectedValue = 0

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtNOCNo_TextChanged(sender As Object, e As EventArgs) Handles txtJONo.TextChanged
        Try
            Clear(sender, e)
            strSql = ""
            strSql += "USP_JO_NO_CHANGE_TRUCK_IN '" & Trim(txtJONo.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlparty.SelectedValue = Val(ds.Tables(0).Rows(0)("CUSTOMERID"))
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('JO No not found');", True)
                txtJONo.Text = ""
                txtJONo.Focus()
                Exit Sub
            End If
            UpdatePanel1.Update()
            UpdatePanel2.Update()
            ddlSlipType.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnNOCList_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select top 1 * from Temp_JO_List_TruckIn where userid=" & Session("UserId_DomCFS") & " order by AddedOn desc"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtJONo.Text = Trim(dt.Rows(0)("JONo") & "")
                txtNOCNo_TextChanged(sender, e)
            End If
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txttrailerno_TextChanged(sender As Object, e As EventArgs)
        Try
            txttrailerNo1.Text = ""
            txtVehilceNo.Text = ""
            strSql = ""
            strSql = "Usp_gettrailern_In '" & Trim(txttrailerno.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txttrailerNo1.Text = Trim(dt.Rows(0)("trailername"))
                'ddltransporter.SelectedValue = Trim(dt.Rows(0)("TransID"))
                'txtdrivercode.Text = Trim(dt.Rows(0)("DriverCode"))
                divtrailer.Attributes.Add("style", "display:None")
                divtrai.Attributes.Add("style", "display:block")
            Else
                divtrailer.Attributes.Add("style", "display:block")
                divtrai.Attributes.Add("style", "display:None")
                'ddltransporter.SelectedValue = 0
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnadd_Click(sender As Object, e As EventArgs)
        Try
            Dim dtDepoContainer As New DataTable
            Dim intRows As Integer = 0
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)

            For i = 0 To dtDepoContainer.Rows.Count - 1
                If Trim(txtBatchNo.Text & "") = Trim(dtDepoContainer.Rows(i)("BatchNo") & "") Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Batch No already added!');", True)
                    txtBatchNo.Text = ""
                    txtPkgs.Text = ""
                    txtRakeNo.Text = ""
                    txtWeight.Text = ""
                    ddlCommodity.SelectedValue = 0
                    Exit Sub
                End If

            Next
            Dim dtRow As DataRow = dtDepoContainer.NewRow
            intRows = 0
            dtRow.Item("BatchNo") = Trim(txtBatchNo.Text & "")
            dtRow.Item("RakeNo") = Trim(txtRakeNo.Text & "")
            dtRow.Item("BatchPkgs") = Val(txtPkgs.Text & "")
            dtRow.Item("BatchWeight") = Val(txtWeight.Text & "")
            dtRow.Item("Commodity") = Trim(ddlCommodity.SelectedItem.Text & "")
            dtRow.Item("CommodityID") = Val(ddlCommodity.SelectedValue & "")
            dtRow.Item("InvoiceNo") = Trim(txtInvoiceNo.Text & "")

            dtDepoContainer.Rows.Add(dtRow)

            Session("table_DepoContainer") = dtDepoContainer
            Add(sender, e)
            txtBatchNo.Text = ""
            txtPkgs.Text = ""
            txtRakeNo.Text = ""
            txtWeight.Text = ""
            ddlCommodity.SelectedValue = 0
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Add(sender As Object, e As EventArgs)
        Try
            Dim dtDepoContainer As New DataTable
            Dim intRows As Integer = 0
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)
            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDepoContainer
            grdcontainer.DataBind()
            Session("table_DepoContainer") = dtDepoContainer
            up_grid.Update()
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkdelete_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkRemove.CommandArgument
            Dim dtDepoContainer As New DataTable
            Dim intRows As Integer = 0
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)
            For i = 0 To dtDepoContainer.Rows.Count - 1
                If Trim(AutoID & "") = Trim(dtDepoContainer.Rows(i)("BatchNo") & "") Then
                    dtDepoContainer.Rows(i).Delete()
                    Exit For
                End If
            Next
            Session("table_DepoContainer") = dtDepoContainer
            Add(sender, e)
            up_grid.Update()
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    
End Class