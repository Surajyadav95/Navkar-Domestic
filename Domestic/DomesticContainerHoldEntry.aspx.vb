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
            txtHoldDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            btnsearch_Click(sender, e)
            Filldropdown()
            'grid()
        End If
    End Sub
    Protected Sub grid()
        
        strSql = ""
        strSql = "USP_Domestic_HoldContainer'"
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub

    Public Sub Filldropdown()
        ds = db.sub_GetDataSets("USP_Domestic_Holdreason")
        If (ds.Tables(0).Rows.Count > 0) Then
            ddlholdreason.DataSource = ds.Tables(0)
            ddlholdreason.DataTextField = "HoldReason"
            ddlholdreason.DataValueField = "HoldReasonID"
            ddlholdreason.DataBind()
            ddlholdreason.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        
    End Sub

    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function

    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = " USP_HOLD_SEARCH_CONTAINER'" & Trim(txtcontainer.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtjoNo.Text = Trim(dt.Rows(0)("JONo") & "")
                txtSize.Text = Trim(dt.Rows(0)("Size") & "")
                txtlineName.Text = Trim(dt.Rows(0)("SLName") & "")
                ddlholdreason.SelectedValue = Trim(dt.Rows(0)("HoldReason") & "")
                txtInDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("InDate_Time")) & "").ToString("yyyy-MM-ddTHH:mm")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid Container No.');", True)
                'MsgBox("Invalid Container No")
                txtcontainer.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnputonhold_Click(sender As Object, e As EventArgs)
        Try
            strSql = "usp_container_vaild '" & Trim(txtcontainer.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No is already On Hold.');", True)
                txtcontainer.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql = "Exec USP_Insert_Domestic_HoldsHolds '" & txtjoNo.Text & "','" & txtcontainer.Text & "','" & ddlholdreason.SelectedValue & "','" & txtremarks.Text & "','H','" & Session("UserId_DomCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)
            ClearControls()
            lblSession.Text = "Hold Details Updated Successfully "

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try

            strSql = ""
            strSql = "USP_Domestic_HoldContainer'" & Trim(txtsearche.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnclearfrom_Click(sender As Object, e As EventArgs)

        Try

            strSql = "select top(1) JoNo ,ContainerNo ,HoldStatus,dbo.Fn_GET_NAME(ClearedBy,'User Name')[Cleared By] ,convert(varchar(17),ClearedOn,113) [Cleared On] from Domestic_Holds  where  ContainerNo ='" & Trim(txtcontainer.Text & "") & "' and JoNo ='" & Trim(txtjoNo.Text & "") & "'  order by HoldDate  desc "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                If Trim(dt.Rows(0)("HoldStatus")) = "C" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No is already cleared .');", True)
                    'MsgBox("Container No ='" & Trim(txtContainerNo.Text & "") & "' and JONO='" & Trim(txtjoNo.Text & "") & "' is already cleared on " & Trim(dt.Rows(0)("Cleared On")) & " by  " & Trim(dt.Rows(0)("Cleared By")) & ".", vbInformation)
                    Exit Sub
                End If


            End If
            strSql = "UPDATE Domestic_Holds SET ClearedRemarks='" & Trim(txtremarks.Text) & "', HoldStatus='C', ClearedBy=" & Session("UserId_DomCFS") & ", ClearedOn='" & Format(Now, "dd-MMM-yyyy HH:mm:ss") & "' WHERE JoNo=" & Val(txtjoNo.Text) & " AND "
            strSql += " ContainerNo='" & Trim(txtcontainer.Text) & "' and EntryID=(SELECT MAX(EntryID) FROM Domestic_Holds WHERE JoNo=" & Val(txtjoNo.Text) & " and ContainerNo='" & Trim(txtcontainer.Text) & "'"
            strSql += " AND HoldStatus='H' AND HoldReasonID=" & Val(ddlholdreason.SelectedValue) & ")"
            db.sub_ExecuteNonQuery(strSql)
            ClearControls()
            lblSession.Text = "Hold Cleared Successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub ClearControls()
        txtcontainer.Text = ""
        txtJONo.Text = ""
        ddlholdreason.SelectedValue = 0
        txtRemarks.Text = ""
        txtSize.Text = ""
        txtlineName.Text = ""
 
        txtInDate.Text = ""



    End Sub
End Class
