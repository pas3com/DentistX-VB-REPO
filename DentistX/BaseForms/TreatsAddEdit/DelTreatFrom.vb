Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils

Public Class DelTreatFrom

    ' Read-only property for Saved
    Public Property Saved As Boolean
    ' Read-only property for Deleted
    Public Property Deleted As Boolean
    ' Read-only property for _Canceled
    Public Property Canceled As Boolean
    Dim patientTrts As IEnumerable(Of Patient_ToothTrt)
    Dim clsPatient As Patient
    Dim toothnum As List(Of Byte)
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
    End Sub
    Public Sub New(ByVal Patientcls As Patient, ByVal toothnumList As List(Of Byte))
        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        ' Add any initialization after the InitializeComponent() call.
        clsPatient = Patientcls
        toothnum = If(toothnumList, New List(Of Byte))
        If Patientcls IsNot Nothing AndAlso toothnum.Count > 0 Then
            SetBS(Patientcls, toothnum)
        End If
        CreateTreatCheckEdits()
    End Sub
    Private Sub SetBS(ByVal clsPatient As Patient, ByVal toothnum As List(Of Byte))
        If clsPatient Is Nothing OrElse toothnum Is Nothing OrElse toothnum.Count = 0 Then Return
        Dim d As New Patient_ToothTrtDATA
        patientTrts = d.GetPatientTreats(clsPatient.PatientID, toothnum)
        BS.DataSource = patientTrts.ToList
        txtPatientName.Text = clsPatient.PatientName
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub
    Private Sub btnDelTrt_Click(sender As Object, e As EventArgs) Handles btnDelTrt.Click
        If clsPatient Is Nothing Then
            MessageBox.Show("Patient reference is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim toothTrtData = New Patient_ToothTrtDATA()
        Dim ids = GetCheckedToothTrtIDs()

        ' First confirmation (standard MessageBox)
        Dim msgEn As String = "Are you sure you want to delete these " & ids.Count & " treatments?"
        Dim msgAr As String = "هل أنت متأكد أنك تريد حذف هذه " & ids.Count & " العلاجات؟"
        Dim msg As String = If(Eng, msgEn, msgAr)
        Dim titleEn As String = "First Warning"
        Dim titleAr As String = "التحذير الأول"
        Dim title As String = If(Eng, titleEn, titleAr)
        If MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If
        ' Second confirmation (custom dialog)
        Dim confirmMsgEn As String = "FINAL WARNING: This will permanently delete " & ids.Count & " treatments. Check the box to confirm."
        Dim confirmMsgAr As String = "تحذير نهائي: سيؤدي هذا إلى حذف " & ids.Count & " العلاجات بشكل دائم. تحقق من المربع للتأكيد."
        Dim confirmMsg As String = If(Eng, confirmMsgEn, confirmMsgAr)
        Dim confirmTitleEn As String = "Patient Treatments Deletion Form"
        Dim confirmTitleAr As String = "نموذج حذف علاجات المرضى"
        Dim confirmTitle As String = If(Eng, confirmTitleEn, confirmTitleAr)

        Using confirmDialog As New DoubleConfirmDialog() With {.Text = confirmTitle}
            confirmDialog.Message = confirmMsg
            If confirmDialog.ShowDialog(Me) <> DialogResult.OK OrElse Not confirmDialog.IsConfirmed Then
                Return ' Exit if user cancels or doesn't check the box
            End If
        End Using
        ' Proceed with deletion
        'If toothTrtData.DeletePatientTreats(clsPatient, toothnum) Then


        If ids.Count = 0 Then
            MessageBox.Show("No treatments selected.", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If DeletePatientTreatsExact(clsPatient, ids) Then
            Dim successMsgEn As String = "Treatments deleted successfully."
            Dim successMsgAr As String = "تم حذف العلاجات بنجاح."
            Dim successMsg As String = If(Eng, successMsgEn, successMsgAr)
            Dim successTitleEn As String = "Success"
            Dim successTitleAr As String = "نجاح"
            Dim successTitle As String = If(Eng, successTitleEn, successTitleAr)
            MessageBox.Show(successMsg, successTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Deleted = True
            Saved = True
            Me.DialogResult = DialogResult.OK
        Else
            Dim errorMsgEn As String = "Failed to delete treatments."
            Dim errorMsgAr As String = "فشل في حذف العلاجات."
            Dim errorMsg As String = If(Eng, errorMsgEn, errorMsgAr)
            Dim errorTitleEn As String = "Error"
            Dim errorTitleAr As String = "خطأ"
            Dim errorTitle As String = If(Eng, errorTitleEn, errorTitleAr)
            MessageBox.Show(errorMsg, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Deleted = False
            Saved = False
        End If
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim cancelEn As String = "Cancel The Operation."
        Dim cancelAr As String = "إلغاء العملية."
        Dim cancelMsg As String = If(Eng, cancelEn, cancelAr)
        Dim titleEn As String = "Cancel"
        Dim titleAr As String = "إلغاء"
        Dim title As String = If(Eng, titleEn, titleAr)
        MessageBox.Show(cancelMsg, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Me.DialogResult = DialogResult.Cancel
        Saved = False
        Deleted = False
        Canceled = True
    End Sub

    Private Sub DelTreatFromNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub CreateTreatCheckEdits()
        pnl1.AutoScroll = True
        pnl1.SuspendLayout()
        pnl1.Controls.Clear()
        If patientTrts Is Nothing Then Exit Sub
        Dim topPos As Integer = 20
        Dim leftPos As Integer = 10
        ' Order helps readability: tooth → treat
        For Each trt In patientTrts.OrderBy(Function(t) t.ToothNum)
            Dim suffix As String = If(trt.ToothNum = 0, TrtSourceHelper.FormatWholeMouthLabel(Eng), trt.ToothNum.ToString())
            Dim chk As New DevExpress.XtraEditors.CheckEdit With {
            .Checked = True,
            .Left = leftPos,
            .Top = topPos,
            .Tag = trt.ToothTrtID,
            .Text = $"{trt.Treat} ==>> {suffix}",
            .AutoSize = True
        }
            chk.Properties.AutoWidth = True
            chk.AutoSize = True   ' height only, still useful
            pnl1.Controls.Add(chk)
            topPos += chk.Height + 6
        Next
        pnl1.ResumeLayout()
    End Sub
    Private Function BuildTreatCaption(trt As Patient_ToothTrt) As String
        ' Example output:
        ' Tooth 24 - Root Canal
        ' Tooth 24 - Crown (Zirconia)

        If Eng Then
            Return $"Tooth {trt.ToothNum} - {trt.Treat}"
        Else
            Return $"سن {trt.ToothNum} - {trt.Treat}"
        End If
    End Function
    Private Function GetCheckedToothTrtIDs() As List(Of Integer)
        Return pnl1.Controls.
        OfType(Of DevExpress.XtraEditors.CheckEdit)().
        Where(Function(c) c.Checked).
        Select(Function(c) CInt(c.Tag)).
        Distinct().
        ToList()
    End Function
    Public Function DeletePatientTreatsExact(
    clsPatient As Patient,
    toothTrtIDs As List(Of Integer)
) As Boolean

        If clsPatient.PatientID <= 0 Then
            MsgBox("Missing PatientID.")
            Return False
        End If

        If toothTrtIDs Is Nothing OrElse toothTrtIDs.Count = 0 Then
            MsgBox("No treatments specified for deletion.")
            Return False
        End If

        Dim idList As String = String.Join(",", toothTrtIDs)

        Dim deleteStatement As String = "
BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @DeletedFromInvoiceItems INT = 0;
    DECLARE @DeletedFromTrts INT = 0;
    DECLARE @DeletedFromTrtInfo INT = 0;
    DECLARE @DeletedFromToothTrt INT = 0;

    DECLARE @ToothTrtIDs TABLE (ID INT);

    INSERT INTO @ToothTrtIDs
    SELECT value FROM STRING_SPLIT(@ToothTrtIDList, ',');

    -------------------------------------------------
    -- 0. Invoice_Items (must come FIRST)
    -------------------------------------------------
    DELETE FROM Invoice_Items
    WHERE TrtID IN (
        SELECT TrtID
        FROM Patient_Trts
        WHERE ToothTrtID IN (SELECT ID FROM @ToothTrtIDs)
          AND PatientID = @PatientID
    );
    SET @DeletedFromInvoiceItems = @@ROWCOUNT;

    -------------------------------------------------
    -- 1. Patient_Trts
    -------------------------------------------------
    DELETE FROM Patient_Trts
    WHERE ToothTrtID IN (SELECT ID FROM @ToothTrtIDs)
      AND PatientID = @PatientID;
    SET @DeletedFromTrts = @@ROWCOUNT;

    -------------------------------------------------
    -- 2. Patient_TrtInfo (schema-faithful)
    -------------------------------------------------
    DELETE FROM Patient_TrtInfo
    WHERE PatientID = @PatientID
      AND (
            ParentToothTrtID IN (SELECT ID FROM @ToothTrtIDs)
            OR ToothNum IN (
                SELECT ToothNum
                FROM Patient_ToothTrt
                WHERE ToothTrtID IN (SELECT ID FROM @ToothTrtIDs)
            )
          );
    SET @DeletedFromTrtInfo = @@ROWCOUNT;

    -------------------------------------------------
    -- 3. Patient_ToothTrt
    -------------------------------------------------
    DELETE FROM Patient_ToothTrt
    WHERE ToothTrtID IN (SELECT ID FROM @ToothTrtIDs)
      AND PatientID = @PatientID;
    SET @DeletedFromToothTrt = @@ROWCOUNT;

    COMMIT TRANSACTION;

    SELECT CASE
        WHEN (@DeletedFromInvoiceItems
            + @DeletedFromTrts
            + @DeletedFromTrtInfo
            + @DeletedFromToothTrt) > 0
        THEN 1 ELSE 0 END AS Success;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH
"

        Using connection As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            connection.Open()
            Try
                Dim result = connection.ExecuteScalar(Of Integer)(
                deleteStatement,
                New With {
                    .PatientID = clsPatient.PatientID,
                    .ToothTrtIDList = idList
                })

                Return result > 0

            Catch ex As Exception
                MsgBox($"Error deleting treatments: {ex.Message}")
                Return False
            End Try
        End Using
    End Function

End Class