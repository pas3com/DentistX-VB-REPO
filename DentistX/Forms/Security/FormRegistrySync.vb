Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Reflection
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Dapper

Public Class FormRegistryPruneResult
    Public Property RemovedForms As Integer
    Public Property RemovedUserFormAccessRows As Integer
End Class

''' <summary>
''' Registers types into the Forms table (FormName = class name).
''' Never calls <see cref="Activator.CreateInstance"/> — many forms read <c>CurrentPatient</c>, DB, or timers in field
''' initializers / constructors (e.g. <c>frmGenerateInvoice</c>, <c>SchedulerFull</c>), which throws during sync.
''' </summary>
Public Module FormRegistrySync

    Public Function SyncWinFormsFromExecutingAssembly() As Integer
        Dim inserted As Integer = 0
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Open()
            Dim types = GetConcreteFormAndUserControlTypes(Assembly.GetExecutingAssembly())
            For Each formType In types
                inserted += UpsertFormType(conn, formType)
            Next
            inserted += MergeShellMappedFormNames(conn)
        End Using
        Return inserted
    End Function

    ''' <summary>
    ''' Deletes <c>Forms</c> rows whose <c>FormName</c> is not present in the current EXE (forms/user controls)
    ''' nor in <see cref="MainViewFormAccessMap"/> targets. Removes matching <c>UserFormAccess</c> rows first.
    ''' </summary>
    Public Function PruneObsoleteFormsFromRegistry() As FormRegistryPruneResult
        Dim valid = BuildValidFormNameSet(Assembly.GetExecutingAssembly())
        Dim removedForms = 0
        Dim removedUfa = 0
        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Open()
            Dim allRows = conn.Query(Of FormAccess)("SELECT FormID, FormName FROM Forms").ToList()
            Dim orphanIds As New List(Of Integer)
            For Each f In allRows
                If String.IsNullOrWhiteSpace(f.FormName) Then Continue For
                If Not valid.Contains(f.FormName.Trim()) Then orphanIds.Add(f.FormID)
            Next
            If orphanIds.Count = 0 Then Return New FormRegistryPruneResult()
            Using tran = conn.BeginTransaction()
                For Each fid In orphanIds
                    removedUfa += conn.Execute("DELETE FROM UserFormAccess WHERE FormID = @FormID", New With {.FormID = fid}, tran)
                Next
                For Each fid In orphanIds
                    removedForms += conn.Execute("DELETE FROM Forms WHERE FormID = @FormID", New With {.FormID = fid}, tran)
                Next
                tran.Commit()
            End Using
        End Using
        Return New FormRegistryPruneResult With {
            .RemovedForms = removedForms,
            .RemovedUserFormAccessRows = removedUfa}
    End Function

    Private Function BuildValidFormNameSet(asm As Assembly) As HashSet(Of String)
        Dim hs As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each t In GetConcreteFormAndUserControlTypes(asm)
            hs.Add(t.Name)
        Next
        For Each n In MainViewFormAccessMap.GetAllDistinctShellFormTypeNames()
            If Not String.IsNullOrWhiteSpace(n) Then hs.Add(n.Trim())
        Next
        Return hs
    End Function

    Private Function GetConcreteFormAndUserControlTypes(asm As Assembly) As List(Of Type)
        Dim raw As Type() = Nothing
        Try
            raw = asm.GetTypes()
        Catch ex As ReflectionTypeLoadException
            raw = ex.Types.Where(Function(t) t IsNot Nothing).ToArray()
        End Try

        Dim formT = GetType(Form)
        Dim ucT = GetType(UserControl)

        Return raw.Where(Function(t) t IsNot Nothing).
            Where(Function(t) Not t.IsAbstract AndAlso Not t.IsGenericTypeDefinition AndAlso Not t.IsNested).
            Where(Function(t) Not t.Name.StartsWith("<", StringComparison.Ordinal)).
            Where(Function(t) t IsNot formT AndAlso t IsNot ucT).
            Where(Function(t) ShouldIncludeWinFormsType(t)).
            Where(Function(t) formT.IsAssignableFrom(t) OrElse ucT.IsAssignableFrom(t)).
            Distinct().
            ToList()
    End Function

    ''' <summary>Skip framework/DevExpress surface types; keep app types (e.g. DentistX.*).</summary>
    Private Function ShouldIncludeWinFormsType(t As Type) As Boolean
        Dim ns = t.Namespace
        If String.IsNullOrEmpty(ns) Then Return True
        If ns.StartsWith("DevExpress", StringComparison.Ordinal) Then Return False
        If ns.StartsWith("System.Windows.Forms", StringComparison.Ordinal) Then Return False
        Return True
    End Function

    Private Function UpsertFormType(conn As SqlConnection, formType As Type) As Integer
        Dim formName = formType.Name
        Dim description = formType.FullName
        Dim titleHint = GetDisplayTitleHint(formType)

        Dim exists = conn.ExecuteScalar(Of Integer)(
            "SELECT COUNT(*) FROM Forms WHERE FormName = @FormName",
            New With {.FormName = formName})
        If exists = 0 Then
            conn.Execute(
                "INSERT INTO Forms (FormName, Description, DisplayTitle, DisplayTitleAr) VALUES (@FormName, @Description, @DisplayTitle, @DisplayTitleAr)",
                New With {.FormName = formName, .Description = description, .DisplayTitle = titleHint, .DisplayTitleAr = CType(Nothing, String)})
            Return 1
        End If
        If description IsNot Nothing Then
            conn.Execute(
                "UPDATE Forms SET Description = ISNULL(@Description, Description) WHERE FormName = @FormName",
                New With {.FormName = formName, .Description = description})
        End If
        Return 0
    End Function

    ''' <summary>No type creation — uses <see cref="DisplayNameAttribute"/> on the class when present.</summary>
    Private Function GetDisplayTitleHint(t As Type) As String
        For Each o In t.GetCustomAttributes(GetType(DisplayNameAttribute), inherit:=False)
            Dim a = TryCast(o, DisplayNameAttribute)
            If a IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(a.DisplayName) Then
                Dim s = a.DisplayName.Trim()
                If s.Length > 255 Then s = s.Substring(0, 255)
                Return s
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>Ensures every shell-mapped screen exists in <c>Forms</c>, even if reflection skipped the type.</summary>
    Private Function MergeShellMappedFormNames(conn As SqlConnection) As Integer
        Dim n = 0
        Const desc = "MainView ribbon / accordion map"
        For Each formName In MainViewFormAccessMap.GetAllDistinctShellFormTypeNames()
            If formName.Length > 100 Then Continue For
            Dim exists = conn.ExecuteScalar(Of Integer)(
                "SELECT COUNT(*) FROM Forms WHERE FormName = @FormName",
                New With {.FormName = formName})
            If exists = 0 Then
                conn.Execute(
                    "INSERT INTO Forms (FormName, Description, DisplayTitle, DisplayTitleAr) VALUES (@FormName, @Description, @DisplayTitle, @DisplayTitleAr)",
                    New With {.FormName = formName, .Description = desc, .DisplayTitle = CType(Nothing, String), .DisplayTitleAr = CType(Nothing, String)})
                n += 1
            End If
        Next
        Return n
    End Function

End Module
