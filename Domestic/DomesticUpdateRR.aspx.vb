Imports System.Data
Imports System.IO
Imports System.Data.SqlClient

Partial Class Account_ItemList
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim WHID, WHIDView As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'db.sub_ExecuteNonQuery("delete from TEMP_SLABS_DOMESTIC where USER_ID=" & Session("UserId_DomCFS") & "")
            sub_CreateTable()
            FillDropdown()
            ddlTrainNo.Focus()
        End If
    End Sub
    Private Sub sub_CreateTable()
        Dim dtDepoContainer As New DataTable

        dtDepoContainer.Columns.Clear()

        Session("table_DepoContainer") = ""

        dtDepoContainer.Columns.Add("SrNo")
        dtDepoContainer.Columns.Add("TrainNo")
        dtDepoContainer.Columns.Add("TrainID")
        dtDepoContainer.Columns.Add("RRNo")

        Dim dtRow2 As DataRow = dtDepoContainer.NewRow

        grdRRUpdate.DataSource = Nothing
        grdRRUpdate.DataSource = dtDepoContainer
        grdRRUpdate.DataBind()
        Session("table_DepoContainer") = dtDepoContainer

    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim dtDepoContainer As New DataTable
            Dim dtMNR As New DataTable
            Dim intRows As Integer = 0
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)

            If Not dtDepoContainer.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please add RR Details first');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If

            Dim param As New SqlParameter()
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@UserID", Session("UserId_DomCFS"))

            param.ParameterName = "@PT_DOMESTICRRUPDATION"
            param.Value = dtDepoContainer
            param.TypeName = "PT_DOMESTICRRUPDATION"
            param.SqlDbType = SqlDbType.Structured
            cmd.Parameters.Add(param)

            Dim strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("SqlConnString_Domestic").ConnectionString
            Dim con As New SqlConnection(strConnString)

            cmd.CommandText = "USP_INSERT_INTO_DOMESTIC_TRAIN_RR"
            cmd.Connection = con

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            Try
                con.Open()
                da.Fill(dtMNR)
            Catch ex As Exception
                Response.Write(ex.Message)
                If con.State = ConnectionState.Open Then con.Close()
                con.Dispose()
            Finally
                If con.State = ConnectionState.Open Then con.Close()
                con.Dispose()
            End Try

            If dtMNR.Rows.Count > 0 Then
                If dtMNR.Rows(0)(0) > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record saved successfully');", True)
                    ddlTrainNo.SelectedValue = 0
                    txtRRNo.Text = ""
                    sub_CreateTable()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not saved successfully');", True)
                    Exit Sub
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not saved successfully');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim dtDepoContainer As New DataTable
            Dim intRows As Integer = 0
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)
            For i = 0 To dtDepoContainer.Rows.Count - 1
                If Trim(txtRRNo.Text) = Trim(dtDepoContainer.Rows(i)("RRNo")) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('RR No already added');", True)
                    txtRRNo.Focus()
                    Exit Sub
                End If
            Next
            strSql = ""
            strSql += "select * from domestic_train_RR WHERE RRNO='" & Trim(txtRRNo.Text) & "' and iscancel=0"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('RR No already saved');", True)
                txtRRNo.Focus()
                Exit Sub
            End If

            Dim dtRow As DataRow = dtDepoContainer.NewRow

            dtRow.Item("SrNo") = Val(dtDepoContainer.Rows.Count + 1)
            dtRow.Item("TrainNo") = Trim(ddlTrainNo.SelectedItem.Text & "")
            dtRow.Item("TrainID") = Val(ddlTrainNo.SelectedValue & "")
            dtRow.Item("RRNo") = Trim(txtRRNo.Text & "")

            dtDepoContainer.Rows.Add(dtRow)

            grdRRUpdate.DataSource = Nothing
            grdRRUpdate.DataSource = dtDepoContainer
            grdRRUpdate.DataBind()

            Session("table_DepoContainer") = dtDepoContainer
            ddlTrainNo.SelectedValue = 0
            txtRRNo.Text = ""
            ddlTrainNo.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    'Public Sub FillGrid()
    '    Try
    '        strSql = ""
    '        strSql += "select * from TEMP_SLABS_DOMESTIC WHERE IS_CANCEL=0 AND USER_ID=" & Session("UserId_DomCFS") & ""
    '        dt = db.sub_GetDatatable(strSql)
    '        grdSlabList.DataSource = dt
    '        grdSlabList.DataBind()
    '    Catch ex As Exception
    '        lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '    End Try
    'End Sub
    Public Sub FillDropdown()
        Try
            strSql = ""
            strSql += "select TrainID,TrainNo from Domestic_trainmaster where IsCancel=0 order by TrainNo"
            dt = db.sub_GetDatatable(strSql)
            ddlTrainNo.DataSource = dt
            ddlTrainNo.DataValueField = "TrainID"
            ddlTrainNo.DataTextField = "TrainNo"
            ddlTrainNo.DataBind()
            ddlTrainNo.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim SrNo As String = lnkcancel.CommandArgument

            Dim dtDepoContainer As New DataTable
            Dim intRows As Integer = 0
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)
            For i = 0 To dtDepoContainer.Rows.Count - 1
                If Trim(SrNo & "") = Trim(dtDepoContainer.Rows(i)("SrNo") & "") Then
                    dtDepoContainer.Rows(i).Delete()
                    Exit For
                End If
            Next
            grdRRUpdate.DataSource = Nothing
            grdRRUpdate.DataSource = dtDepoContainer
            grdRRUpdate.DataBind()
            Session("table_DepoContainer") = dtDepoContainer
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
