Public Class FormTitleHelper
    ''' <summary>Localized module name only (no patient suffix). Matches the base titles used in <see cref="GetFormTitle"/>.</summary>
    Public Shared Function GetModuleTitle(formType As String, isEnglish As Boolean) As String
        Select Case formType
            Case "Treat" : Return If(isEnglish, "🦷✨ Treatments", "🦷✨العلاجات")
            Case "Ortho" : Return If(isEnglish, "🦷✨ Orthodontics", "🦷✨ التقويمات")
            Case "Mobile" : Return If(isEnglish, "🦷✨ Mobile Structure", "🦷✨ العلاجات المتحركة")
            Case "Accounts" : Return If(isEnglish, "🦷✨ Accounts", "🦷✨ الحسابات")
            Case "Visits" : Return If(isEnglish, "🦷✨ Visits", "🦷✨ الزيارات")
            Case "Notes" : Return If(isEnglish, "🦷✨ Notes", "🦷✨ الملاحظات")
            Case "Images" : Return If(isEnglish, "🦷✨ Images", "🦷✨ الصور")
            Case "Diag" : Return If(isEnglish, "🦷✨ Diagnostics", "🦷✨ التشخيصات")
            Case "Rx" : Return If(isEnglish, "🦷✨ Prescriptions", "🦷✨ الوصفات الطبية")
            Case "Appointments Scheduler" : Return If(isEnglish, "🦷✨ Appointments Scheduler", "🦷✨ جدولة المواعيد")
            Case Else : Return If(isEnglish, "Patient", "🦷✨ المريض")
        End Select
    End Function

    Public Shared Function GetFormTitle(formType As String, patient As Patient, isEnglish As Boolean) As String
        Dim baseTitle As String = GetModuleTitle(formType, isEnglish)

        If patient IsNot Nothing Then
            Return If(isEnglish, $"{baseTitle} For: {patient.PatientName}", $"{baseTitle} للمريض: {patient.PatientName}")
        Else
            Return If(isEnglish, $"{baseTitle} - No Patient Selected", $"{baseTitle} - لا يوجد مريض محدد")
        End If
    End Function
End Class