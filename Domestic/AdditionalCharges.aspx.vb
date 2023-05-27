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
        'If Session("UserId_DomCFS") Is Nothing Then
        '    Session("UserId_DomCFS") = Request.Cookies("UserIDPRE_Bond").Value
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
            txtwodate.Text = Convert.ToDateTime(Now).ToString("dd-MM-yyyy")

            Filldropdown()
            txtContainerNo.Focus()
        End If

    End Sub
    Protected Sub Filldropdown()
        Try
            
            ds = db.sub_GetDataSets("USP_fill_details_accountndetails_Domestic")

            ddlacchead.DataSource = ds.Tables(0)
            ddlacchead.DataTextField = "AccountName"
            ddlacchead.DataValueField = "AccountID"
            ddlacchead.DataBind()
            ddlacchead.Items.Insert(0, New ListItem("--Select--", 0))

            grdchargesother.DataSource = ds.Tables(1)
            grdchargesother.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_validation_additional_save_charges_Domestic '" & Trim(txtContainerNo.Text) & "','" & ddlacchead.SelectedValue & "'"
            ds = db.sub_GetDataSets(strSql)
            'If ds.Tables(1).Rows.Count > 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('An entry for this record already exists. Cannot save.!');", True)
            '    Exit Sub
            'End If
            If ds.Tables(0).Rows.Count > 0 Then
                If Val(ds.Tables(0).Rows(0)(0)) = 0 Then
                    txtwono.Text = 1
                Else
                    txtwono.Text = Val(ds.Tables(0).Rows(0)(0)) + 1
                End If
            End If

            strSql = ""
            strSql += "USP_insert_into_Domestic_wocharges '" & Trim(txtwono.Text) & "','" & Convert.ToDateTime(txtwodate.Text).ToString("yyyy-MM-dd") & "',"
            strSql += "'" & Trim(txtContainerNo.Text) & "','" & Trim(ddlacchead.SelectedValue) & "','" & Val(txtamtcollect.Text) & "',"
            strSql += "'" & Trim(txtnarration.Text) & "','" & chkisActive.Checked & "','" & Session("UserId_DomCFS") & "','" & Trim(lblLoadedID.Text & "") & "','" & Trim(txtQty.Text & "") & "','" & Trim(txtWeight.Text & "") & "','" & Trim(txtRate.Text & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            Clear()
            txtContainerNo.Text = ""
            lblSession.Text = "Record saved successfully!"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim WONO As String = lnkcancel.CommandArgument
            strSql = ""
            strSql += "UPDATE Domestic_WOCharges  SET IsCancel=1, CancelledBy=" & Session("UserId_DomCFS") & ", CancelledOn=getdate()"
            strSql += " WHERE WONo=" & WONO & ""
            db.sub_ExecuteNonQuery(strSql)
            lblSession.Text = "Record cancelled successfully!"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtnocno_TextChanged(sender As Object, e As EventArgs)
        Try
            Clear()
            strSql = ""
            strSql += "select top 1 * from Domestic_Loading where d_container_no='" & Trim(txtContainerNo.Text) & "' and iscancel=0 order by Loaded_Date desc"
            dt = db.sub_GetDatatable(strSql)
            If Not dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No not found!');", True)
                txtContainerNo.Text = ""
                txtContainerNo.Focus()
                Exit Sub
            Else
                lblLoadedID.Text = Val(dt.Rows(0)("Loaded_ID"))
            End If
            ddlacchead.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            txtwono.Text = ""
            txtwodate.Text = Convert.ToDateTime(Now).ToString("dd-MM-yyyy")            
            ddlacchead.SelectedValue = 0
            txtamtcollect.Text = ""
            chkisActive.Checked = True
            txtnarration.Text = ""
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtWeight_TextChanged(sender As Object, e As EventArgs)
        Try
            txtamtcollect.Text = Trim(txtWeight.Text & "") * Trim(txtRate.Text & "")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtQty_TextChanged(sender As Object, e As EventArgs)
        Try
            txtamtcollect.Text = Trim(txtRate.Text & "") * Trim(txtQty.Text & "")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
