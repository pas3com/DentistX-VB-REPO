''' <summary>
''' Runs licensing once the app can reach SQL Server (required for PC_Trials and CursorPcTrialsRepository).
''' Called from main shell EnsureDatabaseConnection after TestDatabaseConnection succeeds — not from MyApplication.Startup
''' (connection string / DB may not be ready until FrmChooseConn completes).
''' </summary>
Public Module LicenseStartupGate
    Private _initialized As Boolean

    ''' <summary>
    ''' Runs CursorLicManager.Initialize on first success only. Safe to call from MainView1 / MainView3.
    ''' </summary>
    Public Function RunOnceAfterDatabaseConnected() As Boolean
        If _initialized Then Return True
        _initialized = True
        Return CursorLicManager.Initialize()
    End Function
End Module
