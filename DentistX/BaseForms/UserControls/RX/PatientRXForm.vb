Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraTreeList

Public Class PatientRXForm

    Private clsMedGrp As IEnumerable(Of MedicineGroups)
    Private clsMedFam As IEnumerable(Of MedicineFamily)
    Private clsMedScFam As IEnumerable(Of MedScienceFamily)
    Private clsMedItm As IEnumerable(Of MedicineItems)
    Private clsMedShp As IEnumerable(Of MedicineShape)
    Private clsMedDoz As IEnumerable(Of MedicineDoze)

    Private clsMedGrpData As MedicineGroupsDATA
    Private clsMedFamData As MedicineFamilyDATA
    Private clsMedScFamData As MedScienceFamilyDATA
    Private clsMedItmData As MedicineItemsDATA
    Private clsMedShpData As MedicineShapeDATA
    Private clsMedDozData As MedicineDozeDATA

    Private clsRx As IEnumerable(Of Patient_RX)
    Private clsRxData As Patient_RXDATA
    Private clsRxBodyData As RxBodyDATA
    Private clsRxBody As IEnumerable(Of RxBody)
    Private clsPatientData As New PatientDATA
    Private clsPatient As Patient
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
            'Patient_RX
            clsRx = clsPatientData.GetPatient_RX(clsPatient)
            If Patient_RXBindingSource Is Nothing Then
                Patient_RXBindingSource = New BindingSource()
            End If
            ' Set the DataSource
            Patient_RXBindingSource.DataSource = clsRx.ToList()
        End If
    End Sub
    Private Sub PatientRX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadClasses()
            LoadRX(PatientID)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        TreeViewFill()

    End Sub

    Public WriteOnly Property ValueFromParent() As Integer
        Set(ByVal Value As Integer)
            LoadRX(Value)
        End Set
    End Property





#Region "Patient RX"

    Private Sub grpDoze_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grpDoze.SelectedIndexChanged

    End Sub

    Private Sub grpDoze_DoubleClick(sender As Object, e As EventArgs) Handles grpDoze.DoubleClick
        If grpDoze.SelectedItem IsNot Nothing Then
            Try
                Dim Comm, Shape, Doze As String

                Dim itemRow As DataRowView = CType(grpItems.SelectedItem, DataRowView)
                Comm = itemRow("CommName").ToString()
                'Comm = Me.grpItems.SelectedValue.ToString
                Dim shapeRow As DataRowView = CType(grpShape.SelectedItem, DataRowView)
                Shape = shapeRow("MedicineShape").ToString()
                'Shape = Me.grpShape.SelectedItem.ToString
                Dim dozeRow As DataRowView = CType(grpDoze.SelectedItem, DataRowView)
                Doze = dozeRow("Doze").ToString()
                'Doze = Me.grpDoze.SelectedItem.ToString

                Me.Ultra.Text += "*  " & Comm & " " & Shape & "  " & vbCrLf & "    " & Doze & vbCrLf & vbCrLf
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub btAddRX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToRX.Click
        Try
            'MedicineItems = Comm
            'MedicineShape = Shape
            'MedicineDoze = Doze
            Dim Comm, Shape, Doze As String
            Dim itemIndex, shapeIndex, dozeIndex As Integer
            itemIndex = Me.grpItems.SelectedIndex
            shapeIndex = Me.grpShape.SelectedIndex
            dozeIndex = Me.grpDoze.SelectedIndex
            Comm = Me.grpItems.GetItemText(itemIndex).ToString
            Shape = Me.grpShape.GetItemText(shapeIndex).ToString
            Doze = Me.grpDoze.GetItemText(dozeIndex).ToString

            Me.Ultra.Text += "*  " & Comm & " " & Shape & "  " & vbCrLf & "    " & Doze & vbCrLf & vbCrLf
        Catch ex As Exception
            MsgBox(ex.Message)
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
        'Dim RxRow As DsRx.PatientRXVIEWRow
        'RxRow = CType(CType(Me.PatientRXVIEWBindingSource.Current, DataRowView).Row, DsRx.PatientRXVIEWRow)
        'RxID = RxRow.RXID

    End Sub



    Private Sub btPrintFullRX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPrintFullRX.Click


        '=================================

        If Me.Ultra.Text = "" OrElse RxID = -1 Then
            MsgBox(If(Eng, "You Cant Print Empty RX", "لا يمكن طباعة وصفة فارغة...."))
            Exit Sub
        End If

        Try
            Dim wimg As Image = Nothing
            Dim s As String = ""
            Dim useimg, usetxt As Boolean

            Dim row As RxBody = clsRxBody?.FirstOrDefault()
            If row IsNot Nothing Then
                useimg = row.UseWtrImg
                usetxt = row.UseWtrText

                If row.WtrImg IsNot Nothing Then
                    Using ms As New IO.MemoryStream(row.WtrImg)
                        wimg = Image.FromStream(ms)
                    End Using

                    ' Save image to file
                    Dim picPath = IO.Path.Combine(Application.StartupPath, "Images", "Wtr.jpg")
                    Dim dirPath = IO.Path.GetDirectoryName(picPath)
                    If Not IO.Directory.Exists(dirPath) Then IO.Directory.CreateDirectory(dirPath)
                    If IO.File.Exists(picPath) Then IO.File.Delete(picPath)
                    wimg.Save(picPath, Imaging.ImageFormat.Jpeg)
                End If

                If Not String.IsNullOrEmpty(row.WtrText) Then
                    s = row.WtrText
                End If

                Dim x As New MainRX(PatientID, RxID)
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

                x.Watermark.CopyFrom(pictureWatermark)

                Dim printTool As New ReportPrintTool(x)
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
            Dim x As New RxPrint(PatientID, RxID)
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(x)
            printTool.ShowPreviewDialog()
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
        RxDetailFrm.ShowDialog()

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

    Private Sub AddSubNodeNew(ByVal Node As TreeNode, ByVal Level As String, ByVal ParentID As Integer)
        Try
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
                    Dim filteredFam = clsMedFam.Where(Function(x) x.MedicineID = ParentID)
                    For Each medFam In filteredFam
                        Dim SubNode As New TreeNode With {
                        .Text = medFam.MedicineSubCat,
                        .Tag = medFam.SubCatID,
                        .Name = "MedScienceFamily",
                        .ForeColor = Color.DarkOrange
                    }
                        SubNode.Nodes.Add("Loading...") ' Placeholder node
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedScienceFamily"
                    ' Filter clsMedScFam by SubCatID = ParentID
                    Dim filteredSciFam = clsMedScFam.Where(Function(x) x.SubCatID = ParentID)
                    For Each medSci In filteredSciFam
                        Dim SubNode As New TreeNode With {
                        .Text = medSci.ScienceName,
                        .Tag = medSci.ScincID,
                        .Name = "MedicineItems",
                        .ForeColor = Color.Green
                    }
                        SubNode.Nodes.Add("Loading...") ' Placeholder node
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedicineItems"
                    ' Filter clsMedItm by ScincID = ParentID
                    Dim filteredItems = clsMedItm.Where(Function(x) x.ScincID = ParentID)
                    For Each medItem In filteredItems
                        Dim SubNode As New TreeNode With {
                        .Text = medItem.CommName,
                        .Tag = medItem.MedicineItemID,
                        .Name = "MedicineShape",
                        .ForeColor = Color.Red
                    }
                        SubNode.Nodes.Add("Loading...") ' Placeholder node
                        Node.Nodes.Add(SubNode)
                    Next

                Case "MedicineShape"
                    ' Filter clsMedShp by MedicineItemID = ParentID
                    Dim filteredShapes = clsMedShp.Where(Function(x) x.MedicineItemID = ParentID)
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
            Case Else
                Return String.Empty ' No further levels
        End Select
    End Function



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
                Call Me.AddSubNode(MainHeader, "MedicineGroups", 0)
                .EndUpdate()
                .Select()
                .Nodes(0).Expand()
                '.ExpandAll()

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub AddSubNode(ByVal Node As TreeNode, ByVal Level As String, ByVal ParentID As Integer)
        'Try
        '    Select Case Level
        '        Case "MedicineGroups"
        '            For Each DrLine As DataRow In Me.DsRx.MedicineGroups.Rows
        '                Dim SubNode As New TreeNode With {
        '                .Text = DrLine("MedicineFamily").ToString(),
        '                .Tag = DrLine("MedicineSubCat").ToString(),
        '                .Name = "MedicineFamily",
        '                .ForeColor = Color.Blue
        '            }
        '                SubNode.Nodes.Add("Loading...") ' Placeholder node
        '                Node.Nodes.Add(SubNode)
        '            Next

        '        Case "MedicineFamily"
        '            Dim FilteredRows = Me.DsRx.MedicineFamily.AsEnumerable().Where(Function(r) r.Field(Of Integer)("MedicineSubCat") = ParentID)
        '            For Each DrSubCat As DataRow In FilteredRows
        '                Dim SubNode As New TreeNode With {
        '                .Text = DrSubCat("MedicineSubCat").ToString(),
        '                .Tag = DrSubCat("SubCatID").ToString(),
        '                .Name = "MedScienceFamily", ' Next level's name
        '                .ForeColor = Color.DarkOrange
        '            }
        '                SubNode.Nodes.Add("Loading...") ' Placeholder node
        '                Node.Nodes.Add(SubNode)
        '            Next

        '        Case "MedScienceFamily"
        '            Dim FilteredRows = Me.DsRx.MedScienceFamily.AsEnumerable().Where(Function(r) r.Field(Of Integer)("SubCatID") = ParentID)
        '            For Each DrScience As DataRow In FilteredRows
        '                Dim SubNode As New TreeNode With {
        '                .Text = DrScience("ScienceName").ToString(),
        '                .Tag = DrScience("ScincID").ToString(),
        '                .Name = "MedicineItems", ' Next level's name
        '                .ForeColor = Color.Green
        '            }
        '                SubNode.Nodes.Add("Loading...") ' Placeholder node
        '                Node.Nodes.Add(SubNode)
        '            Next

        '        Case "MedicineItems"
        '            Dim FilteredRows = Me.DsRx.MedicineItems.AsEnumerable().Where(Function(r) r.Field(Of Integer)("ScincID") = ParentID)
        '            For Each DrItem As DataRow In FilteredRows
        '                Dim SubNode As New TreeNode With {
        '                .Text = DrItem("CommName").ToString(),
        '                .Tag = DrItem("MedicineItemID").ToString(),
        '                .Name = "MedicineShape", ' Next level's name
        '                .ForeColor = Color.Red
        '            }
        '                SubNode.Nodes.Add("Loading...") ' Placeholder node
        '                Node.Nodes.Add(SubNode)
        '            Next

        '        Case "MedicineShape"
        '            Dim FilteredRows = Me.DsRx.MedicineShape.AsEnumerable().Where(Function(r) r.Field(Of Integer)("MedicineItemID") = ParentID)
        '            For Each DrShape As DataRow In FilteredRows
        '                Dim SubNode As New TreeNode With {
        '                .Text = DrShape("MedicineShape").ToString(),
        '                .Tag = DrShape("ShapeID").ToString(),
        '                .Name = "MedicineDoze", ' Next level's name
        '                .ForeColor = Color.Pink
        '            }
        '                SubNode.Nodes.Add("Loading...") ' Placeholder node
        '                Node.Nodes.Add(SubNode)
        '            Next

        '        Case "MedicineDoze"
        '            Dim FilteredRows = Me.DsRx.MedicineDoze.AsEnumerable().Where(Function(r) r.Field(Of Integer)("ShapeID") = ParentID)
        '            For Each DrDoze As DataRow In FilteredRows
        '                Dim SubNode As New TreeNode With {
        '                .Text = DrDoze("Doze").ToString(),
        '                .Tag = DrDoze("DozeID").ToString(),
        '                .Name = "MedicineDoze", ' No further levels
        '                .ForeColor = Color.Magenta
        '            }
        '                Node.Nodes.Add(SubNode) ' No placeholder for final level
        '            Next
        '    End Select
        'Catch ex As Exception
        '    MsgBox($"Error in AddSubNode: {ex.Message}", MsgBoxStyle.Critical)
        'End Try
    End Sub


    Private Sub TreeView_NodeExpanded(sender As Object, e As TreeViewEventArgs) Handles TV.AfterExpand

        Dim expandedNode As TreeNode = e.Node

        If expandedNode.Nodes.Count = 1 AndAlso expandedNode.Nodes(0).Text = "Loading..." Then
            expandedNode.Nodes.Clear()

            Dim level As String = expandedNode.Name
            Dim parentId As Integer = CInt(expandedNode.Tag)

            AddSubNode(expandedNode, level, parentId)
        End If
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
#End Region

End Class


'If Not My.Application.OpenForms("printForm") Is Nothing Then
'    IsPatient = True
'    My.Application.OpenForms.Item("printForm").BringToFront()

'Else
'Try
'    Dim print As New printForm
'    'print.MdiParent = DentistX.MainViewCopy
'    IsPatient = True
'    print.StartPosition = FormStartPosition.CenterParent
'    print.ShowDialog(MainViewCopy)

'Catch ex As Exception
'    MsgBox(ex.Message)
'End Try

'End If


'If s.Length > 0 Then
'    Dim textWatermark As New Watermark()
'    textWatermark.Text = s
'    textWatermark.TextDirection = DirectionMode.ForwardDiagonal
'    'textWatermark.Font = New DXFont(textWatermark.Font.Name, 40)
'    textWatermark.ForeColor = Color.DodgerBlue
'    textWatermark.TextTransparency = 50
'    textWatermark.ShowBehind = False
'    'textWatermark.PageRange = "1,3-5"
'    x.Watermark.CopyFrom(textWatermark)
'End If

'Dim selectedNode As TreeNode = CType(e.Item, TreeNode)
'' Only allow dragging from the last node (DozeNode)
'If selectedNode.Name = "MedicineDoze" Then
'    ' Start dragging the Text property of the node
'    DoDragDrop(selectedNode.Text, DragDropEffects.Copy)
'End If
''الإجرائية الفرعية لتعبئة شجرة المواد بالتصنيفات ومن ثم بالمواد
'Private Sub AddSubNode(ByVal Node As TreeNode, ByVal Level As String, ByVal ParentID As Integer)
'    Try


'        Select Case Level
'            Case "MedicineGroups"
'                ''إضافة MedicineGroups
'                'Dim DsMedicineGroups As New DataSet
'                'Dim SqlMedicineGroups As String = "SELECT * FROM [dbo].[MedicineGroups] ORDER BY [MedicineFamily]"
'                'Me_DSFillText(DsMedicineGroups, SqlMedicineGroups, "MedicineGroups")

'                For Each DrLine As DataRow In Me.DsRx.MedicineGroups.Rows
'                    Dim SubNode As New TreeNode
'                    With SubNode
'                        .Text = DrLine("MedicineFamily").ToString()
'                        .Tag = DrLine("MedicineSubCat").ToString()
'                        .Name = "MedicineFamily"
'                        .ForeColor = Color.Blue
'                    End With
'                    Node.Nodes.Add(SubNode)
'                    ' Recursively add MedicineFamily nodes
'                    AddSubNode(SubNode, "MedicineFamily", CInt(SubNode.Tag))
'                Next

'            Case "MedicineFamily"
'                ''إضافة MedicineFamily
'                'Dim DsMedicineFamily As New DataSet
'                'Dim SqlMedicineFamily As String = "SELECT * FROM [dbo].[MedicineFamily] WHERE MedicineSubCat=" & ParentID & " ORDER BY [MedicineSubCat]"
'                'Me_DSFillText(DsMedicineFamily, SqlMedicineFamily, "MedicineFamily")

'                For Each DrSubCat As DataRow In Me.DsRx.MedicineFamily.Rows
'                    Dim SubSubNode As New TreeNode
'                    With SubSubNode
'                        .Text = DrSubCat("MedicineSubCat").ToString()
'                        .Tag = DrSubCat("SubCatID").ToString()
'                        .Name = "MedicineSubCat"
'                        .ForeColor = Color.DarkOrange
'                    End With
'                    Node.Nodes.Add(SubSubNode)
'                    ' Recursively add MedScienceFamily nodes
'                    AddSubNode(SubSubNode, "MedScienceFamily", CInt(SubSubNode.Tag))
'                Next

'            Case "MedScienceFamily"
'                ''إضافة MedScienceFamily
'                'Dim DsMedScienceFamily As New DataSet
'                'Dim SqlMedScienceFamily As String = "SELECT * FROM [dbo].[MedScienceFamily] WHERE SubCatID=" & ParentID & " ORDER BY [ScienceName]"
'                'Me_DSFillText(DsMedScienceFamily, SqlMedScienceFamily, "MedScienceFamily")

'                For Each DrScience As DataRow In Me.DsRx.MedScienceFamily.Rows
'                    Dim ScienceNode As New TreeNode
'                    With ScienceNode
'                        .Text = DrScience("ScienceName").ToString()
'                        .Tag = DrScience("ScincID").ToString()
'                        .Name = "ScienceName"
'                        .ForeColor = Color.Green
'                    End With
'                    Node.Nodes.Add(ScienceNode)
'                    ' Recursively add MedicineItems nodes
'                    AddSubNode(ScienceNode, "MedicineItems", CInt(ScienceNode.Tag))
'                Next

'            Case "MedicineItems"
'                ''إضافة MedicineItems
'                'Dim DsMedicineItems As New DataSet
'                'Dim SqlMedicineItems As String = "SELECT * FROM [dbo].[MedicineItems] WHERE ScincID=" & ParentID & " ORDER BY [CommName]"
'                'Me_DSFillText(DsMedicineItems, SqlMedicineItems, "MedicineItems")

'                For Each DrItem As DataRow In Me.DsRx.MedicineItems.Rows
'                    Dim ItemNode As New TreeNode
'                    With ItemNode
'                        .Text = DrItem("CommName").ToString()
'                        .Tag = DrItem("MedicineItemID").ToString()
'                        .Name = "MedicineItem"
'                        .ForeColor = Color.Red
'                    End With
'                    Node.Nodes.Add(ItemNode)
'                    ' Recursively add MedicineShape nodes
'                    AddSubNode(ItemNode, "MedicineShape", CInt(ItemNode.Tag))
'                Next

'            Case "MedicineShape"
'                ''إضافة MedicineShape
'                'Dim DsMedicineShape As New DataSet
'                'Dim SqlMedicineShape As String = "SELECT * FROM [dbo].[MedicineShape] WHERE MedicineItemID=" & ParentID & " ORDER BY [MedicineShape]"
'                'Me_DSFillText(DsMedicineShape, SqlMedicineShape, "MedicineShape")

'                For Each DrShape As DataRow In Me.DsRx.MedicineShape.Rows
'                    Dim ShapeNode As New TreeNode
'                    With ShapeNode
'                        .Text = DrShape("MedicineShape").ToString()
'                        .Tag = DrShape("ShapeID").ToString()
'                        .Name = "MedicineShape"
'                        .ForeColor = Color.Pink
'                    End With
'                    Node.Nodes.Add(ShapeNode)
'                    ' Recursively add MedicineDoze nodes
'                    AddSubNode(ShapeNode, "MedicineDoze", CInt(ShapeNode.Tag))
'                Next

'            Case "MedicineDoze"
'                ''إضافة MedicineDoze
'                'Dim DsMedicineDoze As New DataSet
'                'Dim SqlMedicineDoze As String = "SELECT * FROM [dbo].[MedicineDoze] WHERE ShapeID=" & ParentID & " ORDER BY [Doze]"
'                'Me_DSFillText(DsMedicineDoze, SqlMedicineDoze, "MedicineDoze")

'                For Each DrDoze As DataRow In Me.DsRx.MedicineDoze.Rows
'                    Dim DozeNode As New TreeNode
'                    With DozeNode
'                        .Text = DrDoze("Doze").ToString()
'                        .Tag = DrDoze("DozeID").ToString()
'                        .Name = "MedicineDoze"
'                        .ForeColor = Color.Magenta
'                    End With
'                    Node.Nodes.Add(DozeNode)
'                Next
'        End Select
'    Catch ex As Exception
'        MsgBox(ex.Message)
'    End Try
'End Sub

'======================


'Dim print As New printForm

'IsPatient = True
'print.Activate()
'print.RxFlyViewer.PrintDialog()
''print.PrintDocument1.Print()
'===============================

' Private Sub BalanceGroup_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles  _BalanceGroup.MouseDown
'dragging = True
'beginX = e.X
'beginY = e.Y
'Me.UlPopup.PopupControl = Nothing
'End Sub

' Private Sub BalanceGroup_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles  _BalanceGroup.MouseMove
'If dragging = True Then
'    BalanceGroup.Location = New Point(BalanceGroup.Location.X + e.X - beginX, BalanceGroup.Location.Y + e.Y - beginY)
'    'Me.UlPopup.Show(New Point(BalanceGroup.Location.X + e.X - beginX, BalanceGroup.Location.Y + e.Y - beginY))
'    'Me.BalanceGroup.Location = (New Point(Control.MousePosition.X, Control.MousePosition.Y)) '340, 60))

'    Me.Refresh()
'End If
'End Sub


'#Region "OldTreeView"
'    Dim NodeName As String
'    Dim NodeTag As Integer
'    Dim InvId As Integer

'    'الإجرائية الاساسية لتعبئة شجرة المواد
'    Private Sub TreeViewFill()
'        Try
'            Tv.BeginUpdate()
'            Tv.Nodes.Clear()
'            Dim MainHeader As New TreeNode
'            With MainHeader
'                If Eng Then
'                    .Text = "Products Tree"
'                Else
'                    .Text = "شجرة المواد"
'                End If

'                .Tag = 0
'                .Name = "Head"
'                .ForeColor = Color.Red
'                .NodeFont = New Font(Me.Tv.Font, FontStyle.Bold)
'            End With

'            With Me.Tv
'                .Nodes.Add(MainHeader)
'                Call Me.AddSubNode(MainHeader, 0)
'                .EndUpdate()
'                .ExpandAll()
'                .Select()
'            End With
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'    End Sub

'    'الإجرائية الفرعية لتعبئة شجرة المواد بالتصنيفات ومن ثم بالمواد
'    Private Sub AddSubNode(ByVal Node As TreeNode, ByVal ParentID As Integer)
'        Try
'            'إضافة الأصناف الرئيسية
'            Dim DsMedicineGroups As New DataSet
'            Dim SqlMedicineGroups As String = "SELECT * FROM [dbo].[MedicineGroups] WHERE MedicineSubCat=" & ParentID & " ORDER BY [MedicineFamily]"
'            MyMod.Me_DSFillText(DsMedicineGroups, SqlMedicineGroups, "MedicineGroups")

'            If DsMedicineGroups.Tables("MedicineGroups").Rows.Count = 0 Then Exit Sub

'            For Each DrLine As DataRow In DsMedicineGroups.Tables("MedicineGroups").Rows
'                Dim SubNode As New TreeNode
'                With SubNode
'                    .Text = DrLine("MedicineFamily").ToString()
'                    .Tag = DrLine("MedicineSubCat").ToString()
'                    .Name = "MedicineFamily"
'                    .ForeColor = Color.Blue
'                End With
'                Node.Nodes.Add(SubNode)

'                'Adding MedicineFamily nodes
'                Dim DsMedicineFamily As New DataSet
'                Dim SqlMedicineFamily As String = "SELECT * FROM [dbo].[MedicineFamily] WHERE MedicineSubCat=" & SubNode.Tag & " ORDER BY [MedicineSubCat]"
'                MyMod.Me_DSFillText(DsMedicineFamily, SqlMedicineFamily, "MedicineFamily")

'                If DsMedicineFamily.Tables("MedicineFamily").Rows.Count > 0 Then
'                    For Each DrSubCat As DataRow In DsMedicineFamily.Tables("MedicineFamily").Rows
'                        Dim SubSubNode As New TreeNode
'                        With SubSubNode
'                            .Text = DrSubCat("MedicineSubCat").ToString()
'                            .Tag = DrSubCat("SubCatID").ToString()
'                            .Name = "MedicineSubCat"
'                            .ForeColor = Color.DarkOrange
'                        End With
'                        SubNode.Nodes.Add(SubSubNode)

'                        'Adding MedScienceFamily nodes
'                        Dim DsMedScienceFamily As New DataSet
'                        Dim SqlMedScienceFamily As String = "SELECT * FROM [dbo].[MedScienceFamily] WHERE SubCatID=" & SubSubNode.Tag & " ORDER BY [ScienceName]"
'                        MyMod.Me_DSFillText(DsMedScienceFamily, SqlMedScienceFamily, "MedScienceFamily")

'                        If DsMedScienceFamily.Tables("MedScienceFamily").Rows.Count > 0 Then
'                            For Each DrScience As DataRow In DsMedScienceFamily.Tables("MedScienceFamily").Rows
'                                Dim ScienceNode As New TreeNode
'                                With ScienceNode
'                                    .Text = DrScience("ScienceName").ToString()
'                                    .Tag = DrScience("ScincID").ToString()
'                                    .Name = "ScienceName"
'                                    .ForeColor = Color.Green
'                                End With
'                                SubSubNode.Nodes.Add(ScienceNode)

'                                'Adding MedicineItems nodes
'                                Dim DsMedicineItems As New DataSet
'                                Dim SqlMedicineItems As String = "SELECT * FROM [dbo].[MedicineItems] WHERE ScincID=" & ScienceNode.Tag & " ORDER BY [CommName]"
'                                MyMod.Me_DSFillText(DsMedicineItems, SqlMedicineItems, "MedicineItems")

'                                If DsMedicineItems.Tables("MedicineItems").Rows.Count > 0 Then
'                                    For Each DrItem As DataRow In DsMedicineItems.Tables("MedicineItems").Rows
'                                        Dim ItemNode As New TreeNode
'                                        With ItemNode
'                                            .Text = DrItem("CommName").ToString()
'                                            .Tag = DrItem("MedicineItemID").ToString()
'                                            .Name = "MedicineItem"
'                                            .ForeColor = Color.Yellow
'                                        End With
'                                        ScienceNode.Nodes.Add(ItemNode)

'                                        'Adding MedicineShape nodes
'                                        Dim DsMedicineShape As New DataSet
'                                        Dim SqlMedicineShape As String = "SELECT * FROM [dbo].[MedicineShape] WHERE MedicineItemID=" & ItemNode.Tag & " ORDER BY [MedicineShape]"
'                                        MyMod.Me_DSFillText(DsMedicineShape, SqlMedicineShape, "MedicineShape")

'                                        If DsMedicineShape.Tables("MedicineShape").Rows.Count > 0 Then
'                                            For Each DrShape As DataRow In DsMedicineShape.Tables("MedicineShape").Rows
'                                                Dim ShapeNode As New TreeNode
'                                                With ShapeNode
'                                                    .Text = DrShape("MedicineShape").ToString()
'                                                    .Tag = DrShape("ShapeID").ToString()
'                                                    .Name = "MedicineShape"
'                                                    .ForeColor = Color.Cyan
'                                                End With
'                                                ItemNode.Nodes.Add(ShapeNode)

'                                                'Adding MedicineDoze nodes
'                                                Dim DsMedicineDoze As New DataSet
'                                                Dim SqlMedicineDoze As String = "SELECT * FROM [dbo].[MedicineDoze] WHERE ShapeID=" & ShapeNode.Tag & " ORDER BY [Doze]"
'                                                MyMod.Me_DSFillText(DsMedicineDoze, SqlMedicineDoze, "MedicineDoze")

'                                                If DsMedicineDoze.Tables("MedicineDoze").Rows.Count > 0 Then
'                                                    For Each DrDoze As DataRow In DsMedicineDoze.Tables("MedicineDoze").Rows
'                                                        Dim DozeNode As New TreeNode
'                                                        With DozeNode
'                                                            .Text = DrDoze("Doze").ToString()
'                                                            .Tag = DrDoze("DozeID").ToString()
'                                                            .Name = "MedicineDoze"
'                                                            .ForeColor = Color.Magenta
'                                                        End With
'                                                        ShapeNode.Nodes.Add(DozeNode)
'                                                    Next
'                                                End If
'                                            Next
'                                        End If
'                                    Next
'                                End If
'                            Next
'                        End If
'                    Next
'                End If

'                ' Recursive call to add subnodes
'                AddSubNode(SubNode, CInt(SubNode.Tag))
'            Next
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'    End Sub

'#End Region







'#Region "TreeList"

'    Private Sub SetupMedTreeListNodes()
'        ' Clear existing nodes
'        MedTreeList.ClearNodes()

'        ' Add columns for each node type
'        MedTreeList.Columns.Add(New DevExpress.XtraTreeList.Columns.TreeListColumn() With {.Caption = "Medicine Family", .FieldName = "MedicineFamily", .Visible = True})
'        MedTreeList.Columns.Add(New DevExpress.XtraTreeList.Columns.TreeListColumn() With {.Caption = "Sub Category", .FieldName = "MedicineSubCat", .Visible = True})
'        MedTreeList.Columns.Add(New DevExpress.XtraTreeList.Columns.TreeListColumn() With {.Caption = "Science Name", .FieldName = "ScienceName", .Visible = True})
'        MedTreeList.Columns.Add(New DevExpress.XtraTreeList.Columns.TreeListColumn() With {.Caption = "Commercial Name", .FieldName = "CommName", .Visible = True})
'        MedTreeList.Columns.Add(New DevExpress.XtraTreeList.Columns.TreeListColumn() With {.Caption = "Company", .FieldName = "Company", .Visible = True})
'        MedTreeList.Columns.Add(New DevExpress.XtraTreeList.Columns.TreeListColumn() With {.Caption = "Notes", .FieldName = "Notes", .Visible = True})
'        MedTreeList.Columns.Add(New DevExpress.XtraTreeList.Columns.TreeListColumn() With {.Caption = "Shape", .FieldName = "MedicineShape", .Visible = True})
'        MedTreeList.Columns.Add(New DevExpress.XtraTreeList.Columns.TreeListColumn() With {.Caption = "Shape Info", .FieldName = "ShapeInfo", .Visible = True})
'        MedTreeList.Columns.Add(New DevExpress.XtraTreeList.Columns.TreeListColumn() With {.Caption = "Doze", .FieldName = "Doze", .Visible = True})

'        ' Set up the TreeList control to work in unbound mode
'        MedTreeList.DataSource = Nothing
'        AddHandler MedTreeList.VirtualTreeGetChildNodes, AddressOf OnMedTreeVirtualTreeGetChildNodes
'        AddHandler MedTreeList.VirtualTreeGetCellValue, AddressOf OnMedTreeVirtualTreeGetCellValue
'    End Sub

'    ' Event to provide child nodes
'    Private Sub OnMedTreeVirtualTreeGetChildNodes(ByVal sender As Object, ByVal e As VirtualTreeGetChildNodesInfo)
'        If e.Node Is Nothing Then
'            ' Root level: MedicineGroupsBindingSource
'            e.Children = MedicineGroupsBindingSource.List
'        ElseIf TypeOf e.Node Is DataRowView Then
'            Dim row As DataRowView = CType(e.Node, DataRowView)

'            ' Provide child nodes based on the level of the current node
'            If MedicineGroupsBindingSource.List.Contains(row) Then
'                ' MedicineFamilyBindingSource children where MedicineSubCat matches
'                MedicineFamilyBindingSource.Filter = $"MedicineSubCat = {row("MedicineSubCat")}"
'                e.Children = MedicineFamilyBindingSource.List
'            ElseIf MedicineFamilyBindingSource.List.Contains(row) Then
'                ' MedScienceFamilyBindingSource children where SubCatID matches
'                MedScienceFamilyBindingSource.Filter = $"SubCatID = {row("SubCatID")}"
'                e.Children = MedScienceFamilyBindingSource.List
'            ElseIf MedScienceFamilyBindingSource.List.Contains(row) Then
'                ' MedicineItemsBindingSource children where ScincID matches
'                MedicineItemsBindingSource.Filter = $"ScincID = {row("ScincID")}"
'                e.Children = MedicineItemsBindingSource.List
'            ElseIf MedicineItemsBindingSource.List.Contains(row) Then
'                ' MedicineShapeBindingSource children where MedicineItemID matches
'                MedicineShapeBindingSource.Filter = $"MedicineItemID = {row("MedicineItemID")}"
'                e.Children = MedicineShapeBindingSource.List
'            ElseIf MedicineShapeBindingSource.List.Contains(row) Then
'                ' MedicineDozeBindingSource children where ShapeID matches
'                MedicineDozeBindingSource.Filter = $"ShapeID = {row("ShapeID")}"
'                e.Children = MedicineDozeBindingSource.List
'            End If
'        End If
'    End Sub

'    ' Event to provide the cell value for each node
'    Private Sub OnMedTreeVirtualTreeGetCellValue(ByVal sender As Object, ByVal e As VirtualTreeGetCellValueInfo)
'        Dim row As DataRowView = CType(e.Node, DataRowView)
'        If e.Node IsNot Nothing Then
'            e.CellData = row(e.Column.FieldName)
'        End If
'    End Sub



'#End Region

