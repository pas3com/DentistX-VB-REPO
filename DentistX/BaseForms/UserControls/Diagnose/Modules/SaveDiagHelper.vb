Imports System.Data.SqlClient
Imports Dapper
Imports DevExpress.XtraEditors

Module SaveDiagHelper

    Private selectedTeethList As New List(Of Byte)
    Private selectedToothTrtList As New List(Of Patient_Diagnosis)
    Dim _shapeId As Integer = 0
    Dim _propName As String = ""
    Dim toothTrtData As New Patient_DiagnosisDATA
    Dim _toothTrt As New Patient_Diagnosis
    Dim external As Boolean = False
    Dim paid As Boolean = False
    Dim loaded As Boolean = False

    Public Class SaveDiag
        Implements IDisposable
        'implement idisposable now



        Private selectedTeethList As New List(Of Byte)
        Private selectedToothTrtList As New List(Of Patient_Diagnosis)
        Dim _shapeId As Integer = 0
        Dim _propName As String = ""
        Dim toothTrtData As New Patient_DiagnosisDATA
        Dim _toothTrt As New Patient_Diagnosis
        Dim external As Boolean = False
        Dim paid As Boolean = False
        Dim loaded As Boolean = False
        Public Sub New()

        End Sub
        Public Sub New(ByVal clsToothTrt As List(Of Patient_Diagnosis), ByVal clsPatient As Patient)
            ' This call is required by the designer.

            loaded = False

            selectedToothTrtList = clsToothTrt
            _toothTrt = clsToothTrt(0)

            _propName = clsToothTrt(0).PropertyName
            _shapeId = clsToothTrt(0).ShapeID
            'ImpPopup.Visible = _propName.Contains("IMPLANT")
            'ImplantSpecsLbl.Visible = _propName.Contains("IMPLANT")

            If clsToothTrt(0).IsExternal.HasValue Then
                external = clsToothTrt(0).IsExternal
            Else
                external = False
            End If
            If external = True Then

            End If
            If clsToothTrt(0).IsPaid.HasValue Then
                paid = clsToothTrt(0).IsPaid
            Else
                paid = False
            End If

            loaded = True
            SaveTrt()
        End Sub
        Public Sub New(ByVal clsToothTrt As Patient_Diagnosis, ByVal clsPatient As Patient)

            loaded = False

            _toothTrt = clsToothTrt

            _propName = clsToothTrt.PropertyName
            _shapeId = clsToothTrt.ShapeID
            'ImpPopup.Visible = _propName.Contains("IMPLANT")
            'ImplantSpecsLbl.Visible = _propName.Contains("IMPLANT")
            If clsToothTrt.IsExternal.HasValue Then
                external = clsToothTrt.IsExternal
            Else
                external = False
            End If
            If external = True Then
            End If
            If clsToothTrt.IsPaid.HasValue Then
                paid = clsToothTrt.IsPaid
            Else
                paid = False
            End If
            external = clsToothTrt.IsExternal
            paid = clsToothTrt.IsPaid
            loaded = True
            SaveTrt()
        End Sub



        Private Function GetToothFullName(ByVal toothname As String) As String
            If String.IsNullOrEmpty(toothname) OrElse toothname.Length < 3 Then Return ""
            Dim direction As String = toothname.Substring(0, 1)
            Dim position As String = toothname.Substring(1, 1)
            Dim number As String = toothname.Substring(2)
            Dim directionFull As String = If(direction = "R", "RIGHT", If(direction = "L", "LEFT", ""))
            Dim positionFull As String = If(position = "U", "UPPER", If(position = "D", "LOWER", ""))
            If String.IsNullOrEmpty(directionFull) OrElse String.IsNullOrEmpty(positionFull) Then Return toothname
            Return $"{directionFull} {positionFull} {number}"
        End Function

        Private Function SetToothTrt() As Boolean
            If _propName.Contains("IMPLANT") Then
                _toothTrt.LVL = GetLVL("IMPLANT")
            End If
            Return True
        End Function
        Private Function SetToothTrt(ByVal trtClas As Patient_Diagnosis) As Boolean

            Return True


        End Function

        Public Sub Save(ByVal clsToothTrt As Patient_Diagnosis, ByVal clsPatient As Patient)
            SaveTrt()
        End Sub

        Public Sub Save(ByVal clsToothTrt As List(Of Patient_Diagnosis), ByVal clsPatient As Patient)
            SaveTrt()
        End Sub
#Region "New Save Code"
        Private toothNumsInt As New List(Of Integer)
        Private toothNums As New List(Of String)
        Dim OnePayAdded As Boolean = False
        Private Sub SaveTrt()
            Dim treatmentText, toothName As String
            Dim clsPatientTrtsData As New Patient_TrtsDATA
            ' Determine if this is a multi-tooth treatment
            Dim isMultiTrt As Boolean = IsMultiToothTreatment(_toothTrt.Treat)
            Dim treatmentGroupID As Guid = Guid.Empty
            ' Only create group ID once if this is a multi-tooth treatment
            If isMultiTrt AndAlso selectedToothTrtList.Count > 0 Then
                treatmentGroupID = Guid.NewGuid()
            End If

            If selectedToothTrtList.Count = 0 Then
                ' Single tooth treatment
                If SetToothTrt() Then
                    treatmentText = _toothTrt.Treat
                    toothName = _toothTrt.ToothName
                    Select Case _toothTrt.Treat
                        Case "ABUTMENT", "HEALING CAP"
                            Saved = SaveTreatmentWithAbutment(_toothTrt)
                        Case Else
                            Saved = SaveTreatmentWithTransaction(PatientID, _toothTrt, treatmentText, toothName, isMultiTrt, treatmentGroupID)
                    End Select
                End If
            Else
                ' Multi-tooth treatment
                Dim firstToothInGroup As Boolean = True
                toothNums.Clear()
                For Each trtCls In selectedToothTrtList
                    toothNums.Add(GetShortToothNameWithDash(trtCls.ToothNum))
                    toothNumsInt.Add(CInt(trtCls.ToothNum))
                Next

                ' ORIGINAL CODE BELOW
                For Each trtCls In selectedToothTrtList
                    If SetToothTrt(trtCls) Then
                        ' Set properties BEFORE assigning to _toothTrt
                        trtCls.IsMultiTrt = isMultiTrt
                        trtCls.TrtGroupID = treatmentGroupID
                        _toothTrt = trtCls
                        treatmentText = trtCls.Treat
                        toothName = trtCls.ToothName
                        Select Case _toothTrt.Treat
                            Case "ABUTMENT", "HEALING CAP"
                                Saved = SaveTreatmentWithAbutment(_toothTrt)
                            Case Else
                                Saved = SaveTreatmentWithTransaction(PatientID, _toothTrt, treatmentText, toothName, isMultiTrt, treatmentGroupID, firstToothInGroup)
                        End Select
                        'Saved = SaveTreatmentWithTransaction(trtCls.PatientID, _toothTrt, treatmentText, toothName,
                        firstToothInGroup = False ' Only first tooth gets accounting record
                    End If
                Next
            End If
            OnePayAdded = False

        End Sub
        Private Function GetLastToothTrtID() As Integer
            Dim dx As New DentistXDATA
            Using conn As New SqlConnection(dx.GetConnectionString)
                conn.Open()
                Return conn.ExecuteScalar(Of Integer)("SELECT TOP 1 DiagID FROM Patient_Diagnosis ORDER BY DiagID DESC")
            End Using
        End Function

        Public Function SaveTreatmentWithAbutment(_toothTrt As Patient_Diagnosis) As Boolean
            Dim saved As Boolean = False
            Dim canceled As Boolean = False
            Dim clsTrtsData As New Patient_TrtsDATA
            Dim toothTrtData As New Patient_DiagnosisDATA
            Dim Dx = New DentistXDATA
            Dim ConnectionString = Dx.GetConnectionString
            Return toothTrtData.AddNormal(_toothTrt)
        End Function
        Public Function SaveTreatmentWithTransaction(PatientID As Integer, _toothTrt As Patient_Diagnosis,
                                          treatmentText As String, toothName As String,
                                          Optional isMultiTooth As Boolean = False,
                                          Optional treatmentGroupID As Guid = Nothing,
                                          Optional isFirstInGroup As Boolean = True) As Boolean
            Dim saved As Boolean = False
            Dim canceled As Boolean = False
            Dim clsTrtsData As New Patient_TrtsDATA
            Dim toothTrtData As New Patient_DiagnosisDATA
            Dim Dx = New DentistXDATA
            Dim ConnectionString = Dx.GetConnectionString
            Dim userID As Integer = CurrentUser.UsID
            _toothTrt.UserID = userID
            Using conn As New SqlConnection(ConnectionString)
                conn.Open()
                Using trans As SqlTransaction = conn.BeginTransaction()
                    Try
                        _toothTrt.QrtrTable = ""
                        _toothTrt.QrtrID = Nothing
                        _toothTrt.QrtrAddress = Nothing
                        _toothTrt.QrtrColumnName = ""
                        _toothTrt.QrtrColumnValue = ""

                        If isMultiTooth Then
                            _toothTrt.ParentDiagID = -1
                            _toothTrt.IsMultiTrt = False
                            _toothTrt.TrtGroupID = treatmentGroupID
                        End If
                        ' Save Patient_Diagnosis
                        If _toothTrt.IsExternal = True Then _toothTrt.IsPaid = True
                        'A new Try
                        '======================
                        'its here where i'm stuck
                        Dim MaxLvl As Integer = toothTrtData.GetTreatLVL(_toothTrt.PatientID, _toothTrt.ToothNum)
                        Dim currentLvl As Integer = _toothTrt.LVL
                        'check if treat is a normal one after high level one
                        If (MaxLvl > 4 AndAlso currentLvl < 4) Then
                            Dim msgEng As String = "You Cant Add a Normal Treat On High Level Treat...."
                            Dim msgAr As String = "لا يمكنك إضافة علاج عادي على علاج عالي المستوى...."
                            Dim msg As String = If(Eng, msgEng, msgAr)
                            MsgBox(msg)
                            Return False
                        End If
                        ' Special case: implant (5) → extraction (4) or extraction (4) → implant (5)
                        If (MaxLvl > 4 AndAlso currentLvl = 4) Then ' OrElse (MaxLvl = 4 AndAlso currentLvl = 5) Then
                            ' 1. Finish old treatment
                            conn.Execute("
                                            UPDATE Patient_Diagnosis
                                            SET LVL = 4
                                            WHERE PatientID = @PatientID
                                              AND ToothNum = @ToothNum
                                              AND LVL > 4
                                        ", New With {
                                                .PatientID = _toothTrt.PatientID,
                                                .ToothNum = _toothTrt.ToothNum,
                                                .OldLvl = MaxLvl
                                            }, trans)
                        End If
                        '============================
                        'normal
                        If Not toothTrtData.AddTransactional(conn, trans, _toothTrt) Then
                            Dim msgEng As String = $"Failed to save Treatment in Chart '{treatmentText}' for '{toothName}'."
                            Dim msgAr As String = $"فشل في حفظ العلاج في السجل  '{treatmentText}' ل '{toothName}'."
                            Dim msg As String = If(Eng, msgEng, msgAr)
                            MessageBox.Show(msg)
                        End If
                        ' Get the last inserted DiagID
                        Dim lastToothTrtID As Integer = conn.ExecuteScalar(Of Integer)(
                                "SELECT TOP 1 DiagID FROM Patient_Diagnosis ORDER BY DiagID DESC",
                                transaction:=trans)
                        'If IsMultiTrt then update ParentDiagID and insert into Patient_DiagInfo
                        If isMultiTooth Then
                            'Update ParentDiagID
                            Dim masterToothID As Integer = conn.ExecuteScalar(Of Integer)(
                                                    "SELECT MIN(DiagID) FROM Patient_Diagnosis 
                                                 WHERE TrtGroupID = @TrtGroupID",
                                                    New With {.TrtGroupID = treatmentGroupID},
                                                    transaction:=trans)
                            ' Update all other teeth in group to point to first tooth
                            Dim rowsUpdated As Integer = conn.ExecuteScalar(
                                                    "UPDATE Patient_Diagnosis 
                                                 SET ParentDiagID = @ParentDiagID 
                                                 WHERE TrtGroupID = @TrtGroupID 
                                                ",'  AND DiagID <> @ParentDiagID
                                                    New With {
                                                        .ParentDiagID = masterToothID,
                                                        .TrtGroupID = treatmentGroupID
                                                    },
                                                    transaction:=trans)
                            ' Insert the record into Patient_DiagInfo
                            Dim Trt As Patient_Diagnosis = toothTrtData.Select_Record(New Patient_Diagnosis With {
                                                                                                        .DiagID = lastToothTrtID,
                                                                                                        .PatientID = PatientID,
                                                                                                        .ToothName = toothName
                                                                                                    }, trans) ' Pass the transaction here)

                            ' Validate the record was found
                            If Trt Is Nothing Then
                                Dim msgEng As String = "Failed to retrieve tooth treatment record."
                                Dim msgAr As String = "فشل في استرداد سجل علاج السن."
                                Dim msg As String = If(Eng, msgEng, msgAr)
                                MessageBox.Show(msg)
                            End If
                            ' Insert with all required parameters
                            Dim rowsInserted As Integer = conn.ExecuteScalar(
                                                                                "INSERT INTO [dbo].[Patient_DiagInfo] (
                                                                            [PatientID], [ParentDiagID], [TrtGroupID], 
                                                                            [ToothNum], [ToothName], [TreatDate], [Treat],
                                                                            [TreatNotes], [IsExternal], [ExternalClinicName], 
                                                                            [ExternalTreatmentDate]
                                                                        ) VALUES (
                                                                            @PatientID, @ParentDiagID, @TrtGroupID, 
                                                                            @ToothNum, @ToothName, @TreatDate, @Treat, 
                                                                            @TreatNotes, @IsExternal, @ExternalClinicName, 
                                                                            @ExternalTreatmentDate
                                                                        )",
                                                                            New With {
                                                                                .PatientID = PatientID,
                                                                                .ParentDiagID = If(masterToothID > 0, masterToothID, DBNull.Value),
                                                                                .TrtGroupID = If(treatmentGroupID <> Guid.Empty, treatmentGroupID, DBNull.Value),
                                                                                .ToothNum = Trt.ToothNum,
                                                                                .ToothName = Trt.ToothName, ' Added missing parameter
                                                                                .TreatDate = Trt.TreatDate,
                                                                                .Treat = If(Trt.Treat, DBNull.Value),
                                                                                .TreatNotes = If(Trt.TreatNotes, DBNull.Value),
                                                                                .IsExternal = Trt.IsExternal,
                                                                                .ExternalClinicName = If(Trt.ExternalClinicName, DBNull.Value),
                                                                                .ExternalTreatmentDate = If(Trt.ExternalTreatmentDate.HasValue,
                                                                                                          Trt.ExternalTreatmentDate.Value,
                                                                                                          DBNull.Value)
                                                                            },
                                                                            transaction:=trans)
                        End If
                        ' Create accounting record for:
                        ' 1. Normal treatments, or
                        'Make sure its IN HOUSE Treat
                        If _toothTrt.IsExternal = False Then
                            ' 2. The FIRST tooth in a multi-tooth treatment group
                            If Not isMultiTooth OrElse isFirstInGroup Then
                                Dim detailText As String
                                Dim teeth As String = String.Join(",", toothNums)
                                If isMultiTooth AndAlso Not String.IsNullOrEmpty(teeth) Then
                                    ' Use concatenated teeth string for multi-tooth treatments
                                    detailText = _toothTrt.Treat & " ==>> " & teeth
                                Else
                                    ' Use individual tooth name for single treatments
                                    detailText = _toothTrt.Treat & " ==>> " & GetShortToothNameWithDash(_toothTrt.ToothNum)
                                End If

                            End If


                        End If
                        trans.Commit()
                        saved = True
                        canceled = False
                    Catch ex As Exception
                        trans.Rollback()
                        Dim msgEng As String = "Transaction Error: " & ex.Message
                        Dim msgAr As String = "خطأ في الإضافة: " & ex.Message
                        Dim msg As String = If(Eng, msgEng, msgAr)
                        MessageBox.Show(ex.Message, msg, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        saved = False
                        canceled = True
                    End Try
                End Using
            End Using
            Return saved
        End Function
        Private Function IsFirstInTreatmentGroup(conn As SqlConnection, trans As SqlTransaction, groupID As Guid) As Boolean
            If groupID = Guid.Empty Then Return False
            Dim count = conn.ExecuteScalar(Of Integer)(
            "SELECT COUNT(*) FROM Patient_Diagnosis WHERE TrtGroupID = @GroupID",
            New With {.GroupID = groupID},
            transaction:=trans)
            Return count = 1
        End Function
        Private Function IsMultiToothTreatment(treatmentType As String) As Boolean
            Dim multiToothTreatments As String() = {"METAL BRIDGE", "ZERCONIA BRIDGE", "PFM BRIDGE", "EMAX BRIDGE", "TEMP BRIDGE", "STAINLESS STEEL BRIDGE",
                                                    "REMOVABLE PARTIAL DENTURE", "COMPLETE DENTURE", "EXTRACTION + IMPLANT"}
            Return multiToothTreatments.Contains(treatmentType.ToUpper())
        End Function
#End Region

        ' Read-only property for Saved
        Private _saved As Boolean = False
        Public Property Saved As Boolean
            Get
                Return _saved
            End Get
            Set(value As Boolean)
                _saved = value
            End Set
        End Property
        ' Read-only property for _Canceled
        Private _canceled As Boolean = False
        Private disposedValue As Boolean

        Public Property Canceled As Boolean
            Get
                Return _canceled
            End Get
            Set(value As Boolean)
                _canceled = value
            End Set
        End Property

        Friend ReadOnly Property ShowDialog(ctl As XtraUserControl) As DialogResult
            Get
                If _saved Then
                    Return DialogResult.OK
                Else
                    Return DialogResult.Cancel
                End If

            End Get
        End Property

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects)
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
                ' TODO: set large fields to null
                disposedValue = True
            End If
        End Sub

        ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
        Protected Overrides Sub Finalize()
            ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
            Dispose(disposing:=False)
            MyBase.Finalize()
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
            Dispose(disposing:=True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class


End Module
