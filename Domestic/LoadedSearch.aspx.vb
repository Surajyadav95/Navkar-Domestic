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
            'getItemList()
            db.sub_ExecuteNonQuery("Delete from Temp_Loaded_InD Where UserID=" & Session("UserId_DomCFS") & "")
            btnSave_Click(sender, e)
        End If
    End Sub
    Protected Sub getItemList()
        Try
            strSql = ""
            strSql += "USP_Bond_Search"
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
            strSql += " usp_domestic_list_search '" & Trim(txtsearch.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        'grdcontainer.DataSource = dt
        'grdcontainer.PageIndex = e.NewPageIndex
        Me.getItemList()
    End Sub
    Protected Sub lnkselect_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim Auto_Id As String = lnkRemove.CommandArgument
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)
            strSql = ""
            strSql += "USP_INSERT_Temp_Loaded_InD '" & Auto_Id & "','" & Session("UserId_DomCFS") & "','" & Trim(CType(gr.FindControl("lblJONO"), Label).Text) & "'"
            db.sub_ExecuteNonQuery(strSql)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:callparentfunction();", True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
