Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.IO
Imports ClosedXML.Excel
Imports System.Globalization

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim TariffID, TariffIDView As String
    Dim ed As New clsEncodeDecode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserID") Is Nothing Then
        '    Session("UserID") = Request.Cookies("UserIDPRE_Bond").Value
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
            txtJODate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            sub_CreateTable()
            sub_CreateTable1()
            Fillgrid()
        End If

    End Sub
     
    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function
    Private Sub sub_CreateTable()
        Dim dtDepoContainer As New DataTable

        dtDepoContainer.Columns.Clear()

        Session("table_DepoContainer") = ""

        dtDepoContainer.Columns.Add("SrNo")
        dtDepoContainer.Columns.Add("Mode")
        dtDepoContainer.Columns.Add("DoNo")
        dtDepoContainer.Columns.Add("DoQty")
        dtDepoContainer.Columns.Add("Destination")
        dtDepoContainer.Columns.Add("BatchNo")
        dtDepoContainer.Columns.Add("Location")
        dtDepoContainer.Columns.Add("PFrom")
        dtDepoContainer.Columns.Add("TransportZone")
        dtDepoContainer.Columns.Add("Region")
        dtDepoContainer.Columns.Add("AllocationZone")
        dtDepoContainer.Columns.Add("SRPipeGrade")
        dtDepoContainer.Columns.Add("Thickness")
        dtDepoContainer.Columns.Add("Width")
        dtDepoContainer.Columns.Add("Weight")
        dtDepoContainer.Columns.Add("NetWt")
        dtDepoContainer.Columns.Add("Material")
        dtDepoContainer.Columns.Add("DelAge")
        dtDepoContainer.Columns.Add("Remarks")

        Dim dtRow2 As DataRow = dtDepoContainer.NewRow

        grdOutDets.DataSource = Nothing
        grdOutDets.DataSource = dtDepoContainer
        grdOutDets.DataBind()
        Session("table_DepoContainer") = dtDepoContainer

    End Sub

    Private Sub sub_CreateTable1()
        Dim dtRelianceContainer As New DataTable

        dtRelianceContainer.Columns.Clear()

        Session("table_RelianceContainer") = ""

        dtRelianceContainer.Columns.Add("SrNo")
        dtRelianceContainer.Columns.Add("ContainerNo")
        dtRelianceContainer.Columns.Add("ContainerType")
        dtRelianceContainer.Columns.Add("NoofBags")
        dtRelianceContainer.Columns.Add("Wt")
        dtRelianceContainer.Columns.Add("PRODUCTGRADE")
        dtRelianceContainer.Columns.Add("DESTINATION")
        dtRelianceContainer.Columns.Add("SHIPTOPARTYNAME")
        dtRelianceContainer.Columns.Add("TAXINVOICENO")
        dtRelianceContainer.Columns.Add("EWAYBILLDETAILS")
        dtRelianceContainer.Columns.Add("VALIDUPTO")
        dtRelianceContainer.Columns.Add("Size")
        dtRelianceContainer.Columns.Add("Seal")
        dtRelianceContainer.Columns.Add("Grade")
        dtRelianceContainer.Columns.Add("ContainerTypeID")
        dtRelianceContainer.Columns.Add("LocationID")
        Dim dtRow2 As DataRow = dtRelianceContainer.NewRow

        grdRelianceDets.DataSource = Nothing
        grdRelianceDets.DataSource = dtRelianceContainer
        grdRelianceDets.DataBind()
        Session("table_RelianceContainer") = dtRelianceContainer

    End Sub
    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            If ddlFormat.SelectedValue = "Reliance" Then
                strSql = ""
                strSql += "Select '' [Sr No],'' [Container No],'' [Container Type],'' [No of Bags],'' [WT],'' [PRODUCT GRADE],'' [DESTINATION],'' [SHIP TO PARTY NAME],'' [TAX INVOICE NO],'' [E- WAY BILL DETAILS],'' [VALID UP TO],"
                strSql += "'' [Size],'' [Seal],'' [Grade]"
                dt = db.sub_GetDatatable(strSql)

                If (dt.Rows.Count > 0) Then
                    Using wb As New XLWorkbook()
                        wb.Worksheets.Add(dt, "Reliance Rail Entry")
                        With wb.Worksheets(0)
                            .Column(15).Style.DateFormat.Format = "yyyy-MM-dd"
                        End With
                        Response.Clear()
                        Response.Buffer = True
                        Response.Charset = ""
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                        Response.AddHeader("content-disposition", "attachment;filename=RelianceRailEntryTemplate.xlsx")
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
            Else

                strSql = ""
                strSql += "Select '' [Sr No],'' [Mode of Transport],'' [Destination],'' [Location],'' [Del Batch],'' [DO No],'' [DO Quantity],'' [Thickness],'' [Width/Diameter],'' [Transport Zone],'' [SR Grade/Pipe Grade],"
                strSql += "'' [P From],'' [Region],'' [Allocation Zone],'' [Material Description],'' [Net Wt],'' [Gross Wt],'' [Del Age],'' [Remarks]"
                dt = db.sub_GetDatatable(strSql)

                If (dt.Rows.Count > 0) Then
                    Using wb As New XLWorkbook()
                        wb.Worksheets.Add(dt, "Cargo JO Template")
                        With wb.Worksheets(0)
                            .Column(15).Style.DateFormat.Format = "yyyy-MM-dd"
                        End With
                        Response.Clear()
                        Response.Buffer = True
                        Response.Charset = ""
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                        Response.AddHeader("content-disposition", "attachment;filename=CargoJOSystemTemplate.xlsx")
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
            End If
          
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
    Private Sub Upload(sender As Object, e As EventArgs, FilePath As String)
        Try
            Dim intRows As Integer = 0
            Dim dtDepoContainer As New DataTable
            Dim dtRelianceContainer As New DataTable
            Dim strContainer As String = ""
            Dim strContainer1 As String = ""
            Dim strContainer2 As String = ""
            Dim strContainer3 As String = ""
            Dim strContainer4 As String = ""
            Dim strContainer5 As String = ""
            Dim strContainer6 As String = ""
            Dim strContainer7 As String = ""
            Dim strContainer8 As String = ""
            Dim formats() As String = {"dd-MM-yyyy", "yyyy-MM-dd", "dd/MM/yyyy", "yyyy/MM/dd", "dd-MM-yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "dd/MM/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "dd-MM-yyyy HH:mm:ss tt", "yyyy-MM-dd HH:mm:ss tt", "dd/MM/yyyy HH:mm:ss tt", "yyyy/MM/dd HH:mm:ss tt", "dd-MM-yyyy hh:mm:ss tt", "yyyy-MM-dd hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt", "yyyy/MM/dd hh:mm:ss tt"}
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)
            dtRelianceContainer = DirectCast(Session("table_RelianceContainer"), DataTable)
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
                    'For Each row As IXLRow In workSheet.Rows()
                    '    If Not Trim(row.Cell(2).Value.ToString()) = "" Then
                    '        If Not firstRow Then
                    '            intRows += 1
                    '        Else
                    '            firstRow = False
                    '        End If
                    '    End If
                    'Next
                    'firstRow = True
                    Dim dblSrNo As Double = 0
                    For Each row As IXLRow In workSheet.Rows()
                        Dim dblDuplicate As Double = 0, dblWeight As Double = 0
                        If ddlFormat.SelectedValue = "General" Then
                            If Not Trim(row.Cell(10).Value.ToString()) = "" Then
                                If Not firstRow Then
                                    strSql = ""
                                    strSql += "USP_DUPLICATE_BATCH_VALIDATION_JO '" & Trim(row.Cell(10).Value.ToString() & "") & "'"
                                    ds = db.sub_GetDataSets(strSql)
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        If strContainer1 = "" Then
                                            strContainer1 = Trim(row.Cell(10).Value.ToString())
                                        Else
                                            If Not InStr(strContainer1, Trim(row.Cell(10).Value.ToString())) > 0 Then
                                                strContainer1 += "," + Trim(row.Cell(10).Value.ToString())
                                            End If
                                        End If
                                        GoTo lblnext
                                    End If

                                    dblSrNo += 1
                                    Dim dtRow As DataRow = dtDepoContainer.NewRow

                                    dtRow.Item("SrNo") = dblSrNo
                                    dtRow.Item("Mode") = row.Cell(4).Value.ToString()
                                    dtRow.Item("DoNo") = row.Cell(13).Value.ToString()
                                    dtRow.Item("DoQty") = row.Cell(14).Value.ToString()
                                    dtRow.Item("Destination") = row.Cell(5).Value.ToString()
                                    dtRow.Item("BatchNo") = row.Cell(10).Value.ToString()
                                    dtRow.Item("Location") = row.Cell(6).Value.ToString()
                                    dtRow.Item("PFrom") = row.Cell(38).Value.ToString()
                                    dtRow.Item("TransportZone") = row.Cell(21).Value.ToString()
                                    dtRow.Item("Region") = row.Cell(42).Value.ToString()
                                    dtRow.Item("AllocationZone") = row.Cell(52).Value.ToString()
                                    dtRow.Item("SRPipeGrade") = row.Cell(26).Value.ToString()
                                    dtRow.Item("Thickness") = row.Cell(17).Value.ToString()
                                    dtRow.Item("Width") = row.Cell(18).Value.ToString()
                                    dtRow.Item("Weight") = row.Cell(85).Value.ToString()
                                    dtRow.Item("NetWt") = row.Cell(83).Value.ToString()
                                    dtRow.Item("Material") = row.Cell(60).Value.ToString()
                                    dtRow.Item("DelAge") = row.Cell(86).Value.ToString()
                                    dtRow.Item("Remarks") = row.Cell(7).Value.ToString()

                                    dtDepoContainer.Rows.Add(dtRow)
                                Else
                                    firstRow = False
                                End If
                            End If
                        ElseIf ddlFormat.SelectedValue = "System" Then
                            If Not firstRow Then
                                If Not Trim(row.Cell(5).Value.ToString()) = "" Then
                                    strSql = ""
                                    strSql += "USP_DUPLICATE_BATCH_VALIDATION_JO '" & Trim(row.Cell(5).Value.ToString() & "") & "'"
                                    ds = db.sub_GetDataSets(strSql)
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        If strContainer1 = "" Then
                                            strContainer1 = Trim(row.Cell(5).Value.ToString())
                                        Else
                                            If Not InStr(strContainer1, Trim(row.Cell(5).Value.ToString())) > 0 Then
                                                strContainer1 += "," + Trim(row.Cell(5).Value.ToString())
                                            End If
                                        End If
                                        GoTo lblnext
                                    End If
                                    dblSrNo += 1
                                    Dim dtRow As DataRow = dtDepoContainer.NewRow

                                    dtRow.Item("SrNo") = dblSrNo
                                    dtRow.Item("Mode") = row.Cell(2).Value.ToString()
                                    dtRow.Item("DoNo") = row.Cell(6).Value.ToString()
                                    dtRow.Item("DoQty") = row.Cell(7).Value.ToString()
                                    dtRow.Item("Destination") = row.Cell(3).Value.ToString()
                                    dtRow.Item("BatchNo") = row.Cell(5).Value.ToString()
                                    dtRow.Item("Location") = row.Cell(4).Value.ToString()
                                    dtRow.Item("PFrom") = row.Cell(12).Value.ToString()
                                    dtRow.Item("TransportZone") = row.Cell(10).Value.ToString()
                                    dtRow.Item("Region") = row.Cell(13).Value.ToString()
                                    dtRow.Item("AllocationZone") = row.Cell(14).Value.ToString()
                                    dtRow.Item("SRPipeGrade") = row.Cell(11).Value.ToString()
                                    dtRow.Item("Thickness") = row.Cell(8).Value.ToString()
                                    dtRow.Item("Width") = row.Cell(9).Value.ToString()
                                    dtRow.Item("Weight") = row.Cell(17).Value.ToString()
                                    dtRow.Item("NetWt") = row.Cell(16).Value.ToString()
                                    dtRow.Item("Material") = row.Cell(15).Value.ToString()
                                    dtRow.Item("DelAge") = row.Cell(18).Value.ToString()
                                    dtRow.Item("Remarks") = row.Cell(19).Value.ToString()

                                    dtDepoContainer.Rows.Add(dtRow)
                                End If
                            Else
                                firstRow = False
                            End If
                        ElseIf ddlFormat.SelectedValue = "Reliance" Then
                            If Not firstRow Then
                                If Not Trim(row.Cell(5).Value.ToString()) = "" Then
                                    strSql = ""
                                    strSql += "USP_Check_Loc_PartName '" & Trim(row.Cell(3).Value.ToString() & "") & "','" & row.Cell(7).Value.ToString() & "'"
                                    ds = db.sub_GetDataSets(strSql)
                                    If Not ds.Tables(0).Rows.Count > 0 Then
                                        If strContainer4 = "" Then
                                            strContainer4 = row.Cell(7).Value.ToString()
                                        Else
                                            If Not InStr(strContainer4, Trim(row.Cell(7).Value.ToString())) > 0 Then
                                                strContainer4 += "," + Trim(row.Cell(7).Value.ToString())
                                            End If
                                        End If
                                        GoTo lblnext
                                    End If
                                    If Not ds.Tables(2).Rows.Count > 0 Then
                                        If strContainer8 = "" Then
                                            strContainer8 = Trim(row.Cell(3).Value.ToString() & "")
                                        Else
                                            If Not InStr(strContainer8, Trim(Trim(row.Cell(3).Value.ToString() & ""))) > 0 Then
                                                strContainer8 += "," + Trim(row.Cell(3).Value.ToString())
                                            End If
                                        End If
                                        GoTo lblnext
                                    End If
                                 
                                    dblSrNo += 1
                                    Dim dtRow As DataRow = dtRelianceContainer.NewRow

                                    dtRow.Item("SrNo") = dblSrNo
                                    dtRow.Item("ContainerNo") = row.Cell(2).Value.ToString()
                                    dtRow.Item("ContainerType") = row.Cell(3).Value.ToString()
                                    dtRow.Item("NoofBags") = row.Cell(4).Value.ToString()
                                    dtRow.Item("Wt") = row.Cell(5).Value.ToString()
                                    dtRow.Item("PRODUCTGRADE") = row.Cell(6).Value.ToString()
                                    dtRow.Item("DESTINATION") = row.Cell(7).Value.ToString()
                                    dtRow.Item("SHIPTOPARTYNAME") = row.Cell(8).Value.ToString()
                                    dtRow.Item("TAXINVOICENO") = row.Cell(9).Value.ToString()
                                    dtRow.Item("EWAYBILLDETAILS") = row.Cell(10).Value.ToString()
                                    dtRow.Item("VALIDUPTO") = row.Cell(11).Value.ToString()
                                    dtRow.Item("Size") = row.Cell(12).Value.ToString()
                                    dtRow.Item("Seal") = row.Cell(13).Value.ToString()
                                    dtRow.Item("Grade") = row.Cell(14).Value.ToString()
                                    dtRow.Item("ContainerTypeID") = Val(ds.Tables(2).Rows(0)("ContainerTypeID"))
                                    dtRow.Item("LocationID") = Val(ds.Tables(0).Rows(0)("LocationID"))
                                    dtRelianceContainer.Rows.Add(dtRow)
                                End If
                            Else
                                firstRow = False
                            End If
                        End If

lblnext:
                    Next
                    If Not (strContainer1 = "" And strContainer4 = "" And strContainer8 = "") Then
                        If Not strContainer1 = "" Then
                            strContainer += "Batch No already In -" & strContainer1 & "\n"
                        End If
                        'If Not strContainer2 = "" Then
                        '    strContainer += "Customer not found -" & strContainer2 & "\n"
                        'End If
                        'If Not strContainer3 = "" Then
                        '    strContainer += "Commodity not found -" & strContainer3 & "\n"
                        'End If
                        If Not strContainer4 = "" Then
                            strContainer += "Source Location not found -" & strContainer4 & "\n"
                        End If
                        'If Not strContainer5 = "" Then
                        '    strContainer += "Validity Date Invalid for -" & strContainer5 & ""
                        'End If
                        'If Not strContainer6 = "" Then
                        '    strContainer += "Wagon Capacity exceeded for -" & strContainer6 & ""
                        'End If
                        'If Not strContainer7 = "" Then
                        '    strContainer += "Validity Date must be greater than today for -" & strContainer7 & ""
                        'End If
                        If Not strContainer8 = "" Then
                            strContainer += "Container Type not found -" & strContainer8 & "\n"
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

            grdRelianceDets.DataSource = Nothing
            grdRelianceDets.DataSource = dtRelianceContainer
            grdRelianceDets.DataBind()
            Session("table_RelianceContainer") = dtRelianceContainer
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Fillgrid()

       
        ds = db.sub_GetDataSets("usp_cargo_jo_fill")
        If (ds.Tables(0).Rows.Count > 0) Then
            ddlCustomer.DataSource = ds.Tables(0)
            ddlCustomer.DataTextField = "AGName"
            ddlCustomer.DataValueField = "AGID"
            ddlCustomer.DataBind()
            ddlCustomer.Items.Insert(0, New ListItem("--Select--", 0))

            ddlCommodity.DataSource = ds.Tables(1)
            ddlCommodity.DataTextField = "Commodity"
            ddlCommodity.DataValueField = "id"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, New ListItem("--Select--", 0))

            ddlFrom.DataSource = ds.Tables(2)
            ddlFrom.DataTextField = "location"
            ddlFrom.DataValueField = "LocationID"
            ddlFrom.DataBind()
            ddlFrom.Items.Insert(0, New ListItem("--Select--", 0))

            ddlto.DataSource = ds.Tables(3)
            ddlto.DataTextField = "location"
            ddlto.DataValueField = "LocationID"
            ddlto.DataBind()
            ddlto.Items.Insert(0, New ListItem("--Select--", 0))

            ddlLocation.DataSource = ds.Tables(3)
            ddlLocation.DataTextField = "location"
            ddlLocation.DataValueField = "LocationID"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        'up_grid.Update()
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_INSERT_INTO_DOMESTIC_JOBORDERM_Cargo_JO '" & Convert.ToDateTime(txtJODate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Val(ddlCustomer.SelectedValue) & "','" & Session("UserId_DomCFS") & "','" & Val(ddlLocation.SelectedValue) & "','" & Trim(txtRRNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)

            If dt.Rows.Count > 0 Then
                Dim strJONo As String = Val(dt.Rows(0)(0))

                For Each row In grdOutDets.Rows
                    strSql = ""
                    strSql += "USP_INSERT_INTO_DOMESTIC_JOBORDERD_Cargo_Jo '" & strJONo & "','" & Val(ddlFrom.SelectedValue) & "','" & Val(ddlto.SelectedValue) & "','" & Trim(txtPKGS.Text & "") & "','" & Trim(txtweight.Text & "") & "',"
                    strSql += "'" & Trim(CType(row.FindControl("lblDoNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblBatchNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblWeight"), Label).Text & "") & "',"
                    strSql += "'" & Trim(CType(row.FindControl("lblMaterial"), Label).Text & "") & "','" & Val(ddlCommodity.SelectedValue) & "','" & Trim(CType(row.FindControl("lblDoQty"), Label).Text & "") & "',"
                    strSql += "'" & Trim(ddlFrom.SelectedValue & "") & "','" & Trim(ddlto.SelectedValue & "") & "','" & Trim(CType(row.FindControl("lblPFrom"), Label).Text & "") & "',"
                    strSql += "'" & Trim(CType(row.FindControl("lblTransportZone"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblRegion"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblAllocationZone"), Label).Text & "") & "',"
                    strSql += "'" & Trim(CType(row.FindControl("lblSRPipeGrade"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblThickness"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblWidth"), Label).Text & "") & "',"
                    strSql += "'" & Trim(CType(row.FindControl("lblNetWt"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblMode"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblDelAge"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblRemarks"), Label).Text & "") & "'"
                   
                    db.sub_ExecuteNonQuery(strSql)
                Next
                'Clear()
                lblSession.Text = "Record saved successfully for JO No " & strJONo & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            End If

            If dt.Rows.Count > 0 Then
                Dim strJONo As String = Val(dt.Rows(0)(0))

                For Each row In grdRelianceDets.Rows
                    strSql = ""
                    strSql += "USP_INSERT_INTO_DOMESTIC_JOBORDERD_Reliance '" & strJONo & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblContainerTypeID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblNoofBags"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblWt"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblPRODUCTGRADE"), Label).Text & "") & "',"
                    strSql += "'" & Trim(CType(row.FindControl("lblLocationID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblSHIPTOPARTYNAME"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblTAXINVOICENO"), Label).Text & "") & "',"
                    strSql += "'" & Trim(CType(row.FindControl("lblEWAYBILLDETAILS"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblVALIDUPTO"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblSize"), Label).Text & "") & "',"
                    strSql += "'" & Trim(CType(row.FindControl("lblSeal"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblGrade"), Label).Text & "") & "'"
                    
                    db.sub_ExecuteNonQuery(strSql)
                Next
                'Clear()
                lblSession.Text = "Record saved successfully for JO No " & strJONo & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

     

    Protected Sub ddlFormat_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlFormat.SelectedValue = "Reliance" Then
            divGenSys.Attributes.Add("style", "display:none")
            divReliance.Attributes.Add("style", "display:block")
        Else
            divGenSys.Attributes.Add("style", "display:block")
            divReliance.Attributes.Add("style", "display:none")
        End If
    End Sub
End Class
