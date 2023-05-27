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

            txtdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Filldropdown()
            btnSave_Click(sender, e)
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "usp_fill_Inventory_summary"
            ds = db.sub_GetDataSets(strSql)
           

            ddlcustomer.DataSource = ds.Tables(0)
            ddlcustomer.DataTextField = "AGName"
            ddlcustomer.DataValueField = "AGID"
            ddlcustomer.DataBind()
            ddlcustomer.Items.Insert(0, New ListItem("--Select--", 0))

            ddlLineNAme.DataSource = ds.Tables(1)
            ddlLineNAme.DataTextField = "SLName"
            ddlLineNAme.DataValueField = "SLID"
            ddlLineNAme.DataBind()
            ddlLineNAme.Items.Insert(0, New ListItem("--Select--", 0))

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
            strSql += " USP_EMPTY_CONTAINER_STOCK '" & Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlCategory.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlcustomer.SelectedValue & "") & "','" & Trim(ddlLineNAme.SelectedValue & "") & "'"
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
           

            strSql = ""
            strSql += " USP_EMPTY_CONTAINER_STOCK '" & Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlCategory.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlcustomer.SelectedValue & "") & "','" & Trim(ddlLineNAme.SelectedValue & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += "Select * from Con_Details_Domestic"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Inventory Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt.Columns.Count)
                         .Range("A1:" & Excelno & "1").InsertRowsAbove(7)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Range("A5:" & Excelno & "5").Merge()
                        .Range("A6:" & Excelno & "6").Merge()
                        .Range("A7:" & Excelno & "7").Merge()

                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(2, 1).Value = Trim(dt10.Rows(0)("con_NameI") & "")
                        .Cell(3, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")
                        .Cell(4, 1).Value = Trim(dt10.Rows(0)("AddressII") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(6).Height = 20
                        .Row(6).Height = 20
                        .Cell(6, 1).Value = "From: " + Convert.ToDateTime(txtdate.Text).ToString("dd MMM yyyy")
                        .Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(7, 1).Value = "Inventory Summary"
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
                        .Cell(7, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(7, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(7, 1).Style.Font.FontSize = 17
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=InventorySummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
            If (ddlCategory.SelectedValue = "1") Then
                divtype.Attributes.Add("style", "display:block")

            Else
                divtype.Attributes.Add("style", "display:None")
            End If
            If (ddlCategory.SelectedValue = "2") Then
                divcustomer.Attributes.Add("style", "display:block")
                'ddlLine.SelectedValue = 0

            Else
                divcustomer.Attributes.Add("style", "display:None")
            End If
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
