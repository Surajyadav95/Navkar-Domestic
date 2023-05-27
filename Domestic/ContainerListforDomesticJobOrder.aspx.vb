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
            db.sub_ExecuteNonQuery("Delete from Temp_Containers_Domestic_JobOrder Where UserID=" & Session("UserId_DomCFS") & "")
            If Not Trim(Request.QueryString("IGMNo") & "") = "" Or Not Trim(Request.QueryString("ItemNo") & "") = "" Then
                btnSave_Click(sender, e)
            End If
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " usp_domestic_CONTAINERlist_search_for_JOBORDER '" & Trim(txtsearch.Text & "") & "','" & Request.QueryString("IGMNo") & "','" & Request.QueryString("ItemNo") & "'"
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
        Me.btnSave_Click(sender, e)
    End Sub
    Protected Sub lnkselect_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim Auto_Id As String = lnkRemove.CommandArgument
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)

            strSql = ""
            strSql += "USP_INSERT_Temp_Containers_Domestic_JobOrder '" & Auto_Id & "','" & Session("UserId_DomCFS") & "','" & Trim(CType(gr.FindControl("lblJONo"), Label).Text) & "'"
            db.sub_ExecuteNonQuery(strSql)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:callparentfunction();", True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAddClick_Click(sender As Object, e As EventArgs)
        Try

            For Each row In grdcontainer.Rows

                Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                If (chkright.Checked = True) Then
                    strSql = ""
                    strSql += "USP_INSERT_Temp_Containers_Domestic_JobOrder '" & Trim(CType(row.FindControl("lblCONTAINERNO"), Label).Text) & "','" & Session("UserId_DomCFS") & "','" & Trim(CType(row.FindControl("lblJONo"), Label).Text) & "','" & "Multipal" & "'"
                    db.sub_ExecuteNonQuery(strSql)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:callparentfunction();", True)
                End If
            Next
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
