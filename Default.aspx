Option Explicit
Dim dblGroup1Amt        As Double
Dim dblGroup2Amt        As Double
Dim dblT1Amt            As Double
Dim dblT2Amt            As Double
Dim dblSTaxOnAmount         As Double
Dim dblnetamount            As Double
Dim dblNetAmount_IND        As Double
Dim dblNetPaid              As Double

Private Sub cmbassesstype_LostFocus()
    Call sub_Filltariffs
End Sub

Private Sub cmbdeltype_LostFocus()
    Call sub_Filltariffs
End Sub

Private Sub cmbheader_KeyDown(KeyCode As Integer, Shift As Integer)
     If KeyCode = vbKeyReturn Then
       If txtper.Visible = True Then
            txtper.SetFocus
       Else
            txtadditionalamt.SetFocus
        End If
    End If
    If KeyCode = vbKeyInsert Then
        Load dlgBondAccM
        dlgBondAccM.Show vbModal
        Call sub_fillAccount
    End If
End Sub

Private Sub cmbtariff_Change()
    Dim objRSFetch      As ADODB.Recordset
    
    Set objRSFetch = New ADODB.Recordset
    objRSFetch.Open "SELECT * FROM Bond_TariffMaster WHERE TariffID='" & Trim(cmbtariff.Text) & "'", objDBConnection, adOpenStatic, adLockReadOnly
    If Not objRSFetch.EOF Then
        lbltariffdesc.Caption = Trim(objRSFetch.Fields("TariffDescription"))
    End If
    
End Sub

Private Sub cmbtariff_Click()
    Call cmbtariff_Change
End Sub

Private Sub cmbtariff_KeyDown(KeyCode As Integer, Shift As Integer)
    If KeyCode = vbKeyReturn Then
        cmdcalc.SetFocus
    End If
End Sub

Private Sub cmdadd_Click()
 On Error GoTo errorhandler
 
    If Trim(txtnocno.Text) = "" Then
        MsgBox "NOC no cannot be left blank", vbCritical
        txtnocno.SetFocus
        Exit Sub
    End If
    
    Dim objRSCheck  As ADODB.Recordset
    
    Set objRSCheck = New ADODB.Recordset
    objRSCheck.Open "SELECT  *  FROM bond_assessM WHERE NOCNo='" & Trim(txtnocno.Text) & "'  and IsCancel=0 AND Deliverytype='N'", objDBConnection, adOpenStatic, adLockReadOnly
    If Not objRSCheck.EOF Then
        MsgBox "assessment " & Val(objRSCheck.Fields("Assessno")) & " already exists for noc no. " & txtnocno.Text, vbCritical
        txtnocno.Text = ""
        txtnocno.SetFocus
        Exit Sub
    End If
    Call sub_showNOCDets
     Call sub_Filltariffs
errorhandler:
    If Err Then MsgBox Err.Description
End Sub

Private Sub cmdcalc_Click()
    On Error GoTo errorhandler
    
    Dim intChargeCounter        As Integer
    Dim intContCounter          As Integer
    
    Dim blChargesFound       As Boolean
    
    Dim objRSFetch              As ADODB.Recordset
    Dim objRSSlab               As ADODB.Recordset
    
    Dim objRSTariff             As ADODB.Recordset
    
    Dim dblCalcAmount           As Double
        
    Dim intRow                  As Integer
    Dim intHoldCount            As Integer
                        
'    If Format(dtpvalidupto.Value, "yyyyMMddHHmmss") < Format(dtpassessdate.Value, "yyyyMMddHHmmss") Then
'        MsgBox "valid upto date should not be less than assessment date.", vbCritical
'       ' dtpvalidupto.SetFocus
'        Exit Sub
'    End If
   
    If Trim(lblpin.Caption) = "" Then
        MsgBox "GSTIn Number cannot be left blank", vbCritical
        txtgstin.SetFocus
        Exit Sub
    End If
    
    Call Sub_SGTRate(lblpin.Caption)
   
    If cmbtariff.Text = "" Then
        MsgBox "Please SELECT the tariff before proceeding.", vbInformation
        cmbtariff.SetFocus
        Exit Sub
    End If
    
    Set objRSFetch = New ADODB.Recordset
    objRSFetch.Open "SELECT TOP 1 * FROM bond_tariffmaster WHERE  " & Format(dtpassessdate.Value, "yyyyMMdd") & " BETWEEN EffectiveFROM and EffectiveUpto and IsActive=1", objDBConnection, adOpenStatic, adLockReadOnly
    If objRSFetch.EOF Then
        MsgBox "Invalid tariff details. Please contact your administrator!", vbCritical
        cmbtariff.SetFocus
        Exit Sub
    End If
                            
    lblinsvalidity.Caption = ""
    
    Call sub_ChargesGridSettings
    
    Call sub_CalcTotals
    
    cmdsave.Enabled = True
    mshfcharges.SetFocus
    
errorhandler:
    If Err Then
        MsgBox Err.Description
    End If
End Sub

Private Sub cmdclear_Click()
    dtpvalidupto.Value = Format(Now, "dd-MMM-yyyy 23:59")
    dtpassessdate.Value = Format(Now, "dd-MMM-yyyy 23:59")
    dtpnocdate.Value = Format(Now, "dd-MMM-yyyy 23:59")
    dtpBEdate.Value = Format(Now, "dd-MMM-yyyy 23:59")
    txtassessno.Text = "NEW"
    txtnocno.Text = ""
    txtBeNo.Text = ""
    txtigm.Text = ""
    txtitem.Text = ""
    txt40.Text = ""
    txt20.Text = ""
    txtcustid.Text = ""
    txtcustname.Text = ""
    txtimpid.Text = ""
    txtchaid.Text = ""
    txtcargotype.Text = ""
    txtConsignee.Text = ""
    txtcha.Text = ""
      txtgstin.Text = ""
    txtgstpartyname.Text = ""
    
    lblpartyid.Caption = ""
    lblpin.Caption = ""
    txtcommdity.Text = ""
    txtqty.Text = ""
    txtunit.Text = ""
    txtarea.Text = ""
    txtarean.Text = ""
    txtgrswt.Text = ""
    txtvalue.Text = ""
    txtduty.Text = ""
    cmbtariff.ListIndex = 0
    dblGroup1Amt = 0
    dblGroup2Amt = 0
    dblT1Amt = 0
    dblT2Amt = 0
    dblSTaxOnAmount = 0
    dblnetamount = 0
    dblNetAmount_IND = 0
    dblNetPaid = 0

    Call sub_ChargesGridSettings
    Call sub_CalcTotals
    cmdsave.Enabled = False
    dblAssessNoFromAssess = 0
    txtnocno.SetFocus
End Sub

Private Sub cmdexit_Click()
    Unload Me
End Sub

Private Sub cmdrmvadch_Click()
     mshfadditional.RowHeight(mshfadditional.Row) = 0
End Sub

Private Sub cmdSave_Click()
    On Error GoTo errorhandler
    
    Dim blChargesFound           As Boolean
    Dim intContCounter              As Integer
    Dim intRowCount                 As Integer
    
    Dim dblAssessNo                 As Double
    
    Dim objRSMaxAssessNo            As ADODB.Recordset
    Dim objRSSave                   As ADODB.Recordset
    
    Dim objRSFetch                  As ADODB.Recordset
    
    Dim dblTempSTax                 As Double
    Dim dblDestuffDate              As Date
    Dim dblSQM                      As Double
    Dim dblDestuffDays              As Double
    
    Dim dblDestuffWeek              As Double
    Dim dblweight                   As Double
    Dim dblSumSGSTAmt                As Double
    Dim dblSumCGSTAmt                As Double
    Dim dblSumIGSTAmt               As Double
    Dim dblSumNetAmtTotal                As Double
    
    Dim strSQL                      As String
    If Trim(lblpin.Caption) = "" Then
        MsgBox "GSTIn Number cannot be left blank", vbCritical
        txtgstin.SetFocus
        Exit Sub
    End If
    
    Call Sub_SGTRate(lblpin.Caption)
    
    If Trim(txtnocno.Text) = "" Then
        MsgBox "NOC no. cannot be left blank.", vbCritical
        txtnocno.SetFocus
        Exit Sub
    End If
    
'    If Format(dtpvalidupto.Value, "yyyyMMddHHmmss") < Format(dtpassessdate.Value, "yyyyMMddHHmmss") Then
'        MsgBox "valid upto date should not be less than assessment date.", vbCritical
'        'dtpvalidupto.SetFocus
'        Exit Sub
'    End If
'
    blChargesFound = False
    For intContCounter = 1 To mshfcharges.Rows - 1
        If Trim(mshfcharges.TextMatrix(intContCounter, 0)) <> "" And mshfcharges.RowHeight(intContCounter) <> 0 Then
            blChargesFound = True
            Exit For
        End If
    Next
    If blChargesFound = False Then
        MsgBox "No details selected for assessment.", vbCritical
        txtigm.SetFocus
        Exit Sub
    End If
    
    Set objRSFetch = New ADODB.Recordset
    objRSFetch.Open "SELECT * FROM bond_tariffdetails WHERE TariffID='" & Trim(cmbtariff.Text) & "'", objDBConnection, adOpenStatic, adLockReadOnly
    If objRSFetch.EOF Then
        MsgBox "Tariff ID: " & Trim(cmbtariff.Text) & " not found in database. Please contact your administrator!", vbCritical
        Exit Sub
    End If
    cmdsave.Enabled = False
    
    Set objRSMaxAssessNo = New ADODB.Recordset
    objRSMaxAssessNo.Open "SELECT MAX(AssessNo) FROM Bond_AssessM WITH(XLOCK) WHERE WorkYear='" & strWorkYear & "'", objDBConnection, adOpenStatic, adLockReadOnly
    
    If IsNull(objRSMaxAssessNo.Fields(0)) = True Then
        dblAssessNo = Format(dtpassessdate.Value, "yy") & Right$(String(6, "0") & Val(Mid(0, 3)) + 1, 6)
    ElseIf IsNull(objRSMaxAssessNo.Fields(0)) = False Then
        dblAssessNo = Val(objRSMaxAssessNo.Fields(0)) + 1
    End If
        
    Set objRSSave = New ADODB.Recordset
    objRSSave.Open "SELECT TOP 1 * FROM Bond_AssessD", objDBConnection, adOpenDynamic, adLockOptimistic
        
    For intRowCount = 1 To mshfcharges.Rows - 1
        If Val(mshfcharges.TextMatrix(intRowCount, 0)) <> 0 Then
            objRSSave.AddNew
            objRSSave.Fields("AssessNo") = dblAssessNo
            objRSSave.Fields("WorkYear") = strWorkYear
            objRSSave.Fields("AccountID") = Val(mshfcharges.TextMatrix(intRowCount, 0))
            objRSSave.Fields("NetAmount") = Format(Val(mshfcharges.TextMatrix(intRowCount, 2)), "0")
            objRSSave.Fields("D_SGST") = Format(Val(mshfcharges.TextMatrix(intRowCount, 2)) * (dblSGST / 100), "0.00")
            objRSSave.Fields("D_CGST") = Format(Val(mshfcharges.TextMatrix(intRowCount, 2)) * (dblCGST / 100), "0.00")
            objRSSave.Fields("D_IGST") = Format(Val(mshfcharges.TextMatrix(intRowCount, 2)) * (dblIGST / 100), "0.00")
            objRSSave.Fields("taxgroupID") = dbltaxgroupid
            objRSSave.Update
        End If
    Next
    
    Dim objRSCheck  As ADODB.Recordset
    Set objRSCheck = New ADODB.Recordset
    objRSCheck.Open "get_sum_charges_bond " & dblAssessNo & ", '" & Trim(strWorkYear) & "'  ", objDBConnection, adOpenStatic, adLockReadOnly
    If Not objRSCheck.EOF Then
            dblSumSGSTAmt = Val(objRSCheck.Fields("SGST"))
            dblSumCGSTAmt = Val(objRSCheck.Fields("CGST"))
            dblSumIGSTAmt = Val(objRSCheck.Fields("IGST"))
            dblSumNetAmtTotal = Val(objRSCheck.Fields("Amount"))
    End If
    
    Set objRSSave = New ADODB.Recordset
    objRSSave.Open "SELECT TOP 1 * FROM Bond_AssessM", objDBConnection, adOpenDynamic, adLockOptimistic
    objRSSave.AddNew
    objRSSave.Fields("AssessNo") = dblAssessNo
    objRSSave.Fields("BillNo") = ""
    objRSSave.Fields("WorkYear") = strWorkYear
    objRSSave.Fields("CHAID") = Val(txtchaid.Text)
    objRSSave.Fields("CustID") = Val(txtcustid.Text)
    objRSSave.Fields("ImporterID") = Val(txtimpid.Text)
    objRSSave.Fields("AssessDate") = Format(dtpassessdate.Value, "dd-MMM-yyyy HH:mm:ss")
    objRSSave.Fields("ValidUptoDate") = Format(dtpvalidupto.Value, "dd-MMM-yyyy 23:59:59")
    objRSSave.Fields("InsuValidUptoDate") = Format(lblinsvalidup.Caption, "dd-MMM-yyyy 23:59:59")
    objRSSave.Fields("NocNo") = Trim(txtnocno.Text)
    objRSSave.Fields("BondType") = Trim(cmbbondtype.Text)
    objRSSave.Fields("DeliveryType") = "N"
    objRSSave.Fields("BOENo") = Trim(txtBeNo.Text)
    objRSSave.Fields("TariffID") = Trim(cmbtariff.Text)
    objRSSave.Fields("IGMNo") = Trim(txtigm.Text)
    objRSSave.Fields("ItemNo") = Trim(txtitem.Text)
    objRSSave.Fields("t20") = Trim(txt20.Text)
    objRSSave.Fields("T40") = Trim(txt40.Text)
    objRSSave.Fields("STaxAmount") = dblSumNetAmtTotal
    objRSSave.Fields("NetTotal") = dblSumNetAmtTotal 'Val(mshfcharges.TextMatrix(mshfcharges.Rows - 6, 2))
    objRSSave.Fields("ServiceTax") = 0 ' Val(mshfcharges.TextMatrix(mshfcharges.Rows - 4, 2))
    objRSSave.Fields("SbCcess") = 0 ' Val(mshfcharges.TextMatrix(mshfcharges.Rows - 3, 2))
    objRSSave.Fields("KKcess") = 0 ' Val(mshfcharges.TextMatrix(mshfcharges.Rows - 2, 2))
    objRSSave.Fields("ECess") = 0
    objRSSave.Fields("HCess") = 0
    objRSSave.Fields("STaxAmount1") = 0
    objRSSave.Fields("NetTotal1") = 0
    objRSSave.Fields("ServiceTax1") = 0
    objRSSave.Fields("ECess1") = 0
    objRSSave.Fields("HCess1") = 0
   ' objRSSave.Fields("GrandTotal") = Val(mshfcharges.TextMatrix(mshfcharges.Rows - 1, 2))
    objRSSave.Fields("AddedBy") = strUserName
    objRSSave.Fields("AddedOn") = Format(Now, "dd-MMM-yyyy HH:mm:ss")
    objRSSave.Fields("SGST") = dblSumSGSTAmt
    objRSSave.Fields("CGST") = dblSumCGSTAmt
    objRSSave.Fields("IGST") = dblSumIGSTAmt
    objRSSave.Fields("GrandTotal") = Val(dblSumSGSTAmt) + Val(dblSumCGSTAmt) + Val(dblSumIGSTAmt) + Val(dblSumNetAmtTotal)
    objRSSave.Fields("PartyID") = Val(lblpartyid.Caption)
    objRSSave.Fields("amtinword") = RupeesConvert(Val(dblSumSGSTAmt) + Val(dblSumCGSTAmt) + Val(dblSumIGSTAmt) + Val(dblSumNetAmtTotal))
    objRSSave.Update
    
    objDBConnection.Execute "Update NOC Set InvStatus='R' where nocno='" & Trim(txtnocno.Text) & "' and bondtype='" & Trim(cmbbondtype.Text) & "'"
    objDBConnection.Execute "Update BOND_WOCHARGES Set ReceiptNO=" & Val(dblAssessNo) & "  WHERE nocno='" & Trim(txtnocno.Text) & "' AND Iscancel=0"
    
    MsgBox "Assessment no.: < " & Val(dblAssessNo) & " > generated successfully.", vbInformation
    
    dblAssessNoFromAssess = dblAssessNo
    If (MsgBox("Do you wish to print receipt? " & vbCrLf & "Click 'YES' to print Receipt. Click 'NO' to print PD Adjustment.", vbYesNo) = vbYes) Then
        Load frmnocbondreceipt
        frmnocbondreceipt.Show vbModal
    Else
       ' Load frmBondreceiptPD
       ' frmBondreceiptPD.Show vbModal
    End If

    Call sub_EventLog("NOC Assess Generated", strRefDoc, Trim(txtassessno.Text), strUserName, Format(Now, "dd-MMM-yyyy HH:mm"), strMacIP)
  '  gFrmName = "rptNOCAssessment1"
 '   strSqlToExecute = "{Bond_AssessM.AssessNo}=" & Val(dblAssessNo) & " and  AND {bond_AssessM.WorkYear}='" & Trim(strWorkYear) & "' "
   ' Load frmReport
  '  frmReport.Show vbModal
    
    Call cmdclear_Click
    
    txtnocno.SetFocus
    
errorhandler:
    If Err Then
        If Err Then MsgBox Err.Description
        objDBConnection.Execute "ROLLBACK TRANSACTION"
    End If
End Sub

Private Sub cmdsearch_Click()
    Dim objRSFetch  As ADODB.Recordset
    txtgstpartyname.Visible = True
    dblgstpartyid = 0
    Load frmAllExportGateInNo
    frmAllExportGateInNo.Show vbModal
    Set objRSFetch = New ADODB.Recordset
    objRSFetch.Open "get_GST_Party_ID " & Val(dblgstpartyid) & "", objDBConnection, adOpenStatic, adLockReadOnly
    If Not objRSFetch.EOF Then
        txtgstpartyname.Visible = True
        txtgstin.Text = Trim(objRSFetch.Fields("GSTIn_uniqID"))
        lblpartyid.Caption = Val(objRSFetch.Fields("GSTID"))
        lblpin.Caption = Val(objRSFetch.Fields("state_code"))
        txtgstpartyname.Text = Trim(objRSFetch.Fields("gstname"))
        lbladdress.Caption = Trim(objRSFetch.Fields("gstaddress"))
        txtgstpartyname.SetFocus
    Else
        txtgstin.Text = ""
        lblpartyid.Caption = ""
        lblpin.Caption = ""
        txtgstpartyname.Text = ""
    End If
End Sub


Private Sub dtpvalidupto_KeyDown(KeyCode As Integer, Shift As Integer)
    If KeyCode = vbKeyReturn Then
        txtnocno.SetFocus
    End If
    If KeyCode = vbKeyEscape Then
        Unload Me
    End If
End Sub

Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
    
    If KeyCode = vbKeyF9 Then
        Call lblAllAssess_Click
    End If
      
    If KeyCode = vbKeyF7 Then
        Call lbladdtional_Click
    End If
    
    If KeyCode = vbKeyF5 Then
        Call cmdsearch_Click
    End If
    
    If KeyCode = vbKeyEscape Then
        Call cmdclear_Click
    End If
End Sub

Private Sub Form_Load()
    On Error GoTo errorhandler
    
    lblHeader.BackColor = RGB(141, 98, 53)
    lblFooter.BackColor = RGB(141, 98, 53)
    dtpassessdate.Value = Format(Now, "dd-MMM-yyyy")
    dtpvalidupto.Value = Format(Now, "dd-MMM-yyyy 23:59")
    Call sub_buttonaccess
    Call sub_ChargesGridSettings
    Call sub_CalcTotals
    Call sub_fillAccount
    Call sub_Filltariffs
    cmdsave.Enabled = False
    With cmbbondtype
        .Clear
        .AddItem "OPEN BOND": .ItemData(.NewIndex) = 1
        .AddItem "CLOSE BOND": .ItemData(.NewIndex) = 2
        .AddItem "General": .ItemData(.NewIndex) = 3
    End With
errorhandler:
    If Err Then MsgBox Err.Description, vbInformation
End Sub

Private Sub Label11_Click()
    Load frmbondadditional
    frmbondadditional.Show vbModal
End Sub

Private Sub lbladdtional_Click()
    Load frmbondadditional
    frmbondadditional.Show vbModal
End Sub

Private Sub lblAllAssess_Click()
    Load frmNOCAssessList
    frmNOCAssessList.Show vbModal
End Sub

Private Sub mshfcharges_EnterCell()
    mshfcharges.CellBackColor = vbYellow
End Sub

Private Sub mshfcharges_KeyDown(KeyCode As Integer, Shift As Integer)
    If KeyCode = vbKeyReturn Then
        cmdsave.SetFocus
    End If
End Sub

Private Sub mshfcharges_LeaveCell()
    mshfcharges.CellBackColor = vbWhite
End Sub

Private Sub mshfcharges_LostFocus()
    mshfcharges.CellBackColor = vbWhite
End Sub

Private Sub Timer1_Timer()
    dtpassessdate.Value = Format(Now, "dd-MMM-yyyy HH:mm")
End Sub

Private Sub txtadditionalamt_KeyDown(KeyCode As Integer, Shift As Integer)
    If KeyCode = vbKeyReturn Then
        cmdadditional.SetFocus
    End If
End Sub

Private Sub txtduty_Change()
     lblvalueduty.Caption = Val(txtvalue.Text) + Val(txtduty.Text)
End Sub

Private Sub txtgstpartyname_KeyDown(KeyCode As Integer, Shift As Integer)
    If KeyCode = vbKeyReturn Then
        cmbtariff.SetFocus
    End If
End Sub

Private Sub txtIGM_GotFocus()
    txtigm.SelStart = 0
    txtigm.SelLength = Len(txtigm.Text)
End Sub

Private Sub txtigm_KeyDown(KeyCode As Integer, Shift As Integer)
    If KeyCode = vbKeyReturn Then
        txtitem.SetFocus
    End If
End Sub

Private Sub txtItem_GotFocus()
    txtitem.SelStart = 0
    txtitem.SelLength = Len(txtitem.Text)
End Sub

Private Sub txtNOCNo_KeyPress(KeyAscii As Integer)
    KeyAscii = Asc(UCase(Chr(KeyAscii)))
End Sub

Private Sub txtvalue_Change()
     lblvalueduty.Caption = Val(txtvalue.Text) + Val(txtduty.Text)
End Sub

Private Sub txtvalue_KeyPress(KeyAscii As Integer)
    If Not Chr(KeyAscii) Like "[0-9]" And KeyAscii <> 8 Then
        KeyAscii = 0
    End If
End Sub

Private Sub sub_Filltariffs()
    On Error GoTo errorhandler
    
    Dim objRSTariff         As ADODB.Recordset
    
    Set objRSTariff = New ADODB.Recordset
    objRSTariff.Open "SELECT DISTINCT TariffID FROM bond_tariffmaster WHERE CustiD=" & Val(txtcustid.Text) & " AND " & Format(dtpassessdate.Value, "yyyyMMdd") & " BETWEEN EffectiveFROM and EffectiveUpto and IsActive=1", objDBConnection, adOpenStatic, adLockReadOnly
    
    cmbtariff.Clear
    Do While Not objRSTariff.EOF
        cmbtariff.AddItem objRSTariff.Fields("TariffID")
        objRSTariff.MoveNext
    Loop
    
    If cmbtariff.ListCount = 1 Then cmbtariff.ListIndex = 0
   ' cmbtariff.Text = "GEN"
    If objRSTariff.State = 1 Then objRSTariff.Close
    Set objRSTariff = Nothing

errorhandler:
    If Err Then MsgBox Err.Description, vbInformation
End Sub

Private Sub sub_ChargesGridSettings()
    On Error GoTo errorhandler
    
    Dim objRSFetch              As ADODB.Recordset
    Dim objRStemp               As ADODB.Recordset
    
    mshfcharges.Clear
    mshfcharges.Rows = 2
    mshfcharges.Cols = 5
    
    mshfcharges.TextMatrix(0, 0) = "account id"
    mshfcharges.TextMatrix(0, 1) = "account name"
    mshfcharges.TextMatrix(0, 2) = "amount"
    mshfcharges.TextMatrix(0, 3) = "Is STax"
    mshfcharges.TextMatrix(0, 4) = "Is Other Accounts"
        
    mshfcharges.ColWidth(0) = 0
    mshfcharges.ColWidth(1) = 3900
    mshfcharges.ColWidth(2) = 1400
    mshfcharges.ColWidth(3) = 0
    mshfcharges.ColWidth(4) = 0
    
    mshfcharges.ColAlignmentFixed(0) = flexAlignCenterCenter
    mshfcharges.ColAlignmentFixed(1) = flexAlignCenterCenter
    mshfcharges.ColAlignmentFixed(2) = flexAlignCenterCenter
    
    If Trim(txtnocno.Text) = "" Then
        Exit Sub
    End If
    
    Set objRSFetch = New ADODB.Recordset
    objRSFetch.Open "SELECT DISTINCT AccountID FROM bond_tariffdetails WHERE TariffID='" & Trim(cmbtariff.Text) & "' and " & _
                    "BondType='N' and " & Format(dtpassessdate.Value, "yyyyMMdd") & " BETWEEN EffectiveFROM and EffectiveUpto", objDBConnection, adOpenStatic, adLockReadOnly
    
    dblSTaxOnAmount = 0
    dblGroup1Amt = 0
    dblGroup2Amt = 0
    
    Do While Not objRSFetch.EOF
        DoEvents
        
        Call sub_fetchcharges(objRSFetch.Fields("AccountID"))
        
        If dblnetamount <> 0 Then
            mshfcharges.TextMatrix(mshfcharges.Rows - 1, 0) = objRSFetch.Fields("AccountID")
            
            Set objRStemp = New ADODB.Recordset
            objRStemp.Open "SELECT AccountName,GroupID FROM bond_AccountMaster WHERE AccountID=" & Val(mshfcharges.TextMatrix(mshfcharges.Rows - 1, 0)), objDBConnection, adOpenStatic, adLockReadOnly
            If Not objRStemp.EOF Then
                mshfcharges.TextMatrix(mshfcharges.Rows - 1, 1) = objRStemp.Fields("AccountName")
                If objRStemp.Fields("GroupID") = 1 Then
                    dblGroup1Amt = dblGroup1Amt + dblnetamount
                Else
                    dblGroup2Amt = dblGroup2Amt + dblnetamount
                End If
            End If
            mshfcharges.TextMatrix(mshfcharges.Rows - 1, 2) = dblnetamount
            mshfcharges.TextMatrix(mshfcharges.Rows - 1, 4) = 0
            
            mshfcharges.Rows = mshfcharges.Rows + 1
        End If
        
        objRSFetch.MoveNext
    Loop
    Dim intRowCounter As Integer
    
    Dim blnAccountFound  As Boolean
    Set objRSFetch = New ADODB.Recordset
    objRSFetch.Open "select accountid, SUM(amount) as amount, IsSTax FROM BOND_wocharges WHERE nocnO='" & Trim(txtnocno.Text) & "'  and ReceiptNo=0 and TransNo=0 and IsCancel=0 GROUP BY accountID,GroupID, IsSTax", objDBConnection, adOpenStatic, adLockReadOnly
    Do While Not objRSFetch.EOF
        If objRSFetch.Fields("amount") <> 0 Then
            For intRowCounter = 1 To mshfcharges.Rows - 1
                If objRSFetch.Fields("AccountID") = Val(mshfcharges.TextMatrix(intRowCounter, 0)) Then
                    blnAccountFound = True
                    Exit For
                End If
            Next
            
            If blnAccountFound = False Then
                intRowCounter = mshfcharges.Rows - 1
            End If
            
            If objRSFetch.Fields("IsSTax") = True Then
                dblSTaxOnAmount = dblSTaxOnAmount + Val(objRSFetch.Fields("Amount"))
            End If
         '   If objRSFetch.Fields("GroupID") = 1 Then
                dblGroup1Amt = dblGroup1Amt + Val(objRSFetch.Fields("Amount"))
          '  Else
           '     dblGroup2Amt = dblGroup2Amt + Val(objRSFetch.Fields("Amount"))
           ' End If
            
            mshfcharges.TextMatrix(intRowCounter, 0) = objRSFetch.Fields("AccountID")
            Set objRStemp = New ADODB.Recordset
            objRStemp.Open "SELECT AccountName FROM BOND_accountmaster WHERE AccountID=" & Val(objRSFetch.Fields("accountID")), objDBConnection, adOpenStatic, adLockReadOnly
            If Not objRStemp.EOF Then
                mshfcharges.TextMatrix(intRowCounter, 1) = objRStemp.Fields("AccountName")
            End If
            mshfcharges.TextMatrix(intRowCounter, 2) = Val(mshfcharges.TextMatrix(intRowCounter, 2)) + objRSFetch.Fields("amount")

            If blnAccountFound = False Then
                mshfcharges.Rows = mshfcharges.Rows + 1
            End If
        End If
        
        objRSFetch.MoveNext
    Loop
    
    
    
    
    If chkIns.Value = Checked Then
        Call sub_CalcInsurance
    End If
    
errorhandler:
    If Err Then MsgBox Err.Description, vbInformation
End Sub

Private Sub sub_fetchcharges(strAccountID As String)
    On Error GoTo errorhandler
    
    Dim objRSCharges            As ADODB.Recordset
    Dim objRSPaidCharges        As ADODB.Recordset
    Dim objRSSlab               As ADODB.Recordset
    Dim objRSCheckArea            As ADODB.Recordset
    
    Dim dblSlabValue            As Double
    Dim dblAmount               As Double
    Dim Intweeks                As Integer
    
    Dim dblPaidAmount           As Double
    
    Dim strSQL                  As String
    Dim intDays                 As Double
    Dim intContCounter          As Integer
                
            dblnetamount = 0
            dblPaidAmount = 0
            intDays = 0

            DoEvents
        
            dblAmount = 0
            Set objRSCharges = New ADODB.Recordset
            objRSCharges.Open "SELECT SorF, SlabID, FixedAmt, IsSTax, Size FROM bond_tariffdetails WHERE TariffID='" & Trim(cmbtariff.Text) & "' AND BondType='N' " & _
                            " AND AccountID='" & strAccountID & "' AND " & Format(dtpassessdate.Value, "yyyyMMdd") & " BETWEEN EffectiveFROM and EffectiveUpto", objDBConnection, adOpenStatic, adLockReadOnly
            Do While Not objRSCharges.EOF
              intDays = DateDiff("d", dtpnocdate.Value, dtpvalidupto.Value)
              If objRSCharges.Fields("SorF") = "S" Then
                  dblnetamount = dblnetamount + slab_CalcAmount(objRSCharges.Fields("SlabID"), intDays, 0, Val(txtgrswt.Text), Format(dtpnocdate.Value, "dd-MMM-yyyy HH:mm"))
              ElseIf objRSCharges.Fields("SorF") = "F" Then
                  dblnetamount = dblnetamount + Val(objRSCharges.Fields("FixedAmt"))
              ElseIf objRSCharges.Fields("SorF") = "C" And objRSCharges.Fields("size") = "20" Then
                  dblnetamount = dblnetamount + Val(objRSCharges.Fields("FixedAmt")) * Val(txt20.Text)
              ElseIf objRSCharges.Fields("SorF") = "C" And objRSCharges.Fields("size") = "40" Then
                  dblnetamount = dblnetamount + Val(objRSCharges.Fields("FixedAmt")) * Val(txt40.Text)
              ElseIf objRSCharges.Fields("SorF") = "Q" Then
                  dblnetamount = dblnetamount + Val(objRSCharges.Fields("FixedAmt")) * Val(txtqty.Text)
              ElseIf objRSCharges.Fields("SorF") = "O" Then
                  dblnetamount = dblnetamount + Val(objRSCharges.Fields("FixedAmt")) * Val(Val(txtgrswt.Text) / 1000)
              End If
                  
              If intDays Mod 7 = 0 Then
                  Intweeks = intDays / 7
              Else
                  Intweeks = Int((intDays / 7)) + 1
              End If
                  
              Set objRSCheckArea = New ADODB.Recordset
              objRSCheckArea.Open "SELECT ConsiderArea FROM bond_tariffdetails WHERE tariffID='" & Trim(cmbtariff.Text) & "' AND BondType='N' " & _
                                " AND AccountID='" & strAccountID & "' AND " & Format(dtpassessdate.Value, "yyyyMMdd") & " BETWEEN EffectiveFROM and EffectiveUpto", objDBConnection, adOpenStatic, adLockReadOnly
                                
              If Not objRSCheckArea.EOF Then
                  If objRSCheckArea.Fields("ConsiderArea") = True Then
                      dblnetamount = Val(objRSCharges.Fields("fixedamt")) * Val(txtcalculatearea.Text) * Intweeks
                  End If
              End If
              Dim intRow As Integer
              If chkadditional.Value = 1 Then
              For intRow = 1 To mshfadditional.Rows - 1
                  If chkadditional.Value = 1 And Val(strAccountID) = Val(mshfadditional.TextMatrix(intRow, 0)) Then
                      If mshfadditional.RowHeight(intRow) <> 0 And Trim(mshfadditional.TextMatrix(intRow, 1)) <> "" And Val(strAccountID) = Val(mshfadditional.TextMatrix(intRow, 0)) Then
                          dblnetamount = dblnetamount + Val(mshfadditional.TextMatrix(intRow, 2))
                      End If
                      
                  End If
              Next
              End If
                  
              strSQL = "SELECT SUM(NetAmount) FROM Bond_AssessD D "
              strSQL = strSQL & "INNER JOIN Bond_AssessM M ON M.AssessNo=D.AssessNo AND M.WorkYear=D.WorkYear "
              strSQL = strSQL & "WHERE M.NocNo='" & Trim(txtnocno.Text) & "'  AND M.DeliveryType='N' AND M.IsCancel=0 "
              
              Set objRSPaidCharges = New ADODB.Recordset
              objRSPaidCharges.Open strSQL, objDBConnection, adOpenStatic, adLockReadOnly
                                  
              If Not objRSPaidCharges.EOF Then
                  If IsNull(objRSPaidCharges.Fields(0)) = False Then
                      dblPaidAmount = dblPaidAmount + objRSPaidCharges.Fields(0)
                  End If
              End If
              
              dblnetamount = dblnetamount - dblPaidAmount
              
              If objRSCharges.Fields("IsSTax") = True Then
                  dblSTaxOnAmount = dblSTaxOnAmount + dblnetamount
              End If
      objRSCharges.MoveNext
      Loop
    Set objRSCharges = Nothing

errorhandler:
    If Err Then MsgBox Err.Description, vbInformation
End Sub

Private Function slab_CalcAmount(slabID As Integer, DaysValue As Double, percentage As Double, Weight As Double, NOCDate As Date)
    On Error GoTo errorhandler
    
    Dim objRSSlab       As ADODB.Recordset
    Dim dblSlabAmount   As Double
    
    Dim dtValidDate     As Date
    
    Dim intValidCounter As Integer
    Dim Intweeks        As Integer
    Dim dblAddSecs      As Double
    Dim dblActualHrs    As Double
    Dim dblHrs          As Double
    Dim dblLeftHrs      As Double
    Dim dblAmt          As Double
    Dim dblTotDays      As Double
    
    dblSlabAmount = 0
    
    Set objRSSlab = New ADODB.Recordset
    objRSSlab.Open "SELECT * FROM bond_slabs WHERE SlabID=" & slabID & " ORDER By FROMSlab", objDBConnection, adOpenStatic, adLockReadOnly
                
    If Not objRSSlab.EOF Then
        If objRSSlab.Fields("slabON") = "Days" Then
            If Not objRSSlab.EOF Then
                While objRSSlab.Fields("FromSlab") <= DaysValue
                    If objRSSlab.Fields("ToSlab") < DaysValue Then
                        dblSlabAmount = dblSlabAmount + (Val(objRSSlab.Fields("ToSlab")) - Val(objRSSlab.Fields("FromSlab")) + 1) * Val(objRSSlab.Fields("Value"))
                    Else
                        dblSlabAmount = dblSlabAmount + (Val(DaysValue) - Val(objRSSlab.Fields("FromSlab")) + 1) * Val(objRSSlab.Fields("Value"))
                    End If
                    objRSSlab.MoveNext
                    If objRSSlab.EOF Then GoTo Proceed
                Wend
Proceed:
                slab_CalcAmount = slab_CalcAmount + dblSlabAmount
            End If
       ElseIf objRSSlab.Fields("slabON") = "Weeks" Then
        If DaysValue Mod 7 = 0 Then
            Intweeks = DaysValue / 7
        Else
            Intweeks = Int((DaysValue / 7)) + 1
        End If
        
        Set objRSSlab = New ADODB.Recordset
        objRSSlab.Open "SELECT * FROM bond_slabs WHERE SlabID=" & slabID & " and " & percentage & " BETWEEN FROMSlab and ToSlab ORDER BY FROMSlab", objDBConnection, adOpenStatic, adLockReadOnly
                        
        If Not objRSSlab.EOF Then
            dblSlabAmount = Intweeks * Val(objRSSlab.Fields("Value"))
        End If
        slab_CalcAmount = slab_CalcAmount + dblSlabAmount
    ElseIf objRSSlab.Fields("slabON") = "Percentage" Then
        Set objRSSlab = New ADODB.Recordset
        objRSSlab.Open "SELECT * FROM bond_slabs WHERE SlabID=" & slabID & " and " & Intweeks & " BETWEEN FROMSlab and ToSlab ORDER BY FROMSlab", objDBConnection, adOpenStatic, adLockReadOnly
                        
        If Not objRSSlab.EOF Then
            dblSlabAmount = Intweeks * Val(objRSSlab.Fields("Value"))
        End If
        slab_CalcAmount = slab_CalcAmount + dblSlabAmount
    ElseIf objRSSlab.Fields("slabON") = "Weight" Then
        Set objRSSlab = New ADODB.Recordset
        objRSSlab.Open "SELECT * FROM bond_slabs WHERE SlabID=" & slabID & " and " & Weight & " BETWEEN FROMSlab and ToSlab ORDER BY FROMSlab", objDBConnection, adOpenStatic, adLockReadOnly
                                
        If Not objRSSlab.EOF Then
         If Val(txtgrswt.Text) Mod 100 > 0 Then
         
            dblSlabAmount = Round(Val(objRSSlab.Fields("Value")) * ((Val(txtgrswt.Text) / 1000)))
          If dblSlabAmount < 85 Then
                dblSlabAmount = 85
            End If
        Else
            dblSlabAmount = Round(Val(objRSSlab.Fields("Value")) * (Val(txtgrswt.Text) / 1000))
            If dblSlabAmount < 85 Then
                dblSlabAmount = 85
            End If
        End If
        End If
        slab_CalcAmount = slab_CalcAmount + dblSlabAmount
    ElseIf objRSSlab.Fields("slabON") = "Hours" Then
        dtValidDate = Format(NOCDate, "dd-MMM-yyyy HH:mm")

        ''''''''359 hard coded for 6 hours slab
        dtpvalidupto.Value = Format(DateAdd("s", 359 * 60, Now), "dd-MMM-yyyy HH:mm")

        intValidCounter = 0
        While Format(dtValidDate, "yyyyMMddHHmm") <= Format(Now, "yyyyMMddHHmm")
            If intValidCounter = 0 Then
                dblAddSecs = Val(360) * Val(60)
                dtValidDate = Format(DateAdd("s", dblAddSecs, dtValidDate), "dd-MMM-yyyy HH:mm")
                intValidCounter = intValidCounter + 1
            ElseIf intValidCounter = 1 Then
                dblAddSecs = Val(360) * Val(60)
                dtValidDate = Format(DateAdd("s", dblAddSecs, dtValidDate), "dd-MMM-yyyy HH:mm")
                intValidCounter = intValidCounter + 1
            ElseIf intValidCounter = 2 Then
                dblAddSecs = Val(720) * Val(60)
                dtValidDate = Format(DateAdd("s", dblAddSecs, dtValidDate), "dd-MMM-yyyy HH:mm")
                intValidCounter = 0
            End If
        Wend
        dtValidDate = Format(DateAdd("s", -60, dtValidDate), "dd-MMM-yyyy HH:mm")

        dblHrs = 0
        dblHrs = DateDiff("s", Format(NOCDate, "dd-MMM-yyyy HH:mm"), Format(dtpvalidupto.Value, "dd-MMM-yyyy HH:mm"))
        dblActualHrs = DateDiff("s", Format(NOCDate, "dd-MMM-yyyy HH:mm"), Format(dtpvalidupto.Value, "dd-MMM-yyyy HH:mm"))
        dblHrs = (dblHrs / 60) / 60
        dblActualHrs = (dblActualHrs / 60) / 60
        dblAmt = 0

        dblTotDays = Int(dblActualHrs / 24)
        If dblHrs > 24 Then
            dblLeftHrs = dblActualHrs - (dblHrs * 24)
        Else
            dblLeftHrs = dblActualHrs
        End If

        Set objRSSlab = New ADODB.Recordset
        objRSSlab.Open "SELECT * FROM bond_slabs WHERE SlabID=" & slabID & " and " & dblTotDays & " BETWEEN FROMSlab and ToSlab ORDER BY ToSlab DESC", objDBConnection, adOpenStatic, adLockReadOnly

        If Not objRSSlab.EOF Then
            dblSlabAmount = Val(objRSSlab.Fields("Value")) * dblTotDays
        End If
        slab_CalcAmount = slab_CalcAmount + dblSlabAmount

        Set objRSSlab = New ADODB.Recordset
        objRSSlab.Open "SELECT * FROM bond_slabs WHERE SlabID=" & slabID & " and " & dblLeftHrs & " BETWEEN FROMSlab and ToSlab ORDER BY FROMSlab", objDBConnection, adOpenStatic, adLockReadOnly

        If Not objRSSlab.EOF Then
            dblSlabAmount = Val(objRSSlab.Fields("Value"))
        End If
        slab_CalcAmount = slab_CalcAmount + dblSlabAmount
        End If
    End If

errorhandler:
    If Err Then MsgBox Err.Description, vbInformation
End Function

    Private Sub sub_CalcTotals()
    Dim dblNettotal                 As Double
    Dim intCounter                  As Integer
    
    Dim objRSDiscount               As ADODB.Recordset
    Dim objRSGetSetting             As ADODB.Recordset

    mshfcharges.Rows = mshfcharges.Rows + 7
    
    mshfcharges.TextMatrix(mshfcharges.Rows - 6, 2) = dblGroup1Amt
'    For intCounter = 1 To mshfcharges.Rows - 7
'        mshfcharges.TextMatrix(mshfcharges.Rows - 6, 2) = Val(mshfcharges.TextMatrix(mshfcharges.Rows - 6, 2)) + Val(mshfcharges.TextMatrix(intCounter, 2))
'    Next
    mshfcharges.TextMatrix(mshfcharges.Rows - 6, 1) = "Net Total"
    mshfcharges.Row = mshfcharges.Rows - 6
    mshfcharges.Col = 1
    mshfcharges.CellFontBold = True
'    mshfcharges.TextMatrix(mshfcharges.Rows - 5, 1) = "Discount"
'    mshfcharges.Row = mshfcharges.Rows - 5
    mshfcharges.Col = 1
    mshfcharges.CellFontBold = True
'    Set objRSDiscount = New ADODB.Recordset
'    objRSDiscount.Open "SELECT DiscPercent FROM imp_Tariffmaster WHERE TariffID='" & lblTariffID.Caption & "' and " & Format(dtpAssessdate.Value, "yyyyMMdd") & " BETWEEN EffectiveFrom and EffectiveUpto and IsActive=1", objDBConnection, adOpenStatic, adLockReadOnly
'    If Not objRSDiscount.EOF Then
'        mshfcharges.TextMatrix(mshfcharges.Rows - 5, 2) = Format(mshfcharges.TextMatrix(mshfcharges.Rows - 6, 2) * (Val(objRSDiscount.Fields("DiscPercent") / 100)), "0")
'    Else
'        mshfcharges.TextMatrix(mshfcharges.Rows - 5, 2) = 0
'    End If
    
    
    mshfcharges.TextMatrix(mshfcharges.Rows - 4, 1) = strSGSTPer
    mshfcharges.Row = mshfcharges.Rows - 4
    mshfcharges.Col = 1
    mshfcharges.CellFontBold = True
    mshfcharges.TextMatrix(mshfcharges.Rows - 4, 2) = Format(dblGroup1Amt * (dblSGST / 100), "0.00")
    mshfcharges.TextMatrix(mshfcharges.Rows - 4, 2) = Round(mshfcharges.TextMatrix(mshfcharges.Rows - 4, 2))
    mshfcharges.TextMatrix(mshfcharges.Rows - 3, 1) = StrCGSTPEr
    mshfcharges.Row = mshfcharges.Rows - 3
    mshfcharges.Col = 1
    mshfcharges.CellFontBold = True
    mshfcharges.TextMatrix(mshfcharges.Rows - 3, 2) = Format(dblGroup1Amt * (dblCGST / 100), "0.00")
    mshfcharges.TextMatrix(mshfcharges.Rows - 3, 2) = Round(mshfcharges.TextMatrix(mshfcharges.Rows - 3, 2))
    mshfcharges.TextMatrix(mshfcharges.Rows - 2, 1) = StrIGSTPer
    mshfcharges.Row = mshfcharges.Rows - 2
    mshfcharges.Col = 1
    mshfcharges.CellFontBold = True
    mshfcharges.TextMatrix(mshfcharges.Rows - 2, 2) = Format(dblGroup1Amt * (dblIGST / 100), "0.00")
    mshfcharges.TextMatrix(mshfcharges.Rows - 2, 2) = Format(mshfcharges.TextMatrix(mshfcharges.Rows - 2, 2), "0.00")
    mshfcharges.TextMatrix(mshfcharges.Rows - 1, 1) = "Grand Total"
    mshfcharges.Row = mshfcharges.Rows - 1
    mshfcharges.Col = 1
    mshfcharges.CellFontBold = True
    mshfcharges.TextMatrix(mshfcharges.Rows - 1, 2) = Round(Val(mshfcharges.TextMatrix(mshfcharges.Rows - 6, 2)) - _
                                                        Val(mshfcharges.TextMatrix(mshfcharges.Rows - 5, 2)) + _
                                                        Val(mshfcharges.TextMatrix(mshfcharges.Rows - 4, 2)) + _
                                                        Val(mshfcharges.TextMatrix(mshfcharges.Rows - 3, 2)) + _
                                                        Val(mshfcharges.TextMatrix(mshfcharges.Rows - 2, 2)))
                                                        
    dblT1Amt = mshfcharges.TextMatrix(mshfcharges.Rows - 1, 2)
    
'    Call sub_CalcTotals1
End Sub



Private Sub sub_CalcTotals1()
    Dim dblNettotal                 As Double
    Dim intCounter                  As Integer
    
    Dim objRSDiscount               As ADODB.Recordset
    Dim objRSGetSetting             As ADODB.Recordset

    mshfcharges.Rows = mshfcharges.Rows + 8
    
    mshfcharges.TextMatrix(mshfcharges.Rows - 7, 2) = dblGroup2Amt
'    For intCounter = 1 To mshfcharges.Rows - 7
'        mshfcharges.TextMatrix(mshfcharges.Rows - 6, 2) = Val(mshfcharges.TextMatrix(mshfcharges.Rows - 6, 2)) + Val(mshfcharges.TextMatrix(intCounter, 2))
'    Next
    mshfcharges.TextMatrix(mshfcharges.Rows - 7, 1) = "Net Total"
    mshfcharges.Row = mshfcharges.Rows - 7
    mshfcharges.Col = 1
    mshfcharges.CellFontBold = True
    
'    mshfcharges.TextMatrix(mshfcharges.Rows - 6, 1) = "Discount"
'    mshfcharges.Row = mshfcharges.Rows - 6
'    mshfcharges.Col = 1
'    mshfcharges.CellFontBold = True
''    Set objRSDiscount = New ADODB.Recordset
'    objRSDiscount.Open "SELECT DiscPercent FROM imp_Tariffmaster WHERE TariffID='" & lblTariffID.Caption & "' and " & Format(dtpAssessdate.Value, "yyyyMMdd") & " BETWEEN EffectiveFrom and EffectiveUpto and IsActive=1", objDBConnection, adOpenStatic, adLockReadOnly
'    If Not objRSDiscount.EOF Then
'        mshfcharges.TextMatrix(mshfcharges.Rows - 6, 2) = Format(mshfcharges.TextMatrix(mshfcharges.Rows - 7, 2) * (Val(objRSDiscount.Fields("DiscPercent") / 100)), "0")
'    Else
'        mshfcharges.TextMatrix(mshfcharges.Rows - 6, 2) = 0
'    End If
    
    
    mshfcharges.TextMatrix(mshfcharges.Rows - 5, 1) = strSGSTPer
    mshfcharges.Row = mshfcharges.Rows - 5
    mshfcharges.Col = 1
    mshfcharges.CellFontBold = True
    mshfcharges.TextMatrix(mshfcharges.Rows - 5, 2) = Format(dblGroup2Amt * (dblSGST / 100), "0.00")
    mshfcharges.TextMatrix(mshfcharges.Rows - 5, 2) = Round(mshfcharges.TextMatrix(mshfcharges.Rows - 5, 2))
  '  mshfcharges.TextMatrix(mshfcharges.Rows - 4, 1) = "E Cess"
'    mshfcharges.Row = mshfcharges.Rows - 4
'    mshfcharges.Col = 1
'    mshfcharges.CellFontBold = True
'    mshfcharges.TextMatrix(mshfcharges.Rows - 4, 2) = Format(Val(mshfcharges.TextMatrix(mshfcharges.Rows - 5, 2)) * (dblEDUCESS / 100), "0")
'    mshfcharges.TextMatrix(mshfcharges.Rows - 4, 2) = Format(mshfcharges.TextMatrix(mshfcharges.Rows - 4, 2), "0.00")
'    mshfcharges.TextMatrix(mshfcharges.Rows - 3, 1) = "H Cess"
    mshfcharges.Row = mshfcharges.Rows - 3
    mshfcharges.Col = 1
    mshfcharges.CellFontBold = True
    mshfcharges.TextMatrix(mshfcharges.Rows - 3, 2) = Format(Val(mshfcharges.TextMatrix(mshfcharges.Rows - 5, 2)) * (dblHIGHCESS / 100), "0")
    mshfcharges.TextMatrix(mshfcharges.Rows - 3, 2) = Format(mshfcharges.TextMatrix(mshfcharges.Rows - 3, 2), "0.00")
    mshfcharges.TextMatrix(mshfcharges.Rows - 2, 1) = "Total"
    mshfcharges.Row = mshfcharges.Rows - 2
    mshfcharges.Col = 1
    mshfcharges.CellFontBold = True
    mshfcharges.TextMatrix(mshfcharges.Rows - 2, 2) = Format(Val(mshfcharges.TextMatrix(mshfcharges.Rows - 7, 2)) - _
                                                        Val(mshfcharges.TextMatrix(mshfcharges.Rows - 6, 2)) + _
                                                        Val(mshfcharges.TextMatrix(mshfcharges.Rows - 5, 2)) + _
                                                        Val(mshfcharges.TextMatrix(mshfcharges.Rows - 4, 2)) + _
                                                        Val(mshfcharges.TextMatrix(mshfcharges.Rows - 3, 2)), "0.00")
                                                        
    mshfcharges.TextMatrix(mshfcharges.Rows - 1, 1) = "Grand Total"
    mshfcharges.Row = mshfcharges.Rows - 1
    mshfcharges.Col = 1
    mshfcharges.CellFontBold = True
    mshfcharges.TextMatrix(mshfcharges.Rows - 1, 2) = Round((mshfcharges.TextMatrix(mshfcharges.Rows - 2, 2)) + dblT1Amt)

End Sub

Private Sub sub_showNOCDets()
On Error GoTo errorhandler
    Dim objRSFill   As ADODB.Recordset
      
   
    
    Set objRSFill = New ADODB.Recordset
    objRSFill.Open "SELECT *,CHAName,ImporterName FROM NOC,CHA,Importer, Agent WHERE " & _
    " NOC.CHAID=CHA.CHAID AND NOC.ImporterId=Importer.ImporterId AND Agent.AGID=NOC.CustID AND NOCNo='" & Trim(txtnocno.Text) & "'  " & _
    " and IsCancel=0", objDBConnection, adOpenStatic, adLockReadOnly
    If Not objRSFill.EOF Then
        txtBeNo.Text = objRSFill.Fields("BOENo")
        cmbbondtype.Text = Trim(objRSFill.Fields("bondtype"))
        dtpBEdate.Value = Format(objRSFill.Fields("BOEDate"), "dd-MMM-yyyy")
        txtigm.Text = objRSFill.Fields("IGMNo")
        dtpvalidupto.Value = Format(objRSFill.Fields("ExpiryDate"), "dd-MMM-yyyy 23:59")
        dtpnocdate.Value = Format(objRSFill.Fields("NOCDate"), "dd-MMM-yyyy")
        txtConsignee.Text = objRSFill.Fields("ImporterName")
        txtcommdity.Text = objRSFill.Fields("Commodity")
        txtcha.Text = objRSFill.Fields("CHAName")
        txtchaid.Text = objRSFill.Fields("chaid")
        txtcustid.Text = objRSFill.Fields("CustID")
        txtcustname.Text = objRSFill.Fields("AgName")
        txtimpid.Text = objRSFill.Fields("importerid")
        txt20.Text = objRSFill.Fields("size20")
        txt40.Text = objRSFill.Fields("size40")
        
        If Trim(objRSFill.Fields("Category")) = "HAZ" Then
            txtarea.Text = objRSFill.Fields("StorageSpace") * 2
            txtcalculatearea.Text = objRSFill.Fields("StorageSpace") * 2
            
        Else
            txtarea.Text = objRSFill.Fields("StorageSpace")
            txtcalculatearea.Text = objRSFill.Fields("StorageSpace")
        End If
        
        txtcargotype.Text = Trim(objRSFill.Fields("Category"))
        txtarean.Text = objRSFill.Fields("StorageSpace")
        
        txtqty = objRSFill.Fields("Qty")
        txtunit = objRSFill.Fields("Unit")
        txtgrswt.Text = objRSFill.Fields("GrossWt")
        txtvalue.Text = Val(objRSFill.Fields("value"))
        txtduty.Text = Val(objRSFill.Fields("Duty"))
        cmbtariff.SetFocus
    Else
        MsgBox "specified noc no not found.", vbCritical
        txtnocno.Text = ""
        txtnocno.SetFocus
    End If
errorhandler:
    If Err Then MsgBox Err.Description

End Sub

Private Sub txtnocno_KeyDown(KeyCode As Integer, Shift As Integer)
    
        
    If KeyCode = vbKeyReturn Then
        cmdAdd.SetFocus
    End If
End Sub
Function SRound(Real As Double) As Integer
    Dim tmp As Integer
    tmp = Val(Real)
    If Real - tmp >= 0.1 Then
        SRound = tmp + 1
    Else
        SRound = tmp
    End If
End Function
Private Sub sub_CalcInsurance()
    On Error GoTo errorhandler
    
    Dim intCount            As Integer
    Dim intDays             As Integer
    Dim intCalcDays         As Integer
    Dim intPrvDays          As Integer
    Dim Intweeks            As Double
    Dim varDate             As Variant
    Dim intNOCadvance       As Double
    Dim objRSFetch          As ADODB.Recordset
    Dim objRSCharges        As ADODB.Recordset
    Dim ObjRsNocadvance     As ADODB.Recordset
    Dim objRSCheckArea      As ADODB.Recordset
    Dim strSQL              As String
    Dim objRSPaidCharges    As ADODB.Recordset
    Dim dblPaidAmount       As Double
    Dim ObjRsNocvalidupto   As ADODB.Recordset
    Dim weeks               As Double
    Dim intTotalWeek        As Double
    Dim dtRecordDate        As Date
    Dim dblInsAmount        As Double
    Dim dblInsAmountPaid    As Double
    Dim dblInsRate          As Double
    Dim ObjRsBondExDate     As ADODB.Recordset
    Dim intTotalIDays       As Integer
    Dim IntValiduptdate     As Date
    Dim dbltemp             As Double

    dblInsAmount = 0
   ' dblInsRate = 0.15
    
    dblnetamount = 0
    intCount = 1
    intPrvDays = 0
    dbltemp = 0
    
'    Set ObjRsBondExDate = New ADODB.Recordset
'    ObjRsBondExDate.Open "select AssessDate, insuValidUptoDate from bond_assessM where NOCNO=" & Val(lblNOCNo.Caption) & " AND IGMNo=" & Val(txtigm.Text) & "  and BondType='E' AND Status='A' and IsCancel=0 ORDER By AssessDate DESC", objDBConnection, adOpenStatic, adLockReadOnly
'        If Not ObjRsBondExDate.EOF Then
'             lblreinsvupto.Caption = ObjRsBondExDate.Fields("insuValidUptoDate") + 1
'             dtpbondindate.Value = lblreinsvupto.Caption
'        End If
        
        If Val(lblvalueduty.Caption) <> 0 Then
            intTotalIDays = DateDiff("d", dtpnocdate.Value, dtpvalidupto.Value)
           ' intTotalIDays = DateDiff("d", dtpnocdate.Value, dtpvalidupto.Value) + 1
        End If
        If intTotalIDays Mod 7 = 0 Then
            intTotalWeek = intTotalIDays / 7
            lblinsvalidup.Caption = Format(DateAdd("d", (intTotalWeek * 7), dtpnocdate - 1), "dd-MMM-yyyy")
        Else
            intTotalWeek = SRound(intTotalIDays / 7)
            lblinsvalidup.Caption = Format(DateAdd("d", (intTotalWeek * 7), dtpnocdate - 1), "dd-MMM-yyyy")
        End If
        
   '''' For Prv Charges'''
        Set objRSCharges = New ADODB.Recordset
        objRSCharges.Open "SELECT SorF,BondType, SlabID, InsRate, IsSTax, effectiveFrom, effectiveUpto FROM bond_tariffdetails WHERE TariffID='" & Trim(cmbtariff.Text) & "'  " & _
                            " AND AccountID='3'AND BondType='N' AND  " & Format(dtpassessdate.Value, "yyyyMMdd") & " BETWEEN EffectiveFROM and EffectiveUpto ORDER BY effectivefrom", objDBConnection, adOpenStatic, adLockReadOnly
        If Not objRSCharges.EOF Then
            dbltemp = Val(lblvalueduty.Caption) * Val(objRSCharges.Fields("InsRate") / 1000)
        End If
                   
            dblnetamount = dbltemp * intTotalWeek
            dblInsAmount = Format(dblnetamount, "0.00")
            
     '  dblInsAmount = SRound((Val(lblvalueduty.Caption) * dblInsRate / 1000))
       'dblInsAmount = Round(dblInsAmount * intTotalWeek)
       ' lblinsvalidup.Caption = Format(DateAdd("d", (intTotalWeek * 7), dtpbondindate - 1), "dd-MMM-yyyy")
    mshfcharges.TextMatrix(mshfcharges.Rows - 1, 0) = "3"
    mshfcharges.TextMatrix(mshfcharges.Rows - 1, 1) = "Insurance Charges"
    mshfcharges.TextMatrix(mshfcharges.Rows - 1, 2) = dblInsAmount
    mshfcharges.TextMatrix(mshfcharges.Rows - 1, 4) = 1
    dblGroup1Amt = dblGroup1Amt + dblInsAmount
    'If objRSCharges.Fields("IsSTax") = True Then
                dblSTaxOnAmount = dblSTaxOnAmount + dblnetamount
   ' End If
errorhandler:
    If Err Then MsgBox Err.Description, vbInformation
End Sub

Private Sub sub_buttonaccess()
    Dim objRSCheck As ADODB.Recordset
    
    Set objRSCheck = New ADODB.Recordset
        objRSCheck.Open "SELECT * FROM UserRights WHERE UserID=" & intUserID & " and MenuID=" & dblmenuid & "", objDBConnection, adOpenStatic, adLockReadOnly
        If Not objRSCheck.EOF Then
            If Trim(objRSCheck.Fields("BAccess")) = "D" Then
                cmdsave.Visible = True
                cmdclear.Visible = True
            End If
            
            If Trim(objRSCheck.Fields("BAccess")) = "U" Then
                cmdsave.Visible = True
                cmdclear.Visible = True
            End If
            
            If Trim(objRSCheck.Fields("BAccess")) = "I" Then
                cmdsave.Visible = False
                cmdclear.Visible = True
            End If
            
            If Trim(objRSCheck.Fields("BAccess")) = "F" Then
                cmdsave.Visible = True
                cmdclear.Visible = True
            End If
            
            If Trim(objRSCheck.Fields("BAccess")) = "" Then
                cmdsave.Visible = False
                cmdclear.Visible = False
            End If
        End If
End Sub

Private Sub chkadditional_Click()
     If chkadditional.Value = 1 Then
        
        Call sub_gridadditional
        mshfadditional.Visible = True
        lblachead.Visible = True
        cmbheader.Visible = True
        cmdadditional.Visible = True
        txtadditionalamt.Visible = True
        cmdrmvadch.Visible = True
        cmbheader.SetFocus
    Else
        Call cmdcalc_Click
        mshfadditional.Visible = False
        lblachead.Visible = False
        cmbheader.Visible = False
        txtadditionalamt.Visible = False
        cmdadditional.Visible = False
        cmdrmvadch.Visible = False
        cmbheader.ListIndex = -1
        txtadditionalamt.Text = ""
        Call sub_gridadditional
    End If
End Sub


Private Sub chkwaiver_Click()
    If chkwaiver.Value = 1 Then
        txtAmount.Visible = True
        lblachead.Visible = True
        cmbheader.Visible = True
        lblpercentage.Visible = True
        txtper.Visible = True
        lblwaiveramount.Visible = True
        txtAmount.Visible = True
        cmbheader.SetFocus
    Else
        Call cmdcalc_Click
        txtAmount.Visible = False
        lblachead.Visible = False
        cmbheader.Visible = False
        lblpercentage.Visible = False
        txtper.Visible = False
        lblwaiveramount.Visible = False
        txtAmount.Visible = False
        cmbheader.ListIndex = -1
        txtAmount.Text = ""
        txtper.Text = ""
    End If
End Sub

Private Sub cmdadditional_Click()
        On Error GoTo errorhandler

    Dim blnFound                As Boolean
    Dim intInsertRow            As Integer
    Dim intRow                  As Integer
        
    If Trim(cmbheader.Text) = "" Then
        MsgBox "Account Heads can't be left blank!", vbCritical
        cmbheader.SetFocus
        Exit Sub
    End If
        
    blnFound = False
    
    If blnFound = False Then
        intInsertRow = mshfadditional.Rows - 1
        mshfadditional.Rows = mshfadditional.Rows + 1
    Else
        intInsertRow = intInsertRow
    End If
     
    mshfadditional.RowHeight(intInsertRow) = 285
    mshfadditional.TextMatrix(intInsertRow, 0) = cmbheader.ItemData(cmbheader.ListIndex)
    mshfadditional.TextMatrix(intInsertRow, 1) = Trim(cmbheader.Text)
    mshfadditional.TextMatrix(intInsertRow, 2) = Val(txtadditionalamt.Text)

    If (MsgBox("Do you wish to add more charges?", vbQuestion + vbYesNo + vbDefaultButton1) = vbYes) Then
        txtadditionalamt.Text = ""
        cmbheader.ListIndex = -1
        cmbheader.SetFocus
       ' Call sub_TotalAmt
    Else
        txtadditionalamt.Text = ""
        cmbheader.ListIndex = -1
        cmbheader.SetFocus
       ' Call sub_TotalAmt
    End If
    
errorhandler:
    If Err Then MsgBox Err.Description, vbInformation


End Sub

Private Sub sub_fillAccount()
    On Error GoTo errorhandler

    Dim objRSFill      As ADODB.Recordset
    Dim strSQL          As String
    
    strSQL = "SELECT AccountName, AccountID ,GroupID FROM bond_accountmaster "
    strSQL = strSQL & "WHERE IsActive=1 "
    
    Set objRSFill = New ADODB.Recordset
    objRSFill.Open strSQL, objDBConnection, adOpenStatic, adLockReadOnly
    
    cmbheader.Clear
    Do While Not objRSFill.EOF
        cmbheader.AddItem objRSFill.Fields("AccountName")
        cmbheader.ItemData(cmbheader.NewIndex) = Val(objRSFill.Fields("AccountID"))
        objRSFill.MoveNext
    Loop
    
    If objRSFill.State = 1 Then objRSFill.Close
    Set objRSFill = Nothing
    
errorhandler:
    If Err Then MsgBox Err.Description
End Sub
Private Sub sub_gridadditional()
     
     With mshfadditional
        
        .Clear
        .Cols = 3
        .Rows = 2
        
        .TextMatrix(0, 0) = "AccountID"
        .TextMatrix(0, 1) = "Account Head"
        .TextMatrix(0, 2) = "Amount"
        
        
        .ColAlignmentFixed(0) = flexAlignCenterCenter
        .ColAlignmentFixed(1) = flexAlignCenterCenter
        .ColAlignmentFixed(2) = flexAlignCenterCenter
        
        .ColWidth(0) = 0
        .ColWidth(1) = 2000
        .ColWidth(2) = 900
                
       End With
End Sub

Private Sub txtgstin_KeyDown(KeyCode As Integer, Shift As Integer)
   'cmbcha.SetFocus
   
   If KeyCode = vbKeyReturn Then
        txtgstpartyname.SetFocus
      
    End If
End Sub
