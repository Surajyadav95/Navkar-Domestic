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
            sub_CreateTable()
        End If
    End Sub
    Private Sub sub_CreateTable()
        Dim dtDepoContainer As New DataTable

        dtDepoContainer.Columns.Clear()

        Session("table_DepoContainer") = ""

        dtDepoContainer.Columns.Add("SrNo")
        dtDepoContainer.Columns.Add("WagonNo")
        dtDepoContainer.Columns.Add("WagonType")
        dtDepoContainer.Columns.Add("Capacity")       

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
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Import Wagon Details first');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If
            strSql = ""
            strSql += "USP_DOMESTIC_TRAIN_MASTER_VALIDATION_SAVE '" & Replace(Trim(txtTrainNo.Text & ""), "'", "''") & "','" & Replace(Trim(txtRakeNo.Text & ""), "'", "''") & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Train No Already exists.');", True)
                txtTrainNo.Text = ""
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Rake No Already exists.');", True)
                txtRakeNo.Text = ""
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If
            Dim param As New SqlParameter()
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@UserID", Session("UserId_DomCFS"))
            cmd.Parameters.AddWithValue("@TrainNo", Replace(Trim(txtTrainNo.Text & ""), "'", "''"))
            cmd.Parameters.AddWithValue("@RakeNo", Replace(Trim(txtRakeNo.Text & ""), "'", "''"))

            param.ParameterName = "@PT_DOMESTICTRAINMASTER"
            param.Value = dtDepoContainer
            param.TypeName = "PT_DOMESTICTRAINMASTER"
            param.SqlDbType = SqlDbType.Structured
            cmd.Parameters.Add(param)
            Dim strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("SqlConnString_Domestic").ConnectionString
            Dim con As New SqlConnection(strConnString)
            cmd.CommandText = "USP_INSERT_INTO_DOMESTIC_TRAIN_MASTER"
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
            strSql += "Select '' [Sr No],'' [Wagon No],'' [Wagon Type],'' [Capacity]"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Train Master Template")
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=TrainMasterTemplate.xlsx")
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
                        If Not Trim(row.Cell(2).Value.ToString()) = "" Then
                            If Not firstRow Then
                                intRows += 1
                            Else
                                firstRow = False
                            End If
                        End If
                    Next
                    firstRow = True
                    If intRows > 60 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Wagons should not be greater than 60');", True)
                        Exit Sub
                    End If
                    intRows = 0
                    For Each row As IXLRow In workSheet.Rows()
                        Dim dblDuplicate As Double = 0
                        If Not Trim(row.Cell(2).Value.ToString()) = "" Then
                            If Not firstRow Then
                                For i = 0 To dtDepoContainer.Rows.Count - 1
                                    If Trim(dtDepoContainer.Rows(i)("WagonNo")) = Trim(row.Cell(2).Value.ToString()) Then
                                        dblDuplicate += 1
                                    End If
                                Next
                                If dblDuplicate > 0 Then
                                    GoTo lblnext
                                End If
                                Dim dtRow As DataRow = dtDepoContainer.NewRow
                                intRows += 1
                                dtRow.Item("SrNo") = intRows
                                dtRow.Item("WagonNo") = row.Cell(2).Value.ToString()
                                dtRow.Item("WagonType") = row.Cell(3).Value.ToString()
                                dtRow.Item("Capacity") = row.Cell(4).Value.ToString()
                                dtDepoContainer.Rows.Add(dtRow)
                            Else
                                firstRow = False
                            End If
                        End If
lblnext:
                    Next
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
