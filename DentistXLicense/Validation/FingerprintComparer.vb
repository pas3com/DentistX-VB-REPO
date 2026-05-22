Public Class FingerprintComparer

    Private Const MAX_SCORE As Integer = 100

    Public Function Compare(stored As MachineFingerprint,
                            current As MachineFingerprint) As FingerprintMatchResult

        Dim result As New FingerprintMatchResult With {
            .MaxScore = MAX_SCORE,
            .Score = 100
        }

        ' --- CORE (HARD FAIL) ---
        If stored.CpuId <> current.CpuId Then
            result.IsMatch = False
            result.Mismatches.Add("CPU ID mismatch")
            Return result
        End If

        If stored.MotherboardSerial <> current.MotherboardSerial Then
            result.IsMatch = False
            result.Mismatches.Add("Motherboard mismatch")
            Return result
        End If

        If stored.MachineSid <> current.MachineSid Then
            result.IsMatch = False
            result.Mismatches.Add("Machine SID mismatch")
            Return result
        End If

        ' --- SOFT (WEIGHTED) ---
        DeductIfDifferent(result, stored.DiskSerial, current.DiskSerial, 20, "Disk changed")
        DeductIfDifferent(result, stored.BiosVersion, current.BiosVersion, 10, "BIOS changed")
        DeductIfDifferent(result, stored.HostName, current.HostName, 5, "Hostname changed")
        DeductIfDifferent(result, stored.OsInstallDate, current.OsInstallDate, 5, "OS reinstall")

        If Not CompareMacs(stored.MacAddresses, current.MacAddresses) Then
            result.Score -= 15
            result.Mismatches.Add("MAC addresses changed")
        End If

        result.IsMatch = (result.Score >= 70)
        Return result
    End Function

    Private Sub DeductIfDifferent(result As FingerprintMatchResult,
                                 a As String,
                                 b As String,
                                 points As Integer,
                                 reason As String)

        If a <> b Then
            result.Score -= points
            result.Mismatches.Add(reason)
        End If
    End Sub

    Private Function CompareMacs(a As List(Of String), b As List(Of String)) As Boolean
        If a Is Nothing OrElse b Is Nothing Then Return False
        Return a.Intersect(b).Any()
    End Function

End Class
