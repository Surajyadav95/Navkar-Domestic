Imports System.Data.SqlClient
Imports System.Data

Partial Class RA_asd
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Domestic
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillDashboard()
            fillclosed()
            checkRights()
        End If
    End Sub
    Private Sub checkRights()
        Try
            Dim strConn As String = ""
            Dim conn As SqlConnection
            Dim cmd As New SqlCommand
            strConn = ConfigurationManager.ConnectionStrings("SqlConnString_Domestic").ConnectionString
            If conn Is Nothing OrElse conn.ConnectionString = Nothing Then
                conn = New System.Data.SqlClient.SqlConnection(strConn)
            End If
            If conn.State = System.Data.ConnectionState.Closed Then
                Try
                    conn.Open()
                Catch Ex As Exception
                    conn = New System.Data.SqlClient.SqlConnection(strConn)
                    conn.Open()
                End Try
            End If

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter
            Dim strSQL = ""
            strSQL = " SELECT * FROM UserRights INNER JOIN MenuDetails ON UserRights.MenuID=MenuDetails.MenuID "
            strSQL += " where MenuDetails.menuid IN (209, 305, 517, 203) and UserID='" & Convert.ToInt32(Session("UserId_DomCFS")) & "'"

            cmd.CommandTimeout = 120
            cmd.CommandText = strSQL
            cmd.Connection = conn
            If conn.State = ConnectionState.Closed Then conn.Open()
            da.SelectCommand = cmd
            da.Fill(dt)          
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub fillDashboard()
        Try
            ds = db.sub_GetDataSets("GET_Sp_BOND_CFS_SummaryDtels")
            If (ds.Tables(0).Rows.Count > 0) Then
                lblreg.Text = ds.Tables(0).Rows(0)(1)
                lblbondIn.Text = ds.Tables(0).Rows(1)(1)
                lblbondex.Text = ds.Tables(0).Rows(2)(1)
                lbllive.Text = ds.Tables(0).Rows(3)(1)
                lblEx.Text = ds.Tables(0).Rows(4)(1)
                lblPendingDocs.Text = ds.Tables(2).Rows(0)(0)
            End If
            grdbalvalduty.DataSource = ds.Tables(1)
            grdbalvalduty.DataBind()
            Dim dblValDuty As Double = 0
            Dim dblTotalValDuty As Double = 0

            For Each row In grdbalvalduty.Rows
                dblValDuty = CType(row.FindControl("lblBalance"), Label).Text.ToString()
                dblTotalValDuty += dblValDuty
            Next
            If grdbalvalduty.Rows.Count > 0 Then
                grdbalvalduty.FooterRow.Cells(0).Text = "Total"
                grdbalvalduty.FooterRow.Cells(1).Text = dblTotalValDuty
                grdbalvalduty.FooterRow.Cells(1).HorizontalAlign = HorizontalAlign.Center
            End If
        Catch ex As Exception
            'lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub fillclosed()
        Try
            dt = db.sub_GetDatatable("SP_Bond_Space_Status")
            If dt.Rows.Count > 0 Then
                lblopen.Text = Trim(dt.Rows(0)("Tool") & "")
                lblopensqft.Text = Trim(dt.Rows(0)("Tool1") & "")
                lblclosed.Text = Trim(dt.Rows(1)("Tool") & "")
                lblclosedsqft.Text = Trim(dt.Rows(1)("Tool1") & "")
            End If

        Catch ex As Exception
            'lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
