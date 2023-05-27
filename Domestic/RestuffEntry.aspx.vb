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
            grid()
            Filldropdown()

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

            'ddllocation.DataSource = ds.Tables(1)
            'ddllocation.DataTextField = "WHName"
            'ddllocation.DataValueField = "WHID"
            'ddllocation.DataBind()
            'ddllocation.Items.Insert(0, New ListItem("--Select--", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnshow_Click(sender As Object, e As EventArgs)
        Try
             

            Dim strSQL As String
            Dim dt As New DataTable
            '************************To check valid entry for container*******************
            Dim dt3 As New DataTable
            Dim dt4 As New DataTable

            strSQL = ""
            strSQL = " select containerNo, Jono from DOMESTIC_JOBORDERD where ContainerNo='" & Trim(txtcontainerNo.Text & "") & "' order by jono desc"
            dt = db.sub_GetDatatable(strSQL)
            If dt.Rows.Count > 0 Then
                txtjono.Text = Trim(dt.Rows(0)("Jono") & "")
            Else
                strSQL = ""
                strSQL = " select containerNo from Own_Ctr_Master where ContainerNo='" & Trim(txtcontainerNo.Text & "") & "'"
                dt3 = db.sub_GetDatatable(strSQL)
                If dt3.Rows.Count > 0 Then

                Else
                    strSQL = ""
                    strSQL = " select D_Container_No from Domestic_Stuffing where D_Container_No='" & Trim(txtcontainerNo.Text & "") & "' AND D_Entry_ID='" & lblDEntryID.Text & "'"
                    dt4 = db.sub_GetDatatable(strSQL)
                    If dt4.Rows.Count > 0 Then

                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid Container no!');", True)

                        txtcontainerNo.Focus()
                        Exit Sub
                    End If

                End If
            End If

            strSQL = ""
            strSQL = " select D_Container_No from Domestic_Stuffing where D_Container_No='" & Trim(txtcontainerNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSQL)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No already exists!');", True)

                txtcontainerNo.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql = " usp_domestic_restuff_show'" & Trim(txtcontainerNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblDEntryID.Text = Trim(dt.Rows(0)("Entry_ID") & "")
                lblCHAID.Text = Trim(dt.Rows(0)("CHA") & "")
                lblSLID.Text = Trim(dt.Rows(0)("LINEID") & "")
                lblCUSTOMERID.Text = Trim(dt.Rows(0)("CUSTOMERID") & "")
                lblremarks.Text = Trim(dt.Rows(0)("REMARKS") & "")
                txtjono.Text = Trim(dt.Rows(0)("JobNo") & "")
                txtArrivalDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Arrival Date")) & "").ToString("yyyy-MM-ddTHH:mm")
                txtcustomer.Text = Trim(dt.Rows(0)("AGName") & "")
                txtpkgs.Text = Trim(dt.Rows(0)("PKGS") & "")
                txtWeight.Text = Trim(dt.Rows(0)("WEIGHT") & "")
                txtDCAJoNo.Text = Trim(dt.Rows(0)("REFJONO") & "")
                txtIgmNo.Text = Trim(dt.Rows(0)("IGMNO") & "")
                txtItemNo.Text = Trim(dt.Rows(0)("ItemNo") & "")
                txtCargoDesc.Text = Trim(dt.Rows(0)("CARGODESCRIPTION") & "")
                txtSealNo.Text = Trim(dt.Rows(0)("SealNo") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found!');", True)
                txtcontainerNo.Text = ""
                txtcontainerNo.Focus()
                Exit Sub
            End If

            Dim dtdetails As New DataTable
            Dim dblqty As Double = 0
            Dim dblStuffWt As Double = 0
            strSQL = ""
            strSQL = "SELECT isnull(sum(Stuffed_Wt),0) [Stuffed_Wt]  FROM  Domestic_Stuffing D "
            strSQL += " where  D_Container_No ='" & Trim(txtcontainerNo.Text & "") & "' and D_Entry_ID ='" & Trim(txtjono.Text & "") & "' and IsCancel =0  "

            dtdetails = db.sub_GetDatatable(strSQL)
            If dtdetails.Rows.Count > 0 Then

                dblqty = Val(dtdetails.Rows(0)("Stuffed_Wt") & "")
                dblStuffWt = Trim(txtWeight.Text) - dblqty
                If dblStuffWt <= 0 Then
                    'MsgBox("Restuff is already done for Container No =" & Trim(txtContainerNo.Text & ""), vbInformation)
                    'Exit Sub
                Else
                    ''   txtwt.Text = dblStuffWt
                End If
            End If

            Dim dtIGM As New DataTable
            strSQL = ""
            strSQL = "Select top(1) * from Domestic_Stuffing s inner join userdetails u on u.userid=s.added_by  where D_Container_No='" & Trim(txtcontainerNo.Text) & "' and D_Entry_ID='" & Trim(txtjono.Text) & "' and Iscancel=0 order by AUTO_ID  DESC "
            dtIGM = db.sub_GetDatatable(strSQL)
            If dtIGM.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified container no is already destuffed dated on');" & dtIGM.Rows(0)("Stuffed_Date") & "  by " & Trim(dtIGM.Rows(0)("username")) & " ", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlStuffingType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If Trim(ddlStuffingType.SelectedValue) <> 0 Then

                If ddlStuffingType.SelectedValue = "1" Then

                    divStuffedl.Attributes.Add("style", "display:none")
                    divStuffed.Attributes.Add("style", "display:block")
                    'lblcriteria.Visible = True
                    ddllocation.Visible = False
                    txtcontainerNo.Visible = True
                ElseIf ddlStuffingType.SelectedValue = "3" Then
                    divStuffedl.Attributes.Add("style", "display:none")
                    divStuffed.Attributes.Add("style", "display:block")
                    'lblcriteria.Visible = True
                    ddllocation.Visible = False
                    txtcontainerNo.Visible = True
                ElseIf ddlStuffingType.SelectedValue = "2" Then
                    divStuffedl.Attributes.Add("style", "display:block")
                    divStuffed.Attributes.Add("style", "display:none")
                    'lblcriteria.Text = "Ware House:"
                    'lblcriteria.Visible = False
                    ddllocation.Visible = True
                    grid1()

                ElseIf ddlStuffingType.SelectedValue = "4" Then

                    divStuffedl.Attributes.Add("style", "display:none")
                    divStuffed.Attributes.Add("style", "display:block")
                    'lblcriteria.Visible = True
                    ddllocation.Visible = False
                    txtcontainerNo.Visible = True
                ElseIf ddlStuffingType.SelectedValue = "5" Then
                    divStuffedl.Attributes.Add("style", "display:block")
                    divStuffed.Attributes.Add("style", "display:none")
                    'lblcriteria.Text = "Ware House:"
                    'lblcriteria.Visible = False
                    ddllocation.Visible = True
                    grid1()
                End If

            End If
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
                        lblJoNo.Text = Trim(dtJoNO.Rows(0)("JO No") & "")
                    End If

                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified Container no is not found in Domestic container data master. Cannot proceed');", True)

                    txtcontainerNo.Focus()
                    Exit Sub
                End If

                strSql = ""
                strSql = "Select top(1) * from Domestic_Stuffing s inner join userdetails u on u.userid=s.added_by  where S_Container_No='" & Trim(txtStuffedContainerNo.Text) & "' and S_Entry_ID='" & Trim(lblJoNo.Text) & "' and Iscancel=0 order by AUTO_ID  DESC "
                dtIGM = db.sub_GetDatatable(strSql)
                If dtIGM.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(Specified container no is already stuffed dated on ');" & dtIGM.Rows(0)("Stuffed_Date") & " by " & Trim(dtIGM.Rows(0)("username")) & "", True)

                   
                End If

            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtStuffedQty_TextChanged(sender As Object, e As EventArgs)
        Try
            If Trim(txtStuffedQty.Text & "") > Trim(txtpkgs.Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Qty cannot be greater than total PKGS');", True)
                txtStuffedQty.Text = ""
                txtStuffedQty.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtStuffedWt_TextChanged(sender As Object, e As EventArgs)
        Try
            If Trim(txtStuffedWt.Text & "") > Trim(txtWeight.Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Wt cannot be greater than Weight');", True)
                txtStuffedWt.Text = ""
                txtStuffedWt.Focus()
                Exit Sub
            End If
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
            dtDomContainer.Columns.Add("WareHouse")
            dtDomContainer.Columns.Add("WareHouse_ID")
            dtDomContainer.Columns.Add("StuffQty")
            dtDomContainer.Columns.Add("StuffWt")
            dtDomContainer.Columns.Add("DCA_No")
            dtDomContainer.Columns.Add("IGM_No")
            dtDomContainer.Columns.Add("Item_No")
            dtDomContainer.Columns.Add("Cargo_Desc")
            dtDomContainer.Columns.Add("Seal_No")
            dtDomContainer.Columns.Add("Area")
            dtDomContainer.Columns.Add("Location")

            Dim dtRow2 As DataRow = dtDomContainer.NewRow

            'dtRow2.Item("ProductName") = ""
            'dtRow2.Item("Qty") = ""
            'dtRow2.Item("Unit") = ""
            'dtRow2.Item("Rate") = ""
            'dtRow2.Item("Amount") = ""
            'dtRow2.Item("vatper") = ""
            'dtRow2.Item("staxper") = ""
            'dtRow2.Item("staxamount") = ""
            'dtRow2.Item("cstper") = ""
            'dtRow2.Item("cstamount") = ""
            'dtRow2.Item("TotalAmount") = ""
            'dtDomContainer.Rows.Add(dtRow2)

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()
            Session("table_DomesticContainerRestuff") = dtDomContainer


            If dtDomContainer.Rows.Count > 0 Then
                ' lblError.Text = dtDomContainer.Rows(0)("Designation")
            End If

        Catch ex As Exception
            '  lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim strSQL As String
            Dim dblStwt As Double = 0, dblstQty As Double = 0
            Dim dblqty1 As Double = 0
            If grdcontainer.Rows.Count = 0 Then
                strSql = ""
                strSQL = "SELECT isnull(sum(Stuffed_Wt),0) [Stuffed_Wt]  FROM  Domestic_Stuffing D "
                strSql += " where  D_Container_No ='" & Trim(txtcontainerNo.Text & "") & "' and D_Entry_ID ='" & Trim(txtjono.Text & "") & "' and IsCancel =0  "

                dt1 = db.sub_GetDatatable(strSql)
                If dt1.Rows.Count > 0 Then
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        dblqty1 = dblqty1 + Val(dt1.Rows(i)("Stuffed_Wt") & "")
                    Next
                    dblqty1 = dblqty1 + Trim(txtStuffedWt.Text)
                    'If Val(txtwt.Text) < dblqty1 Then
                    '    MsgBox("Invalid entry.", vbInformation)
                    '    txtStuffWt.Focus()
                    '    Exit Sub
                    'End If
                End If
            Else

                For Each row In grdcontainer.Rows
                    dblStwt = dblStwt + Val(CType(row.FindControl("lblStuffWt"), Label).Text & "")
                    dblstQty = dblstQty + Val(CType(row.FindControl("lblStuffQty"), Label).Text & "")
                Next
                strSQL = ""
                strSQL = "SELECT isnull(sum(Stuffed_Wt),0) [Stuffed_Wt]  FROM  Domestic_Stuffing D "
                strSQL += " where  D_Container_No ='" & Trim(txtcontainerNo.Text & "") & "' and D_Entry_ID ='" & Trim(txtjono.Text & "") & "' and IsCancel =0  "

                dt1 = db.sub_GetDatatable(strSQL)
                If dt1.Rows.Count > 0 Then
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        dblqty1 = dblqty1 + Val(dt1.Rows(i)("Stuffed_Wt") & "")
                    Next
                    dblqty1 = dblqty1 + Trim(txtStuffedWt.Text) + dblStwt
                    If Val(txtWeight.Text) < dblqty1 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Qty cannot be greater than Manifiest Qty. Cannot proceed');", True)
                        'MsgBox("Stuffed Qty cannot be greater than Manifiest Qty. Cannot proceed", vbCritical)
                        txtStuffedWt.Focus()
                        Exit Sub
                    End If
                Else
                    dblqty1 = Val(txtStuffedWt.Text) + dblStwt
                    If Val(txtWeight.Text) < dblqty1 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Stuffed Weight cannot be greater than Manifiest Weight. Cannot proceed');", True)
                        'MsgBox("Stuffed Weight cannot be greater than Manifiest Weight. Cannot proceed", vbCritical)
                        txtStuffedWt.Focus()
                        Exit Sub
                    End If
                End If
            End If

            For Each row In grdcontainer.Rows
                If txtStuffedContainerNo.Text = Trim(CType(row.FindControl("lblcontainerno"), Label).Text & "") Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Container No already their. Cannot Proceed!');", True)
                    txtcontainerNo.Focus()
                    Exit Sub
                End If

            Next

            Dim dtDomContainer As New DataTable
            Dim intRows As Integer = 0

            dtDomContainer = DirectCast(Session("table_DomesticContainerRestuff"), DataTable)
            intRows = dtDomContainer.Rows.Count
            Dim dtRow As DataRow = dtDomContainer.NewRow

            dtRow.Item("ContainerNo") = txtStuffedContainerNo.Text

            If ddllocation.SelectedValue = "" Then
                dtRow.Item("WareHouse") = ""
            Else
                dtRow.Item("WareHouse") = ddllocation.SelectedItem.Text
            End If

            'dtRow.Item("WareHouse") = ddllocation.SelectedItem.Text
            dtRow.Item("WareHouse_ID") = ddllocation.SelectedValue
            dtRow.Item("StuffQty") = txtStuffedQty.Text
            dtRow.Item("StuffWt") = txtStuffedWt.Text
            dtRow.Item("DCA_No") = txtDCAJoNo.Text
            dtRow.Item("IGM_No") = txtIgmNo.Text
            dtRow.Item("Item_No") = txtItemNo.Text
            dtRow.Item("Cargo_Desc") = txtCargoDesc.Text
            dtRow.Item("Seal_No") = txtSealNo.Text
            dtRow.Item("Area") = txtArea.Text
            dtRow.Item("Location") = txtlocation.Text
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

            ddllocation.SelectedValue = 0
            txtStuffedQty.Text = ""
            txtStuffedWt.Text = ""
            txtDCAJoNo.Text = ""
            txtIgmNo.Text = ""
            txtItemNo.Text = ""
            txtCargoDesc.Text = ""
            txtSealNo.Text = ""
            txtArea.Text = ""
            txtlocation.Text = ""
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = "SELECT isnull(MAX(Stuffed_ID),0)+1 as EntryID FROM Domestic_Stuffing"
            dt5 = db.sub_GetDatatable(strSql)
            If dt5.Rows.Count > 0 Then
                txtstuffingID.Text = dt5.Rows(0)("EntryID")
            Else
                txtstuffingID.Text = 1
            End If
            For Each row In grdcontainer.Rows
                strSql = ""
                strSql += "USP_INSERT_DOMESTIC_STUFFING'" & Trim(txtstuffingID.Text & "") & "','" & Convert.ToDateTime(Trim(txtGateInDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                strSql += "'" & Trim(txtcontainerNo.Text & "") & "','" & lblDEntryID.Text & "','" & Trim(CType(row.FindControl("lblcontainerno"), Label).Text & "") & "','" & lblJoNo.Text & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblStuffQty"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblStuffWt"), Label).Text & "") & "',"
                strSql += "'" & lblremarks.Text & "','" & Session("UserId_DomCFS") & "','" & Trim(CType(row.FindControl("lblWareHouseID"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblDCANo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblItemNo"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblIGMNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblCargoDesc"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblSealNo"), Label).Text & "") & "','" & lblCHAID.Text & "','" & lblSLID.Text & "','" & lblCUSTOMERID.Text & "',"
                strSql += "'" & Trim(ddlStuffingType.SelectedValue & "") & "','" & Trim(CType(row.FindControl("lblArea"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLocation"), Label).Text & "") & "'"
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
            txtcontainerNo.Text = ""
            lblDEntryID.Text = ""
            lblJoNo.Text = ""
            lblremarks.Text = ""
            lblCHAID.Text = ""
            lblCUSTOMERID.Text = ""
            lblSLID.Text = ""
            ddlStuffingType.SelectedValue = 0
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtcbm_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtcbm.Text <> "" Then
                txtArea.Text = Val(txtcbm.Text) * 10.76
            Else
                txtArea.Text = ""
            End If


        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
