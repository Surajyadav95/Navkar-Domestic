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
        'If Session("UserIDPRE_Bond") Is Nothing Then
        '    Session("UserIDPRE_Bond") = Request.Cookies("UserIDPRE_Bond").Value
        '    'Session("Dept") = Request.Cookies("Dept").Value
        '    Session("UserNamePRE_Bond") = Request.Cookies("UserNamePRE_Bond").Value
        '    ''Session("PROFILEURL") = Request.Cookies("PROFILEURL").Value
        '    'Session("Location") = Request.Cookies("Location").Value
        '    ''Session("LOcationId") = Request.Cookies("LOcationId").Value
        '    'Session("ID") = Response.Cookies("ID").Value
        '    'Session("CompID") = Response.Cookies("CompID").Value
        '    'Session("Workyear") = Response.Cookies("Workyear").Value
        'End If
        If Not IsPostBack Then
            txtOutDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            sub_CreateTable()
        End If
    End Sub
    Private Sub sub_CreateTable()
        Dim dtDepoContainer As New DataTable

        dtDepoContainer.Columns.Clear()

        Session("table_DepoContainer") = ""

        dtDepoContainer.Columns.Add("TrainNo")
        dtDepoContainer.Columns.Add("TrainID")
        dtDepoContainer.Columns.Add("WagonNo")
        dtDepoContainer.Columns.Add("ContainerNo")
        dtDepoContainer.Columns.Add("EntryID")
        dtDepoContainer.Columns.Add("Size")
        dtDepoContainer.Columns.Add("Type")
        dtDepoContainer.Columns.Add("TypeID")
        dtDepoContainer.Columns.Add("GrossWeight")
        dtDepoContainer.Columns.Add("TareWeight")
        dtDepoContainer.Columns.Add("NetWeight")
        dtDepoContainer.Columns.Add("PartyName")
        dtDepoContainer.Columns.Add("PartyID")
        dtDepoContainer.Columns.Add("Location")
        dtDepoContainer.Columns.Add("LocationID")
        dtDepoContainer.Columns.Add("SealNo")
        dtDepoContainer.Columns.Add("Pkgs")
        dtDepoContainer.Columns.Add("Weight")
        dtDepoContainer.Columns.Add("Commodity")
        dtDepoContainer.Columns.Add("CommodityID")
        dtDepoContainer.Columns.Add("LEType")

        Dim dtRow2 As DataRow = dtDepoContainer.NewRow

        grdOutDets.DataSource = Nothing
        grdOutDets.DataSource = dtDepoContainer
        grdOutDets.DataBind()
        Session("table_DepoContainer") = dtDepoContainer

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
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Import Container Details first');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If

            Dim param As New SqlParameter()
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@UserID", Session("UserId_DomCFS"))
            cmd.Parameters.AddWithValue("@OutDate", Convert.ToDateTime(Trim(txtOutDate.Text & "")).ToString("yyyy-MM-dd HH:mm"))
            param.ParameterName = "@PT_GATEOUTBYTRAIN"
            param.Value = dtDepoContainer
            param.TypeName = "PT_GATEOUTBYTRAIN"
            param.SqlDbType = SqlDbType.Structured
            cmd.Parameters.Add(param)
            Dim strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("SqlConnString_Domestic").ConnectionString
            Dim con As New SqlConnection(strConnString)
            cmd.CommandText = "USP_INSERT_GATE_OUT_BY_TRAIN"
            cmd.Connection = con
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            Dim dtMNR As New DataTable
            Try
                con.Open()
                da.Fill(dtMNR)
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                con.Close()
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
            strSql += "Select '' [Train No],'' [Wagon No],'' [Container No],'' [Size],'' [Gross Weight],'' [Tare Weight],'' [Net Weight],'' [Destination],'' [Seal No],'' [Pkgs],'' [Weight],'' [Type(L/E)]"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Gate Out By Train")
                    'With wb.Worksheets(0)
                    '    .Column(4).Style.DateFormat.Format = "yyyy-MM-dd"
                    'End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=GateOutByTrain.xlsx")
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
            Dim strContainer7 As String = ""
            Dim strContainer8 As String = ""
            Dim strContainer9 As String = ""
            Dim strContainer10 As String = ""

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
                        If Not Trim(row.Cell(3).Value.ToString()) = "" Then
                            If Not firstRow Then
                                If Val(row.Cell(4).Value.ToString()) = 20 Then
                                    intRows += 1
                                ElseIf Val(row.Cell(4).Value.ToString()) = 40 Then
                                    intRows += 2
                                ElseIf Val(row.Cell(4).Value.ToString()) = 45 Then
                                    intRows += 2
                                End If
                            Else
                                firstRow = False
                            End If
                        End If
                    Next
                    If intRows > 90 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container Count should not be greater that 90 Teus');", True)
                        Exit Sub
                    End If
                    firstRow = True
                    For Each row As IXLRow In workSheet.Rows()
                        Dim dblDuplicate As Double = 0, dblWeight As Double = 0, dblDuplicateTrain As Double = 0
                        If Not Trim(row.Cell(1).Value.ToString()) = "" Then
                            If Not firstRow Then
                                For i = 0 To dtDepoContainer.Rows.Count - 1
                                    If Trim(dtDepoContainer.Rows(i)("ContainerNo")) = Trim(row.Cell(3).Value.ToString()) Then
                                        dblDuplicate += 1
                                    End If
                                    If Trim(dtDepoContainer.Rows(i)("WagonNo")) = Trim(row.Cell(2).Value.ToString()) Then
                                        dblWeight += Val(dtDepoContainer.Rows(i)("Weight"))
                                    End If
                                    If Trim(dtDepoContainer.Rows(i)("TrainNo")) <> Trim(row.Cell(1).Value.ToString()) Then
                                        dblDuplicateTrain += 1
                                    End If
                                Next
                                dblWeight += Val(row.Cell(11).Value)
                                If dblDuplicate > 0 Then
                                    GoTo lblnext
                                End If
                                If dblDuplicateTrain > 0 Then
                                    GoTo lblnext
                                End If
                                strSql = ""
                                strSql += "USP_VALIDATION_FOR_GATE_OUT_BY_TRAIN '" & Trim(row.Cell(1).Value.ToString()) & "','" & Trim(row.Cell(2).Value.ToString()) & "',"
                                strSql += "'" & Trim(row.Cell(3).Value.ToString()) & "','" & Trim(row.Cell(8).Value.ToString()) & "'"
                                'strSql += ",'" & Trim(row.Cell(10).Value.ToString()) & "','" & Trim(row.Cell(14).Value.ToString()) & "'"
                                ds = db.sub_GetDataSets(strSql)
                                If Trim(row.Cell(12).Value.ToString()) <> "L" And Trim(row.Cell(12).Value.ToString()) <> "E" Then
                                    If strContainer8 = "" Then
                                        strContainer8 = Trim(row.Cell(3).Value.ToString())
                                    Else
                                        If Not InStr(strContainer8, Trim(row.Cell(3).Value.ToString())) > 0 Then
                                            strContainer8 += "," + Trim(row.Cell(3).Value.ToString())
                                        End If
                                    End If
                                    GoTo lblnext
                                End If
                                If ds.Tables(0).Rows.Count > 0 Then
                                    If dblWeight > Val(ds.Tables(0).Rows(0)("Capacity")) Then
                                        If strContainer1 = "" Then
                                            strContainer1 = Trim(row.Cell(2).Value.ToString())
                                        Else
                                            If Not InStr(strContainer1, Trim(row.Cell(2).Value.ToString())) > 0 Then
                                                strContainer1 += "," + Trim(row.Cell(2).Value.ToString())
                                            End If
                                        End If
                                        GoTo lblnext
                                    End If
                                End If
                                If Not ds.Tables(0).Rows.Count > 0 Then
                                    If strContainer2 = "" Then
                                        strContainer2 = Trim(row.Cell(2).Value.ToString())
                                    Else
                                        If Not InStr(strContainer2, Trim(row.Cell(2).Value.ToString())) > 0 Then
                                            strContainer2 += "," + Trim(row.Cell(2).Value.ToString())
                                        End If
                                    End If
                                    GoTo lblnext
                                End If
                                'If Not ds.Tables(1).Rows.Count > 0 And Trim(row.Cell(12).Value.ToString()) = "L" Then
                                '    If Not ds.Tables(4).Rows.Count > 0 And Trim(row.Cell(12).Value.ToString()) = "L" Then
                                '        If strContainer3 = "" Then
                                '            strContainer3 = Trim(row.Cell(3).Value.ToString())
                                '        Else
                                '            If Not InStr(strContainer3, Trim(row.Cell(3).Value.ToString())) > 0 Then
                                '                strContainer3 += "," + Trim(row.Cell(3).Value.ToString())
                                '            End If
                                '        End If
                                '        GoTo lblnext
                                '    End If
                                'End If

                                'If Not ds.Tables(3).Rows.Count > 0 And Trim(row.Cell(12).Value.ToString()) = "E" Then
                                '    If Not ds.Tables(2).Rows.Count > 0 And Trim(row.Cell(12).Value.ToString()) = "E" Then
                                '        If strContainer3 = "" Then
                                '            strContainer3 = Trim(row.Cell(3).Value.ToString())
                                '        Else
                                '            If Not InStr(strContainer3, Trim(row.Cell(3).Value.ToString())) > 0 Then
                                '                strContainer3 += "," + Trim(row.Cell(3).Value.ToString())
                                '            End If
                                '        End If
                                '        GoTo lblnext
                                '    End If
                                'End If
                                If Not ds.Tables(5).Rows.Count > 0 Then
                                    If strContainer5 = "" Then
                                        strContainer5 = Trim(row.Cell(8).Value.ToString())
                                    Else
                                        If Not InStr(strContainer5, Trim(row.Cell(8).Value.ToString())) > 0 Then
                                            strContainer5 += "," + Trim(row.Cell(8).Value.ToString())
                                        End If
                                    End If
                                    GoTo lblnext
                                End If

                                Dim dtRow As DataRow = dtDepoContainer.NewRow
                                dtRow.Item("TrainNo") = Trim(row.Cell(1).Value.ToString() & "")
                                dtRow.Item("TrainID") = Val(ds.Tables(0).Rows(0)("TrainID"))
                                dtRow.Item("WagonNo") = Trim(row.Cell(2).Value.ToString() & "")
                                dtRow.Item("ContainerNo") = Trim(row.Cell(3).Value.ToString() & "")
                                If Trim(row.Cell(12).Value.ToString()) = "L" Then
                                    If ds.Tables(1).Rows.Count > 0 Then
                                        dtRow.Item("EntryID") = Val(ds.Tables(1).Rows(0)("EntryID"))
                                    Else
                                        If ds.Tables(4).Rows.Count > 0 Then
                                            dtRow.Item("EntryID") = Val(ds.Tables(4).Rows(0)("EntryID"))
                                        End If
                                    End If
                                ElseIf Trim(row.Cell(12).Value.ToString()) = "E" Then
                                    If ds.Tables(3).Rows.Count > 0 Then
                                        dtRow.Item("EntryID") = Val(ds.Tables(3).Rows(0)("EntryID"))
                                    Else
                                        If ds.Tables(2).Rows.Count > 0 Then
                                            dtRow.Item("EntryID") = Val(ds.Tables(2).Rows(0)("EntryID"))
                                        End If
                                    End If
                                End If
                                dtRow.Item("Size") = Trim(row.Cell(4).Value.ToString() & "")
                                If ds.Tables(6).Rows.Count > 0 Then
                                    dtRow.Item("Type") = Trim(ds.Tables(6).Rows(0)("ContainerType"))
                                    dtRow.Item("TypeID") = Val(ds.Tables(6).Rows(0)("TypeID"))
                                End If
                                If ds.Tables(7).Rows.Count > 0 Then
                                    dtRow.Item("Type") = Trim(ds.Tables(7).Rows(0)("ContainerType"))
                                    dtRow.Item("TypeID") = Val(ds.Tables(7).Rows(0)("TypeID"))
                                End If
                                dtRow.Item("GrossWeight") = Val(row.Cell(5).Value.ToString() & "")
                                dtRow.Item("TareWeight") = Val(row.Cell(6).Value.ToString() & "")
                                dtRow.Item("NetWeight") = Val(row.Cell(7).Value.ToString() & "")
                                If Trim(row.Cell(12).Value.ToString()) = "L" Then
                                    If ds.Tables(1).Rows.Count > 0 Then
                                        If ds.Tables(6).Rows.Count > 0 Then
                                            dtRow.Item("PartyName") = Trim(ds.Tables(6).Rows(0)("AGName"))
                                            dtRow.Item("PartyID") = Val(ds.Tables(6).Rows(0)("CustomerID"))
                                        End If
                                    Else
                                        If ds.Tables(7).Rows.Count > 0 Then
                                            dtRow.Item("PartyName") = Trim(ds.Tables(7).Rows(0)("AGName"))
                                            dtRow.Item("PartyID") = Val(ds.Tables(7).Rows(0)("CustID"))
                                        End If
                                    End If
                                ElseIf Trim(row.Cell(12).Value.ToString()) = "E" Then
                                    If ds.Tables(3).Rows.Count > 0 Then
                                        If ds.Tables(7).Rows.Count > 0 Then
                                            dtRow.Item("PartyName") = Trim(ds.Tables(7).Rows(0)("AGName"))
                                            dtRow.Item("PartyID") = Val(ds.Tables(7).Rows(0)("CustID"))
                                        End If
                                    Else
                                        If ds.Tables(6).Rows.Count > 0 Then
                                            dtRow.Item("PartyName") = Trim(ds.Tables(6).Rows(0)("AGName"))
                                            dtRow.Item("PartyID") = Val(ds.Tables(6).Rows(0)("CustomerID"))
                                        End If
                                    End If
                                End If
                                dtRow.Item("Location") = Trim(row.Cell(8).Value.ToString() & "")
                                dtRow.Item("LocationID") = Val(ds.Tables(5).Rows(0)("LocationID"))
                                dtRow.Item("SealNo") = Trim(row.Cell(9).Value.ToString() & "")
                                dtRow.Item("Pkgs") = Val(row.Cell(10).Value.ToString() & "")
                                dtRow.Item("Weight") = Val(row.Cell(11).Value.ToString() & "")
                                If Trim(row.Cell(12).Value.ToString()) = "L" Then
                                    If ds.Tables(6).Rows.Count > 0 Then
                                        dtRow.Item("Commodity") = Trim(ds.Tables(6).Rows(0)("Commodity"))
                                        dtRow.Item("CommodityID") = Val(ds.Tables(6).Rows(0)("CommodityID"))
                                    End If
                                Else
                                    dtRow.Item("Commodity") = ""
                                    dtRow.Item("CommodityID") = 0
                                End If
                                dtRow.Item("LEType") = Trim(row.Cell(12).Value.ToString() & "")

                                dtDepoContainer.Rows.Add(dtRow)
                            Else
                                firstRow = False
                            End If
                        End If
lblnext:
                    Next
                    If Not (strContainer1 = "" And strContainer2 = "" And strContainer3 = "" And strContainer5 = "" And strContainer8 = "") Then
                        If Not strContainer1 = "" Then
                            strContainer += "Wagon Capacity exceeded for -" & strContainer1 & "\n"
                        End If
                        If Not strContainer2 = "" Then
                            strContainer += "Wagon no Not found -" & strContainer2 & "\n"
                        End If
                        If Not strContainer3 = "" Then
                            strContainer += "Containers already out -" & strContainer3 & "\n"
                        End If
                        If Not strContainer8 = "" Then
                            strContainer += "Type(Loaded or Empty) Invalid for -" & strContainer8 & ""
                        End If
                        If Not strContainer5 = "" Then
                            strContainer += "Location not found -" & strContainer5 & ""
                        End If
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('" & strContainer & "');", True)
                    End If
                End Using
                File.Delete(FilePath)
            End If

            grdOutDets.DataSource = Nothing
            grdOutDets.DataSource = dtDepoContainer
            grdOutDets.DataBind()
            Dim dbl20 As Double = 0, dbl40 As Double = 0, dbl45 As Double = 0
            For i = 0 To dtDepoContainer.Rows.Count - 1
                If dtDepoContainer.Rows(i)("Size") = "20" Then
                    dbl20 += 1
                ElseIf dtDepoContainer.Rows(i)("Size") = "40" Then
                    dbl40 += 1
                ElseIf dtDepoContainer.Rows(i)("Size") = "45" Then
                    dbl45 += 1
                End If
            Next
            lbl20.Text = dbl20
            lbl40.Text = dbl40
            lbl45.Text = dbl45
            lblTEUS.Text = Val(dbl20) + Val(dbl40) * 2 + Val(dbl45) * 2
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
