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

            txtLoadingSheetDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtArrivalDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Call sub_CreateTable()
            Filldropdown()
            ddlLoadingType.Focus()
        End If
    End Sub        
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function           
    Protected Sub grid()
        Try
            strSql = ""
            strSql += ""
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_FILL_LoadingType_Domestic"
            ds = db.sub_GetDataSets(strSql)
            ddlLoadingType.DataSource = ds.Tables(0)
            ddlLoadingType.DataTextField = "Name"
            ddlLoadingType.DataValueField = "ID"
            ddlLoadingType.DataBind()
            ddlLoadingType.Items.Insert(0, New ListItem("--Select--", 0))

            ddlLocation.DataSource = ds.Tables(2)
            ddlLocation.DataTextField = "Location"
            ddlLocation.DataValueField = "LocationID"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem("--Select--", 0))

            ddlEquipment.DataSource = ds.Tables(3)
            ddlEquipment.DataTextField = "Equipment_Name"
            ddlEquipment.DataValueField = "AutoID"
            ddlEquipment.DataBind()
            ddlEquipment.Items.Insert(0, New ListItem("--Select--", 0))

            ddlvendorName.DataSource = ds.Tables(4)
            ddlvendorName.DataTextField = "Name"
            ddlvendorName.DataValueField = "VendorId"
            ddlvendorName.DataBind()
            ddlvendorName.Items.Insert(0, New ListItem("--Select--", 0))

            ddlVehicletype.DataSource = ds.Tables(5)
            ddlVehicletype.DataTextField = "trailerType"
            ddlVehicletype.DataValueField = "ID"
            ddlVehicletype.DataBind()
            ddlVehicletype.Items.Insert(0, New ListItem("--Select--", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlLoadingType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            txtStuffedContainerNo.Text = ""
            txtSize.Text = ""
            txtTruckNo.Text = ""
            If ddlLoadingType.SelectedValue = "1" Then
                divStuffedl.Attributes.Add("style", "display:none")
                divStuffed.Attributes.Add("style", "display:block")
                divSize.Attributes.Add("style", "display:block")

                'lblcriteria.Visible = True
                txtTruckNo.Visible = False
                txtStuffedContainerNo.Visible = True
            ElseIf ddlLoadingType.SelectedValue = "2" Then
                divStuffedl.Attributes.Add("style", "display:block")
                divStuffed.Attributes.Add("style", "display:none")
                divSize.Attributes.Add("style", "display:none")
                'lblcriteria.Text = "Ware House:"
                'lblcriteria.Visible = False
                txtTruckNo.Visible = True
                txtStuffedContainerNo.Visible = False
            ElseIf ddlLoadingType.SelectedValue = "3" Then
                divStuffedl.Attributes.Add("style", "display:none")
                divStuffed.Attributes.Add("style", "display:block")
                divSize.Attributes.Add("style", "display:block")

                'lblcriteria.Visible = True
                txtTruckNo.Visible = False
                txtStuffedContainerNo.Visible = True
            Else
                divStuffedl.Attributes.Add("style", "display:none")
                divStuffed.Attributes.Add("style", "display:none")
                divSize.Attributes.Add("style", "display:none")
                'lblcriteria.Text = "Ware House:"
                'lblcriteria.Visible = False
                txtTruckNo.Visible = False
                txtStuffedContainerNo.Visible = False
            End If
            If txtcontainerNo.Text = "" Then
                lnksearch.Focus()
            Else
                If ddlLoadingType.SelectedValue = 1 Then
                    txtStuffedContainerNo.Focus()
                ElseIf ddlLoadingType.SelectedValue = 2 Then
                    txtTruckNo.Focus()
                Else
                    ddlLoadingType.Focus()
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtStuffedContainerNo_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim dtIGM As New DataTable
            If Trim(txtStuffedContainerNo.Text) <> "" Then
                If ddlLoadingType.SelectedValue = 1 Then
                    strSql = ""
                    strSql = "Select top(1) * from Own_Ctr_Master where ContainerNo='" & Trim(txtStuffedContainerNo.Text) & "'"
                    dtIGM = db.sub_GetDatatable(strSql)
                    If dtIGM.Rows.Count > 0 Then
                        lblDEntryID.Text = Trim(dtIGM.Rows(0)("EntryID"))
                        txtSize.Text = Trim(dtIGM.Rows(0)("Size"))
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified Container no is not found in Domestic container data master. Cannot proceed');", True)
                        txtStuffedContainerNo.Text = ""
                        txtSize.Text = ""
                        txtcontainerNo.Focus()
                        Exit Sub
                    End If
                ElseIf ddlLoadingType.SelectedValue = 3 Then
                    strSql = ""
                    strSql += "SELECT TOP 1 * FROM Domestic_EmptyStock DES INNER JOIN DOMESTIC_EMPTY_IN DEI ON DES.containerno=DEI.ContainerNo AND DES.EntryID=DEI.Entry_ID WHERE DES.containerno='" & Trim(txtStuffedContainerNo.Text) & "' AND outdate IS NULL ORDER BY indate DESC"
                    dtIGM = db.sub_GetDatatable(strSql)
                    If dtIGM.Rows.Count > 0 Then
                        lblDEntryID.Text = Trim(dtIGM.Rows(0)("Entry_ID"))
                        txtSize.Text = Trim(dtIGM.Rows(0)("Size"))
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtStuffedQty_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(txtStuffedQty.Text & "") > Val(txtpkgs.Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Qty cannot be greater than total PKGS');", True)
                txtStuffedQty.Text = ""
                txtStuffedQty.Focus()
                Exit Sub
            End If
            txtStuffedWt.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtStuffedWt_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(txtStuffedWt.Text & "") > Val(txtWeight.Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Wt cannot be greater than Weight');", True)
                txtStuffedWt.Text = ""
                txtStuffedWt.Focus()
                Exit Sub
            End If
            txtSealNo.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CreateTable()
        Try

            Dim dtDomContainer As New DataTable
            dtDomContainer.Columns.Clear()
            Session("table_DomesticContainerRestuff") = ""

            'dtDomContainer.Rows.Count(-1)

            dtDomContainer.Columns.Add("ContainerNo")
            dtDomContainer.Columns.Add("Size")
            dtDomContainer.Columns.Add("TruckNo")
            dtDomContainer.Columns.Add("StuffQty")
            dtDomContainer.Columns.Add("StuffWt")
            dtDomContainer.Columns.Add("Cargo_Desc")
            dtDomContainer.Columns.Add("Seal_No")
            dtDomContainer.Columns.Add("Area")
            dtDomContainer.Columns.Add("Reference_No")
            dtDomContainer.Columns.Add("Receiver")
            dtDomContainer.Columns.Add("Location")
            dtDomContainer.Columns.Add("LocationID")
            dtDomContainer.Columns.Add("NoOfLabour")
            dtDomContainer.Columns.Add("Equipment")
            dtDomContainer.Columns.Add("EquipmentID")
            dtDomContainer.Columns.Add("Vendor")
            dtDomContainer.Columns.Add("VendorID")
            dtDomContainer.Columns.Add("Areacbm")
            dtDomContainer.Columns.Add("Vehicle")
            dtDomContainer.Columns.Add("VehicleID")
            dtDomContainer.Columns.Add("LoadingTrailer")
            Dim dtRow2 As DataRow = dtDomContainer.NewRow

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()
            Session("table_DomesticContainerRestuff") = dtDomContainer

        Catch ex As Exception
            '  lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim strSQL As String
            Dim dblStwt As Double = 0, dblstQty As Double = 0
            Dim dblqty1 As Double = 0
            For Each row In grdcontainer.Rows
                dblStwt = dblStwt + Val(CType(row.FindControl("lblStuffWt"), Label).Text & "")
                dblstQty = dblstQty + Val(CType(row.FindControl("lblStuffQty"), Label).Text & "")
            Next
            dblStwt += Val(txtStuffedWt.Text)
            dblstQty += Val(txtStuffedQty.Text)
            If Val(txtWeight.Text) < dblStwt Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Weight cannot be greater than Re-Stuff Weight. Cannot proceed');", True)
                'MsgBox("Stuffed Qty cannot be greater than Manifiest Qty. Cannot proceed", vbCritical)
                txtStuffedWt.Focus()
                Exit Sub
            End If
            If Val(txtpkgs.Text) < dblstQty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Qty cannot be greater than Re-Stuff Qty. Cannot proceed');", True)
                'MsgBox("Stuffed Qty cannot be greater than Manifiest Qty. Cannot proceed", vbCritical)
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
                
                If txtTruckNo.Text <> "" Then
                    If Trim(txtTruckNo.Text & "") = Trim(CType(row.FindControl("lbltruckNo"), Label).Text & "") Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Truck No already added. Cannot Proceed!');", True)
                        txtTruckNo.Focus()
                        Exit Sub
                    End If
                End If
            Next

            Dim dtDomContainer As New DataTable
            Dim intRows As Integer = 0

            dtDomContainer = DirectCast(Session("table_DomesticContainerRestuff"), DataTable)
            intRows = dtDomContainer.Rows.Count
            Dim dtRow As DataRow = dtDomContainer.NewRow

            dtRow.Item("ContainerNo") = txtStuffedContainerNo.Text
            dtRow.Item("Size") = txtSize.Text
            dtRow.Item("TruckNo") = txtTruckNo.Text
            dtRow.Item("StuffQty") = txtStuffedQty.Text
            dtRow.Item("StuffWt") = txtStuffedWt.Text
            dtRow.Item("Cargo_Desc") = txtCargoDesc.Text
            dtRow.Item("Seal_No") = txtSealNo.Text
            dtRow.Item("Area") = txtArea.Text
            dtRow.Item("Reference_No") = txtReferenceNo.Text
            dtRow.Item("Receiver") = txtReceiverName.Text

            If ddlLocation.SelectedValue = 0 Then
                dtRow.Item("Location") = ""
            Else
                dtRow.Item("Location") = ddlLocation.SelectedItem.Text
            End If
            dtRow.Item("LocationID") = ddlLocation.SelectedValue
            dtRow.Item("NoOfLabour") = txtNoOflabour.Text
            If ddlEquipment.SelectedValue = 0 Then
                dtRow.Item("Equipment") = ""
            Else
                dtRow.Item("Equipment") = ddlEquipment.SelectedItem.Text
            End If
            dtRow.Item("EquipmentID") = ddlEquipment.SelectedValue

            If ddlvendorName.SelectedValue = 0 Then
                dtRow.Item("Vendor") = ""
            Else
                dtRow.Item("Vendor") = ddlvendorName.SelectedItem.Text
            End If
            dtRow.Item("VendorID") = ddlvendorName.SelectedValue

            dtRow.Item("Areacbm") = txtareacbm.Text

            If ddlVehicletype.SelectedValue = 0 Then
                dtRow.Item("Vehicle") = ""
            Else
                dtRow.Item("Vehicle") = ddlVehicletype.SelectedItem.Text
            End If
            dtRow.Item("VehicleID") = ddlVehicletype.SelectedValue
            dtRow.Item("LoadingTrailer") = Trim(txttrailer.Text & "")
            dtDomContainer.Rows.Add(dtRow)

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()

            Session("table_DomesticContainerRestuff") = dtDomContainer

            'If ddlJOType.SelectedValue = 1 Then
            '    If Not txtcontainerNo.Text = "" Then
            '        Dim strno As String = Val(Left(txtcontainerNo.Text, 7)) + 1
            '        txtcontainerNo.Text = "OPEN" + strno
            '    Else
            '        txtcontainerNo.Text = "OPEN0000001"
            '    End If
            'Else
            '    txtcontainerNo.Text = ""
            'End If                        
            txtStuffedContainerNo.Text = ""
            txtSize.Text = ""
            txtTruckNo.Text = ""
            txtStuffedQty.Text = ""
            txtStuffedWt.Text = ""
            txtCargoDesc.Text = ""
            txtSealNo.Text = ""
            txtArea.Text = ""
            txtReferenceNo.Text = ""
            txtReceiverName.Text = ""
            ddlLocation.SelectedValue = 0
            txtNoOflabour.Text = ""
            ddlEquipment.SelectedValue = 0
            ddlvendorName.SelectedValue = 0
            txtcbm.Text = ""
            txtareacbm.Text = ""
            ddlVehicletype.SelectedValue = 0
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim blngridrows As Boolean = False
            For Each row In grdcontainer.Rows
                blngridrows = True
                Exit For
            Next
            If blngridrows = False Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Please add loading details. Cannot Proceed!');", True)
                ddlLoadingType.Focus()
                Exit Sub
            End If
            strSql = "SELECT isnull(MAX(Loaded_Id),0)+1 as EntryID FROM Domestic_Loading"
            dt5 = db.sub_GetDatatable(strSql)
            If dt5.Rows.Count > 0 Then
                txtLoadingSheetID.Text = dt5.Rows(0)("EntryID")
            Else
                txtLoadingSheetID.Text = 1
            End If
            For Each row In grdcontainer.Rows
                strSql = ""
                strSql += "USP_INSERT_DOMESTIC_LOADING '" & Trim(txtLoadingSheetID.Text & "") & "','" & Convert.ToDateTime(Trim(txtLoadingSheetDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                strSql += "'" & Trim(txtcontainerNo.Text & "") & "','" & Trim(txtjono.Text & "") & "','" & Trim(CType(row.FindControl("lblcontainerno"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lbltruckNo"), Label).Text & "") & "','" & lblDEntryID.Text & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblStuffQty"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblStuffWt"), Label).Text & "") & "',"
                strSql += "'" & Session("UserId_DomCFS") & "','" & Trim(CType(row.FindControl("lblCargoDesc"), Label).Text & "") & "',"                
                strSql += "'" & Trim(CType(row.FindControl("lblSealNo"), Label).Text & "") & "','" & lblCUSTOMERID.Text & "',"
                strSql += "'" & Trim(ddlLoadingType.SelectedValue & "") & "','" & Trim(CType(row.FindControl("lblArea"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblReferenceJONo"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblReceiver"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLocationID"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblNoOfLabour"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblEquipmentID"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblVendorID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblAreacbm"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblVendorID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTrailerNo"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblSize"), Label).Text & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next

            lblSession.Text = "Record Saved successfully Loaded No " & Trim(txtLoadingSheetID.Text) & ""
            txtLoadingSheetID.Text = ""
            Clear(sender, e)
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
            UpdatePanel7.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear(sender As Object, e As EventArgs)
        Try
            Call sub_CreateTable()

            txtLoadingSheetDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            lblDEntryID.Text = ""
            lblSEntryID.Text = ""
            lblCUSTOMERID.Text = ""
            ddlLoadingType.SelectedValue = 0
            txtcontainerNo.Text = ""
            txtjono.Text = ""
            txtArrivalDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtcustomer.Text = ""
            txtpkgs.Text = ""
            txtWeight.Text = ""
            txtStuffedContainerNo.Text = ""
            txtTruckNo.Text = ""
            txtStuffedQty.Text = ""
            txtStuffedWt.Text = ""
            txtSealNo.Text = ""
            txtReferenceNo.Text = ""
            txtCargoDesc.Text = ""
            txtArea.Text = ""
            txtReceiverName.Text = ""
            ddlLocation.SelectedValue = 0
            txtNoOflabour.Text = ""
            ddlEquipment.SelectedValue = 0
            ddlvendorName.SelectedValue = 0
            txtcbm.Text = ""
            txtareacbm.Text = ""
            ddlVehicletype.SelectedValue = 0
            txtSize.Text = ""
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click1(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_Loading_Container_Search where UserID=" & Session("UserId_DomCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtcontainerNo.Text = Trim(dt.Rows(0)("ContainerNo") & "")
                txtjono.Text = Trim(dt.Rows(0)("JONo") & "")
                strSql = ""
                strSql += "usp_domestic_loaded_show '" & Trim(txtcontainerNo.Text & "") & "','" & Trim(txtjono.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    lblSEntryID.Text = Trim(dt.Rows(0)("Stuffed_ID") & "")
                    lblCUSTOMERID.Text = Trim(dt.Rows(0)("CUSTOMERID") & "")
                    txtArrivalDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Arrival Date")) & "").ToString("yyyy-MM-ddTHH:mm")
                    txtcustomer.Text = Trim(dt.Rows(0)("AGName") & "")
                    txtpkgs.Text = Trim(dt.Rows(0)("Qty") & "")
                    txtWeight.Text = Trim(dt.Rows(0)("Weight") & "")
                    txtBalancePkgs.Text = Trim(dt.Rows(0)("Stuffed_Qty") & "")
                    txtbalanceWeight.Text = Trim(dt.Rows(0)("Stuffed_Wt") & "")
                    txtCargoDesc.Text = Trim(dt.Rows(0)("Cargo_Desc") & "")
                    txtSealNo.Text = Trim(dt.Rows(0)("Seal_No") & "")
                    txtArea.Text = Trim(dt.Rows(0)("Area") & "")
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found!');", True)
                    txtcontainerNo.Text = ""
                    txtjono.Text = ""
                    txtcontainerNo.Focus()
                    Exit Sub
                End If
            End If
            If ddlLoadingType.SelectedValue = 1 Then
                txtStuffedContainerNo.Focus()
            ElseIf ddlLoadingType.SelectedValue = 2 Then
                txtTruckNo.Focus()
            Else
                ddlLoadingType.Focus()
            End If
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)
            Dim dtDomContainer As New DataTable
            Dim intRows As Integer = 0

            dtDomContainer = DirectCast(Session("table_DomesticContainerRestuff"), DataTable)
            For i = 0 To dtDomContainer.Rows.Count - 1
                If Not Trim(CType(gr.FindControl("lblcontainerno"), Label).Text) = "" Then
                    If dtDomContainer.Rows(i)("ContainerNo") = Trim(CType(gr.FindControl("lblcontainerno"), Label).Text) Then
                        dtDomContainer.Rows.Remove(dtDomContainer.Rows(i))
                    End If
                End If
                If Not Trim(CType(gr.FindControl("lbltruckNo"), Label).Text) = "" Then
                    If dtDomContainer.Rows(i)("TruckNo") = Trim(CType(gr.FindControl("lbltruckNo"), Label).Text) Then
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
    Protected Sub txtcbm_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtcbm.Text <> "" Then
                txtareacbm.Text = Val(txtcbm.Text) * 3.5396 * 10.76
            Else
                txtareacbm.Text = ""
            End If


        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txttrailerno_TextChanged(sender As Object, e As EventArgs)
        Try
            txttrailer.Text = ""
            strSql = ""
            strSql = "Usp_gettrailern_In '" & Trim(txttrailerno.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txttrailer.ReadOnly = True
                txttrailer.Text = Trim(dt.Rows(0)("trailername"))
            Else
                txttrailer.ReadOnly = False
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
