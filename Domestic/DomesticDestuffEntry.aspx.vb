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
    Dim VendorID As String
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

            txtGateInDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtArrivalDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Call sub_CreateTable()
            grid1()
            Filldropdown()
            ddlStuffingType.Focus()
        End If
    End Sub
     
     
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function

    Protected Sub grid1()
        Try
            strSql = ""
            strSql += "get_spcombofill_StuffingType_Domestic"
            ds = db.sub_GetDataSets(strSql)
            ddllocation.DataSource = ds.Tables(1)
            ddllocation.DataTextField = "WHName"
            ddllocation.DataValueField = "WHID"
            ddllocation.DataBind()
            ddllocation.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "get_spcombofill_StuffingType_Domestic"
            ds = db.sub_GetDataSets(strSql)
            ddlStuffingType.DataSource = ds.Tables(0)
            ddlStuffingType.DataTextField = "Name"
            ddlStuffingType.DataValueField = "ID"
            ddlStuffingType.DataBind()
            ddlStuffingType.Items.Insert(0, New ListItem("--Select--", 0))
            ddlEquipmentType.DataSource = ds.Tables(2)
            ddlEquipmentType.DataTextField = "Equipment"
            ddlEquipmentType.DataValueField = "ID"
            ddlEquipmentType.DataBind()
            ddlEquipmentType.Items.Insert(0, New ListItem("--Select--", 0))
            ddlVendorName.DataSource = ds.Tables(3)
            ddlVendorName.DataTextField = "Name"
            ddlVendorName.DataValueField = "VendorId"
            ddlVendorName.DataBind()
            ddlVendorName.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlStuffingType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ddllocation.SelectedValue = 0
            txtStuffedContainerNo.Text = ""
            If ddlStuffingType.SelectedValue = "1" Then
                divStuffedl.Attributes.Add("style", "display:none")
                divStuffed.Attributes.Add("style", "display:block")
            ElseIf ddlStuffingType.SelectedValue = "3" Then
                divStuffedl.Attributes.Add("style", "display:none")
                divStuffed.Attributes.Add("style", "display:block")
            ElseIf ddlStuffingType.SelectedValue = "2" Then
                divStuffedl.Attributes.Add("style", "display:block")
                divStuffed.Attributes.Add("style", "display:none")
            ElseIf ddlStuffingType.SelectedValue = "5" Then
                divStuffedl.Attributes.Add("style", "display:none")
                divStuffed.Attributes.Add("style", "display:block")
            ElseIf ddlStuffingType.SelectedValue = "4" Then
                divStuffedl.Attributes.Add("style", "display:block")
                divStuffed.Attributes.Add("style", "display:none")
            Else
                divStuffedl.Attributes.Add("style", "display:none")
                divStuffed.Attributes.Add("style", "display:none")
            End If
            lnksearch.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtStuffedContainerNo_TextChanged(sender As Object, e As EventArgs)
        Try
            If Trim(txtStuffedContainerNo.Text) <> "" Then
                Dim dtIGM As New DataTable
                strSql = ""
                strSql = "Select top(1) * from Own_Ctr_Master where ContainerNo='" & Trim(txtStuffedContainerNo.Text) & "'"
                dtIGM = db.sub_GetDatatable(strSql)
                If dtIGM.Rows.Count > 0 Then
                    Dim dtJoNO As DataTable
                    strSql = "select MAX(EntryID) [JO No] from Own_Ctr_Master where ContainerNo='" & Trim(txtStuffedContainerNo.Text) & "'"
                    dtJoNO = db.sub_GetDatatable(strSql)
                    If dtJoNO.Rows.Count > 0 Then
                        lblSEntryID.Text = Trim(dtJoNO.Rows(0)("JO No") & "")
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified Container no is not found in Domestic container data master. Cannot proceed');", True)
                    txtStuffedContainerNo.Text = ""
                    txtStuffedContainerNo.Focus()
                    Exit Sub
                End If

                strSql = ""
                strSql = "Select top(1) * from Domestic_Stuffing s inner join userdetails u on u.userid=s.added_by  where S_Container_No='" & Trim(txtStuffedContainerNo.Text) & "' and S_Entry_ID='" & Trim(lblSEntryID.Text) & "' and Iscancel=0 order by AUTO_ID  DESC "
                dtIGM = db.sub_GetDatatable(strSql)
                If dtIGM.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(Specified container no is already stuffed dated on ');" & dtIGM.Rows(0)("Stuffed_Date") & " by " & Trim(dtIGM.Rows(0)("username")) & "", True)
                    Exit Sub
                End If
            End If
            txtStuffedQty.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtStuffedQty_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(txtStuffedQty.Text & "") = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Qty cannot be zero');", True)
                txtStuffedQty.Text = ""
                txtStuffedQty.Focus()
                Exit Sub
            End If
            If Val(txtpkgs.Text) - Val(txtStuffedQty.Text) >= 0 Then
                txtShortPkgs.Text = Val(txtpkgs.Text) - Val(txtStuffedQty.Text)
            Else
                txtShortPkgs.Text = 0
            End If
            If Val(txtStuffedQty.Text) - Val(txtpkgs.Text) >= 0 Then
                txtExcessPkgs.Text = Val(txtStuffedQty.Text) - Val(txtpkgs.Text)
            Else
                txtExcessPkgs.Text = 0
            End If
            txtStuffedWt.Text = Format(Val(Val(txtWeight.Text) / Val(txtpkgs.Text)) * Val(txtStuffedQty.Text), "0.0")
            txtStuffedWt.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtStuffedWt_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(txtStuffedWt.Text & "") = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Wt cannot be greater than Balance Weight');", True)
                txtStuffedWt.Text = ""
                txtStuffedWt.Focus()
                Exit Sub
            End If
            txtShortPkgs.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CreateTable()
        Try

            Dim dtDomContainer As New DataTable
            dtDomContainer.Columns.Clear()
            Session("table_DomesticContainerStuffing") = ""

            'dtDomContainer.Rows.Count(-1)

            dtDomContainer.Columns.Add("ContainerNo")
            dtDomContainer.Columns.Add("SEntryID")
            dtDomContainer.Columns.Add("WareHouse")
            dtDomContainer.Columns.Add("WareHouse_ID")
            dtDomContainer.Columns.Add("StuffQty")
            dtDomContainer.Columns.Add("StuffWt")
            dtDomContainer.Columns.Add("ShortPkgs")
            dtDomContainer.Columns.Add("ExcessPkgs")
            dtDomContainer.Columns.Add("DCA_No")
            dtDomContainer.Columns.Add("IGM_No")
            dtDomContainer.Columns.Add("Item_No")
            dtDomContainer.Columns.Add("Seal_No")
            dtDomContainer.Columns.Add("Cargo_Desc")
            dtDomContainer.Columns.Add("Area")
            dtDomContainer.Columns.Add("NoLabours")
            dtDomContainer.Columns.Add("VendorName")
            dtDomContainer.Columns.Add("VendorID")
            dtDomContainer.Columns.Add("EquipmentType")
            dtDomContainer.Columns.Add("EquipmentTypeID")

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()
            Session("table_DomesticContainerStuffing") = dtDomContainer

        Catch ex As Exception
            '  lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim dblStwt As Double = 0, dblstQty As Double = 0
            Dim dblqty1 As Double = 0
            For Each row In grdcontainer.Rows
                dblStwt = dblStwt + Val(CType(row.FindControl("lblStuffWt"), Label).Text & "")
                dblstQty = dblstQty + Val(CType(row.FindControl("lblStuffQty"), Label).Text & "")
            Next
            If Trim(txtStuffedQty.Text & "") = "" Or Val(txtStuffedQty.Text) = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Qty cannot be zero. Cannot proceed');", True)
                txtStuffedQty.Focus()
                Exit Sub
            End If
            If Trim(txtStuffedWt.Text & "") = "" Or Val(txtStuffedWt.Text) = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Weight cannot be zero. Cannot proceed');", True)
                txtStuffedWt.Focus()
                Exit Sub
            End If
            dblStwt += Val(txtStuffedWt.Text)
            dblstQty += Val(txtStuffedQty.Text)
            If Val(txtBalWeight.Text) < dblStwt Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Weight cannot be greater than Balance Weight. Cannot proceed');", True)
                txtStuffedWt.Focus()
                Exit Sub
            End If
            If Val(txtBalPkgs.Text) < dblstQty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Qty cannot be greater than Balance Qty. Cannot proceed');", True)
                txtStuffedQty.Focus()
                Exit Sub
            End If
            For Each row In grdcontainer.Rows
                If Trim(txtStuffedContainerNo.Text & "") <> "" Then
                    If txtStuffedContainerNo.Text = Trim(CType(row.FindControl("lblcontainerno"), Label).Text & "") Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Container No already added. Cannot Proceed!');", True)
                        txtcontainerNo.Focus()
                        Exit Sub
                    End If
                End If
                If ddllocation.SelectedValue <> 0 Then
                    If Val(ddllocation.SelectedValue) = Trim(CType(row.FindControl("lblWareHouseID"), Label).Text & "") Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Warehouse already added. Cannot Proceed!');", True)
                        ddllocation.Focus()
                        Exit Sub
                    End If
                End If
            Next           

            Dim dtDomContainer As New DataTable
            Dim intRows As Integer = 0

            dtDomContainer = DirectCast(Session("table_DomesticContainerStuffing"), DataTable)
            intRows = dtDomContainer.Rows.Count
            Dim dtRow As DataRow = dtDomContainer.NewRow

            dtRow.Item("ContainerNo") = txtStuffedContainerNo.Text
            dtRow.Item("SEntryID") = lblSEntryID.Text

            If ddllocation.SelectedValue = 0 Then
                dtRow.Item("WareHouse") = ""
            Else
                dtRow.Item("WareHouse") = ddllocation.SelectedItem.Text
            End If

            dtRow.Item("WareHouse_ID") = ddllocation.SelectedValue
            dtRow.Item("StuffQty") = txtStuffedQty.Text
            dtRow.Item("StuffWt") = txtStuffedWt.Text
            dtRow.Item("ShortPkgs") = txtShortPkgs.Text
            dtRow.Item("ExcessPkgs") = txtExcessPkgs.Text

            dtRow.Item("DCA_No") = txtDCAJoNo.Text
            dtRow.Item("IGM_No") = txtIgmNo.Text
            dtRow.Item("Item_No") = txtItemNo.Text
            dtRow.Item("Seal_No") = txtSealNo.Text
            dtRow.Item("Cargo_Desc") = txtCargoDesc.Text
            dtRow.Item("Area") = txtArea.Text
            dtRow.Item("NoLabours") = txtNoLabours.Text
            If ddlVendorName.SelectedValue = 0 Then
                dtRow.Item("VendorName") = ""
            Else
                dtRow.Item("VendorName") = ddlVendorName.SelectedItem.Text
            End If
            dtRow.Item("VendorID") = ddlVendorName.SelectedValue
            If ddlEquipmentType.SelectedValue = 0 Then
                dtRow.Item("EquipmentType") = ""
            Else
                dtRow.Item("EquipmentType") = ddlEquipmentType.SelectedItem.Text
            End If            
            dtRow.Item("EquipmentTypeID") = ddlEquipmentType.SelectedValue

            dtDomContainer.Rows.Add(dtRow)

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()

            Session("table_DomesticContainerStuffing") = dtDomContainer
            
            txtStuffedContainerNo.Text = ""
            lblSEntryID.Text = ""
            ddllocation.SelectedValue = 0
            txtStuffedQty.Text = ""
            txtStuffedWt.Text = ""
            txtShortPkgs.Text = ""
            txtExcessPkgs.Text = ""
            txtDCAJoNo.Text = ""
            txtIgmNo.Text = ""
            txtItemNo.Text = ""
            txtCargoDesc.Text = ""
            txtSealNo.Text = ""
            txtArea.Text = ""
            txtNoLabours.Text = ""
            ddlVendorName.SelectedValue = 0
            ddlEquipmentType.SelectedValue = 0

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)
            Dim dtDomContainer As New DataTable
            Dim intRows As Integer = 0

            dtDomContainer = DirectCast(Session("table_DomesticContainerStuffing"), DataTable)
            For i = 0 To dtDomContainer.Rows.Count - 1
                If Not Trim(CType(gr.FindControl("lblcontainerno"), Label).Text) = "" Then
                    If dtDomContainer.Rows(i)("ContainerNo") = Trim(CType(gr.FindControl("lblcontainerno"), Label).Text) Then
                        dtDomContainer.Rows.Remove(dtDomContainer.Rows(i))
                    End If
                End If
                If Not Trim(CType(gr.FindControl("lblWareHouseID"), Label).Text) = 0 Then
                    If dtDomContainer.Rows(i)("WareHouse_ID") = Trim(CType(gr.FindControl("lblWareHouseID"), Label).Text) Then
                        dtDomContainer.Rows.Remove(dtDomContainer.Rows(i))
                    End If
                End If
            Next
            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()

            Session("table_DomesticContainerRestuff") = dtDomContainer
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Val(ddlStuffingType.SelectedValue) = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select stuffing type first');", True)
                ddlStuffingType.Focus()
                Exit Sub
            End If
            If (Val(ddlStuffingType.SelectedValue) = 2 Or Val(ddlStuffingType.SelectedValue) = 4) Then
                If Val(ddllocation.SelectedValue) = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select warehouse first');", True)
                    ddllocation.Focus()
                    Exit Sub
                End If
            End If
            strSql = "SELECT isnull(MAX(Stuffed_ID),0)+1 as EntryID FROM Domestic_Stuffing"
            dt5 = db.sub_GetDatatable(strSql)
            If dt5.Rows.Count > 0 Then
                txtstuffingID.Text = dt5.Rows(0)("EntryID")
            Else
                txtstuffingID.Text = 1
            End If
            For Each row In grdcontainer.Rows
                strSql = ""
                strSql += "USP_INSERT_DOMESTIC_STUFFING '" & Trim(txtstuffingID.Text & "") & "','" & Convert.ToDateTime(Trim(txtGateInDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                strSql += "'" & Trim(txtcontainerNo.Text & "") & "','" & Trim(txtjono.Text & "") & "','" & Trim(CType(row.FindControl("lblcontainerno"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblSEntryID"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblStuffQty"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblStuffWt"), Label).Text & "") & "',"
                strSql += "'" & Session("UserId_DomCFS") & "','" & Trim(CType(row.FindControl("lblWareHouseID"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblDCANo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblItemNo"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblIGMNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblCargoDesc"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblSealNo"), Label).Text & "") & "','" & lblCUSTOMERID.Text & "',"
                strSql += "'" & Trim(ddlStuffingType.SelectedValue & "") & "','" & Trim(CType(row.FindControl("lblArea"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblShortPkgs"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblExcessPkgs"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblNoofLabours"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblVendorID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblEquipmentTypeID"), Label).Text & "") & "'"

                db.sub_ExecuteNonQuery(strSql)
            Next
            Clear()
            lblSession.Text = "Record Saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            Call sub_CreateTable()
            txtstuffingID.Text = ""
            txtGateInDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtArrivalDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtcontainerNo.Text = ""
            lblSEntryID.Text = ""            
            lblCUSTOMERID.Text = ""            
            ddlStuffingType.SelectedValue = 0
            txtjono.Text = ""
            txtcustomer.Text = ""
            txtpkgs.Text = ""
            txtWeight.Text = ""
            txtBalPkgs.Text = ""
            txtBalWeight.Text = ""

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_Destuff_Container where UserID=" & Session("UserId_DomCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtcontainerNo.Text = Trim(dt.Rows(0)("ContainerNo") & "")
                txtjono.Text = Trim(dt.Rows(0)("JONo") & "")
                strSql = ""
                strSql += "usp_domestic_stuffing_show '" & Trim(txtcontainerNo.Text & "") & "','" & Trim(txtjono.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    lblCUSTOMERID.Text = Trim(dt.Rows(0)("CUSTOMERID") & "")
                    'txtArrivalDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Arrival Date")) & "").ToString("yyyy-MM-ddTHH:mm")
                    txtcustomer.Text = Trim(dt.Rows(0)("AGName") & "")
                    txtpkgs.Text = Trim(dt.Rows(0)("PKGS") & "")
                    txtWeight.Text = Trim(dt.Rows(0)("WEIGHT") & "")
                    txtBalPkgs.Text = Trim(dt.Rows(0)("BalanceQty") & "")
                    txtBalWeight.Text = Trim(dt.Rows(0)("BalanceWeight") & "")
                    txtIgmNo.Text = Trim(dt.Rows(0)("IGMNO") & "")
                    txtItemNo.Text = Trim(dt.Rows(0)("ITEMNO") & "")
                    txtDCAJoNo.Text = Trim(dt.Rows(0)("REFJONO") & "")
                    txtCargoDesc.Text = Trim(dt.Rows(0)("CARGODESCRIPTION") & "")
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found!');", True)
                    txtcontainerNo.Text = ""
                    txtjono.Text = ""
                    txtcontainerNo.Focus()
                    Exit Sub
                End If
            End If
            If ddlStuffingType.SelectedValue = 1 Or ddlStuffingType.SelectedValue = 3 Or ddlStuffingType.SelectedValue = 5 Then
                txtStuffedContainerNo.Focus()
            ElseIf ddlStuffingType.SelectedValue = 2 Or ddlStuffingType.SelectedValue = 4 Then
                ddllocation.Focus()
            Else
                ddlStuffingType.Focus()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtShortPkgs_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(txtShortPkgs.Text & "") > Val(txtBalPkgs.Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Short pkgs cannot be greater than Balance Pkgs');", True)
                txtStuffedWt.Text = ""
                txtStuffedWt.Focus()
                Exit Sub
            End If
            txtExcessPkgs.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtcbm_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtcbm.Text <> "" Then
                txtArea.Text = Val(txtcbm.Text) * 3.5396 * 10.76
            Else
                txtArea.Text = ""
            End If
            txtNoLabours.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
