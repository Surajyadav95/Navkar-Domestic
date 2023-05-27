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
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Filldropdown()
            btnSave_Click(sender, e)
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

            dt = db.sub_GetDatatable(" exec get_sp_table 'Domestic_tariffmaster','entryID,tariffID','','entryID'")
            If (dt.Rows.Count > 0) Then
                ddltraiff.DataSource = dt
                ddltraiff.DataTextField = "tariffID"
                ddltraiff.DataValueField = "entryID"
                ddltraiff.DataBind()
                ddltraiff.Items.Insert(0, New ListItem("--Select--", 0))
            End If

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
            strSql += " usp_cancel_tariff_Domestic '" & Trim(ddltraiff.SelectedItem.Text & "") & "','" & Trim(ddlbondType.SelectedValue & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
    End Sub
    Protected Sub btncancel_Click(sender As Object, e As EventArgs)
        Try
            For Each row As GridViewRow In grdcontainer.Rows
                Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                strSql = ""
                strSql += "USP_insert_cancel_update_Domestic " & Trim(chkright.Checked & "") & ",'" & Trim(CType(row.FindControl("lblentryid"), Label).Text) & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next
            btnSave_Click(sender, e)
            lblSession.Text = "Record updated successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel5.Update()
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
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
