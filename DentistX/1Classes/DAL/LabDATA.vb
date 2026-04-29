Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class LabDATA

		Private ConnectionString As String=DentistXDATA.GetConnection.connectionString



		Public Function SelectAll() As IEnumerable(Of Lab)
			Using conn As New SqlConnection(ConnectionString)
			    conn.Open()
			    Return conn.Query(Of Lab)("SELECT * FROM Lab")
			End Using
		End Function
		

		Public Function Select_Record(ByVal clsLab As Lab) As Lab
			Using conn As New SqlConnection(ConnectionString)
			    Dim sql As String = "Select * FROM Lab WHERE LabID = @LabID"
			    Return conn.QuerySingleOrDefault(Of Lab)(sql, New With { .LabID = clsLab.LabID })
			End Using
		End Function

		Public Function Add(ByVal clsLab As Lab) As Boolean
			Dim RowsAffected As Integer=0
			Using conn As New SqlConnection(ConnectionString)
			Dim sql As String = "INSERT INTO Lab (LabName, Adres, Phone, Mobile, WhatsAppPrefix, WhatsApp) VALUES (@LabName, @Adres, @Phone, @Mobile, @WhatsAppPrefix, @WhatsApp)"
			RowsAffected = conn.Execute(sql, New With {.LabName = clsLab.LabName, .Adres = clsLab.Adres, .Phone = clsLab.Phone, .Mobile = clsLab.Mobile, .WhatsAppPrefix = clsLab.WhatsAppPrefix, .WhatsApp = clsLab.WhatsApp})
			Return RowsAffected > 0
			End Using
		End Function


	Public Function Update(oldLab As Lab, newLab As Lab) As Boolean
		Using conn As New SqlConnection(ConnectionString)

        Dim sql As String =
"UPDATE [Lab] SET
    [LabName] = @NewLabName,
    [Adres] = @NewAdres,
    [Phone] = @NewPhone,
    [Mobile] = @NewMobile,
    [WhatsAppPrefix] = @NewWhatsAppPrefix,
    [WhatsApp] = @NewWhatsApp
WHERE
    ([LabName] = @OldLabName OR ([LabName] IS NULL AND @OldLabName IS NULL)) AND
    ([Adres] = @OldAdres OR ([Adres] IS NULL AND @OldAdres IS NULL)) AND
    ([Phone] = @OldPhone OR ([Phone] IS NULL AND @OldPhone IS NULL)) AND
    ([Mobile] = @OldMobile OR ([Mobile] IS NULL AND @OldMobile IS NULL)) AND
    ([WhatsAppPrefix] = @OldWhatsAppPrefix OR ([WhatsAppPrefix] IS NULL AND @OldWhatsAppPrefix IS NULL)) AND
    ([WhatsApp] = @OldWhatsApp OR ([WhatsApp] IS NULL AND @OldWhatsApp IS NULL))"

			Dim parameters = New With {
			.NewLabName = newLab.LabName,
			.NewAdres = newLab.Adres,
			.NewPhone = newLab.Phone,
			.NewMobile = newLab.Mobile,
			.NewWhatsAppPrefix = newLab.WhatsAppPrefix,
			.NewWhatsApp = newLab.WhatsApp,
			.OldLabName = oldLab.LabName,
			.OldAdres = oldLab.Adres,
			.OldPhone = oldLab.Phone,
			.OldMobile = oldLab.Mobile,
			.OldWhatsAppPrefix = oldLab.WhatsAppPrefix,
			.OldWhatsApp = oldLab.WhatsApp
		}

			Dim affectedRows As Integer = conn.Execute(sql, parameters)

        Return affectedRows > 0

    End Using
End Function

	Public Function Update1(oldLab As Lab, newLab As Lab) As Boolean
		Using conn As New SqlConnection(ConnectionString)
			Dim parameters = New With {
					.NewLabName = newLab.LabName, .OldLabName = oldLab.LabName, .NewAdres = newLab.Adres, .OldAdres = oldLab.Adres,
					.NewPhone = newLab.Phone, .OldPhone = oldLab.Phone, .NewMobile = newLab.Mobile, .OldMobile = oldLab.Mobile,
					.NewWhatsAppPrefix = newLab.WhatsAppPrefix, .OldWhatsAppPrefix = oldLab.WhatsAppPrefix, .NewWhatsApp = newLab.WhatsApp,
					.OldWhatsApp = oldLab.WhatsApp
										  }
			Dim affectedRows As Integer = conn.Execute("UPDATE [Lab] SET [LabName] = @NewLabName, [Adres] = @NewAdres, [Phone] = @NewPhone,
														[Mobile] = @NewMobile, [WhatsAppPrefix] =  @NewWhatsAppPrefix, 
														[WhatsApp] = @NewWhatsApp 
													WHERE [LabName] = @OldLabName AND [Adres] = @OldAdres AND 
														[Phone] = @OldPhone AND [Mobile] = @OldMobile AND 
														[WhatsAppPrefix] =  @OldWhatsAppPrefix AND  [WhatsApp] = @OldWhatsApp", parameters)
			Return affectedRows > 0

		End Using
	End Function

	Public Function Delete(ByVal clsLab As Lab) As Boolean
			Dim deleteStatement As String =
			"DELETE FROM [Lab] 
			WHERE LabID = @LabID"
			Using connection As SqlConnection = DentistXData.GetConnection()
			    connection.Open()
			    Dim affectedRows As Integer = connection.Execute(deleteStatement, New With { .LabID = clsLab.LabID })
			    Return affectedRows > 0
			End Using
		End Function


'Methods to get parents and childs
		Public Function GetLabOrder(ByVal clsLab As Lab ) As IEnumerable(Of LabOrder)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [LabOrder] WHERE [LabID] = @LabID"
				Return conn.Query(Of LabOrder)(query, New With { .LabID= clsLab.LabID })
			End Using
		End Function

		Public Function GetLabPay(ByVal clsLab As Lab ) As IEnumerable(Of LabPay)
			Using conn As New SqlConnection(ConnectionString)
				conn.Open()
				Dim query As String = "SELECT * FROM [LabPay] WHERE [LabID] = @LabID"
				Return conn.Query(Of LabPay)(query, New With { .LabID= clsLab.LabID })
			End Using
		End Function

	End Class
