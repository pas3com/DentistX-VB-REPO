

'Imports DevExpress.XtraEditors.SvgImageBox

Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils.Svg
Imports DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper
Imports DevExpress.XtraEditors
Imports Infragistics.Win.UltraWinGrid
Imports SvgResources

Module Helpers

    ' ComboBoxItem class
    Public Class ComboBoxItem
        Public Property ID As Integer
        Public Property Name As String

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class




#Region "Log To File"
    ' Logs a timed action
    Public Sub LogTime(methodName As String, formName As String, stopwatch As Stopwatch)
        LogToFile($"{Now:dd-MM-yyyy HH:mm:ss}: [{methodName}] [{formName}] Time spent: {stopwatch.Elapsed.TotalSeconds:N2} seconds")
    End Sub

    ' Starts and returns a stopwatch, for performance tracking
    Public Function StartTimer() As Stopwatch
        Dim sw As New Stopwatch()
        sw.Start()
        Return sw
    End Function
    Public Sub LogToFile(
    message As String,
    Optional ctrl As Control = Nothing,
    <CallerMemberName> Optional memberName As String = "",
    <CallerFilePath> Optional filePath As String = "")

        Dim logDirectory As String = Path.Combine(Application.StartupPath, "DentistXLogs")
        Dim datePart As String = DateTime.Now.ToString("dd-MM-yyyy")
        Dim logFilePath As String = Path.Combine(logDirectory, $"DentistX_{datePart}.txt")

        Try
            Directory.CreateDirectory(logDirectory)

            Dim formOrControlName As String = If(ctrl IsNot Nothing, ctrl.Name, "UnknownControl")
            Dim className As String = Path.GetFileNameWithoutExtension(filePath)

            Dim logEntry As String = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss}: [{className}.{memberName}] [{formOrControlName}] {message}"

            Using writer As New StreamWriter(logFilePath, append:=True)
                writer.WriteLine(logEntry)
            End Using

        Catch ex As Exception
            MsgBox($"Logging error: {ex.Message}")
        End Try
    End Sub

    Public Sub LogToFileOld(message As String)
        ' File logging (keep your existing code)
        Dim logDirectory As String = Path.Combine(Application.StartupPath, "DentistXLogs")
        Dim datePart As String = DateTime.Now.ToString("dd-MM-yyyy")
        Dim logFilePath As String = Path.Combine(logDirectory, $"DbaseMig_{datePart}.txt")

        Try
            Directory.CreateDirectory(logDirectory)
            Using writer As New StreamWriter(logFilePath, append:=True)
                writer.WriteLine($"{DateTime.Now:dd-MM-yyyy HH:mm:ss}: {message}")
            End Using
        Catch ex As Exception
            MsgBox($"Logging error: {ex.Message}")
        End Try

        ' MemoEdit logging
        Try
            If MigratForm IsNot Nothing AndAlso MigratForm.LogTxt IsNot Nothing Then
                If MigratForm.LogTxt.InvokeRequired Then
                    MigratForm.LogTxt.Invoke(Sub() LogToMemoEditDirect(message))
                Else
                    LogToMemoEditDirect(message)
                End If
            End If
        Catch ex As Exception
            ' Silent fail for memoedit logging
        End Try
    End Sub

    Private Sub LogToMemoEditDirect(message As String)
        Dim logEntry As String = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss}: {message}" & Environment.NewLine
        MigratForm.LogTxt.Text += logEntry
        MigratForm.LogTxt.SelectionStart = MigratForm.LogTxt.Text.Length
        MigratForm.LogTxt.ScrollToCaret()
    End Sub

    Public Sub LogToFileOld1(message As String)
        Dim logDirectory As String = Path.Combine(Application.StartupPath, "DentistXLogs")
        Dim datePart As String = DateTime.Now.ToString("dd-MM-yyyy")
        Dim logFilePath As String = Path.Combine(logDirectory, $"DbaseMig_{datePart}.txt")

        Try
            ' Ensure the log directory exists
            Directory.CreateDirectory(logDirectory)

            ' Append the new message to the daily log file
            Using writer As New StreamWriter(logFilePath, append:=True)
                writer.WriteLine($"{DateTime.Now:dd-MM-yyyy HH:mm:ss}: {message}")
            End Using

        Catch ex As Exception
            MsgBox($"Logging error: {ex.Message}")
        End Try
    End Sub

    Public Sub LogToFiles(message As String)
        Dim logDirectory As String = Path.Combine(Application.StartupPath, "DentistXLogs")
        Dim datePart As String = DateTime.Now.ToString("dd-MM-yyyy")
        Dim filePrefix As String = $"DentistX{datePart}"
        Dim fileNumber As Integer = 0
        Dim logFilePath As String = ""

        Try
            ' Ensure directory exists
            Directory.CreateDirectory(logDirectory)

            ' Get and process existing files
            Dim existingFiles = Directory.EnumerateFiles(logDirectory, $"{filePrefix}*.txt").Select(Function(filePath) Path.GetFileName(filePath)).Where(Function(fileName) fileName.StartsWith(filePrefix)).OrderBy(Function(fileName) fileName).ToList()

            ' Calculate next file number
            If existingFiles.Any() Then
                Dim lastFile = existingFiles.Last()
                Dim numStr = Path.GetFileNameWithoutExtension(lastFile).Substring(filePrefix.Length)
                Integer.TryParse(numStr, fileNumber)
                fileNumber += 1
            End If

            ' Create new file path
            logFilePath = Path.Combine(logDirectory, $"{filePrefix}{fileNumber:00}.txt")

            ' Write to log file
            Using writer As New StreamWriter(logFilePath, False)
                writer.WriteLine($"{DateTime.Now:dd-MM-yyyy HH:mm:ss}: Application Start")
                writer.WriteLine($"{DateTime.Now:dd-MM-yyyy HH:mm:ss}: {message}")
            End Using

        Catch ex As Exception
            MsgBox($"Logging error: {ex.Message}")
        End Try
    End Sub

    Public Sub LogToFile1(message As String)
        Dim logFilePath As String = "C:\Logs\UpdateRecordLog.txt" ' Change the path as needed
        Try
            Using writer As New StreamWriter(logFilePath, True)
                writer.WriteLine($"{DateTime.Now}: {message}")
            End Using
        Catch ex As Exception
            ' Handle exceptions if needed
        End Try
    End Sub


#End Region



#Region "Svgs"

    ''' <summary>Sets Module1.BackClr for tooth/SVG treatment tinting. Uses the shell that hosts <see cref="BasePatientWorkspace"/> when possible.</summary>
    Public Sub SyncModuleBackClrForJawRendering(Optional hostControl As Control = Nothing)
        Dim cf = FormManager.Instance.CurrentForm
        If cf IsNot Nothing AndAlso Not cf.IsDisposed Then
            Dim shell = cf.FindForm()
            Dim mv1Own = TryCast(shell, MainView1)
            If mv1Own IsNot Nothing Then
                Module1.BackClr = mv1Own.BackColor
                Return
            End If
            Dim mv3Own = TryCast(shell, MainView3)
            If mv3Own IsNot Nothing AndAlso mv3Own.ContainerA IsNot Nothing Then
                Module1.BackClr = mv3Own.ContainerA.BackColor
                Return
            End If
        End If
        Dim mv3 = TryCast(Application.OpenForms("MainView3"), MainView3)
        If mv3 IsNot Nothing AndAlso mv3.ContainerA IsNot Nothing Then
            Module1.BackClr = mv3.ContainerA.BackColor
            Return
        End If
        Dim mv1 = TryCast(Application.OpenForms("MainView1"), MainView1)
        If mv1 IsNot Nothing Then
            Module1.BackClr = mv1.BackColor
            Return
        End If
        If hostControl IsNot Nothing Then
            Module1.BackClr = hostControl.BackColor
        End If
    End Sub

    Public Sub ClearSvgBackground(svgBox As SvgImageBox)
        ' Properly dispose old background image if exists
        If svgBox.BackgroundImage IsNot Nothing Then
            Dim oldImage = svgBox.BackgroundImage
            svgBox.BackgroundImage = Nothing
            oldImage.Dispose()
        End If
        svgBox.BackColor = Color.Transparent
    End Sub

    Public Sub ApplyGradientBackground(svgBox As SvgImageBox,
                                   startColor As Color,
                                   endColor As Color,
                                   Optional gradientMode As Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.ForwardDiagonal,
                                   Optional opacity As Integer = 255)
        ' Ensure opacity is within valid range
        opacity = Math.Max(0, Math.Min(255, opacity))

        ' Apply transparency to colors
        Dim transparentStart As Color = Color.FromArgb(opacity, startColor)
        Dim transparentEnd As Color = Color.FromArgb(opacity, endColor)

        ' Create gradient image matching the control size
        Dim gradientImage As New Bitmap(Math.Max(1, svgBox.Width), Math.Max(1, svgBox.Height))

        Using g As Graphics = Graphics.FromImage(gradientImage)
            Using brush As New Drawing2D.LinearGradientBrush(
            New Rectangle(0, 0, svgBox.Width, svgBox.Height),
            transparentStart,
            transparentEnd,
            gradientMode)

                ' Optional: Add gamma correction for smoother gradients
                brush.GammaCorrection = True
                g.FillRectangle(brush, New Rectangle(0, 0, svgBox.Width, svgBox.Height))
            End Using
        End Using

        ' Apply to control
        svgBox.BackgroundImage = gradientImage
        svgBox.BackgroundImageLayout = ImageLayout.Stretch
        svgBox.BackColor = Color.Transparent
    End Sub

    Public Sub ApplyCtlGradientBackground(Ctl As Control,
                                   startColor As Color,
                                   endColor As Color,
                                   Optional gradientMode As Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.ForwardDiagonal,
                                   Optional opacity As Integer = 255)
        ' Ensure opacity is within valid range
        opacity = Math.Max(0, Math.Min(255, opacity))

        ' Apply transparency to colors
        Dim transparentStart As Color = Color.FromArgb(opacity, startColor)
        Dim transparentEnd As Color = Color.FromArgb(opacity, endColor)

        ' Create gradient image matching the control size
        Dim gradientImage As New Bitmap(Math.Max(1, Ctl.Width), Math.Max(1, Ctl.Height))

        Using g As Graphics = Graphics.FromImage(gradientImage)
            Using brush As New Drawing2D.LinearGradientBrush(
            New Rectangle(0, 0, Ctl.Width, Ctl.Height),
            transparentStart,
            transparentEnd,
            gradientMode)

                ' Optional: Add gamma correction for smoother gradients
                brush.GammaCorrection = True
                g.FillRectangle(brush, New Rectangle(0, 0, Ctl.Width, Ctl.Height))
            End Using
        End Using

        ' Apply to control
        Ctl.BackgroundImage = gradientImage
        Ctl.BackgroundImageLayout = ImageLayout.Stretch
        Ctl.BackColor = Color.Transparent
    End Sub

    Public Sub ResetControlBackground(Ctl As Control)
        ' Remove the gradient image
        If Ctl.BackgroundImage IsNot Nothing Then
            Ctl.BackgroundImage.Dispose()
            Ctl.BackgroundImage = Nothing
        End If

        ' Reset background properties to default
        Ctl.BackgroundImageLayout = ImageLayout.Tile ' Default for most controls
        Ctl.BackColor = SystemColors.Control ' Default control color (or use Color.Transparent if appropriate)
    End Sub

#Region "SVG resources"

    Public AdultResourcesReady As Boolean = False
    Public KidResourcesReady As Boolean = False

    Public ReadOnly Property AllSvgResourcesReady As Boolean
        Get
            Return AdultResourcesReady AndAlso KidResourcesReady
        End Get
    End Property

    Public Structure SvgResourcesSet
        Public Property SvgOutResource As SvgImage
        Public Property SvgInResource As SvgImage
        Public Property SvgTopResource As SvgImage
    End Structure

    Public ReadOnly AdultResourceMapping As New Dictionary(Of String, SvgResourcesSet)
    Public ReadOnly KidResourceMapping As New Dictionary(Of String, SvgResourcesSet)

    ' Async loading for better performance
    Public Async Function LoadAdultSvgResourcesAsync(Optional progressCallback As Action(Of String) = Nothing) As Task
        Dim adultKeys = {"Ld1", "Ld2", "Ld3", "Ld4", "Ld5", "Ld6", "Ld7", "Ld8",
                     "Lu1", "Lu2", "Lu3", "Lu4", "Lu5", "Lu6", "Lu7", "Lu8",
                     "Rd1", "Rd2", "Rd3", "Rd4", "Rd5", "Rd6", "Rd7", "Rd8",
                     "Ru1", "Ru2", "Ru3", "Ru4", "Ru5", "Ru6", "Ru7", "Ru8"}

        Await Task.Run(Sub()
                           For Each key In adultKeys
                               Try
                                   progressCallback?.Invoke(If(Eng, $"Loading Adult: {key}...", $"تحميل صور البالغين {key} ...."))
                                   'Dim loadingEn As String = "Loading Adult Images: "
                                   'Dim loadingAr As String = "جاري تحميل صور البالغين: "
                                   'Dim loading As String = If(Eng, loadingEn, loadingAr)
                                   Dim svgOut = SvgResourceProvider.GetAdultSvgResourceCached(key, "OUT")
                                   Dim svgIn = SvgResourceProvider.GetAdultSvgResourceCached(key, "IN")
                                   Dim svgTop = SvgResourceProvider.GetAdultSvgResourceCached(key, "TOP")

                                   Dim outImage As SvgImage = Nothing
                                   Dim inImage As SvgImage = Nothing
                                   Dim topImage As SvgImage = Nothing


                                   'Debug.WriteLine($"Key: {key} - Out: {svgOut IsNot Nothing}, In: {svgIn IsNot Nothing}, Top: {svgTop IsNot Nothing}")
                                   If svgOut IsNot Nothing Then outImage = New SvgImage(svgOut)
                                   If svgIn IsNot Nothing Then inImage = New SvgImage(svgIn)
                                   If svgTop IsNot Nothing Then topImage = New SvgImage(svgTop)

                                   SyncLock AdultResourceMapping
                                       AdultResourceMapping(key) = New SvgResourcesSet With {
                        .SvgOutResource = outImage,
                        .SvgInResource = inImage,
                        .SvgTopResource = topImage
                    }
                                   End SyncLock

                               Catch ex As Exception
                                   Debug.WriteLine($"Key {key} failed: {ex.Message}")
                               End Try
                           Next
                       End Sub)

        AdultResourcesReady = True
    End Function

    Public Async Function LoadKidSvgResourcesAsync(Optional progressCallback As Action(Of String) = Nothing) As Task
        Dim kidKeys = {"LD1", "LD2", "LD3", "LD4", "LD5", "LD6", "LD7",
                   "LU1", "LU2", "LU3", "LU4", "LU5", "LU6", "LU7",
                   "RD1", "RD2", "RD3", "RD4", "RD5", "RD6", "RD7",
                   "RU1", "RU2", "RU3", "RU4", "RU5", "RU6", "RU7"}

        Await Task.Run(Sub()
                           For Each key In kidKeys
                               Try
                                   progressCallback?.Invoke(If(Eng, $"Loading Kid: {key}...", $"تحميل صور الاطفال {key} ...."))

                                   Dim svgOut = SvgResourceProvider.GetKidsSvgResourceCached(key, "OUT")
                                   Dim svgIn = SvgResourceProvider.GetKidsSvgResourceCached(key, "IN")
                                   Dim svgTop = SvgResourceProvider.GetKidsSvgResourceCached(key, "TOP")

                                   Dim outImage As SvgImage = Nothing
                                   Dim inImage As SvgImage = Nothing
                                   Dim topImage As SvgImage = Nothing

                                   'Debug.WriteLine($"Key: {key} - Out: {svgOut IsNot Nothing}, In: {svgIn IsNot Nothing}, Top: {svgTop IsNot Nothing}")
                                   If svgOut IsNot Nothing Then outImage = New SvgImage(svgOut)
                                   If svgIn IsNot Nothing Then inImage = New SvgImage(svgIn)
                                   If svgTop IsNot Nothing Then topImage = New SvgImage(svgTop)

                                   SyncLock KidResourceMapping
                                       KidResourceMapping(key) = New SvgResourcesSet With {
                        .SvgOutResource = outImage,
                        .SvgInResource = inImage,
                        .SvgTopResource = topImage
                    }
                                   End SyncLock

                               Catch ex As Exception
                                   Debug.WriteLine($"Key {key} failed: {ex.Message}")
                               End Try
                           Next
                       End Sub)

        KidResourcesReady = True
    End Function

#End Region



#Region "Svgs Helper"

    Public Function GetSvgKey(toothNum As Integer) As String
        Select Case toothNum
            Case 21 To 28 : Return "Lu" & (toothNum - 20)
            Case 11 To 18 : Return "Ru" & (toothNum - 10)
            Case 31 To 38 : Return "Ld" & (toothNum - 30)
            Case 41 To 48 : Return "Rd" & (toothNum - 40)
        '
            Case 61 To 65 : Return "Lu" & (toothNum - 60)
            Case 51 To 55 : Return "Ru" & (toothNum - 50)
            Case 71 To 75 : Return "Ld" & (toothNum - 70)
            Case 81 To 85 : Return "Rd" & (toothNum - 80)
            Case Else : Return String.Empty
        End Select
    End Function

#End Region



#End Region


#Region "Treats Helpers"
    Public Class TreatmentInfo


        Public Property Treat
        Public Property DisplayTreat As String
        Public Property PropertyName
        Public Property FillColor

    End Class

    Public Function GetAddedTreatmentsInfo(patientID As Integer, toothNum As Byte, toothName As String) As List(Of TreatmentInfo)
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Define the SQL query
        Dim query As String = "
                            SELECT 
                                Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyyy') + ')' AS DisplayTreat,
                                FillColor
                            FROM Patient_ToothTrt 
                            WHERE PatientID = @PatientID AND ToothNum = @ToothNum
                            ORDER BY TreatDate DESC"

        ' Use a Dapper query to execute and retrieve the results
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            ' Use Query(Of T) to fetch results and return them as a list of TreatmentInfo objects
            Return connection.Query(Of TreatmentInfo)(query, New With {
            .PatientID = patientID,
            .ToothNum = toothNum
        }).ToList()
        End Using
    End Function

    Public Function GetTreatment(patientID As Integer, toothNum As Byte, propName As String) As String
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Define the SQL query
        Dim query As String = "SELECT Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyyy') + ')  ' + ISNULL(ExternalClinicName,'') AS DisplayTreat " &
                         "FROM Patient_ToothTrt " &
                         "WHERE PatientID = @PatientID AND ToothNum = @ToothNum AND PropertyName = @propName " &
                         "ORDER BY TreatDate DESC"

        ' Use a Dapper query to execute and retrieve the results
        Using connection As New SqlConnection(connectionString)
            Try
                ' Use QueryFirstOrDefault to get a single string result or Nothing
                Dim result = connection.QueryFirstOrDefault(Of String)(query, New With {
                .PatientID = patientID,
                .ToothNum = toothNum,
                .PropName = propName
            })

                ' Return the result or empty string if nothing found
                Return If(result, String.Empty)
            Catch ex As Exception
                ' Handle potential errors (log them, etc.)
                ' Return empty string in case of error
                Return String.Empty
            End Try
        End Using
    End Function

    Public Function GetMobTreatment(patientID As Integer, toothNum As Byte, propName As String) As String
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Define the SQL query
        Dim query As String = "SELECT Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyyy') + ')  ' + ISNULL(ExternalClinicName,'') AS DisplayTreat " &
                         "FROM Patient_Mobile " &
                         "WHERE PatientID = @PatientID AND ToothNum = @ToothNum AND PropertyName = @propName " &
                         "ORDER BY TreatDate DESC"

        ' Use a Dapper query to execute and retrieve the results
        Using connection As New SqlConnection(connectionString)
            Try
                ' Use QueryFirstOrDefault to get a single string result or Nothing
                Dim result = connection.QueryFirstOrDefault(Of String)(query, New With {
                .PatientID = patientID,
                .ToothNum = toothNum,
                .PropName = propName
            })

                ' Return the result or empty string if nothing found
                Return If(result, String.Empty)
            Catch ex As Exception
                ' Handle potential errors (log them, etc.)
                ' Return empty string in case of error
                Return String.Empty
            End Try
        End Using
    End Function
    Public Function GetAddedTreatments(patientID As Integer, toothNum As Byte, toothName As String) As List(Of String)
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Define the SQL query
        Dim query As String = "
                            SELECT 
                                Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyyy') + ')  ' + ISNULL(ExternalClinicName,'') AS DisplayTreat
                            FROM Patient_ToothTrt 
                            WHERE PatientID = @PatientID AND ToothNum = @ToothNum
                            ORDER BY TreatDate DESC"

        ' Use a Dapper query to execute and retrieve the results
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            ' Use Query(Of T) to fetch results and return them as a list of strings
            Return connection.Query(Of String)(query, New With {
            .PatientID = patientID,
            .ToothNum = toothNum
        }).ToList()
        End Using
    End Function

    Public Function GetAddedDiags(patientID As Integer, toothNum As Byte, toothName As String) As List(Of String)
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Define the SQL query
        Dim query As String = "
                            SELECT 
                                Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyyy') + ')  ' + ISNULL(ExternalClinicName,'') AS DisplayTreat
                            FROM Patient_Diagnosis 
                            WHERE PatientID = @PatientID AND ToothNum = @ToothNum
                            ORDER BY TreatDate DESC"

        ' Use a Dapper query to execute and retrieve the results
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            ' Use Query(Of T) to fetch results and return them as a list of strings
            Return connection.Query(Of String)(query, New With {
            .PatientID = patientID,
            .ToothNum = toothNum
        }).ToList()
        End Using
    End Function

    Public Function GetAddedMobiles(patientID As Integer, toothNum As Byte, toothName As String) As List(Of String)
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Define the SQL query
        Dim query As String = "
                            SELECT 
                                Treat + '  -->  (' + FORMAT(TreatDate, 'dd/MM/yyyy') + ')  ' + ISNULL(ExternalClinicName,'') AS DisplayTreat
                            FROM Patient_Mobile 
                            WHERE PatientID = @PatientID AND ToothNum = @ToothNum
                            ORDER BY TreatDate DESC"

        ' Use a Dapper query to execute and retrieve the results
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            ' Use Query(Of T) to fetch results and return them as a list of strings
            Return connection.Query(Of String)(query, New With {
            .PatientID = patientID,
            .ToothNum = toothNum
        }).ToList()
        End Using
    End Function
    Public Function GetAddedTreatmentsInfo(patientID As Integer, toothNum As Byte, treat As String, propertyName As String) As TreatmentInfo
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Define the SQL query
        Dim query As String = "
                            SELECT TOP 1
                                Treat, PropertyName, FillColor
                            FROM 
                                Patient_ToothTrt 
                            WHERE 
                                PatientID = @PatientID AND ToothNum = @ToothNum AND Treat = @Treat AND PropertyName = @PropertyName
                            ORDER BY 
                                TreatDate DESC"

        ' Use a Dapper query to execute and retrieve the result
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            ' Multiple chart rows can match; QuerySingleOrDefault throws if >1 row
            Return connection.QueryFirstOrDefault(Of TreatmentInfo)(query, New With {
            .PatientID = patientID,
            .ToothNum = toothNum,
            .Treat = treat,
            .PropertyName = propertyName
        })
        End Using
    End Function

    Public Function GetTrtColor(ByVal Trt As String, ByVal patientid As Integer, toothNum As Byte) As Color
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        Dim treat As String = ""
        Dim query = "SELECT TOP 1 [FillColor] FROM [Patient_ToothTrt] WHERE Treat = @Treat AND PatientID=@PatientID AND ToothNum=@ToothNum ORDER BY TreatDate DESC"
        Using connection As New SqlConnection(connectionString)
            treat = connection.QueryFirstOrDefault(Of String)(query, New With {.Treat = Trt, .PatientID = patientid, .ToothNum = toothNum})
        End Using
        Return ColorTranslator.FromHtml(treat)
    End Function

    Public Function GetTrtColorByProp(ByVal Prop As String, ByVal patientid As Integer, toothNum As Byte) As Color
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        Dim treat As String = ""
        Dim query = "SELECT TOP 1 [FillColor] FROM [Patient_ToothTrt] WHERE [PropertyName] = @Prop AND PatientID=@PatientID AND ToothNum=@ToothNum ORDER BY TreatDate DESC"
        Using connection As New SqlConnection(connectionString)
            treat = connection.QueryFirstOrDefault(Of String)(query, New With {.PropertyName = Prop, .PatientID = patientid, .ToothNum = toothNum})
        End Using
        Return ColorTranslator.FromHtml(treat)
    End Function

    Public Function GetDefaultTrtColor(ByVal Trt As String) As Color
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Query to get the TrtColor for the selected treatment
        Dim query As String = "SELECT dbo.TblTRTS.DefFillColor
                                FROM dbo.TblTRTS  
                                WHERE dbo.TblTRTS.Trt = @Trt"


        ' Use a database connection to execute the query
        Using connection As New SqlClient.SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlClient.SqlCommand(query, connection)
                ' Add parameter to avoid SQL injection
                command.Parameters.AddWithValue("@Trt", Trt)

                ' Execute the query and read the result
                Dim result = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.ToString()) Then
                    ' Convert the color from the database to a Color object
                    Return ColorTranslator.FromHtml(result.ToString())
                Else
                    ' Handle case where no color is found
                    Return Color.Empty ' Default to no color
                End If
            End Using
        End Using
    End Function
    Public Function GetDefaultCapColor(ByVal Trt As String) As Color
        Select Case Trt
            Case "INDIRECT PULP CAPPING"
                Trt = "INDIRECTCAP"
            Case "DIRECT PULP CAPPING"
                Trt = "DIRECTCAP"
            Case "PULPOTOMY"
                Trt = "PULPCAP"
        End Select

        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Query to get the TrtColor for the selected treatment
        Dim query As String = "SELECT dbo.TblTRTS.DefFillColor
                                FROM dbo.TblTRTS  
                                WHERE dbo.TblTRTS.Trt = @Trt"


        ' Use a database connection to execute the query
        Using connection As New SqlClient.SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlClient.SqlCommand(query, connection)
                ' Add parameter to avoid SQL injection
                command.Parameters.AddWithValue("@Trt", Trt)

                ' Execute the query and read the result
                Dim result = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.ToString()) Then
                    ' Convert the color from the database to a Color object
                    Return ColorTranslator.FromHtml(result.ToString())
                Else
                    ' Handle case where no color is found
                    Return Color.Empty ' Default to no color
                End If
            End Using
        End Using
    End Function
    Public Function GetDefaultRootColor(ByVal Trt As String) As Color
        Select Case Trt
            Case "INDIRECT PULP CAPPING"
                Trt = "INDIRECTROOT"
            Case "DIRECT PULP CAPPING"
                Trt = "DIRECTROOT"
            Case "PULPOTOMY"
                Trt = "PULPROOT"
        End Select

        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Query to get the TrtColor for the selected treatment
        Dim query As String = "SELECT dbo.TblTRTS.DefFillColor
                                FROM dbo.TblTRTS  
                                WHERE dbo.TblTRTS.Trt = @Trt"


        ' Use a database connection to execute the query
        Using connection As New SqlClient.SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlClient.SqlCommand(query, connection)
                ' Add parameter to avoid SQL injection
                command.Parameters.AddWithValue("@Trt", Trt)

                ' Execute the query and read the result
                Dim result = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.ToString()) Then
                    ' Convert the color from the database to a Color object
                    Return ColorTranslator.FromHtml(result.ToString())
                Else
                    ' Handle case where no color is found
                    Return Color.Empty ' Default to no color
                End If
            End Using
        End Using
    End Function

    Public Function GetCutomTrtColorByProp(ByVal Prop As String) As Color
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        Dim treat As String = ""
        Dim query = "SELECT        dbo.TblTRTS.TrtColor
                     FROM            dbo.Shapes INNER JOIN
                         dbo.TblTRTS ON dbo.Shapes.ShapeID = dbo.TblTRTS.ShapeID
                     WHERE dbo.Shapes.ShapeName = @Prop "
        ' Use a database connection to execute the query
        Using connection As New SqlClient.SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlClient.SqlCommand(query, connection)
                ' Add parameter to avoid SQL injection
                command.Parameters.AddWithValue("@Prop", Prop)

                ' Execute the query and read the result
                Dim result = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.ToString()) Then
                    ' Convert the color from the database to a Color object
                    Return ColorTranslator.FromHtml(result.ToString())
                Else
                    ' Handle case where no color is found
                    Return Color.Empty ' Default to no color
                End If
            End Using
        End Using
    End Function
    Public Function GetCustomTrtColor(ByVal Trt As String) As Color
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        Dim query As String = "SELECT dbo.TblTRTS.TrtColor 
                                FROM dbo.TblTRTS 
                                WHERE dbo.TblTRTS.Trt = @Trt"


        ' Query to get the TrtColor for the selected treatment


        ' Use a database connection to execute the query
        Using connection As New SqlClient.SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlClient.SqlCommand(query, connection)
                ' Add parameter to avoid SQL injection
                command.Parameters.AddWithValue("@Trt", Trt)

                ' Execute the query and read the result
                Dim result = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.ToString()) Then
                    ' Convert the color from the database to a Color object
                    Return ColorTranslator.FromHtml(result.ToString())
                Else
                    ' Handle case where no color is found
                    Return Color.Empty ' Default to no color
                End If
            End Using
        End Using
    End Function

    Public Function GetOrigOldTrtColor(ByVal oldTrt As String) As Color
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Query to get the TrtColor for the selected treatment
        Dim query As String = "SELECT TrtColor FROM TblTrt WHERE OldTrt = @OldTrt"

        ' Use a database connection to execute the query
        Using connection As New SqlClient.SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlClient.SqlCommand(query, connection)
                ' Add parameter to avoid SQL injection
                command.Parameters.AddWithValue("@OldTrt", oldTrt)

                ' Execute the query and read the result
                Dim result = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.ToString()) Then
                    ' Convert the color from the database to a Color object
                    Return ColorTranslator.FromHtml(result.ToString())
                Else
                    ' Handle case where no color is found
                    Return Color.Empty ' Default to no color
                End If
            End Using
        End Using
    End Function


#End Region


#Region "Treats"


#Region "TRTS"

    Private Function GetIsAdult(toothNum As Integer) As Boolean
        Select Case toothNum
            Case 21 To 48 : Return True
            Case Else : Return False
        End Select
    End Function

    Private Function GetIsKid(toothNum As Integer) As Boolean
        Select Case toothNum
            Case 51 To 85 : Return True
            Case Else : Return False
        End Select
    End Function

    Public Function GetOldTrtShape(ByVal oldTrt As String) As String
        ' Establish the connection
        Dim connection As New SqlConnection()
        connection = DentistXDATA.GetConnection()

        Dim query As String = "
        SELECT dbo.Shapes.ShapeName
        FROM dbo.Shapes INNER JOIN
                         dbo.TblTRTS ON dbo.Shapes.ShapeID = dbo.TblTRTS.ShapeID
        WHERE [OldTrt] = @OldTrt"
        Try
            Using connection
                connection.Open()
                Dim shape As String = connection.QueryFirstOrDefault(Of String)(query, New With {.OldTrt = oldTrt})
                Return shape
            End Using
        Catch ex As Exception
            MsgBox($"Error retrieving Shape: {ex.Message}", MsgBoxStyle.Critical, "Database Error")
            Return Nothing
        End Try
    End Function

    Public Function GetTrtShape(ByVal treat As String) As String
        ' Establish the connection
        Dim connection As New SqlConnection()
        connection = DentistXDATA.GetConnection()

        Dim query As String = "
        SELECT dbo.Shapes.ShapeName
        FROM dbo.Shapes INNER JOIN
                         dbo.TblTRTS ON dbo.Shapes.ShapeID = dbo.TblTRTS.ShapeID
        WHERE [Trt] = @Trt"
        Try
            Using connection
                connection.Open()
                Dim shape As String = connection.QueryFirstOrDefault(Of String)(query, New With {.Trt = treat})
                Return shape
            End Using
        Catch ex As Exception
            MsgBox($"Error retrieving Shape: {ex.Message}", MsgBoxStyle.Critical, "Database Error")
            Return Nothing
        End Try
    End Function

    Public Function GetShapeIDByTrt(ByVal treat As String) As Integer
        ' Establish the connection
        Dim connection As New SqlConnection()
        connection = DentistXDATA.GetConnection()

        Dim query As String = "
        SELECT TOP 1 dbo.Shapes.ShapeID
        FROM dbo.Shapes INNER JOIN
                         dbo.TblTRTS ON dbo.Shapes.ShapeID = dbo.TblTRTS.ShapeID
        WHERE dbo.TblTRTS.Trt = @Trt
        ORDER BY dbo.Shapes.ShapeID"
        Try
            Using connection
                connection.Open()
                ' QuerySingleOrDefault throws if >1 row; duplicates in TblTRTS are possible for same Trt
                Return If(connection.QueryFirstOrDefault(Of Integer?)(query, New With {.Trt = treat}), 0)
            End Using
        Catch ex As Exception
            MsgBox($"Error retrieving Shape: {ex.Message}", MsgBoxStyle.Critical, "Database Error")
            Return 0
        End Try
    End Function



    Public Function GetShapeIDByOldTrt(ByVal oldTrt As String) As Integer
        ' Establish the connection
        Dim connection As New SqlConnection()
        connection = DentistXDATA.GetConnection()

        Dim query As String = "
        SELECT TOP 1 dbo.Shapes.ShapeID
        FROM dbo.Shapes INNER JOIN
                         dbo.TblTRTS ON dbo.Shapes.ShapeID = dbo.TblTRTS.ShapeID
        WHERE [oldTrt] = @oldTrt
        ORDER BY dbo.Shapes.ShapeID"
        Try
            Using connection
                connection.Open()
                Return If(connection.QueryFirstOrDefault(Of Integer?)(query, New With {.oldTrt = oldTrt}), 0)
            End Using
        Catch ex As Exception
            MsgBox($"Error retrieving Shape: {ex.Message}", MsgBoxStyle.Critical, "Database Error")
            Return 0
        End Try
    End Function

    Public Function GetNewTrt(ByVal oldTrt As String) As String

        ' Establish the connection
        Dim connection As New SqlConnection()
        connection = DentistXDATA.GetConnection()

        Dim query As String = "
        SELECT [Trt]
        FROM [dbo].[TblTRTS]
        WHERE [OldTrt] = @OldTrt"
        Try
            Using connection
                connection.Open()
                Dim trt As String = connection.QueryFirstOrDefault(Of String)(query, New With {.OldTrt = oldTrt})
                Return trt
            End Using
        Catch ex As Exception
            MsgBox($"Error retrieving Trt: {ex.Message}", MsgBoxStyle.Critical, "Database Error")
            Return Nothing
        End Try
    End Function



    Public Function GetOldTrt(ByVal trt As String) As String
        ' Establish the connection
        Dim connection As New SqlConnection()
        connection = DentistXDATA.GetConnection()
        Dim query As String = "
        SELECT [OldTrt]
        FROM [dbo].[TblTRTS]
        WHERE [Trt] = @Trt"
        Try
            Using connection
                connection.Open()
                Dim oldTrt As String = connection.QueryFirstOrDefault(Of String)(query, New With {.Trt = GetFirstTreatmentPart(trt)})
                Return oldTrt
            End Using
        Catch ex As Exception
            MsgBox($"Error retrieving OldTrt: {ex.Message}", MsgBoxStyle.Critical, "Database Error")
            Return Nothing
        End Try
    End Function




#End Region

    ' Helper function to Get treat LeVeL
    Public Function GetLVL1(treat As String) As Byte
        Dim query As String = "
        SELECT        dbo.TblTRTS.TrtID, dbo.TblTRTS.Trt, dbo.TblTRTS.ShapeID, dbo.Shapes.ShapeName, dbo.TblTRTS.TrtDetails, dbo.TblTRTS.TrtAr,
                      dbo.TblTRTS.TrtArDetails, dbo.TblTRTS.ToothID, dbo.TblTRTS.OldTrt, dbo.TblTRTS.TrtGroup, dbo.TblTRTS.TrtColor
        FROM            dbo.TblTRTS INNER JOIN
                         dbo.Shapes ON dbo.TblTRTS.ShapeID = dbo.Shapes.ShapeID"

        Dim x As Byte = 0
        ' HEMISECTION APICECTOMY  EXTRACTION  IMPLANT ,
        Select Case treat
            Case "APICECTOMY"
                x = 1
            Case "HEMISECTION"
                x = 2
            Case "EXTRACTION"
                x = 3
            Case "IMPLANT"
                x = 4
            Case Else
                x = 0
        End Select
        Return x
    End Function

    ' Helper function to Get treat LeVeL
    Public Function GetLVL(treat As String) As Byte
        'Level 0 - 2: Basic treatments(fillings, veneers, root canals, etc.)
        'Level 3: Crown on natural tooth
        'Level 4: Extracted tooth
        'Level 5: Implant placed
        'Level 6: Implant with components (healing cap, abutment)
        'Level 7: Crown on implant 
        'Level 8: Bridge
        Dim x As Byte = 0
        ' HEMISECTION APICECTOMY  EXTRACTION  IMPLANT ,
        Select Case GetFirstTreatmentPart(treat)
            Case "APICECTOMY"
                x = 1
            Case "HEMISECTION"
                x = 2
            Case "STAINLESS STEEL CROWN T", "METAL CROWN T", "ZERCONIA CROWN T", "PFM CROWN T", "EMAX CROWN T", "TEMPORARY CROWN T"
                x = 3
            Case "BUILD UP GI", "BUILD UP ACR", "BUILD UP COM"
                x = 3
            Case "EXTRACTION"
                x = 4
            Case "IMPLANT"
                x = 5
            Case "EXTRACTION + IMPLANT"
                x = 5
            Case "HEALING CAP", "ABUTMENT"
                x = 6
            Case "STAINLESS STEEL CROWN I", "METAL CROWN I", "ZERCONIA CROWN I", "PFM CROWN I", "EMAX CROWN I", "TEMPORARY CROWN I"
                x = 7
            Case "METAL BRIDGE", "ZERCONIA BRIDGE", "PFM BRIDGE", "EMAX BRIDGE", "TEMP BRIDGE"
                x = 8
            Case "CD", "COMPLETE DENTURE", "RPD", "REMOVABLE PARTIAL DENTURE"
                x = 9

            Case Else
                x = 0
        End Select
        Return x
    End Function

    Public Function GetTreatmentLevel(treatment As Patient_ToothTrt) As Byte
        ' Clear level hierarchy:
        ' 0-2: Basic treatments
        ' 3: Crown on natural tooth
        ' 4: Extracted tooth
        ' 5: Implant placed
        ' 6: Implant components
        ' 7: Crown on implant or bridge
        ' 8: Dentures

        Select Case treatment.Treat.ToUpper()
        ' Basic treatments (0-2)
            Case "APICECTOMY" : Return 1
            Case "HEMISECTION" : Return 2

        ' Crowns - level depends on context
            Case "STAINLESS STEEL CROWN", "METAL CROWN", "ZERCONIA CROWN",
             "PFM CROWN", "EMAX CROWN", "TEMPORARY CROWN"
                Return If(treatment.isOnImplant, 7, 3)

        ' Extraction/Implant
            Case "EXTRACTION" : Return 4
            Case "IMPLANT" : Return 5
            Case "HEALING CAP", "ABUTMENT" : Return 6

        ' Bridges (always level 7)
            Case "METAL BRIDGE", "ZERCONIA BRIDGE", "PFM BRIDGE",
             "EMAX BRIDGE", "TEMP BRIDGE" : Return 7

        ' Dentures
            Case "CD", "COMPLETE DENTURE", "RPD", "REMOVABLE PARTIAL DENTURE" : Return 8

                ' Default (fillings, veneers, etc.)
            Case Else : Return 0
        End Select
    End Function


    ' Helper method to extract first part of treatment text (before " - " separator)
    Public Function GetFirstTreatmentPart(treatmentText As String) As String
        If String.IsNullOrEmpty(treatmentText) Then Return treatmentText

        ' Look for the standard " - " separator
        Dim separatorIndex As Integer = treatmentText.IndexOf(" - ")
        If separatorIndex > 0 Then
            ' Return only the part before the separator
            Return treatmentText.Substring(0, separatorIndex).Trim()
        End If

        ' If no separator found, return the original text
        Return treatmentText.Trim()
    End Function


#End Region




    Function ExtractOnesDigit(ByVal number As Integer) As Integer
        Return Math.Abs(number) Mod 10
    End Function

    Public Function ExtractDigit(input As String) As Integer
        ' Use LINQ to find the first digit in the string
        Dim digit As Char = input.FirstOrDefault(Function(c) Char.IsDigit(c))

        ' Return the digit as an integer, or -1 if no digit is found
        If digit <> Char.MinValue Then
            Return Integer.Parse(digit.ToString())
        Else
            Return -1 ' Return -1 if no digit is found
        End If
    End Function

    Public Function ExtractSingleDigit(input As String) As Integer?
        Dim match As Match = Regex.Match(input, "\d")
        If match.Success Then
            Return Integer.Parse(match.Value)
        End If
        Return Nothing ' Return Nothing if no digit is found
    End Function


#Region "Mobile"

    Public Function GetToothName(toothNum As Byte) As String
        If toothNum < 11 OrElse toothNum > 48 Then
            Return "InvalidTooth"
        End If

        Dim quadrant As Integer = toothNum \ 10 ' First digit
        Dim number As Integer = toothNum Mod 10 ' Second digit

        If number < 1 OrElse number > 8 Then
            Return "InvalidTooth"
        End If

        Dim prefix As String = quadrant
        Select Case quadrant
            Case 1 : prefix = "RUOUT"
            Case 2 : prefix = "LUOUT"
            Case 3 : prefix = "LDOUT"
            Case 4 : prefix = "RDOUT"
            Case Else : Return "InvalidTooth"
        End Select

        Return prefix & number.ToString()
    End Function

    ''' <summary>Label for accounting/detail text after " ==>> ": full quadrant wording + tooth index (e.g. UPPER RIGHT 3), honoring <see cref="TreatsUserControl.AlternateQuadrantLabelsEnabled"/>.</summary>
    Public Function GetShortToothNameWithDash(toothNum As Byte) As String
        Dim adult = ToothSvgQuadrantNaming.GetAdultToothTrtLookupNameByFdi(CInt(toothNum))
        If Not String.IsNullOrWhiteSpace(adult) Then Return adult

        Dim kid = ToothSvgQuadrantNaming.GetKidToothTrtLookupNameByFdi(CInt(toothNum))
        If Not String.IsNullOrWhiteSpace(kid) Then Return kid

        Return "InvalidTooth"
    End Function

    ''' <summary><c>Patient_Trts.Detail</c> for one tooth: <c>treat &amp; " ==>> " &amp; full tooth label</c> (same as add and edit treatment flows).</summary>
    Public Function FormatPatientTrtsDetail(treat As String, toothNum As Byte) As String
        Return treat & " ==>> " & GetShortToothNameWithDash(toothNum)
    End Function

    Public Function GetShortToothName(toothNum As Byte) As String
        If toothNum < 11 OrElse toothNum > 48 Then
            Return "InvalidTooth"
        End If

        Dim quadrant As Integer = toothNum \ 10 ' First digit
        Dim number As Integer = toothNum Mod 10 ' Second digit

        If number < 1 OrElse number > 8 Then
            Return "InvalidTooth"
        End If

        Dim prefix As String = quadrant
        Select Case quadrant
            Case 1 : prefix = "RU"
            Case 2 : prefix = "LU"
            Case 3 : prefix = "LD"
            Case 4 : prefix = "RD"
            Case Else : Return "InvalidTooth"
        End Select

        Return prefix & number.ToString()
    End Function
    Public Function GetTopToothName(toothName As String) As String
        If String.IsNullOrWhiteSpace(toothName) OrElse Not toothName.Contains("OUT") Then
            Return "InvalidToothName"
        End If

        Return toothName.Replace("OUT", "TOP")
    End Function

#End Region





End Module
