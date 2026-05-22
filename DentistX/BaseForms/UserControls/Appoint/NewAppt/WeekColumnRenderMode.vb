''' <summary>Week day column appointment rendering backend. Default <see cref="ApptCard"/>; optional compact paint strip for overflow rows.</summary>
Public Enum WeekColumnRenderMode
    ApptCard = 0
    ''' <summary>Keep full cards for the visible cap; paint additional rows without extra <see cref="ApptCard"/> handles.</summary>
    ApptCardWithCompactPaintOverflow = 1
End Enum
