Imports System.Data.SqlClient
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Public Class FrmDefColors

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal _Trt As String, ByVal _propName As String)
        ' This call is required by the designer.
        InitializeComponent()
        Me.Icon = AppIcon
        ' Add any initialization after the InitializeComponent() call.
        Treat = _Trt
        FilterTrtsTreeView(_Trt)
        txtSrchTrt.Text = _Trt
        ShowLayer(zSvg.RootItems, _propName)

        ' Select the node matching _Trt after the TreeView is populated



    End Sub

    ' Helper method to find and select a node by its text
    Private Sub SelectNodeByText(treeView As TreeView, textToFind As String)
        ' Search through all nodes recursively
        Dim foundNode As TreeNode = FindNodeByText(treeView.Nodes, textToFind)

        If foundNode IsNot Nothing Then
            treeView.SelectedNode = foundNode
            foundNode.EnsureVisible() ' Scroll to make the node visible
        End If
    End Sub

    Private Sub SelectNodeByTrt(treeView As TreeView, trtToFind As String)
        For Each groupNode As TreeNode In treeView.Nodes
            For Each trtNode As TreeNode In groupNode.Nodes
                Dim tag = TryCast(trtNode.Tag, Object)
                If tag IsNot Nothing AndAlso tag.Trt.ToString().Equals(trtToFind, StringComparison.OrdinalIgnoreCase) Then
                    treeView.SelectedNode = trtNode
                    trtNode.EnsureVisible()
                    Exit Sub
                End If
            Next
        Next
    End Sub
    ' Recursive method to find a node by its text
    Private Function FindNodeByText(nodes As TreeNodeCollection, textToFind As String) As TreeNode
        For Each node As TreeNode In nodes
            ' Check if this node matches
            If node.Text.Equals(textToFind, StringComparison.OrdinalIgnoreCase) Then
                Return node
            End If

            ' Check child nodes recursively
            Dim foundChildNode As TreeNode = FindNodeByText(node.Nodes, textToFind)
            If foundChildNode IsNot Nothing Then
                Return foundChildNode
            End If
        Next

        Return Nothing
    End Function
    Private Sub FrmDefColors_Load(sender As Object, e As EventArgs) Handles Me.Load
        HideLayers()
        SetTrtsTreeViewDataSource()
        If Not String.IsNullOrEmpty(Treat) Then
            SelectNodeByText(TrtsTreeView, Treat)
        End If
    End Sub


    Private Sub HideLayers()

        Dim col As SvgImageItemCollection = zSvg.RootItems

        ' Now set visibility only for items with a tag containing "IMG"
        For Each item As SvgImageItem In col 'sv.CustomizedItems
            item.Visible = False
        Next
    End Sub
    'Private slctdFillClr As Color
    'Private slctdBrdrClr As Color
    'Private slctdBrdThick As Int16
    Private propertyName As String
    Private clsTblTrtDATA As New TblTRTSDATA
    Private newTrt As New TblTRTS
    Private oldTrt As New TblTRTS

    Function ColorToHex(ByVal clr As Color) As String
        ' Set alpha to 50% (0x80)
        Dim alpha As Byte = &H80
        Dim red As Byte = clr.R
        Dim green As Byte = clr.G
        Dim blue As Byte = clr.B

        ' Format as #AARRGGBB
        Return String.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", alpha, red, green, blue)
    End Function
    Function ColorToHex(ByVal clr As Color, ByVal alphaValue As Integer) As String
        ' Ensure alpha is within byte range (0-255)
        Dim alpha As Byte = CByte(Math.Max(0, Math.Min(255, alphaValue)))
        Dim red As Byte = clr.R
        Dim green As Byte = clr.G
        Dim blue As Byte = clr.B

        ' Format as #AARRGGBB (ensures 2-digit hex for each component)
        Return String.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", alpha, red, green, blue)
    End Function
    Public Function HexToColor(hexColor As String) As Color
        ' Remove the # if present
        If hexColor.StartsWith("#") Then
            hexColor = hexColor.Substring(1)
        End If

        ' Check if we have a valid ARGB format (8 characters)
        If hexColor.Length <> 8 Then
            Throw New ArgumentException("Hex color must be in #AARRGGBB format")
        End If

        ' Parse each component and create Color
        Dim a As Byte = Convert.ToByte(hexColor.Substring(0, 2), 16)
        Dim r As Byte = Convert.ToByte(hexColor.Substring(2, 2), 16)
        Dim g As Byte = Convert.ToByte(hexColor.Substring(4, 2), 16)
        Dim b As Byte = Convert.ToByte(hexColor.Substring(6, 2), 16)

        Return Color.FromArgb(a, r, g, b)
    End Function

    Function IsValidHexColor(colorStr As String) As Boolean
        ' Regex pattern for # followed by 8 hex digits (AARRGGBB)
        Dim pattern As String = "^#[0-9A-Fa-f]{8}$"
        Return System.Text.RegularExpressions.Regex.IsMatch(colorStr, pattern)
    End Function

    Public Sub SetFillClrFromHex(hexColor As String)
        Dim alpha, red, green, blue As Byte
        ' Remove the # if present
        If hexColor.StartsWith("#") Then
            hexColor = hexColor.Substring(1)
        End If

        ' Check if we have a valid ARGB format (8 characters)
        If hexColor.Length <> 8 Then
            Throw New ArgumentException("Hex color must be in #AARRGGBB format")
        End If

        ' Parse each component
        alpha = Convert.ToByte(hexColor.Substring(0, 2), 16)
        red = Convert.ToByte(hexColor.Substring(2, 2), 16)
        green = Convert.ToByte(hexColor.Substring(4, 2), 16)
        blue = Convert.ToByte(hexColor.Substring(6, 2), 16)
        tbFillOpacity.Value = alpha
        clrFillColor.Color = Color.FromArgb(alpha, red, green, blue)

    End Sub

    Public Sub SetBrdrClrFromHex(hexColor As String)
        Dim alpha, red, green, blue As Byte
        ' Remove the # if present
        If hexColor.StartsWith("#") Then
            hexColor = hexColor.Substring(1)
        End If

        ' Check if we have a valid ARGB format (8 characters)
        If hexColor.Length <> 8 Then
            Throw New ArgumentException("Hex color must be in #AARRGGBB format")
        End If

        ' Parse each component
        alpha = Convert.ToByte(hexColor.Substring(0, 2), 16)
        red = Convert.ToByte(hexColor.Substring(2, 2), 16)
        green = Convert.ToByte(hexColor.Substring(4, 2), 16)
        blue = Convert.ToByte(hexColor.Substring(6, 2), 16)
        tbBrdrOpacity.Value = alpha
        clrBorderColor.Color = Color.FromArgb(alpha, red, green, blue)

    End Sub

    Private Sub btnSET_Click(sender As Object, e As EventArgs) Handles btnSET.Click
        If Treat IsNot Nothing AndAlso trtID <> -1 AndAlso DefaultFillColor IsNot Nothing AndAlso ShapeName IsNot Nothing Then
            If IsValidHexColor(slctdFillClr) AndAlso IsValidHexColor(slctdBrdrClr) Then

                If clsTblTrtDATA.UpdateTrtClr(trtID, ColorToHex(fillCLR, tbFillOpacity.Value), ColorToHex(brdCLR, tbBrdrOpacity.Value), brdrThic) Then
                    Dim x = clsTblTrtDATA.UpdateTreatFillColor(ColorToHex(fillCLR, tbFillOpacity.Value), Treat)
                    If x > 0 Then
                        MsgBox(x & " Colors Updated In Treats Table")
                    End If
                    SetTrtsTreeViewDataSource()
                    MsgBox("Colors Updated")
                End If
            End If

        End If

    End Sub


#Region "Colors"

    Private slctdFillClr, slctdBrdrClr As String

    Property fillCLR As Color
        Get
            Return _fillCLR
        End Get
        Set
            _fillCLR = Value
            Dim col As SvgImageItemCollection = zSvg.RootItems
            For Each item As SvgImageItem In col
                If item.Visible = True AndAlso item IsNot Nothing Then
                    item.Appearance.Normal.FillColor = Value
                    lblFill.BackColor = Value
                End If
            Next
        End Set
    End Property

    Property brdCLR As Color
        Get
            Return _brdCLR
        End Get
        Set
            _brdCLR = Value
            Dim col As SvgImageItemCollection = zSvg.RootItems
            For Each item As SvgImageItem In col
                If item.Visible = True AndAlso item IsNot Nothing Then
                    item.Appearance.Normal.BorderColor = Value
                    lblBorder.BackColor = Value
                End If
            Next
        End Set
    End Property

    Property brdrThic As Byte
        Get
            Return _brdrThic
        End Get
        Set
            _brdrThic = Value
            Dim col As SvgImageItemCollection = zSvg.RootItems
            For Each item As SvgImageItem In col
                If item.Visible = True AndAlso item IsNot Nothing Then
                    item.Appearance.Normal.BorderThickness = Value
                    tbThick.Value = Value
                End If
            Next
        End Set
    End Property

    Private Sub clrFillColor_EditValueChanged(sender As Object, e As EventArgs) Handles clrFillColor.EditValueChanged
        fillCLR = clrFillColor.Color
        lblFill.BackColor = fillCLR
        slctdFillClr = ColorToHex(fillCLR)
    End Sub

    Private Sub clrBorderColor_EditValueChanged(sender As Object, e As EventArgs) Handles clrBorderColor.EditValueChanged
        brdCLR = clrBorderColor.Color
        lblBorder.BackColor = brdCLR
        slctdBrdrClr = ColorToHex(brdCLR)
    End Sub

    Private Sub tbThick_EditValueChanged(sender As Object, e As EventArgs) Handles tbThick.EditValueChanged
        Dim col As SvgImageItemCollection = zSvg.RootItems
        For Each item As SvgImageItem In col
            If item.Visible = True AndAlso item IsNot Nothing Then
                item.Appearance.Normal.BorderThickness = tbThick.Value
            End If
        Next
    End Sub
    Private Sub tbFillOpacity_EditValueChanged(sender As Object, e As EventArgs) Handles tbFillOpacity.EditValueChanged
        Dim a As Integer = tbFillOpacity.Value
        Dim r As Integer = fillCLR.R
        Dim g As Integer = fillCLR.G
        Dim b As Integer = fillCLR.B
        lblFill.BackColor = Color.FromArgb(a, r, g, b)
        fillCLR = Color.FromArgb(a, r, g, b)
        slctdFillClr = ColorToHex(fillCLR, a)
    End Sub

    Private Sub tbBrdrOpacity_EditValueChanged(sender As Object, e As EventArgs) Handles tbBrdrOpacity.EditValueChanged
        Dim a As Integer = tbBrdrOpacity.Value
        Dim r As Integer = brdCLR.R
        Dim g As Integer = brdCLR.G
        Dim b As Integer = brdCLR.B
        lblBorder.BackColor = Color.FromArgb(a, r, g, b)
        brdCLR = Color.FromArgb(a, r, g, b)
        slctdBrdrClr = ColorToHex(brdCLR, a)
    End Sub



#End Region


#Region "TrtsList and TrtsTreeView"

    'Private originalTrtsTable As DataTable
    'Private originalAddedTrtsTable As DataTable
    Private mouseDownLocation As Point
    Private mouseDownTime As DateTime

#Region "SUBS"


    Dim Treat As String = ""
    Dim trtID As Integer = 0
    'Dim TrtClrID As Integer = 0
    Dim treatmentText As String = ""
    Private DefaultFillColor As String = ""
    Private DefaultBrdrColor As String = ""
    Private DefaultBrdrThick As Short
    Private TrtColor As String = ""
    Private BrdrColor As String = ""

    Private ShapeID As Integer = 0
    Private ShapeName As String = ""
    Private _fillCLR As Color
    Private _brdCLR As Color
    Private _brdrThic As Short


    Private Sub SetTrtsTreeViewDataSource()
        ' Define the connection string
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString

        ' Clear existing nodes in the TreeView
        TrtsTreeView.Nodes.Clear()

        ' Define the SQL query with filtering for ToothID and including ShapeName
        Dim query As String = ""
        'query = "SELECT  dbo.TblTRTS.TrtID, dbo.TblTRTS.Trt,  dbo.TblTrtClr.FillColor AS DefaultColor, dbo.TblTRTS.ShapeID, dbo.Shapes.ShapeName, dbo.TblTRTS.TrtClrID,
        '             dbo.TblTrtClr.FillColor AS CurrentColor, dbo.TblTRTS.TrtGroup
        '     FROM    dbo.TblTRTS INNER JOIN
        '             dbo.TblTrtClr ON dbo.TblTRTS.TrtClrID = dbo.TblTrtClr.TrtClrID LEFT OUTER JOIN
        '             dbo.Shapes ON dbo.TblTRTS.ShapeID = dbo.Shapes.ShapeID
        '     ORDER BY dbo.TblTRTS.TrtGroup, dbo.TblTRTS.Trt"
        query = "SELECT dbo.TblTRTS.TrtID, dbo.TblTRTS.Trt, dbo.TblTRTS.ShapeID, dbo.Shapes.ShapeName, dbo.TblTRTS.TrtGroup, dbo.TblTRTS.TrtColor,
                        dbo.TblTRTS.TrtBrdrClr, dbo.TblTRTS.TrtBrdrThick, dbo.TblTRTS.DefFillColor, 
                        dbo.TblTRTS.DefBrdrColor, dbo.TblTRTS.DefBrdrThick
                FROM    dbo.TblTRTS INNER JOIN
                        dbo.Shapes ON dbo.TblTRTS.ShapeID = dbo.Shapes.ShapeID
                ORDER BY dbo.TblTRTS.TrtGroup, dbo.TblTRTS.Trt"
        ' Create and open a connection
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            ' Create a command with the parameterized query
            Dim command As New SqlCommand(query, connection)
            ' Create a DataAdapter to fill a DataTable
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            ' Group treatments by TrtGroup
            Dim groups = dataTable.AsEnumerable().GroupBy(Function(row) row.Field(Of String)("TrtGroup"))

            ' Add groups and treatments to the TreeView
            For Each group In groups
                ' Create a node for the group (parent node)
                Dim groupNode As New TreeNode(group.Key) With {
                .Name = group.Key,
                .ForeColor = Color.Blue ' Group nodes in blue
            }

                ' Add child nodes for treatments
                For Each treatment In group
                    Dim shapeIdObj = treatment("ShapeID")
                    Dim shapeNameObj = treatment("ShapeName")

                    Dim treatmentNode As New TreeNode(treatment.Field(Of String)("Trt")) With {
                    .Tag = New With {
                        Key .TrtID = treatment.Field(Of Integer)("TrtID"),
                        Key .Trt = treatment.Field(Of String)("Trt"), ' Changed from .Treat to .Trt to match field name
                        Key .ShapeID = If(shapeIdObj Is DBNull.Value, Nothing, CInt(shapeIdObj)),
                        Key .ShapeName = If(shapeNameObj Is DBNull.Value, String.Empty, CStr(shapeNameObj)),
                        Key .TrtColor = treatment.Field(Of String)("TrtColor"),
                        Key .TrtBrdrClr = treatment.Field(Of String)("TrtBrdrClr"),
                        Key .TrtBrdrThick = treatment.Field(Of Byte)("TrtBrdrThick"),
                        Key .DefFillColor = treatment.Field(Of String)("DefFillColor")
                    },
                    .ForeColor = Color.Green ' Treatment nodes in green
                }
                    groupNode.Nodes.Add(treatmentNode)
                Next
                ' Add the group node to the TreeView
                TrtsTreeView.Nodes.Add(groupNode)
            Next
        End Using
    End Sub

    Private Sub FilterTrtsTreeView(searchText As String)
        ' Rebuild the TreeView with filtered data
        Dim connectionString As String = DentistXDATA.GetConnection.ConnectionString
        Dim query As String = "SELECT TrtGroup, TrtID, Trt, ToothID FROM TblTRTS WHERE ToothID = 'ALL' OR CHARINDEX(',' + @ToothID + ',', ',' + ToothID + ',') = 0 ORDER BY TrtGroup, Trt"
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ToothID", "ALL") ' Use appropriate ToothID if available
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)

            ' Clear the TreeView
            TrtsTreeView.Nodes.Clear()

            ' Group treatments by TrtGroup
            Dim groups = dataTable.AsEnumerable().
            Where(Function(row) row.Field(Of String)("Trt").ToLower().Contains(searchText)).
            GroupBy(Function(row) row.Field(Of String)("TrtGroup"))

            ' Add groups and treatments to TreeView
            For Each group In groups
                Dim groupNode As New TreeNode(group.Key)
                For Each treatment In group
                    Dim treatmentNode As New TreeNode(treatment.Field(Of String)("Trt")) With {
                    .Tag = treatment.Field(Of Integer)("TrtID")
                }
                    groupNode.Nodes.Add(treatmentNode)
                Next
                TrtsTreeView.Nodes.Add(groupNode)
            Next
        End Using

        For Each groupNode As TreeNode In TrtsTreeView.Nodes
            Dim groupVisible As Boolean = False ' Track visibility for the parent node
            For Each treatmentNode As TreeNode In groupNode.Nodes
                If treatmentNode.Text.ToLower().Contains(searchText.ToLower()) Then
                    treatmentNode.ForeColor = Color.Red ' Highlight matching child node in red
                    treatmentNode.BackColor = Color.LightYellow ' Optional: Background highlight
                    treatmentNode.EnsureVisible() ' Ensure visibility of the matching node
                    groupVisible = True
                Else
                    treatmentNode.ForeColor = Color.Green ' Non-matching child nodes in green
                    treatmentNode.BackColor = Color.Transparent
                End If
            Next

            If groupVisible Then
                groupNode.ForeColor = Color.Blue ' Parent node in blue if any child is visible
                groupNode.BackColor = Color.Transparent
                groupNode.Expand() ' Expand parent if matches are found in children
            Else
                groupNode.ForeColor = Color.Transparent ' Hide parent node if no matching children
                groupNode.BackColor = Color.Transparent
            End If
        Next
    End Sub
    Private Sub txtSrchTrt_TextChanged(sender As Object, e As EventArgs) Handles txtSrchTrt.TextChanged
        FilterTrtsTreeView(txtSrchTrt.Text)
    End Sub

#End Region
#Region "TrtsTreeViewEvents"

    Public Class DragDropConstants
        Public Const CustomFormat As String = "YourApp.DragDropDataFormat"
    End Class

    <Serializable()>
    Public Class DragDropData
        Public Property treatText As String
        Public Property ShapeName As String
        Public Property ShapeID As Integer
    End Class
    'TrtsTreeView
    Private Sub TrtsTreeView_MouseDown(sender As Object, e As MouseEventArgs) Handles TrtsTreeView.MouseDown
        ' Record the initial position and time of the mouse down
        If e.Button = MouseButtons.Left Then
            mouseDownLocation = e.Location
            mouseDownTime = DateTime.Now
        End If
    End Sub
    Private Sub TrtsTreeView_MouseMove(sender As Object, e As MouseEventArgs) Handles TrtsTreeView.MouseMove
        ' Ensure the mouse down location and time are tracked globally
        If mouseDownLocation = Point.Empty Then Exit Sub

        ' Calculate distance and time since mouse down
        Dim distance As Integer = CInt(Math.Sqrt((e.X - mouseDownLocation.X) ^ 2 + (e.Y - mouseDownLocation.Y) ^ 2))
        Dim timeHeld As TimeSpan = DateTime.Now - mouseDownTime

        ' Initiate drag if left button is held, user has moved enough, and held for over 150 ms
        If e.Button = MouseButtons.Left AndAlso distance > 5 AndAlso timeHeld.TotalMilliseconds > 150 AndAlso TrtsTreeView.SelectedNode IsNot Nothing Then
            ' Get the selected node and ensure it's valid
            Dim selectedNode As TreeNode = TrtsTreeView.SelectedNode
            ' Prevent dragging the root node
            If selectedNode.Parent Is Nothing Then
                ' Root nodes have no parent; skip dragging
                Exit Sub
            End If
            ' Extract treatment data from the node
            'Extract Data from the node's Tag
            Dim trtText As String = selectedNode.Text
            Dim trtShapeName As String = selectedNode.Tag.ShapeName
            Dim shapeID As Integer = selectedNode.Tag.ShapeID

            ' Create a custom object with all data
            Dim dragData As New DragDropData With {
                .treatText = trtText,
                .ShapeName = trtShapeName,
                .ShapeID = shapeID
            }

            ' Package the data into a DataObject
            Dim dataObj As New DataObject(DragDropConstants.CustomFormat, dragData)

            ' Start drag-and-drop
            TrtsTreeView.DoDragDrop(dataObj, DragDropEffects.Copy)
        End If
    End Sub
    Private Sub TrtsTreeView_KeyDown(sender As Object, e As KeyEventArgs) Handles TrtsTreeView.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Ensure a node is selected
            Dim selectedNode = TrtsTreeView.SelectedNode
            If selectedNode Is Nothing Then
                MessageBox.Show("Please select an SvgImageBox and a treatment node before proceeding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Retrieve the treatment text from the double-clicked node
            Dim treatmentText As String = selectedNode.Text.Trim()
            Dim propertyName As String = ""
            If String.IsNullOrEmpty(treatmentText) Then
                MessageBox.Show("Invalid treatment data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If




        End If
    End Sub
    Private Sub TreeView_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TrtsTreeView.NodeMouseDoubleClick
        ' Ensure a node was double-clicked
        If e.Node Is Nothing OrElse e.Node.Parent Is Nothing Then
            MessageBox.Show("Please select an SvgImageBox before adding a treatment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Retrieve the treatment text from the double-clicked node
        Dim treatmentText As String = e.Node.Text.Trim()
        Dim propertyName As String = ""
        If String.IsNullOrEmpty(treatmentText) Then
            MessageBox.Show("Invalid treatment data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If



    End Sub

    Private Sub TrtsTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TrtsTreeView.AfterSelect
        HideLayers()
        ' Check if a treatment node is selected
        If e.Node?.Tag IsNot Nothing Then
            Try
                Dim selectedData = DirectCast(e.Node.Tag, Object)
                Treat = If(selectedData.Trt?.ToString(), String.Empty)
                trtID = CInt(selectedData.TrtID)
                'TrtClrID = CInt(selectedData.TrtClrID)
                'Private DefaultFillColor As String = ""
                'Private DefaultBrdrColor As String = ""
                'Private DefaultBrdrThick As Short
                'Private TrtColor As String = ""
                'Private BrdrColor As String = ""
                DefaultFillColor = If(selectedData.DefFillColor?.ToString(), "#FFFFFF") ' Default to white if null

                TrtColor = If(selectedData.TrtColor?.ToString(), "#FFFFFF") ' Default to white if null
                BrdrColor = If(selectedData.TrtBrdrClr?.ToString(), "#FFFFFF") ' Default to white if null DefBrdrThick
                _brdrThic = If(selectedData.TrtBrdrThick?.ToString(), "#FFFFFF") ' Default to white if null DefBrdrThick


                lblDefColor.BackColor = ColorTranslator.FromHtml(DefaultFillColor)
                lblBorder.BackColor = ColorTranslator.FromHtml(BrdrColor)
                lblFill.BackColor = ColorTranslator.FromHtml(TrtColor)

                fillCLR = ColorTranslator.FromHtml(TrtColor)
                clrFillColor.Color = fillCLR
                clrBorderColor.Color = ColorTranslator.FromHtml(BrdrColor)
                brdCLR = ColorTranslator.FromHtml(BrdrColor)
                tbThick.Value = _brdrThic

                ShapeName = If(selectedData.ShapeName?.ToString(), String.Empty)
                lblTreat.Text = Treat & vbCrLf & ShapeName
                Dim col As SvgImageItemCollection = zSvg.RootItems
                Dim Crowns() As String = {"METAL CROWN", "ZERCONIA CROWN", "PFM CROWN", "EMAX CROWN", "TEMP CROWN", "STAINLESS STEEL CROWN"}
                If Crowns.Contains(Treat) Then
                    Dim crownTop = col.Find(Function(c) c.Id = "CROWN_FILL")
                    If crownTop IsNot Nothing Then
                        crownTop.Visible = True
                        crownTop.Appearance.Normal.BorderThickness = 0
                        crownTop.Appearance.Normal.FillColor = ColorTranslator.FromHtml(TrtColor)
                        'lblDefColor.BackColor = ColorTranslator.FromHtml(DefaultFillColor)
                    End If
                    Exit Sub
                End If
                Dim Bridges() As String = {"METAL BRIDGE", "ZERCONIA BRIDGE", "PFM BRIDGE", "EMAX BRIDGE", "TEMP BRIDGE", "STAINLESS STEEL BRIDGE"}
                If Bridges.Contains(Treat) Then
                    Dim Bridge = col.Find(Function(c) c.Id = "CROWN_FILL")
                    If Bridge IsNot Nothing Then
                        Bridge.Visible = True
                        Bridge.Appearance.Normal.BorderThickness = 0
                        Bridge.Appearance.Normal.FillColor = ColorTranslator.FromHtml(TrtColor)
                        lblDefColor.BackColor = ColorTranslator.FromHtml(DefaultFillColor)
                    End If
                    Exit Sub
                End If
                Dim Dentures() As String = {"COMPLETE DENTURE", "REMOVABLE PARTIAL DENTURE"}
                If Dentures.Contains(Treat) Then
                    Dim Denture = col.Find(Function(c) c.Id = "CROWN_FILL")
                    If Denture IsNot Nothing Then
                        Denture.Visible = True
                        Denture.Appearance.Normal.BorderThickness = 0
                        Denture.Appearance.Normal.FillColor = ColorTranslator.FromHtml(TrtColor)
                        lblDefColor.BackColor = ColorTranslator.FromHtml(DefaultFillColor)
                    End If
                    Exit Sub
                End If
                Dim layer = col.Find(Function(c) c.Id = ShapeName)
                If layer IsNot Nothing Then


                    layer.Visible = True
                    layer.Appearance.Normal.FillColor = ColorTranslator.FromHtml(TrtColor)
                    lblDefColor.BackColor = ColorTranslator.FromHtml(DefaultFillColor)
                Else
                    Treat = Nothing
                    trtID = -1
                    DefaultFillColor = Nothing
                    fillCLR = Color.Transparent
                    ShapeName = Nothing
                    lblDefColor.BackColor = Color.Transparent
                End If
                'ShowLayer(zSvg.RootItems, ShapeName)
            Catch ex As Exception
                MessageBox.Show("Error loading treatment data: " & ex.Message)
            End Try
        End If
    End Sub



    Private Sub ShowLayer(col As SvgImageItemCollection, shapName As String)
        HideLayers()
        Dim layer = col.FirstOrDefault(Function(c) c.Id = shapName)
        If layer IsNot Nothing Then
            layer.Visible = True
            layer.Appearance.Normal.FillColor = ColorTranslator.FromHtml(TrtColor)
            lblDefColor.BackColor = ColorTranslator.FromHtml(DefaultFillColor)
        Else
            Treat = Nothing
            trtID = -1
            DefaultFillColor = Nothing
            fillCLR = Color.Transparent
            ShapeName = Nothing
            lblDefColor.BackColor = Color.Transparent
        End If
    End Sub

    Private Sub btnSaveExit_Click(sender As Object, e As EventArgs) Handles btnSaveExit.Click
        If Treat IsNot Nothing AndAlso trtID <> -1 AndAlso DefaultFillColor IsNot Nothing AndAlso ShapeName IsNot Nothing Then
            If IsValidHexColor(slctdFillClr) AndAlso IsValidHexColor(slctdBrdrClr) Then

                If clsTblTrtDATA.UpdateTrtClr(trtID, ColorToHex(fillCLR, tbFillOpacity.Value), ColorToHex(brdCLR, tbBrdrOpacity.Value), brdrThic) Then
                    Dim x = clsTblTrtDATA.UpdateTreatFillColor(ColorToHex(fillCLR, tbFillOpacity.Value), Treat)
                    If x > 0 Then
                        MsgBox(x & " Colors Updated In Treats Table")
                    End If
                    SetTrtsTreeViewDataSource()
                    MsgBox("Colors Updated")
                End If
            End If
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If

    End Sub










#End Region

#End Region


End Class