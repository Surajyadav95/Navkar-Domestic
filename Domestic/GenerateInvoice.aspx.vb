Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds, ds1, ds2, ds3, ds4 As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim dblNetAmount As Double
    Dim dblNetAmount_IND As Double
    Dim dblSTaxOnAmount As Double
    Dim dblGroup1Amt As Double
    Dim dblGroup2Amt As Double
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
            db.sub_ExecuteNonQuery("Delete From Temp_Assessment_Domestic Where USERID=" & Session("UserId_DomCFS") & "")
            txtFromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-01T00:00")
            txtToDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            txtInvoiceDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")            
            txtValidUptoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            Filldropdown()
            strSql = ""
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
            grdImport.DataSource = dt
            grdImport.DataBind()
            txtgstin.Focus()
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_DOMESTIC_INVOICE_FILLdROPDOWN"
            ds = db.sub_GetDataSets(strSql)
            ddlCustomer.DataSource = ds.Tables(0)
            ddlCustomer.DataTextField = "AGName"
            ddlCustomer.DataValueField = "AGID"
            ddlCustomer.DataBind()
            ddlCustomer.Items.Insert(0, New ListItem("--Select--", 0))
            ddlTariff.DataSource = ds.Tables(1)
            ddlTariff.DataTextField = "TARIFFID"
            ddlTariff.DataValueField = "EntryID"
            ddlTariff.DataBind()
            ddlTariff.Items.Insert(0, New ListItem("--Select--", 0))
            ddlTax.DataSource = ds.Tables(2)
            ddlTax.DataTextField = "Tax"
            ddlTax.DataValueField = "settingsID"
            ddlTax.DataBind()

            ddllocation.DataSource = ds.Tables(3)
            ddllocation.DataTextField = "location"
            ddllocation.DataValueField = "location"
            ddllocation.DataBind()
            ddllocation.Items.Insert(0, New ListItem("ALL", 0))
            strSql = ""
            strSql += "USP_Fill_Noc_list_DOMESTIC"
            ds = db.sub_GetDataSets(strSql)
            ddlCommodity.DataSource = ds.Tables(8)
            ddlCommodity.DataTextField = "Commodity_Group_Name"
            ddlCommodity.DataValueField = "Commodity_Group_ID"
            ddlCommodity.DataBind()


            ddltxtTax.DataSource = ds.Tables(9)
            ddltxtTax.DataTextField = "tax_type_desc"
            ddltxtTax.DataValueField = "id"
            ddltxtTax.DataBind()

          
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim strWorkyear As String = ""
            Dim InvoiceDate As Date = Trim(txtInvoiceDate.Text)
            If InvoiceDate.Month < 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") - 1 & "-" & Format(InvoiceDate, "yy")
            ElseIf InvoiceDate.Month >= 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") & "-" & Format(InvoiceDate, "yy") + 1
            End If
            Dim count As Double = 0, dblassessno As Double = 0, blnAmountDets As Boolean = False
            Dim dblSumSGSTAmt As Double = 0, dblSumNetAmtTotal As Double = 0, dblSumCGSTAmt As Double = 0, dblSumIGSTAmt As Double = 0, dblgrandtotal As Double = 0

            For Each row In rptIndentLIst.Rows
                blnAmountDets = True
                Exit For
            Next
            If blnAmountDets = False Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No details found for Invoice');", True)
                Exit Sub
            End If
            If Not (Val(lblCGST.Text) = 0 And Val(lblSGST.Text) = 0 And Val(lblIGST.Text) = 0) Then
                Call Sub_SGTRate()
            Else
                dbltaxgroupid = 11
            End If
            strSql = ""
            strSql += "select isnull(max(Assessno),0)+1 from Domestic_ProformaM WITH(XLOCK) WHERE WorkYear='" & strWorkyear & "' "
            dt = db.sub_GetDatatable(strSql)
            If Val(dt.Rows(0)(0)) = 0 Then
                dblassessno = 1
            Else
                dblassessno = Val(dt.Rows(0)(0))
            End If
            For Each row In rptIndentLIst.Rows
                strSql = ""
                strSql += "USP_INSERT_INTO_Domestic_ProformaD '" & dblassessno & "','" & strWorkyear & "','" & Val(CType(row.FindControl("lblaccntid"), Label).Text) & "',"
                strSql += "'" & Val(CType(row.FindControl("lblntamnt"), Label).Text) & "','" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblSGST / 100), "0.00") & "',"
                strSql += "'" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblCGST / 100), "0.00") & "','" & Format(Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblIGST / 100), "0.00") & "',"
                strSql += "'" & dbltaxgroupid & "','" & Trim(CType(row.FindControl("lblContainerNoforAmount"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTruckNoforAmount"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblLoadingNo"), Label).Text) & "'"
                strSql += ",'" & Val(CType(row.FindControl("lblQty"), Label).Text) & "','" & Val(CType(row.FindControl("lblWeight"), Label).Text) & "'"
                strSql += ",'" & Trim(ddlTariff.SelectedItem.Text) & "','" & Trim(ddlCriteria.SelectedItem.Text) & "','" & Trim(ddlInvoiceType.SelectedItem.Text) & "','" & Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(txtValidUptoDate.Text).ToString("yyyy-MM-dd") & "','" & Trim(ddlLorD.SelectedValue) & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next
            strSql = ""
            strSql += "get_sum_charges_Domestic '" & dblassessno & "', '" & strWorkyear & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                dblSumSGSTAmt = Val(dt.Rows(0)("SGST"))
                dblSumCGSTAmt = Val(dt.Rows(0)("CGST"))
                dblSumIGSTAmt = Val(dt.Rows(0)("IGST"))
                dblSumNetAmtTotal = Val(dt.Rows(0)("Amount"))
                dblgrandtotal = Val(dblSumSGSTAmt) + Val(dblSumCGSTAmt) + Val(dblSumIGSTAmt) + Val(dblSumNetAmtTotal)
            End If
            strSql = ""
            strSql += "USP_INSERT_INTO_Domestic_ProformaM '" & dblassessno & "','" & strWorkyear & "','" & Val(ddlCustomer.SelectedValue) & "',"
            strSql += "'" & Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd") & "',"            
            strSql += "'" & Trim(ddlTariff.SelectedItem.Text) & "','" & dblSumNetAmtTotal & "','" & dblSumNetAmtTotal & "','" & Session("UserId_DomCFS") & "',"
            strSql += "'" & dblSumSGSTAmt & "','" & dblSumCGSTAmt & "','" & dblSumIGSTAmt & "','" & dblgrandtotal & "','" & lblpartyid.Text & "','" & Replace(Trim(txtRemarks.Text), "'", "''") & "','" & Trim(ddlInvoiceType.SelectedItem.Text) & "'"
            strSql += ",'" & Replace(Trim(txtInWard.Text), "'", "''") & "','" & Replace(Trim(txtOutWard.Text), "'", "''") & "','" & Replace(Trim(txtRefNo.Text), "'", "''") & "','" & Trim(ddlCriteria.SelectedItem.Text) & "','" & Trim(ddlInvoiceType.SelectedItem.Text) & "'"
            strSql += ",'" & Replace(Trim(txtIGMNo.Text), "'", "''") & "','" & Replace(Trim(txtItemNo.Text), "'", "''") & "','" & Trim(ddlLorD.SelectedValue & "") & "','" & Replace(Trim(txtRakeNo.Text), "'", "''") & "','" & Trim(ddltxtTax.SelectedValue & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            txtassessno.Text = dblassessno
            txtworkyear.Text = strWorkyear
            txtInvoiceType.Text = Trim(ddlInvoiceType.SelectedValue)
            Clear()
            btnsave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-success btn-sm outline pull-right")

            lblSession.Text = "Record Saved successfully for Assess NO " & dblassessno & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-success btn-sm outline pull-right")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try            
            txtgstin.Text = ""
            txtgstname.Text = ""
            lblstatecode.Text = ""
            lblpartyid.Text = ""
            ddlTariff.SelectedValue = 0
            ddlInvoiceType.SelectedValue = "General"
            ddlCustomer.SelectedValue = 0
            ddlCriteria.SelectedValue = "Cargo"
            txtRefNo.Text = ""
            txtFromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-01T00:00")
            txtToDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            txtInvoiceDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            'txtAssessmentDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            db.sub_ExecuteNonQuery("Delete from Temp_Assessment_Domestic Where UserID=" & Session("UserId_DomCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_GST_Search_Domestic Where UserID=" & Session("UserId_DomCFS") & "")
            strSql = ""
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
            lblchargescount.Visible = False
            LBLNO.Text = dt.Rows.Count
            LBLNO.Visible = False
            divtblWOTOtal.Attributes.Add("style", "display:none")
            upModalSave1.Update()
            txtgstin.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnGSTSearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_GST_Search_Domestic where userid='" & Session("UserId_DomCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtgstin.Text = Trim(dt.Rows(0)("GSTNo") & "")
                txtgstname.Text = Trim(dt.Rows(0)("GSTName") & "")
                lblstatecode.Text = Val(dt.Rows(0)("Statecode"))
                lblpartyid.Text = Trim(dt.Rows(0)("Gstid") & "")
                'ddltariff.SelectedValue = Trim(dt.Rows(0)("TariffID") & "")
            End If
            If Not ddlInvoiceType.SelectedValue = "Import" Then
                divRegularGrid.Attributes.Add("style", "display:block;margin-left:10px;margin-right:0px;")
                divImportGrid.Attributes.Add("style", "display:none;margin-left:10px;margin-right:0px;")
                divIGMItem.Attributes.Add("style", "display:none;")
            Else
                divRegularGrid.Attributes.Add("style", "display:none;margin-left:10px;margin-right:0px;")
                divImportGrid.Attributes.Add("style", "display:block;margin-left:10px;margin-right:0px;")
                divIGMItem.Attributes.Add("style", "display:block;")
            End If
            'ddlCustomer.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub chkright_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dblchkcount As Double = 0
            For Each row In grdcontainer.Rows
                If CType(row.FindControl("chkright"), CheckBox).Checked = True Then
                    dblchkcount += 1
                End If
            Next
            If dblchkcount = grdcontainer.Rows.Count Then
                chkSelectAll.Checked = True
            Else
                chkSelectAll.Checked = False
            End If
            'UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chkSelectAll.Checked = True Then
                For Each row In grdcontainer.Rows
                    DirectCast(row.FindControl("chkright"), CheckBox).Checked = True
                Next
            Else
                For Each row In grdcontainer.Rows
                    DirectCast(row.FindControl("chkright"), CheckBox).Checked = False
                Next
            End If
            'UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try
            If Not ddlInvoiceType.SelectedValue = "Import" Then
                strSql = ""
                strSql += "USP_DOMESTIC_INVOICE_LOADED_CONTAINER_LIST " & Val(ddlCustomer.SelectedValue) & ",'" & Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtRefNo.Text & "") & "','" & Trim(ddlCriteria.SelectedItem.Text & "") & "','" & Trim(ddllocation.SelectedValue & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                grdcontainer.DataSource = dt
                grdcontainer.DataBind()
                chkright_CheckedChanged(sender, e)
                ddlTariff.Focus()
            Else
                strSql = ""
                strSql += "USP_DOMESTIC_IMPORT_INVOICE_IGM_ITEM_WISE '" & Trim(txtIGMNo.Text & "") & "','" & Trim(txtItemNo.Text & "") & "','" & Trim(txtContainerNo.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                grdImport.DataSource = dt
                grdImport.DataBind()
                chkright_CheckedChanged(sender, e)
                If dt.Rows.Count > 0 Then
                    ddlCustomer.SelectedValue = Val(dt.Rows(0)("CUSTOMERID") & "")
                    ddlCustomer_SelectedIndexChanged(sender, e)
                End If
                ddlTariff.Focus()
            End If
            If Not ddlInvoiceType.SelectedValue = "Import" Then
                divRegularGrid.Attributes.Add("style", "display:block;margin-left:10px;margin-right:0px;")
                divImportGrid.Attributes.Add("style", "display:none;margin-left:10px;margin-right:0px;")
                divIGMItem.Attributes.Add("style", "display:none;")
            Else
                divRegularGrid.Attributes.Add("style", "display:none;margin-left:10px;margin-right:0px;")
                divImportGrid.Attributes.Add("style", "display:block;margin-left:10px;margin-right:0px;")
                divIGMItem.Attributes.Add("style", "display:block;")
            End If
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btncal_Click(sender As Object, e As EventArgs)
        Try
            db.sub_ExecuteNonQuery("Delete from Temp_Assessment_Domestic Where UserID=" & Session("UserId_DomCFS") & "")
            dblSTaxOnAmount = 0
            dblGroup1Amt = 0
            dblGroup2Amt = 0
            Dim blnAccountFound As Boolean
            Dim blnContainerFound As Boolean = False
            If Not ddlInvoiceType.SelectedValue = "Import" Then
                For Each row As GridViewRow In grdcontainer.Rows
                    Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                    If chkright.Checked = True Then
                        blnContainerFound = True
                        Exit For
                    End If
                Next
            Else
                For Each row As GridViewRow In grdImport.Rows
                    Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                    If chkright.Checked = True Then
                        blnContainerFound = True
                        Exit For
                    End If
                Next
            End If
            
            If blnContainerFound = False Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No details selected for invoice.');", True)
                Exit Sub
            End If
            Call Sub_SGTRate()
            strSql = ""
            strSql += "USP_Domestic_assessment_for_accountid '" & Trim(ddlTariff.SelectedItem.Text) & "','" & Convert.ToDateTime(Trim(txtInvoiceDate.Text)).ToString("yyyyMMdd") & "','" & Trim(ddlCriteria.SelectedItem.Text & "") & "','" & Trim(ddlInvoiceType.SelectedValue) & "'"
            ds = db.sub_GetDataSets(strSql)
            If ddlInvoiceType.SelectedValue = "General" Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        dblNetAmount = 0
                        For Each row As GridViewRow In grdcontainer.Rows
                            Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                            If chkright.Checked = True Then
                                Call sub_fetchcharges(ds.Tables(0).Rows(i)("AccountID"), Trim(CType(row.FindControl("lblContainerNo"), Label).Text & ""), Trim(CType(row.FindControl("lblTruckNo"), Label).Text & ""), Val(CType(row.FindControl("lblSize"), Label).Text & ""), Convert.ToDateTime(Trim(CType(row.FindControl("lblStuffingDate"), Label).Text & "")).ToString("yyyy-MM-dd"), Trim(CType(row.FindControl("lblPkgs"), Label).Text & ""), Trim(CType(row.FindControl("lblWeight"), Label).Text & ""), Trim(CType(row.FindControl("lblArea"), Label).Text & ""), Convert.ToDateTime(Trim(CType(row.FindControl("lblLoadingDate"), Label).Text & "")).ToString("yyyy-MM-dd"), Val(CType(row.FindControl("lblLoadedID"), Label).Text & ""))
                                If dblNetAmount <> 0 Then
                                    strSql = ""
                                    strSql += "SELECT AccountName,GroupID,(" & dblNetAmount & ") as dblnetamount FROM Domestic_AccountMaster WHERE AccountID=" & Val(ds.Tables(0).Rows(i)("AccountID")) & ""
                                    dt4 = db.sub_GetDatatable(strSql)

                                    If dt4.Rows(0)("GroupID") = 1 Then
                                        dblGroup1Amt = dblGroup1Amt + Val(dt4.Rows(0)("dblnetamount"))
                                    Else
                                        dblGroup2Amt = dblGroup2Amt + Val(dt4.Rows(0)("dblnetamount"))
                                    End If
                                    strSql = ""
                                    strSql += "USP_insert_into_Temp_Assessment_Domestic '" & Trim(ds.Tables(0).Rows(i)("AccountID")) & "','" & Val(dt4.Rows(0)("dblnetamount")) & "',0,0,'" & Session("UserId_DomCFS") & "','" & Convert.ToDateTime(lblValidDate.Text).ToString("yyyy-MM-dd") & "'"
                                    strSql += ",'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTruckNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLoadedID"), Label).Text & "") & "'"
                                    strSql += ",'" & Val(CType(row.FindControl("lblPkgs"), Label).Text) & "','" & Val(CType(row.FindControl("lblWeight"), Label).Text) & "'"
                                    db.sub_ExecuteNonQuery(strSql)
                                End If
                            End If
                        Next
                    Next
                End If

                For Each row In grdcontainer.Rows
                    Dim dtAccountFound As New DataTable
                    strSql = ""
                    strSql += "USP_ADDITIONAL_CHARGES_FETCHING_DOMESTIC '" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLoadedID"), Label).Text & "") & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            strSql = ""
                            strSql += "select * from Temp_Assessment_Domestic where accountid='" & Val(dt.Rows(i)("accountid")) & "' and UserId='" & Session("UserId_DomCFS") & "'and rate='" & Val(dt.Rows(i)("rate")) & "'"
                            dtAccountFound = db.sub_GetDatatable(strSql)
                            If dtAccountFound.Rows.Count > 0 Then
                                blnAccountFound = True
                            End If
                            If blnAccountFound = False Then
                                strSql = ""
                                strSql += "USP_insert_into_Temp_Assessment_Domestic '" & Val(dt.Rows(i)("accountid")) & "','" & Val(dt.Rows(i)("amount")) & "',0,0,'" & Session("UserId_DomCFS") & "','" & Convert.ToDateTime(Trim(CType(row.FindControl("lblLoadingDate"), Label).Text & "")).ToString("yyyy-MM-dd") & "'"
                                strSql += ",'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTruckNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLoadedID"), Label).Text & "") & "'"
                                strSql += ",'" & Val(CType(row.FindControl("lblPkgs"), Label).Text) & "','" & Val(CType(row.FindControl("lblWeight"), Label).Text) & "','" & Val(dt.Rows(i)("rate")) & "'"
                                db.sub_ExecuteNonQuery(strSql)
                            End If
                            If dt.Rows(i)("GroupID") = 1 Then
                                dblGroup1Amt = dblGroup1Amt + Val(dt.Rows(i)("amount"))
                                If dt.Rows(i)("IsSTax") = True Then
                                    dblSTaxOnAmount += dblGroup1Amt
                                End If
                            Else
                                dblGroup2Amt = dblGroup2Amt + Val(dt.Rows(i)("amount"))
                                If dt.Rows(i)("IsSTax") = True Then
                                    dblSTaxOnAmount += dblGroup2Amt
                                End If
                            End If
                            blnAccountFound = False
                        Next
                    End If
                Next

            ElseIf ddlInvoiceType.SelectedValue = "Transport" Then
                If ds.Tables(2).Rows.Count > 0 Then
                    Dim dtTransport As New DataSet
                    For i = 0 To ds.Tables(2).Rows.Count - 1
                        dblNetAmount = 0
                        For Each row As GridViewRow In grdcontainer.Rows
                            Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                            If chkright.Checked = True Then
                                If Not Trim(CType(row.FindControl("lblTrailerNo"), Label).Text & "") = "" Then
                                    'If ds.Tables(2).Rows(i)("AccountID") = 5 Then
                                    strSql = "USP_Invoice_Domestic_Transport_Trailer '" & Trim(CType(row.FindControl("lblTrailerNo"), Label).Text & "") & "','" & Trim(ddlTariff.SelectedItem.Text & "") & "','" & Trim(ds.Tables(2).Rows(i)("AccountID") & "") & "','" & Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyyMMdd") & "','" & Trim(ddlCriteria.SelectedItem.Text & "") & "'"
                                    dtTransport = db.sub_GetDataSets(strSql)
                                    If dtTransport.Tables(1).Rows.Count > 0 Then
                                        For j = 0 To dtTransport.Tables(1).Rows.Count - 1
                                            If Trim(dtTransport.Tables(1).Rows(j)("SorF") & "") = "L" Then
                                                If (Val(CType(row.FindControl("lblLocationID"), Label).Text & "") = Val(dtTransport.Tables(1).Rows(j)("Location"))) Then
                                                    dblNetAmount += Val(dtTransport.Tables(1).Rows(j)("FixedAmt")) * Val(CType(row.FindControl("lblWeight"), Label).Text & "")
                                                    If dtTransport.Tables(1).Rows(j)("IsSTax") = True Then
                                                        dblSTaxOnAmount += dblNetAmount
                                                    End If
                                                End If
                                            End If
                                        Next
                                        'dblNetAmount += Val(dtTransport.Tables(2).Rows(i)("FixedAmt"))
                                    End If
                                    'End If
                                End If
                                If dblNetAmount <> 0 Then
                                    strSql = ""
                                    strSql += "SELECT AccountName,GroupID,(" & dblNetAmount & ") as dblnetamount FROM Domestic_AccountMaster WHERE AccountID=" & Val(ds.Tables(2).Rows(i)("AccountID")) & ""
                                    dt4 = db.sub_GetDatatable(strSql)

                                    If dt4.Rows(0)("GroupID") = 1 Then
                                        dblGroup1Amt = dblGroup1Amt + Val(dt4.Rows(0)("dblnetamount"))
                                    Else
                                        dblGroup2Amt = dblGroup2Amt + Val(dt4.Rows(0)("dblnetamount"))
                                    End If
                                    strSql = ""
                                    strSql += "USP_insert_into_Temp_Assessment_Domestic '" & Trim(ds.Tables(2).Rows(i)("AccountID")) & "','" & Val(dt4.Rows(0)("dblnetamount")) & "',0,0,'" & Session("UserId_DomCFS") & "','" & Convert.ToDateTime(Trim(CType(row.FindControl("lblLoadingDate"), Label).Text & "")).ToString("yyyy-MM-dd") & "'"
                                    strSql += ",'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTruckNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLoadedID"), Label).Text & "") & "'"
                                    strSql += ",'" & Val(CType(row.FindControl("lblPkgs"), Label).Text) & "','" & Val(CType(row.FindControl("lblWeight"), Label).Text) & "'"
                                    db.sub_ExecuteNonQuery(strSql)
                                End If
                            End If
                        Next
                    Next
                End If
            ElseIf ddlInvoiceType.SelectedValue = "Import" Then
                If ds.Tables(3).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(3).Rows.Count - 1
                        dblNetAmount = 0
                        For Each row As GridViewRow In grdImport.Rows
                            Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                            If chkright.Checked = True Then
                                Call sub_fetchcharges_Import(ds.Tables(3).Rows(i)("AccountID"), Trim(CType(row.FindControl("lblContainerNo"), Label).Text & ""), Trim(CType(row.FindControl("lblTruckNo"), Label).Text & ""), Val(CType(row.FindControl("lblSize"), Label).Text & ""), Convert.ToDateTime(Trim(CType(row.FindControl("lblStuffingDate"), Label).Text & "")).ToString("yyyy-MM-dd"), Trim(CType(row.FindControl("lblPkgs"), Label).Text & ""), Trim(CType(row.FindControl("lblWeight"), Label).Text & ""), Trim(CType(row.FindControl("lblArea"), Label).Text & ""), Convert.ToDateTime(Trim(CType(row.FindControl("lblLoadingDate"), Label).Text & "")).ToString("yyyy-MM-dd"), Val(CType(row.FindControl("lblLoadedID"), Label).Text & ""))
                                If dblNetAmount <> 0 Then
                                    strSql = ""
                                    strSql += "SELECT AccountName,GroupID,(" & dblNetAmount & ") as dblnetamount FROM Domestic_AccountMaster WHERE AccountID=" & Val(ds.Tables(3).Rows(i)("AccountID")) & ""
                                    dt4 = db.sub_GetDatatable(strSql)

                                    If dt4.Rows(0)("GroupID") = 1 Then
                                        dblGroup1Amt = dblGroup1Amt + Val(dt4.Rows(0)("dblnetamount"))
                                    Else
                                        dblGroup2Amt = dblGroup2Amt + Val(dt4.Rows(0)("dblnetamount"))
                                    End If
                                    strSql = ""
                                    strSql += "USP_insert_into_Temp_Assessment_Domestic '" & Trim(ds.Tables(3).Rows(i)("AccountID")) & "','" & Val(dt4.Rows(0)("dblnetamount")) & "',0,0,'" & Session("UserId_DomCFS") & "','" & Convert.ToDateTime(lblValidDate.Text).ToString("yyyy-MM-dd") & "'"
                                    strSql += ",'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTruckNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLoadedID"), Label).Text & "") & "'"
                                    strSql += ",'" & Val(CType(row.FindControl("lblPkgs"), Label).Text) & "','" & Val(CType(row.FindControl("lblWeight"), Label).Text) & "'"
                                    db.sub_ExecuteNonQuery(strSql)
                                End If
                            End If
                        Next
                    Next
                End If

                For Each row In grdImport.Rows
                    Dim dtAccountFound As New DataTable
                    strSql = ""
                    strSql += "USP_ADDITIONAL_CHARGES_FETCHING_DOMESTIC '" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLoadedID"), Label).Text & "") & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            strSql = ""
                            strSql += "select * from Temp_Assessment_Domestic where accountid='" & Val(dt.Rows(i)("accountid")) & "' and UserId='" & Session("UserId_DomCFS") & "'"
                            dtAccountFound = db.sub_GetDatatable(strSql)
                            If dtAccountFound.Rows.Count > 0 Then
                                blnAccountFound = True
                            End If
                            If blnAccountFound = False Then
                                strSql = ""
                                strSql += "USP_insert_into_Temp_Assessment_Domestic '" & Val(dt.Rows(i)("accountid")) & "','" & Val(dt.Rows(i)("amount")) & "',0,0,'" & Session("UserId_DomCFS") & "','" & Convert.ToDateTime(Trim(CType(row.FindControl("lblLoadingDate"), Label).Text & "")).ToString("yyyy-MM-dd") & "'"
                                strSql += ",'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTruckNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLoadedID"), Label).Text & "") & "'"
                                strSql += ",'" & Val(CType(row.FindControl("lblPkgs"), Label).Text) & "','" & Val(CType(row.FindControl("lblWeight"), Label).Text) & "'"
                                db.sub_ExecuteNonQuery(strSql)
                            End If
                            If dt.Rows(i)("GroupID") = 1 Then
                                dblGroup1Amt = dblGroup1Amt + Val(dt.Rows(i)("amount"))
                                If dt.Rows(i)("IsSTax") = True Then
                                    dblSTaxOnAmount += dblGroup1Amt
                                End If
                            Else
                                dblGroup2Amt = dblGroup2Amt + Val(dt.Rows(i)("amount"))
                                If dt.Rows(i)("IsSTax") = True Then
                                    dblSTaxOnAmount += dblGroup2Amt
                                End If
                            End If
                            blnAccountFound = False
                        Next
                    End If
                Next
            ElseIf ddlInvoiceType.SelectedValue = "General 1" Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        dblNetAmount = 0
                        For Each row As GridViewRow In grdcontainer.Rows
                            Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                            If chkright.Checked = True Then
                                Call sub_fetchcharges(ds.Tables(0).Rows(i)("AccountID"), Trim(CType(row.FindControl("lblContainerNo"), Label).Text & ""), Trim(CType(row.FindControl("lblTruckNo"), Label).Text & ""), Val(CType(row.FindControl("lblSize"), Label).Text & ""), Convert.ToDateTime(Trim(CType(row.FindControl("lblStuffingDate"), Label).Text & "")).ToString("yyyy-MM-dd"), Trim(CType(row.FindControl("lblPkgs"), Label).Text & ""), Trim(CType(row.FindControl("lblWeight"), Label).Text & ""), Trim(CType(row.FindControl("lblArea"), Label).Text & ""), Convert.ToDateTime(Trim(CType(row.FindControl("lblLoadingDate"), Label).Text & "")).ToString("yyyy-MM-dd"), Val(CType(row.FindControl("lblLoadedID"), Label).Text & ""))
                                If dblNetAmount <> 0 Then
                                    strSql = ""
                                    strSql += "SELECT AccountName,GroupID,(" & dblNetAmount & ") as dblnetamount FROM Domestic_AccountMaster WHERE AccountID=" & Val(ds.Tables(0).Rows(i)("AccountID")) & ""
                                    dt4 = db.sub_GetDatatable(strSql)

                                    If dt4.Rows(0)("GroupID") = 1 Then
                                        dblGroup1Amt = dblGroup1Amt + Val(dt4.Rows(0)("dblnetamount"))
                                    Else
                                        dblGroup2Amt = dblGroup2Amt + Val(dt4.Rows(0)("dblnetamount"))
                                    End If
                                    strSql = ""
                                    strSql += "USP_insert_into_Temp_Assessment_Domestic '" & Trim(ds.Tables(0).Rows(i)("AccountID")) & "','" & Val(dt4.Rows(0)("dblnetamount")) & "',0,0,'" & Session("UserId_DomCFS") & "','" & Convert.ToDateTime(lblValidDate.Text).ToString("yyyy-MM-dd") & "'"
                                    strSql += ",'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTruckNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLoadedID"), Label).Text & "") & "'"
                                    strSql += ",'" & Val(CType(row.FindControl("lblPkgs"), Label).Text) & "','" & Val(CType(row.FindControl("lblWeight"), Label).Text) & "'"
                                    db.sub_ExecuteNonQuery(strSql)
                                End If
                            End If
                        Next
                    Next
                End If

                For Each row In grdcontainer.Rows
                    Dim dtAccountFound As New DataTable
                    strSql = ""
                    strSql += "USP_ADDITIONAL_CHARGES_FETCHING_DOMESTIC '" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLoadedID"), Label).Text & "") & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            strSql = ""
                            strSql += "select * from Temp_Assessment_Domestic where accountid='" & Val(dt.Rows(i)("accountid")) & "' and UserId='" & Session("UserId_DomCFS") & "'"
                            dtAccountFound = db.sub_GetDatatable(strSql)
                            If dtAccountFound.Rows.Count > 0 Then
                                blnAccountFound = True
                            End If
                            If blnAccountFound = False Then
                                strSql = ""
                                strSql += "USP_insert_into_Temp_Assessment_Domestic '" & Val(dt.Rows(i)("accountid")) & "','" & Val(dt.Rows(i)("amount")) & "',0,0,'" & Session("UserId_DomCFS") & "','" & Convert.ToDateTime(Trim(CType(row.FindControl("lblLoadingDate"), Label).Text & "")).ToString("yyyy-MM-dd") & "'"
                                strSql += ",'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTruckNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLoadedID"), Label).Text & "") & "'"
                                strSql += ",'" & Val(CType(row.FindControl("lblPkgs"), Label).Text) & "','" & Val(CType(row.FindControl("lblWeight"), Label).Text) & "'"
                                db.sub_ExecuteNonQuery(strSql)
                            End If
                            If dt.Rows(i)("GroupID") = 1 Then
                                dblGroup1Amt = dblGroup1Amt + Val(dt.Rows(i)("amount"))
                                If dt.Rows(i)("IsSTax") = True Then
                                    dblSTaxOnAmount += dblGroup1Amt
                                End If
                            Else
                                dblGroup2Amt = dblGroup2Amt + Val(dt.Rows(i)("amount"))
                                If dt.Rows(i)("IsSTax") = True Then
                                    dblSTaxOnAmount += dblGroup2Amt
                                End If
                            End If
                            blnAccountFound = False
                        Next
                    End If
                Next
            End If
            grid1()
            sub_CalcTotals()
            If Not ddlInvoiceType.SelectedValue = "Import" Then
                divRegularGrid.Attributes.Add("style", "display:block;margin-left:10px;margin-right:0px;")
                divImportGrid.Attributes.Add("style", "display:none;margin-left:10px;margin-right:0px;")
                divIGMItem.Attributes.Add("style", "display:none;")
            Else
                divRegularGrid.Attributes.Add("style", "display:none;margin-left:10px;margin-right:0px;")
                divImportGrid.Attributes.Add("style", "display:block;margin-left:10px;margin-right:0px;")
                divIGMItem.Attributes.Add("style", "display:block;")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub grid1()
        strSql = ""
        strSql += "select * from Temp_Assessment_Domestic where userid=" & Session("UserId_DomCFS") & ""
        dt = db.sub_GetDatatable(strSql)
        If dt.Rows.Count > 0 Then
            lblchargescount.Visible = True
            LBLNO.Text = dt.Rows.Count
            LBLNO.Visible = True
            divtblWOTOtal.Attributes.Add("style", "display:block")
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
        Else
            lblchargescount.Visible = False
            LBLNO.Text = dt.Rows.Count
            LBLNO.Visible = False
            divtblWOTOtal.Attributes.Add("style", "display:none")
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No charges found!');", True)
        End If
        upModalSave1.Update()
    End Sub
    Private Sub sub_CalcTotals()
        Try
            Dim dbltotal As Double = 0, dblvalSGST As Double = 0, dbltotalsgst As Double = 0, dbltotalcgst As Double = 0, dbltotaligst As Double = 0, dblvalCGST As Double = 0, dblvalIGST As Double = 0, dbldisc As Double = 0, dblalltotal As Double = 0
            dblGroup1Amt = dblGroup1Amt + dblGroup2Amt
            dbltotal = dblGroup1Amt
            dbltotalcgst = Format(dblSTaxOnAmount * (dblSGST / 100), "0.00")
            dbltotalsgst = Format(dblSTaxOnAmount * (dblCGST / 100), "0.00")
            dbltotaligst = Format(dblSTaxOnAmount * (dblIGST / 100), "0.00")
            strSql = ""
            strSql += "select CEILING(" & dbltotalcgst & ") as totalcgst,CEILING(" & dbltotalsgst & ") as totalsgst,CEILING(" & dbltotaligst & ") as totaligst"
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
            strSql = ""
            strSql += "select round(" & dblalltotal & ",0) as dblalltotal"
            dt1 = db.sub_GetDatatable(strSql)
            lblAllTotal.Text = Val(dt1.Rows(0)("dblalltotal"))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_fetchcharges(strAccountID As String, ContainerNo As String, TruckNo As String, Size As Integer, InDate As DateTime, dblPackages As Double, dblweight As Double, dblArea As Double, validDate As DateTime, LoadedID As Integer)
        Try
            Dim dblSQM As Double = 0, dblPerc As Double = 0, dblAmount As Double = 0, dblDestuffDays As Double = 0, dblIGMWeight As Double = 0, dblPaidAmount As Double = 0
            Dim blnSTax As Boolean = False
            Dim strAccountName As String = ""
            Dim dblDestuffDate As Date
            Dim intDays As Double = 0
            Dim intMonth As Double = 0

            Dim Intweeks As Integer = 0

            lblValidDate.Text = validDate
            dblNetAmount = 0
            dblPaidAmount = 0
            intDays = 0
            strSql = ""
            strSql += "USP_calculation_fetchcharges_Domestic '" & Trim(ddlTariff.SelectedItem.Text & "") & "','" & strAccountID & "','" & Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyyMMdd") & "','" & ContainerNo & "','" & TruckNo & "'," & LoadedID & ",'" & Trim(ddlCriteria.SelectedItem.Text) & "'"
            strSql += ",'" & Trim(txtRefNo.Text) & "','" & Trim(ddlInvoiceType.SelectedValue) & "'"
            ds1 = db.sub_GetDataSets(strSql)

            If ds1.Tables(3).Rows.Count > 0 Then
                'dblPaidAmount = dblPaidAmount + Val(ds1.Tables(2).Rows(0)(0))
                If strAccountID = 9 And Val(ds1.Tables(3).Rows(0)(0)) > 0 Then
                    Exit Sub
                End If
            End If
            If ds1.Tables(0).Rows.Count > 0 Then
                intDays = DateDiff("d", InDate, validDate) + 1
                'intDays = DateDiff("d", InDate, "2019-05-27") + 1

                'intMonth = DateDiff("m", InDate, validDate)
            End If
            If intDays Mod 30 = 0 Then
                intMonth = intDays / 30
            Else
                intMonth = Int((intDays / 30)) + 1
            End If
            If intDays Mod 7 = 0 Then
                Intweeks = intDays / 7
            Else
                Intweeks = Int((intDays / 7)) + 1
            End If
            Dim dtFetchCharges As New DataTable
            strSql = ""
            strSql += "select * from Temp_Assessment_Domestic where UserId=" & Session("UserId_DomCFS") & " and accountid='" & strAccountID & "' and ContainerNo='" & Trim(ContainerNo & "") & "'"
            dtFetchCharges = db.sub_GetDatatable(strSql)
            For i = 0 To ds1.Tables(0).Rows.Count - 1
                If Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "S" Then
                    If dtFetchCharges.Rows.Count > 0 Then
                        Exit Sub
                    End If
                    dblNetAmount = dblNetAmount + slab_CalcAmount(ds1.Tables(0).Rows(i)("SlabID"), intDays, 0, Val(dblweight), Convert.ToDateTime(InDate).ToString("dd-MMM-yyyy HH:mm"), strAccountID, ds1.Tables(0).Rows(i)("Size"), Size, validDate)
                ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "F" And ds1.Tables(1).Rows(i)("ConsiderArea") = False Then
                    If dtFetchCharges.Rows.Count > 0 Then
                        Exit Sub
                    End If
                    dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt"))
                ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "20" And Size = 20 Then
                    If dtFetchCharges.Rows.Count > 0 Then
                        Exit Sub
                    End If
                    dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt"))
                ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "40" And Size = 40 Then
                    If dtFetchCharges.Rows.Count > 0 Then
                        Exit Sub
                    End If
                    dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt"))
                ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "45" And Size = 45 Then
                    If dtFetchCharges.Rows.Count > 0 Then
                        Exit Sub
                    End If
                    dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt"))
                ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "Q" Then
                    dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(dblPackages)
                ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "O" Then
                    dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(dblweight)
                ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "D" Then
                    dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(Val(dblweight) / 1000) * intDays
                ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "F" And ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                    dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(dblArea) * Intweeks
                ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "M" And ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                    dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(dblArea) * intMonth
                ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "A" And ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                    dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(dblArea) * intDays
                ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "P" Then
                    If dtFetchCharges.Rows.Count > 0 Then
                        Exit Sub
                    End If
                    dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) + slab_CalcAmount(ds1.Tables(0).Rows(i)("SlabID"), intDays, 0, Val(dblweight), Convert.ToDateTime(InDate).ToString("dd-MMM-yyyy HH:mm"), strAccountID, ds1.Tables(0).Rows(i)("Size"), Size, validDate)
                End If
            Next
            If ds1.Tables(2).Rows.Count > 0 Then
                dblPaidAmount = dblPaidAmount + Val(ds1.Tables(2).Rows(0)(0))
            End If
            dblNetAmount = dblNetAmount - dblPaidAmount
            If dblNetAmount < 0 Then
                dblNetAmount = 0
            End If
            dblNetAmount = Format(dblNetAmount, "0.00")
            If ds1.Tables(0).Rows.Count > 0 Then
                If ds1.Tables(0).Rows(0)("IsSTax") = True Then
                    dblSTaxOnAmount += dblNetAmount
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_fetchcharges_Import(strAccountID As String, ContainerNo As String, TruckNo As String, Size As Integer, InDate As DateTime, dblPackages As Double, dblweight As Double, dblArea As Double, validDate As DateTime, LoadedID As Integer)
        Try
            Dim dblSQM As Double = 0, dblPerc As Double = 0, dblAmount As Double = 0, dblDestuffDays As Double = 0, dblIGMWeight As Double = 0, dblPaidAmount As Double = 0
            Dim blnSTax As Boolean = False
            Dim strAccountName As String = ""
            Dim dblDestuffDate As Date
            Dim intDays As Double = 0
            Dim intMonth As Double = 0

            Dim Intweeks As Integer = 0

            validDate = txtValidUptoDate.Text
            lblValidDate.Text = validDate
            dblNetAmount = 0
            dblPaidAmount = 0
            intDays = 0
            strSql = ""
            strSql += "USP_calculation_fetchcharges_Domestic_Import '" & Trim(ddlTariff.SelectedItem.Text & "") & "','" & strAccountID & "','" & Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyyMMdd") & "','" & ContainerNo & "','" & TruckNo & "'," & LoadedID & ",'" & Trim(ddlCriteria.SelectedItem.Text) & "'"
            strSql += ",'" & Trim(ddlLorD.SelectedValue) & "'"
            ds1 = db.sub_GetDataSets(strSql)

            If ds1.Tables(3).Rows.Count > 0 Then
                'dblPaidAmount = dblPaidAmount + Val(ds1.Tables(2).Rows(0)(0))
                If strAccountID = 9 And Val(ds1.Tables(3).Rows(0)(0)) > 0 Then
                    Exit Sub
                End If
            End If
            If ds1.Tables(0).Rows.Count > 0 Then
                intDays = DateDiff("d", InDate, validDate) + 1
                'intDays = DateDiff("d", InDate, "2019-05-27") + 1

                'intMonth = DateDiff("m", InDate, validDate)
                If intDays Mod 30 = 0 Then
                    intMonth = intDays / 30
                Else
                    intMonth = Int((intDays / 30)) + 1
                End If
                If intDays Mod 7 = 0 Then
                    Intweeks = intDays / 7
                Else
                    Intweeks = Int((intDays / 7)) + 1
                End If
                Dim dtFetchCharges As New DataTable
                strSql = ""
                strSql += "select * from Temp_Assessment_Domestic where UserId=" & Session("UserId_DomCFS") & " and accountid='" & strAccountID & "' and ContainerNo='" & Trim(ContainerNo & "") & "'"
                dtFetchCharges = db.sub_GetDatatable(strSql)
                For i = 0 To ds1.Tables(0).Rows.Count - 1
                    If Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "S" Then
                        If dtFetchCharges.Rows.Count > 0 Then
                            Exit Sub
                        End If
                        dblNetAmount = dblNetAmount + slab_CalcAmount(ds1.Tables(0).Rows(i)("SlabID"), intDays, 0, Val(dblweight), Convert.ToDateTime(InDate).ToString("dd-MMM-yyyy HH:mm"), strAccountID, ds1.Tables(0).Rows(i)("Size"), Size, validDate)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "F" And ds1.Tables(1).Rows(i)("ConsiderArea") = False Then
                        If dtFetchCharges.Rows.Count > 0 Then
                            Exit Sub
                        End If
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt"))
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "20" And Size = 20 Then
                        If dtFetchCharges.Rows.Count > 0 Then
                            Exit Sub
                        End If
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt"))
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "40" And Size = 40 Then
                        If dtFetchCharges.Rows.Count > 0 Then
                            Exit Sub
                        End If
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt"))
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "C" And ds1.Tables(0).Rows(i)("size") = "45" And Size = 45 Then
                        If dtFetchCharges.Rows.Count > 0 Then
                            Exit Sub
                        End If
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt"))
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "Q" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(dblPackages)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "O" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(dblweight)
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "D" Then
                        dblNetAmount = dblNetAmount + Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(Val(dblweight) / 1000) * intDays
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "F" And ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                        dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(dblArea) * Intweeks
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "M" And ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                        dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(dblArea) * intMonth
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "A" And ds1.Tables(1).Rows(i)("ConsiderArea") = True Then
                        dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) * Val(dblArea) * intDays
                    ElseIf Trim(ds1.Tables(0).Rows(i)("SorF") & "") = "P" Then
                        If dtFetchCharges.Rows.Count > 0 Then
                            Exit Sub
                        End If
                        dblNetAmount += Val(ds1.Tables(0).Rows(i)("FixedAmt")) + slab_CalcAmount(ds1.Tables(0).Rows(i)("SlabID"), intDays, 0, Val(dblweight), Convert.ToDateTime(InDate).ToString("dd-MMM-yyyy HH:mm"), strAccountID, ds1.Tables(0).Rows(i)("Size"), Size, validDate)
                    End If
                Next
                If ds1.Tables(2).Rows.Count > 0 Then
                    dblPaidAmount = dblPaidAmount + Val(ds1.Tables(2).Rows(0)(0))
                End If
                dblNetAmount = dblNetAmount - dblPaidAmount
                If dblNetAmount < 0 Then
                    dblNetAmount = 0
                End If
                dblNetAmount = Format(dblNetAmount, "0.00")
                If ds1.Tables(0).Rows(0)("IsSTax") = True Then
                    dblSTaxOnAmount += dblNetAmount
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Function slab_CalcAmount(slabID As Integer, DaysValue As Double, percentage As Double, Weight As Double, InDate As Date, AccountID As String, size As Integer, ContainerSize As Integer, ValidDate As Date) As Double
        Try
            Dim dblSlabAmount As Double = 0, dblWeekValue As Double = 0, dbldtvaliddate As Double = 0, dblAddSecs As Double = 0, dblHrs As Double = 0, dblActualHrs As Double = 0, dblAmt As Double = 0, dblTotDays As Double = 0, dblLeftHrs As Double = 0
            Dim dtValidDate As Date, dblclcamt As Double = 0
            Dim intValidCounter As Integer = 0

            If DaysValue Mod 7 = 0 Then
                dblWeekValue = DaysValue / 7
            Else
                dblWeekValue = Int(DaysValue / 7) + 1
            End If
            dblHrs = DateDiff("s", Trim(Convert.ToDateTime(InDate).ToString("dd-MMM-yyyy HH:mm") & ""), Trim(Convert.ToDateTime(ValidDate).ToString("dd-MMM-yyyy HH:mm") & ""))
            dblActualHrs = DateDiff("s", Trim(Convert.ToDateTime(InDate).ToString("dd-MMM-yyyy HH:mm") & ""), Trim(Convert.ToDateTime(ValidDate).ToString("dd-MMM-yyyy HH:mm") & "")) 'DateDiff("s", Format(InDate, "dd-MMM-yyyy HH:mm"), Format(txtvaldate.Text, "dd-MMM-yyyy HH:mm"))
            dblHrs = (dblHrs / 60) / 60
            dblActualHrs = (dblActualHrs / 60) / 60

            dblTotDays = Int(dblActualHrs / 24)
            If dblHrs > 24 Then
                dblLeftHrs = dblActualHrs - (dblHrs * 24)
            Else
                dblLeftHrs = dblActualHrs
            End If
            strSql = ""
            strSql += " USP_cal_STORAGE_SLABS_Domestic " & slabID & "," & percentage & "," & dblWeekValue & "," & Weight & "," & dblTotDays & "," & dblLeftHrs & ""
            ds3 = db.sub_GetDataSets(strSql)
            If ds3.Tables(0).Rows.Count > 0 Then
                If Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Days" Then
                    If size = ContainerSize Then
                        For i = 0 To ds3.Tables(0).Rows.Count - 1
                            If Trim(ds3.Tables(0).Rows(i)("FromSlab")) <= DaysValue Then
                                If Trim(ds3.Tables(0).Rows(i)("ToSlab")) >= DaysValue Then
                                    dblSlabAmount = dblSlabAmount + (Val(DaysValue) - Val(ds3.Tables(0).Rows(i)("FromSlab")) + 1) * Val(ds3.Tables(0).Rows(i)("Value"))
                                    'Else
                                    '    dblSlabAmount = dblSlabAmount + (Val(DaysValue) - Val(ds3.Tables(0).Rows(i)("FromSlab")) + 1) * Val(ds3.Tables(0).Rows(i)("Value"))
                                End If
                            End If
                        Next
                    End If                    
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount
                ElseIf Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Weeks" Then
                    'If ds3.Tables(2).Rows.Count > 0 Then
                    '    If size = ContainerSize Then
                    '        dblSlabAmount = dblWeekValue * Val(ds3.Tables(2).Rows(0)("Value"))
                    '        'ElseIf size = 40 Then
                    '        '    dblSlabAmount = dblWeekValue * Val(ds3.Tables(2).Rows(0)("Value"))
                    '    End If
                    'End If
                    If size = ContainerSize Then
                        For i = 0 To ds3.Tables(0).Rows.Count - 1
                            If Trim(ds3.Tables(0).Rows(i)("FromSlab")) <= dblWeekValue Then
                                If Trim(ds3.Tables(0).Rows(i)("ToSlab")) >= dblWeekValue Then
                                    dblSlabAmount = dblSlabAmount + (Val(dblWeekValue) - Val(ds3.Tables(0).Rows(i)("FromSlab")) + 1) * Val(ds3.Tables(0).Rows(i)("Value"))
                                    'Else
                                    '    dblSlabAmount = dblSlabAmount + (Val(DaysValue) - Val(ds3.Tables(0).Rows(i)("FromSlab")) + 1) * Val(ds3.Tables(0).Rows(i)("Value"))
                                End If
                            End If
                        Next
                    End If                    
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount

                ElseIf Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Percentage" Then

                    If ds3.Tables(1).Rows.Count > 0 Then
                        dblSlabAmount = Val(ds3.Tables(1).Rows(0)("Value"))
                    End If
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount
                ElseIf Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Weight" Then
                    Dim Charges As Integer = 0
                    Dim strImpSlab As String = ""
                    Dim dtImpSlab As New DataTable
                    Dim dtslabid As New DataTable
                    'strSql = ""
                    'strSql += "select * from Temp_Assessment where accountid in (select * from bond_tariffdetails where ChargesBased in (11,12) and bondtype='Noc' and tariffID='" & Trim(ddltraiff.SelectedItem.Text & "") & "')"
                    'dt = db.sub_GetDatatable(strSql)
                    'If dblNetAmount = 0 Then
                    '    If (txt20.Text <> "0" And txt40.Text = "0") Or (txt20.Text = "0" And txt40.Text <> "0") Then
                    '        Charges = 11
                    '        If txt20.Text <> "0" Then
                    '            strSql = ""
                    '            strSql += "select slabID from Domestic_tariffdetails where accountID='" & AccountID & "' and chargesbased=" & Val(Charges) & " and Size=20 and bondtype='Noc'"
                    '            dtslabid = db.sub_GetDatatable(strSql)
                    '            If dtslabid.Rows.Count > 0 Then
                    '                slabID = Val(dtslabid.Rows(0)("slabID"))
                    '            End If
                    '            strImpSlab = ""
                    '            strImpSlab += "SELECT * FROM Domestic_slabs WHERE SlabID=" & slabID & " and " & Weight & " BETWEEN FromSlab and ToSlab ORDER BY FromSlab"
                    '            dtImpSlab = db.sub_GetDatatable(strImpSlab)
                    '            If dtImpSlab.Rows.Count > 0 Then
                    '                dblSlabAmount = Val(dtImpSlab.Rows(0)("Value"))
                    '            End If
                    '        ElseIf txt40.Text <> "0" Then
                    '            strSql = ""
                    '            strSql += "select slabID from Domestic_tariffdetails where accountID='" & AccountID & "' and chargesbased=" & Val(Charges) & " and Size=40 and bondtype='Noc'"
                    '            dtslabid = db.sub_GetDatatable(strSql)
                    '            If dtslabid.Rows.Count > 0 Then
                    '                slabID = Val(dtslabid.Rows(0)("slabID"))
                    '            End If
                    '            strImpSlab = ""
                    '            strImpSlab += "SELECT * FROM Domestic_slabs WHERE SlabID=" & slabID & " and " & Weight & " BETWEEN FromSlab and ToSlab ORDER BY FromSlab"
                    '            dtImpSlab = db.sub_GetDatatable(strImpSlab)
                    '            If dtImpSlab.Rows.Count > 0 Then
                    '                dblSlabAmount = Val(dtImpSlab.Rows(0)("Value"))
                    '            End If
                    '        End If

                    '    ElseIf (txt20.Text <> "0" And txt40.Text <> "0") Then
                    '        Charges = 12
                    '        strSql = ""
                    '        strSql += "select slabID from Domestic_tariffdetails where accountID='" & AccountID & "' and chargesbased=" & Val(Charges) & " and Size=0 and bondtype='Noc'"
                    '        dtslabid = db.sub_GetDatatable(strSql)
                    '        If dtslabid.Rows.Count > 0 Then
                    '            slabID = Val(dtslabid.Rows(0)("slabID"))
                    '        End If
                    '        strImpSlab = ""
                    '        strImpSlab += "SELECT * FROM Domestic_slabs WHERE SlabID=" & slabID & " and " & Weight & " BETWEEN FromSlab and ToSlab ORDER BY FromSlab"
                    '        dtImpSlab = db.sub_GetDatatable(strImpSlab)
                    '        If dtImpSlab.Rows.Count > 0 Then
                    '            dblSlabAmount = Val(dtImpSlab.Rows(0)("Value"))
                    '        End If
                    '    End If
                    'End If

                    If ds3.Tables(3).Rows.Count > 0 Then
                        dblSlabAmount = Math.Round(Val(ds3.Tables(3).Rows(0)("Value")) * (Val(Weight) / 1000))
                    End If
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount
                ElseIf Trim(ds3.Tables(0).Rows(0)("slabON") & "") = "Hours" Then
                    dtValidDate = Trim(Convert.ToDateTime(InDate).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(InDate, "dd-MMM-yyyy HH:mm")
                    lblValidDate.Text = Trim(Convert.ToDateTime(DateAdd("s", 359 * 60, Now)).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(DateAdd("s", 359 * 60, Now), "dd-MMM-yyyy HH:mm")
                    dbldtvaliddate = Trim(Convert.ToDateTime(dtValidDate).ToString("yyyyMMddHHmm") & "") 'Format(dtValidDate, "yyyyMMddHHmm")
                    While dbldtvaliddate <= Trim(Convert.ToDateTime(Now).ToString("yyyyMMddHHmm") & "") 'Format(Now, "yyyyMMddHHmm")
                        If intValidCounter = 0 Then
                            dblAddSecs = Val(360) * Val(60)
                            dtValidDate = Trim(Convert.ToDateTime(DateAdd("s", dblAddSecs, dtValidDate)).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(DateAdd("s", dblAddSecs, dtValidDate), "dd-MMM-yyyy HH:mm")
                            intValidCounter = intValidCounter + 1
                        ElseIf intValidCounter = 1 Then
                            dblAddSecs = Val(360) * Val(60)
                            dtValidDate = Trim(Convert.ToDateTime(DateAdd("s", dblAddSecs, dtValidDate)).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(DateAdd("s", dblAddSecs, dtValidDate), "dd-MMM-yyyy HH:mm")
                            intValidCounter = intValidCounter + 1
                        ElseIf intValidCounter = 2 Then
                            dblAddSecs = Val(720) * Val(60)
                            dtValidDate = Trim(Convert.ToDateTime(DateAdd("s", dblAddSecs, dtValidDate)).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(DateAdd("s", dblAddSecs, dtValidDate), "dd-MMM-yyyy HH:mm")
                            intValidCounter = 0
                        End If
                        dbldtvaliddate = Trim(Convert.ToDateTime(dtValidDate).ToString("yyyyMMddHHmm") & "") 'Format(dtValidDate, "yyyyMMddHHmm")
                        dbldtvaliddate += 1
                    End While
                    dtValidDate = Trim(Convert.ToDateTime(DateAdd("s", -60, dtValidDate)).ToString("dd-MMM-yyyy HH:mm") & "") 'Format(DateAdd("s", -60, dtValidDate), "dd-MMM-yyyy HH:mm")

                    If ds3.Tables(5).Rows.Count > 0 Then
                        dblSlabAmount = Val(ds3.Tables(5).Rows(0)("Value")) * dblTotDays
                    End If
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount

                    If ds3.Tables(6).Rows.Count > 0 Then
                        dblSlabAmount = Val(ds3.Tables(6).Rows(0)("Value"))
                    End If
                    slab_CalcAmount = slab_CalcAmount + dblSlabAmount
                End If

            End If
            Return slab_CalcAmount
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Function
    Protected Sub ddlCustomer_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = "select top 1 EntryID from Domestic_tariffmaster where CustID=" & Val(ddlCustomer.SelectedValue) & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddlTariff.SelectedValue = Val(dt.Rows(0)("EntryID"))
            Else
                ddlTariff.SelectedValue = 0
            End If
            ddlCustomer.Focus()
            UpdatePanel2.Update()
            UpdatePanel4.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Sub_SGTRate()
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
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                dblSGST = Val(dt.Rows(0)("SGST"))
                dblCGST = Val(dt.Rows(0)("CGST"))
                dblIGST = Val(dt.Rows(0)("IGST"))
                dbltaxgroupid = Trim(dt.Rows(0)("settingsID") & "")
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

    Protected Sub saveQuoOK_ServerClick(sender As Object, e As EventArgs)
        Try
            lblquoteApprove.Text = "Do you wish to print Invoice?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel5.Update()
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

    Protected Sub txtRefNo_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "usp_invoice_rr_No '" & Trim(txtRefNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            chkright_CheckedChanged(sender, e)
            ddlTariff.Focus()
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
