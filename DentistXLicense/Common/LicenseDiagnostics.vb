Public Class LicenseDiagnostics

    Public Shared Function ExplainFingerprint(result As FingerprintMatchResult) As String
        If result.IsMatch Then
            Return $"Match OK ({result.Score}/{result.MaxScore})"
        End If

        Return "Mismatch: " & String.Join("; ", result.Mismatches)
    End Function

End Class
