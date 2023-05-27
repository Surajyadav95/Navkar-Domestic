Imports System.Data
Imports System.IO
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
            getItemList()

        End If
    End Sub

    Protected Sub getItemList()
        Try
            strSql = ""
            strSql += "usp_list_EQUIPMENT_Master"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            'up_grid.Update()
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
            strSql += "USP_EQUIPMENT_Master '" & Replace(Trim(txtEquipmentId.Text & ""), "'", "''") & "','" & Replace(Trim(txtEquipmentname.Text & ""), "'", "''") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                txtEquipmentId.Text = ""
                txtEquipmentId.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Equipment ID already exists .');", True)
                Exit Sub
            End If
            If (ds.Tables(1).Rows.Count > 0) Then
                txtEquipmentname.Text = ""
                txtEquipmentname.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' Equipment name already exists .');", True)
                Exit Sub
            End If

            strSql = ""
            strSql += "USP_INSERT_EQUIPMENT_M'" & Replace(Trim(txtEquipmentname.Text & ""), "'", "''") & "'"
            dt = db.sub_GetDatatable(strSql)
            txtEquipmentId.Focus()
            lblSession.Text = "Record saved successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            'UpdatePanel.Update()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.getItemList()
    End Sub
End Class
