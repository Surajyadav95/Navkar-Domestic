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
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT00:00")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-ddT23:59")
            btnSave_Click(sender, e)
            Filldropdown()
        End If
    End Sub
    Public Sub Filldropdown()
        ds = db.sub_GetDataSets("usp_jobOrder_Fill")
        If (ds.Tables(0).Rows.Count > 0) Then
            ddlCustomerName.DataSource = ds.Tables(0)
            ddlCustomerName.DataTextField = "AGName"
            ddlCustomerName.DataValueField = "AGID"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        If (ds.Tables(1).Rows.Count > 0) Then
            ddlImporter.DataSource = ds.Tables(1)
            ddlImporter.DataTextField = "ImporterName"
            ddlImporter.DataValueField = "ImporterID"
            ddlImporter.DataBind()
            ddlImporter.Items.Insert(0, New ListItem("--select--", 0))
        End If

        If (ds.Tables(2).Rows.Count > 0) Then
            ddlcha.DataSource = ds.Tables(2)
            ddlcha.DataTextField = "CHAName"
            ddlcha.DataValueField = "CHAID"
            ddlcha.DataBind()
            ddlcha.Items.Insert(0, New ListItem("--Select--", 0))
        End If


    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim strSearchText As String = ""
            If txtigmno.Text <> "" Then
                strSearchText = txtigmno.Text
            ElseIf txtContainer.Text <> "" Then
                strSearchText = txtContainer.Text
            ElseIf txtbeno.Text <> "" Then
                strSearchText = txtbeno.Text
            ElseIf txtJono.Text <> "" Then
                strSearchText = txtJono.Text
            End If
            strSql = ""
            strSql += " USP_DOMESTIC_JOB_ORDER_SUMMARY '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd HH:mm") & "',"
            strSql += "'" & Val(ddlcriteria.SelectedValue) & "','" & Val(ddlCustomerName.SelectedValue) & "','" & Val(ddlImporter.SelectedValue) & "','" & Val(ddlcha.SelectedValue) & "','" & strSearchText & "'"
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

    Protected Sub ddlcriteria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If (ddlcriteria.SelectedValue = "1") Then
                divCust.Attributes.Add("style", "display:block")

            Else
                divCust.Attributes.Add("style", "display:None")
            End If
            If (ddlcriteria.SelectedValue = "2") Then
                divimporter.Attributes.Add("style", "display:block")


            Else
                divimporter.Attributes.Add("style", "display:None")
            End If

            If (ddlcriteria.SelectedValue = "3") Then
                divCHA.Attributes.Add("style", "display:block")


            Else
                divCHA.Attributes.Add("style", "display:None")
            End If

            If (ddlcriteria.SelectedValue = "4") Then
                divSearch.Attributes.Add("style", "display:block")


            Else
                divSearch.Attributes.Add("style", "display:None")
            End If

            If (ddlcriteria.SelectedValue = "5") Then
                divContainer.Attributes.Add("style", "display:block")


            Else
                divContainer.Attributes.Add("style", "display:None")
            End If

            If (ddlcriteria.SelectedValue = "6") Then
                divBeno.Attributes.Add("style", "display:block")


            Else
                divBeno.Attributes.Add("style", "display:None")
            End If

            If (ddlcriteria.SelectedValue = "7") Then
                divJoNo.Attributes.Add("style", "display:block")


            Else
                divJoNo.Attributes.Add("style", "display:None")
            End If

            'If (ddlcriteria.SelectedValue = "8") Then
            '    divSearch.Attributes.Add("style", "display:block")


            'Else
            '    divSearch.Attributes.Add("style", "display:None")
            'End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If
            Dim strSearchText As String = ""
            If txtigmno.Text <> "" Then
                strSearchText = txtigmno.Text
            ElseIf txtContainer.Text <> "" Then
                strSearchText = txtContainer.Text
            ElseIf txtbeno.Text <> "" Then
                strSearchText = txtbeno.Text
            ElseIf txtJono.Text <> "" Then
                strSearchText = txtJono.Text
            End If
            strSql = ""
            strSql += " USP_DOMESTIC_JOB_ORDER_SUMMARY '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd HH:mm") & "',"
            strSql += "'" & Val(ddlcriteria.SelectedValue) & "','" & Val(ddlCustomerName.SelectedValue) & "','" & Val(ddlImporter.SelectedValue) & "','" & Val(ddlcha.SelectedValue) & "','" & strSearchText & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Job Order Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
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
                        .Cell(11, 1).Value = "Job Order Summary"
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
                    Response.AddHeader("content-disposition", "attachment;filename=JobOrderSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
End Class
