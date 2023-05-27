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
            sub_CreateTable()
            db.sub_ExecuteNonQuery("Delete from Temp_Loaded_InD Where UserID=" & Session("UserId_DomCFS") & "")
            txtGateInDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtInvoiceDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            'btnsearch_Click(sender, e)
            Filldropdown()
        End If
    End Sub
    Public Sub Filldropdown()
        ds = db.sub_GetDataSets("usp_own_container_fill")
        
        If (ds.Tables(2).Rows.Count > 0) Then
            ddlcondtion.DataSource = ds.Tables(2)
            ddlcondtion.DataTextField = "Name"
            ddlcondtion.DataValueField = "ID"
            ddlcondtion.DataBind()
            ddlcondtion.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        ds = db.sub_GetDataSets("usp_empty_in_contaner")
        If (ds.Tables(0).Rows.Count > 0) Then
            ddltransporter.DataSource = ds.Tables(0)
            ddltransporter.DataTextField = "TransName"
            ddltransporter.DataValueField = "TransID"
            ddltransporter.DataBind()
            ddltransporter.Items.Insert(0, New ListItem("--Select--", 0))

            If (ds.Tables(3).Rows.Count > 0) Then
                ddltype.DataSource = ds.Tables(3)
                ddltype.DataTextField = "ContainerType"
                ddltype.DataValueField = "ContainerTypeID"
                ddltype.DataBind()
                ddltype.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(5).Rows.Count > 0) Then
                ddllinename.DataSource = ds.Tables(5)
                ddllinename.DataTextField = "SLName"
                ddllinename.DataValueField = "SLID"
                ddllinename.DataBind()
                ddllinename.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(2).Rows.Count > 0) Then
                ddlsize.DataSource = ds.Tables(2)
                ddlsize.DataTextField = "ContainerSize"
                ddlsize.DataValueField = "ContainerSizeID"
                ddlsize.DataBind()
                ddlsize.Items.Insert(0, New ListItem("--Select--", 0))
            End If
        End If
        ddlCargoType.DataSource = ds.Tables(7)
        ddlCargoType.DataTextField = "Cargotype"
        ddlCargoType.DataValueField = "Cargotypeid"
        ddlCargoType.DataBind()
        ddlCargoType.Items.Insert(0, New ListItem("--Select--", 0))

        ddlCommodity.DataSource = ds.Tables(8)
        ddlCommodity.DataTextField = "Commodity"
        ddlCommodity.DataValueField = "Id"
        ddlCommodity.DataBind()
        ddlCommodity.Items.Insert(0, New ListItem("--Select--", 0))

        ddlFromLocation.DataSource = ds.Tables(9)
        ddlFromLocation.DataTextField = "Location"
        ddlFromLocation.DataValueField = "LocationID"
        ddlFromLocation.DataBind()
        ddlFromLocation.Items.Insert(0, New ListItem("--Select--", 0))

        ddlToLocation.DataSource = ds.Tables(9)
        ddlToLocation.DataTextField = "Location"
        ddlToLocation.DataValueField = "LocationID"
        ddlToLocation.DataBind()
        ddlToLocation.Items.Insert(0, New ListItem("--Select--", 0))

        ddlCustomer.DataSource = ds.Tables(4)
        ddlCustomer.DataTextField = "AGName"
        ddlCustomer.DataValueField = "AGID"
        ddlCustomer.DataBind()
        ddlCustomer.Items.Insert(0, New ListItem("--Select--", 0))

        ddlToLocation.SelectedValue = 292
    End Sub     
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_Loaded_InD where userid='" & Session("UserId_DomCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows.Count > 0 Then
                    'txtcontainer.Text = Trim(dt.Rows(0)("ContainerNo") & "")
                    txtjono.Text = Trim(dt.Rows(0)("JONo") & "")
                Else

                    txtjono.Text = ""
                    Exit Sub
                End If
                Call txtjono_TextChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txttrailerno_TextChanged(sender As Object, e As EventArgs)
        Try
            txttrailerNo1.Text = ""
            txttrailer.Text = ""
            strSql = ""
            strSql = "Usp_gettrailern_In '" & Trim(txttrailerno.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txttrailerNo1.Text = Trim(dt.Rows(0)("trailername"))
                ddltransporter.SelectedValue = Trim(dt.Rows(0)("TransID"))
                'txtdrivercode.Text = Trim(dt.Rows(0)("DriverCode"))
                divtrailer.Attributes.Add("style", "display:None")
                divtrai.Attributes.Add("style", "display:block")
            Else
                divtrailer.Attributes.Add("style", "display:block")
                divtrai.Attributes.Add("style", "display:None")
                ddltransporter.SelectedValue = 0
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtcontainer_TextChanged(sender As Object, e As EventArgs)
        Try
            If Trim(ddlCategory.SelectedValue) = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Please select Category first!');", True)
                txtcontainer.Text = ""
                ddlCategory.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql += "USP_DOMESTIC_GATE_IN_OUT_DUPLICATION_VALIDATION '" & Trim(txtcontainer.Text & "") & "','L'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Container is in Empty Inventory!');", True)
                txtcontainer.Text = ""
                txtcontainer.Focus()
                Exit Sub
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Container is in Loaded Inventory!');", True)
                txtcontainer.Text = ""
                txtcontainer.Focus()
                Exit Sub
            End If
            If ds.Tables(2).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Container is in Loaded Inventory!');", True)
                txtcontainer.Text = ""
                txtcontainer.Focus()
                Exit Sub
            End If
            If ds.Tables(3).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Container is in Empty Inventory!');", True)
                txtcontainer.Text = ""
                txtcontainer.Focus()
                Exit Sub
            End If
            ddlsize.Focus()
            If Trim(ddlCategory.SelectedValue) = "I" Then
                strSql = ""
                strSql += "USP_DOMESTIC_CONTAINER_DETAILS_LOADED_IN '" & Trim(txtcontainer.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ddlsize.SelectedValue = Val(dt.Rows(0)("ContainerSizeID") & "")
                    ddltype.SelectedValue = Val(dt.Rows(0)("CONTAINERTYPEID") & "")
                    ddlCargoType.SelectedValue = Val(dt.Rows(0)("CARGOTYPEID") & "")
                    ddlFromLocation.SelectedValue = Val(dt.Rows(0)("FROMLOCATION") & "")
                    txtjono.Text = Trim(dt.Rows(0)("JONO") & "")
                    txtJOType.Text = Trim(dt.Rows(0)("JOTYPE") & "")
                    txtpkgs.Text = Val(dt.Rows(0)("PKGS") & "")
                    txtweight.Text = Val(dt.Rows(0)("WEIGHT") & "")
                    ddllinename.SelectedValue = Val(dt.Rows(0)("LINEID") & "")
                    ddlCustomer.SelectedValue = Val(dt.Rows(0)("CUSTOMERID") & "")
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Container not found!');", True)
                    txtcontainer.Text = ""
                    ddlsize.SelectedValue = 0
                    ddltype.SelectedValue = 0
                    ddlCargoType.SelectedValue = 0
                    ddlFromLocation.SelectedValue = 0
                    txtjono.Text = ""
                    txtJOType.Text = ""
                    txtpkgs.Text = ""
                    txtweight.Text = ""
                    ddllinename.SelectedValue = 0
                    ddlCustomer.SelectedValue = 0
                    txtcontainer.Focus()
                    Exit Sub
                End If
                txtDONo.Focus()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Val(ddlFromLocation.SelectedValue) = Val(ddlToLocation.SelectedValue) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Please select from & to location different!');", True)
                ddlFromLocation.SelectedValue = 0
                Exit Sub
            End If
            Dim dblCount As Double = 0, dblQty As Double = 0, dblWeight As Double = 0
            For Each row In grdcontainer.Rows
                dblQty += Val(CType(row.FindControl("lblLoadedInPkgs"), Label).Text & "")
                dblWeight += Val(CType(row.FindControl("lblLoadedInWeight"), Label).Text & "")
                dblCount += 1
            Next
            If dblCount = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Please fill Trailer details first!');", True)
                Exit Sub
            End If
            'Dim strSearchText As String = ""
            'If txttrailerNo1.Text <> "" Then
            '    strSearchText = txttrailerNo1.Text
            'ElseIf txttrailer.Text <> "" Then
            '    strSearchText = txttrailer.Text
            'End If
            Dim dblGateInNo As Double = 0
            strSql = ""
            strSql += "select isnull(max(gatein_no),0)+1 gatein_no from Domestic_IN"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                dblGateInNo = Val(dt.Rows(0)("gatein_no"))
            End If
            For Each row In grdcontainer.Rows
                strSql = ""
                strSql += "USP_INSERT_DOMESTIC_IN " & dblGateInNo & ",'" & Convert.ToDateTime(Trim(txtGateInDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtjono.Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblContainerno"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblSealNo"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblConditionID"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblTrailerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTransporterID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblDriverName"), Label).Text & "") & "',"
                strSql += "'" & Trim(txtremarks.Text & "") & "','" & Session("UserId_DomCFS") & "','" & Val(CType(row.FindControl("lblLoadedInPkgs"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblLoadedInWeight"), Label).Text & "") & "','" & Trim(ddllinename.SelectedValue & "") & "',"
                strSql += "'" & Val(CType(row.FindControl("lblSizeID"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblTypeID"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblCargoTypeID"), Label).Text & "") & "','" & Replace(Trim(txtDONo.Text & ""), "'", "''") & "','" & Replace(Trim(txtInvoiceNo.Text & ""), "'", "''") & "','" & Val(ddlCommodity.SelectedValue) & "',"
                strSql += "'" & Val(CType(row.FindControl("lblFromLocationID"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblToLocationID"), Label).Text & "") & "','" & Val(ddlCustomer.SelectedValue) & "','" & Convert.ToDateTime(Trim(txtInvoiceDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlCategory.SelectedValue) & "'"
                dt = db.sub_GetDatatable(strSql)
            Next

            txtSlipNoPrint.Text = Val(dt.Rows(0)("gatein_no") & "")
            Clear()
            lblSession.Text = "Record saved successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub SaveOk_ServerClick(sender As Object, e As EventArgs)
        Try
            lblPrintQue.Text = "Do you wish to print?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            txtGateInDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtjono.Text = ""
            txtcontainer.Text = ""
            txtseal.Text = ""
            ddlcondtion.SelectedValue = 0
            txttrailerNo1.Text = ""
            txttrailer.Text = ""
            ddltransporter.SelectedValue = 0
            txtdrivercode.Text = ""
            txtremarks.Text = ""
            'Select.Text = ""
            'txttype.Text = ""
            'txtshippingLine.Text = ""
            ddlCustomer.SelectedValue = 0
            ddlCommodity.SelectedValue = 0
            ddlFromLocation.SelectedValue = 0
            'ddlToLocation.SelectedValue = 0

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CreateTable()
        Try
            Dim dtDomContainer As New DataTable
            dtDomContainer.Columns.Clear()
            Session("table_DomesticLoadedIn") = ""

            'dtDomContainer.Rows.Count(-1)
            dtDomContainer.Columns.Add("ContainerNo")
            dtDomContainer.Columns.Add("Size")
            dtDomContainer.Columns.Add("SizeID")
            dtDomContainer.Columns.Add("Type")
            dtDomContainer.Columns.Add("TypeID")
            dtDomContainer.Columns.Add("CargoType")
            dtDomContainer.Columns.Add("CargoTypeID")
            dtDomContainer.Columns.Add("FromLocation")
            dtDomContainer.Columns.Add("FromLocationID")
            dtDomContainer.Columns.Add("ToLocation")
            dtDomContainer.Columns.Add("ToLocationID")


            dtDomContainer.Columns.Add("TrailerNo")
            dtDomContainer.Columns.Add("LoadedInPkgs")
            dtDomContainer.Columns.Add("LoadedInWeight")
            dtDomContainer.Columns.Add("SealNo")
            dtDomContainer.Columns.Add("Condition")
            dtDomContainer.Columns.Add("ConditionID")
            dtDomContainer.Columns.Add("Transporter")
            dtDomContainer.Columns.Add("TransporterID")
            dtDomContainer.Columns.Add("DriverName")

            Dim dtRow2 As DataRow = dtDomContainer.NewRow

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()
            Session("table_DomesticLoadedIn") = dtDomContainer


            If dtDomContainer.Rows.Count > 0 Then
                ' lblError.Text = dtDomContainer.Rows(0)("Designation")
            End If

        Catch ex As Exception
            '  lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim dblCount As Double = 0, dblQty As Double = 0, dblWeight As Double = 0
            For Each row In grdcontainer.Rows
                dblQty += Val(CType(row.FindControl("lblLoadedInPkgs"), Label).Text & "")
                dblWeight += Val(CType(row.FindControl("lblLoadedInWeight"), Label).Text & "")
                dblCount += 1
            Next
            If Not Trim(txtjono.Text) = "" Then
                If txtJOType.Text = "Container" Then
                    If dblCount > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Cannot proceed for this JO Type!');", True)
                        Exit Sub
                    End If
                    'If Val(txtLoadedInPkgs.Text) <> Val(txtBalPkgs.Text) Then
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Loaded In Pkgs must be equal to Balance pkgs!');", True)
                    '    Exit Sub
                    'End If
                End If
                If (dblQty) > Val(txtLoadedInPkgs.Text) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Loaded In Pkgs should not be greater than Balance Pkgs!');", True)
                    txtLoadedInPkgs.Focus()
                    Exit Sub
                End If
                If (dblWeight) > Val(txtLoadedInWeight.Text) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Loaded In Weight should not be greater than Balance Weight!');", True)
                    txtLoadedInWeight.Focus()
                    Exit Sub
                End If
            End If
            
            Dim dtDomContainer As New DataTable
            Dim intRows As Integer = 0

            dtDomContainer = DirectCast(Session("table_DomesticLoadedIn"), DataTable)
            intRows = dtDomContainer.Rows.Count
            Dim dtRow As DataRow = dtDomContainer.NewRow
            dtRow.Item("ContainerNo") = Trim(txtcontainer.Text & "")
            If Not ddlsize.SelectedValue = 0 Then
                dtRow.Item("Size") = Trim(ddlsize.SelectedItem.Text & "")
            Else
                dtRow.Item("Size") = ""
            End If
            dtRow.Item("SizeID") = ddlsize.SelectedValue

            If Not ddltype.SelectedValue = 0 Then
                dtRow.Item("Type") = Trim(ddltype.SelectedItem.Text & "")
            Else
                dtRow.Item("Type") = ""
            End If
            dtRow.Item("TypeID") = ddltype.SelectedValue

            If Not ddlCargoType.SelectedValue = 0 Then
                dtRow.Item("CargoType") = Trim(ddlCargoType.SelectedItem.Text & "")
            Else
                dtRow.Item("CargoType") = ""
            End If
            dtRow.Item("CargoTypeID") = ddlCargoType.SelectedValue

            If Not ddlFromLocation.SelectedValue = 0 Then
                dtRow.Item("FromLocation") = Trim(ddlFromLocation.SelectedItem.Text & "")
            Else
                dtRow.Item("FromLocation") = ""
            End If
            dtRow.Item("FromLocationID") = ddlFromLocation.SelectedValue

            If Not ddlToLocation.SelectedValue = 0 Then
                dtRow.Item("ToLocation") = Trim(ddlToLocation.SelectedItem.Text & "")
            Else
                dtRow.Item("ToLocation") = ""
            End If
            dtRow.Item("ToLocationID") = ddlToLocation.SelectedValue



            If Trim(txttrailer.Text & "") <> "" Then
                dtRow.Item("TrailerNo") = Trim(txttrailer.Text & "")
            ElseIf Trim(txttrailerNo1.Text & "") <> "" Then
                dtRow.Item("TrailerNo") = Trim(txttrailerNo1.Text & "")
            End If
            dtRow.Item("LoadedInPkgs") = Val(txtLoadedInPkgs.Text & "")
            dtRow.Item("LoadedInWeight") = Val(txtLoadedInWeight.Text & "")
            dtRow.Item("SealNo") = Trim(txtseal.Text & "")
            If Not ddlcondtion.SelectedValue = 0 Then
                dtRow.Item("Condition") = Trim(ddlcondtion.SelectedItem.Text & "")
            Else
                dtRow.Item("Condition") = ""
            End If
            dtRow.Item("ConditionID") = ddlcondtion.SelectedValue
            If Not ddltransporter.SelectedValue = 0 Then
                dtRow.Item("Transporter") = Trim(ddltransporter.SelectedItem.Text & "")
            Else
                dtRow.Item("Transporter") = ""
            End If            
            dtRow.Item("TransporterID") = ddltransporter.SelectedValue
            dtRow.Item("DriverName") = Trim(txtdrivercode.Text & "")

            dtDomContainer.Rows.Add(dtRow)

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()

            Session("table_DomesticContainers") = dtDomContainer
            txttrailer.Text = ""
            txttrailerno.Text = ""
            txttrailerNo1.Text = ""
            txtLoadedInPkgs.Text = ""
            txtLoadedInWeight.Text = ""
            txtseal.Text = ""
            txtcontainer.Text = ""
            ddlsize.SelectedValue = 0
            ddltype.SelectedValue = 0
            ddlCargoType.SelectedValue = 0
            ddlFromLocation.SelectedValue = 0
            'ddlToLocation.SelectedValue = 0
            ddlcondtion.SelectedValue = 0
            ddltransporter.SelectedValue = 0
            txtdrivercode.Text = ""
            divtrailer.Attributes.Add("style", "display:None")
            divtrai.Attributes.Add("style", "display:block")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtjono_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "usp_textchange_Loaded_In '" & Trim(txtjono.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                'txtcontainer.Text = Trim(dt.Rows(0)("bondno") & "")
                'txtSize.Text = Trim(dt.Rows(0)("SIZE") & "")
                'txttype.Text = Trim(dt.Rows(0)("Cargotype") & "")
                ddlCustomer.SelectedValue = Trim(dt.Rows(0)("AGName") & "")
                'txtshippingLine.Text = Trim(dt.Rows(0)("SLName") & "")
                txtjono.Text = Trim(dt.Rows(0)("JONO") & "")
                txtpkgs.Text = Trim(dt.Rows(0)("PKGS") & "")
                txtweight.Text = Trim(dt.Rows(0)("WEIGHT") & "")
                txtJOType.Text = Trim(dt.Rows(0)("JOTYPE") & "")
                txtBalPkgs.Text = Trim(dt.Rows(0)("BalPkgs") & "")
                txtBalWeight.Text = Trim(dt.Rows(0)("BalWeight") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found!');", True)
                txtjono.Text = ""
                txtjono.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
