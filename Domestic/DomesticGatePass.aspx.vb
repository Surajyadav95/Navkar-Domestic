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
    Dim intJONo As Integer = 0
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
            Filldropdown()
            txtgatepassdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtBoeDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtLrDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            grid()
        End If
    End Sub
    Protected Sub grid()
        Try
            strSql = ""
            strSql += ""
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub fillgrid()
        Try
            strSql = ""
            strSql += "usp_Loaded_Gate_Pass_search_NEW '" & Trim(lblLoaded.Text & "") & "','" & Trim(lblLoadingID.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "usp_gate_pass_fill_Domestic"
            ds = db.sub_GetDataSets(strSql)
            ddltransporter.DataSource = ds.Tables(0)
            ddltransporter.DataTextField = "TransName"
            ddltransporter.DataValueField = "transID"
            ddltransporter.DataBind()
            ddltransporter.Items.Insert(0, New ListItem("--Select--", 0))

            ddlLocation.DataSource = ds.Tables(1)
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
    'Protected Sub btnshow_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim strSQL As String
    '        If ddldeliverytype.SelectedValue = "1" Or ddldeliverytype.SelectedValue = "2" Then

    '            If ddldeliverytype.SelectedValue = "2" Then

    '                strSQL = "Select containerno from Domestic_Line_Ctr_Destuff where containerno='" & Trim(txtcontainerNo.Text & "") & "' and iscancel=0"
    '                dt = db.sub_GetDatatable(strSQL)
    '                If Not dt.Rows.Count > 0 Then
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please do destuff entry to proceed.');", True)

    '                    Exit Sub
    '                End If
    '            End If

    '            Dim dtdetails As New DataTable
    '            'strSQL = ""
    '            'strSQL += "USP_DMGatePassFetch '" & Trim(ddlsearchby.SelectedValue & "") & "','" & Trim(txtcontainerNo.Text & "") & "','" & Trim(txtigmno.Text & "") & "','" & Trim(txtItemNo.Text & "") & "'"
    '            'dtdetails = db.sub_GetDatatable(strSQL)
    '            strSQL = ""
    '            strSQL = "SELECT   D.jono ,BENo ,BEDATE from DOMESTIC_JOBORDERM m INNER JOIN DOMESTIC_JOBORDERD d ON M. jono=D.jono    where D.containerno ='" & Trim(txtcontainerNo.Text & "") & "' and D.jono ='" & Trim(intJONo & "") & "' and M.iscancel =0 order by  jodate desc "
    '            dtdetails = db.sub_GetDatatable(strSQL)
    '            If dtdetails.Rows.Count > 0 Then
    '                intJONo = Trim(dtdetails.Rows(0)("JONo") & "")

    '                txtboeno.Text = Trim(dtdetails.Rows(0)("BENo") & "")
    '                txtBoeDate.Text = Convert.ToDateTime(Trim(dtdetails.Rows(0)("BEDATE")) & "").ToString("yyyy-MM-ddTHH:mm")
    '            End If
    '            strSQL = ""
    '            strSQL += "usp_Domestic_Gate_Pass_Search '" & Trim(ddlsearchby.SelectedValue & "") & "','" & Trim(txtcontainerNo.Text & "") & "','" & Trim(txtigmno.Text & "") & "','" & Trim(txtItemNo.Text & "") & "'"
    '            dt = db.sub_GetDatatable(strSQL)
    '            grdcontainer.DataSource = dt
    '            grdcontainer.DataBind()


    '        Else
    '            Dim dt1 As New DataTable
    '            strSQL = ""
    '            strSQL += "usp_Domestic_Gate_Pass_Search_In '" & Trim(ddlsearchby.SelectedValue & "") & "','" & Trim(txtcontainerNo.Text & "") & "','" & Trim(txtigmno.Text & "") & "','" & Trim(txtItemNo.Text & "") & "'"
    '            dt1 = db.sub_GetDatatable(strSQL)
    '            grdcontainer.DataSource = dt1
    '            grdcontainer.DataBind()
    '        End If
    '' usp_Loaded_Gate_Pass_search
    '    Catch ex As Exception
    '        lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '    End Try
    'End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim Blncount As Boolean = False
            For Each row In grdcontainer.Rows
                Blncount = True
                Exit For
            Next
            If Blncount = False Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please add gate pass details first');", True)
                Exit Sub
            End If
            
            For Each row In grdcontainer.Rows
                If Trim(CType(row.FindControl("txtLoadedWeight"), TextBox).Text & "") = "" Or Val(CType(row.FindControl("txtLoadedWeight"), TextBox).Text & "") = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Enter Delivered Qty');", True)
                    CType(row.FindControl("txtLoadedWeight"), TextBox).Focus()
                    Exit Sub
                End If
                If Trim(CType(row.FindControl("txtLoadedQty"), TextBox).Text & "") = "" Or Val(CType(row.FindControl("txtLoadedQty"), TextBox).Text & "") = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Enter Delivered Weight');", True)
                    CType(row.FindControl("txtLoadedQty"), TextBox).Focus()
                    Exit Sub
                End If
                If Trim(CType(row.FindControl("txtVehicle"), TextBox).Text & "") = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Enter Vehicle No');", True)
                    CType(row.FindControl("txtVehicle"), TextBox).Focus()
                    Exit Sub
                End If
                strSql = ""
                strSql += "USP_TRAILER_CHECKING_DOMESTIC '" & Trim(CType(row.FindControl("txtVehicle"), TextBox).Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If Not dt.Rows.Count > 0 And Trim(txtLrno.Text & "") = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter LR No');", True)
                    txtLrno.Focus()
                    btnSave.Text = "Save"
                    btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                    Exit Sub
                End If
            Next
            If Not Trim(txtLrno.Text & "") = "" Then
                If Trim(txtLrDate.Text & "") = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter LR Date');", True)
                    txtLrDate.Focus()
                    btnSave.Text = "Save"
                    btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                    Exit Sub
                End If
            End If
            strSql = "SELECT isnull(MAX(GPNo),0)+1 as EntryID FROM Domestic_GatePassM"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtgatepassno.Text = dt.Rows(0)("EntryID")
            Else
                txtgatepassno.Text = 1
            End If
            For Each row In grdcontainer.Rows
                strSql = ""
                strSql += "USp_DomesticGatePassDetailsM '" & Trim(txtgatepassno.Text & "") & "','" & Convert.ToDateTime(Trim(txtgatepassdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(CType(row.FindControl("lblLoaded_ID"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblIGMNO"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblITEMNO"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblCargoType"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lbIMPORTER"), Label).Text & "") & "','" & Session("UserId_DomCFS") & "','" & Trim(txtboeno.Text & "") & "','" & Convert.ToDateTime(Trim(txtBoeDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                strSql += "'" & Trim(txtRemarks.Text & "") & "','" & Trim(CType(row.FindControl("lblREFJONO"), Label).Text & "") & "','" & Trim(txtAddress.Text & "") & "','" & Trim(txtLrno.Text & "") & "','" & Convert.ToDateTime(Trim(txtLrDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblJoNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblCHA"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLINEID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblCUSTOMERID"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblPONO"), Label).Text & "") & "','" & Val(ddlLocation.SelectedValue) & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblSize"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("txtSealNo"), TextBox).Text & "") & "','" & Trim(CType(row.FindControl("txtVehicle"), TextBox).Text & "") & "','" & Trim(ddltransporter.SelectedValue & "") & "','" & Val(CType(row.FindControl("txtLoadedQty"), TextBox).Text & "") & "',"
                strSql += "'" & Val(CType(row.FindControl("txtLoadedWeight"), TextBox).Text & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next
            For Each row In grdcontainer.Rows
                strSql = ""
                strSql += " USP_INSERT_DOMESTIC_GATEPASSD'" & Trim(txtgatepassno.Text & "") & "','" & Trim(CType(row.FindControl("lblJoNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblSize"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("txtSealNo"), TextBox).Text & "") & "','" & Trim(CType(row.FindControl("txtVehicle"), TextBox).Text & "") & "','" & Trim(ddltransporter.SelectedValue & "") & "','" & Val(CType(row.FindControl("txtLoadedQty"), TextBox).Text & "") & "',"
                strSql += "'" & Val(CType(row.FindControl("txtLoadedWeight"), TextBox).Text & "") & "','" & Trim(CType(row.FindControl("lbllocationID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblPKGS"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblStuffed_Wt"), Label).Text & "") & "','" & Convert.ToDateTime(Trim(txtgatepassdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(CType(row.FindControl("lblLoaded_ID"), Label).Text & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next
            txtgatepassno.Text = Val(dt.Rows(0)("EntryID") & "")
            Clear()
            lblSession.Text = "Record Saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel5.Update()
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
    Protected Sub txtDestuffPkgs_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, TextBox).NamingContainer, GridViewRow)
            If Val(CType(gr.FindControl("txtDestuffPkgs"), TextBox).Text & "") > Val(CType(gr.FindControl("lblPKGS"), Label).Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Delivered Qty cannot be greater than total PKGS.');", True)
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
            If Val(CType(gr.FindControl("txtshortpkgs"), TextBox).Text & "") > Val(CType(gr.FindControl("lblWEIGHT"), Label).Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Delivered Weight cannot be greater than total PKGS.');", True)
                CType(gr.FindControl("txtshortpkgs"), TextBox).Text = ""
                CType(gr.FindControl("txtshortpkgs"), TextBox).Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            strSql = ""
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()

            'txtcontainerNo.Text = ""
            'divContainer.Attributes.Add("style", "display:none")
            'divIgmNo.Attributes.Add("style", "display:none")
            'divItem.Attributes.Add("style", "display:none")
            'ddldeliverytype.SelectedValue = 0
            'ddlsearchby.SelectedValue = 0
            ddltransporter.SelectedValue = 0
            txtLrno.Text = ""
            txtLrDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtboeno.Text = ""
            txtBoeDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtRefJoNo.Text = ""
            txtAddress.Text = ""
            txtRemarks.Text = ""
            'ddldeliverytype.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnDomesticgate_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select ContainerNo,LoadedID from Temp_Container_Search_Gate where userid=" & Session("UserId_DomCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblLoaded.Text = Trim(dt.Rows(0)("ContainerNo") & "")
                lblLoadingID.Text = Trim(dt.Rows(0)("LoadedID") & "")
                fillgrid()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
