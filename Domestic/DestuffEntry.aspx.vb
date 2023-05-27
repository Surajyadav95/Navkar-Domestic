Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2 As DataTable
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
            db.sub_ExecuteNonQuery("Delete from Temp_Destuff_Line Where Addedby=" & Session("UserId_DomCFS") & "")
            txtGateInDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Filldropdown()
            btnsearch_Click(sender, e)

            'grid()
        End If
    End Sub
   
     
     
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function

    Protected Sub ddlSearchby_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If (ddlSearchby.SelectedValue = "Container No") Then
                divContainer.Attributes.Add("style", "display:block")
                divDomestic.Attributes.Add("style", "display:block")

            Else
                divContainer.Attributes.Add("style", "display:None")
                divDomestic.Attributes.Add("style", "display:None")
            End If

            If (ddlSearchby.SelectedValue = "IGM-Item No") Then
                divSearch.Attributes.Add("style", "display:block")
                divItem.Attributes.Add("style", "display:block")


            Else
                divSearch.Attributes.Add("style", "display:None")
                divItem.Attributes.Add("style", "display:None")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Filldropdown()
        ds = db.sub_GetDataSets("usp_destuff_fill_Vendor")
        If (ds.Tables(0).Rows.Count > 0) Then
            ddlvendor.DataSource = ds.Tables(0)
            ddlvendor.DataTextField = "Name"
            ddlvendor.DataValueField = "VendorID"
            ddlvendor.DataBind()
            ddlvendor.Items.Insert(0, New ListItem("--Select--", 0))
        End If


    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
          
            strSql = ""
            strSql += "USP_DOMESTIC_DESTUFF_DETS '" & Trim(ddlSearchby.SelectedValue & "") & "','" & Trim(txtContainer.Text & "") & "','" & Trim(txtigmno.Text & "") & "','" & Trim(txtItemNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdinvoices.DataSource = dt
            grdinvoices.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub grdinvoices_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Try
            If (e.Row.RowType = DataControlRowType.DataRow) Then
                Dim ddlEquipment As DropDownList = CType(e.Row.FindControl("ddlEquipment"), DropDownList)

                strSql = ""
                strSql += "SELECT * FROM Equipment_M  "
                ds = db.sub_GetDataSets(strSql)
                ddlEquipment.DataSource = ds.Tables(0)
                ddlEquipment.DataTextField = "Equipment_Name"
                ddlEquipment.DataValueField = "AutoID"
                ddlEquipment.DataBind()
                ddlEquipment.Items.Insert(0, New ListItem("Select", 0))

            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub txtDestuffPkgs_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, TextBox).NamingContainer, GridViewRow)
            If Val(CType(gr.FindControl("txtDestuffPkgs"), TextBox).Text & "") > Val(CType(gr.FindControl("lblPKGS"), Label).Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Destuff PKGS cannot be greater than total PKGS.');", True)
                CType(gr.FindControl("txtDestuffPkgs"), TextBox).Text = ""
                CType(gr.FindControl("txtDestuffPkgs"), TextBox).Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtshortpkgs_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, TextBox).NamingContainer, GridViewRow)
            If Val(CType(gr.FindControl("txtshortpkgs"), TextBox).Text & "") > Val(CType(gr.FindControl("lblPKGS"), Label).Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Short PKGS cannot be greater than total PKGS.');", True)
                CType(gr.FindControl("txtshortpkgs"), TextBox).Text = ""
                CType(gr.FindControl("txtshortpkgs"), TextBox).Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtExcesspkgs_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, TextBox).NamingContainer, GridViewRow)
            If Val(CType(gr.FindControl("txtExcesspkgs"), TextBox).Text & "") > Val(CType(gr.FindControl("lblPKGS"), Label).Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Excess PKGS cannot be greater than total PKGS.');", True)
                CType(gr.FindControl("txtExcesspkgs"), TextBox).Text = ""
                CType(gr.FindControl("txtExcesspkgs"), TextBox).Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_validate()
        Try
           

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            For Each row In grdinvoices.Rows
                If Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") = Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") Then
                    strSql = ""
                    strSql = "usp_validate'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblJoNo"), Label).Text & "") & "'"
                    dt2 = db.sub_GetDatatable(strSql)
                    If dt2.Rows.Count > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No already exists .');", True)
                        Exit Sub
                    End If

                End If
            Next
            For Each row In grdinvoices.Rows
                strSql = ""
                strSql += "USP_INSERT_DOMESTIC_LINE_CTR_DESTUFF'" & Convert.ToDateTime(Trim(txtGateInDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(CType(row.FindControl("lblJoNo"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblSize"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblCargoTypeID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblWeight"), Label).Text & "") & "','" & Trim(CType(row.FindControl("txtWeight"), TextBox).Text & "") & "','" & Trim(CType(row.FindControl("lblPKGS"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("txtDestuffPkgs"), TextBox).Text & "") & "','" & Trim(CType(row.FindControl("txtshortpkgs"), TextBox).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("txtExcesspkgs"), TextBox).Text & "") & "','" & Trim(CType(row.FindControl("ddlEquipment"), DropDownList).Text & "") & "','" & Trim(CType(row.FindControl("txtNoOfLabour"), TextBox).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("txtRemarks"), TextBox).Text & "") & "','" & Session("UserId_DomCFS") & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next

            lblSession.Text = "Record Saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtWeight_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, TextBox).NamingContainer, GridViewRow)
            If Val(CType(gr.FindControl("txtWeight"), TextBox).Text & "") > Val(CType(gr.FindControl("lblWeight"), Label).Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Destuf Weight cannot be greater than total weight.');", True)
                CType(gr.FindControl("lblWeight"), TextBox).Text = ""
                CType(gr.FindControl("lblWeight"), TextBox).Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnDomestic_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select ContainerNo from Temp_JO_List_Domestic where userid=" & Session("UserId_DomCFS") & " order by AddedOn desc"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtContainer.Text = Trim(dt.Rows(0)("ContainerNo") & "")
                'txtNOCNo_TextChanged(sender, e)
            End If
            'UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try

    End Sub
End Class
