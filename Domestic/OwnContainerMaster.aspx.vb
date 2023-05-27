Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim VendorID As String
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
            txtPurchaseDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            btnsearch_Click(sender, e)
            Filldropdown()
        End If
    End Sub
    Public Sub Filldropdown()
        ds = db.sub_GetDataSets("usp_own_container_fill")
        If (ds.Tables(0).Rows.Count > 0) Then
            ddlSize.DataSource = ds.Tables(0)
            ddlSize.DataTextField = "ContainerSize"
            ddlSize.DataValueField = "ContainerSizeID"
            ddlSize.DataBind()
            ddlSize.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        If (ds.Tables(1).Rows.Count > 0) Then
            ddltype.DataSource = ds.Tables(1)
            ddltype.DataTextField = "ContainerType"
            ddltype.DataValueField = "ContainerTypeID"
            ddltype.DataBind()
            ddltype.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        If (ds.Tables(2).Rows.Count > 0) Then
            ddlcondtion.DataSource = ds.Tables(2)
            ddlcondtion.DataTextField = "Name"
            ddlcondtion.DataValueField = "ID"
            ddlcondtion.DataBind()
            ddlcondtion.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        If (ds.Tables(3).Rows.Count > 0) Then
            ddlstatus.DataSource = ds.Tables(3)
            ddlstatus.DataTextField = "statusdesc"
            ddlstatus.DataValueField = "statusID"
            ddlstatus.DataBind()
            ddlstatus.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        If (ds.Tables(4).Rows.Count > 0) Then
            ddllocation.DataSource = ds.Tables(4)
            ddllocation.DataTextField = "Location"
            ddllocation.DataValueField = "LocationID"
            ddllocation.DataBind()
            ddllocation.Items.Insert(0, New ListItem("--Select--", 0))
        End If

        If (ds.Tables(5).Rows.Count > 0) Then
            ddlisocode.DataSource = ds.Tables(5)
            ddlisocode.DataTextField = "ISOCode"
            ddlisocode.DataValueField = "ISOID"
            ddlisocode.DataBind()
            ddlisocode.Items.Insert(0, New ListItem("--Select--", 0))
        End If
    End Sub
     
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
      
    

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "usp_search_own'" & Trim(txtsearche.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try

            strSql = ""
            strSql = "select ContainerNo from Own_Ctr_Master where ContainerNo='" & Trim(txtcontainer.Text & "") & "' "
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                txtcontainer.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No already exists .');", True)
                txtcontainer.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql += "USP_INSERT_OWN_CTR_MASTER'" & Trim(txtcontainer.Text & "") & "','" & Trim(ddlSize.SelectedItem.Text & "") & "','" & Trim(ddltype.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlcondtion.SelectedValue & "") & "','" & Trim(ddlstatus.SelectedValue & "") & "','" & Trim(ddllocation.SelectedValue & "") & "',"
            strSql += "'" & Trim(ddlisocode.SelectedValue & "") & "','" & Trim(txtTarewt.Text & "") & "','" & Trim(txtPermissiblewt.Text & "") & "',"
            strSql += "'" & Trim(ddlOwnedBy.SelectedValue & "") & "','" & Convert.ToDateTime(Trim(txtPurchaseDate.Text & "")).ToString("yyyy-MM-dd") & "',"
            strSql += "'" & Trim(txtLeaserName.Text & "") & "','" & Trim(txtleasefrom.Text & "") & "','" & Trim(txtleaseTill.Text & "") & "','" & Session("UserId_DomCFS") & "','" & Trim(chkisActive.Checked & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            lblSession.Text = "Record saved successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
