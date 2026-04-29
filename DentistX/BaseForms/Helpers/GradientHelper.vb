Public Module GradientHelper

    Public Sub ApplyGradientWithGlassOrig(Ctl As Control,
                                  startColor As Color,
                                  endColor As Color,
                                  Optional gradientMode As Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.ForwardDiagonal,
                                  Optional opacity As Integer = 255,
                                  Optional glassStyle As GlassStyle = GlassStyle.Simple,
                                  Optional glassParams As Dictionary(Of String, Object) = Nothing)

        opacity = Math.Max(0, Math.Min(255, opacity))

        ' Default glass parameters
        If glassParams Is Nothing Then
            glassParams = New Dictionary(Of String, Object) From {
            {"Intensity", 0.3F},
            {"Height", 0.3F},
            {"Reflection", True},
            {"GlowColor", Color.White}
        }
        End If

        Dim transparentStart As Color = Color.FromArgb(opacity, startColor)
        Dim transparentEnd As Color = Color.FromArgb(opacity, endColor)

        Dim finalImage As New Bitmap(Math.Max(1, Ctl.Width), Math.Max(1, Ctl.Height))

        Using g As Graphics = Graphics.FromImage(finalImage)
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

            ' Draw base gradient
            Using baseBrush As New Drawing2D.LinearGradientBrush(
            New Rectangle(0, 0, Ctl.Width, Ctl.Height),
            transparentStart,
            transparentEnd,
            gradientMode)

                baseBrush.GammaCorrection = True
                g.FillRectangle(baseBrush, New Rectangle(0, 0, Ctl.Width, Ctl.Height))
            End Using

            ' Apply glass effect based on style
            Select Case glassStyle
                Case GlassStyle.Simple
                    ApplySimpleGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Aero
                    ApplyAeroGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Frosted
                    ApplyFrostedGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Reflective
                    ApplyReflectiveGlass(g, Ctl.Width, Ctl.Height, glassParams)
            End Select
        End Using

        Ctl.BackgroundImage = finalImage
        Ctl.BackgroundImageLayout = ImageLayout.Stretch
        Ctl.BackColor = Color.Transparent
    End Sub


    Public Sub ApplyGradientWithGlassOld(Ctl As Control,
                                  startColor As Color,
                                  endColor As Color,
                                  Optional gradientMode As Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.ForwardDiagonal,
                                  Optional opacity As Integer = 255,
                                  Optional glassStyle As GlassStyle = GlassStyle.Simple,
                                  Optional glassParams As Dictionary(Of String, Object) = Nothing)

        opacity = Math.Max(0, Math.Min(255, opacity))

        ' Default glass parameters
        If glassParams Is Nothing Then
            glassParams = New Dictionary(Of String, Object)()
        End If

        ' Set default parameters based on glass style
        Select Case glassStyle
            Case GlassStyle.Simple
                If Not glassParams.ContainsKey("Intensity") Then glassParams("Intensity") = 0.3F
                If Not glassParams.ContainsKey("Height") Then glassParams("Height") = 0.3F

            Case GlassStyle.Aero
                If Not glassParams.ContainsKey("BlurAmount") Then glassParams("BlurAmount") = 10
                If Not glassParams.ContainsKey("GlassOpacity") Then glassParams("GlassOpacity") = 0.7F
                If Not glassParams.ContainsKey("ShineOpacity") Then glassParams("ShineOpacity") = 0.3F

            Case GlassStyle.Frosted
                If Not glassParams.ContainsKey("FrostIntensity") Then glassParams("FrostIntensity") = 0.5F
                If Not glassParams.ContainsKey("NoiseDensity") Then glassParams("NoiseDensity") = 0.1F

            Case GlassStyle.Reflective
                If Not glassParams.ContainsKey("ReflectionHeight") Then glassParams("ReflectionHeight") = 0.25F
                If Not glassParams.ContainsKey("ReflectionOpacity") Then glassParams("ReflectionOpacity") = 0.6F
                If Not glassParams.ContainsKey("HasHighlights") Then glassParams("HasHighlights") = True

            Case GlassStyle.Metallic
                If Not glassParams.ContainsKey("MetallicIntensity") Then glassParams("MetallicIntensity") = 0.5F
                If Not glassParams.ContainsKey("HasRivets") Then glassParams("HasRivets") = False

            Case GlassStyle.Aqua
                If Not glassParams.ContainsKey("WaveIntensity") Then glassParams("WaveIntensity") = 0.3F
                If Not glassParams.ContainsKey("BubbleCount") Then glassParams("BubbleCount") = 20
        End Select

        Dim transparentStart As Color = Color.FromArgb(opacity, startColor)
        Dim transparentEnd As Color = Color.FromArgb(opacity, endColor)

        Dim finalImage As New Bitmap(Math.Max(1, Ctl.Width), Math.Max(1, Ctl.Height))

        Using g As Graphics = Graphics.FromImage(finalImage)
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

            ' Draw base gradient
            Using baseBrush As New Drawing2D.LinearGradientBrush(
            New Rectangle(0, 0, Ctl.Width, Ctl.Height),
            transparentStart,
            transparentEnd,
            gradientMode)

                baseBrush.GammaCorrection = True
                g.FillRectangle(baseBrush, New Rectangle(0, 0, Ctl.Width, Ctl.Height))
            End Using

            ' Apply glass effect based on style
            Select Case glassStyle
                Case GlassStyle.Simple
                    ApplySimpleGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Aero
                    ApplyAeroGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Frosted
                    ApplyFrostedGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Reflective
                    ApplyReflectiveGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Metallic
                    ApplyMetallicGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Aqua
                    ApplyAquaGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.None
                    ' No glass effect
            End Select
        End Using

        Ctl.BackgroundImage = finalImage
        Ctl.BackgroundImageLayout = ImageLayout.Stretch
        Ctl.BackColor = Color.Transparent
    End Sub

    Public Sub ApplyGradientWithGlass(Ctl As Control,
                                  startColor As Color,
                                  endColor As Color,
                                  Optional gradientMode As Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.ForwardDiagonal,
                                  Optional opacity As Integer = 255,
                                  Optional glassStyle As GlassStyle = GlassStyle.Simple,
                                  Optional glassParams As Dictionary(Of String, Object) = Nothing)

        opacity = Math.Max(0, Math.Min(255, opacity))

        ' Default glass parameters if none provided
        If glassParams Is Nothing Then
            glassParams = CreateDefaultParams(glassStyle)
        End If

        Dim transparentStart As Color = Color.FromArgb(opacity, startColor)
        Dim transparentEnd As Color = Color.FromArgb(opacity, endColor)

        Dim finalImage As New Bitmap(Math.Max(1, Ctl.Width), Math.Max(1, Ctl.Height))

        Using g As Graphics = Graphics.FromImage(finalImage)
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

            ' Draw base gradient
            Using baseBrush As New Drawing2D.LinearGradientBrush(
            New Rectangle(0, 0, Ctl.Width, Ctl.Height),
            transparentStart,
            transparentEnd,
            gradientMode)

                baseBrush.GammaCorrection = True
                g.FillRectangle(baseBrush, New Rectangle(0, 0, Ctl.Width, Ctl.Height))
            End Using

            ' Apply glass effect based on style
            Select Case glassStyle
                Case GlassStyle.Simple
                    ApplySimpleGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Aero
                    ApplyAeroGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Frosted
                    ApplyFrostedGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Reflective
                    ApplyReflectiveGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Metallic
                    ApplyMetallicGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.Aqua
                    ApplyAquaGlass(g, Ctl.Width, Ctl.Height, glassParams)
                Case GlassStyle.None
                    ' No glass effect
            End Select
        End Using

        Ctl.BackgroundImage = finalImage
        Ctl.BackgroundImageLayout = ImageLayout.Stretch
        Ctl.BackColor = Color.Transparent
    End Sub
    '    'Usage Examples:
    '' 1. Simple Glass Effect (using defaults)
    'ApplyGradientWithGlass(Button1, Color.SteelBlue, Color.DarkBlue, 
    '                      Drawing2D.LinearGradientMode.Vertical,
    '                      opacity:=230,
    '                      glassStyle:=GlassStyle.Simple)

    '' 2. Simple Glass Effect with custom parameters
    'ApplyGradientWithGlass(Button2, Color.SteelBlue, Color.DarkBlue,
    '                      Drawing2D.LinearGradientMode.Vertical,
    '                      opacity:=230,
    '                      glassStyle:=GlassStyle.Simple,
    '                      glassParams:=New Dictionary(Of String, Object) From {
    '                          {"Intensity", 0.4F},
    '                          {"Height", 0.25F}
    '                      })

    '' 3. Aero Glass (Windows Vista/7 style)
    'ApplyGradientWithGlass(Panel1, Color.LightGray, Color.DarkGray,
    '                      Drawing2D.LinearGradientMode.Horizontal,
    '                      glassStyle:=GlassStyle.Aero,
    '                      glassParams:=New Dictionary(Of String, Object) From {
    '                          {"BlurAmount", 15},
    '                          {"GlassOpacity", 0.6F},
    '                          {"ShineOpacity", 0.4F}
    '                      })

    '' 4. Frosted Glass
    'ApplyGradientWithGlass(GroupBox1, Color.LightSteelBlue, Color.SteelBlue,
    '                      glassStyle:=GlassStyle.Frosted,
    '                      glassParams:=New Dictionary(Of String, Object) From {
    '                          {"FrostIntensity", 0.7F},
    '                          {"NoiseDensity", 0.08F}
    '                      })

    '' 5. Reflective Glass (like Apple/Mac style)
    'ApplyGradientWithGlass(Panel2, Color.Silver, Color.DimGray,
    '                      Drawing2D.LinearGradientMode.Vertical,
    '                      glassStyle:=GlassStyle.Reflective)

    '' 6. Metallic Glass (industrial look)
    'ApplyGradientWithGlass(Button3, Color.Gold, Color.DarkGoldenrod,
    '                      Drawing2D.LinearGradientMode.ForwardDiagonal,
    '                      glassStyle:=GlassStyle.Metallic,
    '                      glassParams:=New Dictionary(Of String, Object) From {
    '                          {"MetallicIntensity", 0.6F},
    '                          {"HasRivets", True}
    '                      })

    '' 7. Aqua Glass (water/underwater effect)
    'ApplyGradientWithGlass(Panel3, Color.LightBlue, Color.DarkBlue,
    '                      glassStyle:=GlassStyle.Aqua)

    '' 8. No glass effect - just gradient
    'ApplyGradientWithGlass(Panel4, Color.Red, Color.DarkRed,
    '                      glassStyle:=GlassStyle.None)

#Region "Helpers"

    Public Function CreateDefaultParams(style As GlassStyle) As Dictionary(Of String, Object)
        Dim params As New Dictionary(Of String, Object)()

        Select Case style
            Case GlassStyle.Simple
                params.Add("Intensity", 0.3F)
                params.Add("Height", 0.3F)

            Case GlassStyle.Aero
                params.Add("BlurAmount", 10)
                params.Add("GlassOpacity", 0.7F)
                params.Add("ShineOpacity", 0.3F)

            Case GlassStyle.Frosted
                params.Add("FrostIntensity", 0.5F)
                params.Add("NoiseDensity", 0.1F)

            Case GlassStyle.Reflective
                params.Add("ReflectionHeight", 0.25F)
                params.Add("ReflectionOpacity", 0.6F)
                params.Add("HasHighlights", True)

            Case GlassStyle.Metallic
                params.Add("MetallicIntensity", 0.5F)
                params.Add("HasRivets", False)

            Case GlassStyle.Aqua
                params.Add("WaveIntensity", 0.3F)
                params.Add("BubbleCount", 20)
        End Select

        Return params
    End Function
    ' Quick apply methods for common styles
    Public Sub ApplySimpleGlassEffect(Ctl As Control, startColor As Color, endColor As Color)
        ApplyGradientWithGlass(Ctl, startColor, endColor, GlassStyle.Simple, glassStyle:=GlassStyle.Simple)
    End Sub

    Public Sub ApplyAeroGlassEffect(Ctl As Control, startColor As Color, endColor As Color)
        ApplyGradientWithGlass(Ctl, startColor, endColor, GlassStyle.Aero, glassStyle:=GlassStyle.Aero)
    End Sub

    Public Sub ApplyReflectiveGlassEffect(Ctl As Control, startColor As Color, endColor As Color)
        'ApplyGradientWithGlass(Ctl, startColor, endColor, GlassStyle.Reflective, CreateDefaultParams(GlassStyle.Reflective))
        ' CORRECT - Specify parameter names or use correct order:
        ApplyGradientWithGlass(Ctl, startColor, endColor, opacity:=230, glassStyle:=GlassStyle.Reflective)

    End Sub
    Public Enum GlassStyle
        Simple
        Aero
        Frosted
        Reflective
        Metallic
        Aqua
        None
    End Enum

#End Region



    Private Sub ApplySimpleGlass(g As Graphics, width As Integer, height As Integer, params As Dictionary(Of String, Object))
        Dim intensity As Single = CSng(If(params.ContainsKey("Intensity"), params("Intensity"), 0.3F))
        Dim glassHeightRatio As Single = CSng(If(params.ContainsKey("Height"), params("Height"), 0.3F))

        Dim glassHeight As Integer = CInt(height * glassHeightRatio)
        If glassHeight < 1 Then Return

        Using glassBrush As New Drawing2D.LinearGradientBrush(
        New Rectangle(0, 0, width, glassHeight),
        Color.FromArgb(CInt(200 * intensity), Color.White),
        Color.FromArgb(CInt(50 * intensity), Color.White),
        Drawing2D.LinearGradientMode.Vertical)

            g.FillRectangle(glassBrush, New Rectangle(0, 0, width, glassHeight))
        End Using
    End Sub

    ' ... (Implement other glass styles similarly)
    Private Sub ApplyAeroGlass(g As Graphics, width As Integer, height As Integer, params As Dictionary(Of String, Object))
        Dim blurAmount As Integer = CInt(If(params.ContainsKey("BlurAmount"), params("BlurAmount"), 10))
        Dim glassOpacity As Single = CSng(If(params.ContainsKey("GlassOpacity"), params("GlassOpacity"), 0.7F))
        Dim baseColor As Color = DirectCast(If(params.ContainsKey("BaseColor"), params("BaseColor"), Color.SteelBlue), Color)

        ' Apply glass effect (simulated blur)
        For i As Integer = 0 To blurAmount - 1
            Dim alpha As Integer = CInt((glassOpacity * 255) * (1 - (i / blurAmount)))
            Dim blurColor As Color = Color.FromArgb(alpha, Color.White)

            Using blurBrush As New SolidBrush(blurColor)
                g.FillRectangle(blurBrush,
                           i, i,
                           width - (i * 2),
                           height - (i * 2))
            End Using
        Next

        ' Add top shine
        Dim shineOpacity As Single = CSng(If(params.ContainsKey("ShineOpacity"), params("ShineOpacity"), 0.3F))
        Dim shineHeight As Integer = CInt(height * 0.15)

        Using shineBrush As New Drawing2D.LinearGradientBrush(
        New Rectangle(0, 0, width, shineHeight),
        Color.FromArgb(CInt(shineOpacity * 255), Color.White),
        Color.Transparent,
        Drawing2D.LinearGradientMode.Vertical)

            g.FillRectangle(shineBrush, New Rectangle(0, 0, width, shineHeight))
        End Using

        ' Add subtle inner glow
        Using innerGlowPath As New Drawing2D.GraphicsPath()
            innerGlowPath.AddRectangle(New Rectangle(2, 2, width - 4, height - 4))

            Using innerGlowPen As New Pen(Color.FromArgb(30, Color.White), 2)
                innerGlowPen.Alignment = Drawing2D.PenAlignment.Inset
                g.DrawPath(innerGlowPen, innerGlowPath)
            End Using
        End Using
    End Sub

    Private Sub ApplyFrostedGlass(g As Graphics, width As Integer, height As Integer, params As Dictionary(Of String, Object))
        Dim frostIntensity As Single = CSng(If(params.ContainsKey("FrostIntensity"), params("FrostIntensity"), 0.5F))
        Dim noiseDensity As Single = CSng(If(params.ContainsKey("NoiseDensity"), params("NoiseDensity"), 0.1F))
        Dim rnd As New Random()

        ' Add frost/white overlay
        Using frostBrush As New SolidBrush(Color.FromArgb(CInt(150 * frostIntensity), Color.White))
            g.FillRectangle(frostBrush, New Rectangle(0, 0, width, height))
        End Using

        ' Add noise for frosted effect
        Dim noiseCount As Integer = CInt(width * height * noiseDensity)

        For i As Integer = 0 To noiseCount - 1
            Dim x As Integer = rnd.Next(0, width)
            Dim y As Integer = rnd.Next(0, height)
            Dim size As Integer = rnd.Next(1, 4)
            Dim alpha As Integer = rnd.Next(30, 120)

            Using noiseBrush As New SolidBrush(Color.FromArgb(alpha, Color.White))
                g.FillRectangle(noiseBrush, x, y, size, size)
            End Using
        Next

        ' Add larger "ice crystal" effects
        Dim crystalCount As Integer = CInt(noiseCount * 0.05)

        For i As Integer = 0 To crystalCount - 1
            Dim x As Integer = rnd.Next(0, width)
            Dim y As Integer = rnd.Next(0, height)
            Dim size As Integer = rnd.Next(2, 6)
            Dim alpha As Integer = rnd.Next(80, 180)

            Using crystalPen As New Pen(Color.FromArgb(alpha, Color.White), 1)
                g.DrawEllipse(crystalPen, x, y, size, size)
            End Using
        Next

        ' Add subtle border
        Using borderPen As New Pen(Color.FromArgb(100, Color.White), 1)
            g.DrawRectangle(borderPen, 0, 0, width - 1, height - 1)
        End Using
    End Sub

    Private Sub ApplyReflectiveGlass(g As Graphics, width As Integer, height As Integer, params As Dictionary(Of String, Object))
        Dim reflectionHeightRatio As Single = CSng(If(params.ContainsKey("ReflectionHeight"), params("ReflectionHeight"), 0.25F))
        Dim reflectionOpacity As Single = CSng(If(params.ContainsKey("ReflectionOpacity"), params("ReflectionOpacity"), 0.6F))
        Dim hasHighlights As Boolean = CBool(If(params.ContainsKey("HasHighlights"), params("HasHighlights"), True))

        Dim reflectionHeight As Integer = CInt(height * reflectionHeightRatio)

        ' Create curved reflection path
        Using reflectionPath As New Drawing2D.GraphicsPath()
            reflectionPath.AddCurve({
            New Point(0, 0),
            New Point(width \ 4, reflectionHeight \ 3),
            New Point(width \ 2, 0),
            New Point(width * 3 \ 4, reflectionHeight \ 3),
            New Point(width, 0)
        })

            reflectionPath.AddLine(width, 0, width, reflectionHeight)
            reflectionPath.AddLine(width, reflectionHeight, 0, reflectionHeight)
            reflectionPath.CloseFigure()

            ' Fill with gradient
            Using reflectionBrush As New Drawing2D.PathGradientBrush(reflectionPath)
                reflectionBrush.CenterColor = Color.FromArgb(CInt(180 * reflectionOpacity), Color.White)

                Dim surroundColors() As Color = {Color.FromArgb(0, Color.White)}
                reflectionBrush.SurroundColors = surroundColors

                reflectionBrush.CenterPoint = New PointF(width / 2, 0)
                reflectionBrush.FocusScales = New PointF(0.8F, 0.3F)

                g.FillPath(reflectionBrush, reflectionPath)
            End Using
        End Using

        ' Add multiple highlight lines for more realistic glass
        If hasHighlights Then
            ' Primary highlight
            Using highlightPen As New Pen(Color.FromArgb(200, Color.White), 1.5F)
                g.DrawLine(highlightPen, 0, 1, width, 1)
            End Using

            ' Secondary subtle highlight
            Using subtleHighlightPen As New Pen(Color.FromArgb(120, Color.White), 1)
                g.DrawLine(subtleHighlightPen, width \ 4, 3, width * 3 \ 4, 3)
            End Using

            ' Edge highlights
            Using edgePen As New Pen(Color.FromArgb(80, Color.White), 1)
                ' Left edge
                g.DrawLine(edgePen, 1, 0, 1, reflectionHeight \ 2)
                ' Right edge
                g.DrawLine(edgePen, width - 2, 0, width - 2, reflectionHeight \ 2)
            End Using
        End If

        ' Add bottom fade reflection
        If reflectionHeight < height Then
            Dim bottomReflectionHeight As Integer = CInt((height - reflectionHeight) * 0.3)
            Dim bottomRect As New Rectangle(0, height - bottomReflectionHeight, width, bottomReflectionHeight)

            Using bottomBrush As New Drawing2D.LinearGradientBrush(
            bottomRect,
            Color.FromArgb(40, Color.White),
            Color.Transparent,
            Drawing2D.LinearGradientMode.Vertical)

                g.FillRectangle(bottomBrush, bottomRect)
            End Using
        End If
    End Sub

    Private Sub ApplyMetallicGlass(g As Graphics, width As Integer, height As Integer, params As Dictionary(Of String, Object))
        Dim metallicIntensity As Single = CSng(If(params.ContainsKey("MetallicIntensity"), params("MetallicIntensity"), 0.5F))
        Dim hasRivets As Boolean = CBool(If(params.ContainsKey("HasRivets"), params("HasRivets"), False))

        ' Create metallic shine effect
        Dim centerX As Integer = width \ 2
        Dim centerY As Integer = height \ 2

        Using metallicPath As New Drawing2D.GraphicsPath()
            ' Create elliptical shine
            metallicPath.AddEllipse(centerX - width \ 4, centerY - height \ 4, width \ 2, height \ 2)

            Using metallicBrush As New Drawing2D.PathGradientBrush(metallicPath)
                metallicBrush.CenterColor = Color.FromArgb(CInt(120 * metallicIntensity), Color.White)

                Dim surroundColors() As Color = {Color.FromArgb(0, Color.White)}
                metallicBrush.SurroundColors = surroundColors

                metallicBrush.CenterPoint = New PointF(centerX, centerY - height \ 8)

                g.FillPath(metallicBrush, metallicPath)
            End Using
        End Using

        ' Add corner shines
        Dim cornerSize As Integer = Math.Min(width, height) \ 4

        ' Top-left corner
        Using cornerBrush1 As New Drawing2D.LinearGradientBrush(
        New Rectangle(0, 0, cornerSize, cornerSize),
        Color.FromArgb(CInt(80 * metallicIntensity), Color.White),
        Color.Transparent,
        45.0F)

            g.FillRectangle(cornerBrush1, 0, 0, cornerSize, cornerSize)
        End Using

        ' Top-right corner
        Using cornerBrush2 As New Drawing2D.LinearGradientBrush(
        New Rectangle(width - cornerSize, 0, cornerSize, cornerSize),
        Color.FromArgb(CInt(80 * metallicIntensity), Color.White),
        Color.Transparent,
        135.0F)

            g.FillRectangle(cornerBrush2, width - cornerSize, 0, cornerSize, cornerSize)
        End Using

        ' Add rivets if requested
        If hasRivets Then
            Using rivetBrush As New SolidBrush(Color.FromArgb(100, Color.Gray))
                Dim rivetSize As Integer = 3
                Dim spacing As Integer = 20

                For x As Integer = spacing To width - spacing Step spacing
                    For y As Integer = spacing To height - spacing Step spacing
                        If (x = spacing OrElse x = width - spacing OrElse y = spacing OrElse y = height - spacing) Then
                            g.FillEllipse(rivetBrush, x - rivetSize \ 2, y - rivetSize \ 2, rivetSize, rivetSize)

                            ' Add highlight to rivet
                            Using highlightBrush As New SolidBrush(Color.FromArgb(150, Color.White))
                                g.FillEllipse(highlightBrush, x - rivetSize \ 2 + 1, y - rivetSize \ 2 + 1, rivetSize - 2, rivetSize - 2)
                            End Using
                        End If
                    Next
                Next
            End Using
        End If
    End Sub

    Private Sub ApplyAquaGlass(g As Graphics, width As Integer, height As Integer, params As Dictionary(Of String, Object))
        Dim waveIntensity As Single = CSng(If(params.ContainsKey("WaveIntensity"), params("WaveIntensity"), 0.3F))
        Dim bubbleCount As Integer = CInt(If(params.ContainsKey("BubbleCount"), params("BubbleCount"), 20))
        Dim rnd As New Random()

        ' Create water-like waves
        Dim waveHeight As Integer = CInt(height * 0.1)

        For i As Integer = 0 To 3
            Dim waveY As Integer = CInt(height * (i * 0.2))
            Dim waveAlpha As Integer = CInt(40 * waveIntensity * (1 - i * 0.2))

            Using wavePen As New Pen(Color.FromArgb(waveAlpha, Color.White), 2)
                For x As Integer = 0 To width Step 20
                    Dim curveHeight As Integer = rnd.Next(2, 6)
                    g.DrawCurve(wavePen, {
                    New Point(x, waveY),
                    New Point(x + 10, waveY - curveHeight),
                    New Point(x + 20, waveY)
                })
                Next
            End Using
        Next

        ' Add bubbles
        For i As Integer = 0 To bubbleCount - 1
            Dim bubbleX As Integer = rnd.Next(0, width)
            Dim bubbleY As Integer = rnd.Next(0, height)
            Dim bubbleSize As Integer = rnd.Next(3, 10)
            Dim bubbleAlpha As Integer = rnd.Next(30, 100)

            ' Bubble outer ring
            Using bubblePen As New Pen(Color.FromArgb(bubbleAlpha, Color.White), 1)
                g.DrawEllipse(bubblePen, bubbleX, bubbleY, bubbleSize, bubbleSize)
            End Using

            ' Bubble highlight
            Using highlightBrush As New SolidBrush(Color.FromArgb(bubbleAlpha + 50, Color.White))
                g.FillEllipse(highlightBrush, bubbleX + 1, bubbleY + 1, bubbleSize \ 2, bubbleSize \ 2)
            End Using
        Next

        ' Add surface reflection
        Dim surfaceHeight As Integer = CInt(height * 0.25)

        Using surfaceBrush As New Drawing2D.LinearGradientBrush(
        New Rectangle(0, 0, width, surfaceHeight),
        Color.FromArgb(80, Color.White),
        Color.FromArgb(20, Color.White),
        Drawing2D.LinearGradientMode.Vertical)

            g.FillRectangle(surfaceBrush, New Rectangle(0, 0, width, surfaceHeight))
        End Using
    End Sub

#Region "Simple Gradient Application"

    Public Sub ApplyGradientToControl(Ctl As Control,
                                      startColor As Color,
                                      endColor As Color,
                                      Optional gradientMode As Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.Vertical,
                                      Optional opacity As Integer = 255)

        ' Remove existing paint handler to avoid duplicates
        RemovePaintHandler(Ctl)
        ' Set control to transparent
        Ctl.BackColor = Color.Transparent
        ' Apply transparency to colors
        Dim transparentStart As Color = Color.FromArgb(opacity, startColor)
        Dim transparentEnd As Color = Color.FromArgb(opacity, endColor)

        ' Create paint handler
        Dim paintHandler As PaintEventHandler = Sub(sender As Object, e As PaintEventArgs)
                                                    Dim control As Control = DirectCast(sender, Control)

                                                    ' Draw gradient background
                                                    Using brush As New Drawing2D.LinearGradientBrush(
                                                        control.ClientRectangle,
                                                        transparentStart,
                                                        transparentEnd,
                                                        gradientMode)

                                                        brush.GammaCorrection = True
                                                        e.Graphics.FillRectangle(brush, control.ClientRectangle)
                                                    End Using
                                                End Sub

        ' Store handler reference in Tag
        Ctl.Tag = New GradientHandlerInfo(paintHandler)

        ' Add paint handler
        AddHandler Ctl.Paint, paintHandler



        ' Force immediate repaint
        Ctl.Invalidate()
    End Sub

    Public Sub RemoveGradientFromControl(Ctl As Control)
        ' Check if we have gradient handler info
        If Ctl.Tag IsNot Nothing AndAlso TypeOf Ctl.Tag Is GradientHandlerInfo Then
            Dim info As GradientHandlerInfo = DirectCast(Ctl.Tag, GradientHandlerInfo)

            ' Remove the paint handler
            RemoveHandler Ctl.Paint, info.PaintHandler

            ' Clear the tag
            Ctl.Tag = Nothing

            ' Reset background color
            Ctl.BackColor = SystemColors.Control

            ' Force repaint
            Ctl.Invalidate()
        End If
    End Sub

    Private Sub RemovePaintHandler(Ctl As Control)
        If Ctl.Tag IsNot Nothing AndAlso TypeOf Ctl.Tag Is GradientHandlerInfo Then
            Dim info As GradientHandlerInfo = DirectCast(Ctl.Tag, GradientHandlerInfo)
            RemoveHandler Ctl.Paint, info.PaintHandler
        End If
    End Sub

    ' Class to store paint handler reference
    Private Class GradientHandlerInfo
        Public Property PaintHandler As PaintEventHandler

        Public Sub New(handler As PaintEventHandler)
            PaintHandler = handler
        End Sub
    End Class


    Public Class GradientControlWrapper
        Private _control As Control
        Private _startColor As Color
        Private _endColor As Color
        Private _gradientMode As Drawing2D.LinearGradientMode

        Public Sub New(ctl As Control, startColor As Color, endColor As Color,
                       Optional gradientMode As Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.Vertical)

            _control = ctl
            _startColor = startColor
            _endColor = endColor
            _gradientMode = gradientMode

            ' Wire up events
            AddHandler ctl.Paint, AddressOf Control_Paint
            AddHandler ctl.SizeChanged, AddressOf Control_SizeChanged
            AddHandler ctl.VisibleChanged, AddressOf Control_VisibleChanged

            ' Set transparent background
            ctl.BackColor = Color.Transparent
            ctl.Invalidate()
        End Sub

        Private Sub Control_Paint(sender As Object, e As PaintEventArgs)
            Dim ctl As Control = DirectCast(sender, Control)

            ' Draw gradient
            Using brush As New Drawing2D.LinearGradientBrush(
                ctl.ClientRectangle,
                _startColor,
                _endColor,
                _gradientMode)

                brush.GammaCorrection = True
                e.Graphics.FillRectangle(brush, ctl.ClientRectangle)
            End Using
        End Sub

        Private Sub Control_SizeChanged(sender As Object, e As EventArgs)
            ' Force repaint when size changes
            _control.Invalidate()
        End Sub

        Private Sub Control_VisibleChanged(sender As Object, e As EventArgs)
            If _control.Visible Then
                _control.Invalidate()
            End If
        End Sub

        Public Sub UpdateGradient(startColor As Color, endColor As Color,
                                  Optional gradientMode As Drawing2D.LinearGradientMode = Drawing2D.LinearGradientMode.Vertical)

            _startColor = startColor
            _endColor = endColor
            _gradientMode = gradientMode
            _control.Invalidate()
        End Sub

        Public Sub Dispose()
            ' Clean up event handlers
            RemoveHandler _control.Paint, AddressOf Control_Paint
            RemoveHandler _control.SizeChanged, AddressOf Control_SizeChanged
            RemoveHandler _control.VisibleChanged, AddressOf Control_VisibleChanged
            _control.BackColor = SystemColors.Control
            _control.Invalidate()
        End Sub
    End Class


#End Region

End Module