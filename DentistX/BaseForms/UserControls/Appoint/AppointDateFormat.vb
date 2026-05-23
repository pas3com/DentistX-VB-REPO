Imports System.Globalization

''' <summary>Fixed appointment UI date formats — independent of Windows regional short-date overrides.</summary>
Friend Module AppointDateFormat

    Public Const DatePattern As String = "dd/MM/yyyy"
    Public Const DayDatePattern As String = "dddd dd/MM/yyyy"
    Public Const DayShortDatePattern As String = "ddd dd/MM/yyyy"

    ''' <summary>Weekday + numeric date in the current UI language (e.g. "Saturday 23/05/2026" / "السبت 23/05/2026").</summary>
    Public Function FormatDate(d As Date) As String
        Return d.ToString(DayDatePattern, CultureInfo.CurrentCulture)
    End Function

    ''' <summary>Weekday name + numeric date (dddd dd/MM/yyyy).</summary>
    Public Function FormatDayDate(d As Date) As String
        Return d.ToString(DayDatePattern, CultureInfo.CurrentCulture)
    End Function

    ''' <summary>Short weekday + numeric date (ddd dd/MM/yyyy).</summary>
    Public Function FormatDayShortDate(d As Date) As String
        Return d.ToString(DayShortDatePattern, CultureInfo.CurrentCulture)
    End Function

    Public Function FormatWeekdayOnly(d As Date) As String
        Return d.ToString("dddd", CultureInfo.CurrentCulture)
    End Function

    Public Function FormatDayShortOnly(d As Date) As String
        Return d.ToString("ddd", CultureInfo.CurrentCulture)
    End Function

    Public Function FormatDateRange(startDate As Date, endDate As Date) As String
        Return $"{FormatDate(startDate)} - {FormatDate(endDate)}"
    End Function

    ''' <summary>Month caption (localized month name + year).</summary>
    Public Function FormatMonthYear(monthStart As Date) As String
        Return monthStart.ToString("MMMM yyyy", CultureInfo.CurrentCulture)
    End Function

    Public Function FormatDateTime(dt As Date) As String
        Return dt.ToString($"{DatePattern} HH:mm", CultureInfo.InvariantCulture)
    End Function

    Public Function FormatDateTimeWithAmPm(dt As Date) As String
        Return dt.ToString($"{DatePattern} hh:mm tt", CultureInfo.CurrentCulture)
    End Function

    ''' <summary>Apply fixed short/long date patterns to a culture (Gregorian calendar).</summary>
    Friend Sub ApplyToCulture(culture As CultureInfo)
        culture.DateTimeFormat.Calendar = New GregorianCalendar()
        culture.DateTimeFormat.ShortDatePattern = DatePattern
        culture.DateTimeFormat.LongDatePattern = DayDatePattern
    End Sub

End Module
