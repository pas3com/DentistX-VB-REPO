Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils

Public Class DelDiagFrom

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon

        ' Add any initialization after the InitializeComponent() call.
        ' One Stage Size 385, 327
        'Multi Stage Size 911, 327
    End Sub

    Public Sub New(ByVal Patientcls As Patient, ByVal toothnumList As List(Of Byte))

        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        ' Add any initialization after the InitializeComponent() call.
        If toothnumList.Count > 0 Then
            SetBS(Patientcls, toothnumList)
        End If
        clsPatient = New Patient
        clsPatient = Patientcls
        toothnum = toothnumList
    End Sub



    Dim patientTrts As IEnumerable(Of Patient_Diagnosis)
    Dim clsPatient As Patient
    Dim toothnum As List(Of Byte)


    Private Sub SetBS(ByVal clsPatient As Patient, ByVal toothnum As List(Of Byte))
        If toothnum.Count > 0 Then
            Dim d As New Patient_DiagnosisDATA
            patientTrts = d.GetPatientTreats(clsPatient.PatientID, toothnum)
            BS.DataSource = patientTrts.ToList
            txtPatientName.Text = clsPatient.PatientName
        End If
    End Sub



    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub


    Private Sub btnDelTrt_Click(sender As Object, e As EventArgs) Handles btnDelTrt.Click
        Dim toothTrtData = New Patient_DiagnosisDATA()

        ' First confirmation (standard MessageBox)
        Dim msgEn As String = "Are you sure you want to delete these " & BS.Count & " Diagnosis?"
        Dim msgAr As String = "هل أنت متأكد أنك تريد حذف هذه " & BS.Count & " التشخيصات؟"
        Dim msg As String = If(Eng, msgEn, msgAr)
        Dim titleEn As String = "First Warning"
        Dim titleAr As String = "التحذير الأول"
        Dim title As String = If(Eng, titleEn, titleAr)
        If MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        ' Second confirmation (custom dialog)
        Dim confirmMsgEn As String = "FINAL WARNING: This will permanently delete " & toothnum.Count & " Diagnosis. Check the box to confirm."
        Dim confirmMsgAr As String = "تحذير نهائي: سيؤدي هذا إلى حذف " & toothnum.Count & " التشخيصات بشكل دائم. تحقق من المربع للتأكيد."
        Dim confirmMsg As String = If(Eng, confirmMsgEn, confirmMsgAr)
        Dim confirmTitleEn As String = "Patient Diagnosis Deletion Form"
        Dim confirmTitleAr As String = "نموذج حذف تشخيصات المرضى"
        Dim confirmTitle As String = If(Eng, confirmTitleEn, confirmTitleAr)

        Using confirmDialog As New DoubleConfirmDialog() With {.Text = confirmTitle}
            confirmDialog.Message = confirmMsg
            If confirmDialog.ShowDialog(Me) <> DialogResult.OK OrElse Not confirmDialog.IsConfirmed Then
                Return ' Exit if user cancels or doesn't check the box
            End If
        End Using

        ' Proceed with deletion
        If toothTrtData.DeletePatientTreats(clsPatient, toothnum) Then
            Dim successMsgEn As String = "Diagnosis deleted successfully."
            Dim successMsgAr As String = "تم حذف التشخيصات بنجاح."
            Dim successMsg As String = If(Eng, successMsgEn, successMsgAr)
            Dim successTitleEn As String = "Success"
            Dim successTitleAr As String = "نجاح"
            Dim successTitle As String = If(Eng, successTitleEn, successTitleAr)
            MessageBox.Show(successMsg, successTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Deleted = True
            Saved = True
            Me.DialogResult = DialogResult.OK
        Else
            Dim errorMsgEn As String = "Failed to delete Diagnosis."
            Dim errorMsgAr As String = "فشل في حذف التشخيصات."
            Dim errorMsg As String = If(Eng, errorMsgEn, errorMsgAr)
            Dim errorTitleEn As String = "Error"
            Dim errorTitleAr As String = "خطأ"
            Dim errorTitle As String = If(Eng, errorTitleEn, errorTitleAr)
            MessageBox.Show(errorMsg, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Deleted = False
            Saved = False
        End If
    End Sub

    'Private Sub btnDelTrt_Click(sender As Object, e As EventArgs) Handles btnDelTrt.Click
    '    Dim toothTrtData = New Patient_DiagnosisDATA()
    '    If MessageBox.Show("Are you sure you want to delete these " & toothnum.Count & " treatments from Treats?", "Delete", MessageBoxButtons.YesNo) = DialogResult.Yes Then
    '        If toothTrtData.DeletePatientTreats(clsPatient, toothnum) Then
    '            MessageBox.Show("Treatments deleted and saved successfully.", "Success")
    '            Deleted = True
    '            Saved = True
    '            Me.DialogResult = DialogResult.OK
    '        Else
    '            MessageBox.Show("Failed to delete treatment.", "Error")
    '            Deleted = False
    '            Saved = False
    '        End If
    '    End If
    'End Sub

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

    ' Read-only property for Saved
    Public Property Saved As Boolean


    ' Read-only property for Deleted
    Public Property Deleted As Boolean


    ' Read-only property for _Canceled
    Public Property Canceled As Boolean





End Class