
Partial Class Summary_PopUp
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Convert.ToInt32(Session("UserId_DomCFS")) = 0 Then
            Response.Redirect("../Domestic/Common/SessionExpired.aspx")
      
        End If

    End Sub
End Class

