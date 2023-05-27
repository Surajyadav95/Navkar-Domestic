Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel
Imports System.Globalization

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then            
            sub_CreateTable()
            FillDropdown()
        End If
    End Sub
    Private Sub sub_CreateTable()
        Dim dtDepoContainer As New DataTable

        dtDepoContainer.Columns.Clear()

        Session("table_DepoContainer") = ""

        dtDepoContainer.Columns.Add("ContainerNo")
        dtDepoContainer.Columns.Add("EntryID")
        dtDepoContainer.Columns.Add("StuffPkgs")
        dtDepoContainer.Columns.Add("StuffWeight")
        dtDepoContainer.Columns.Add("StuffDate")
        dtDepoContainer.Columns.Add("TrainNo")
        dtDepoContainer.Columns.Add("TrainID")
        dtDepoContainer.Columns.Add("WagonNo")
        dtDepoContainer.Columns.Add("InvoiceNo")
        dtDepoContainer.Columns.Add("DONo")
        dtDepoContainer.Columns.Add("SealNo")
        dtDepoContainer.Columns.Add("Remark")      
        dtDepoContainer.Columns.Add("Location")
        dtDepoContainer.Columns.Add("ScrapPks")

        Dim dtRow2 As DataRow = dtDepoContainer.NewRow

        grdOutDets.DataSource = Nothing
        grdOutDets.DataSource = dtDepoContainer
        grdOutDets.DataBind()
        Session("table_DepoContainer") = dtDepoContainer

    End Sub
    Private Sub FillDropdown()
        Try
            strSql = ""
            strSql += "select LocationID,Location from Ext_Location_M where isactive=1 order by Location"
            dt = db.sub_GetDatatable(strSql)
            ddlLocation.DataSource = dt
            ddlLocation.DataTextField = "Location"
            ddlLocation.DataValueField = "LocationID"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem("--Select-", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim dtDepoContainer As New DataTable
            Dim intRows As Integer = 0
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)

            If Not dtDepoContainer.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Import Stuffing Details first');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If

            Dim param As New SqlParameter()
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@UserID", Session("UserId_DomCFS"))
            cmd.Parameters.AddWithValue("@LocationID", Val(ddlLocation.SelectedValue))
            'cmd.Parameters.AddWithValue("@TrainNo", Replace(Trim(txtTrainNo.Text & ""), "'", "''"))
            param.ParameterName = "@PT_DOMESTICONWHEELSTUFFING"
            param.Value = dtDepoContainer
            param.TypeName = "PT_DOMESTICONWHEELSTUFFING"
            param.SqlDbType = SqlDbType.Structured
            cmd.Parameters.Add(param)
            Dim strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("SqlConnString_Domestic").ConnectionString
            Dim con As New SqlConnection(strConnString)
            cmd.CommandText = "USP_INSERT_INTO_DOMESTIC_LOADING_BY_TRAIN"
            cmd.Connection = con
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            Dim dtMNR As New DataTable
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
                    Control_Clear()
                    btnSave.Text = "Save"
                    btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                    lblSession.Text = "Record saved successfully "
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                    UpdatePanel3.Update()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not saved successfully');", True)
                    btnSave.Text = "Save"
                    btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                    Exit Sub
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not saved successfully');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If

        Catch ex As Exception

            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
        End Try
    End Sub
    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select '' [Container No],'' [Stuff Pkgs],'' [Stuff Weight],'' [Stuff Date],'' [Train No],'' [Wagon No],'' [Invoice No],'' [DO No], '' [Seal No],'' [Remark], '' as [Location], '' as [Scrap Pks]"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "On Wheel Stuff Template")
                    With wb.Worksheets(0)
                        .Column(4).Style.DateFormat.Format = "yyyy-MM-dd"
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=OnWheelStuffTemplate.xlsx")
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
    Private Sub Upload(sender As Object, e As EventArgs, FilePath As String)
        Try
            Dim intRows As Integer = 0
            Dim dtDepoContainer As New DataTable
            Dim strContainer As String = ""
            Dim strContainer1 As String = ""
            Dim strContainer2 As String = ""
            Dim strContainer3 As String = ""
            Dim strContainer4 As String = ""
            Dim strContainer5 As String = ""
            Dim strContainer6 As String = ""
            Dim formats() As String = {"dd-MM-yyyy", "yyyy-MM-dd", "dd/MM/yyyy", "yyyy/MM/dd", "dd-MM-yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "dd/MM/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "dd-MM-yyyy HH:mm:ss tt", "yyyy-MM-dd HH:mm:ss tt", "dd/MM/yyyy HH:mm:ss tt", "yyyy/MM/dd HH:mm:ss tt", "dd-MM-yyyy hh:mm:ss tt", "yyyy-MM-dd hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt", "yyyy/MM/dd hh:mm:ss tt"}
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)
            Dim int20 As Integer = 0, int40 As Integer = 0, int45 As Integer = 0, intTues As Integer = 0
            If FileUpload1.HasFile Then
                'Dim filePath As String = FileUpload1.FileName
                Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
                lblfilename.Text = "File Name: " + FileName
                'Dim FilePath As String = Server.MapPath(FolderPath + FileName)
                Using workBook As New XLWorkbook(FilePath)
                    Dim workSheet As IXLWorksheet = workBook.Worksheet(1)
                    Dim firstRow As Boolean = True
                   
                    For Each row As IXLRow In workSheet.Rows()
                        Dim dblDuplicate As Double = 0, dblWeight As Double = 0
                        If Not Trim(row.Cell(1).Value.ToString()) = "" Then
                            If Not firstRow Then
                                For i = 0 To dtDepoContainer.Rows.Count - 1
                                    If Trim(dtDepoContainer.Rows(i)("ContainerNo")) = Trim(row.Cell(1).Value.ToString()) Then
                                        dblDuplicate += 1
                                    End If
                                    If Trim(dtDepoContainer.Rows(i)("WagonNo")) = Trim(row.Cell(6).Value.ToString()) Then
                                        dblWeight += Val(dtDepoContainer.Rows(i)("StuffWeight"))
                                    End If
                                Next
                                dblWeight += Val(row.Cell(3).Value)
                                'If dblDuplicate > 0 Then
                                '    GoTo lblnext
                                'End If
                                strSql = ""
                                strSql += "USP_VALIDATION_ON_WHEEL_STUFFING_UPLOAD '" & Trim(row.Cell(1).Value.ToString()) & "','" & Trim(row.Cell(2).Value.ToString()) & "',"
                                strSql += "'" & Trim(row.Cell(3).Value.ToString()) & "','" & Trim(row.Cell(5).Value.ToString()) & "','" & Trim(row.Cell(6).Value.ToString()) & "','" & Trim(row.Cell(11).Value.ToString()) & "'"
                                ds = db.sub_GetDataSets(strSql)
                                If Not ds.Tables(0).Rows.Count > 0 Then
                                    If strContainer1 = "" Then
                                        strContainer1 = Trim(row.Cell(1).Value.ToString())
                                    Else
                                        If Not InStr(strContainer1, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                            strContainer1 += "," + Trim(row.Cell(1).Value.ToString())
                                        End If
                                    End If
                                End If
                                If ds.Tables(1).Rows.Count > 0 Then
                                    If strContainer2 = "" Then
                                        strContainer2 = Trim(row.Cell(1).Value.ToString())
                                    Else
                                        If Not InStr(strContainer2, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                            strContainer2 += "," + Trim(row.Cell(1).Value.ToString())
                                        End If
                                    End If
                                End If
                                If ds.Tables(2).Rows.Count > 0 Then
                                    If strContainer3 = "" Then
                                        strContainer3 = Trim(row.Cell(1).Value.ToString())
                                    Else
                                        If Not InStr(strContainer3, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                            strContainer3 += "," + Trim(row.Cell(1).Value.ToString())
                                        End If
                                    End If
                                End If
                                If Not ds.Tables(3).Rows.Count > 0 Then
                                    If strContainer4 = "" Then
                                        strContainer4 = Trim(row.Cell(1).Value.ToString())
                                    Else
                                        If Not InStr(strContainer4, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                            strContainer4 += "," + Trim(row.Cell(1).Value.ToString())
                                        End If
                                    End If
                                End If
                                Dim dtdate As DateTime
                                If DateTime.TryParseExact(Trim(row.Cell(4).Value.ToString()), formats, New CultureInfo("en-GB"), DateTimeStyles.None, dtdate) Then
                                Else
                                    If strContainer5 = "" Then
                                        strContainer5 = Trim(row.Cell(1).Value.ToString())
                                    Else
                                        If Not InStr(strContainer5, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                            strContainer5 += "," + Trim(row.Cell(1).Value.ToString())
                                        End If
                                    End If
                                    GoTo lblnext
                                End If
                                If ds.Tables(3).Rows.Count > 0 Then
                                    If dblWeight > Val(ds.Tables(3).Rows(0)("Capacity")) Then
                                        If strContainer6 = "" Then
                                            strContainer6 = Trim(row.Cell(6).Value.ToString())
                                        Else
                                            If Not InStr(strContainer6, Trim(row.Cell(6).Value.ToString())) > 0 Then
                                                strContainer6 += "," + Trim(row.Cell(6).Value.ToString())
                                            End If
                                        End If
                                        GoTo lblnext
                                    End If
                                End If

                                If Trim(ds.Tables(4).Rows(0)("LocationID")) > 0 Then

                                Else
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Location not found');", True)
                                    Exit Sub
                                End If

                                If Not ds.Tables(0).Rows.Count > 0 Or ds.Tables(1).Rows.Count > 0 Or ds.Tables(2).Rows.Count > 0 Or Not ds.Tables(3).Rows.Count > 0 Then
                                    GoTo lblnext
                                End If
                                Dim dtRow As DataRow = dtDepoContainer.NewRow
                                dtRow.Item("ContainerNo") = row.Cell(1).Value.ToString()
                                dtRow.Item("EntryID") = Val(ds.Tables(0).Rows(0)("entryID"))
                                dtRow.Item("StuffPkgs") = row.Cell(2).Value.ToString()
                                dtRow.Item("StuffWeight") = row.Cell(3).Value.ToString()
                                dtRow.Item("StuffDate") = Convert.ToDateTime(row.Cell(4).Value).ToString("yyyy-MM-dd HH:mm")
                                dtRow.Item("TrainNo") = row.Cell(5).Value.ToString()
                                dtRow.Item("TrainID") = Val(ds.Tables(3).Rows(0)("TrainID"))
                                dtRow.Item("WagonNo") = row.Cell(6).Value.ToString()
                                dtRow.Item("InvoiceNo") = row.Cell(7).Value.ToString()
                                dtRow.Item("DONo") = row.Cell(8).Value.ToString()
                                dtRow.Item("SealNo") = row.Cell(9).Value.ToString()
                                dtRow.Item("Remark") = row.Cell(10).Value.ToString()
                                dtRow.Item("Location") = row.Cell(11).Value.ToString()
                                dtRow.Item("ScrapPks") = row.Cell(12).Value.ToString()
                                dtDepoContainer.Rows.Add(dtRow)
                            Else
                                firstRow = False
                            End If
                        End If
lblnext:
                    Next
                    If Not (strContainer1 = "" And strContainer2 = "" And strContainer3 = "" And strContainer4 = "" And strContainer5 = "" And strContainer6 = "") Then
                        If Not strContainer1 = "" Then
                            strContainer += "Container(s) not found -" & strContainer1 & "\n"
                        End If
                        If Not strContainer2 = "" Then
                            strContainer += "Pkgs invalid for -" & strContainer2 & "\n"
                        End If
                        If Not strContainer3 = "" Then
                            strContainer += "Weight Invalid for -" & strContainer3 & "\n"
                        End If
                        If Not strContainer4 = "" Then
                            strContainer += "Wagon no not found for -" & strContainer4 & "\n"
                        End If
                        If Not strContainer5 = "" Then
                            strContainer += "Stuff Date Invalid for -" & strContainer5 & ""
                        End If
                        If Not strContainer6 = "" Then
                            strContainer += "Wagon Capacity exceeded for -" & strContainer6 & ""
                        End If
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('" & strContainer & "');", True)
                    End If
                End Using
                File.Delete(FilePath)
            End If

            grdOutDets.DataSource = Nothing
            grdOutDets.DataSource = dtDepoContainer
            grdOutDets.DataBind()
            Session("table_DepoContainer") = dtDepoContainer
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If FileUpload1.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)

                Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
                lblfilename.Text = "File Name: " + FileName

                Dim FilePath As String = Server.MapPath(FolderPath + FileName)
                If Not ((Extension = ".xls") Or (Extension = ".xlsx")) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Only .xls or .xlsx files are required!');", True)
                    btnUpload.Text = "Import"
                    btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
                    Exit Sub
                End If
                FileUpload1.SaveAs(FilePath)
                Upload(sender, e, FilePath)
                'Import_To_Grid(FilePath, Extension)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please choose file!');", True)
                btnUpload.Text = "Import"
                btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
                Exit Sub
            End If
        Catch ex As Exception
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub Control_Clear()
        sub_CreateTable()
        btnSave.Text = "Save"
        btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
        btnUpload.Text = "Import"
        btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
    End Sub
End Class
