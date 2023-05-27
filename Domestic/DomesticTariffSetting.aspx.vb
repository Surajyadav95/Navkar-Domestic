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
            db.sub_ExecuteNonQuery("Delete from Temp_Charges_Domestic Where UniqueID=" & Session("UserId_DomCFS") & "")
            Filldropdown()
            Add(sender, e)
            If Not (Request.QueryString("TariffIDView") = "") Then
                TariffID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("TariffIDView")))
                strSql = ""
                strSql = "use_view_cancel '" & TariffID & "','" & Session("UserId_DomCFS") & "'"
                dt = db.sub_GetDatatable(strSql)

                If (dt.Rows.Count > 0) Then
                    ddltraiff.SelectedItem.Text = Trim(dt.Rows(0)("tariffID") & "")
                    ddlbond.SelectedItem.Text = Trim(dt.Rows(0)("bondtype") & "")
                    txtfrom.Text = Trim(dt.Rows(0)("effectivefrom") & "")
                    txtUpTo.Text = Trim(dt.Rows(0)("effectiveupto") & "")
                    ddlAccount.SelectedItem.Text = Trim(dt.Rows(0)("accountID") & "")
                    ddlsize.SelectedItem.Text = Trim(dt.Rows(0)("Size") & "")
                    ddlCharges.SelectedItem.Text = Trim(dt.Rows(0)("SorF") & "")
                    txtfixed.Text = Trim(dt.Rows(0)("fixedamt") & "")

                    'Dim IsTax As String
                    'If dt.Rows(0)("IsSTax") = True Then
                    '    IsTax = True
                    'Else
                    '    IsTax = False
                    'End If

                    'Dim OnDelivereed As String
                    'If dt.Rows(0)("ConsiderArea") = True Then
                    '    OnDelivereed = True
                    'Else
                    '    OnDelivereed = False
                    'End If
                End If
                Panel2.Enabled = False
                btnSave.Visible = False
                btnclear.Visible = False
            End If
        End If

    End Sub
    Public Sub grid()
        strSql = ""
        strSql += ""
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("exec get_sp_table 'Domestic_tariffmaster' ,'TariffID,EntryID','','TariffID'")
            If (dt.Rows.Count > 0) Then
                ddltraiff.DataSource = dt
                ddltraiff.DataTextField = "TariffID"
                ddltraiff.DataValueField = "EntryID"
                ddltraiff.DataBind()
                ddltraiff.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            dt = db.sub_GetDatatable("exec get_sp_table 'Domestic_accountmaster','AccountName,AccountID','','AccountID'")
            If (dt.Rows.Count > 0) Then
                ddlAccount.DataSource = dt
                ddlAccount.DataTextField = "AccountName"
                ddlAccount.DataValueField = "AccountID"
                ddlAccount.DataBind()
                ddlAccount.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            dt = db.sub_GetDatatable("exec get_sp_table 'Domestic_Chargesmaster','ChargesID,ChargesBased','','ChargesID'")
            If (dt.Rows.Count > 0) Then
                ddlCharges.DataSource = dt
                ddlCharges.DataTextField = "ChargesBased"
                ddlCharges.DataValueField = "ChargesID"
                ddlCharges.DataBind()
                ddlCharges.Items.Insert(0, New ListItem("--Select--", 0))
            End If
            strSql = ""
            strSql += "USP_LOCATION_CUSTOMER_WISE '" & Trim(ddltraiff.SelectedItem.Text & "") & "','Domestic'"
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
    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            For Each row As GridViewRow In grdcontainer.Rows
                strSql = ""
                strSql += "usp_insert_into_Domestic_tariff"
                strSql += "'" & Trim(CType(row.FindControl("lbltariffID"), Label).Text) & "','" & Trim(CType(row.FindControl("lblInvoicetype"), Label).Text) & "','" & Convert.ToDateTime(Trim(CType(row.FindControl("lbleffectivefrom"), Label).Text)).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(Trim(CType(row.FindControl("lbleffectiveupto"), Label).Text)).ToString("yyyy-MM-dd") & "',"
                strSql += "'" & Replace(Trim(CType(row.FindControl("lblAccountHead"), Label).Text), "'", "''") & "','" & Trim(CType(row.FindControl("lblsize"), Label).Text) & "','" & Trim(CType(row.FindControl("lblchargesBased"), Label).Text) & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblFixedAmount"), Label).Text) & "','" & Trim(CType(row.FindControl("lblIsTax"), Label).Text) & "','" & Trim(CType(row.FindControl("lblOnDelivered"), Label).Text) & "'"
                strSql += ",'" & Trim(CType(row.FindControl("lblconsarea"), Label).Text) & "','" & Trim(CType(row.FindControl("lblLocationID"), Label).Text) & "','" & Trim(CType(row.FindControl("lblSlabID"), Label).Text) & "'"
                strSql += ",'" & Trim(CType(row.FindControl("lblCriteria"), Label).Text) & "','" & Trim(CType(row.FindControl("lblLorD1"), Label).Text) & "'"
                dt = db.sub_GetDatatable(strSql)                
            Next
            Button1.Text = "Save"
            Button1.Attributes.Add("Class", "btn btn-primary")
            lblSession.Text = "Record saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()

        Catch ex As Exception
            Button1.Text = "Save"
            Button1.Attributes.Add("Class", "btn btn-primary")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddltraiff_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "exec get_sp_table 'Domestic_tariffmaster','TariffID, Convert(VARCHAR(50),Convert(Datetime, EffectiveFrom),105) as Efrom,Convert(VARCHAR(50),Convert(Datetime, EffectiveUpto),105) as Upto,TariffDescription ','EntryID=''" & Trim(ddltraiff.SelectedValue & "") & "'' ','TariffID'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txtfrom.Text = Trim(dt.Rows(0)("Efrom") & "")
                txtUpTo.Text = Trim(dt.Rows(0)("Upto") & "")
                txtTariffDesc.Text = Trim(dt.Rows(0)("TariffDescription") & "")
            End If
            ddlLocation.DataSource = Nothing
            ddlLocation.DataBind()
            strSql = ""
            strSql += "USP_LOCATION_CUSTOMER_WISE '" & Trim(ddltraiff.SelectedItem.Text & "") & "','Domestic'"
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
    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Dim IsTax As String
            If (chkIstax.Checked = True) Then
                IsTax = "Yes"
            Else
                IsTax = "No"
            End If
            Dim OnDelivereed As String
            If (ChkOnDelivered.Checked = True) Then
                OnDelivereed = "Yes"
            Else
                OnDelivereed = "No"
            End If

            strSql = ""
            strSql += "use_temp_traiff_setting_domestic "
            strSql += "'" & Trim(ddltraiff.SelectedItem.Text & "") & "','" & Trim(ddlbond.SelectedItem.Text) & "','" & Trim(txtfrom.Text & "") & "','" & Trim(txtUpTo.Text & "") & "',"
            strSql += "'" & Replace(Trim(ddlAccount.SelectedItem.Text), "'", "''") & "','" & Trim(ddlsize.SelectedValue & "") & "','" & Trim(ddlCharges.SelectedItem.Text & "") & "',"
            strSql += "'" & Trim(txtfixed.Text & "") & " ','" & IsTax & "','" & OnDelivereed & "','" & Session("UserId_DomCFS") & "','" & chkconsarea.Checked & "','" & ddlLocation.SelectedValue & "'," & Val(ddlSlab.SelectedValue) & ""
            strSql += ",'" & Trim(ddlCriteria.SelectedItem.Text) & "','" & Trim(ddlLorD.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            Add(sender, e)
            'ddltraiff.SelectedValue = "0"
            'txtfrom.Text = ""
            'txtUpTo.Text = ""
            
            Button1.Text = "Add"
            Button1.Attributes.Add("Class", "btn btn-info")
            Label2.Text = "Do you want to add more?"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel4.Update()
        Catch ex As Exception
            Button1.Text = "Add"
            Button1.Attributes.Add("Class", "btn btn-info")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Add(sender As Object, e As EventArgs)
        Try
            strSql = "USP_FETCH_DATA_FROM_Temp_Charges_Domestic '" & Session("UserId_DomCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkRemove.CommandArgument
            dt = db.sub_GetDatatable("USP_Delete_temp_Domestic_traiff '" & AutoID & "','" & Session("UserId_DomCFS") & "'")
            Add(sender, e)
            If (dt.Rows.Count > 0) Then
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlCharges_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If ddlCharges.SelectedValue = 2 Or ddlCharges.SelectedValue = 13 Or ddlCharges.SelectedValue = 14 Then
                chkconsarea.Checked = True
                divFixedAmount.Attributes.Add("style", "display:block")
                divLocation.Attributes.Add("style", "display:none")
                divSlab.Attributes.Add("style", "display:none")
                divSlabAdd.Attributes.Add("style", "display:none")
            ElseIf ddlCharges.SelectedValue = 9 Then
                divLocation.Attributes.Add("style", "display:block")
                divFixedAmount.Attributes.Add("style", "display:block")
                divSlab.Attributes.Add("style", "display:none")
                divSlabAdd.Attributes.Add("style", "display:none")
                chkconsarea.Checked = False
            ElseIf ddlCharges.SelectedValue = 11 Or ddlCharges.SelectedValue = 12 Or ddlCharges.SelectedValue = 7 Then
                strSql = ""
                strSql += "select distinct slabID FROM Domestic_slabs order by slabID "
                dt = db.sub_GetDatatable(strSql)
                ddlSlab.DataSource = dt
                ddlSlab.DataTextField = "slabID"
                ddlSlab.DataValueField = "slabID"
                ddlSlab.DataBind()
                ddlSlab.Items.Insert(0, New ListItem("--Select--", 0))
                divFixedAmount.Attributes.Add("style", "display:none")
                divLocation.Attributes.Add("style", "display:none")
                divSlab.Attributes.Add("style", "display:block")
                divSlabAdd.Attributes.Add("style", "display:block")
                chkconsarea.Checked = False
            ElseIf ddlCharges.SelectedValue = 15 Then
                strSql = ""
                strSql += "select distinct slabID FROM Domestic_slabs order by slabID "
                dt = db.sub_GetDatatable(strSql)
                ddlSlab.DataSource = dt
                ddlSlab.DataTextField = "slabID"
                ddlSlab.DataValueField = "slabID"
                ddlSlab.DataBind()
                ddlSlab.Items.Insert(0, New ListItem("--Select--", 0))
                divFixedAmount.Attributes.Add("style", "display:block")
                divLocation.Attributes.Add("style", "display:none")
                divSlab.Attributes.Add("style", "display:block")
                divSlabAdd.Attributes.Add("style", "display:block")
                chkconsarea.Checked = False
            Else
                divLocation.Attributes.Add("style", "display:none")
                divFixedAmount.Attributes.Add("style", "display:block")
                divSlab.Attributes.Add("style", "display:none")
                divSlabAdd.Attributes.Add("style", "display:none")
                chkconsarea.Checked = False
            End If
            txtfixed.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnNOCList_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from TEMP_TARIFF_SEARCH_Domestic where userid='" & Session("UserId_DomCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddltraiff.SelectedValue = Val(dt.Rows(0)("EntryID"))
                ddltraiff_SelectedIndexChanged(sender, e)
            End If
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnYes_ServerClick(sender As Object, e As EventArgs)
        Try
            ddlAccount.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnNo_ServerClick(sender As Object, e As EventArgs)
        Try
            ddlAccount.SelectedValue = "0"
            ddlsize.SelectedValue = "0"
            ddlCharges.SelectedValue = "0"
            txtfixed.Text = ""
            'ddlbond.SelectedValue = "0"
            chkIstax.Checked = True
            ChkOnDelivered.Checked = False
            chkconsarea.Checked = False
            ddlCharges_SelectedIndexChanged(sender, e)
            divSlabDets.Attributes.Add("style", "display:none")
            UpdatePanel1.Update()
            btnSave.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlSlab_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If ddlSlab.SelectedValue = 0 Then
                divSlabDets.Attributes.Add("style", "display:none")
                UpdatePanel2.Update()
            Else
                divSlabDets.Attributes.Add("style", "display:block")
                strSql = ""
                strSql += "select * FROM Domestic_slabs WHERE SlabID=" & ddlSlab.SelectedValue & " order by FromSlab"
                dt = db.sub_GetDatatable(strSql)
                grdSlabDets.DataSource = dt
                grdSlabDets.DataBind()
                UpdatePanel2.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSlab_Click(sender As Object, e As EventArgs)
        Try
            ddlCharges_SelectedIndexChanged(sender, e)
            ddlSlab.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try        
    End Sub
End Class
