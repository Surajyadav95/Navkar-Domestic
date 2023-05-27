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
            strSql = "select D_Container_No ,JONo  from Domestic_Stuffing where D_Container_No ='" & txtContainerno.Text & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblEntryID.Text = dt.Rows(0)("JONo")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid Container Number');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()
                lblEntryID.Text = ""
                Exit Sub
            End If

            strSql = ""
            strSql = "usp_Domestic_Stuff_Show'" & Trim(txtContainerno.Text & "") & "','" & Trim(lblEntryID.Text & "") & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                txtStuffdate.Text = Convert.ToDateTime(Trim(dt1.Rows(0)("Stuffed_Date"))).ToString("yyyy-MM-ddTHH:mm")
                txtStuffedPkgs.Text = Trim(dt1.Rows(0)("Stuffed_Qty") & "")
                txtStuffedWeight.Text = Trim(dt1.Rows(0)("Stuffed_Wt") & "")
                txtDCANo.Text = Trim(dt1.Rows(0)("DCA_No") & "")
                'txtRemarks.Text = Trim(dt1.Rows(0)("Remarks") & "")
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
            txtStuffedPkgs.Text = ""
            txtDCANo.Text = ""
            txtStuffedWeight.Text = ""

            lblEntryID.Text = ""
            
          
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
        strSql = " usp_Domestic_Stuffing_Date_Update '" & Trim(txtContainerno.Text) & "','" & Trim(lblEntryID.Text) & "','" & Convert.ToDateTime(Trim(txtStuffdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "' ,"
        strSql += " '" & Trim(txtStuffedPkgs.Text) & "','" & Trim(txtStuffedWeight.Text) & "','" & Trim(txtDCANo.Text) & "'"
        dt3 = db.sub_GetDatatable(strSql)
        ''Call db.AmmendmentLog(" " & Trim(txtContainerno.Text) & " modify Destuff details'" & Trim(lblEntryID.Text) & "'in Domestic_Stuffing ", Session("UserId_DomCFS"))
        lblSession.Text = "record updated successfully"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mymodalforupdate", "$('#mymodalforupdate').modal();", True)
        Control_Clear(sender, e)
    End Sub
End Class
