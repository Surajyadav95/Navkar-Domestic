Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
Imports System.Configuration

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt10 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now.AddDays(-7)).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")
            Filldropdown()
            btnSave_Click(sender, e)
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            'strSql = ""
            'strSql += "usp_fill_Loading_summary"
            'ds = db.sub_GetDataSets(strSql)
            'ddlStuffingType.DataSource = ds.Tables(0)
            'ddlStuffingType.DataTextField = "Name"
            'ddlStuffingType.DataValueField = "ID"
            'ddlStuffingType.DataBind()
            'ddlStuffingType.Items.Insert(0, New ListItem("--Select--", 0))
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
            strSql += " USP_ON_WHEEL_STUFFING_REGISTER '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd 23:59:00 ") & "','" & Trim(ddlCategory.SelectedValue & "") & "',"
            strSql += "'" & Trim(txtSearchText.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If

            strSql = ""
            strSql += " USP_ON_WHEEL_STUFFING_REGISTER '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd 23:59:00 ") & "','" & Trim(ddlCategory.SelectedValue & "") & "',"
            strSql += "'" & Trim(txtSearchText.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += "Select * from con_details_domestic"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "On Wheel Stuff Summary")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(12)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Range("A5:" & Excelno & "5").Merge()
                        .Range("A6:" & Excelno & "6").Merge()
                        .Range("A7:" & Excelno & "7").Merge()
                        .Range("A8:" & Excelno & "8").Merge()
                        .Range("A9:" & Excelno & "9").Merge()
                        .Range("A10:" & Excelno & "10").Merge()
                        .Range("A11:" & Excelno & "11").Merge()
                        .Range("A12:" & Excelno & "12").Merge()

                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(2, 1).Value = Trim(dt10.Rows(0)("con_NameI") & "")
                        .Cell(3, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")
                        .Cell(4, 1).Value = Trim(dt10.Rows(0)("AddressII") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(6).Height = 20
                        .Row(10).Height = 20
                        .Cell(10, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(10, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(11, 1).Value = "On Wheel Stuff Summary"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(2, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(4, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(5, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(5, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(6, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(6, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(6, 1).Style.Font.FontSize = 17
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Cell(11, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(11, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(11, 1).Style.Font.FontSize = 17
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=OnWheelStuffSummary.xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        Response.Flush()
                        Response.End()
                    End Using
                End Using
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No record found!');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try


        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkcancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkRemove.CommandArgument
            dt = db.sub_GetDatatable("USP_Cancel_Container '" & AutoID & "','" & Session("UserId_DomCFS") & "'")

            lblSession.Text = "Record updated successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
