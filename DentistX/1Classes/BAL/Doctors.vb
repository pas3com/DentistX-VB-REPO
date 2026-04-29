Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration
Imports Dapper

	Public Class Doctors



	Property DrID As Integer

	Property DrName As String

	Property DrAdres As String

	Property DrPhone As String

	Property DrMobile As String

	Property DrColor As String
	Property DoctorClr As Color

	''' <summary>Country / trunk prefix for WhatsApp (e.g. +961), stored as entered.</summary>
	Property WhatsAppPrefix As String
	''' <summary>Local WhatsApp number digits (without prefix).</summary>
	Property WhatsApp As String


	Public Property AppointmentCIEnumerable As IEnumerable(Of AppointmentC)
	Public Property USERSIEnumerable As IEnumerable(Of USERS)

End Class
