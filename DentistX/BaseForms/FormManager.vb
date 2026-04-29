Imports System.Threading

Public Class FormManager
    Private Shared _instance As FormManager
    Private _currentWorkspace As BasePatientWorkspace
    Private _switchVersion As Long
    Private _currentUserControl As UserControl
    Private _patientWorkspaceOwner As Form
    Private _hostMainView3 As MainView3
    Private _hostMainView1 As MainView1
    Private _mainView1WorkspaceHost As Panel
    Private _mainView1HostHandlersAttached As Boolean

    Public Shared ReadOnly Property Instance As FormManager
        Get
            If _instance Is Nothing Then
                _instance = New FormManager()
            End If
            Return _instance
        End Get
    End Property

    ''' <summary>Current embedded <see cref="BasePatientWorkspace"/> (header + body).</summary>
    Public ReadOnly Property CurrentForm As BasePatientWorkspace
        Get
            Return _currentWorkspace
        End Get
    End Property

    Public ReadOnly Property IsBasePatientFormOpen As Boolean
        Get
            Return _currentWorkspace IsNot Nothing AndAlso Not _currentWorkspace.IsDisposed
        End Get
    End Property

    Private Function GetWorkspaceParent(host As Form) As Control
        Dim mv3 = TryCast(host, MainView3)
        If mv3 IsNot Nothing Then Return mv3.ContainerA
        Dim mv1 = TryCast(host, MainView1)
        If mv1 IsNot Nothing Then Return EnsureMainView1WorkspaceHost(mv1)
        Return Nothing
    End Function

    Private Function EnsureMainView1WorkspaceHost(mv1 As MainView1) As Panel
        If _mainView1WorkspaceHost Is Nothing OrElse _mainView1WorkspaceHost.IsDisposed OrElse _mainView1WorkspaceHost.Parent IsNot mv1 Then
            _mainView1WorkspaceHost = New Panel With {.Name = "PatientWorkspaceHostPanel", .TabStop = False}
            mv1.Controls.Add(_mainView1WorkspaceHost)
            mv1.Controls.SetChildIndex(_mainView1WorkspaceHost, 0)
        End If
        If Not _mainView1HostHandlersAttached Then
            AddHandler mv1.Resize, AddressOf OnMainView1WorkspaceLayout
            AddHandler mv1.SizeChanged, AddressOf OnMainView1WorkspaceLayout
            AddHandler mv1.RibbonControl.SizeChanged, AddressOf OnMainView1WorkspaceLayout
            AddHandler mv1.StatusBar1.SizeChanged, AddressOf OnMainView1WorkspaceLayout
            AddHandler mv1.StatusBar1.LocationChanged, AddressOf OnMainView1WorkspaceLayout
            _mainView1HostHandlersAttached = True
        End If
        SyncMainView1WorkspaceHostBounds(mv1)
        Return _mainView1WorkspaceHost
    End Function

    Private Sub OnMainView1WorkspaceLayout(sender As Object, e As EventArgs)
        If _hostMainView1 Is Nothing OrElse _hostMainView1.IsDisposed Then Return
        SyncMainView1WorkspaceHostBounds(_hostMainView1)
    End Sub

    Private Sub SyncMainView1WorkspaceHostBounds(mv1 As MainView1)
        If _mainView1WorkspaceHost Is Nothing OrElse mv1 Is Nothing Then Return
        Dim top = mv1.RibbonControl.Bottom
        Dim bottom = mv1.StatusBar1.Top
        If bottom <= top OrElse mv1.ClientSize.Width <= 0 Then Return
        _mainView1WorkspaceHost.SetBounds(0, top, mv1.ClientSize.Width, bottom - top)
    End Sub

    Private Sub DetachMainView1HostHandlers()
        If Not _mainView1HostHandlersAttached OrElse _hostMainView1 Is Nothing Then
            _mainView1HostHandlersAttached = False
            Return
        End If
        RemoveHandler _hostMainView1.Resize, AddressOf OnMainView1WorkspaceLayout
        RemoveHandler _hostMainView1.SizeChanged, AddressOf OnMainView1WorkspaceLayout
        RemoveHandler _hostMainView1.RibbonControl.SizeChanged, AddressOf OnMainView1WorkspaceLayout
        RemoveHandler _hostMainView1.StatusBar1.SizeChanged, AddressOf OnMainView1WorkspaceLayout
        RemoveHandler _hostMainView1.StatusBar1.LocationChanged, AddressOf OnMainView1WorkspaceLayout
        _mainView1HostHandlersAttached = False
    End Sub

    Public Function GetOrCreateWorkspace(Optional initialFilter As String = "Treat", Optional workspaceHost As Form = Nothing) As BasePatientWorkspace
        ResolveWorkspaceOwner(workspaceHost)
        If _patientWorkspaceOwner Is Nothing OrElse _patientWorkspaceOwner.IsDisposed Then Return Nothing

        Dim parent = GetWorkspaceParent(_patientWorkspaceOwner)
        If parent Is Nothing Then Return Nothing

        _hostMainView3 = TryCast(_patientWorkspaceOwner, MainView3)
        _hostMainView1 = TryCast(_patientWorkspaceOwner, MainView1)

        If _currentWorkspace IsNot Nothing AndAlso _currentWorkspace.IsDisposed Then
            _currentWorkspace = Nothing
        End If

        If _currentWorkspace Is Nothing Then
            _currentWorkspace = New BasePatientWorkspace(initialFilter)
            AddHandler _currentWorkspace.Disposed, AddressOf Workspace_Disposed
        ElseIf _currentWorkspace.Parent IsNot parent Then
            If _currentWorkspace.Parent IsNot Nothing Then
                _currentWorkspace.Parent.Controls.Remove(_currentWorkspace)
            End If
        End If

        If _currentWorkspace.Parent IsNot parent Then
            parent.Controls.Add(_currentWorkspace)
            _currentWorkspace.Dock = DockStyle.Fill
        End If

        _currentWorkspace.Visible = True
        _currentWorkspace.BringToFront()
        Return _currentWorkspace
    End Function

    Private Sub Workspace_Disposed(sender As Object, e As EventArgs)
        DetachMainView1HostHandlers()
        _currentWorkspace = Nothing
        _currentUserControl = Nothing
    End Sub

    Public Sub CloseBaseForm()
        DetachMainView1HostHandlers()
        _hostMainView3 = Nothing
        _hostMainView1 = Nothing

        If _currentWorkspace IsNot Nothing Then
            If Not _currentWorkspace.IsDisposed Then
                RemoveHandler _currentWorkspace.Disposed, AddressOf Workspace_Disposed
                _currentWorkspace.Shutdown()
                Dim p = _currentWorkspace.Parent
                If p IsNot Nothing Then
                    p.Controls.Remove(_currentWorkspace)
                End If
                _currentWorkspace.Dispose()
            End If
        End If

        _currentWorkspace = Nothing
        _currentUserControl = Nothing
        _patientWorkspaceOwner = Nothing

        If _mainView1WorkspaceHost IsNot Nothing Then
            If _mainView1WorkspaceHost.Parent IsNot Nothing Then
                _mainView1WorkspaceHost.Parent.Controls.Remove(_mainView1WorkspaceHost)
            End If
            If Not _mainView1WorkspaceHost.IsDisposed Then
                _mainView1WorkspaceHost.Dispose()
            End If
            _mainView1WorkspaceHost = Nothing
        End If

        Try
            PatientEventManager.RaisePatientChanged(Nothing, False, False)
        Catch
        End Try
    End Sub

    Private Sub ResolveWorkspaceOwner(Optional workspaceHost As Form = Nothing)
        If workspaceHost IsNot Nothing Then
            _patientWorkspaceOwner = workspaceHost
            Return
        End If
        Dim active = Form.ActiveForm
        Dim activeMv3 = TryCast(active, MainView3)
        If activeMv3 IsNot Nothing Then
            _patientWorkspaceOwner = activeMv3
            Return
        End If
        Dim activeMv1 = TryCast(active, MainView1)
        If activeMv1 IsNot Nothing Then
            _patientWorkspaceOwner = activeMv1
            Return
        End If
        If _patientWorkspaceOwner IsNot Nothing AndAlso Not _patientWorkspaceOwner.IsDisposed Then
            Return
        End If
        Dim mv3 = TryCast(Application.OpenForms("MainView3"), MainView3)
        If mv3 IsNot Nothing Then
            _patientWorkspaceOwner = mv3
            Return
        End If
        Dim mv1 = TryCast(Application.OpenForms("MainView1"), MainView1)
        If mv1 IsNot Nothing Then
            _patientWorkspaceOwner = mv1
        End If
    End Sub

    Public ReadOnly Property CurrentPatient As Patient
        Get
            Return GetCurrentPatient()
        End Get
    End Property

    Public Function GetCurrentPatient() As Patient
        If _currentWorkspace IsNot Nothing AndAlso Not _currentWorkspace.IsDisposed Then
            Return _currentWorkspace.Current_Patient
        End If
        Return Nothing
    End Function

#Region "Selected Tooth Management"
    Private ReadOnly _openedControls As New List(Of UserControl)

    Public Sub SwitchUserControl(userControlType As Type,
                                 Optional filterTarget As String = Nothing,
                                 Optional passedPatient As Patient = Nothing,
                                 Optional workspaceHost As Form = Nothing)
        ResolveWorkspaceOwner(workspaceHost)

        Dim ws = GetOrCreateWorkspace(If(filterTarget, "Treat"), workspaceHost)
        If ws Is Nothing Then Return

        If filterTarget IsNot Nothing Then
            ws.FormType = filterTarget
        End If

        If passedPatient IsNot Nothing Then
            ws.Current_Patient = passedPatient
        Else
            SyncWorkspacePatientFromHeader(ws)
        End If

        ' Patient the user was working with; header filter may temporarily select someone else during UpdateFilterTarget.
        Dim switchPatient As Patient = ws.Current_Patient

        ' Only hide when replacing an existing module. Hiding an empty body on first open breaks layout (splitter/jaw size stays 0).
        Dim hadExistingBody = ws.BodyPanel IsNot Nothing AndAlso ws.BodyPanel.Controls.Count > 0
        If hadExistingBody Then
            ws.BodyPanel.Visible = False
            Dim existingUc = TryCast(ws.BodyPanel.Controls(0), UserControl)
            If existingUc IsNot Nothing Then
                PatientAwareHelper.UnsubscribeFromPatientChanges(existingUc)
            End If
        End If

        Dim filterForHeader = If(filterTarget, ws.FormType)
        If Not String.IsNullOrEmpty(filterForHeader) Then
            ' Do not queue a body reload here: this call is always followed by a full body swap below.
            ws.UpdateFilterTarget(filterForHeader, switchPatient, refreshEmbeddedBody:=False)
        End If

        ' First open: workspace often has no Current_Patient until the header finishes filtering; sync before building the body.
        If switchPatient Is Nothing AndAlso ws.HeaderControl IsNot Nothing Then
            switchPatient = ws.HeaderControl.Current_Patient
        End If
        If switchPatient IsNot Nothing Then
            ws.Current_Patient = switchPatient
        Else
            ws.Current_Patient = Nothing
        End If
        ws.UpdateFormTitle(switchPatient)

        ' Defer construction of the new module to the next UI message so the navigator/header can layout and paint first.
        Dim switchVer = Interlocked.Increment(_switchVersion)
        Dim wsRef = ws
        Dim typeRef = userControlType
        Dim patientRef = switchPatient
        ws.BeginInvoke(New MethodInvoker(Sub() ApplySwitchedUserControlBody(wsRef, typeRef, switchVer, patientRef)))
    End Sub

    Private Sub ApplySwitchedUserControlBody(ws As BasePatientWorkspace, userControlType As Type, switchVer As Long, switchPatient As Patient)
        If switchVer <> _switchVersion Then Return
        If ws Is Nothing OrElse ws.IsDisposed OrElse ws.BodyPanel Is Nothing Then Return

        Dim effectivePatient As Patient = switchPatient
        Try
            ws.SuspendLayout()
            ws.BodyPanel.SuspendLayout()
            Try
                If _currentUserControl IsNot Nothing Then
                    PatientAwareHelper.UnsubscribeFromPatientChanges(_currentUserControl)
                    If _currentUserControl.Parent Is ws.BodyPanel Then
                        ws.BodyPanel.Controls.Remove(_currentUserControl)
                    End If
                    _currentUserControl.Dispose()
                    _currentUserControl = Nothing
                End If
                For Each ctl In _openedControls
                    PatientAwareHelper.UnsubscribeFromPatientChanges(ctl)
                    If ctl.Parent Is ws.BodyPanel Then ws.BodyPanel.Controls.Remove(ctl)
                    ctl.Dispose()
                Next
                _openedControls.Clear()

                Dim newControl = Activator.CreateInstance(userControlType)
                _currentUserControl = DirectCast(newControl, UserControl)
                _openedControls.Add(_currentUserControl)
                _currentUserControl.Dock = DockStyle.Fill
                _currentUserControl.Visible = False
                ws.BodyPanel.Controls.Add(_currentUserControl)
                If effectivePatient Is Nothing AndAlso ws.HeaderControl IsNot Nothing Then
                    effectivePatient = ws.HeaderControl.Current_Patient
                End If
                If effectivePatient IsNot Nothing Then
                    ws.Current_Patient = effectivePatient
                Else
                    ws.Current_Patient = Nothing
                End If
                SubscribeToPatientAwareControl(_currentUserControl, effectivePatient)
            Finally
                ws.BodyPanel.ResumeLayout(True)
                ws.ResumeLayout(True)
            End Try

            If _currentUserControl IsNot Nothing Then
                _currentUserControl.SetBounds(0, 0, ws.BodyPanel.ClientSize.Width, ws.BodyPanel.ClientSize.Height, BoundsSpecified.All)
                _currentUserControl.Visible = True
            End If
            ' Navigator loads patients async; if still no current patient, retry until header commits selection.
            If effectivePatient Is Nothing Then
                ws.BeginInvoke(New MethodInvoker(Sub() SyncEmbeddedPatientWhenHeaderReady(ws, switchVer, 0)))
            End If
        Finally
            If Not ws.IsDisposed AndAlso ws.BodyPanel IsNot Nothing AndAlso switchVer = _switchVersion Then
                ws.BodyPanel.Visible = True
                ws.BodyPanel.PerformLayout()
                If _currentUserControl IsNot Nothing AndAlso Not _currentUserControl.IsDisposed Then
                    _currentUserControl.PerformLayout()
                End If
            End If
        End Try
    End Sub

    ''' <summary>Patient headers (Navigator / Navigator2) may fill lists asynchronously; first frame may have no Current_Patient yet.</summary>
    Private Sub SyncEmbeddedPatientWhenHeaderReady(ws As BasePatientWorkspace, switchVer As Long, attempt As Integer)
        Const maxAttempts As Integer = 120
        If switchVer <> _switchVersion Then Return
        If ws Is Nothing OrElse ws.IsDisposed OrElse ws.BodyPanel Is Nothing OrElse ws.BodyPanel.Controls.Count = 0 Then Return
        Dim hp As Patient = Nothing
        If ws.HeaderControl IsNot Nothing Then hp = ws.HeaderControl.Current_Patient
        If hp Is Nothing Then
            If attempt < maxAttempts Then
                ws.BeginInvoke(New MethodInvoker(Sub() SyncEmbeddedPatientWhenHeaderReady(ws, switchVer, attempt + 1)))
            Else
                Dim uClear = TryCast(ws.BodyPanel.Controls(0), UserControl)
                Dim awareClear = TryCast(uClear, IPatientAwareUserControl)
                If awareClear IsNot Nothing Then
                    awareClear.LoadPatientData(-1)
                End If
            End If
            Return
        End If
        ws.Current_Patient = hp
        Dim u = TryCast(ws.BodyPanel.Controls(0), UserControl)
        Dim aware = TryCast(u, IPatientAwareUserControl)
        If aware IsNot Nothing Then
            aware.LoadPatientData(hp.PatientID)
        End If
        ws.BodyPanel.PerformLayout()
        If u IsNot Nothing AndAlso Not u.IsDisposed Then
            u.PerformLayout()
        End If
    End Sub

    Private Shared Sub SyncWorkspacePatientFromHeader(ws As BasePatientWorkspace)
        If ws Is Nothing OrElse ws.HeaderControl Is Nothing Then Return
        Dim hp = ws.HeaderControl.Current_Patient
        If hp Is Nothing Then Return
        If ws.Current_Patient Is Nothing OrElse ws.Current_Patient.PatientID <> hp.PatientID Then
            ws.Current_Patient = hp
            ws.UpdateFormTitle(hp)
        End If
    End Sub

    Private Sub SubscribeToPatientAwareControl(control As UserControl, patient As Patient)
        Dim patientAwareControl = TryCast(control, IPatientAwareUserControl)
        If patientAwareControl IsNot Nothing Then
            PatientAwareHelper.SubscribeToPatientChanges(control, patientAwareControl)
            If patient IsNot Nothing Then
                patientAwareControl.LoadPatientData(patient.PatientID)
            Else
                patientAwareControl.LoadPatientData(-1)
            End If
        End If
    End Sub

    ''' <summary>Embedded body module when the user opened Appointments Scheduler; used for scheduled snapshot capture.</summary>
    Friend Function TryGetEmbeddedSchedulerNew() As SchedulerNew
        Return TryCast(_currentUserControl, SchedulerNew)
    End Function
#End Region
End Class
