Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Text

Public Module CursorPcTrialsRepository
    Private ReadOnly connStr As String = DentistXDATA.GetConnection.ConnectionString

    Public Function HasTrialRow(fingerprintHash As String) As Boolean
        Try
            Using cn As New SqlConnection(connStr)
                cn.Open()
                Using cmd As New SqlCommand("SELECT COUNT(*) FROM PC_Trials WHERE FingerprintHash = @fp", cn)
                    cmd.Parameters.Add("@fp", SqlDbType.NVarChar, 128).Value = fingerprintHash
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine($"HasTrialRow error: {ex.Message}")
            Return False
        End Try
    End Function

    Public Function DeleteTrialRowForFingerprint(fingerprintHash As String) As Boolean
        Try
            Using cn As New SqlConnection(connStr)
                cn.Open()
                Using cmd As New SqlCommand("DELETE FROM PC_Trials WHERE FingerprintHash = @fp", cn)
                    cmd.Parameters.Add("@fp", SqlDbType.NVarChar, 128).Value = fingerprintHash
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            Debug.WriteLine($"DeleteTrialRowForFingerprint error: {ex.Message}")
            Return False
        End Try
    End Function

    Public Function UpsertTrialState(state As CursorTrialState, fingerprintRaw As String, updatedBy As String) As Boolean
        Try
            Using cn As New SqlConnection(connStr)
                cn.Open()
                Dim exists As Boolean
                Using checkCmd As New SqlCommand("SELECT COUNT(*) FROM PC_Trials WHERE FingerprintHash = @fp", cn)
                    checkCmd.Parameters.Add("@fp", SqlDbType.NVarChar, 128).Value = state.FingerprintHash
                    exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
                End Using

                Dim encFpData = CursorCrypto.EncryptBytes(Encoding.UTF8.GetBytes(fingerprintRaw))
                Dim encStart = CursorCrypto.EncryptBytes(Encoding.UTF8.GetBytes(state.StartDate.ToString("dd/MM/yyyy")))
                Dim encEnd = CursorCrypto.EncryptBytes(Encoding.UTF8.GetBytes(state.EndDate.ToString("dd/MM/yyyy")))
                Dim encLast = CursorCrypto.EncryptBytes(Encoding.UTF8.GetBytes(state.LastCheckDate.ToString("dd/MM/yyyy")))

                If exists Then
                    Using cmd As New SqlCommand(
                        "UPDATE PC_Trials SET EndDate=@end, LastCheckDate=@last, LastExtendedBy=@by, " &
                        "LastExtensionDate=@dt, UpdatedAt=@updated WHERE FingerprintHash=@fp", cn)
                        cmd.Parameters.Add("@end", SqlDbType.VarBinary, -1).Value = encEnd
                        cmd.Parameters.Add("@last", SqlDbType.VarBinary, -1).Value = encLast
                        cmd.Parameters.Add("@by", SqlDbType.NVarChar, 100).Value = updatedBy
                        cmd.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTime.UtcNow
                        cmd.Parameters.Add("@updated", SqlDbType.DateTime).Value = DateTime.Now
                        cmd.Parameters.Add("@fp", SqlDbType.NVarChar, 128).Value = state.FingerprintHash
                        cmd.ExecuteNonQuery()
                    End Using
                Else
                    Using cmd As New SqlCommand(
                        "INSERT INTO PC_Trials " &
                        "(FingerprintHash, FingerprintData, StartDate, EndDate, LastCheckDate, " &
                        "LastExtendedBy, LastExtensionDate, CreatedAt, UpdatedAt, IsBlocked) " &
                        "VALUES (@fp, @fpdata, @start, @end, @last, @by, @dt, @created, @updated, @blocked)", cn)
                        cmd.Parameters.Add("@fp", SqlDbType.NVarChar, 128).Value = state.FingerprintHash
                        cmd.Parameters.Add("@fpdata", SqlDbType.VarBinary, -1).Value = encFpData
                        cmd.Parameters.Add("@start", SqlDbType.VarBinary, -1).Value = encStart
                        cmd.Parameters.Add("@end", SqlDbType.VarBinary, -1).Value = encEnd
                        cmd.Parameters.Add("@last", SqlDbType.VarBinary, -1).Value = encLast
                        cmd.Parameters.Add("@by", SqlDbType.NVarChar, 100).Value = updatedBy
                        cmd.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTime.UtcNow
                        cmd.Parameters.Add("@created", SqlDbType.DateTime).Value = DateTime.Now
                        cmd.Parameters.Add("@updated", SqlDbType.DateTime).Value = DateTime.Now
                        cmd.Parameters.Add("@blocked", SqlDbType.Bit).Value = state.IsBlocked
                        cmd.ExecuteNonQuery()
                    End Using
                End If
            End Using

            Return True
        Catch ex As Exception
            Debug.WriteLine($"UpsertTrialState error: {ex.Message}")
            Return False
        End Try
    End Function
End Module
