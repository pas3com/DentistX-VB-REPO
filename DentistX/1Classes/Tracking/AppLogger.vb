Public Class AppLogger
    Public Shared Sub Log(loggedBy As String, patientName As String, tableName As String, action As String, Optional info As String = Nothing)
        Dim logger As New PatientHistoryDATA()
        logger.LogHistory(loggedBy, patientName, tableName, action, info)

    End Sub
    Public Shared Sub LogH(loggedBy As String, recordName As String, tableName As String, action As String, Optional info As String = Nothing)
        Dim logger As New PatientHistoryDATA()
        logger.LogHistory(loggedBy, recordName, tableName, action, info)
    End Sub

    Public Shared Sub LogAud(ActionType As String, tableName As String, RecordID As String, OldValue As String, NewValue As String, ChangedBy As String, Optional info As String = Nothing)
        Dim audit As New AuditLogEntryDATA
        Dim clsAudit As New AuditLogEntry With {.ActionType = ActionType, .TableName = tableName, .RecordID = RecordID,
                                                .OldValue = OldValue, .NewValue = NewValue, .ChangedBy = ChangedBy, .ChangeDate = Now}

        audit.Add(clsAudit)
    End Sub



End Class


'[ActionType], [TableName], [RecordID], [OldValue], [NewValue], [ChangedBy], [ChangeDate]


'    ' After inserting a new patient
'    Dim logger As New PatientHistoryLogDATA()
'Insert
'logger.LogHistory(newPatient.PatientID, newPatient.Name, "Patient", "Insert")
'Update
'logger.LogHistory(patient.PatientID, patient.Name, "Patient", "Update", "Changed address and phone")
'Delete
'logger.LogHistory(patient.PatientID, patient.Name, "Patient", "Delete", "Deleted due to duplication")
'View
'logger.LogHistory(patient.PatientID, patient.Name, "Patient", "View")
