Imports System.Collections.Concurrent
Imports System.ComponentModel

Public Class FullOrthoTreating
    Implements IPatientAwareUserControl

    Private _currentPatient As Patient
    Friend Property CurrentPatient As Patient
        Get
            Return _currentPatient
        End Get
        Set(value As Patient)
            _currentPatient = value
        End Set
    End Property

    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        SyncCurrentPatientFromForm(patientId)
        LoadPatientData(patientId)
    End Sub
    Private Sub SyncCurrentPatientFromForm(patientId As Integer)
        Dim ws = PatientAwareHelper.TryGetPatientWorkspace(Me)
        If ws IsNot Nothing AndAlso ws.Current_Patient IsNot Nothing AndAlso ws.Current_Patient.PatientID = patientId Then
            CurrentPatient = ws.Current_Patient
        ElseIf FormManager.Instance.IsBasePatientFormOpen AndAlso FormManager.Instance.GetCurrentPatient() IsNot Nothing AndAlso FormManager.Instance.GetCurrentPatient().PatientID = patientId Then
            CurrentPatient = FormManager.Instance.GetCurrentPatient()
        End If
        OpenNewOrth1.ValueFromParent = patientId
        OrthoTreating1.ValueFromParent = patientId
    End Sub

    Public Sub LoadPatientData(patientId As Integer)
        LoadTreatPatient(patientId)
    End Sub


#Region "Resize"
    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1200 ', 600
    Private Const OriginalPanelHeight As Integer = 600
    Private ReadOnly originalPanelSize As New Size(OriginalPanelWidth, OriginalPanelHeight)
    Private originaMelSize As Size
    Private Sub StoreOriginalBounds(container As Control)
        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
    End Sub
    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return
        Dim widthRatio = CSng(Me.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.Height) / OriginalPanelHeight
        For Each kvp In controlBoundsCache
            kvp.Key.SetBounds(
                CInt(kvp.Value.X * widthRatio),
                CInt(kvp.Value.Y * heightRatio),
                CInt(kvp.Value.Width * widthRatio),
                CInt(kvp.Value.Height * heightRatio))
        Next
    End Sub
    Private Sub FullOrthoTreating_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If controlBoundsCache.IsEmpty Then Return
        ResizeControlsProportionally()
    End Sub
#End Region


    Private Sub FullOrthoTreating_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StoreOriginalBounds(Me)
        PatientAwareHelper.SubscribeToPatientChanges(Me, Me)
        'AddHandler lnkUseNewOrtho.HyperlinkClick, AddressOf lnkUseNewOrtho_Click
        ' Ensure classic header panel is always on top
        ' Load initial patient in case PatientChanged already fired before we subscribed
        Dim wsLoad = PatientAwareHelper.TryGetPatientWorkspace(Me)
        Dim pid As Integer = 0
        If wsLoad IsNot Nothing AndAlso wsLoad.Current_Patient IsNot Nothing Then
            pid = wsLoad.Current_Patient.PatientID
        ElseIf FormManager.Instance.IsBasePatientFormOpen AndAlso FormManager.Instance.GetCurrentPatient() IsNot Nothing Then
            pid = FormManager.Instance.GetCurrentPatient().PatientID
        End If
        If pid > 0 Then
            SyncCurrentPatientFromForm(pid)
            LoadPatientData(pid)
        End If
    End Sub



    ' Define a public event to bubble it up
    Public Event BeginOrthTrt As EventHandler(Of BeginOrthTrtEventArgs)

    Private Sub HandleBeginOrthTrtEventArgs(sender As Object, e As BeginOrthTrtEventArgs)
        ' Raise this up to the host form (JawsForm2New)
        RaiseEvent BeginOrthTrt(Me, e)
    End Sub
    Public Sub LoadTreatPatient(patientID As Integer)
        ShowHide(patientID)
        LoadData(patientID)
    End Sub
    Private Sub HdrValue(ByVal sender As Object, ByVal e As BeginOrthTrtEventArgs) Handles OpenNewOrth1.BeginOrthTrt
        ShowHide(e.NewPatID)
        LoadData(e.NewPatID)
    End Sub
    Public WriteOnly Property ValueFromParent() As Integer
        Set(ByVal Value As Integer)
            LoadData(Value)

        End Set
    End Property

    Public Sub LoadData(ByVal PatientID As Integer)
        Try
            ShowHide(PatientID)
            OpenNewOrth1.ValueFromParent = PatientID
            OrthoTreating1.ValueFromParent = PatientID

        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private clsOrthoTreat As New OrthoTreat
    Private clsOrthoTreatDATA As New OrthoTreatDATA
    Private clsPatientData As New PatientDATA
    Public Sub ShowHide(ByVal PatientID As Integer)
        Try
            Dim DiagCount, InfCount, TrtCount As Integer
            DiagCount = clsOrthoTreatDATA.DiagCount(PatientID)
            InfCount = clsOrthoTreatDATA.InfCount(PatientID)
            TrtCount = clsOrthoTreatDATA.TrtCount(PatientID)

            Me.SuspendLayout()
            Try
                If DiagCount = 0 And InfCount = 0 Then
                    Me.OrthoTreating1.Visible = False
                    Me.OrthoTreating1.SendToBack()
                    Me.OpenNewOrth1.SetBounds(0, 0, Me.ClientSize.Width, Me.ClientSize.Height, BoundsSpecified.All)
                    Me.OpenNewOrth1.Visible = True
                    Me.OpenNewOrth1.BringToFront()
                Else
                    If DiagCount <> 0 Or InfCount <> 0 Then
                        Me.OpenNewOrth1.Visible = False
                        Me.OpenNewOrth1.SendToBack()
                        Me.OrthoTreating1.SetBounds(0, 0, Me.ClientSize.Width, Me.ClientSize.Height, BoundsSpecified.All)
                        Me.OrthoTreating1.Visible = True
                        Me.OrthoTreating1.BringToFront()
                        If TrtCount = 0 Then
                            'OrthoTreating1.btAddTreat.Visible = True
                            'OrthoTreating1.GrpTrtMainINf.Visible = False
                            'OrthoTreating1.GrpTrtSteps.Visible = False
                            'OrthoTreating1.OrthoTrtDetGrid.Visible = False
                        Else
                            'OrthoTreating1.btAddTreat.Visible = False
                            'OrthoTreating1.GrpTrtMainINf.Visible = True
                            'OrthoTreating1.GrpTrtSteps.Visible = True
                            'OrthoTreating1.OrthoTrtDetGrid.Visible = True
                        End If
                    End If
                End If
                ' Keep header always in front of hosted controls
            Finally
                Me.ResumeLayout(True)
            End Try
        Catch ex As Exception

        End Try
    End Sub


    ' In FullOrthoTreating:
    Public ReadOnly Property A As OpenNewOrthCTL
        Get
            Return OpenNewOrth1
        End Get
    End Property

    Public ReadOnly Property B As OrthoTreatingCTL
        Get
            Return OrthoTreating1
        End Get
    End Property

End Class
