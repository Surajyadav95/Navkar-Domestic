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
            Call sub_CreateTable()
            txtGateOutDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            grid()
            Filldropdown()

        End If
    End Sub
     
     
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
      
    
    Public Sub Filldropdown()
        ds = db.sub_GetDataSets("usp_empty_out_contaner")
        If (ds.Tables(0).Rows.Count > 0) Then
            ddltransporter.DataSource = ds.Tables(0)
            ddltransporter.DataTextField = "TransName"
            ddltransporter.DataValueField = "TransID"
            ddltransporter.DataBind()
            ddltransporter.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        'If (ds.Tables(1).Rows.Count > 0) Then
        '    ddlDriver.DataSource = ds.Tables(1)
        '    ddlDriver.DataTextField = "DriverCode"
        '    ddlDriver.DataValueField = "DriverID"
        '    ddlDriver.DataBind()
        '    ddlDriver.Items.Insert(0, New ListItem("--Select--", 0))
        'End If

        If (ds.Tables(2).Rows.Count > 0) Then
            ddlSize.DataSource = ds.Tables(2)
            ddlSize.DataTextField = "ContainerSize"
            ddlSize.DataValueField = "ContainerSizeID"
            ddlSize.DataBind()
            ddlSize.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        If (ds.Tables(3).Rows.Count > 0) Then
            ddltype.DataSource = ds.Tables(3)
            ddltype.DataTextField = "ContainerType"
            ddltype.DataValueField = "ContainerTypeID"
            ddltype.DataBind()
            ddltype.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        If (ds.Tables(4).Rows.Count > 0) Then
            ddlcustomer.DataSource = ds.Tables(4)
            ddlcustomer.DataTextField = "AGName"
            ddlcustomer.DataValueField = "AGID"
            ddlcustomer.DataBind()
            ddlcustomer.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        If (ds.Tables(5).Rows.Count > 0) Then
            ddlshippingline.DataSource = ds.Tables(5)
            ddlshippingline.DataTextField = "SLName"
            ddlshippingline.DataValueField = "SLID"
            ddlshippingline.DataBind()
            ddlshippingline.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        If (ds.Tables(7).Rows.Count > 0) Then
            ddlSource.DataSource = ds.Tables(7)
            ddlSource.DataTextField = "Location"
            ddlSource.DataValueField = "LocationID"
            ddlSource.DataBind()
            ddlSource.Items.Insert(0, New ListItem("--select--", 0))
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
    Protected Sub txttrailerno_TextChanged(sender As Object, e As EventArgs)
        Try
            txttrailerNo1.Text = ""
            txttrailer.Text = ""
            strSql = ""
            strSql = "Usp_gettrailern_In '" & Trim(txttrailerno.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txttrailerNo1.Text = Trim(dt.Rows(0)("trailername"))
                ddltransporter.SelectedValue = Trim(dt.Rows(0)("TransID"))
                'txtdrivercode.Text = Trim(dt.Rows(0)("DriverCode"))
                divtrailer.Attributes.Add("style", "display:None")
                divtrai.Attributes.Add("style", "display:block")
            Else
                divtrailer.Attributes.Add("style", "display:block")
                divtrai.Attributes.Add("style", "display:None")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtcontainerNo_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim intSize As String = ""
            Dim intListCount As String = ""
            Dim intTotalDays As String = ""
            Dim intLength As String = ""
            Dim intChar As String = ""
            Dim intTotal As String = ""
            Dim intDivide As String = ""
            Dim intMulti As String = ""
            Dim intSubstract As String = ""

            Dim strChar As String = ""
            Dim blCheck As Boolean = False


            strSql = "usp_domestic_empty_out_container '" & Trim(txtcontainerNo.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtjono.Text = Trim(dt.Rows(0)("JobNo") & "")
                ddlcustomer.SelectedValue = Trim(dt.Rows(0)("CustID"))
                ddlshippingline.SelectedValue = Trim(dt.Rows(0)("LineID"))
                ddlSize.SelectedItem.Text = Trim(dt.Rows(0)("size"))
                ddltype.SelectedValue = Trim(dt.Rows(0)("TypeID"))
                'lblCfsCode.Text = Trim(dt.Rows(0)("CFSCode"))
            Else
                txtjono.Text = ""
                ddlcustomer.SelectedValue = 0
                ddlshippingline.SelectedValue = 0
                ddlSize.SelectedValue = 0
                ddltype.SelectedValue = 0
                'lblCfsCode.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Record not found');", True)
                'MsgBox("Record not found.", vbInformation)
                txtcontainerNo.Text = ""
                txtcontainerNo.Focus()
                Exit Sub

            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CreateTable()
        Try

            Dim dtDomContainer As New DataTable
            dtDomContainer.Columns.Clear()
            Session("table_DomesticContainerout") = ""

            'dtDomContainer.Rows.Count(-1)

            dtDomContainer.Columns.Add("ContainerNo")
            dtDomContainer.Columns.Add("Size")
            dtDomContainer.Columns.Add("Type")
            dtDomContainer.Columns.Add("TypeID")
      
            dtDomContainer.Columns.Add("GrossWeight")
            dtDomContainer.Columns.Add("TareWeight")

            Dim dtRow2 As DataRow = dtDomContainer.NewRow

            'dtRow2.Item("ProductName") = ""
            'dtRow2.Item("Qty") = ""
            'dtRow2.Item("Unit") = ""
            'dtRow2.Item("Rate") = ""
            'dtRow2.Item("Amount") = ""
            'dtRow2.Item("vatper") = ""
            'dtRow2.Item("staxper") = ""
            'dtRow2.Item("staxamount") = ""
            'dtRow2.Item("cstper") = ""
            'dtRow2.Item("cstamount") = ""
            'dtRow2.Item("TotalAmount") = ""
            'dtDomContainer.Rows.Add(dtRow2)

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()
            Session("table_DomesticContainerout") = dtDomContainer


            If dtDomContainer.Rows.Count > 0 Then
                ' lblError.Text = dtDomContainer.Rows(0)("Designation")
            End If

        Catch ex As Exception
            '  lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            Dim size1 As String = ""
            If grdcontainer.Rows.Count = 2 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' Size is Invalid!.');", True)
                Exit Sub
            End If
            For Each row As GridViewRow In grdcontainer.Rows

                size1 = Val(CType(row.FindControl("lblsize"), Label).Text.ToString())

                'size1 = 20
            Next
            If size1 = "20" Then
                If ddlSize.SelectedItem.Text <> 20 Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Size is Invalid!.');", True)
                    Exit Sub
                End If
            ElseIf size1 = "40" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Size is Invalid!!.');", True)
                Exit Sub
            ElseIf size1 = "45" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' Size is Invalid!.');", True)
                Exit Sub
            End If

            For Each row In grdcontainer.Rows
                If txtcontainerNo.Text = Trim(CType(row.FindControl("lblcontainerno"), Label).Text & "") Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Container No already their. Cannot Proceed!');", True)
                    txtcontainerNo.Focus()
                    Exit Sub
                End If

            Next
            Dim dtDomContainer As New DataTable
            Dim intRows As Integer = 0

            dtDomContainer = DirectCast(Session("table_DomesticContainerout"), DataTable)
            intRows = dtDomContainer.Rows.Count
            Dim dtRow As DataRow = dtDomContainer.NewRow

            dtRow.Item("ContainerNo") = txtcontainerNo.Text
            dtRow.Item("Size") = ddlSize.SelectedItem.Text
            dtRow.Item("Type") = ddltype.SelectedItem.Text
            dtRow.Item("TypeID") = ddltype.SelectedValue

            dtRow.Item("GrossWeight") = Trim(txtGrossWeight.Text & "")
            dtRow.Item("TareWeight") = Trim(txttareWeight.Text & "")
            
            dtDomContainer.Rows.Add(dtRow)

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()

            Session("table_DomesticContainerout") = dtDomContainer
 
            ddlSize.SelectedValue = 0
            ddltype.SelectedValue = 0
            txtGrossWeight.Text = ""
            txttareWeight.Text = ""

            txtcontainerNo.Text = ""
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Trim(txttrailerNo1.Text) = "" And Trim(txttrailer.Text) = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Please fill the required fields!');", True)
                txttrailerno.Focus()
                Exit Sub
            End If
            Dim Blncount As Boolean = False
            For Each row In grdcontainer.Rows
                Blncount = True
                Exit For
            Next
            If Blncount = False Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Please add Container details!');", True)
                txtcontainerNo.Focus()
                Exit Sub
            End If
            For Each row In grdcontainer.Rows
                If Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") = Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") Then
                    strSql = ""
                    strSql = "select containerno  from Domestic_Empty_Out where containerno='" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "' and JONo='" & Trim(txtjono.Text & "") & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If dt.Rows.Count > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No already exists .');", True)
                        Exit Sub
                    End If

                End If
            Next
            Dim strSearchText As String = ""
            If txttrailerNo1.Text <> "" Then
                strSearchText = txttrailerNo1.Text
            ElseIf txttrailer.Text <> "" Then
                strSearchText = txttrailer.Text
            End If
            'Dim OutNo As String = ""
            strSql = "SELECT isnull(MAX(OUTNO),0)+1 as OUTNO FROM Domestic_Empty_Out"
            dt2 = db.sub_GetDatatable(strSql)
            If dt2.Rows.Count > 0 Then
                Lbloutno.Text = dt2.Rows(0)("OUTNO")
            Else
                Lbloutno.Text = 1
            End If

            For Each row In grdcontainer.Rows
                strSql = ""
                strSql += "USP_INSERT_DOMESTIC_EMPTY_OUT'" & Lbloutno.Text & "','" & Trim(txtjono.Text & "") & "','" & Trim(CType(row.FindControl("lblcontainerno"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblsize"), Label).Text & "") & "','" & strSearchText & "','" & Convert.ToDateTime(Trim(txtGateOutDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                strSql += "'" & Trim(txtBookingNo.Text & "") & "','" & Trim(ddlcustomer.SelectedValue & "") & "','" & Trim(ddlshippingline.SelectedValue & "") & "',"
                strSql += "'" & Trim(ddltransporter.SelectedValue & "") & "','" & Trim(ddlSource.SelectedValue & "") & "','" & Trim(CType(row.FindControl("lblcontypeID"), Label).Text & "") & "',"
                strSql += "'" & Trim(txtRemarks.Text & "") & "','" & Session("UserId_DomCFS") & "','" & Trim(CType(row.FindControl("lblGrossWeight"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTareWeight"), Label).Text & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next
            txtSlipNoPrint.Text = Lbloutno.Text
            Clear()
            lblSession.Text = "Record Saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            Call sub_CreateTable()
            txtentryID.Text = ""
            txtjono.Text = ""
            Lbloutno.Text = ""
            ddlcustomer.SelectedValue = 0
            ddlshippingline.SelectedValue = 0
            txtBookingNo.Text = ""
            ddlSource.SelectedValue = 0
            txtRemarks.Text = ""
            txtGateOutDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txttrailerNo1.Text = ""
            txttrailer.Text = ""
            txttrailerno.Text = ""
            ddltransporter.SelectedValue = 0
            txtdrivercode.Text = ""

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
End Class
