Imports System.Collections.Concurrent
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports Dapper
Imports DevExpress.Utils.Extensions
Imports DevExpress.XtraEditors
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraTreeList

Public Class RxCTL
    Implements IPatientAwareUserControl



    Private Sub IPatientAwareUserControl_LoadPatientData(patientId As Integer) Implements IPatientAwareUserControl.LoadPatientData
        Me.Hide()
        LoadPatientData(patientId)
        Me.Show()
    End Sub

    Public Sub LoadPatientData(patientId As Integer)
        LoadData(patientId)
    End Sub


    Private clsMedGrp As IEnumerable(Of MedicineGroups)
    Private clsMedFam As IEnumerable(Of MedicineFamily)
    Private clsMedScFam As IEnumerable(Of MedScienceFamily)
    Private clsMedItm As IEnumerable(Of MedicineItems)
    Private clsMedShp As IEnumerable(Of MedicineShape)
    Private clsMedDoz As IEnumerable(Of MedicineDoze)

    Private clsMedGrpData As New MedicineGroupsDATA
    Private clsMedFamData As New MedicineFamilyDATA
    Private clsMedScFamData As New MedScienceFamilyDATA
    Private clsMedItmData As New MedicineItemsDATA
    Private clsMedShpData As New MedicineShapeDATA
    Private clsMedDozData As New MedicineDozeDATA

    Private clsRx As IEnumerable(Of Patient_RX)
    Private clsRxData As New Patient_RXDATA
    Private clsRxBodyData As New RxBodyDATA
    Private clsRxBody As IEnumerable(Of RxBody)
    Private clsPatientData As New PatientDATA
    Private clsPatient As Patient
    Private clsRxViewS As IEnumerable(Of RxView)
    Private clsRxView As RxView

    Public Sub New()
        Dim sw = StartTimer()
        ' This call is required by the designer.
        InitializeComponent()
        ' Enable double buffering to reduce flickering
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.UserPaint Or
                 ControlStyles.DoubleBuffer, True)
        UpdateStyles()
        StoreOriginalBounds(Me)
        ' Add any initialization after the InitializeComponent() call.
        AddHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged

        LogTime("Public Sub New", Me.Name, sw)


    End Sub

    Public Sub New(ByVal clsPatient As Patient)
        Dim sw = StartTimer()
        ' This call is required by the designer.
        InitializeComponent()
        StoreOriginalBounds(Me)
        ' Add any initialization after the InitializeComponent() call.
        AddHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
        'Attach the event handler
        CurrentPatient = clsPatient
        LogTime("Public Sub New", Me.Name, sw)

    End Sub

    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1156
    Private Const OriginalPanelHeight As Integer = 654
    Private ReadOnly originalPanelSize As New Size(OriginalPanelWidth, OriginalPanelHeight)

    Private Sub StoreOriginalBounds(container As Control)
        Dim sw As New Stopwatch
        sw.Start()

        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
        sw.Stop()

    End Sub
    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return
        Dim sw As New Stopwatch
        sw.Start()
        Dim widthRatio = CSng(Me.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(Me.Height) / OriginalPanelHeight

        For Each kvp In controlBoundsCache
            kvp.Key.SetBounds(
            CInt(kvp.Value.X * widthRatio),
            CInt(kvp.Value.Y * heightRatio),
            CInt(kvp.Value.Width * widthRatio),
            CInt(kvp.Value.Height * heightRatio))
        Next
        sw.Stop()
    End Sub

    Private Sub HandlePatientChanged(sender As Object, e As PatientChangedEventArgs) 'Handles AdultFullJaw1.ParentChanged
        ' Update JawsForm2 based on new patient selection
        Dim sw = StartTimer()
        If e.NewPatient Is Nothing Then Exit Sub

        CurrentPatient = e.NewPatient
        ' Do whatever is needed when a new patient is selected
        Dim hlth As String = e.NewPatient.Health
        If hlth <> "سليم" Then
            LabelControl2.ForeColor = Color.Red
        Else
            LabelControl2.ForeColor = Color.Blue
        End If
        LabelControl2.Text = hlth
        If CurrentPatient.PatientID > 0 Then
            ValueFromParent = CurrentPatient.PatientID
            Me.Text = CurrentPatient.PatientName
            LabelControl2.Text = e.NewPatient.Health
        End If
        LogTime(NameOf(HandlePatientChanged), Me.Name, sw)
    End Sub


    Private Sub RxCTL_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        RemoveHandler PatientEventManager.PatientChanged, AddressOf HandlePatientChanged
    End Sub
    Private Sub PatientRXTreeFrm_Load(sender As Object, e As EventArgs) Handles Me.Load
        If CurrentPatient Is Nothing Then Exit Sub
        Me.Text = CurrentPatient.PatientName
    End Sub

    Private Sub PatientRXTreeFrm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ResizeControlsProportionally()
    End Sub








    Private Sub LoadClasses()
        ' MedicineGroups
        clsMedGrp = clsMedGrpData.SelectAll
        If MedicineGroupsBindingSource Is Nothing Then
            MedicineGroupsBindingSource = New BindingSource()
        End If
        ' Set the DataSource
        MedicineGroupsBindingSource.DataSource = clsMedGrp.ToList()

        'MedicineFamily
        clsMedFam = clsMedFamData.SelectAll
        If MedicineFamilyBindingSource Is Nothing Then
            MedicineFamilyBindingSource = New BindingSource()
        End If
        ' Set the DataSource
        MedicineFamilyBindingSource.DataSource = clsMedFam.ToList()

        'MedScienceFamily
        clsMedScFam = clsMedScFamData.SelectAll
        If MedScienceFamilyBindingSource Is Nothing Then
            MedScienceFamilyBindingSource = New BindingSource()
        End If
        ' Set the DataSource
        MedScienceFamilyBindingSource.DataSource = clsMedScFam.ToList()

        'MedicineItems
        clsMedItm = clsMedItmData.SelectAll
        If MedicineItemsBindingSource Is Nothing Then
            MedicineItemsBindingSource = New BindingSource()
        End If
        ' Set the DataSource
        MedicineItemsBindingSource.DataSource = clsMedItm.ToList()
        Me.LabelControl1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.MedicineItemsBindingSource, "Notes", True))

        'MedicineShape
        clsMedShp = clsMedShpData.SelectAll
        If MedicineShapeBindingSource Is Nothing Then
            MedicineShapeBindingSource = New BindingSource()
        End If
        ' Set the DataSource
        MedicineShapeBindingSource.DataSource = clsMedShp.ToList()

        'MedicineDoze
        clsMedDoz = clsMedDozData.SelectAll
        If MedicineDozeBindingSource Is Nothing Then
            MedicineDozeBindingSource = New BindingSource()
        End If
        ' Set the DataSource
        MedicineDozeBindingSource.DataSource = clsMedDoz.ToList()


    End Sub

    Private Sub LoadRX(ByVal patientID As Integer)

        clsPatient = New Patient
        clsPatient.PatientID = patientID
        clsPatient = clsPatientData.Select_Record(clsPatient)
        If clsPatient IsNot Nothing Then
            Dim hlth As String = clsPatient.Health
            If hlth <> "سليم" Then
                LabelControl2.ForeColor = Color.Red
            Else
                LabelControl2.ForeColor = Color.Blue
            End If
            LabelControl2.Text = hlth
            'Patient_RX
            clsRx = clsPatientData.GetPatient_RX(clsPatient)
            If Patient_RXBindingSource Is Nothing Then
                Patient_RXBindingSource = New BindingSource()
            End If
            ' Set the DataSource
            Patient_RXBindingSource.DataSource = clsRx.ToList()
            'Patient_RXView
            clsRxViewS = GetRxView(patientID)
            PatientRXVIEWBindingSource.DataSource = clsRxViewS.ToList
            ' BIND ONLY ONCE — clear old bindings and add just one
            Me.Ultra.DataBindings.Clear()
            Me.Ultra.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.PatientRXVIEWBindingSource, "RX", True))
            'Patient_RXBody 
            clsRxBody = clsRxBodyData.SelectAll
            RxBodyBindingSource.DataSource = clsRxBody.ToList
        End If
    End Sub

    Private Sub PatientRX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadClasses()
            LoadRX(PatientID)
            TreeViewFill()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Public WriteOnly Property ValueFromParent() As Integer
        Set(ByVal Value As Integer)
            LoadRX(Value)
        End Set
    End Property

    Public Sub LoadData(ByVal PatientID As Integer)
        If CurrentPatient Is Nothing Then Exit Sub
        Try
            Me.SuspendLayout()
            LoadRX(PatientID)

        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        Finally
            Me.ResumeLayout()
        End Try
    End Sub

    Private Function GetRxView(ByVal patientID As Integer) As List(Of RxView)
        Dim sql As String = "SELECT        dbo.Patient_RX.RxID, dbo.Patient.PatientID, dbo.Patient.PatientName, dbo.Patient.Sex, dbo.Patient.Age,
                                           dbo.Patient_RX.RXDate, dbo.Patient_RX.RX
                            FROM            dbo.Patient INNER JOIN
                            dbo.Patient_RX ON dbo.Patient.PatientID = dbo.Patient_RX.PatientID
                            WHERE dbo.Patient.PatientID = @PatientID"
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            Return conn.Query(Of RxView)(sql, New With {.PatientID = patientID}).ToList()
        End Using

    End Function

    Public Class RxView
        Property RxID As Integer
        Property PatientID As Integer
        Property PatientName As String
        Property Sex As String
        Property Age As Integer
        Property RXDate As DateTime
        Property RX As String
    End Class

#Region "Patient RX"


    Private Sub btAddRX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToRX.Click

        Try
            Dim selectedNode As TreeNode = TV.SelectedNode ' e.Node
            ' Check if the clicked node is a DozeNode
            If selectedNode.Name = "MedicineDoze" Then
                ' Retrieve texts from the selected node and its parents
                Dim dozeText As String = selectedNode.Text
                Dim shapeText As String = selectedNode.Parent?.Text
                Dim itemText As String = selectedNode.Parent?.Parent?.Text

                ' Ensure parent nodes exist
                If String.IsNullOrEmpty(shapeText) OrElse String.IsNullOrEmpty(itemText) Then
                    MsgBox("Incomplete node hierarchy. Ensure proper data is loaded.", MsgBoxStyle.Exclamation)
                    Return
                End If

                ' Format the output string
                Dim formattedText As String = $"*  {itemText} {shapeText}" & vbCrLf & $"    {dozeText}" & vbCrLf & vbCrLf

                ' Append the formatted text to the Ultra.Text control
                Me.Ultra.Text += formattedText
            End If
        Catch ex As Exception
            MsgBox($"Error: {ex.Message}", MsgBoxStyle.Critical)
        End Try


    End Sub

    Private Sub btSaveRX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveRX.Click
        If String.IsNullOrWhiteSpace(Me.Ultra.Text) Then
            MsgBox(If(Eng, "You Can't Save Empty RX...", "لا يمكن حفظ وصفة فارغة...."))
            Exit Sub
        End If

        Try
            Dim RXdat As Date = Date.Now

            ' Replace Qrs.Patient_RXInsert with your class method:
            clsRxData.Add(New Patient_RX With {.PatientID = PatientID, .RXDate = RXdat, .RX = Me.Ultra.Text})

            ' Refresh RX list
            LoadRX(PatientID)
            Me.btnAddToRX.Enabled = False
            PatientRXVIEWBindingSource.MoveLast()
            Patient_RXBindingSource.MoveLast()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '=====================================

    End Sub

    Private Sub btNewRX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btNewRX.Click
        Me.Ultra.Text = ""
        Me.btnAddToRX.Enabled = True
        Me.btSaveRX.Enabled = True
    End Sub
    Private Sub RxDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxDel.Click
        Try
            Dim msg As String = If(Eng, "Delete The Record????", "هل تريد حذف الوصفة؟؟؟")
            If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim currentRX = CType(PatientRXVIEWBindingSource.Current, DataRowView)
                If currentRX IsNot Nothing Then
                    Dim rxID As Integer = CInt(currentRX("RXID"))
                    Dim rx As New Patient_RX With {.RxID = rxID}
                    clsRxData.Delete(rx)
                    LoadRX(PatientID)
                End If
            Else
                LoadRX(PatientID)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '===============================

    End Sub
    Private Sub RxSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxSave.Click
        Try
            Me.Validate()

        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PatientRXVIEWBindingSource_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatientRXVIEWBindingSource.PositionChanged

        Dim RxRow = CType(PatientRXVIEWBindingSource.Current, RxView)
        If RxRow IsNot Nothing Then
            RxID = RxRow.RxID
        End If
    End Sub



    Private Sub btPrintFullRX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPrintFullRX.Click
        If Me.Ultra.Text = "" OrElse RxID = -1 Then
            MsgBox(If(Eng, "You Cant Print Empty RX", "لا يمكن طباعة وصفة فارغة...."))
            Exit Sub
        End If
        Try
            Dim wimg As Image = Nothing
            Dim s As String = ""
            Dim useimg, usetxt As Boolean
            'Patient_RXBody 
            clsRxBody = clsRxBodyData.SelectAll
            RxBodyBindingSource.DataSource = clsRxBody.ToList
            Dim row As RxBody = CType(RxBodyBindingSource.Current, RxBody) 'clsRxBody?.FirstOrDefault()
            If row IsNot Nothing Then
                useimg = row.UseWtrImg
                usetxt = row.UseWtrText
                If row.WtrImg IsNot Nothing AndAlso row.WtrImg.Length > 0 Then
                    Using ms As New IO.MemoryStream(row.WtrImg)
                        Using original = Image.FromStream(ms)
                            wimg = CType(original.Clone(), Image) ' Clone to avoid stream issues
                        End Using
                    End Using


                    Dim picPath = IO.Path.Combine(Application.StartupPath, "Images", "Wtr.jpg")

                    Dim dirPath = IO.Path.GetDirectoryName(picPath)
                    If Not IO.Directory.Exists(dirPath) Then IO.Directory.CreateDirectory(dirPath)
                    If IO.File.Exists(picPath) Then IO.File.Delete(picPath)

                    Try
                        ' Fully recreate a bitmap into memory (does not depend on stream)
                        Dim tempBmp As New Bitmap(wimg.Width, wimg.Height)
                        Using g As Graphics = Graphics.FromImage(tempBmp)
                            g.DrawImage(wimg, 0, 0, wimg.Width, wimg.Height)
                        End Using

                        tempBmp.Save(picPath, Imaging.ImageFormat.Jpeg)
                    Catch ex As Exception
                        MsgBox($"Image save failed: {ex.Message}")
                    End Try
                End If


                If Not String.IsNullOrEmpty(row.WtrText) Then
                    s = row.WtrText
                End If

                Dim rpt As New MainRX(PatientID, RxID)
                Dim pictureWatermark As New Watermark()

                If wimg IsNot Nothing AndAlso useimg Then
                    pictureWatermark.ImageSource = ImageSource.FromFile(IO.Path.Combine(Application.StartupPath, "Images", "Wtr.jpg"))
                    pictureWatermark.ImageAlign = ContentAlignment.MiddleCenter
                    pictureWatermark.ImageTiling = False
                    pictureWatermark.ImageViewMode = ImageViewMode.Zoom
                    pictureWatermark.ImageTransparency = 200
                    pictureWatermark.ShowBehind = True
                End If

                If s.Length > 0 AndAlso usetxt Then
                    pictureWatermark.Text = s
                    pictureWatermark.TextDirection = DirectionMode.ForwardDiagonal
                    pictureWatermark.ForeColor = Color.DodgerBlue
                    pictureWatermark.TextTransparency = 50
                    pictureWatermark.ShowBehind = False
                End If

                rpt.Watermark.CopyFrom(pictureWatermark)

                Dim printTool As New ReportPrintTool(rpt)
                printTool.ShowPreviewDialog()
            Else
                MsgBox(If(Eng,
                      "The Rx Details Is Empty....." & vbCrLf & "Fill The Rx Details And Try Again.",
                      "تفاصيل الوصفة فارغة...." & vbCrLf & "قم بتعبئة تفاصيل الوصفة وحاول مرة اخرى."))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub btnPrintRX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintRX.Click

        Try
            Dim rpt As New RxPrint(PatientID, RxID)
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            ' Make sure the document is generated
            rpt.CreateDocument()
            Dim previewForm As Form = printTool.PreviewForm

            ' Force manual positioning and reposition once the form is shown
            previewForm.StartPosition = FormStartPosition.Manual
            AddHandler previewForm.Shown, Sub(s, ev)
                                              Try
                                                  Dim scr = Screen.FromControl(Me).WorkingArea
                                                  Dim x = scr.Left + (scr.Width - previewForm.Width) \ 2
                                                  Dim y = scr.Top + (scr.Height - previewForm.Height) \ 2
                                                  previewForm.Location = New Point(Math.Max(scr.Left, x), Math.Max(scr.Top, y))
                                              Catch
                                                  ' swallow any error (fallback to default positioning)
                                              End Try
                                          End Sub

            previewForm.ShowDialog(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Ultra_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ultra.TextChanged

        If Me.Ultra.Text = "" Then
            Me.btSaveRX.Enabled = False
        End If
    End Sub

    Private Sub btnEditRxDetail_Click(sender As Object, e As EventArgs) Handles btnEditRxDetail.Click
        FrmRxDetails.ShowDialog()

    End Sub

#End Region

#Region "New TreeView"

    Private Sub TreeViewFillNew()
        Try
            TV.BeginUpdate()
            TV.Nodes.Clear()

            Dim MainHeader As New TreeNode With {
            .Tag = 0,
            .Name = "Head",
            .ForeColor = Color.Red,
            .NodeFont = New Font(TV.Font, FontStyle.Bold)
        }

            If Eng Then
                MainHeader.Text = "Medicine Tree"
            Else
                MainHeader.Text = "شجرة الادوية"
            End If

            TV.Nodes.Add(MainHeader)

            ' Populate first level under main header
            AddSubNodeNew(MainHeader, "MedicineGroups", 0)

            TV.Nodes(0).Expand()
            TV.Select()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            TV.EndUpdate()
        End Try
    End Sub



#Region "Lazy Load"

    Private Sub TreeViewFillLazy()
        Try
            TV.BeginUpdate()
            TV.Nodes.Clear()

            Dim MainHeader As New TreeNode With {
            .Tag = 0,
            .Name = "Head",
            .ForeColor = Color.Red,
            .NodeFont = New Font(TV.Font, FontStyle.Bold)
        }

            If Eng Then
                MainHeader.Text = "Medicine Tree"
            Else
                MainHeader.Text = "شجرة الادوية"
            End If

            TV.Nodes.Add(MainHeader)

            ' Add first-level nodes under the main header on demand (lazy load)
            MainHeader.Nodes.Add("Loading...") ' placeholder

            TV.Nodes(0).Expand()
            TV.Select()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            TV.EndUpdate()
        End Try
    End Sub




    'Private Sub TV_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles TV.BeforeExpand
    'Dim currentNode As TreeNode = e.Node

    ' Check if the node has the placeholder child "Loading..."
    '    If currentNode.Nodes.Count = 1 AndAlso currentNode.Nodes(0).Text = "Loading..." Then
    '        ' Remove placeholder
    '        currentNode.Nodes.Clear()

    '        ' Determine next level name and ParentID for AddSubNode
    '        Dim levelName As String = currentNode.Name ' e.g. "MedicineGroups", "MedicineFamily", etc.
    '        Dim parentID As Integer = 0

    '        ' Try to parse ParentID from Tag
    '        If Integer.TryParse(currentNode.Tag?.ToString(), parentID) = False Then
    '            parentID = 0
    '        End If

    '        ' Call AddSubNode with current node, next level, and parentID
    '        AddSubNodeLazy(currentNode, levelName, parentID)
    '    End If
    'End Sub





#End Region

#End Region


#Region "TreeView"
    Dim NodeName As String
    Dim NodeTag As Integer
    Dim InvId As Integer
    '
    Private Sub TreeViewFill()
        Try
            TV.BeginUpdate()
            TV.Nodes.Clear()
            Dim MainHeader As New TreeNode
            With MainHeader
                If Eng Then
                    .Text = "Medicine Tree"
                Else
                    .Text = "شجرة الادوية"
                End If

                .Tag = 0
                .Name = "Head"
                .ForeColor = Color.Red
                .NodeFont = New Font(Me.TV.Font, FontStyle.Bold)
            End With

            With Me.TV
                .Nodes.Add(MainHeader)
                Call Me.AddSubNodeNew(MainHeader, "MedicineGroups", 0)
                .EndUpdate()
                .Select()
                .Nodes(0).Expand()
                '.ExpandAll()

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function GetNextLevel(currentLevel As String) As String
        Select Case currentLevel
            Case "Head"
                Return "MedicineGroups"
            Case "MedicineGroups"
                Return "MedicineFamily"
            Case "MedicineFamily"
                Return "MedScienceFamily"
            Case "MedScienceFamily"
                Return "MedicineItems"
            Case "MedicineItems"
                Return "MedicineShape"
            Case "MedicineShape"
                Return "MedicineDoze"
            Case "MedicineDoze"
                Return "MedicineDoze"
            Case Else
                Return String.Empty ' No further levels
        End Select
    End Function
    Private Function GetNextLevel2(currentLevel As String) As String
        Select Case currentLevel
            Case "Head"               ' Level 0
                Return "MedicineGroups" ' Level 1
            Case "MedicineGroups"      ' Level 1
                Return "MedicineFamily" ' Level 2
            Case "MedicineFamily"      ' Level 2
                Return "MedScienceFamily" ' Level 3 (THIS IS YOUR MISSING LEVEL)
            Case "MedScienceFamily"    ' Level 3
                Return "MedicineItems"  ' Level 4
            Case "MedicineItems"       ' Level 4
                Return "MedicineShape"  ' Level 5
            Case "MedicineShape"       ' Level 5
                Return "MedicineDoze"   ' Level 6
            Case Else
                Return String.Empty
        End Select
    End Function
    Private Sub TV_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles TV.BeforeExpand
        Dim currentNode As TreeNode = e.Node

        If currentNode.Nodes.Count = 1 AndAlso currentNode.Nodes(0).Text = "Loading..." Then
            currentNode.Nodes.Clear()

            Dim nextLevel As String = GetNextLevel(currentNode.Name)
            Dim parentID As Integer = 0

            Integer.TryParse(currentNode.Tag?.ToString(), parentID)

            If Not String.IsNullOrEmpty(nextLevel) Then
                AddSubNodeNew(currentNode, nextLevel, parentID)
            End If
        End If
    End Sub
    Private Sub AddSubNodeNew1(ByVal Node As TreeNode, ByVal Level As String, ByVal ParentID As Integer)
        Try
            ' Add debug output
            Debug.WriteLine($"Processing level: {Level}, ParentID: {ParentID}")

            Select Case Level
                Case "MedicineGroups"
                    ' Assuming clsMedGrp is List(Of clsMedGrpData)
                    For Each medGrp In clsMedGrp
                        Dim SubNode As New TreeNode With {
                        .Text = medGrp.MedicineFamily,
                        .Tag = medGrp.MedicineID,
                        .Name = "MedicineFamily",
                        .ForeColor = Color.Blue
                    }
                        SubNode.Nodes.Add("Loading...") ' Placeholder node
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedicineFamily"
                    ' Filter clsMedFam by MedicineSubCat = ParentID
                    Dim filteredFam = clsMedFam.Where(Function(x) x.MedicineID = ParentID).ToList()
                    Debug.WriteLine($"Found {filteredFam.Count} MedicineFamily records for MedicineSubCat={ParentID}")

                    For Each medFam In filteredFam
                        Dim SubNode As New TreeNode With {
                        .Text = medFam.MedicineSubCat,
                        .Tag = medFam.SubCatID,  ' CRITICAL: This must match MedScienceFamily's parent key
                        .Name = "MedScienceFamily",
                        .ForeColor = Color.DarkOrange
                    }
                        SubNode.Nodes.Add("Loading...")
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedScienceFamily"  ' Your missing level
                    ' Filter clsMedScFam by SubCatID = ParentID
                    Dim filteredSciFam = clsMedScFam.Where(Function(x) x.SubCatID = ParentID).ToList()
                    Debug.WriteLine($"Found {filteredSciFam.Count} MedScienceFamily records for SubCatID={ParentID}")

                    For Each medSci In filteredSciFam
                        Dim SubNode As New TreeNode With {
                        .Text = medSci.ScienceName,
                        .Tag = medSci.ScincID,  ' This will be used by MedicineItems
                        .Name = "MedicineItems",
                        .ForeColor = Color.Green
                    }
                        SubNode.Nodes.Add("Loading...")
                        Node.Nodes.Add(SubNode)
                    Next
                Case "MedicineItems"
                    ' Filter clsMedItm by ScincID = ParentID
                    Dim filteredItems = clsMedItm.Where(Function(x) x.ScincID = ParentID)
                    Debug.WriteLine($"Found {filteredItems.Count} MedicineItems records for ScincID={ParentID}")
                    For Each medItem In filteredItems
                        Dim SubNode As New TreeNode With {
                        .Text = medItem.CommName,
                        .Tag = medItem.MedicineItemID,
                        .Name = "MedicineShape",
                        .ForeColor = Color.Red
                    }
                        LabelControl1.Text = medItem.Notes
                        SubNode.Nodes.Add("Loading...") ' Placeholder node
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedicineShape"
                    ' Filter clsMedShp by MedicineItemID = ParentID
                    Dim filteredShapes = clsMedShp.Where(Function(x) x.MedicineItemID = ParentID)
                    Debug.WriteLine($"Found {filteredShapes.Count} MedicineShape records for MedicineItemID={ParentID}")
                    For Each medShape In filteredShapes
                        Dim SubNode As New TreeNode With {
                        .Text = medShape.MedicineShape,
                        .Tag = medShape.ShapeID,
                        .Name = "MedicineDoze",
                        .ForeColor = Color.Pink
                    }
                        SubNode.Nodes.Add("Loading...") ' Placeholder node
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedicineDoze"
                    ' Filter clsMedDoz by ShapeID = ParentID
                    Dim filteredDozes = clsMedDoz.Where(Function(x) x.ShapeID = ParentID)
                    Debug.WriteLine($"Found {filteredDozes.Count} MedicineDoze records for ShapeID={ParentID}")
                    For Each medDoze In filteredDozes
                        Dim SubNode As New TreeNode With {
                        .Text = medDoze.Doze,
                        .Tag = medDoze.DozeID,
                        .Name = "MedicineDoze",
                        .ForeColor = Color.Magenta
                    }
                        Node.Nodes.Add(SubNode) ' Final level, no placeholder
                    Next
            End Select
        Catch ex As Exception
            MsgBox($"Error in AddSubNode: {ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub TreeView_NodeExpanded(sender As Object, e As TreeViewEventArgs) Handles TV.AfterExpand

        Dim expandedNode As TreeNode = e.Node

        If expandedNode.Nodes.Count = 1 AndAlso expandedNode.Nodes(0).Text = "Loading..." Then
            expandedNode.Nodes.Clear()

            Dim level As String = expandedNode.Name
            Dim parentId As Integer = CInt(expandedNode.Tag)

            AddSubNodeNew(expandedNode, level, parentId)
        End If
    End Sub

    Private Sub AddSubNodeNew(ByVal Node As TreeNode, ByVal Level As String, ByVal ParentID As Integer)
        Try
            Debug.WriteLine($"Processing level: {Level}, ParentID: {ParentID}")

            Select Case Level
                Case "MedicineGroups"
                    For Each medGrp In clsMedGrp
                        Dim SubNode As New TreeNode With {
                        .Text = medGrp.MedicineFamily,  ' Changed from MedicineFamily to MedicineGroupName
                        .Tag = medGrp.MedicineID,
                        .Name = "MedicineGroups",
                        .ForeColor = Color.Blue
                    }
                        SubNode.Nodes.Add("Loading...")
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedicineFamily"
                    Dim filteredFam = clsMedFam.Where(Function(x) x.MedicineID = ParentID).ToList()
                    Debug.WriteLine($"Found {filteredFam.Count} MedicineFamily records for MedicineSubCat={ParentID}")

                    For Each medFam In filteredFam
                        Dim SubNode As New TreeNode With {
                        .Text = medFam.MedicineSubCat,
                        .Tag = medFam.SubCatID,
                         .Name = "MedicineFamily",
                        .ForeColor = Color.DarkOrange
                    }
                        SubNode.Nodes.Add("Loading...")
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedScienceFamily"
                    Dim filteredSciFam = clsMedScFam.Where(Function(x) x.SubCatID = ParentID).ToList()
                    Debug.WriteLine($"Found {filteredSciFam.Count} MedScienceFamily records for SubCatID={ParentID}")

                    For Each medSci In filteredSciFam
                        Dim SubNode As New TreeNode With {
                        .Text = medSci.ScienceName,
                        .Tag = medSci.ScincID,
                        .Name = "MedScienceFamily",
                        .ForeColor = Color.Green
                    }
                        SubNode.Nodes.Add("Loading...")
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedicineItems"
                    Dim filteredItems = clsMedItm.Where(Function(x) x.ScincID = ParentID).ToList()
                    Debug.WriteLine($"Found {filteredItems.Count} MedicineItems records for ScincID={ParentID}")

                    For Each medItem In filteredItems
                        Dim SubNode As New TreeNode With {
                        .Text = medItem.CommName,
                        .Tag = medItem.MedicineItemID,
                        .Name = "MedicineItems",
                        .ForeColor = Color.Red
                    }
                        LabelControl1.Text = medItem.Notes
                        SubNode.Nodes.Add("Loading...")
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedicineShape"
                    Dim filteredShapes = clsMedShp.Where(Function(x) x.MedicineItemID = ParentID).ToList()
                    Debug.WriteLine($"Found {filteredShapes.Count} MedicineShape records for MedicineItemID={ParentID}")

                    For Each medShape In filteredShapes
                        Dim SubNode As New TreeNode With {
                        .Text = medShape.MedicineShape,
                        .Tag = medShape.ShapeID,
                        .Name = "MedicineShape",
                        .ForeColor = Color.Pink
                    }
                        SubNode.Nodes.Add("Loading...")
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedicineDoze"
                    Dim filteredDozes = clsMedDoz.Where(Function(x) x.ShapeID = ParentID).ToList()
                    Debug.WriteLine($"Found {filteredDozes.Count} MedicineDoze records for ShapeID={ParentID}")

                    For Each medDoze In filteredDozes
                        Dim SubNode As New TreeNode With {
                        .Text = medDoze.Doze,
                        .Tag = medDoze.DozeID,
                        .Name = "MedicineDoze",
                        .ForeColor = Color.Magenta
                    }
                        Node.Nodes.Add(SubNode)
                    Next
            End Select
        Catch ex As Exception
            MsgBox($"Error in AddSubNode: {ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub


#End Region


    Private Sub TV_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TV.AfterSelect

    End Sub
    Private Sub TV_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TV.NodeMouseDoubleClick
        Try
            Dim selectedNode As TreeNode = e.Node
            ' Check if the clicked node is a DozeNode
            If selectedNode.Name = "MedicineDoze" Then
                ' Retrieve texts from the selected node and its parents
                Dim dozeText As String = selectedNode.Text
                Dim shapeText As String = selectedNode.Parent?.Text
                Dim itemText As String = selectedNode.Parent?.Parent?.Text

                ' Ensure parent nodes exist
                If String.IsNullOrEmpty(shapeText) OrElse String.IsNullOrEmpty(itemText) Then
                    MsgBox("Incomplete node hierarchy. Ensure proper data is loaded.", MsgBoxStyle.Exclamation)
                    Return
                End If

                ' Format the output string
                Dim formattedText As String = $"*  {itemText} {shapeText}" & vbCrLf & $"    {dozeText}" & vbCrLf & vbCrLf

                ' Append the formatted text to the Ultra.Text control
                Me.Ultra.Text += formattedText
            End If
        Catch ex As Exception
            MsgBox($"Error: {ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub

    ' Enable dragging from the TreeView
    Private Sub TV_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles TV.ItemDrag
        Dim selectedNode As TreeNode = CType(e.Item, TreeNode)
        If selectedNode.Name = "MedicineDoze" Then
            ' Fetch details from parent nodes
            Dim dozeText As String = selectedNode.Text
            Dim shapeText As String = selectedNode.Parent?.Text
            Dim itemText As String = selectedNode.Parent?.Parent?.Text

            ' Format the drag data
            Dim dragData As String = $"*  {itemText} {shapeText}" & vbCrLf & $"    {dozeText}" & vbCrLf & vbCrLf

            ' Start dragging the formatted text
            DoDragDrop(dragData, DragDropEffects.Copy)
        End If
    End Sub

    ' Handle DragEnter event on the target control
    Private Sub Ultra_DragEnter(sender As Object, e As DragEventArgs) Handles Ultra.DragEnter
        ' Check if the data being dragged is text
        If e.Data.GetDataPresent(DataFormats.Text) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ' Handle DragDrop event on the target control
    Private Sub Ultra_DragDrop(sender As Object, e As DragEventArgs) Handles Ultra.DragDrop
        Dim droppedText As String = CType(e.Data.GetData(DataFormats.Text), String)
        ' Append the dropped data to the target control (e.g., Ultra.Text)
        Me.Ultra.Text += droppedText
    End Sub


End Class


