Imports System.Collections.Generic
Imports System.Linq

''' <summary>Maps shell control names to Forms.FormName (typically the WinForms class name).</summary>
Public Module MainViewFormAccessMap

    Private ReadOnly BarMap As Dictionary(Of String, String) = BuildBarMap()
    Private ReadOnly AccMap As Dictionary(Of String, String) = BuildAccMap()

    Private Function BuildBarMap() As Dictionary(Of String, String)
        Dim cmp = StringComparer.OrdinalIgnoreCase
        Return New Dictionary(Of String, String)(cmp) From {
            {"ListSettingsMnu", "FrmSettings"},
            {"ListCitiesMnu", "Frm_TblCities"},
            {"ListHealthMnu", "Frm_Health"},
            {"ListTreatsMnu", "FrmTblTRTS"},
            {"ListWireTypeMnu", "FrmTblWireType"},
            {"ListWireMeasureMnu", "FrmTblMeasure"},
            {"ListVisitTypeMnu", "FrmVisitTypes"},
            {"ListRxDetailsMnu", "FrmRxDetails"},
            {"BtnMedic", "MedicFormDS"},
            {"BtnDashCreate", "FrmExchMny"},
            {"BtnFinancePass", "FrmChngFinancePass"},
            {"btnRxFly", "PatientRXFlyFrm"},
            {"btnListLabs", "FrmLab"},
            {"btnLabOrder", "FrmLabOrder2"},
            {"btnRecieveOrder", "FrmRecieveLabOrder"},
            {"btnLabPay", "FrmLabPay"},
            {"btnListPatients", "FrmPatient"},
            {"btnPatientsDebts", "FrmPatientDebts"},
            {"RibMenuDocs", "FrmDoctors"},
            {"btnSecretaries", "FrmSecretaries"},
            {"btnEmployees", "FrmEmp"},
            {"btnAbout", "FormAbout"},
            {"btnClinicInfo", "FrmClinic"},
            {"BtnListVendors", "StockHubForm"},
            {"btnCheuqes", "Frm5DueChqs"},
            {"btnWhatsApp", "FrmWhatsHub"},
            {"btnApptSend", "FrmApptsWhats"},
            {"btnWhatsAppActivityLog", "FrmWhatsAppActivityLog"},
            {"btnApptsReminder", "FrmApptsReminder"},
            {"btnAccountWhats", "FrmAccountWhats"},
            {"btnAccountReminder", "FrmAccountReminderSchedule"},
            {"btnSettings", "FormSettings"},
            {"btnStaffMange", "StaffHubForm"},
            {"btnLast10Patients", "FrmLastPatients"},
            {"TodayButton", "TodayApptEditorForm"},
            {"ListBackupMnu", "FormBackup"},
            {"ListRestoreMnu", "FormRestoreAdv"}
        }
    End Function

    Private Function BuildAccMap() As Dictionary(Of String, String)
        Dim cmp = StringComparer.OrdinalIgnoreCase
        Return New Dictionary(Of String, String)(cmp) From {
            {"TreatsButton", "TreatsUserControl"},
            {"OrthoButton", "FullOrthoTreating"},
            {"DiagButton", "DiagUserControl"},
            {"ScheduleAdmin", "SchedulerFull"},
            {"AccountsButton", "NewAccounting"},
            {"VisitsButton", "PatientVisitsCtl"},
            {"NotesButton", "CtlNotes"},
            {"RxButton", "RxCTLNEW"},
            {"ImagesButton", "ThumbNailViewer"}
        }
    End Function

    Public Function TryGetFormForBarItem(barItemName As String) As String
        If String.IsNullOrEmpty(barItemName) Then Return Nothing
        Dim v As String = Nothing
        If BarMap.TryGetValue(barItemName, v) Then Return v
        Return Nothing
    End Function

    ''' <summary>
    ''' Many <see cref="DevExpress.XtraBars.Navigation.AccordionControlElement"/> names (e.g. btnListPatients)
    ''' are stored in <see cref="BarMap"/> for historical reasons; accordion apply must resolve them too.
    ''' </summary>
    Public Function TryGetFormForAccordion(elementName As String) As String
        If String.IsNullOrEmpty(elementName) Then Return Nothing
        Dim v As String = Nothing
        If AccMap.TryGetValue(elementName, v) Then Return v
        If BarMap.TryGetValue(elementName, v) Then Return v
        Return Nothing
    End Function

    ''' <summary>Distinct form class names referenced by the main shell (ribbon + accordion maps), for registry sync.</summary>
    Public Function GetAllDistinctShellFormTypeNames() As List(Of String)
        Dim hs As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each kv In BarMap
            If Not String.IsNullOrWhiteSpace(kv.Value) Then hs.Add(kv.Value.Trim())
        Next
        For Each kv In AccMap
            If Not String.IsNullOrWhiteSpace(kv.Value) Then hs.Add(kv.Value.Trim())
        Next
        Return hs.OrderBy(Function(s) s).ToList()
    End Function

End Module
