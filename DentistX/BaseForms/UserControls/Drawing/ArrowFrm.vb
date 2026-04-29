Public Class ArrowFrm



    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(hl As Integer, hw As Integer, swb As Integer, swt As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        tbHeadLength.Value = hl
        tbheadWidth.Value = hw
        tbshaftWidthBase.Value = swb
        tbshaftWidthTip.Value = swt
        HeadLength = hl
        HeadWidth = hw
        ShaftWidthBase = swb
        ShaftWidthTip = swt
    End Sub

    Property HeadLength As Integer
    Property HeadWidth As Integer
    Property ShaftWidthBase As Integer
    Property ShaftWidthTip As Integer

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        tbHeadLength.Value = 100
        tbheadWidth.Value = 50
        tbshaftWidthBase.Value = 50
        tbshaftWidthTip.Value = 20
        HeadLength = 100
        HeadWidth = 50
        ShaftWidthBase = 50
        ShaftWidthTip = 20
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        HeadLength = tbHeadLength.Value
        HeadWidth = tbheadWidth.Value
        ShaftWidthBase = tbshaftWidthBase.Value
        ShaftWidthTip = tbshaftWidthTip.Value
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ArrowFrm_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
End Class