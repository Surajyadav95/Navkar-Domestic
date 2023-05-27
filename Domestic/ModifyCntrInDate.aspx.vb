Imports System.Data
Imports System.IO
Partial Class Account_ItemList
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim WHID, WHIDView As String
    Dim ed As New clsEncodeDecode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            txtContainerno.Focus()
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function  
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
           
            strSQL = ""
            strSql = "select ContainerNo ,JoNo  from Domestic_IN where ContainerNo ='" & txtContainerno.Text & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblEntryID.Text = dt.Rows(0)("JoNo")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid Container Number');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()
                lblEntryID.Text = ""
                Exit Sub
            End If

            strSql = ""
            strSql = "usp_Demestic_Modify_OutDate'" & Trim(txtContainerno.Text & "") & "','" & Trim(lblEntryID.Text & "") & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                txtoutdate.Text = Convert.ToDateTime(Trim(dt1.Rows(0)("InDate_Time"))).ToString("yyyy-MM-ddTHH:mm")
                txtLoadedPkgs.Text = Trim(dt1.Rows(0)("PKGS") & "")
                txtLoadedWeight.Text = Trim(dt1.Rows(0)("WEIGHT") & "")
                txtSeal.Text = Trim(dt1.Rows(0)("SealNo") & "")
                txtRemarks.Text = Trim(dt1.Rows(0)("Remarks") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not found');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()

                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Control_Clear(sender As Object, e As EventArgs)
        Try
            txtContainerno.Text = ""
            txtLoadedPkgs.Text = ""
            txtSeal.Text = ""
            txtLoadedWeight.Text = ""
           
            lblEntryID.Text = ""
            txtRemarks.Text = ""

          
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)
        Try
            If Trim(txtContainerno.Text) = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No cannot be left blank');", True)
                txtContainerno.Focus()
                Exit Sub
            End If
            lblquoteApprove.Text = "Are you sure to update ?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btncancelyes_ServerClick(sender As Object, e As EventArgs)
        strSql = ""
        strSql = " usp_Domestic_Gate_Date_Update '" & Trim(txtContainerno.Text) & "','" & Trim(lblEntryID.Text) & "','" & Convert.ToDateTime(Trim(txtoutdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "' ,"
        strSql += " '" & Trim(txtLoadedPkgs.Text) & "','" & Trim(txtLoadedWeight.Text) & "','" & Trim(txtSeal.Text) & "','" & Trim(txtRemarks.Text & "") & "'"
        dt3 = db.sub_GetDatatable(strSql)
        Call db.AmmendmentLog(" " & Trim(txtContainerno.Text) & " modify In details'" & Trim(lblEntryID.Text) & "'in Domestic_IN ", Session("UserId_DomCFS"))
        lblSession.Text = "record updated successfully"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mymodalforupdate", "$('#mymodalforupdate').modal();", True)
        Control_Clear(sender, e)
    End Sub
End Class
