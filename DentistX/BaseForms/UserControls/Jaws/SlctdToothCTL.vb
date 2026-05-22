Imports System.Collections.Concurrent
Imports DevExpress.XtraEditors
Imports System.Text.RegularExpressions
Imports DevExpress.XtraTab

Public Class SlctdToothCTL


    'Dim TrtIdsList As New List(Of String)

    Public Sub New() '1214, 654

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        StoreOriginalBounds(Me)
    End Sub

    'NuGet\Install-Package Svg -Version 3.4.7



    Dim clsPatientData As New PatientDATA
    Dim clsPatient As Patient
    Private ToothTrts As IEnumerable(Of Patient_ToothTrt)
    Private PatientTreats As IEnumerable(Of Patient_ToothTrt)
    Private clsToothTrtData As New Patient_ToothTrtDATA    ' --- Legacy Patient_Mobile / Patient_MobileDATA: mobile chart rows were stored in dbo.Patient_Mobile. ---
    ' --- After DB migration (Mobile -> Patient_ToothTrt, LVL=9), the "Mobile" UI path below uses Patient_ToothTrt only. ---
    ' --- Fields & DAL kept (not removed) for reference / rollback; call sites for Source="Mobile" now use clsToothTrtData.GetPatientToothMobTreats. ---
    Private ToothMobs As IEnumerable(Of Patient_Mobile)
    Private PatientMobiles As IEnumerable(Of Patient_Mobile)
    Private clsMobData As New Patient_MobileDATA

    Public Property PatientID As Integer
    Public Property PatientName As String

    ' Define a simple DTO (Data Transfer Object)
    Public Class TreatmentSummary
        Public Property ToothTrtID As Integer
        Public Property PatientID As Integer
        Public Property Tooth As String
        'Public Property ToothNum As Byte?
        'Public Property ToothName As String
        Public Property TreatDate As Date
        Public Property Treat As String
        'Public Property TreatPlan As String
        Public Property TreatDetails As String
        Public Property TreatNotes As String
        Public Property TreatEndDate As Date?
    End Class

    ''' <param name="isMobileChart">True when rendering dbo.Patient_ToothTrt rows with LVL=9 (former Patient_Mobile); forwards to TreatHelper.ProcessToothTreatments mobile rules.</param>
    Public Sub LoadTeethTreats(patientTreats As IEnumerable(Of Patient_ToothTrt), Optional isMobileChart As Boolean = False)
        Dim sw = StartTimer()
        PanelSvgs.SuspendLayout()
        Me.Visible = False

        Dim displayTreats = TrtSourceHelper.ExpandWholeMouthTreatsForJawDisplay(PanelSvgs, patientTreats)
        For Each ct As Control In Me.PanelSvgs.Controls
            If TypeOf ct Is SvgImageBox Then
                Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
                TreatHelper.ProcessToothTreatments(svg, svgExternalList, displayTreats, isMobileChart)
            End If
        Next
        SlctdToothTab.SelectedTabPage = ToothTrtsPage ' Ensure tooth treatments tab is selected when loading treatments
        PanelSvgs.ResumeLayout()
        Me.Visible = True
        LogTime(NameOf(LoadTeethTreats), Me.Name, sw)
    End Sub

    Public Sub LoadSnglToothTreats(patientTreats As IEnumerable(Of Patient_ToothTrt), Optional isMobileChart As Boolean = False)
        Dim sw = StartTimer()
        ToothShapePage.SuspendLayout()
        Me.Visible = False

        Dim displayTreats = TrtSourceHelper.ExpandWholeMouthTreatsForJawDisplay(ToothShapePage, patientTreats)
        For Each ct As Control In Me.ToothShapePage.Controls
            If TypeOf ct Is SvgImageBox Then
                Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
                TreatHelper.ProcessToothTreatments(svg, svgExternalList, displayTreats, isMobileChart)
            End If
        Next

        ToothShapePage.ResumeLayout()
        Me.Visible = True
        LogTime(NameOf(LoadTeethTreats), Me.Name, sw)
    End Sub

    Private svgSelectedList As New List(Of SvgImageBox)
    Private svgExternalList As New List(Of SvgImageBox)
    Public Sub LoadTreat(patientId As Integer, toothNum As Byte)
        Me.PatientID = patientId
        ' Load patient data
        clsPatient = New Patient With {.PatientID = patientId}
        clsPatient = clsPatientData.Select_Record(clsPatient)

        If clsPatient IsNot Nothing Then
            PatientName = clsPatient.PatientName
            Select Case Source
                Case "Treat"
                    PatientTreats = clsToothTrtData.GetPatientToothTreats(patientId, toothNum)
                    If PatientTreats IsNot Nothing Then
                        ' Initialize BindingSource if needed
                        If TrtBS Is Nothing Then
                            TrtBS = New BindingSource()
                        End If

                        TrtBS.DataSource = PatientTreats.Select(Function(t) New TreatmentSummary With {.ToothTrtID = t.ToothTrtID,
                                                                                                       .PatientID = patientId,
                                                                                                       .Tooth = FormatStoredToothNameForDisplay(t.ToothName) & " - " & t.ToothNum.ToString(),
                                                                                                       .TreatDate = t.TreatDate,
                                                                                                       .Treat = t.Treat,
                                                                                                       .TreatDetails = t.TreatDetails,
                                                                                                       .TreatNotes = t.TreatNotes,
                                                                                                       .TreatEndDate = If(t.TreatEndDate.HasValue, t.TreatEndDate.Value, Date.MinValue)
                                                                                                       }).ToList()

                    End If
                Case "Mobile"
                    ' --- Legacy: read dbo.Patient_Mobile via clsMobData.GetPatientToothMobiles + list keyed by MobID. ---
                    'PatientMobiles = clsMobData.GetPatientToothMobiles(patientId, toothNum)
                    'If PatientMobiles IsNot Nothing Then
                    '    If TrtBS Is Nothing Then
                    '        TrtBS = New BindingSource()
                    '    End If
                    '    TrtBS.DataSource = PatientMobiles.Select(Function(t) New TreatmentSummary With {.ToothTrtID = t.MobID,
                    '                                                                                   .PatientID = patientId,
                    '                                                                                   .Tooth = FormatStoredToothNameForDisplay(t.ToothName) & " - " & t.ToothNum.ToString(),
                    '                                                                                   .TreatDate = t.TreatDate,
                    '                                                                                   .Treat = t.Treat,
                    '                                                                                   .TreatNotes = t.TreatNotes,
                    '                                                                                   .TreatEndDate = If(t.TreatEndDate.HasValue, t.TreatEndDate.Value, Date.MinValue)
                    '                                                                                   }).ToList()
                    'End If
                    ' --- Migration experiment: mobile chart data in dbo.Patient_ToothTrt (LVL=9), same as AdultJaw GetPatientTeethMobTreats. ---
                    PatientTreats = clsToothTrtData.GetPatientToothMobTreats(patientId, toothNum)
                    If PatientTreats IsNot Nothing Then
                        If TrtBS Is Nothing Then
                            TrtBS = New BindingSource()
                        End If
                        TrtBS.DataSource = PatientTreats.Select(Function(t) New TreatmentSummary With {.ToothTrtID = t.ToothTrtID,
                                                                                                    .PatientID = patientId,
                                                                                                    .Tooth = FormatStoredToothNameForDisplay(t.ToothName) & " - " & t.ToothNum.ToString(),
                                                                                                    .TreatDate = t.TreatDate,
                                                                                                    .Treat = t.Treat,
                                                                                                    .TreatDetails = t.TreatDetails,
                                                                                                    .TreatNotes = t.TreatNotes,
                                                                                                    .TreatEndDate = If(t.TreatEndDate.HasValue, t.TreatEndDate.Value, Date.MinValue)
                                                                                                    }).ToList()
                    End If
            End Select


            VGridTrts.DataSource = TrtBS
            'How can i get TreatmentSummary.Tooth to replace toothNum in the next line ????

            If TrtBS IsNot Nothing AndAlso TrtBS.Count > 0 Then

                Dim firstSummary As TreatmentSummary = CType(TrtBS(0), TreatmentSummary)

            Else

            End If

            ' Pass the loaded PatientTreats data to the next method
            Select Case Source
                Case "Treat"
                    'LoadPatientToothTreat(PatientTreats)
                    LoadTeethTreats(PatientTreats)
                Case "Mobile"
                    ' --- Legacy: LoadPatientMOBILE(PatientMobiles) rendered Patient_Mobile-specific SVG branches. ---
                    'LoadPatientMOBILE(PatientMobiles)
                    LoadTeethTreats(PatientTreats, isMobileChart:=True)
            End Select

        Else
            Dim msgEng As String = "Patient not found."
            Dim msgAr As String = "المريض غير موجود."
            Dim msg As String = If(Eng, msgEng, msgAr)
            MessageBox.Show(msg)
        End If
        StoreOriginalBounds(Me)
    End Sub


    Public Sub LoadPatientToothTreat(patientTreats As IEnumerable(Of Patient_ToothTrt))
        PanelSvgs.SuspendLayout()


        ' If there are no treatments, reset visibility for all SvgImageBoxes EXTRACTED IMG
        If patientTreats IsNot Nothing OrElse Not patientTreats.Any() Then
            For Each ct As Control In Me.PanelSvgs.Controls
                If TypeOf ct Is SvgImageBox Then
                    Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
                    ClearSvgBackground(svg)
                    Dim col As SvgImageItemCollection = svg.RootItems
                    If Not svgSelectedList.Contains(svg) OrElse Not svgExternalList.Contains(svg) Then
                        svg.BackColor = Color.Transparent
                    End If
                    ' Reset all items invisible
                    For Each item As SvgImageItem In col
                        item.Visible = False
                    Next
                    ' Get tooth number
                    Dim toothNum As Byte = CByte(svg.Tag)

                    Dim baseTooth = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG") AndAlso c.Id <> "CROWN_IMG")
                    Dim trtsList = patientTreats.Where(Function(t) t.ToothNum = toothNum).OrderBy(Function(t) t.LVL).ToList
                    Dim rootTrts = patientTreats.Where(Function(t) t.TrtLoc = "ROOT").OrderBy(Function(t) t.LVL).ToList
                    Dim crownTrts = patientTreats.Where(Function(t) t.TrtLoc = "CROWN").OrderBy(Function(t) t.LVL).ToList

#Region "External Treats"
                    '======IMPORTANT==========================================
                    'External Treats
                    ' Check for external treatments
                    Dim externalTrts = trtsList.Where(Function(t) t.IsExternal.HasValue AndAlso t.IsExternal.Value = True).ToList()
                    If externalTrts.Any() Then


                        ' 3. Deep Ocean Metallic (Horizontal)
                        ApplyGradientBackground(svg,
                                           ColorTranslator.FromHtml("#E8E9EB"),' Color.FromArgb(0, 163, 255),   ' Deep ocean blue
                                             ColorTranslator.FromHtml("#EBECF0"),'Color.FromArgb(0, 255, 188),   ' Metallic teal
                                            Drawing2D.LinearGradientMode.BackwardDiagonal,
                                            128) '200= 80% opacity

                        '    ' Apply special styling for external treatments

                        svgExternalList.Add(svg)
                        '' Add a tooltip to indicate external treatment
                        'Dim clinicNames = externalTrts.Select(Function(t) t.ExternalClinicName).Distinct()
                        'svg.ToolTip = "Treated externally at: " & String.Join(", ", clinicNames)

                        '' You could also add a small overlay icon/mark
                        'Dim mark = svg.RootItems.Find(Function(c) c.Id = "EXTRACTION")
                        'If mark IsNot Nothing Then mark.Visible = True
                    End If

                    'External Treats
                    '================================================
#End Region

                    If trtsList.Count = 0 Then
                        ' No treatments for this tooth, just show base image
                        If baseTooth IsNot Nothing Then baseTooth.Visible = True
                        Continue For
                    End If
                    Dim maxLevel As Integer = trtsList.Max(Function(t) t.LVL)
                    ' Always show IMG placeholder if needed
                    If baseTooth IsNot Nothing AndAlso maxLevel < 4 Then
                        baseTooth.Visible = True
                    End If

                    Select Case maxLevel
                        Case 0, 1, 2, 3
                            For Each t In trtsList
                                Dim item = col.Find(Function(c) c.Id = t.PropertyName)
                                If item IsNot Nothing Then
                                    item.Visible = True
                                    item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                    item.Appearance.Normal.BorderThickness = t.BorderThickness
                                End If
                                If t.LVL = 0 AndAlso (t.Treat = "PULPECTOMY") Then
                                    Dim pulpecItem = col.Find(Function(c) c.Id = "PULPECTOMY")
                                    pulpecItem.Visible = True
                                    pulpecItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    pulpecItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                    pulpecItem.Appearance.Normal.BorderThickness = t.BorderThickness
                                End If
                                If t.LVL = 0 AndAlso t.Treat = "DIRECT PULP CAPPING" Then
                                    If item IsNot Nothing Then
                                        Dim dir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")

                                        item.Visible = True
                                        item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                        item.Appearance.Normal.BorderThickness = 2 ' t.BorderThickness
                                        Dim indir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")
                                        If indir IsNot Nothing Then
                                            indir.Visible = True
                                            indir.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                            indir.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                            indir.Appearance.Normal.BorderThickness = t.BorderThickness
                                        End If
                                    End If
                                End If


                                If t.LVL = 3 AndAlso t.Treat = "STAINLESS STEEL CROWN T" Then
                                    Dim crownTooth = col.Find(Function(c) c.Id = "STAINLESS_STEEL_CROWN")
                                    If crownTooth IsNot Nothing Then
                                        'For Each itemc As SvgImageItem In col
                                        '    itemc.Visible = False
                                        'Next
                                        'ShowBaseTooth(col)
                                        crownTooth.Visible = True
                                        crownTooth.Appearance.Normal.BorderThickness = 1
                                        crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        'For Each tc In trtsList
                                        '    Select Case tc.Treat
                                        '        Case "METAL CROWN T", "ZERCONIA CROWN T", "PFM CROWN T", "EMAX CROWN T", "TEMP CROWN T", "STAINLESS STEEL CROWN T"
                                        '            If crownTooth IsNot Nothing Then crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor)
                                        '    End Select
                                        'Next
                                    End If
                                    If rootTrts.Any Then
                                        Dim rootItem = col.Find(Function(c) c.Id = t.PropertyName)
                                        If rootItem IsNot Nothing Then
                                            rootItem.Visible = True
                                            rootItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                            rootItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                            rootItem.Appearance.Normal.BorderThickness = t.BorderThickness
                                        End If
                                        If t.LVL = 0 AndAlso t.Treat = "DIRECT PULP CAPPING" Then
                                            If rootItem IsNot Nothing Then
                                                Dim dir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")

                                                rootItem.Visible = True
                                                rootItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                                rootItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                                rootItem.Appearance.Normal.BorderThickness = 2 ' t.BorderThickness
                                                Dim indir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")
                                                If indir IsNot Nothing Then
                                                    indir.Visible = True
                                                    indir.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                                    indir.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                                    indir.Appearance.Normal.BorderThickness = t.BorderThickness
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Next

                        Case 4 ' EXTRACTED
                            If svgExternalList.Contains(svg) Then
                                Dim extracted = col.Find(Function(c) c.Id = "EXTRACTION")
                                If extracted IsNot Nothing Then extracted.Visible = True
                                ' Ensure only extraction is visible
                                For Each item As SvgImageItem In col
                                    If item.Id <> "EXTRACTION" Then item.Visible = False
                                Next
                            Else
                                Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                                If extracted IsNot Nothing Then extracted.Visible = True
                                ' Ensure only extraction is visible
                                For Each item As SvgImageItem In col
                                    If item.Id <> "EXTRACTED" Then item.Visible = False
                                Next
                            End If

                        Case 5 ' Implant
                            Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                            If implant IsNot Nothing Then implant.Visible = True
                            For Each item As SvgImageItem In col
                                If item.Id <> "IMPLANT" Then item.Visible = False
                            Next
                        Case 6 ' Implant + stages
                            ' Show implant always
                            Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                            If implant IsNot Nothing Then implant.Visible = True
                            ' Check and show Healing Cap
                            If trtsList.Any(Function(t) t.PropertyName = "HEALING_CAP" AndAlso t.LVL = 6) Then
                                Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                                If heal IsNot Nothing Then heal.Visible = True
                            End If
                            ' Check and show Abutment
                            If trtsList.Any(Function(t) t.PropertyName = "ABUTMENT" AndAlso t.LVL = 6) Then
                                Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                                If abut IsNot Nothing Then abut.Visible = True
                            End If
#Region "TOdel"
                            ' Show final crown and hide Healing/Abutment if present
                            If trtsList.Any(Function(t) t.PropertyName = "CROWN_IMG" AndAlso t.LVL = 6) Then
                                Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG")
                                If crownImg IsNot Nothing Then crownImg.Visible = True
                                Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                                If heal IsNot Nothing Then heal.Visible = False
                                Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                                If abut IsNot Nothing Then abut.Visible = False
                                ' Optional: extra visual crown
                                If svg.Name.Contains("Top") Then
                                    ' Make sure base image is shown if needed
                                    Dim topBase = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG"))
                                    If topBase IsNot Nothing Then topBase.Visible = True
                                    ' Hide implant layer
                                    Dim implantLayer = col.Find(Function(c) c.Id = "IMPLANT")
                                    If implantLayer IsNot Nothing Then implantLayer.Visible = False
                                    Dim heal1 = col.Find(Function(c) c.Id = "HEALING_CAP")
                                    If heal1 IsNot Nothing Then heal.Visible = False
                                    Dim abut1 = col.Find(Function(c) c.Id = "ABUTMENT")
                                    If abut1 IsNot Nothing Then abut.Visible = False
                                End If
                                Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                                If crownFill IsNot Nothing Then
                                    crownFill.Visible = True
                                    crownFill.Appearance.Normal.BorderThickness = 0
                                    For Each t In trtsList.Where(Function(c) c.LVL = 3)
                                        Select Case t.Treat
                                            Case "METAL CROWN", "ZERCONIA CROWN", "PFM CROWN", "EMAX CROWN", "TEMP CROWN", "STAINLESS STEEL CROWN"
                                                If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        End Select
                                    Next
                                End If
                            End If
#End Region
                        Case 7 'CROWN ON IMPLANT
                            'TOP VIEW
                            If svg.Name.Contains("Top") Then
                                For Each item As SvgImageItem In col
                                    item.Visible = False
                                Next
                                Dim crownIMP = col.Find(Function(c) c.Id = "CROWN_FILL")
                                If crownIMP IsNot Nothing Then
                                    crownIMP.Visible = True
                                    crownIMP.Appearance.Normal.BorderThickness = 1
                                    For Each tc In trtsList
                                        Select Case tc.Treat
                                            Case "METAL CROWN I", "ZERCONIA CROWN I", "PFM CROWN I", "EMAX CROWN I", "TEMP CROWN I", "STAINLESS STEEL CROWN I"
                                                If crownIMP IsNot Nothing Then crownIMP.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor)
                                        End Select
                                    Next
                                End If
                                If baseTooth IsNot Nothing Then
                                    baseTooth.Visible = True
                                End If
                            Else
                                ' Show implant always
                                Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                                If implant IsNot Nothing Then implant.Visible = True
                                Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                                If crownImg IsNot Nothing Then
                                    crownImg.Visible = True
                                End If
                                Dim crownIMP = col.Find(Function(c) c.Id = "CROWN_FILL")
                                If crownIMP IsNot Nothing Then
                                    crownIMP.Visible = True
                                    crownIMP.Appearance.Normal.BorderThickness = 1
                                    For Each tc In trtsList

                                        Select Case tc.Treat
                                            Case "METAL CROWN I", "ZERCONIA CROWN I", "PFM CROWN I", "EMAX CROWN I",
                                        "TEMP CROWN I", "STAINLESS STEEL CROWN I"
                                                crownIMP.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor)
                                        End Select
                                    Next
                                End If
                            End If
                        Case 8 ' BRIDGE  
                            ' 1. Handle bridge differently per view
                            If svg.Name.Contains("Top") Then
                                For Each item As SvgImageItem In col
                                    item.Visible = False
                                Next
                                ' Top View - style the base tooth as crown
                                If baseTooth IsNot Nothing Then
                                    baseTooth.Visible = True
                                End If
                                'Show BRIDGE Mark
                                Dim bridgMark = col.Find(Function(c) c.Id = "BR")
                                If bridgMark IsNot Nothing Then
                                    bridgMark.Visible = True
                                    bridgMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
                                End If
#Region "delete"
                            ElseIf svg.Name.Contains("Out") Then
                                ' 2. Always show CON if exists
                                If trtsList.Any(Function(t) t.PropertyName = "IMPLANT") Then
                                    Dim IMPLANT = col.Find(Function(c) c.Id = "IMPLANT")
                                    If IMPLANT IsNot Nothing Then IMPLANT.Visible = True
                                    ' Out View - show dedicated crown layer
                                    Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                                    If crownImg IsNot Nothing Then
                                        crownImg.Visible = True
                                    End If
                                ElseIf trtsList.Any(Function(t) t.PropertyName = "EXTRACTION") Then
                                    Dim EXTRACTED = col.Find(Function(c) c.Id = "EXTRACTION")
                                    If EXTRACTED IsNot Nothing Then EXTRACTED.Visible = True
                                    ' Out View - show dedicated crown layer
                                    Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                                    If crownImg IsNot Nothing Then
                                        crownImg.Visible = True
                                    End If
                                Else
                                    baseTooth.Visible = True
                                End If
                                'Show BRIDGE Mark
                                Dim bridgMark = col.Find(Function(c) c.Id = "BR")
                                If bridgMark IsNot Nothing Then
                                    bridgMark.Visible = True
                                    bridgMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
                                End If
                                ' Out View - show dedicated crown layer
                                Dim crownItem = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                                If crownItem IsNot Nothing Then
                                    crownItem.Visible = True
                                End If
#End Region

                            End If
                            'Show Crown Fill
                            Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                            If crownFill IsNot Nothing Then
                                crownFill.Visible = True
                                For Each t In trtsList
                                    Select Case t.Treat
                                        Case "METAL BRIDGE", "ZERCONIA BRIDGE", "PFM BRIDGE", "EMAX BRIDGE",
                                        "TEMP BRIDGE", "STAINLESS STEEL BRIDGE"
                                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    End Select
                                Next
                            End If



                    End Select

                End If

            Next
        End If



        PanelSvgs.ResumeLayout()
    End Sub

    ' --- Legacy full renderer for dbo.Patient_Mobile (unused after migration; kept for reference / comparison with TreatHelper mobile path). ---
    Public Sub LoadPatientMOBILE(patientMobiles As IEnumerable(Of Patient_Mobile))
        Dim sw = StartTimer()
        PanelSvgs.SuspendLayout()
        Me.Visible = False
        For Each ct As Control In Me.PanelSvgs.Controls
            If TypeOf ct Is SvgImageBox Then
                Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
                Dim col As SvgImageItemCollection = svg.RootItems
                If Not svgSelectedList.Contains(svg) OrElse Not svgExternalList.Contains(svg) Then
                    svg.BackColor = Color.Transparent
                End If
                ' Reset all items invisible
                For Each item As SvgImageItem In col
                    item.Visible = False
                Next
                ' Get tooth number
                Dim toothNum As Byte = CByte(svg.Tag)
                'Dim baseCH = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id = "BR")
                'If baseCH IsNot Nothing Then baseCH.Visible = True
                Dim baseTooth = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG") AndAlso c.Id <> "CROWN_IMG")
                Dim mobsList = patientMobiles.Where(Function(t) t.ToothNum = toothNum).ToList()
                '======IMPORTANT==========================================
                'External Treats
                ' Check for external treatments
                Dim externalTrts = mobsList.Where(Function(t) t.IsExternal.HasValue AndAlso t.IsExternal.Value = True).ToList()

                If externalTrts.Any() Then
                    ' Apply special styling for external treatments
                    svg.BackColor = Color.LightGray ' Or any distinctive color
                    svgExternalList.Add(svg)
                    ' Add a tooltip to indicate external treatment
                    Dim clinicNames = externalTrts.Select(Function(t) t.ExternalClinicName).Distinct()
                    svg.ToolTip = "Treated externally at: " & String.Join(", ", clinicNames)

                    ' You could also add a small overlay icon/mark
                    Dim mark = svg.RootItems.Find(Function(c) c.Id = "EXTERNAL_MARK")
                    If mark IsNot Nothing Then mark.Visible = True
                End If
                'External Treats
                '================================================

                If mobsList.Count = 0 Then
                    ' No treatments for this tooth, just show base image
                    If baseTooth IsNot Nothing Then baseTooth.Visible = True
                    'Dim chTooth = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("CH"))
                    'If chTooth IsNot Nothing Then chTooth.Visible = True
                    Continue For
                End If


                Dim maxLevel As Integer = mobsList.Max(Function(t) t.LVL)
                ' Always show IMG placeholder if needed
                If baseTooth IsNot Nothing AndAlso maxLevel < 3 Then
                    baseTooth.Visible = True
                End If
                Select Case maxLevel
#Region "Case 0, 1, 2"
        'Case 0, 1, 2
                    '    For Each mob In mobsList
                    '        Dim item = col.Find(Function(c) c.Id = mob.PropertyName)
                    '        If mob.Treat.Contains("VENEERS") Then
                    '            Dim crownItem = col.Find(Function(c) c.Id = "CROWN")
                    '            If crownItem IsNot Nothing Then
                    '                crownItem.Visible = True
                    '                crownItem.Appearance.Normal.FillColor = GetTrtColor(mob.Treat, PatientID, toothNum)
                    '                crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(mob.BorderColor)
                    '            End If
                    '        Else
                    '            If item IsNot Nothing AndAlso Not item.Id = "BR" Then
                    '                item.Visible = True
                    '                item.Appearance.Normal.FillColor = GetTrtColor(mob.Treat, PatientID, toothNum)
                    '                item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(mob.BorderColor)
                    '                item.Appearance.Normal.BorderThickness = mob.BorderThickness
                    '            End If
                    '        End If
                    '        If mob.Treat = "COMPLETE DENTURE" Then 'REMOVABLE PARTIAL DENTURE
                    '            If svg.Name.Contains("Top") Then
                    '                ' Top View - style the base tooth as crown
                    '                If baseTooth IsNot Nothing Then
                    '                    baseTooth.Visible = True
                    '                    ' Apply crown styling (use first bridge treatment's color)
                    '                    Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                    '                    If crownFill IsNot Nothing Then crownFill.Visible = True
                    '                    Dim dentureItem = col.Find(Function(d) d.Id = mob.PropertyName)
                    '                    If dentureItem IsNot Nothing Then
                    '                        dentureItem.Visible = True
                    '                        dentureItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#9AF447D1")
                    '                    End If
                    '                End If
                    '            Else

                    '                'Dim dentureItem = col.Find(Function(d) d.Id = mob.PropertyName)
                    '                'If dentureItem IsNot Nothing Then
                    '                '    dentureItem.Visible = True
                    '                '    dentureItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#9AF447D1")
                    '                'End If
                    '                baseTooth.Visible = False
                    '                Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                    '                If extracted IsNot Nothing Then
                    '                    extracted.Visible = True
                    '                    extracted.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#9AF447D1")
                    '                End If
                    '                Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG")
                    '                If crownImg IsNot Nothing Then crownImg.Visible = True
                    '                Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                    '                If crownFill IsNot Nothing Then crownFill.Visible = True
                    '                For Each t In mobsList
                    '                    Select Case t.Treat
                    '                        Case "METAL CROWN"
                    '                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                    '                        Case "ZERCONIA CROWN"
                    '                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                    '                        Case "PFM CROWN"
                    '                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                    '                        Case "EMAX CROWN"
                    '                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                    '                        Case "TEMP CROWN"
                    '                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                    '                        Case "STAINLESS STEEL CROWN"
                    '                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                    '                    End Select
                    '                Next
                    '            End If
                    '        ElseIf mob.Treat = "REMOVABLE PARTIAL DENTURE" Then 'REMOVABLE PARTIAL DENTURE
                    '            If svg.Name.Contains("Top") Then
                    '                ' Top View - style the base tooth as crown
                    '                If baseTooth IsNot Nothing Then
                    '                    baseTooth.Visible = True
                    '                    ' Apply crown styling (use first bridge treatment's color)
                    '                    Dim topCrownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                    '                    If topCrownFill IsNot Nothing Then topCrownFill.Visible = True
                    '                End If

                    '                'Dim dentureItem = col.Find(Function(d) d.Id = mob.PropertyName)
                    '                'If dentureItem IsNot Nothing Then
                    '                '    dentureItem.Visible = True
                    '                '    dentureItem.Appearance.Normal.FillColor = Color.Indigo
                    '                'End If
                    '            Else
                    '                baseTooth.Visible = False
                    '                Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                    '                If extracted IsNot Nothing Then
                    '                    extracted.Visible = True
                    '                    extracted.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#80F447D1")
                    '                End If
                    '                Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG")
                    '                If crownImg IsNot Nothing Then crownImg.Visible = True
                    '                Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                    '                If crownFill IsNot Nothing Then crownFill.Visible = True

                    '            End If
                    '        End If



                    '        If mob.LVL = 1 AndAlso mob.Treat = "APICECTOMY" Then
                    '            Dim apic = col.Find(Function(c) c.Id = "APICECTOMY")
                    '            If apic IsNot Nothing Then apic.Visible = True
                    '        End If

                    '        If mob.LVL = 2 AndAlso mob.Treat = "HEMISECTION" Then
                    '            Dim hemi = col.Find(Function(c) c.Id = "HEMISECTION")
                    '            If hemi IsNot Nothing Then hemi.Visible = True
                    '        End If

                    '    Next

#End Region

                    Case 3 ' EXTRACTED
                        Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                        If extracted IsNot Nothing Then extracted.Visible = True

                    Case 4 ' Implant
                        Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                        If implant IsNot Nothing Then implant.Visible = True

                    Case 5 ' Implant + stages
                        ' Show implant always
                        Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                        If implant IsNot Nothing Then implant.Visible = True

                        ' Check and show Healing Cap
                        If mobsList.Any(Function(t) t.PropertyName = "HEALING_CAP") Then
                            Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                            If heal IsNot Nothing Then heal.Visible = True
                        End If

                        ' Check and show Abutment
                        If mobsList.Any(Function(t) t.PropertyName = "ABUTMENT") Then
                            Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                            If abut IsNot Nothing Then abut.Visible = True
                        End If

                        ' Show final crown and hide Healing/Abutment if present
                        If mobsList.Any(Function(t) t.PropertyName = "CROWN_IMG") Then
                            Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG")
                            If crownImg IsNot Nothing Then crownImg.Visible = True

                            Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                            If heal IsNot Nothing Then heal.Visible = False

                            Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                            If abut IsNot Nothing Then abut.Visible = False

                            ' Optional: extra visual crown
                            If svg.Name.Contains("Top") Then
                                ' When crown is final, hide implant and base layers to avoid visual overlap

                                ' Make sure base image is shown if needed
                                Dim topBase = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG"))
                                If topBase IsNot Nothing Then topBase.Visible = True
                                ' Hide implant layer
                                Dim implantLayer = col.Find(Function(c) c.Id = "IMPLANT")
                                If implantLayer IsNot Nothing Then implantLayer.Visible = False
                                Dim heal1 = col.Find(Function(c) c.Id = "HEALING_CAP")
                                If heal1 IsNot Nothing Then heal.Visible = False

                                Dim abut1 = col.Find(Function(c) c.Id = "ABUTMENT")
                                If abut1 IsNot Nothing Then abut.Visible = False
                                ' Optional top view crown

                                Dim crownTop = col.Find(Function(c) c.Id = "CROWN")
                                If crownTop IsNot Nothing Then
                                    crownTop.Visible = True
                                    Dim t = mobsList.FirstOrDefault(Function(x) x.PropertyName = "CROWN")
                                    If t IsNot Nothing Then
                                        crownTop.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    End If
                                End If

                            End If

                            Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                            If crownFill IsNot Nothing Then crownFill.Visible = True
                            For Each t In mobsList
                                Select Case t.Treat
                                    Case "METAL CROWN"
                                        If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    Case "ZERCONIA CROWN"
                                        If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    Case "PFM CROWN"
                                        If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    Case "EMAX CROWN"
                                        If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    Case "TEMP CROWN"
                                        If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    Case "STAINLESS STEEL CROWN"
                                        If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                End Select
                            Next


                        End If
                    Case 6   'CROWN
                        ' 1. Handle CROWN differently per view
                        If svg.Name.Contains("Top") Then
                            ' Top View - style the base tooth as crown
                            If baseTooth IsNot Nothing Then
                                baseTooth.Visible = True
                                ' Apply crown styling (use first bridge treatment's color)
                                Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 6)
                                'If bridgeTrt IsNot Nothing Then
                                '    baseTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(bridgeTrt.FillColor)
                                '    baseTooth.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                                'End If
                            End If
                        ElseIf svg.Name.Contains("Out") OrElse svg.Name.Contains("IN") Then
                            ' 2. Always show extraction if exists
                            Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                            If extracted IsNot Nothing Then extracted.Visible = True
                            ' Out View - show dedicated crown layer
                            Dim crownItem = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                            If crownItem IsNot Nothing Then
                                crownItem.Visible = True
                                ' Apply styling
                                Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 6)
                                'If bridgeTrt IsNot Nothing Then
                                '    crownItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(bridgeTrt.FillColor)
                                '    crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                                'End If
                            End If
                        End If

                    Case 7 ' BRIDGE  
                        ' 1. Handle bridge differently per view
                        If svg.Name.Contains("Top") Then
                            ' Top View - style the base tooth as crown
                            If baseTooth IsNot Nothing Then
                                baseTooth.Visible = True
                                ' Apply crown styling (use first bridge treatment's color)
                                Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 7)
                                If bridgeTrt IsNot Nothing Then
                                    baseTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(bridgeTrt.FillColor)
                                    baseTooth.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                                End If
                            End If
                        ElseIf svg.Name.Contains("Out") OrElse svg.Name.Contains("IN") Then
                            '' 2. Always show extraction if exists
                            Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                            If extracted IsNot Nothing Then extracted.Visible = True
                            ' Out View - show dedicated crown layer
                            Dim crownItem = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                            If crownItem IsNot Nothing Then
                                crownItem.Visible = True
                                ' Apply styling
                                Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 7)
                                If bridgeTrt IsNot Nothing Then
                                    crownItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(bridgeTrt.FillColor)
                                    crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                                End If
                            End If
                        End If
                    Case 8 ' DENTURE  
                        ' 1. Handle bridge differently per view
                        If svg.Name.Contains("Top") Then
                            ' Top View - style the base tooth as crown
                            If baseTooth IsNot Nothing Then
                                baseTooth.Visible = True
                                'Show Denture Mark
                                Dim dentMark = col.Find(Function(c) c.Id = "CH")
                                If dentMark IsNot Nothing Then
                                    dentMark.Visible = True
                                    dentMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("DENTUREMARK")
                                End If
                                ' Apply crown styling (use first bridge treatment's color)
                                Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 8)
                                If bridgeTrt IsNot Nothing Then
                                    baseTooth.Appearance.Normal.FillColor = GetCustomTrtColor(bridgeTrt.Treat) ' ColorTranslator.FromHtml(bridgeTrt.FillColor)
                                    baseTooth.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                                End If
                            End If
                        ElseIf svg.Name.Contains("Out") OrElse svg.Name.Contains("IN") Then
                            '' 2. Always show extraction if exists
                            Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                            If extracted IsNot Nothing Then extracted.Visible = False
                            'Show Denture Mark
                            Dim dentMark = col.Find(Function(c) c.Id = "CH")
                            If dentMark IsNot Nothing Then
                                dentMark.Visible = True
                                dentMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("DENTUREMARK")
                            End If
                            ' Out View - show dedicated crown layer
                            Dim crownItem = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                            If crownItem IsNot Nothing Then
                                crownItem.Visible = True
                                ' Apply styling
                                Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 8)
                                If bridgeTrt IsNot Nothing Then
                                    crownItem.Appearance.Normal.FillColor = GetCustomTrtColor(bridgeTrt.Treat) '  ColorTranslator.FromHtml(bridgeTrt.FillColor)
                                    crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                                End If
                            End If
                        End If
                End Select
            End If
        Next
        PanelSvgs.ResumeLayout()
        Me.Visible = True
        LogTime(NameOf(LoadPatientMOBILE), Me.Name, sw)
    End Sub

    Private Sub TrtBS_PositionChanged(sender As Object, e As EventArgs) Handles TrtBS.PositionChanged
        Try
            ' Check if the current position in TrtBS is valid
            If TrtBS.Current IsNot Nothing Then
                ' Cast the current item to Patient_ToothTrt
                Dim currentTrt As TreatmentSummary = CType(TrtBS.Current, TreatmentSummary)

                ' Load the history for the current treatment


                ' Check if ToothTrtHist is not Nothing and bind it to a BindingSource

                'If HistBS Is Nothing Then
                '    HistBS = New BindingSource()
                'End If
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred while loading the treatment history: {ex.Message}")
        End Try
    End Sub

    '===========================================
    Public Sub LoadSnglTreat(patientId As Integer, toothNum As Byte)
        If patientId <= 0 Then Return
        Me.PatientID = patientId
        If clsPatientData Is Nothing Then clsPatientData = New PatientDATA()
        clsPatient = New Patient With {.PatientID = patientId}
        clsPatient = clsPatientData.Select_Record(clsPatient)

        If clsPatient IsNot Nothing Then
            PatientName = clsPatient.PatientName
            ' Load tooth checks for the patient

            PatientTreats = clsToothTrtData.Select_BypID_tNum(New Patient_ToothTrt With {.PatientID = patientId, .ToothNum = toothNum})
            ' Pass the loaded PatientCheck data to the next method
            'LoadPatientSnglToothTreat(PatientTreats)
            LoadSnglToothTreats(PatientTreats)
        Else
            Dim msgEng As String = "Patient not found."
            Dim msgAr As String = "المريض غير موجود."
            Dim msg As String = If(Eng, msgEng, msgAr)
            MessageBox.Show(msg)
        End If
    End Sub

    Public Sub LoadPatientSnglToothTreat(patientTreats As IEnumerable(Of Patient_ToothTrt))


        ' If there are no treatments, reset visibility for all SvgImageBoxes EXTRACTED IMG
        If patientTreats Is Nothing OrElse Not patientTreats.Any() Then
            For Each ct As Control In Me.ToothShapePage.Controls
                If TypeOf ct Is SvgImageBox Then
                    Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
                    Dim col As SvgImageItemCollection = svg.RootItems
                    ' Reset visibility for all items

                    For Each item As SvgImageItem In col
                        item.Visible = Not String.IsNullOrEmpty(item.Id) AndAlso item.Id.Contains("IMG") AndAlso item.Id <> "CROWN_IMG"
                    Next

                End If
            Next
            Return
        End If

        ' Process the treatments and update the SvgImageBoxes
        For Each ct As Control In Me.ToothShapePage.Controls
            If TypeOf ct Is SvgImageBox Then
                Dim svg As SvgImageBox = DirectCast(ct, SvgImageBox)
                ClearSvgBackground(svg)
                Dim col As SvgImageItemCollection = svg.RootItems
                If Not svgSelectedList.Contains(svg) OrElse Not svgExternalList.Contains(svg) Then
                    svg.BackColor = Color.Transparent
                End If
                ' Reset all items invisible
                For Each item As SvgImageItem In col
                    item.Visible = False
                Next
                ' Get tooth number
                Dim toothNum As Byte = CByte(svg.Tag)

                Dim baseTooth = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG") AndAlso c.Id <> "CROWN_IMG")
                Dim trtsList = patientTreats.Where(Function(t) t.ToothNum = toothNum).OrderBy(Function(t) t.LVL).ToList
                Dim rootTrts = patientTreats.Where(Function(t) t.TrtLoc = "ROOT").OrderBy(Function(t) t.LVL).ToList
                Dim crownTrts = patientTreats.Where(Function(t) t.TrtLoc = "CROWN").OrderBy(Function(t) t.LVL).ToList

#Region "External Treats"
                '======IMPORTANT==========================================
                'External Treats
                ' Check for external treatments
                Dim externalTrts = trtsList.Where(Function(t) t.IsExternal.HasValue AndAlso t.IsExternal.Value = True).ToList()
                If externalTrts.Any() Then


                    ' 3. Deep Ocean Metallic (Horizontal)
                    ApplyGradientBackground(svg,
                                           ColorTranslator.FromHtml("#E8E9EB"),' Color.FromArgb(0, 163, 255),   ' Deep ocean blue
                                             ColorTranslator.FromHtml("#EBECF0"),'Color.FromArgb(0, 255, 188),   ' Metallic teal
                                            Drawing2D.LinearGradientMode.BackwardDiagonal,
                                            128) '200= 80% opacity

                    '    ' Apply special styling for external treatments

                    svgExternalList.Add(svg)
                    '' Add a tooltip to indicate external treatment
                    'Dim clinicNames = externalTrts.Select(Function(t) t.ExternalClinicName).Distinct()
                    'svg.ToolTip = "Treated externally at: " & String.Join(", ", clinicNames)

                    '' You could also add a small overlay icon/mark
                    'Dim mark = svg.RootItems.Find(Function(c) c.Id = "EXTRACTION")
                    'If mark IsNot Nothing Then mark.Visible = True
                End If

                'External Treats
                '================================================
#End Region

                If trtsList.Count = 0 Then
                    ' No treatments for this tooth, just show base image
                    If baseTooth IsNot Nothing Then baseTooth.Visible = True
                    Continue For
                End If
                Dim maxLevel As Integer = trtsList.Max(Function(t) t.LVL)
                ' Always show IMG placeholder if needed
                If baseTooth IsNot Nothing AndAlso maxLevel < 4 Then
                    baseTooth.Visible = True
                End If

                Select Case maxLevel
                    Case 0, 1, 2, 3
                        For Each t In trtsList
                            Dim item = col.Find(Function(c) c.Id = t.PropertyName)
                            If item IsNot Nothing Then
                                item.Visible = True
                                item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                item.Appearance.Normal.BorderThickness = t.BorderThickness
                            End If
                            If t.LVL = 0 AndAlso (t.Treat = "PULPECTOMY") Then
                                Dim pulpecItem = col.Find(Function(c) c.Id = "PULPECTOMY")
                                pulpecItem.Visible = True
                                pulpecItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                pulpecItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                pulpecItem.Appearance.Normal.BorderThickness = t.BorderThickness
                            End If
                            If t.LVL = 0 AndAlso t.Treat = "DIRECT PULP CAPPING" Then
                                If item IsNot Nothing Then
                                    Dim dir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")

                                    item.Visible = True
                                    item.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                    item.Appearance.Normal.BorderThickness = 2 ' t.BorderThickness
                                    Dim indir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")
                                    If indir IsNot Nothing Then
                                        indir.Visible = True
                                        indir.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        indir.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                        indir.Appearance.Normal.BorderThickness = t.BorderThickness
                                    End If
                                End If
                            End If


                            If t.LVL = 3 AndAlso t.Treat = "STAINLESS STEEL CROWN T" Then
                                Dim crownTooth = col.Find(Function(c) c.Id = "STAINLESS_STEEL_CROWN")
                                If crownTooth IsNot Nothing Then
                                    'For Each itemc As SvgImageItem In col
                                    '    itemc.Visible = False
                                    'Next
                                    'ShowBaseTooth(col)
                                    crownTooth.Visible = True
                                    crownTooth.Appearance.Normal.BorderThickness = 1
                                    crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    'For Each tc In trtsList
                                    '    Select Case tc.Treat
                                    '        Case "METAL CROWN T", "ZERCONIA CROWN T", "PFM CROWN T", "EMAX CROWN T", "TEMP CROWN T", "STAINLESS STEEL CROWN T"
                                    '            If crownTooth IsNot Nothing Then crownTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor)
                                    '    End Select
                                    'Next
                                End If
                                If rootTrts.Any Then
                                    Dim rootItem = col.Find(Function(c) c.Id = t.PropertyName)
                                    If rootItem IsNot Nothing Then
                                        rootItem.Visible = True
                                        rootItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        rootItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                        rootItem.Appearance.Normal.BorderThickness = t.BorderThickness
                                    End If
                                    If t.LVL = 0 AndAlso t.Treat = "DIRECT PULP CAPPING" Then
                                        If rootItem IsNot Nothing Then
                                            Dim dir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")

                                            rootItem.Visible = True
                                            rootItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                            rootItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                            rootItem.Appearance.Normal.BorderThickness = 2 ' t.BorderThickness
                                            Dim indir = col.Find(Function(c) c.Id = "INDIRECT_PULP_CAPPING")
                                            If indir IsNot Nothing Then
                                                indir.Visible = True
                                                indir.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                                indir.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(t.BorderColor)
                                                indir.Appearance.Normal.BorderThickness = t.BorderThickness
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next

                    Case 4 ' EXTRACTED
                        If svgExternalList.Contains(svg) Then
                            Dim extracted = col.Find(Function(c) c.Id = "EXTRACTION")
                            If extracted IsNot Nothing Then extracted.Visible = True
                            ' Ensure only extraction is visible
                            For Each item As SvgImageItem In col
                                If item.Id <> "EXTRACTION" Then item.Visible = False
                            Next
                        Else
                            Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                            If extracted IsNot Nothing Then extracted.Visible = True
                            ' Ensure only extraction is visible
                            For Each item As SvgImageItem In col
                                If item.Id <> "EXTRACTED" Then item.Visible = False
                            Next
                        End If

                    Case 5 ' Implant
                        Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                        If implant IsNot Nothing Then implant.Visible = True
                        For Each item As SvgImageItem In col
                            If item.Id <> "IMPLANT" Then item.Visible = False
                        Next
                    Case 6 ' Implant + stages
                        ' Show implant always
                        Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                        If implant IsNot Nothing Then implant.Visible = True
                        ' Check and show Healing Cap
                        If trtsList.Any(Function(t) t.PropertyName = "HEALING_CAP" AndAlso t.LVL = 6) Then
                            Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                            If heal IsNot Nothing Then heal.Visible = True
                        End If
                        ' Check and show Abutment
                        If trtsList.Any(Function(t) t.PropertyName = "ABUTMENT" AndAlso t.LVL = 6) Then
                            Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                            If abut IsNot Nothing Then abut.Visible = True
                        End If
#Region "TOdel"
                        ' Show final crown and hide Healing/Abutment if present
                        If trtsList.Any(Function(t) t.PropertyName = "CROWN_IMG" AndAlso t.LVL = 6) Then
                            Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG")
                            If crownImg IsNot Nothing Then crownImg.Visible = True
                            Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                            If heal IsNot Nothing Then heal.Visible = False
                            Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                            If abut IsNot Nothing Then abut.Visible = False
                            ' Optional: extra visual crown
                            If svg.Name.Contains("Top") Then
                                ' Make sure base image is shown if needed
                                Dim topBase = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG"))
                                If topBase IsNot Nothing Then topBase.Visible = True
                                ' Hide implant layer
                                Dim implantLayer = col.Find(Function(c) c.Id = "IMPLANT")
                                If implantLayer IsNot Nothing Then implantLayer.Visible = False
                                Dim heal1 = col.Find(Function(c) c.Id = "HEALING_CAP")
                                If heal1 IsNot Nothing Then heal.Visible = False
                                Dim abut1 = col.Find(Function(c) c.Id = "ABUTMENT")
                                If abut1 IsNot Nothing Then abut.Visible = False
                            End If
                            Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                            If crownFill IsNot Nothing Then
                                crownFill.Visible = True
                                crownFill.Appearance.Normal.BorderThickness = 0
                                For Each t In trtsList.Where(Function(c) c.LVL = 3)
                                    Select Case t.Treat
                                        Case "METAL CROWN", "ZERCONIA CROWN", "PFM CROWN", "EMAX CROWN", "TEMP CROWN", "STAINLESS STEEL CROWN"
                                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    End Select
                                Next
                            End If
                        End If
#End Region
                    Case 7 'CROWN ON IMPLANT
                        'TOP VIEW
                        If svg.Name.Contains("Top") Then
                            For Each item As SvgImageItem In col
                                item.Visible = False
                            Next
                            Dim crownIMP = col.Find(Function(c) c.Id = "CROWN_FILL")
                            If crownIMP IsNot Nothing Then
                                crownIMP.Visible = True
                                crownIMP.Appearance.Normal.BorderThickness = 1
                                For Each tc In trtsList
                                    Select Case tc.Treat
                                        Case "METAL CROWN I", "ZERCONIA CROWN I", "PFM CROWN I", "EMAX CROWN I", "TEMP CROWN I", "STAINLESS STEEL CROWN I"
                                            If crownIMP IsNot Nothing Then crownIMP.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor)
                                    End Select
                                Next
                            End If
                            If baseTooth IsNot Nothing Then
                                baseTooth.Visible = True
                            End If
                        Else
                            ' Show implant always
                            Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                            If implant IsNot Nothing Then implant.Visible = True
                            Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                            If crownImg IsNot Nothing Then
                                crownImg.Visible = True
                            End If
                            Dim crownIMP = col.Find(Function(c) c.Id = "CROWN_FILL")
                            If crownIMP IsNot Nothing Then
                                crownIMP.Visible = True
                                crownIMP.Appearance.Normal.BorderThickness = 1
                                For Each tc In trtsList

                                    Select Case tc.Treat
                                        Case "METAL CROWN I", "ZERCONIA CROWN I", "PFM CROWN I", "EMAX CROWN I",
                                        "TEMP CROWN I", "STAINLESS STEEL CROWN I"
                                            crownIMP.Appearance.Normal.FillColor = ColorTranslator.FromHtml(tc.FillColor)
                                    End Select
                                Next
                            End If
                        End If
                    Case 8 ' BRIDGE  
                        ' 1. Handle bridge differently per view
                        If svg.Name.Contains("Top") Then
                            For Each item As SvgImageItem In col
                                item.Visible = False
                            Next
                            ' Top View - style the base tooth as crown
                            If baseTooth IsNot Nothing Then
                                baseTooth.Visible = True
                            End If
                            'Show BRIDGE Mark
                            Dim bridgMark = col.Find(Function(c) c.Id = "BR")
                            If bridgMark IsNot Nothing Then
                                bridgMark.Visible = True
                                bridgMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
                            End If
#Region "delete"
                        ElseIf svg.Name.Contains("Out") Then
                            ' 2. Always show CON if exists
                            If trtsList.Any(Function(t) t.PropertyName = "IMPLANT") Then
                                Dim IMPLANT = col.Find(Function(c) c.Id = "IMPLANT")
                                If IMPLANT IsNot Nothing Then IMPLANT.Visible = True
                                ' Out View - show dedicated crown layer
                                Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                                If crownImg IsNot Nothing Then
                                    crownImg.Visible = True
                                End If
                            ElseIf trtsList.Any(Function(t) t.PropertyName = "EXTRACTION") Then
                                Dim EXTRACTED = col.Find(Function(c) c.Id = "EXTRACTION")
                                If EXTRACTED IsNot Nothing Then EXTRACTED.Visible = True
                                ' Out View - show dedicated crown layer
                                Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                                If crownImg IsNot Nothing Then
                                    crownImg.Visible = True
                                End If
                            Else
                                baseTooth.Visible = True
                            End If
                            'Show BRIDGE Mark
                            Dim bridgMark = col.Find(Function(c) c.Id = "BR")
                            If bridgMark IsNot Nothing Then
                                bridgMark.Visible = True
                                bridgMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("BRIDGEMARK")
                            End If
                            ' Out View - show dedicated crown layer
                            Dim crownItem = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                            If crownItem IsNot Nothing Then
                                crownItem.Visible = True
                            End If
#End Region

                        End If
                        'Show Crown Fill
                        Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                        If crownFill IsNot Nothing Then
                            crownFill.Visible = True
                            For Each t In trtsList
                                Select Case t.Treat
                                    Case "METAL BRIDGE", "ZERCONIA BRIDGE", "PFM BRIDGE", "EMAX BRIDGE",
                                        "TEMP BRIDGE", "STAINLESS STEEL BRIDGE"
                                        If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                End Select
                            Next
                        End If



                End Select

            End If

        Next

    End Sub

    Public Sub LoadSnglMobile(patientId As Integer, toothNum As Byte)
        Me.PatientID = patientId
        ' Load patient data
        clsPatient = New Patient With {.PatientID = patientId}
        clsPatient = clsPatientData.Select_Record(clsPatient)

        If clsPatient IsNot Nothing Then
            PatientName = clsPatient.PatientName
            ' Load tooth checks for the patient

            ' --- Legacy single-tooth mobile from Patient_Mobile. ---
            'PatientMobiles = clsMobData.Select_BypID_tNum(New Patient_Mobile With {.PatientID = patientId, .ToothNum = toothNum})
            'LoadPatientSnglToothMobile(PatientMobiles)
            PatientTreats = clsToothTrtData.GetPatientToothMobTreats(patientId, toothNum)
            LoadSnglToothTreats(PatientTreats, isMobileChart:=True)
        Else
            Dim msgEng As String = "Patient not found."
            Dim msgAr As String = "المريض غير موجود."
            Dim msg As String = If(Eng, msgEng, msgAr)
            MessageBox.Show(msg)
        End If
    End Sub
    ' --- Legacy single-tooth renderer for Patient_Mobile (unused after migration). ---
    Public Sub LoadPatientSnglToothMobile(patientMobiles As IEnumerable(Of Patient_Mobile))
        ' If there are no treatments, reset visibility for all SvgImageBoxes EXTRACTION IMG
        If patientMobiles Is Nothing OrElse Not patientMobiles.Any() Then
            Dim col1 As SvgImageItemCollection = SvgSlctd.RootItems
            ' Reset visibility for all items
            For Each item As SvgImageItem In col1
                item.Visible = Not String.IsNullOrEmpty(item.Id) AndAlso item.Id.Contains("IMG") AndAlso item.Id <> "CROWN_IMG"
            Next
            Return
        End If
        Dim col As SvgImageItemCollection = SvgSlctd.RootItems
        For Each item As SvgImageItem In col
            item.Visible = False
        Next
        'For Each item As SvgImageItem In col
        '    item.Visible = Not String.IsNullOrEmpty(item.Id) AndAlso item.Id.Contains("IMG") AndAlso item.Id <> "CROWN_IMG"
        'Next

        Dim baseTooth = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG") AndAlso c.Id <> "CROWN_IMG")
        ' Get the treatments for the current tooth
        Dim toothNum As Byte = CByte(SvgSlctd.Tag)
        For Each trt As Patient_Mobile In patientMobiles

            Dim mobsList = patientMobiles.Where(Function(t) t.ToothNum = toothNum).ToList()
            If mobsList.Count = 0 Then
                ' No treatments for this tooth, just show base image
                If baseTooth IsNot Nothing Then baseTooth.Visible = True
                'Dim chTooth = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("CH"))
                'If chTooth IsNot Nothing Then chTooth.Visible = True
                Continue For
            End If
            Dim maxLevel As Integer = mobsList.Max(Function(t) t.LVL)
            ' Always show IMG placeholder if needed
            If baseTooth IsNot Nothing AndAlso maxLevel < 3 Then
                baseTooth.Visible = True
            End If

            Select Case maxLevel
                Case 0, 1, 2
                    For Each mob In mobsList
                        Dim item = col.Find(Function(c) c.Id = mob.PropertyName)
                        If mob.Treat.Contains("VENEERS") Then
                            Dim crownItem = col.Find(Function(c) c.Id = "CROWN")
                            If crownItem IsNot Nothing Then
                                crownItem.Visible = True
                                crownItem.Appearance.Normal.FillColor = GetTrtColor(mob.Treat, PatientID, toothNum)
                                crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(mob.BorderColor)
                            End If
                        Else
                            If item IsNot Nothing AndAlso Not item.Id = "BR" Then
                                item.Visible = True
                                item.Appearance.Normal.FillColor = GetTrtColor(mob.Treat, PatientID, toothNum)
                                item.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(mob.BorderColor)
                                item.Appearance.Normal.BorderThickness = mob.BorderThickness
                            End If
                        End If
                        If mob.Treat = "COMPLETE DENTURE" Then 'REMOVABLE PARTIAL DENTURE
                            If SvgSlctd.Name.Contains("Top") Then
                                ' Top View - style the base tooth as crown
                                If baseTooth IsNot Nothing Then
                                    baseTooth.Visible = True
                                    ' Apply crown styling (use first bridge treatment's color)
                                    Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                                    If crownFill IsNot Nothing Then crownFill.Visible = True
                                    Dim dentureItem = col.Find(Function(d) d.Id = mob.PropertyName)
                                    If dentureItem IsNot Nothing Then
                                        dentureItem.Visible = True
                                        dentureItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#9AF447D1")
                                    End If
                                End If
                            Else

                                'Dim dentureItem = col.Find(Function(d) d.Id = mob.PropertyName)
                                'If dentureItem IsNot Nothing Then
                                '    dentureItem.Visible = True
                                '    dentureItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#9AF447D1")
                                'End If
                                baseTooth.Visible = False
                                Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                                If extracted IsNot Nothing Then
                                    extracted.Visible = True
                                    extracted.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#9AF447D1")
                                End If
                                Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG")
                                If crownImg IsNot Nothing Then crownImg.Visible = True
                                Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                                If crownFill IsNot Nothing Then crownFill.Visible = True
                                For Each t In mobsList
                                    Select Case t.Treat
                                        Case "METAL CROWN"
                                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        Case "ZERCONIA CROWN"
                                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        Case "PFM CROWN"
                                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        Case "EMAX CROWN"
                                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        Case "TEMP CROWN"
                                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                        Case "STAINLESS STEEL CROWN"
                                            If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                    End Select
                                Next
                            End If
                        ElseIf mob.Treat = "REMOVABLE PARTIAL DENTURE" Then 'REMOVABLE PARTIAL DENTURE
                            If SvgSlctd.Name.Contains("Top") Then
                                ' Top View - style the base tooth as crown
                                If baseTooth IsNot Nothing Then
                                    baseTooth.Visible = True
                                    ' Apply crown styling (use first bridge treatment's color)
                                    Dim topCrownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                                    If topCrownFill IsNot Nothing Then topCrownFill.Visible = True
                                End If

                                'Dim dentureItem = col.Find(Function(d) d.Id = mob.PropertyName)
                                'If dentureItem IsNot Nothing Then
                                '    dentureItem.Visible = True
                                '    dentureItem.Appearance.Normal.FillColor = Color.Indigo
                                'End If
                            Else
                                baseTooth.Visible = False
                                Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                                If extracted IsNot Nothing Then
                                    extracted.Visible = True
                                    extracted.Appearance.Normal.FillColor = ColorTranslator.FromHtml("#80F447D1")
                                End If
                                Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG")
                                If crownImg IsNot Nothing Then crownImg.Visible = True
                                Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                                If crownFill IsNot Nothing Then crownFill.Visible = True

                            End If
                        End If



                        If mob.LVL = 1 AndAlso mob.Treat = "APICECTOMY" Then
                            Dim apic = col.Find(Function(c) c.Id = "APICECTOMY")
                            If apic IsNot Nothing Then apic.Visible = True
                        End If

                        If mob.LVL = 2 AndAlso mob.Treat = "HEMISECTION" Then
                            Dim hemi = col.Find(Function(c) c.Id = "HEMISECTION")
                            If hemi IsNot Nothing Then hemi.Visible = True
                        End If

                    Next
                Case 3 ' EXTRACTED
                    Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                    If extracted IsNot Nothing Then extracted.Visible = True

                Case 4 ' Implant
                    Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                    If implant IsNot Nothing Then implant.Visible = True

                Case 5 ' Implant + stages
                    ' Show implant always
                    Dim implant = col.Find(Function(c) c.Id = "IMPLANT")
                    If implant IsNot Nothing Then implant.Visible = True

                    ' Check and show Healing Cap
                    If mobsList.Any(Function(t) t.PropertyName = "HEALING_CAP") Then
                        Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                        If heal IsNot Nothing Then heal.Visible = True
                    End If

                    ' Check and show Abutment
                    If mobsList.Any(Function(t) t.PropertyName = "ABUTMENT") Then
                        Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                        If abut IsNot Nothing Then abut.Visible = True
                    End If

                    ' Show final crown and hide Healing/Abutment if present
                    If mobsList.Any(Function(t) t.PropertyName = "CROWN_IMG") Then
                        Dim crownImg = col.Find(Function(c) c.Id = "CROWN_IMG")
                        If crownImg IsNot Nothing Then crownImg.Visible = True

                        Dim heal = col.Find(Function(c) c.Id = "HEALING_CAP")
                        If heal IsNot Nothing Then heal.Visible = False

                        Dim abut = col.Find(Function(c) c.Id = "ABUTMENT")
                        If abut IsNot Nothing Then abut.Visible = False

                        ' Optional: extra visual crown
                        If SvgSlctd.Name.Contains("Top") Then
                            ' When crown is final, hide implant and base layers to avoid visual overlap

                            ' Make sure base image is shown if needed
                            Dim topBase = col.Find(Function(c) c.Id IsNot Nothing AndAlso c.Id.Contains("IMG"))
                            If topBase IsNot Nothing Then topBase.Visible = True
                            ' Hide implant layer
                            Dim implantLayer = col.Find(Function(c) c.Id = "IMPLANT")
                            If implantLayer IsNot Nothing Then implantLayer.Visible = False
                            Dim heal1 = col.Find(Function(c) c.Id = "HEALING_CAP")
                            If heal1 IsNot Nothing Then heal.Visible = False

                            Dim abut1 = col.Find(Function(c) c.Id = "ABUTMENT")
                            If abut1 IsNot Nothing Then abut.Visible = False
                            ' Optional top view crown

                            Dim crownTop = col.Find(Function(c) c.Id = "CROWN")
                            If crownTop IsNot Nothing Then
                                crownTop.Visible = True
                                Dim t = mobsList.FirstOrDefault(Function(x) x.PropertyName = "CROWN")
                                If t IsNot Nothing Then
                                    crownTop.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                End If
                            End If

                        End If

                        Dim crownFill = col.Find(Function(c) c.Id = "CROWN_FILL")
                        If crownFill IsNot Nothing Then crownFill.Visible = True
                        For Each t In mobsList
                            Select Case t.Treat
                                Case "METAL CROWN"
                                    If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                Case "ZERCONIA CROWN"
                                    If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                Case "PFM CROWN"
                                    If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                Case "EMAX CROWN"
                                    If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                Case "TEMP CROWN"
                                    If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                                Case "STAINLESS STEEL CROWN"
                                    If crownFill IsNot Nothing Then crownFill.Appearance.Normal.FillColor = ColorTranslator.FromHtml(t.FillColor)
                            End Select
                        Next


                    End If
                Case 6   'CROWN
                    ' 1. Handle CROWN differently per view
                    If SvgSlctd.Name.Contains("Top") Then
                        ' Top View - style the base tooth as crown
                        If baseTooth IsNot Nothing Then
                            baseTooth.Visible = True
                            ' Apply crown styling (use first bridge treatment's color)
                            Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 6)
                            'If bridgeTrt IsNot Nothing Then
                            '    baseTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(bridgeTrt.FillColor)
                            '    baseTooth.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                            'End If
                        End If
                    ElseIf SvgSlctd.Name.Contains("Out") OrElse SvgSlctd.Name.Contains("IN") Then
                        ' 2. Always show extraction if exists
                        Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                        If extracted IsNot Nothing Then extracted.Visible = True
                        ' Out View - show dedicated crown layer
                        Dim crownItem = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                        If crownItem IsNot Nothing Then
                            crownItem.Visible = True
                            ' Apply styling
                            Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 6)
                            'If bridgeTrt IsNot Nothing Then
                            '    crownItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(bridgeTrt.FillColor)
                            '    crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                            'End If
                        End If
                    End If

                Case 7 ' BRIDGE  
                    ' 1. Handle bridge differently per view
                    If SvgSlctd.Name.Contains("Top") Then
                        ' Top View - style the base tooth as crown
                        If baseTooth IsNot Nothing Then
                            baseTooth.Visible = True
                            ' Apply crown styling (use first bridge treatment's color)
                            Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 7)
                            If bridgeTrt IsNot Nothing Then
                                baseTooth.Appearance.Normal.FillColor = ColorTranslator.FromHtml(bridgeTrt.FillColor)
                                baseTooth.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                            End If
                        End If
                    ElseIf SvgSlctd.Name.Contains("Out") OrElse SvgSlctd.Name.Contains("IN") Then
                        '' 2. Always show extraction if exists
                        Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                        If extracted IsNot Nothing Then extracted.Visible = True
                        ' Out View - show dedicated crown layer
                        Dim crownItem = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                        If crownItem IsNot Nothing Then
                            crownItem.Visible = True
                            ' Apply styling
                            Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 7)
                            If bridgeTrt IsNot Nothing Then
                                crownItem.Appearance.Normal.FillColor = ColorTranslator.FromHtml(bridgeTrt.FillColor)
                                crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                            End If
                        End If
                    End If

                Case 8 ' DENTURE 
                    If Source = "Mobile" Then
                        ' 1. Handle bridge differently per view
                        If SvgSlctd.Name.Contains("Top") Then
                            ' Top View - style the base tooth as crown
                            If baseTooth IsNot Nothing Then
                                baseTooth.Visible = True
                                'Show Denture Mark
                                Dim dentMark = col.Find(Function(c) c.Id = "CH")
                                If dentMark IsNot Nothing Then
                                    dentMark.Visible = True
                                    dentMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("DENTUREMARK")
                                End If
                                ' Apply crown styling (use first bridge treatment's color)
                                Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 8)
                                If bridgeTrt IsNot Nothing Then
                                    baseTooth.Appearance.Normal.FillColor = GetCustomTrtColor(bridgeTrt.Treat) ' ColorTranslator.FromHtml(bridgeTrt.FillColor)
                                    baseTooth.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                                End If
                            End If
                        ElseIf SvgSlctd.Name.Contains("Out") OrElse SvgSlctd.Name.Contains("IN") Then
                            '' 2. Always show extraction if exists
                            Dim extracted = col.Find(Function(c) c.Id = "EXTRACTED")
                            If extracted IsNot Nothing Then extracted.Visible = False
                            'Show Denture Mark
                            Dim dentMark = col.Find(Function(c) c.Id = "CH")
                            If dentMark IsNot Nothing Then
                                dentMark.Visible = True
                                dentMark.Appearance.Normal.FillColor = GetCutomTrtColorByProp("DENTUREMARK")
                            End If
                            ' Out View - show dedicated crown layer
                            Dim crownItem = col.Find(Function(c) c.Id = "CROWN_IMG") ' Fixed typo
                            If crownItem IsNot Nothing Then
                                crownItem.Visible = True
                                ' Apply styling
                                Dim bridgeTrt = mobsList.FirstOrDefault(Function(t) t.LVL = 8)
                                If bridgeTrt IsNot Nothing Then
                                    crownItem.Appearance.Normal.FillColor = GetCustomTrtColor(bridgeTrt.Treat) '  ColorTranslator.FromHtml(bridgeTrt.FillColor)
                                    crownItem.Appearance.Normal.BorderColor = ColorTranslator.FromHtml(bridgeTrt.BorderColor)
                                End If
                            End If
                        End If
                    End If

            End Select
        Next
    End Sub

    '==============================================

    Private toothNum As Byte
    Private isKid As Boolean = False
    Private recievedSVG As SvgImageBox
    Private recievedPatientID As Integer
    Dim Loaded As Boolean = False
    Dim Source As String = ""
    Public Sub ShowSlctdToothNew(ByVal patientid As Integer, ByVal svg As SvgImageBox, ByVal fromSource As String)
        Source = fromSource
        clsPatient = clsPatientData.Select_Record(New Patient With {.PatientID = patientid})
        Me.PatientID = patientid
        Me.PatientName = If(clsPatient IsNot Nothing, clsPatient.PatientName, String.Empty)
        recievedPatientID = patientid
        recievedSVG = svg
        ClickedSVG = svg.Name
        isDoubleClick = True
        Dim fdiOpened = ToothSvgQuadrantNaming.TryParseFdiToothTag(svg.Tag)
        If ToothSvgQuadrantNaming.IsKidJawSvgControlName(svg.Name) Then
            isKid = True
            SetkIDSvgsNew(svg.Name, fdiOpened, patientid) ', treats)
            UpdateKIDToothDisplay()
        Else
            isKid = False
            SetSvgsNew(svg.Name, fdiOpened, patientid) ', treats)
            UpdateToothDisplay()
        End If

        '========================
        ' Refresh the control to apply visibility changes

        lblPatientName.Text = PatientName
        lblToothName.Text = GetToothHeadingLabel(isKid, fdiOpened)
        lblView.Text = GetViewAngleLabel(svg.Name)
        If SvgSlctd.SvgImage Is Nothing Then
            If isKid Then
                KidResourcesApplySelectedToothPreview(svg.Name, fdiOpened, patientid)
            Else
                AdultResourcesApplySelectedToothPreview(svg.Name, fdiOpened, patientid)
            End If
        End If
        If SvgSlctd.SvgImage Is Nothing AndAlso svg IsNot Nothing AndAlso svg.SvgImage IsNot Nothing Then
            SvgSlctd.Name = svg.Name
            SvgSlctd.Tag = svg.Tag
            SvgSlctd.SvgImage = svg.SvgImage
            ApplyTagAndVisibility(SvgSlctd, fdiOpened)
            Select Case Source
                Case "Treat"
                    LoadSnglTreat(patientid, If(fdiOpened > 0 AndAlso fdiOpened <= 255, CByte(fdiOpened), CByte(0)))
                Case "Mobile"
                    LoadSnglMobile(patientid, If(fdiOpened > 0 AndAlso fdiOpened <= 255, CByte(fdiOpened), CByte(0)))
            End Select
        End If

        SvOut.Refresh()
        SvIN.Refresh()
        SvTop.Refresh()
        Loaded = True
    End Sub



    Public Sub ShowToothNew(ByVal patientid As Integer, ByVal svg As SvgImageBox, ByVal tag As Integer)

        GetSvgsNew(svg.Name, tag, patientid)

        '========================
        ' Refresh the control to apply visibility changes
        SvOut.Refresh()
        SvIN.Refresh()
        SvTop.Refresh()


    End Sub



    Private isDoubleClick As Boolean = False
    Private ClickedSVG As String = ""

    Private Sub SvgOut_DoubleClick(sender As Object, e As EventArgs) Handles SvOut.DoubleClick, SvIN.DoubleClick, SvTop.DoubleClick
        SvgSlctd.SvgImage = Nothing
        Dim svg As SvgImageBox = DirectCast(sender, SvgImageBox)
        ClickedSVG = svg.Name
        isDoubleClick = True
        'SvgSlctd.SvgImage = svg.SvgImage
        SvgSlctd.Tag = svg.Tag
        Dim fdi = ToothSvgQuadrantNaming.TryParseFdiToothTag(svg.Tag)
        lblToothName.Text = GetToothHeadingLabel(isKid, fdi)
        lblView.Text = GetViewAngleLabel(svg.Name)
        SvgSlctd.Hide()
        SvgSlctd.Name = svg.Name
        SvgSlctd.SvgImage = svg.SvgImage
        LoadSnglTreat(PatientID, If(fdi > 0 AndAlso fdi <= 255, CByte(fdi), CByte(0)))
        SvgSlctd.Refresh()
        SvgSlctd.Show()

    End Sub


    ' Navigation handlers
    Private Sub btnPrevT_Click(sender As Object, e As EventArgs) Handles btnPrevT.Click
        If isKid Then
            ' Move to the previous index, wrap around if necessary
            currentKIDIndex = (currentKIDIndex - 1 + KIDtoothNames.Count) Mod KIDtoothNames.Count
            UpdateKIDToothDisplay()
        Else
            ' Move to the previous index, wrap around if necessary
            currentIndex = (currentIndex - 1 + toothNames.Count) Mod toothNames.Count
            UpdateToothDisplay()
        End If


    End Sub

    Private Sub btnNextT_Click(sender As Object, e As EventArgs) Handles btnNextT.Click

        If isKid Then
            ' Move to the next index, wrap around if necessary
            currentKIDIndex = (currentKIDIndex + 1) Mod KIDtoothNames.Count
            UpdateKIDToothDisplay()
        Else
            ' Move to the next index, wrap around if necessary
            currentIndex = (currentIndex + 1) Mod toothNames.Count
            UpdateToothDisplay()
        End If
    End Sub

#Region "Methods"


    Public Function ExtractView(inputString As String) As String
        If String.IsNullOrEmpty(inputString) Then Return String.Empty

        Dim pattern As String = "(Out|IN|Top)"
        Dim match As Match = Regex.Match(inputString, pattern, RegexOptions.IgnoreCase)

        If match.Success Then
            Return match.Value.ToLower()
        End If

        Return String.Empty
    End Function

    ' Method to update and display the current tooth
    Private Sub UpdateToothDisplay()
        If currentIndex >= 0 AndAlso currentIndex < toothNames.Count Then
            HideSvg()
            Dim currentTooth As String = toothNames(currentIndex)
            SetSvgsNew(currentTooth, GetTag2(currentTooth), PatientID)
            lblToothName.Text = GetToothHeadingLabel(False, GetTag2(currentTooth))
            lblPatientName.Text = PatientName
            If isDoubleClick AndAlso Not String.IsNullOrEmpty(ClickedSVG) Then
                SetSvgSlctd(ClickedSVG, GetTag2(currentTooth), PatientID)
                lblView.Text = GetViewAngleLabel(ClickedSVG)
            End If
            SvgSlctd.Refresh()
            ShowSvg()
        End If
    End Sub

    Public Sub GetSvgsNew(name As String, tag As Integer, patientId As Integer)
        ' Extract the base key
        Dim baseKey As String = name.Replace("Out", "").Replace("Top", "").Replace("IN", "")
        currentIndex = GetToothIndex(baseKey)
        If AdultResourceMapping.ContainsKey(baseKey) Then
            Dim resources = AdultResourceMapping(baseKey)

            ' Determine left/right behavior and assign resources accordingly
            Dim isLeftOrRight As Boolean = baseKey.StartsWith("Ld") OrElse baseKey.StartsWith("Rd")
            SvOut.SvgImage = If(isLeftOrRight, resources.SvgInResource, resources.SvgOutResource)
            SvIN.SvgImage = If(isLeftOrRight, resources.SvgOutResource, resources.SvgInResource)
            SvTop.SvgImage = resources.SvgTopResource

            ' Set tags and visibility for each SvgImageBox
            For Each svgBox As SvgImageBox In {SvOut, SvIN, SvTop}
                ApplyTagAndVisibility(svgBox, tag)
            Next

        End If
    End Sub

    Public Sub GetKidSvgsNew(name As String, tag As Integer, patientId As Integer)
        ' Extract the base key
        Dim baseKey As String = name.Replace("OUTK", "").Replace("TOPK", "").Replace("INK", "")
        currentIndex = GetToothIndex(baseKey)
        If KidResourceMapping.ContainsKey(baseKey) Then
            Dim resources = KidResourceMapping(baseKey)

            ' Determine left/right behavior and assign resources accordingly
            Dim isLeftOrRight As Boolean = baseKey.StartsWith("LD") OrElse baseKey.StartsWith("RD")
            SvOut.SvgImage = If(isLeftOrRight, resources.SvgInResource, resources.SvgOutResource)
            SvIN.SvgImage = If(isLeftOrRight, resources.SvgOutResource, resources.SvgInResource)
            SvTop.SvgImage = resources.SvgTopResource

            ' Set tags and visibility for each SvgImageBox
            For Each svgBox As SvgImageBox In {SvOut, SvIN, SvTop}
                ApplyTagAndVisibility(svgBox, tag)
            Next

        End If
    End Sub
    ' Method to fetch resources and manage visibility
    Public Sub SetSvgsNew(name As String, tag As Integer, patientId As Integer) ', treats As IEnumerable(Of Patient_ToothTrt)
        ' Extract the base key
        Dim baseKey As String = name.Replace("Out", "").Replace("Top", "").Replace("IN", "")
        currentIndex = GetToothIndex(baseKey)
        Dim resourceMap = Helpers.AdultResourceMapping
        If resourceMap.ContainsKey(baseKey) Then
            Dim resources = resourceMap(baseKey)


            ' Determine left/right behavior and assign resources accordingly
            Dim isLeftOrRight As Boolean = baseKey.StartsWith("Ld") OrElse baseKey.StartsWith("Rd")
            SvOut.SvgImage = If(isLeftOrRight, resources.SvgInResource, resources.SvgOutResource)
            SvIN.SvgImage = If(isLeftOrRight, resources.SvgOutResource, resources.SvgInResource)
            SvTop.SvgImage = resources.SvgTopResource

            ' Set tags and visibility for each SvgImageBox
            For Each svgBox As SvgImageBox In {SvOut, SvIN, SvTop}
                ApplyTagAndVisibility(svgBox, tag)
            Next

            Select Case Source
                Case "Treat"
                    LoadTreat(patientId, CByte(tag))
                Case "Mobile"
                    LoadTreat(patientId, CByte(tag))
            End Select
            ' Corrected Select Case
            Select Case True
                Case name.Contains("Out")
                    ClickedSVG = name
                    isDoubleClick = True
                    SetSvgSlctd(name, CInt(tag), patientId)
                    'SvgSlctd.SvgImage = SvgOut.SvgImage
                Case name.Contains("Top")
                    ClickedSVG = name
                    isDoubleClick = True
                    SetSvgSlctd(name, CInt(tag), patientId)
                Case name.Contains("IN")
                    ClickedSVG = name
                    isDoubleClick = True
                    SetSvgSlctd(name, CInt(tag), patientId)
            End Select
        End If
    End Sub


    ' Method to fetch resources and manage visibility
    Public Sub SetSvgSlctd(name As String, tag As Integer, patientId As Integer) ', treats As IEnumerable(Of Patient_ToothTrt)
        Dim baseKey As String = GetToothPosition(CByte(tag))
        If baseKey = "Invalid" Then Return
        currentIndex = GetToothIndex(baseKey)
        Dim resourceMap = Helpers.AdultResourceMapping
        If Not resourceMap.ContainsKey(baseKey) Then Return

        SvgSlctd.Name = name
        SvgSlctd.Tag = tag

        Dim srcBox = GetSlctdSourceBoxForClickedView(name)
        If srcBox IsNot Nothing AndAlso srcBox.SvgImage IsNot Nothing Then
            SvgSlctd.SvgImage = srcBox.SvgImage
        Else
            Dim resources = resourceMap(baseKey)
            Dim isLeftOrRight As Boolean = baseKey.StartsWith("Ld") OrElse baseKey.StartsWith("Rd")
            If name.IndexOf("Top", StringComparison.OrdinalIgnoreCase) >= 0 Then
                SvgSlctd.SvgImage = resources.SvgTopResource
            ElseIf name.IndexOf("IN", StringComparison.OrdinalIgnoreCase) >= 0 Then
                SvgSlctd.SvgImage = resources.SvgInResource
            ElseIf name.IndexOf("Out", StringComparison.OrdinalIgnoreCase) >= 0 Then
                SvgSlctd.SvgImage = If(isLeftOrRight, resources.SvgInResource, resources.SvgOutResource)
            Else
                SvgSlctd.SvgImage = If(isLeftOrRight, resources.SvgInResource, resources.SvgOutResource)
            End If
        End If

        ApplyTagAndVisibility(SvgSlctd, tag)
        Select Case Source
            Case "Treat"
                LoadSnglTreat(patientId, CByte(tag))
            Case "Mobile"
                LoadSnglMobile(patientId, CByte(tag))
        End Select
    End Sub

    ''' <summary>Re-apply single-tooth SVG + treatments when adult resources were not ready on the first SetSvgsNew pass.</summary>
    Private Sub AdultResourcesApplySelectedToothPreview(name As String, tag As Integer, patientId As Integer)
        Dim baseKey As String = name.Replace("Out", "").Replace("Top", "").Replace("IN", "")
        If Helpers.AdultResourceMapping.ContainsKey(baseKey) Then
            SetSvgSlctd(name, tag, patientId)
        End If
    End Sub

    ''' <summary>Re-apply single-tooth SVG + treatments when kid resources were not ready on the first SetkIDSvgsNew pass.</summary>
    Private Sub KidResourcesApplySelectedToothPreview(name As String, tag As Integer, patientId As Integer)
        Dim baseKey As String = name.Replace("OUTK", "").Replace("TOPK", "").Replace("INK", "")
        If Helpers.KidResourceMapping.ContainsKey(baseKey) Then
            SetKIDSvgSlctd(name, tag, patientId)
        End If
    End Sub

#End Region

#Region "KID Helper Methods"

    ' Method to fetch resources and manage visibility
    Public Sub SetkIDSvgsNew(name As String, tag As Integer, patientId As Integer) ', treats As IEnumerable(Of Patient_ToothTrt)
        ' Extract the base key
        Dim baseKey As String = name.Replace("OUTK", "").Replace("TOPK", "").Replace("INK", "")
        currentKIDIndex = GetKIDToothIndex(baseKey)
        Dim resourceMap = Helpers.KidResourceMapping
        If resourceMap.ContainsKey(baseKey) Then
            Dim resources = resourceMap(baseKey)

            ' Determine left/right behavior and assign resources accordingly
            Dim isLeftOrRight As Boolean = baseKey.StartsWith("LD") OrElse baseKey.StartsWith("RD")
            SvOut.SvgImage = If(isLeftOrRight, resources.SvgInResource, resources.SvgOutResource)
            SvIN.SvgImage = If(isLeftOrRight, resources.SvgOutResource, resources.SvgInResource)
            SvTop.SvgImage = resources.SvgTopResource

            ' Set tags and visibility for each SvgImageBox
            For Each svgBox As SvgImageBox In {SvOut, SvIN, SvTop}
                ApplyTagAndVisibility(svgBox, tag)
            Next
            LoadTreat(patientId, CByte(tag))


            ' Corrected Select Case
            Select Case True
                Case name.Contains("OUT")
                    SetKIDSvgSlctd(name, CInt(tag), patientId)
                    'SvgSlctd.SvgImage = SvgOut.SvgImage
                Case name.Contains("TOP")
                    SetKIDSvgSlctd(name, CInt(tag), patientId)
                Case name.Contains("IN")
                    SetKIDSvgSlctd(name, CInt(tag), patientId)
            End Select
        End If
    End Sub

    ' Method to fetch resources and manage visibility
    Public Sub SetKIDSvgSlctd(name As String, tag As Integer, patientId As Integer) ', treats As IEnumerable(Of Patient_ToothTrt)
        isDoubleClick = True
        ClickedSVG = name
        Dim baseKey As String = GetKIDToothPosition(CByte(tag))
        If baseKey = "Invalid" Then Return
        currentKIDIndex = GetKIDToothIndex(baseKey)
        Dim resourceMap = Helpers.KidResourceMapping
        If Not resourceMap.ContainsKey(baseKey) Then Return

        SvgSlctd.Name = name
        SvgSlctd.Tag = tag

        Dim srcBox = GetSlctdSourceBoxForClickedView(name)
        If srcBox IsNot Nothing AndAlso srcBox.SvgImage IsNot Nothing Then
            SvgSlctd.SvgImage = srcBox.SvgImage
        Else
            Dim resources = resourceMap(baseKey)
            Dim isLeftOrRight As Boolean = baseKey.StartsWith("LD") OrElse baseKey.StartsWith("RD")
            If name.IndexOf("TOP", StringComparison.OrdinalIgnoreCase) >= 0 Then
                SvgSlctd.SvgImage = resources.SvgTopResource
            ElseIf name.IndexOf("INK", StringComparison.OrdinalIgnoreCase) >= 0 OrElse name.IndexOf("IN", StringComparison.OrdinalIgnoreCase) >= 0 Then
                SvgSlctd.SvgImage = resources.SvgInResource
            ElseIf name.IndexOf("OUT", StringComparison.OrdinalIgnoreCase) >= 0 Then
                SvgSlctd.SvgImage = If(isLeftOrRight, resources.SvgInResource, resources.SvgOutResource)
            Else
                SvgSlctd.SvgImage = If(isLeftOrRight, resources.SvgInResource, resources.SvgOutResource)
            End If
        End If

        ApplyTagAndVisibility(SvgSlctd, tag)
        Select Case Source
            Case "Treat"
                LoadSnglTreat(patientId, CByte(tag))
            Case "Mobile"
                LoadSnglMobile(patientId, CByte(tag))
        End Select
    End Sub

    Function GetKIDToothPosition(tag As Byte) As String
        Select Case tag
            Case 51 To 55
                Return "RU" & (tag - 50)
            Case 61 To 65
                Return "LU" & (tag - 60)
            Case 71 To 75
                Return "LD" & (tag - 70)
            Case 81 To 85
                Return "RD" & (tag - 80)
            Case Else
                Return "Invalid"
        End Select
    End Function


    ' Track the current index
    Private currentKIDIndex As Integer = 0
    ' Define a list of tooth names in the desired order
    Private KIDtoothNames As List(Of String) = New List(Of String) From {
        "RU5", "RU4", "RU3", "RU2", "RU1",
        "LU1", "LU2", "LU3", "LU4", "LU5",
        "LD5", "LD4", "LD3", "LD2", "LD1",
        "RD1", "RD2", "RD3", "RD4", "RD5"
    }

    ' Helper method to get tag mapping
    Private Function GetKIDTag2(ByVal currentTooth As String) As Integer
        Dim toothMapping As New Dictionary(Of String, Integer) From {
        {"LD1", 71}, {"LD2", 72}, {"LD3", 73}, {"LD4", 74}, {"LD5", 75},
        {"LU1", 61}, {"LU2", 62}, {"LU3", 63}, {"LU4", 64}, {"LU5", 65},
        {"RD1", 81}, {"RD2", 82}, {"RD3", 83}, {"RD4", 84}, {"RD5", 85},
        {"RU1", 51}, {"RU2", 52}, {"RU3", 53}, {"RU4", 54}, {"RU5", 55}
    }

        Dim value As Integer
        If toothMapping.TryGetValue(currentTooth, value) Then
            Return value
        End If

        Return 0 ' Default value if the tooth is not found
    End Function

    ' Helper method to get Index mapping
    Private Function GetKIDToothIndex(ByVal currentTooth As String) As Integer
        Dim toothMapping As New Dictionary(Of String, Integer) From {
        {"LD1", 14}, {"LD2", 13}, {"LD3", 12}, {"LD4", 11}, {"LD5", 10},
        {"LU1", 5}, {"LU2", 6}, {"LU3", 7}, {"LU4", 8}, {"LU5", 9},
        {"RD1", 15}, {"RD2", 16}, {"RD3", 17}, {"RD4", 18}, {"RD5", 19},
        {"RU1", 4}, {"RU2", 3}, {"RU3", 2}, {"RU4", 1}, {"RU5", 0}
    }

        Dim value As Integer
        If toothMapping.TryGetValue(currentTooth, value) Then
            Return value
        End If

        Return -1 ' Default value if the tooth is not found
    End Function


    Private Function GetKIDToothNameByNum(ByVal currentTooth As Integer) As String
        Dim abbrev = GetKIDToothPosition(CByte(currentTooth))
        If abbrev = "Invalid" Then Return ""
        Return GridHelper.GetToothFullName(abbrev, TreatsUserControl.AlternateQuadrantLabelsEnabled)
    End Function

    ' Method to update and display the current tooth
    Private Sub UpdateKIDToothDisplay()
        If currentKIDIndex >= 0 AndAlso currentKIDIndex < KIDtoothNames.Count Then
            HideSvg()
            Dim currentTooth As String = KIDtoothNames(currentKIDIndex)
            SetkIDSvgsNew(currentTooth, GetKIDTag2(currentTooth), PatientID)
            lblToothName.Text = GetToothHeadingLabel(True, GetKIDTag2(currentTooth))
            lblPatientName.Text = PatientName
            If isDoubleClick AndAlso Not String.IsNullOrEmpty(ClickedSVG) Then
                SetKIDSvgSlctd(ClickedSVG, GetKIDTag2(currentTooth), PatientID)
                lblView.Text = GetViewAngleLabel(ClickedSVG)
            End If
            SvgSlctd.Refresh()
            ShowSvg()
        End If
    End Sub

#End Region

#Region "Helper Methods"

    ''' <summary>Rewrites legacy stored tooth titles (RIGHT UPPER …) to alternate order when New Naming is enabled.</summary>
    Private Function FormatStoredToothNameForDisplay(storedToothName As String) As String
        If String.IsNullOrWhiteSpace(storedToothName) OrElse Not TreatsUserControl.AlternateQuadrantLabelsEnabled Then
            Return If(storedToothName, String.Empty)
        End If
        Dim t = storedToothName.Trim()
        Const pRu As String = "RIGHT UPPER "
        Const pLu As String = "LEFT UPPER "
        Const pRdLower As String = "RIGHT LOWER "
        Const pLdLower As String = "LEFT LOWER "
        Const pRdDown As String = "RIGHT DOWN "
        Const pLdDown As String = "LEFT DOWN "
        If t.StartsWith(pRu, StringComparison.OrdinalIgnoreCase) Then Return "UPPER RIGHT " & t.Substring(pRu.Length)
        If t.StartsWith(pLu, StringComparison.OrdinalIgnoreCase) Then Return "UPPER LEFT " & t.Substring(pLu.Length)
        If t.StartsWith(pRdLower, StringComparison.OrdinalIgnoreCase) Then Return "LOWER RIGHT " & t.Substring(pRdLower.Length)
        If t.StartsWith(pLdLower, StringComparison.OrdinalIgnoreCase) Then Return "LOWER LEFT " & t.Substring(pLdLower.Length)
        If t.StartsWith(pRdDown, StringComparison.OrdinalIgnoreCase) Then Return "LOWER RIGHT " & t.Substring(pRdDown.Length)
        If t.StartsWith(pLdDown, StringComparison.OrdinalIgnoreCase) Then Return "LOWER LEFT " & t.Substring(pLdDown.Length)
        Return t
    End Function

    Function GetToothPosition(tag As Byte) As String
        Select Case tag
            Case 11 To 18
                Return "Ru" & (tag - 10)
            Case 21 To 28
                Return "Lu" & (tag - 20)
            Case 31 To 38
                Return "Ld" & (tag - 30)
            Case 41 To 48
                Return "Rd" & (tag - 40)
            Case Else
                Return "Invalid"
        End Select
    End Function


    ' Track the current index
    Private currentIndex As Integer = 0
    ' Define a list of tooth names in the desired order

    Private toothNames As List(Of String) = New List(Of String) From {
        "Ru8", "Ru7", "Ru6", "Ru5", "Ru4", "Ru3", "Ru2", "Ru1",
        "Lu1", "Lu2", "Lu3", "Lu4", "Lu5", "Lu6", "Lu7", "Lu8",
        "Ld8", "Ld7", "Ld6", "Ld5", "Ld4", "Ld3", "Ld2", "Ld1",
        "Rd1", "Rd2", "Rd3", "Rd4", "Rd5", "Rd6", "Rd7", "Rd8"
    }


    ' Helper methods to hide and show SVGs
    Private Sub HideSvg()
        SvOut.Hide()
        SvTop.Hide()
        SvIN.Hide()
        SvgSlctd.Hide()
    End Sub

    Private Sub ShowSvg()
        SvOut.Show()
        SvTop.Show()
        SvIN.Show()
        SvgSlctd.Show()
    End Sub
    ' Helper method to apply tag and toggle visibility
    Private Sub ApplyTagAndVisibility(svgBox As SvgImageBox, tag As Integer)
        svgBox.Tag = tag
        For Each item As SvgImageItem In svgBox.RootItems
            item.Visible = Not String.IsNullOrEmpty(item.Id) AndAlso item.Id.Contains("IMG") AndAlso item.Id <> "CROWN_IMG"
        Next
    End Sub

    ''' <summary>Human-readable tooth line for headers: GridHelper quadrant full name + FDI tooth number (ISO 3950), not the SVG control name.</summary>
    Private Function GetToothHeadingLabel(isKidJaw As Boolean, fdiToothTag As Integer) As String
        Return ToothSvgQuadrantNaming.GetToothHeadingLabelForFdi(isKidJaw, fdiToothTag)
    End Function

    ''' <summary>Maps the clicked jaw control name to the local preview box that already has the correct SVG (incl. Ld/Rd out/in swap).</summary>
    Private Function GetSlctdSourceBoxForClickedView(clickedControlName As String) As SvgImageBox
        If String.IsNullOrEmpty(clickedControlName) Then Return SvOut
        Dim n = clickedControlName
        If n.IndexOf("Top", StringComparison.OrdinalIgnoreCase) >= 0 OrElse n.IndexOf("TOP", StringComparison.Ordinal) >= 0 Then Return SvTop
        ' Kid inner: …INK5; adult inner: …IN8 — match before generic "Out" / "OUT"
        If n.IndexOf("INK", StringComparison.OrdinalIgnoreCase) >= 0 Then Return SvIN
        If n.IndexOf("IN", StringComparison.OrdinalIgnoreCase) >= 0 Then Return SvIN
        If n.IndexOf("Out", StringComparison.OrdinalIgnoreCase) >= 0 OrElse n.IndexOf("OUT", StringComparison.Ordinal) >= 0 Then Return SvOut
        Return SvOut
    End Function

    Private Function GetViewAngleLabel(clickedControlName As String) As String
        If String.IsNullOrEmpty(clickedControlName) Then Return String.Empty
        Dim n = clickedControlName
        If n.IndexOf("Top", StringComparison.OrdinalIgnoreCase) >= 0 OrElse n.IndexOf("TOP", StringComparison.Ordinal) >= 0 Then Return If(Eng, "Occlusal / top view", "منظر علوي")
        If n.IndexOf("INK", StringComparison.OrdinalIgnoreCase) >= 0 OrElse n.IndexOf("IN", StringComparison.OrdinalIgnoreCase) >= 0 Then Return If(Eng, "Inner (lingual/palatal) view", "منظر داخلي")
        If n.IndexOf("Out", StringComparison.OrdinalIgnoreCase) >= 0 OrElse n.IndexOf("OUT", StringComparison.Ordinal) >= 0 Then Return If(Eng, "Outer (buccal) view", "منظر خارجي")
        Return String.Empty
    End Function

    ' Helper method to get toothname (uses global alternate naming when enabled)
    Private Function GetToothFullName(ByVal toothname As String) As String
        Return GridHelper.GetToothFullName(toothname, TreatsUserControl.AlternateQuadrantLabelsEnabled)
    End Function

    ' Helper method to get tag mapping
    Private Function GetTag2(ByVal currentTooth As String) As Integer
        Dim toothMapping As New Dictionary(Of String, Integer) From {
        {"Ld1", 31}, {"Ld2", 32}, {"Ld3", 33}, {"Ld4", 34}, {"Ld5", 35}, {"Ld6", 36}, {"Ld7", 37}, {"Ld8", 38},
        {"Lu1", 21}, {"Lu2", 22}, {"Lu3", 23}, {"Lu4", 24}, {"Lu5", 25}, {"Lu6", 26}, {"Lu7", 27}, {"Lu8", 28},
        {"Rd1", 41}, {"Rd2", 42}, {"Rd3", 43}, {"Rd4", 44}, {"Rd5", 45}, {"Rd6", 46}, {"Rd7", 47}, {"Rd8", 48},
        {"Ru1", 11}, {"Ru2", 12}, {"Ru3", 13}, {"Ru4", 14}, {"Ru5", 15}, {"Ru6", 16}, {"Ru7", 17}, {"Ru8", 18}
    }

        Dim value As Integer
        If toothMapping.TryGetValue(currentTooth, value) Then
            Return value
        End If

        Return 0 ' Default value if the tooth is not found
    End Function


    ' Helper method to get Index mapping
    Private Function GetToothIndex(ByVal currentTooth As String) As Integer
        Dim toothMapping As New Dictionary(Of String, Integer) From {
        {"Ld1", 23}, {"Ld2", 22}, {"Ld3", 21}, {"Ld4", 20}, {"Ld5", 19}, {"Ld6", 18}, {"Ld7", 17}, {"Ld8", 16},
        {"Lu1", 8}, {"Lu2", 9}, {"Lu3", 10}, {"Lu4", 11}, {"Lu5", 12}, {"Lu6", 13}, {"Lu7", 14}, {"Lu8", 15},
        {"Rd1", 24}, {"Rd2", 25}, {"Rd3", 26}, {"Rd4", 27}, {"Rd5", 28}, {"Rd6", 29}, {"Rd7", 30}, {"Rd8", 31},
        {"Ru1", 7}, {"Ru2", 6}, {"Ru3", 5}, {"Ru4", 4}, {"Ru5", 3}, {"Ru6", 2}, {"Ru7", 1}, {"Ru8", 0}
    }

        Dim value As Integer
        If toothMapping.TryGetValue(currentTooth, value) Then
            Return value
        End If

        Return -1 ' Default value if the tooth is not found
    End Function


    Private Function GetToothNameByNum(ByVal currentTooth As Integer) As String
        Dim abbrev = GetToothPosition(CByte(currentTooth))
        If abbrev = "Invalid" Then Return ""
        Return GridHelper.GetToothFullName(abbrev.ToUpperInvariant(), TreatsUserControl.AlternateQuadrantLabelsEnabled)
    End Function


#End Region


    '1361, 761
    Private ReadOnly controlBoundsCache As New ConcurrentDictionary(Of Control, Rectangle)
    Private Const OriginalPanelWidth As Integer = 1214
    Private Const OriginalPanelHeight As Integer = 654

    Private Sub SlctdToothCTL_Load(sender As Object, e As EventArgs) Handles Me.Load
        ResizeControlsProportionally()
    End Sub
    Private Sub StoreOriginalBounds(container As Control)
        For Each ctrl As Control In container.Controls
            controlBoundsCache.TryAdd(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
    End Sub
    Private Sub ResizeControlsProportionally()
        If controlBoundsCache.IsEmpty Then Return

        Dim widthRatio = CSng(MainSplit.Width) / OriginalPanelWidth
        Dim heightRatio = CSng(MainSplit.Height) / OriginalPanelHeight

        For Each kvp In controlBoundsCache
            kvp.Key.SetBounds(
                CInt(kvp.Value.X * widthRatio),
                CInt(kvp.Value.Y * heightRatio),
                CInt(kvp.Value.Width * widthRatio),
                CInt(kvp.Value.Height * heightRatio))
        Next
    End Sub

    Private Sub SlctdToothCTL_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If controlBoundsCache.IsEmpty Then Return
        ResizeControlsProportionally()
    End Sub


    Private Sub btnNextT_StyleChanged(sender As Object, e As EventArgs) Handles btnNextT.StyleChanged
        If Loaded Then
            'ShowSlctdToothNew(recievedPatientID, recievedSVG)
            LoadTreat(recievedPatientID, CByte(recievedSVG.Tag))
            LoadSnglTreat(recievedPatientID, CByte(recievedSVG.Tag))
        End If
    End Sub

    Private Sub SlctdToothTab_SelectedPageChanged(sender As Object, e As TabPageChangedEventArgs) Handles SlctdToothTab.SelectedPageChanged
        'If e.Page.Name = "ToothTrtsPage" Then
        '    GridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full
        'End If
    End Sub
End Class
