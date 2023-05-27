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
            btnsearch_Click(sender, e)
           
            
        End If
    End Sub

    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Trim(txtCommodityname.Text) <> "" Then
                strSql = ""
                strSql += "SELECT Count(*) FROM Commodity_M_Domestic WHERE Commodity='" & Replace(Trim(txtCommodityname.Text & ""), "'", "''") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows(0)(0) >= 1) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Commodity name already exists .');", True)
                    'MsgBox("This service name already exists", vbInformation)
                    txtCommodityname.Text = ""
                    txtCommodityname.Focus()

                    Exit Sub
                End If
            End If

                strSql = ""
            strSql += "USP_INSERT_COMMODITY_M_DOMESTIC'" & Replace(Trim(txtCommodityname.Text & ""), "'", "''") & "'"
                dt = db.sub_GetDatatable(strSql)
            lblSession.Text = "Record saved successfully     "
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                ''UpdatePanel3.Update()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_search_commodity_m_domestic '" & Trim(txtsearch.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            'grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnsearch_Click(sender, e)
    End Sub
End Class
