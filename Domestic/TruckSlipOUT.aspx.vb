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
            db.sub_ExecuteNonQuery("Delete from Temp_truck_out_Domestic Where ADDED_BY=" & Session("UserId_DomCFS") & "")
            txtslipdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtlrdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Filldropdown()
            sub_CreateTable()
            Add(sender, e)
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

        Dim dtRow2 As DataRow = dtDepoContainer.NewRow

        grdcontainer.DataSource = Nothing
        grdcontainer.DataSource = dtDepoContainer
        grdcontainer.DataBind()
        Session("table_DepoContainer") = dtDepoContainer

    End Sub

    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_truck_fill_pecent_slip_OUT_Domestic")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If

            ds = db.sub_GetDataSets("USP_TRUCK_FILL_Domestic")
            If ds.Tables(0).Rows.Count > 0 Then
                ddlparty.DataSource = ds.Tables(0)
                ddlparty.DataTextField = "agentName"
                ddlparty.DataValueField = "agentID"
                ddlparty.DataBind()
                ddlparty.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                ddlcontype.DataSource = ds.Tables(1)
                ddlcontype.DataTextField = "ContainerType"
                ddlcontype.DataValueField = "ContainerTypeID"
                ddlcontype.DataBind()
                ddlcontype.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            ddlFromLocation.DataSource = ds.Tables(2)
            ddlFromLocation.DataTextField = "Location"
            ddlFromLocation.DataValueField = "LocationID"
            ddlFromLocation.DataBind()
            ddlFromLocation.Items.Insert(0, New ListItem("--Select--", 0))

            ddlToLocation.DataSource = ds.Tables(2)
            ddlToLocation.DataTextField = "Location"
            ddlToLocation.DataValueField = "LocationID"
            ddlToLocation.DataBind()
            ddlToLocation.Items.Insert(0, New ListItem("--Select--", 0))
            ddlFromLocation.SelectedValue = 292

            ddlTransporterName.DataSource = ds.Tables(4)
            ddlTransporterName.DataTextField = "TransName"
            ddlTransporterName.DataValueField = "TransID"
            ddlTransporterName.DataBind()
            ddlTransporterName.Items.Insert(0, New ListItem("--Select--", 0))

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
                ddlToLocation.SelectedValue = 0
                Exit Sub
            End If
            strSql = ""
            strSql += "USP_TRAILER_CHECKING_DOMESTIC '" & Trim(txtVehilceNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If Not dt.Rows.Count > 0 And Trim(txtlrNO.Text & "") = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter LR No');", True)
                txtlrNO.Focus()
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If
            If Not Trim(txtlrNO.Text & "") = "" Then
                If Trim(txtlrdate.Text & "") = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter LR Date');", True)
                    txtlrdate.Focus()
                    btnSave.Text = "Save"
                    btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                    Exit Sub
                End If
            End If
            Dim dtDepoContainer As New DataTable
            Dim intRows As Integer = 0
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)

            If Not dtDepoContainer.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Add Batch Details first');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If
            Dim param As New SqlParameter()
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@SLIP_DATE", Convert.ToDateTime(Trim(txtslipdate.Text & "")).ToString("yyyy-MM-dd HH:mm"))
            cmd.Parameters.AddWithValue("@SLIP_IN_NO", Trim(txtSlipNoIN.Text & ""))
            cmd.Parameters.AddWithValue("@ADDED_BY", Session("UserId_DomCFS"))
            If Trim(txtremarks.Text & "") = "" Then
                cmd.Parameters.AddWithValue("@REMARKS", Trim(txtremarks.Text & ""))
            Else
                cmd.Parameters.AddWithValue("@REMARKS", Replace(Trim(txtremarks.Text & ""), "'", "''"))
            End If
            cmd.Parameters.AddWithValue("@FROMID", Val(ddlFromLocation.SelectedValue & ""))
            cmd.Parameters.AddWithValue("@TOID", Val(ddlToLocation.SelectedValue & ""))
            cmd.Parameters.AddWithValue("@LRNO", Trim(txtlrNO.Text & ""))
            cmd.Parameters.AddWithValue("@LRDATE", Convert.ToDateTime(Trim(txtlrdate.Text & "")).ToString("yyyy-MM-dd HH:mm"))
            param.ParameterName = "@PT_DOMESTICTRUCKOUT"
            param.Value = dtDepoContainer
            param.TypeName = "PT_DOMESTICTRUCKOUT"
            param.SqlDbType = SqlDbType.Structured
            cmd.Parameters.Add(param)
            Dim strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("SqlConnString_Domestic").ConnectionString
            Dim con As New SqlConnection(strConnString)
            cmd.CommandText = "USP_INSERT_TRUCK_OUT_DomesticTruck"
            cmd.Connection = con
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            Dim dtMNR As New DataTable
            Try
                con.Open()
                da.Fill(dtMNR)
            Catch ex As Exception
                Response.Write(ex.Message)
                If con.State = ConnectionState.Open Then con.Close()
                con.Dispose()
            Finally
                If con.State = ConnectionState.Open Then con.Close()
                con.Dispose()
            End Try
            If dtMNR.Rows.Count > 0 Then
                txtSlipNoPrint.Text = dtMNR.Rows(0)("SLIP_OUT_NO")
                Clear(sender, e)
                txtSlipNoIN.Text = ""
                lblSession.Text = "Record Saved successfully for Slip No " & Val(txtSlipNoPrint.Text) & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not saved successfully');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
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
            db.sub_ExecuteNonQuery("Delete from Temp_truck_out_Domestic Where ADDED_BY=" & Session("UserId_DomCFS") & "")
            txtslipdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtlrdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Add(sender, e)
            ddlSlipType.SelectedValue = 0
            txtVehilceNo.Text = ""
            ddlparty.SelectedValue = 0
            txtdriver.Text = ""
            txtlicense.Text = ""
            txtcontact.Text = ""
            txtlrNO.Text = ""
            txtremarks.Text = ""
            ddlToLocation.SelectedValue = 0
            txtBatchNo.Text = ""
            txtRakeNo.Text = ""

            txtPkgs.Text = ""
            txtWeight.Text = ""
            ddlCommodity.SelectedValue = 0
            sub_CreateTable()
            UpdatePanel1.Update()
            up_grid.Update()
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtSlipNoIN_TextChanged(sender As Object, e As EventArgs) Handles txtSlipNoIN.TextChanged
        Try
            Clear(sender, e)
            strSql = ""
            strSql += "USP_SLIP_IN_NO_TEXT_CHANGED_Domestic " & Val(txtSlipNoIN.Text & "") & "," & Session("UserId_DomCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddlSlipType.SelectedValue = Trim(dt.Rows(0)("SLIP_TYPE") & "")
                txtVehilceNo.Text = Trim(dt.Rows(0)("VEHICLE_NO") & "")
                ddlparty.SelectedValue = Trim(dt.Rows(0)("AGENTID") & "")
                txtdriver.Text = Trim(dt.Rows(0)("DRIVER_NAME") & "")
                txtlicense.Text = Trim(dt.Rows(0)("LICENSE_NO") & "")
                txtcontact.Text = Trim(dt.Rows(0)("CONTACT_NO") & "")
                txtlrNO.Text = Trim(dt.Rows(0)("LR_NO") & "")
                txtlrdate.Text = Trim(dt.Rows(0)("SlipDate") & "")
                ddlTransporterName.SelectedValue = Trim(dt.Rows(0)("TransID") & "")
                'Add(sender, e)
                'txtcontainer.Focus()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found!.');", True)
                txtSlipNoIN.Text = ""
                txtSlipNoIN.Focus()
                Clear(sender, e)
                Exit Sub
            End If
            UpdatePanel1.Update()
            up_grid.Update()
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnTruckOut_Click(sender As Object, e As EventArgs)
        Try
            Clear(sender, e)
            txtSlipNoIN.Text = ""
            strSql = ""
            strSql += "select * from Temp_Truck_Slip_Out where UserID='" & Session("UserId_DomCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtSlipNoIN.Text = Trim(dt.Rows(0)("Slip_In_No") & "")
                Call txtSlipNoIN_TextChanged(sender, e)
            End If
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtBatchNo_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_BATCH_NO_TEXT_CHANGE_TRUCK_OUT '" & Trim(txtBatchNo.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Batch No Already out');", True)
                txtBatchNo.Text = ""
                txtPkgs.Text = ""
                txtRakeNo.Text = ""
                txtWeight.Text = ""
                ddlCommodity.SelectedValue = 0
                Exit Sub
            Else
                If ds.Tables(1).Rows.Count > 0 Then
                    txtPkgs.Text = Val(ds.Tables(1).Rows(0)("BatchPkgs") & "")
                    txtRakeNo.Text = Trim(ds.Tables(1).Rows(0)("TrainNo") & "")
                    txtWeight.Text = Val(ds.Tables(1).Rows(0)("BatchWeight") & "")
                    ddlCommodity.SelectedValue = Val(ds.Tables(1).Rows(0)("Commodity") & "")
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid Batch No');", True)
                    txtBatchNo.Text = ""
                    txtPkgs.Text = ""
                    txtRakeNo.Text = ""
                    txtWeight.Text = ""
                    ddlCommodity.SelectedValue = 0
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
