Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt9 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim dblGroup1Amt As Double
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
            db.sub_ExecuteNonQuery("Delete from Temp_Other_Assessment_DOMESTIC Where UserID=" & Session("UserId_DomCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_GST_Search_Domestic Where UserID=" & Session("UserId_DomCFS") & "")
            txtvaliddate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtinvdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")

            Filldropdown()
            FillGrid()

        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_Fill_Noc_list_DOMESTIC"
            ds = db.sub_GetDataSets(strSql)
            ddlcustomer.DataSource = ds.Tables(0)
            ddlcustomer.DataTextField = "agentName"
            ddlcustomer.DataValueField = "agentID"
            ddlcustomer.DataBind()
            ddlcustomer.Items.Insert(0, New ListItem("--Select--", 0))
            ddlimporter.DataSource = ds.Tables(1)
            ddlimporter.DataTextField = "ImporterName"
            ddlimporter.DataValueField = "ImporterID"
            ddlimporter.DataBind()
            ddlimporter.Items.Insert(0, New ListItem("--Select--", 0))
            ddlCHA.DataSource = ds.Tables(2)
            ddlCHA.DataTextField = "CHAName"
            ddlCHA.DataValueField = "CHAID"
            ddlCHA.DataBind()
            ddlCHA.Items.Insert(0, New ListItem("--Select--", 0))
            ddlaccntheads.DataSource = ds.Tables(4)
            ddlaccntheads.DataTextField = "AccountName"
            ddlaccntheads.DataValueField = "AccountID"
            ddlaccntheads.DataBind()
            ddlaccntheads.Items.Insert(0, New ListItem("--Select--", 0))
            ddlwarehouse.DataSource = ds.Tables(5)
            ddlwarehouse.DataTextField = "Warehouse_code"
            ddlwarehouse.DataValueField = "Warehouse_code"
            ddlwarehouse.DataBind()
            ddlwarehouse.Items.Insert(0, New ListItem("--Select--", 0))
            ddlTax.DataSource = ds.Tables(7)
            ddlTax.DataTextField = "Tax"
            ddlTax.DataValueField = "settingsID"
            ddlTax.DataBind()
            ddlTax.Items.Insert(0, New ListItem("--Select--", 0))

            ddlCommodity.DataSource = ds.Tables(8)
            ddlCommodity.DataTextField = "Commodity_Group_Name"
            ddlCommodity.DataValueField = "Commodity_Group_ID"
            ddlCommodity.DataBind()


            ddltxtTax.DataSource = ds.Tables(9)
            ddltxtTax.DataTextField = "tax_type_desc"
            ddltxtTax.DataValueField = "id"
            ddltxtTax.DataBind()
         
            strSql = ""
            strSql += "USP_LOCATION_CUSTOMER_WISE '" & lblTariffId.Text & "','Bond'"
            dt = db.sub_GetDatatable(strSql)
            ddlLocation.DataSource = dt
            ddlLocation.DataTextField = "Location"
            ddlLocation.DataValueField = "LocationID"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_GST_Search_Domestic where userid='" & Session("UserId_DomCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows.Count > 0 Then
                    txtgstin.Text = Trim(dt.Rows(0)("GSTNo") & "")
                    txtgstname.Text = Trim(dt.Rows(0)("GSTName") & "")
                    lblstatecode.Text = Val(dt.Rows(0)("Statecode"))
                    lblpartyid.Text = Trim(dt.Rows(0)("Gstid") & "")
                    ddlcustomer.Focus()
                    Call Sub_SGTRate()
                    FillGrid()                    

                Else
                    txtgstin.Text = ""
                    txtgstname.Text = ""
                    lblstatecode.Text = ""
                    txtgstin.Focus()
                    lblpartyid.Text = ""
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Sub_SGTRate()
        Try
            Dim compid As String = ""
            strSql = ""
            strSql += "select Tinnumber from settings"
            dt9 = db.sub_GetDatatable(strSql)
            If dt9.Rows.Count > 0 Then
                compid = Trim(dt9.Rows(0)(0))
            End If
            strSql = ""
            strSql += "USP_GST_Cal_Domestic_other " & Val(ddlTax.SelectedValue) & ""
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                dblSGST = Val(dt1.Rows(0)("SGST"))
                dblCGST = Val(dt1.Rows(0)("CGST"))
                dblIGST = Val(dt1.Rows(0)("IGST"))
                dbltaxgroupid = Trim(dt1.Rows(0)("settingsID") & "")
                strSGSTPer = "SGST " & dblSGST & "%"
                StrCGSTPEr = "CGST " & dblCGST & "%"
                StrIGSTPer = "IGST " & dblIGST & "%"
                If lblstatecode.Text = compid Then
                    dblIGST = 0
                    StrIGSTPer = "IGST " & dblIGST & "%"
                Else
                    dblSGST = 0
                    dblCGST = 0
                    strSGSTPer = "SGST " & dblSGST & "%"
                    StrCGSTPEr = "CGST " & dblCGST & "%"
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            'If txtStorageFrom.Text <> "" And txtStorageUpto.Text <> "" Then
            '    If Convert.ToDateTime(txtStorageFrom.Text).ToString("yyyyMMdd") > Convert.ToDateTime(txtStorageUpto.Text).ToString("yyyyMMdd") Then
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter valid date');", True)
            '        txtStorageUpto.Focus()
            '        btnSave.Text = "Save"
            '        btnSave.Attributes.Add("Class", "btn btn-primary")
            '        Exit Sub
            '    End If
            'End If
            'If txtInsFrom.Text <> "" And txtInsUpto.Text <> "" Then
            '    If Convert.ToDateTime(txtInsFrom.Text).ToString("yyyyMMdd") > Convert.ToDateTime(txtInsUpto.Text).ToString("yyyyMMdd") Then
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter valid date');", True)
            '        txtInsUpto.Focus()
            '        btnSave.Text = "Save"
            '        btnSave.Attributes.Add("Class", "btn btn-primary")
            '        Exit Sub
            '    End If
            'End If
            
            Dim strWorkyear As String = ""
            Dim InvoiceDate As Date = Trim(txtinvdate.Text)
            If InvoiceDate.Month < 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") - 1 & "-" & Format(InvoiceDate, "yy")
            ElseIf InvoiceDate.Month >= 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") & "-" & Format(InvoiceDate, "yy") + 1
            End If
            Dim count As Double = 0, dblassessno As Double = 0
            Dim dblSumSGSTAmt As Double = 0, dblSumNetAmtTotal As Double = 0, dblSumCGSTAmt As Double = 0, dblSumIGSTAmt As Double = 0, dblgrandtotal As Double = 0

            For Each row In grdcharges.Rows
                count += 1
            Next
            If Not count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No details selected for assessment');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary")
                Exit Sub
            End If
            'strSql = ""
            'strSql += "select * from NOC where NOCNo='" & Trim(txtnocno.Text & "") & "' and iscancel=0"
            'dt1 = db.sub_GetDatatable(strSql)
            'If Not dt1.Rows.Count > 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('NOC No not found. Please enter valid NOC No.');", True)
            '    txtnocno.Focus()
            '    btnSave.Text = "Save"
            '    btnSave.Attributes.Add("Class", "btn btn-primary")
            '    Exit Sub
            'End If
            Call Sub_SGTRate()

            strSql = ""
            strSql += "SELECT isnull(MAX(AssessNo),0) FROM Domestic_assessM WITH(XLOCK) WHERE WorkYear='" & strWorkyear & "' "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows(0)(0) = 0 Then
                dblassessno = 1
            Else
                dblassessno = Val(dt.Rows(0)(0)) + 1
            End If
            For Each row In grdcharges.Rows
                strSql = ""
                strSql += "USP_insert_into_DOMESTIC_assessd_other_assessment '" & dblassessno & "','" & strWorkyear & "','" & Val(CType(row.FindControl("lblaccntid"), Label).Text) & "',"
                strSql += "'" & Val(CType(row.FindControl("lblntamnt"), Label).Text) & "','" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblSGST / 100), "0.00") & "',"
                strSql += "'" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblCGST / 100), "0.00") & "','" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblIGST / 100), "0.00") & "',"
                strSql += "'" & dbltaxgroupid & "','" & Trim(txtcontainerno.Text & "") & "','" & Val(CType(row.FindControl("lblRate"), Label).Text) & "','" & Val(CType(row.FindControl("lblQty"), Label).Text) & "','" & Val(CType(row.FindControl("lblWeight"), Label).Text) & "'"

                db.sub_ExecuteNonQuery(strSql)
            Next
            strSql = ""
            strSql += "get_sum_charges_Domestic_oTHER '" & dblassessno & "', '" & strWorkyear & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                dblSumSGSTAmt = Val(dt.Rows(0)("SGST"))
                dblSumCGSTAmt = Val(dt.Rows(0)("CGST"))
                dblSumIGSTAmt = Val(dt.Rows(0)("IGST"))
                dblSumNetAmtTotal = Val(dt.Rows(0)("Amount"))
                dblgrandtotal = Val(dblSumSGSTAmt) + Val(dblSumCGSTAmt) + Val(dblSumIGSTAmt) + Val(dblSumNetAmtTotal)
            End If
            strSql = ""
            strSql += "USP_INSERT_INTO_DOMESTIC_ASSESSM_OTHER '" & dblassessno & "','" & strWorkyear & "','" & Val(ddlcustomer.SelectedValue) & "',"
            strSql += "'" & Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd") & "',"
            strSql += "'" & dblSumNetAmtTotal & "','" & dblSumNetAmtTotal & "','" & Session("UserId_DomCFS") & "',"
            strSql += "'" & dblSumSGSTAmt & "','" & dblSumCGSTAmt & "','" & dblSumIGSTAmt & "','" & Format(dblgrandtotal, "0") & "','" & lblpartyid.Text & "','" & Trim(ddlInvoiceType.SelectedItem.Text) & "','" & Val(ddlLocation.SelectedValue) & "','" & Replace(Trim(txtremarks.Text), "'", "''") & "',"
            strSql += "'" & Replace(Trim(txtinword.Text), "'", "''") & "','" & Replace(Trim(txtoutword.Text), "'", "''") & "','" & Trim(ddlCommodity.SelectedItem.Text & "") & "','" & Trim(txtRakeno.Text & "") & "','" & Trim(ddltxtTax.SelectedValue & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            Clear()
            txtinvdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtcontainerno.Text = ""
            txtassessno.Text = dblassessno
            txtworkyear.Text = strWorkyear
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary")
            lblSession.Text = "Record saved successfully for Assess NO " & dblassessno & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub FillGrid()
        Try
            strSql = ""
            strSql += "USP_fill_grid_other_assessment_DOMESTIC '" & Session("UserId_DomCFS") & "'"
            ds = db.sub_GetDataSets(strSql)
            grdcharges.DataSource = ds.Tables(0)
            grdcharges.DataBind()
            If ds.Tables(0).Rows.Count > 0 Then
                divtblWOTOtal.Attributes.Add("style", "display:block")
            Else
                divtblWOTOtal.Attributes.Add("style", "display:none")
            End If
            If Val(ds.Tables(1).Rows(0)(0)) <> 0 Then
                dblGroup1Amt = Val(ds.Tables(1).Rows(0)(0))
                sub_CalcTotals()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CalcTotals()
        Try
            Dim dbltotal As Double = 0, dblvalSGST As Double = 0, dbltotalsgst As Double = 0, dbltotalcgst As Double = 0, dbltotaligst As Double = 0, dblvalCGST As Double = 0, dblvalIGST As Double = 0, dbldisc As Double = 0, dblalltotal As Double = 0, dblTaxAmount As Double = 0
            dblTaxAmount = Val(lblTaxAmount.Text)
            dbltotal = dblGroup1Amt            
            dbltotalcgst = Format(dblTaxAmount * (dblSGST / 100), "0.00")
            dbltotalsgst = Format(dblTaxAmount * (dblCGST / 100), "0.00")
            dbltotaligst = Format(dblTaxAmount * (dblIGST / 100), "0.00")
            strSql = ""
            strSql += "select CEILING(" & dbltotalcgst & ") as totalcgst,CEILING(" & dbltotalsgst & ") as totalsgst,Ceiling(" & dbltotaligst & ") as totaligst"
            dt = db.sub_GetDatatable(strSql)
            dblalltotal = dbltotal - dbldisc + Val(dt.Rows(0)("totalsgst")) + Val(dt.Rows(0)("totalcgst")) + Val(dt.Rows(0)("totaligst"))
            lblTotal.Text = dbltotal
            lbldisc.Text = dbldisc
            lblSgstPer.Text = strSGSTPer
            lblCgstPer.Text = StrCGSTPEr
            lblIgstPer.Text = StrIGSTPer
            lblCGST.Text = Val(dt.Rows(0)("totalcgst"))
            lblSGST.Text = Val(dt.Rows(0)("totalsgst"))
            lblIGST.Text = Val(dt.Rows(0)("totaligst"))
            lblAllTotal.Text = Format(dblalltotal, "0")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkadd_Click(sender As Object, e As EventArgs) Handles lnkadd.Click
        Try
            Dim dblAmount As Double = 0
            dblAmount = Val(lblTaxAmount.Text)
            strSql = ""
            strSql += "select * from Temp_Other_Assessment_DOMESTIC where IsCancel=0 and AccountID='" & Trim(ddlaccntheads.SelectedValue) & "' and UserID='" & Session("UserId_DomCFS") & "'and Qty=" & Trim(txtQty.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('These charges are already applied.');", True)
                lnkadd.Attributes.Add("Class", "btn btn-info")
                ddlaccntheads.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql += "USP_insert_into_temp_other_assessment_DOMESTIC '" & Trim(ddlaccntheads.SelectedValue) & "','" & Trim(txtamount.Text & "") & "','" & Session("UserId_DomCFS") & "','" & Trim(txtQty.Text & "") & "','" & Trim(txtrate.Text & "") & "','" & Trim(txtWeight.Text & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            dblAmount += Val(txtamount.Text)
            lblTaxAmount.Text = dblAmount
            Call Sub_SGTRate()
            FillGrid()
            ddlaccntheads.SelectedValue = 0
            txtamount.Text = ""
            txtQty.Text = ""
            txtrate.Text = ""
            txtWeight.Text = ""

            lnkadd.Attributes.Add("Class", "btn btn-info")
            ddlaccntheads.Focus()
        Catch ex As Exception
            lnkadd.Attributes.Add("Class", "btn btn-info")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim AccountId As String = lnkcancel.CommandArgument
            strSql = ""
            strSql += "Update Temp_Other_Assessment_DOMESTIC set IsCancel=1 where accountid='" & AccountId & "'"
            db.sub_ExecuteNonQuery(strSql)
            Sub_SGTRate()
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            txtgstin.Text = ""
            txtgstname.Text = ""
            ddlcustomer.SelectedValue = 0
            ddlCHA.SelectedValue = 0
            ddlimporter.SelectedValue = 0
            txtbeno.Text = ""
            txtbondno.Text = ""
            ddlaccntheads.SelectedValue = 0
            txtamount.Text = ""
            txtremarks.Text = ""
            txtStorageFrom.Text = ""
            txtStorageUpto.Text = ""
            ddlTax.SelectedValue = 0
            ddlLocation.SelectedValue = 0
            ddlInvoiceType.SelectedValue = 0
            txtQty.Text = ""
            txtrate.Text = ""
            txtvaliddate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            'txtinvdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            db.sub_ExecuteNonQuery("Delete from Temp_Other_Assessment_DOMESTIC Where UserID=" & Session("UserId_DomCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_GST_Search_Domestic Where UserID=" & Session("UserId_DomCFS") & "")
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnoksave_ServerClick(sender As Object, e As EventArgs)
        Try
            lblquoteApprove.Text = "Do you wish to print Invoice?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtcontainerno_TextChanged(sender As Object, e As EventArgs)
        Try
            Clear()
            strSql = ""
            strSql += "USP_NOC_TEXT_CHANGED_OTHER_ASSESSMENT '" & Trim(txtcontainerno.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddlcustomer.SelectedValue = Trim(dt.Rows(0)("CustID") & "")
                ddlCHA.SelectedValue = Trim(dt.Rows(0)("CHAID") & "")
                ddlimporter.SelectedValue = Trim(dt.Rows(0)("ImporterId") & "")
                txtbeno.Text = Trim(dt.Rows(0)("BOENo") & "")
                txtbondno.Text = Trim(dt.Rows(0)("BondNo") & "")
                ddlwarehouse.SelectedValue = Trim(dt.Rows(0)("wr_code") & "")
                txtStorageFrom.Text = Convert.ToDateTime(Trim(dt.Rows(0)("NOCDate"))).ToString("yyyy-MM-dd")
                txtInsFrom.Text = Convert.ToDateTime(Trim(dt.Rows(0)("InsuDate"))).ToString("yyyy-MM-dd")

                strSql = ""
                strSql += "select top 1 TariffID from bond_tariffmaster where custid=" & Val(ddlcustomer.SelectedValue) & " order by AddedOn desc"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    lblTariffId.Text = Trim(dt.Rows(0)("TariffID") & "")
                End If
                strSql = ""
                strSql += "USP_LOCATION_CUSTOMER_WISE '" & lblTariffId.Text & "','Bond'"
                dt = db.sub_GetDatatable(strSql)
                ddlLocation.DataSource = dt
                ddlLocation.DataTextField = "Location"
                ddlLocation.DataValueField = "LocationID"
                ddlLocation.DataBind()
                ddlLocation.Items.Insert(0, New ListItem("--Select--", 0))
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('NOC No not found');", True)
                txtcontainerno.Text = ""
                txtcontainerno.Focus()
                Exit Sub
            End If
            txtgstin.Focus()
            UpdatePanel1.Update()
            UpdatePanel4.Update()
            'UpdatePanel6.Update()
            'UpdatePanel7.Update()
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnIndentlist_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_Other_Search_NOC where USERID=" & Session("UserId_DomCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtcontainerno.Text = Trim(dt.Rows(0)("NOCNO") & "")
                txtcontainerno_TextChanged(sender, e)
            End If
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlaccntheads_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim dblAmount As Double = 0
            dblAmount = Val(lblTaxAmount.Text)
            If ddlInvoiceType.SelectedValue = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select Invoice type first');", True)
                ddlInvoiceType.Focus()
                Exit Sub
            End If
            If ddlInvoiceType.SelectedValue = "Transport" Then
                If ddlLocation.SelectedValue = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select Location for Transport invoice');", True)
                    ddlLocation.Focus()
                    Exit Sub
                End If
            End If
            strSql = ""
            strSql += "USP_FETCH_OTHER_ASSESSMENT_AMOUNT '" & Trim(ddlInvoiceType.SelectedValue & "") & "','" & Val(ddlaccntheads.SelectedValue & "") & "','" & Val(ddlLocation.SelectedValue & "") & "','" & Trim(lblTariffId.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtamount.Text = Val(dt.Rows(0)("fixedamt"))
                If dt.Rows(0)("isSTax") = True Then
                    dblAmount += Val(dt.Rows(0)("fixedamt"))
                    lblIsTax.Text = 1
                Else
                    lblIsTax.Text = 0
                End If
            End If
            lblTaxAmount.Text = dblAmount
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

 

    Protected Sub txtWeight_TextChanged(sender As Object, e As EventArgs)
        Try
            txtamount.Text = Trim(txtWeight.Text & "") * Trim(txtrate.Text & "")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

   

    Protected Sub ddlCommodity_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = " select * from Commodity_Group_M where Commodity_Group_ID=" & Val(ddlCommodity.SelectedValue) & " "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddltxtTax.SelectedValue = Val(dt.Rows(0)("TaxGroupID") & "")
            Else
                ddltxtTax.SelectedValue = Val(dt.Rows(0)("TaxGroupID") & "")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtQty_TextChanged(sender As Object, e As EventArgs)
        Try
            txtamount.Text = Trim(txtrate.Text & "") * Trim(txtQty.Text & "")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
