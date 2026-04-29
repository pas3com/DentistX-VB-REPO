Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class AppointmentsDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Appointments)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Appointments)("SELECT * FROM Appointments")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsAppointments As Appointments) As Appointments
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Appointments WHERE UniqueID = @UniqueID"
			    Return conn.QuerySingleOrDefault(Of Appointments)(sql, New With { .UniqueID = clsAppointments.UniqueID })
			End Using
		End Function

		Public Function Add(ByVal clsAppointments As Appointments) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "INSERT INTO Appointments (Type, StartDate, EndDate, QueryStartDate, QueryEndDate, AllDay, Subject, Location, Description, Status, Label, ResourceID, ResourceIDs, ReminderInfo, RecurrenceInfo, TimeZoneId, CustomField1, PatientID) VALUES (@Type, @StartDate, @EndDate, @QueryStartDate, @QueryEndDate, @AllDay, @Subject, @Location, @Description, @Status, @Label, @ResourceID, @ResourceIDs, @ReminderInfo, @RecurrenceInfo, @TimeZoneId, @CustomField1, @PatientID)" 
			    RowsAffected = conn.Execute(sql, New With { .Type =  clsAppointments.Type, .StartDate =  clsAppointments.StartDate, .EndDate =  clsAppointments.EndDate, .QueryStartDate =  clsAppointments.QueryStartDate, .QueryEndDate =  clsAppointments.QueryEndDate, .AllDay =  clsAppointments.AllDay, .Subject =  clsAppointments.Subject, .Location =  clsAppointments.Location, .Description =  clsAppointments.Description, .Status =  clsAppointments.Status, .Label =  clsAppointments.Label, .ResourceID =  clsAppointments.ResourceID, .ResourceIDs =  clsAppointments.ResourceIDs, .ReminderInfo =  clsAppointments.ReminderInfo, .RecurrenceInfo =  clsAppointments.RecurrenceInfo, .TimeZoneId =  clsAppointments.TimeZoneId, .CustomField1 =  clsAppointments.CustomField1, .PatientID =  clsAppointments.PatientID })
			    Return RowsAffected > 0
			End Using
		End Function

		Public Function Update(oldAppointments As Appointments, newAppointments As Appointments) As Boolean
			Using conn As New SqlConnection(ConnectionString)
			    Dim parameters = New With { 
					.NewType = newAppointments.Type, .OldType = oldAppointments.Type, .NewStartDate = newAppointments.StartDate, .OldStartDate = oldAppointments.StartDate, .NewEndDate = newAppointments.EndDate, .OldEndDate = oldAppointments.EndDate, .NewQueryStartDate = newAppointments.QueryStartDate, .OldQueryStartDate = oldAppointments.QueryStartDate, .NewQueryEndDate = newAppointments.QueryEndDate, .OldQueryEndDate = oldAppointments.QueryEndDate, .NewAllDay = newAppointments.AllDay, .OldAllDay = oldAppointments.AllDay, .NewSubject = newAppointments.Subject, .OldSubject = oldAppointments.Subject, .NewLocation = newAppointments.Location, .OldLocation = oldAppointments.Location, .NewDescription = newAppointments.Description, .OldDescription = oldAppointments.Description, .NewStatus = newAppointments.Status, .OldStatus = oldAppointments.Status, .NewLabel = newAppointments.Label, .OldLabel = oldAppointments.Label, .NewResourceID = newAppointments.ResourceID, .OldResourceID = oldAppointments.ResourceID, .NewResourceIDs = newAppointments.ResourceIDs, .OldResourceIDs = oldAppointments.ResourceIDs, .NewReminderInfo = newAppointments.ReminderInfo, .OldReminderInfo = oldAppointments.ReminderInfo, .NewRecurrenceInfo = newAppointments.RecurrenceInfo, .OldRecurrenceInfo = oldAppointments.RecurrenceInfo, .NewTimeZoneId = newAppointments.TimeZoneId, .OldTimeZoneId = oldAppointments.TimeZoneId, .NewCustomField1 = newAppointments.CustomField1, .OldCustomField1 = oldAppointments.CustomField1, .NewPatientID = newAppointments.PatientID, .OldPatientID = oldAppointments.PatientID
										  }
			    Dim affectedRows As Integer = conn.Execute("UPDATE [Appointments] SET [Type] = @NewType, [StartDate] = @NewStartDate, [EndDate] = @NewEndDate, [QueryStartDate] = @NewQueryStartDate, [QueryEndDate] = @NewQueryEndDate, [AllDay] = @NewAllDay, [Subject] = @NewSubject, [Location] = @NewLocation, [Description] = @NewDescription, [Status] = @NewStatus, [Label] = @NewLabel, [ResourceID] = @NewResourceID, [ResourceIDs] = @NewResourceIDs, [ReminderInfo] = @NewReminderInfo, [RecurrenceInfo] = @NewRecurrenceInfo, [TimeZoneId] = @NewTimeZoneId, [CustomField1] = @NewCustomField1, [PatientID] = @NewPatientID WHERE [Type] = @OldType AND [StartDate] = @OldStartDate AND [EndDate] = @OldEndDate AND [QueryStartDate] = @OldQueryStartDate AND [QueryEndDate] = @OldQueryEndDate AND [AllDay] = @OldAllDay AND [Subject] = @OldSubject AND [Location] = @OldLocation AND [Description] = @OldDescription AND [Status] = @OldStatus AND [Label] = @OldLabel AND [ResourceID] = @OldResourceID AND [ResourceIDs] = @OldResourceIDs AND [ReminderInfo] = @OldReminderInfo AND [RecurrenceInfo] = @OldRecurrenceInfo AND [TimeZoneId] = @OldTimeZoneId AND [CustomField1] = @OldCustomField1 AND [PatientID] = @OldPatientID", parameters)
			    Return affectedRows > 0
			End Using
		End Function

		Public Function Delete(ByVal clsAppointments As Appointments) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Appointments] 
			WHERE UniqueID = @UniqueID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .UniqueID = clsAppointments.UniqueID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
	End Class
