
Module ModuleEvents
    'How it works
    'Declare all your events here
    'Declare all events subs here to Notify the change
    '************************************************************
    'In FormA : the form which will raise the event:-
    'Add the sub of the event for example:
    ''     Private Sub ThinHDRChrt1_KidAgeChanged(sender As Object, e As KidAgeChangedEventArgs) Handles ThinHDRChrt1.KidAgeChanged
    ''        ' Notify ModuleEvents about the PatientID change
    ''          ModuleEvents.NotifyPatientIDChanged(Me, e)
    ''     End Sub
    '************************************************************
    'In FormB add these subs where they belong :-
    ' PatientAccounts (FormB)
    ''Public Class PatientAccounts
    '     in the load form event do this:
    ''    Private Sub PatientAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ''        ' Subscribe to the PatientIDChanged event
    ''        AddHandler ModuleEvents.PatientIDChanged, AddressOf HandlePatientIDChanged
    ''    End Sub

    '      Add this sub for the event
    ''    Private Sub HandlePatientIDChanged(sender As Object, e As KidAgeChangedEventArgs)
    ''        ' Handle the PatientID change
    ''        MessageBox.Show($"New PatientID: {e.NewPatID}, PatientName: {e.NewPatName}")
    ''    End Sub

    '    Remove the added event
    ''    Private Sub PatientAccounts_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    ''        ' Unsubscribe from the event to prevent memory leaks
    ''        RemoveHandler ModuleEvents.PatientIDChanged, AddressOf HandlePatientIDChanged
    ''    End Sub
    ''End Class
    '***********************************************************


    '===============================================================
    ' Shared event to notify PatientID changes
    Public Event PatientIDChanged(ByVal sender As Object, ByVal e As KidAgeChangedEventArgs)

    ' Shared event to notify PatientID changes
    Public Event PatientChanged(ByVal sender As Object, ByVal e As PatientChangedEventArgs)

    ' Shared event to notify Balance changes
    Public Event BalChanged(ByVal sender As Object, ByVal e As BalChangedEventArgs)

    'Shared Method to raise the event (optional helper method)Notify KidAgeChanged
    Public Sub NotifyPatientIDChanged(sender As Object, e As KidAgeChangedEventArgs)
        RaiseEvent PatientIDChanged(sender, e)
    End Sub

    'Shared Method to raise the event (optional helper method)Notify PatientChangedEventArgs
    Public Sub NotifyPatientChanged(sender As Object, e As PatientChangedEventArgs)
        RaiseEvent PatientChanged(sender, e)
    End Sub

    'Shared Method to raise the event (optional helper method)Notify BalanceChanged
    Public Sub NotifyBalanceChanged(sender As Object, e As BalChangedEventArgs)
        RaiseEvent BalChanged(sender, e)
    End Sub

End Module
