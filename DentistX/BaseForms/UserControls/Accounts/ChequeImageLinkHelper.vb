Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

''' <summary>Which physical file to create for a cheque scan (front/back sides).</summary>
Public Enum ChequeImageFace
    ''' <summary>Legacy single file: Pay{id}_Chq{num}{ext} without side suffix.</summary>
    Unspecified = 0
    Front = 1
    Back = 2
End Enum

''' <summary>
''' Resolves cheque scan file paths under Images\Patient{id}\Cheques — same rules as <see cref="NewAccounting"/>.
''' Preferred: Pay{PayID}_Chq* with optional _Front / _Back; legacy: Chq{ChqNumber}*.
''' </summary>
Public NotInheritable Class ChequeImageLinkHelper
    Private Shared ReadOnly ChequeImageExtensions As String() = {".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff"}

    Private Sub New()
    End Sub

    Public Shared Function GetPatientChequesFolder(patientId As Integer) As String
        If patientId <= 0 Then Return ""
        Dim appDir = Application.StartupPath
        If String.IsNullOrEmpty(appDir) Then Return ""
        Return Path.Combine(appDir, "Images", "Patient" & patientId.ToString(), "Cheques")
    End Function

    Private Shared Function IsChequeImageFile(path As String) As Boolean
        Dim ext = IO.Path.GetExtension(path)
        If String.IsNullOrEmpty(ext) Then Return False
        Return ChequeImageExtensions.Contains(ext.ToLowerInvariant())
    End Function

    Private Shared Function NormalizeChqNumber(raw As String) As String
        Dim t = If(raw, "").Trim()
        If t.Length = 0 Then Return Nothing
        If t.Length > 10 Then t = t.Substring(0, 10)
        Return t
    End Function

    Private Shared Function SafeChequeFileNamePart(raw As String) As String
        Dim t = If(raw, "").Trim()
        For Each c In Path.GetInvalidFileNameChars()
            t = t.Replace(c, "_"c)
        Next
        If t.Length > 48 Then t = t.Substring(0, 48)
        Return If(t.Length = 0, "Chq", t)
    End Function

    Public Const SideSuffixFront As String = "_Front"
    Public Const SideSuffixBack As String = "_Back"

    ''' <summary>File name only (no directory): Pay{id}_Chq{safePart}_Front.ext / _Back / or legacy without side.</summary>
    Public Shared Function BuildLinkedChequeFileName(payId As Integer, safeChqPart As String, ext As String, face As ChequeImageFace) As String
        If String.IsNullOrWhiteSpace(ext) Then ext = ".jpg"
        Dim baseName = "Pay" & payId.ToString() & "_Chq" & safeChqPart
        Select Case face
            Case ChequeImageFace.Front
                Return baseName & SideSuffixFront & ext
            Case ChequeImageFace.Back
                Return baseName & SideSuffixBack & ext
            Case Else
                Return baseName & ext
        End Select
    End Function

    ''' <summary>Sort key: 0 = front (_Front or _A), 1 = back (_Back or _B), 2 = legacy / other.</summary>
    Private Shared Function SideSortKey(fileName As String) As Integer
        Dim fn = Path.GetFileNameWithoutExtension(fileName)
        If fn.EndsWith(SideSuffixFront, StringComparison.OrdinalIgnoreCase) Then Return 0
        If fn.EndsWith(SideSuffixBack, StringComparison.OrdinalIgnoreCase) Then Return 1
        If fn.EndsWith("_A", StringComparison.OrdinalIgnoreCase) Then Return 0
        If fn.EndsWith("_B", StringComparison.OrdinalIgnoreCase) Then Return 1
        Return 2
    End Function

    ''' <summary>
    ''' Copies one scanned file to Pay{PayID}_Chq… under the patient Cheques folder (same rules as <see cref="FrmChqPayAccnt"/>).
    ''' </summary>
    ''' <returns>Full path of the linked file.</returns>
    Public Shared Function CopyScannedToLinkedCheque(patientId As Integer, payId As Integer, chqNumberRaw As String, sourcePath As String, Optional face As ChequeImageFace = ChequeImageFace.Front) As String
        If payId <= 0 Then Throw New InvalidOperationException("Invalid payment")
        If patientId <= 0 Then Throw New InvalidOperationException("Invalid patient")
        If String.IsNullOrWhiteSpace(sourcePath) OrElse Not File.Exists(sourcePath) Then Throw New InvalidOperationException("Invalid scan file")
        Dim n = NormalizeChqNumber(chqNumberRaw)
        If String.IsNullOrWhiteSpace(n) Then Throw New InvalidOperationException("Cheque number required")
        Dim ext = Path.GetExtension(sourcePath)
        If String.IsNullOrWhiteSpace(ext) Then ext = ".jpg"
        Dim folder = GetPatientChequesFolder(patientId)
        If String.IsNullOrWhiteSpace(folder) Then Throw New InvalidOperationException("Invalid folder")
        If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)
        Dim dest = Path.Combine(folder, BuildLinkedChequeFileName(payId, SafeChequeFileNamePart(n), ext, face))
        File.Copy(sourcePath, dest, overwrite:=True)
        Return dest
    End Function

    ''' <summary>All image files linked to this cheque (Front, then Back, then legacy unsuffixed, then name order).</summary>
    Public Shared Function FindLinkedChequeImagePaths(payId As Integer, patientId As Integer, chqNumber As String) As List(Of String)
        If payId <= 0 OrElse patientId <= 0 Then Return New List(Of String)
        Dim folder = GetPatientChequesFolder(patientId)
        If String.IsNullOrWhiteSpace(folder) OrElse Not Directory.Exists(folder) Then Return New List(Of String)
        Dim candidates = Directory.EnumerateFiles(folder).Where(AddressOf IsChequeImageFile).ToList()
        Dim prefixPay = "Pay" & payId.ToString() & "_Chq"
        Dim payMatches = candidates.Where(Function(f) Path.GetFileName(f).StartsWith(prefixPay, StringComparison.OrdinalIgnoreCase)).ToList()
        If payMatches.Count > 0 Then
            Return payMatches.
                OrderBy(Function(f) SideSortKey(Path.GetFileName(f))).
                ThenBy(Function(f) Path.GetFileName(f), StringComparer.OrdinalIgnoreCase).
                ToList()
        End If
        Dim chqRaw = If(chqNumber, "").Trim()
        If chqRaw.Length = 0 Then Return New List(Of String)
        Dim baseName = "Chq" & chqRaw
        Return candidates.Where(Function(f) Path.GetFileName(f).StartsWith(baseName, StringComparison.OrdinalIgnoreCase)).
            OrderBy(Function(f) Path.GetFileName(f), StringComparer.OrdinalIgnoreCase).
            ToList()
    End Function

    Public Shared Function FindLinkedChequeImagePath(payId As Integer, patientId As Integer, chqNumber As String) As String
        Dim lst = FindLinkedChequeImagePaths(payId, patientId, chqNumber)
        Return If(lst.Count > 0, lst(0), Nothing)
    End Function
End Class
