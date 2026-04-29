Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

''' <summary>
''' Binds <see cref="Patient"/>.WhatsApp / WhatsAppPrefix to DevExpress cboPrefix + txtWhats,
''' persists via <see cref="PatientDATA.UpdateWhatsAppAndPrefix"/>, and mirrors FrmPatientAccnt behavior
''' (snapshot dirty check, no ValidateChildren, suppress auto-save during modal send).
''' </summary>
Public NotInheritable Class PatientWhatsControlsBinder
    Private ReadOnly _cboPrefix As ComboBoxEdit
    Private ReadOnly _txtWhats As TextEdit
    Private ReadOnly _lblWhats As Control
    Private ReadOnly _host As Control

    Private _patient As Patient
    Private _whatsBs As BindingSource
    Private _whatsBindLoading As Boolean
    Private _whatsFullDigitsSnapshot As String = ""
    Private _inWhatsSave As Boolean
    Private _inPrefixLocalSync As Boolean
    Private _suppressAutoWhatsSave As Boolean

    Public Sub New(cboPrefix As ComboBoxEdit, txtWhats As TextEdit, lblWhats As Control, host As Control)
        _cboPrefix = cboPrefix
        _txtWhats = txtWhats
        _lblWhats = lblWhats
        _host = host
    End Sub

    Public ReadOnly Property BoundPatient As Patient
        Get
            Return _patient
        End Get
    End Property

    Public Sub BindToPatient(p As Patient, Optional reloadWhatsFieldsFromDatabase As Boolean = False)
        If p Is Nothing OrElse p.PatientID <= 0 Then
            Unbind()
            Return
        End If
        _patient = p
        If reloadWhatsFieldsFromDatabase Then
            ReloadPatientWhatsFromDatabase()
        End If
        SetupBindings()
        TakeSnapshot()
        RefreshLabel()
    End Sub

    Public Sub Unbind()
        ClearControlBindings()
        _whatsBs = Nothing
        _patient = Nothing
        _whatsFullDigitsSnapshot = ""
        If _lblWhats IsNot Nothing AndAlso Not _lblWhats.IsDisposed Then _lblWhats.Text = ""
    End Sub

    Private Sub ReloadPatientWhatsFromDatabase()
        If _patient Is Nothing OrElse _patient.PatientID <= 0 Then Return
        Try
            Dim pd As New PatientDATA()
            Dim row = pd.Select_RecordByID(_patient.PatientID)
            If row Is Nothing Then Return
            _patient.WhatsApp = row.WhatsApp
            _patient.WhatsAppPrefix = row.WhatsAppPrefix
            _patient.Phone = row.Phone
        Catch
        End Try
    End Sub

    Private Sub ClearControlBindings()
        If _txtWhats IsNot Nothing AndAlso Not _txtWhats.IsDisposed Then _txtWhats.DataBindings.Clear()
        If _cboPrefix IsNot Nothing AndAlso Not _cboPrefix.IsDisposed Then _cboPrefix.DataBindings.Clear()
    End Sub

    Private Sub SetupBindings()
        If _patient Is Nothing Then Return
        ClearControlBindings()
        _whatsBindLoading = True
        Try
            _whatsBs = New BindingSource(New List(Of Patient) From {_patient}, Nothing)

            Dim bLocal As New Binding("EditValue", _whatsBs, "WhatsApp", True, DataSourceUpdateMode.OnValidation)
            AddHandler bLocal.Format, Sub(s As Object, conv As ConvertEventArgs)
                                          Dim cur = TryCast(_whatsBs.Current, Patient)
                                          conv.Value = If(WhatsHelper.GetLocalWhatsDisplayForPatient(cur, _cboPrefix), "")
                                      End Sub
            AddHandler bLocal.Parse, Sub(s As Object, conv As ConvertEventArgs)
                                         conv.Value = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(Convert.ToString(conv.Value))
                                     End Sub
            _txtWhats.DataBindings.Add(bLocal)

            Dim bPrefix As New Binding("EditValue", _whatsBs, "WhatsAppPrefix", True, DataSourceUpdateMode.OnPropertyChanged)
            AddHandler bPrefix.Format, Sub(s As Object, conv As ConvertEventArgs)
                                           Dim cur = TryCast(_whatsBs.Current, Patient)
                                           Dim col = If(cur Is Nothing, "", If(cur.WhatsAppPrefix, ""))
                                           conv.Value = WhatsHelper.GetResolvedWhatsAppPrefixForBinding(_cboPrefix, col)
                                       End Sub
            AddHandler bPrefix.Parse, Sub(s As Object, conv As ConvertEventArgs)
                                          conv.Value = WhatsHelper.GetPrefixTextForStorage(_cboPrefix)
                                      End Sub
            _cboPrefix.DataBindings.Add(bPrefix)

            _whatsBs.ResetBindings(False)
        Finally
            _whatsBindLoading = False
        End Try
    End Sub

    Public Sub TakeSnapshot()
        If _patient Is Nothing Then _whatsFullDigitsSnapshot = "" : Return
        _whatsFullDigitsSnapshot = If(WhatsHelper.GetFullWhatsDigitsFromPatient(_patient), "")
    End Sub

    Public Sub SaveIfDirty()
        If _suppressAutoWhatsSave Then Return
        If _inWhatsSave Then Return
        If _whatsBindLoading Then Return
        If _host IsNot Nothing AndAlso (_host.Disposing OrElse _host.IsDisposed) Then Return
        If _patient Is Nothing OrElse _patient.PatientID <= 0 OrElse _whatsBs Is Nothing Then Return
        If _cboPrefix Is Nothing OrElse _cboPrefix.IsDisposed OrElse _txtWhats Is Nothing OrElse _txtWhats.IsDisposed Then Return
        _inWhatsSave = True
        Try
            _whatsBs.EndEdit()
            Dim w = WhatsHelper.NormalizeLocalWhatsTenDigitsForStorage(If(_patient.WhatsApp, ""))
            Dim px = WhatsHelper.GetPrefixTextForStorage(_cboPrefix)
            _patient.WhatsApp = w
            _patient.WhatsAppPrefix = px
            Dim nowFull = If(WhatsHelper.GetFullWhatsDigitsFromPatient(_patient), "")
            If String.Equals(nowFull, _whatsFullDigitsSnapshot, StringComparison.Ordinal) Then
                RefreshLabel()
                Return
            End If
            Try
                Dim pd As New PatientDATA()
                If pd.UpdateWhatsAppAndPrefix(_patient.PatientID, w, px) Then
                    _whatsFullDigitsSnapshot = nowFull
                    ReloadPatientWhatsFromDatabase()
                    SyncBindingsFromPatient()
                Else
                    RefreshLabel()
                End If
            Catch
                RefreshLabel()
            End Try
        Finally
            _inWhatsSave = False
        End Try
    End Sub

    Public Sub SyncBindingsFromPatient()
        If _whatsBs Is Nothing OrElse _patient Is Nothing Then Return
        _whatsBindLoading = True
        Try
            _whatsBs.ResetBindings(False)
        Finally
            _whatsBindLoading = False
        End Try
        RefreshLabel()
    End Sub

    Public Sub RefreshLabel()
        If _lblWhats IsNot Nothing AndAlso Not _lblWhats.IsDisposed AndAlso _patient IsNot Nothing Then
            _lblWhats.Text = WhatsHelper.GetFullWhatsDigitsFromPatient(_patient)
        End If
    End Sub

    Public Function GetFullInternationalDigitsFromBoundPatient() As String
        If _patient Is Nothing Then Return ""
        Return If(WhatsHelper.GetFullWhatsDigitsFromPatient(_patient), "")
    End Function

    Public Sub OnCboPrefixSelectedIndexChanged()
        If _whatsBindLoading OrElse _whatsBs Is Nothing Then Return
        If _inPrefixLocalSync Then Return
        _inPrefixLocalSync = True
        Try
            Try
                If _txtWhats.DataBindings.Count > 0 Then _txtWhats.DataBindings("EditValue").ReadValue()
            Catch
            End Try
            RefreshLabel()
            SaveIfDirty()
        Finally
            _inPrefixLocalSync = False
        End Try
    End Sub

    Public Sub OnCboPrefixEditValueChanged()
        RefreshLabel()
    End Sub

    Public Sub OnTxtWhatsEditValueChanged()
        RefreshLabel()
    End Sub

    Public Sub OnTxtWhatsLeave()
        SaveIfDirty()
    End Sub

    Public Sub OnTxtWhatsValidated()
        SaveIfDirty()
    End Sub

    Public Sub BeginSuppressAutoSaveWhileModal()
        _suppressAutoWhatsSave = True
    End Sub

    Public Sub EndSuppressAutoSaveAndSync()
        _suppressAutoWhatsSave = False
        SyncBindingsFromPatient()
    End Sub
End Class
