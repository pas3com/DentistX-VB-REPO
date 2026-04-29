Imports System.Collections.Generic

Public NotInheritable Class PatientAwareHelper

    Private Shared ReadOnly _handlers As New Dictionary(Of Control, EventHandler(Of Patient))

    ''' <summary>Walks parent chain to find the embedded <see cref="BasePatientWorkspace"/>.</summary>
    Public Shared Function FindPatientWorkspace(control As Control) As BasePatientWorkspace
        Dim c As Control = control
        While c IsNot Nothing
            Dim w = TryCast(c, BasePatientWorkspace)
            If w IsNot Nothing Then Return w
            c = c.Parent
        End While
        Return Nothing
    End Function

    ''' <summary>Workspace that contains <paramref name="control"/>, or <see cref="FormManager.CurrentForm"/> when a patient workspace is open.</summary>
    Public Shared Function TryGetPatientWorkspace(control As Control) As BasePatientWorkspace
        Dim w = FindPatientWorkspace(control)
        If w IsNot Nothing Then Return w
        If FormManager.Instance.IsBasePatientFormOpen Then Return FormManager.Instance.CurrentForm
        Return Nothing
    End Function

    Public Shared Sub SubscribeToPatientChanges(
        control As UserControl,
        patientAwareControl As IPatientAwareUserControl
    )
        Dim host = FindPatientWorkspace(control)
        If host Is Nothing Then Return
        If _handlers.ContainsKey(control) Then Return

        Dim handler As EventHandler(Of Patient) =
            Sub(sender, patient)
                If control.IsDisposed Then Return
                If patient Is Nothing Then
                    patientAwareControl.LoadPatientData(-1)
                Else
                    patientAwareControl.LoadPatientData(patient.PatientID)
                End If
            End Sub

        _handlers(control) = handler
        AddHandler host.PatientChanged, handler
    End Sub

    Public Shared Sub UnsubscribeFromPatientChanges(control As UserControl)
        Dim host = FindPatientWorkspace(control)
        If host Is Nothing Then Return

        Dim handler As EventHandler(Of Patient) = Nothing
        If _handlers.TryGetValue(control, handler) Then
            RemoveHandler host.PatientChanged, handler
            _handlers.Remove(control)
        End If
    End Sub

    Public Shared Sub ClearAll()
        _handlers.Clear()
    End Sub

End Class
