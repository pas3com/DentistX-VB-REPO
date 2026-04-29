Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports DevExpress.Xpo.DB.Helpers

Public Class DatabaseSynchronizer

    Private _logger As Logger

    'Private ReadOnly SourceConnectionString As String ' Dentist
    'Private ReadOnly TargetConnectionString As String ' DentistX
    Private SourceConnectionString As String = "Data Source=.;Initial Catalog=Dentist;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"
    Private TargetConnectionString As String = "Data Source=.;Initial Catalog=DentistX;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"

    Public Sub New(sourceConn As String, targetConn As String)
        SourceConnectionString = sourceConn
        TargetConnectionString = targetConn
    End Sub
    Public Sub New(sourceConn As String, targetConn As String, logger As Logger)
        SourceConnectionString = sourceConn
        TargetConnectionString = targetConn
        _logger = logger
    End Sub


    Public Sub SynchronizeDatabases()
        _logger.Log("Starting synchronization from Dentist → DentistX...")

        DisableAllForeignKeys()
        DisableAllTriggers()

        Using srcConn As New SqlConnection(SourceConnectionString),
          trgConn As New SqlConnection(TargetConnectionString)

            srcConn.Open()
            trgConn.Open()

            Dim tables As List(Of String) = GetTablesInDependencyOrder(trgConn)
            _logger.Log($"Found {tables.Count} tables in DentistX schema")

            Dim failedTables As New List(Of String)()
            Dim processedTables As Integer = 0

            For Each table In tables
                Try
                    _logger.Log($"Processing table: {table} ({processedTables + 1}/{tables.Count})")

                    If Not TableExists(srcConn, table) Then
                        _logger.Log($"Table {table} not found in Dentist. Creating...")
                        CreateTableInSource(srcConn, table)
                    Else
                        EnsureSchemaMatch(srcConn, trgConn, table)
                    End If

                    CopyData(srcConn, trgConn, table)
                    processedTables += 1

                Catch ex As Exception
                    _logger.Log($"❌ Failed to process table {table}: {ex.Message}")
                    failedTables.Add(table)
                    processedTables += 1
                End Try
            Next

            If failedTables.Any() Then
                _logger.Log($"Completed with {failedTables.Count} failed tables: {String.Join(", ", failedTables)}")
            Else
                _logger.Log("All tables processed successfully")
            End If
        End Using

        ' Check foreign key constraints before re-enabling
        CheckAndFixForeignKeys()

        EnableAllTriggers()

        _logger.Log("Synchronization completed.")
    End Sub

    Private Sub CheckAndFixForeignKeys()
        _logger.Log("Checking foreign key constraints...")

        Using conn As New SqlConnection(TargetConnectionString)
            conn.Open()

            ' Get all foreign key constraints that are violated
            Dim violatedFKs As New List(Of String)()
            Using cmd As New SqlCommand("
            SELECT 
                fk.name AS ForeignKeyName,
                OBJECT_NAME(fk.parent_object_id) AS TableName,
                COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS ColumnName,
                OBJECT_NAME(fk.referenced_object_id) AS ReferencedTableName,
                COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS ReferencedColumnName
            FROM sys.foreign_keys fk
            INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
            WHERE fk.is_disabled = 1", conn)

                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        violatedFKs.Add($"{rdr("ForeignKeyName")} ({rdr("TableName")}.{rdr("ColumnName")} -> {rdr("ReferencedTableName")}.{rdr("ReferencedColumnName")})")
                    End While
                End Using
            End Using

            If violatedFKs.Any() Then
                _logger.Log($"Found {violatedFKs.Count} foreign key constraints that may be violated:")
                For Each fk In violatedFKs
                    _logger.Log($"  - {fk}")
                Next

                ' Option 1: Enable constraints with NOCHECK (keep violated constraints disabled)
                _logger.Log("Enabling constraints with NOCHECK for violated FKs...")
                Using enableCmd As New SqlCommand("
                DECLARE @sql NVARCHAR(MAX) = ''
                SELECT @sql = @sql + 
                    'ALTER TABLE ' + 
                    QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' + 
                    QUOTENAME(OBJECT_NAME(parent_object_id)) + 
                    ' WITH NOCHECK CHECK CONSTRAINT ' + 
                    QUOTENAME(name) + ';' + CHAR(13)
                FROM sys.foreign_keys 
                WHERE is_disabled = 1
                EXEC sp_executesql @sql", conn)
                    enableCmd.ExecuteNonQuery()
                End Using
                _logger.Log("Foreign keys re-enabled with NOCHECK (violated constraints remain disabled)")
            Else
                ' Enable all constraints normally
                EnableAllForeignKeys()
            End If
        End Using
    End Sub



    Public Sub SynchronizeDatabases2()
        _logger.Log("Starting synchronization from Dentist → DentistX...")
        'EmptyAllTables()
        'Dim tables As List(Of String) = GetTableList(TargetConnectionString)
        '_logger.Log($"Found {tables.Count} tables in DentistX schema")

        DisableAllForeignKeys()
        DisableAllTriggers()

        Using srcConn As New SqlConnection(SourceConnectionString),
              trgConn As New SqlConnection(TargetConnectionString)

            srcConn.Open()
            trgConn.Open()
            ' Get tables in dependency order (parents first)
            Dim tables As List(Of String) = GetTablesInDependencyOrder(trgConn)

            _logger.Log($"Found {tables.Count} tables in DentistX schema")
            For Each table In tables
                _logger.Log($"Processing table: {table}")

                If Not TableExists(srcConn, table) Then
                    _logger.Log($"Table {table} not found in Dentist. Creating...")
                    CreateTableInSource(srcConn, table)
                Else
                    EnsureSchemaMatch(srcConn, trgConn, table)
                End If

                'Using disableCmd As New SqlCommand($"ALTER TABLE [{table}] NOCHECK CONSTRAINT ALL", trgConn)
                '    disableCmd.ExecuteNonQuery()
                'End Using

                CopyData(srcConn, trgConn, table)
                'Using enableCmd As New SqlCommand($"ALTER TABLE [{table}] WITH CHECK CHECK CONSTRAINT ALL", trgConn)
                '    enableCmd.ExecuteNonQuery()
                'End Using

            Next
        End Using

        EnableAllForeignKeys()
        EnableAllTriggers()

        _logger.Log("Synchronization completed successfully.")
    End Sub


    Public Sub SynchronizeDatabases3()
        _logger.Log("Starting synchronization from Dentist → DentistX...")

        DisableAllForeignKeys()
        DisableAllTriggers()

        Using srcConn As New SqlConnection(SourceConnectionString),
          trgConn As New SqlConnection(TargetConnectionString)

            srcConn.Open()
            trgConn.Open()

            Dim tables As List(Of String) = GetTablesInDependencyOrder(trgConn)
            _logger.Log($"Found {tables.Count} tables in DentistX schema")

            Dim failedTables As New List(Of String)()

            For Each table In tables
                Try
                    _logger.Log($"Processing table: {table}")

                    If Not TableExists(srcConn, table) Then
                        _logger.Log($"Table {table} not found in Dentist. Creating...")
                        CreateTableInSource(srcConn, table)
                    Else
                        EnsureSchemaMatch(srcConn, trgConn, table)
                    End If

                    CopyData(srcConn, trgConn, table)

                Catch ex As Exception
                    _logger.Log($"❌ Failed to process table {table}: {ex.Message}")
                    failedTables.Add(table)
                    ' Continue with next table
                End Try
            Next

            If failedTables.Any() Then
                _logger.Log($"Completed with {failedTables.Count} failed tables: {String.Join(", ", failedTables)}")
            Else
                _logger.Log("All tables processed successfully")
            End If
        End Using

        EnableAllForeignKeys()
        EnableAllTriggers()

        _logger.Log("Synchronization completed.")
    End Sub

#Region "Identity"

    ' Returns the schema name for the given table (defaults to dbo)
    Private Function GetTableSchema(conn As SqlConnection, tableName As String) As String
        Dim schemaName As String = "dbo"
        Using cmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @T", conn)
            cmd.Parameters.AddWithValue("@T", tableName)
            Dim s = cmd.ExecuteScalar()
            If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
        End Using
        Return schemaName
    End Function

    ' Enables IDENTITY_INSERT for the given table on the supplied open connection.
    ' Returns the identity column name if enabled, or Nothing if the table has no identity column.
    Private Function EnableIdentityInsert(conn As SqlConnection, tableName As String) As String
        Dim schemaName = GetTableSchema(conn, tableName)
        ' NOTE: OBJECT_ID requires a literal, so we build the string here (safe as schema/table are from catalog)
        Dim idSql As String = $"SELECT TOP 1 [name] FROM sys.columns WHERE object_id = OBJECT_ID(N'{schemaName}.{tableName}') AND is_identity = 1"
        Dim identityName As String = Nothing
        Using idCmd As New SqlCommand(idSql, conn)
            Dim res = idCmd.ExecuteScalar()
            If res IsNot Nothing AndAlso Not Convert.IsDBNull(res) Then identityName = res.ToString()
        End Using

        If String.IsNullOrEmpty(identityName) Then
            Return Nothing
        End If

        ' Safe bracketed name
        Dim safeSchema = schemaName.Replace("]", "]]")
        Dim safeTable = tableName.Replace("]", "]]")
        Dim bracketed = $"[{safeSchema}].[{safeTable}]"

        Using onCmd As New SqlCommand($"SET IDENTITY_INSERT {bracketed} ON", conn)
            onCmd.ExecuteNonQuery()
        End Using
        _logger?.Log($"  IDENTITY_INSERT ON for {bracketed}")
        Return identityName
    End Function

    ' Disables IDENTITY_INSERT for the given table on the supplied open connection.
    Private Sub DisableIdentityInsert(conn As SqlConnection, tableName As String)
        Dim schemaName = GetTableSchema(conn, tableName)
        Dim safeSchema = schemaName.Replace("]", "]]")
        Dim safeTable = tableName.Replace("]", "]]")
        Dim bracketed = $"[{safeSchema}].[{safeTable}]"
        Try
            Using offCmd As New SqlCommand($"SET IDENTITY_INSERT {bracketed} OFF", conn)
                offCmd.ExecuteNonQuery()
            End Using
            _logger?.Log($"  IDENTITY_INSERT OFF for {bracketed}")
        Catch ex As Exception
            _logger?.Log($"  Failed to disable IDENTITY_INSERT for {bracketed}: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Schema Operations"

    Private Function GetTablesInDependencyOrder(conn As SqlConnection) As List(Of String)
        Dim tables As New List(Of String)()

        ' Get tables in proper dependency order (tables without foreign keys first, then dependent tables)
        Dim sql = "
        WITH TableDependencies AS (
            SELECT 
                t.TABLE_NAME,
                CASE WHEN EXISTS (
                    SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc 
                    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu ON tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
                    WHERE tc.TABLE_NAME = t.TABLE_NAME AND tc.CONSTRAINT_TYPE = 'FOREIGN KEY'
                ) THEN 1 ELSE 0 END AS HasForeignKey,
                CASE WHEN EXISTS (
                    SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc 
                    WHERE tc.TABLE_NAME = t.TABLE_NAME AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
                ) THEN 1 ELSE 0 END AS HasPrimaryKey
            FROM INFORMATION_SCHEMA.TABLES t
            WHERE t.TABLE_TYPE = 'BASE TABLE'
        )
        SELECT TABLE_NAME
        FROM TableDependencies
        ORDER BY 
            HasForeignKey,  -- Tables without FKs first
            HasPrimaryKey DESC, -- Tables with PKs before those without
            TABLE_NAME"

        Using cmd As New SqlCommand(sql, conn)
            Using rdr = cmd.ExecuteReader()
                While rdr.Read()
                    tables.Add(rdr.GetString(0))
                End While
            End Using
        End Using

        _logger.Log($"Tables ordered by dependencies: {String.Join(" -> ", tables)}")
        Return tables
    End Function

    Private Function GetTablesInDependencyOrder3(conn As SqlConnection) As List(Of String)
        Dim tables As New List(Of String)()

        ' Get tables without foreign keys first, then tables with foreign keys
        Dim sql = "
        SELECT t.TABLE_NAME,
               CASE WHEN EXISTS (
                   SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc 
                   WHERE tc.TABLE_NAME = t.TABLE_NAME AND tc.CONSTRAINT_TYPE = 'FOREIGN KEY'
               ) THEN 1 ELSE 0 END AS HasForeignKey
        FROM INFORMATION_SCHEMA.TABLES t
        WHERE t.TABLE_TYPE = 'BASE TABLE'
        ORDER BY HasForeignKey, t.TABLE_NAME"

        Using cmd As New SqlCommand(sql, conn)
            Using rdr = cmd.ExecuteReader()
                While rdr.Read()
                    tables.Add(rdr.GetString(0))
                End While
            End Using
        End Using

        Return tables
    End Function



    Public Sub SynchronizeDatabases1()
        _logger.Log("Starting synchronization from Dentist → DentistX...")

        DisableAllForeignKeys()
        DisableAllTriggers()

        Using srcConn As New SqlConnection(SourceConnectionString),
          trgConn As New SqlConnection(TargetConnectionString)

            srcConn.Open()
            trgConn.Open()

            ' Get tables in dependency order (parents first)
            Dim tables As List(Of String) = GetTablesInDependencyOrder(trgConn)

            _logger.Log($"Found {tables.Count} tables in DentistX schema")

            For Each table In tables
                _logger.Log($"Processing table: {table}")
                ' ... rest of your existing table processing code
            Next
        End Using

        EnableAllForeignKeys()
        EnableAllTriggers()
        _logger.Log("Synchronization completed successfully.")
    End Sub

    Private Function GetTablesInDependencyOrder1(conn As SqlConnection) As List(Of String)
        Dim tables As New List(Of String)()

        ' Simple approach: Get tables without foreign keys first, then others
        Dim sql = "
        SELECT TABLE_NAME 
        FROM INFORMATION_SCHEMA.TABLES 
        WHERE TABLE_TYPE='BASE TABLE' 
        ORDER BY 
            CASE WHEN EXISTS (
                SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
                WHERE TABLE_NAME = INFORMATION_SCHEMA.TABLES.TABLE_NAME 
                AND CONSTRAINT_TYPE = 'FOREIGN KEY'
            ) THEN 1 ELSE 0 END,
            TABLE_NAME"

        Using cmd As New SqlCommand(sql, conn)
            Using rdr = cmd.ExecuteReader()
                While rdr.Read()
                    tables.Add(rdr.GetString(0))
                End While
            End Using
        End Using

        Return tables
    End Function
    Private Function GetTableList(connectionString As String) As List(Of String)
        Dim list As New List(Of String)
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Using cmd As New SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'", conn)
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        list.Add(rdr.GetString(0))
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    Private Function TableExists(conn As SqlConnection, tableName As String) As Boolean
        Dim cmd As New SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", conn)
        cmd.Parameters.AddWithValue("@T", tableName)
        Return CInt(cmd.ExecuteScalar()) > 0
    End Function

    Private Sub CreateTableInSource(srcConn As SqlConnection, tableName As String)
        Dim tableDef As String = GetTableDefinition(TargetConnectionString, tableName)
        If tableDef IsNot Nothing Then
            Using cmd As New SqlCommand(tableDef, srcConn)
                cmd.ExecuteNonQuery()
            End Using
            _logger.Log($"Created table {tableName} in Dentist.")
        End If
    End Sub

    Private Function GetTableDefinition(connStr As String, tableName As String) As String
        Dim sb As New StringBuilder()
        Using conn As New SqlConnection(connStr)
            conn.Open()
            Dim cmd As New SqlCommand($"sp_helptext 'dbo.{tableName}'", conn)
            Try
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        sb.Append(rdr.GetString(0))
                    End While
                End Using
            Catch ex As Exception
                ' sp_helptext won't always work for tables; fallback to dynamic creation
                Return GenerateCreateTableScript(conn, tableName)
            End Try
        End Using
        Return sb.ToString()
    End Function

    Private Function GenerateCreateTableScript(conn As SqlConnection, tableName As String) As String
        Dim script As New StringBuilder()
        script.AppendLine($"CREATE TABLE [{tableName}] (")

        Dim cmd As New SqlCommand("
            SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE 
            FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@T
        ", conn)
        cmd.Parameters.AddWithValue("@T", tableName)

        Dim defs As New List(Of String)
        Using rdr = cmd.ExecuteReader()
            While rdr.Read()
                Dim col = "[" & rdr("COLUMN_NAME").ToString() & "] " & rdr("DATA_TYPE").ToString()
                Dim len = If(IsDBNull(rdr("CHARACTER_MAXIMUM_LENGTH")), -1, CInt(rdr("CHARACTER_MAXIMUM_LENGTH")))
                If len > 0 AndAlso Not {"text", "ntext", "image"}.Contains(rdr("DATA_TYPE").ToString()) Then
                    col &= "(" & len & ")"
                End If
                If rdr("IS_NULLABLE").ToString() = "NO" Then
                    col &= " NOT NULL"
                End If
                defs.Add(col)
            End While
        End Using

        script.AppendLine(String.Join("," & vbCrLf, defs))
        script.AppendLine(")")
        Return script.ToString()
    End Function

    Private Sub EnsureSchemaMatch(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
        ' Compare columns and add missing ones in Dentist (source)
        Dim srcCols = GetColumnList(srcConn, tableName)
        Dim trgCols = GetColumnList(trgConn, tableName)

        For Each trgCol In trgCols
            If Not srcCols.Any(Function(c) c.Name.Equals(trgCol.Name, StringComparison.OrdinalIgnoreCase)) Then
                ' Handle data types with sizes (like nvarchar(50))
                Dim dataType As String = trgCol.DataType
                Dim maxLength As Integer = GetColumnLength(trgConn, tableName, trgCol.Name)

                If dataType.Contains("char") OrElse dataType.Contains("binary") Then
                    If maxLength > 0 AndAlso maxLength <> -1 Then
                        dataType &= $"({maxLength})"
                    ElseIf maxLength = -1 Then
                        dataType &= "(max)"
                    End If
                End If

                Dim alterSQL As String = $"ALTER TABLE [{tableName}] ADD [{trgCol.Name}] {dataType}"
                Try
                    Using cmd As New SqlCommand(alterSQL, srcConn)
                        cmd.ExecuteNonQuery()
                        _logger.Log($"Added missing column [{trgCol.Name}] ({dataType}) to Dentist.{tableName}")
                    End Using
                Catch ex As Exception
                    _logger.Log($"⚠️ Failed to add column [{trgCol.Name}] to Dentist.{tableName}: {ex.Message}")
                End Try
            End If
        Next
    End Sub



    Private Function GetColumnList(connection As SqlConnection, tableName As String) As List(Of ColumnInfo)
        Dim cols As New List(Of ColumnInfo)()
        Dim schema As String = "dbo"
        Using scmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", connection)
            scmd.Parameters.AddWithValue("@T", tableName)
            Dim s = scmd.ExecuteScalar()
            If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schema = s.ToString()
        End Using

        Dim sql As String = "
        SELECT COLUMN_NAME AS ColName, DATA_TYPE AS DataType
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T
        ORDER BY ORDINAL_POSITION"
        Using cmd As New SqlCommand(sql, connection)
            cmd.Parameters.AddWithValue("@S", schema)
            cmd.Parameters.AddWithValue("@T", tableName)
            Using rdr = cmd.ExecuteReader()
                While rdr.Read()
                    cols.Add(New ColumnInfo With {
                    .Name = rdr("ColName").ToString(),
                    .DataType = rdr("DataType").ToString()
                })
                End While
            End Using
        End Using
        Return cols
    End Function

    Private Class ColumnInfo1
        Public Property Name As String
        Public Property Type As String
    End Class


#End Region
    Public Sub EmptyAllTables()
        Dim connectionString As String = "Data Source=.;Initial Catalog=DentistX;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"

        Using conn As New SqlConnection(connectionString)
            conn.Open()

            ' Step 1: Get all user table names in DentistX
            Dim tableNames As New List(Of String)
            Using cmd As New SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'", conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        tableNames.Add(reader("TABLE_NAME").ToString())
                    End While
                End Using
            End Using

            ' Step 2: Disable foreign key constraints to avoid FK errors
            Using cmd As New SqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'", conn)
                cmd.ExecuteNonQuery()
            End Using

            ' Step 3: Empty each table
            For Each tableName In tableNames
                Using cmd As New SqlCommand($"DELETE FROM [{tableName}]", conn)
                    cmd.ExecuteNonQuery()
                End Using
            Next

            ' Step 4: Re-enable foreign key constraints
            Using cmd As New SqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'", conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        _logger.Log("All tables in DentistX have been emptied successfully! , Done")
    End Sub


#Region "Data CopyTest"
    Private Sub CopyData(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
        Try

            ' Special handling for tables known to have FK issues
            If tableName = "OrthoInf" Then
                _logger.Log($"Special handling for {tableName} to avoid FK issues")
                CopyDataWithFkCheck(srcConn, trgConn, tableName)
                Return
            End If

            ' --- Get schema name ---
            Dim schemaName As String = "dbo"
            Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
                schemaCmd.Parameters.AddWithValue("@T", tableName)
                Dim s = schemaCmd.ExecuteScalar()
                If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
            End Using

            _logger.Log($" Target {tableName} initially has {GetRowCount(trgConn, schemaName, tableName)} rows")

            ' --- Get ALL target column information ---
            Dim targetColumns As New List(Of ColumnInfo)()
            Using colsCmd As New SqlCommand("SELECT COLUMN_NAME, IS_NULLABLE, COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') as IsIdentity FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T ORDER BY ORDINAL_POSITION", trgConn)
                colsCmd.Parameters.AddWithValue("@S", schemaName)
                colsCmd.Parameters.AddWithValue("@T", tableName)
                Using rdr = colsCmd.ExecuteReader()
                    While rdr.Read()
                        targetColumns.Add(New ColumnInfo With {
                        .Name = rdr.GetString(0),
                        .IsNullable = rdr.GetString(1) = "YES",
                        .IsIdentity = Convert.ToInt32(rdr("IsIdentity")) = 1
                    })
                    End While
                End Using
            End Using

            ' --- Get ALL source column information ---
            Dim sourceColumns As New List(Of String)()
            Using srcColsCmd As New SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T ORDER BY ORDINAL_POSITION", srcConn)
                srcColsCmd.Parameters.AddWithValue("@S", schemaName)
                srcColsCmd.Parameters.AddWithValue("@T", tableName)
                Using rdr = srcColsCmd.ExecuteReader()
                    While rdr.Read()
                        sourceColumns.Add(rdr.GetString(0))
                    End While
                End Using
            End Using

            ' --- Build SELECT query using columns that exist in BOTH source and target ---
            Dim selectColumns As New List(Of String)()
            For Each targetCol In targetColumns
                ' Skip identity columns in target (let SQL Server generate new ones)
                If targetCol.IsIdentity Then
                    _logger.Log($" Excluding identity column '{targetCol.Name}'")
                    Continue For
                End If

                ' Only include columns that exist in source
                If sourceColumns.Contains(targetCol.Name) Then
                    selectColumns.Add($"[{targetCol.Name}]")
                Else
                    _logger.Log($" ⚠️ Column '{targetCol.Name}' exists in target but not in source")
                End If
            Next

            ' Check if we have required non-nullable columns
            Dim missingRequiredColumns = targetColumns.Where(Function(c) Not c.IsNullable AndAlso Not c.IsIdentity AndAlso Not sourceColumns.Contains(c.Name)).ToList()
            If missingRequiredColumns.Any() Then
                _logger.Log($" ❌ Cannot copy {tableName}: Missing required columns in source: {String.Join(", ", missingRequiredColumns.Select(Function(c) c.Name))}")
                Return
            End If

            ' If we have no columns to select, use fallback immediately
            If selectColumns.Count = 0 Then
                _logger.Log($" No common columns found, using fallback method")
                CopyDataFallback(srcConn, trgConn, tableName)
                Return
            End If

            Dim selectQuery As String = $"SELECT {String.Join(", ", selectColumns)} FROM [{schemaName}].[{tableName}]"

            _logger.Log($" Using {selectColumns.Count} columns for copy: {String.Join(", ", selectColumns)}")

            ' --- Read source data ---
            Dim data As New DataTable()
            Using srcCmd As New SqlCommand(selectQuery, srcConn)
                Using reader As SqlDataReader = srcCmd.ExecuteReader()
                    data.Load(reader)
                End Using
            End Using

            _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

            If data.Rows.Count = 0 Then
                _logger.Log($" No data to copy for {tableName}")
                Exit Sub
            End If

            ' --- Check for NULL values in non-nullable columns ---
            For Each targetCol In targetColumns
                If Not targetCol.IsNullable AndAlso Not targetCol.IsIdentity AndAlso data.Columns.Contains(targetCol.Name) Then
                    Dim nullRows = data.AsEnumerable().Where(Function(row) row.IsNull(targetCol.Name)).Count()
                    If nullRows > 0 Then
                        _logger.Log($" ⚠️ Found {nullRows} rows with NULL in required column '{targetCol.Name}', using fallback method")
                        CopyDataFallback(srcConn, trgConn, tableName)
                        Return
                    End If
                End If
            Next

            ' --- Clear target table ---
            Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
                Dim deletedRows = delCmd.ExecuteNonQuery()
                _logger.Log($" Cleared {deletedRows} rows from target")
            End Using

            ' --- Use SqlBulkCopy with safe column mapping ---
            Using localConn As New SqlConnection(trgConn.ConnectionString)
                localConn.Open()

                Using bulkCopy As New SqlBulkCopy(localConn)
                    bulkCopy.DestinationTableName = $"[{schemaName}].[{tableName}]"
                    bulkCopy.BatchSize = 1000
                    bulkCopy.BulkCopyTimeout = 600

                    ' Map all columns from our selected data
                    For Each column As DataColumn In data.Columns
                        bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName)
                    Next

                    bulkCopy.WriteToServer(data)
                End Using

                ' Verify insertion
                Dim finalCount = GetRowCount(localConn, schemaName, tableName)
                _logger.Log($" Immediate verification: {tableName} now has {finalCount} rows")
            End Using

            ' Final verification
            Dim totalCount = GetRowCount(trgConn, schemaName, tableName)
            _logger.Log($" Final count for {tableName}: {totalCount} rows")

            If totalCount = data.Rows.Count Then
                _logger.Log($" ✅ Successfully copied {data.Rows.Count} rows into DentistX.{tableName}")
            Else
                _logger.Log($" ⚠️ Copy incomplete: Expected {data.Rows.Count}, got {totalCount} rows")
            End If

        Catch ex As Exception
            _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")

            ' Try fallback method for problematic tables
            _logger.Log($" Attempting fallback method for {tableName}...")
            Try
                CopyDataFallback(srcConn, trgConn, tableName)
            Catch fallbackEx As Exception
                _logger.Log($" Fallback also failed: {fallbackEx.Message}")
                ' Don't throw - just log and continue with next table
                _logger.Log($" Skipping table {tableName} due to persistent errors")
            End Try
        End Try
    End Sub

    Private Sub CopyDataWithFkCheck1(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
        Try
            Dim schemaName As String = "dbo"
            Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
                schemaCmd.Parameters.AddWithValue("@T", tableName)
                Dim s = schemaCmd.ExecuteScalar()
                If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
            End Using

            ' Read source data
            Dim data As New DataTable()
            Using srcCmd As New SqlCommand($"SELECT * FROM [{schemaName}].[{tableName}]", srcConn)
                Using reader As SqlDataReader = srcCmd.ExecuteReader()
                    data.Load(reader)
                End Using
            End Using

            _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

            If data.Rows.Count = 0 Then Exit Sub

            ' Clear target
            Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
                delCmd.ExecuteNonQuery()
            End Using

            ' Insert rows one by one with FK validation
            Using localConn As New SqlConnection(trgConn.ConnectionString)
                localConn.Open()

                Dim successCount As Integer = 0
                Dim errorCount As Integer = 0

                For Each row As DataRow In data.Rows
                    Try
                        ' Build INSERT statement for this row
                        Dim columns As New List(Of String)()
                        Dim values As New List(Of String)()
                        Dim parameters As New List(Of SqlParameter)()

                        For Each col As DataColumn In data.Columns
                            If row.IsNull(col.ColumnName) Then
                                columns.Add($"[{col.ColumnName}]")
                                values.Add("NULL")
                            Else
                                columns.Add($"[{col.ColumnName}]")
                                values.Add($"@p{columns.Count}")
                                parameters.Add(New SqlParameter($"@p{columns.Count}", row(col.ColumnName)))
                            End If
                        Next

                        Dim insertSql = $"INSERT INTO [{schemaName}].[{tableName}] ({String.Join(", ", columns)}) VALUES ({String.Join(", ", values)})"
                        Using insertCmd As New SqlCommand(insertSql, localConn)
                            insertCmd.Parameters.AddRange(parameters.ToArray())
                            insertCmd.ExecuteNonQuery()
                            successCount += 1
                        End Using

                    Catch ex As Exception
                        _logger.Log($"  Skipped row due to FK violation: {ex.Message}")
                        errorCount += 1
                    End Try
                Next

                _logger.Log($" ✅ Copied {successCount} rows into DentistX.{tableName} ({errorCount} rows skipped due to FK issues)")
            End Using

        Catch ex As Exception
            _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")
        End Try
    End Sub


    Private Class ColumnInfo
        Public Property Name As String
        Public Property IsNullable As Boolean
        Public Property IsIdentity As Boolean
        Public Property DataType As String
    End Class

    ' Fixed fallback method with proper data type handling
    Private Sub CopyDataFallback(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
        _logger.Log($" Using enhanced fallback method for {tableName}")

        Dim schemaName As String = "dbo"
        Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
            schemaCmd.Parameters.AddWithValue("@T", tableName)
            Dim s = schemaCmd.ExecuteScalar()
            If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
        End Using

        ' Get target column information
        Dim targetColumns As New List(Of ColumnInfo)()
        Using colsCmd As New SqlCommand("SELECT COLUMN_NAME, IS_NULLABLE, DATA_TYPE, COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') as IsIdentity FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T ORDER BY ORDINAL_POSITION", trgConn)
            colsCmd.Parameters.AddWithValue("@S", schemaName)
            colsCmd.Parameters.AddWithValue("@T", tableName)
            Using rdr = colsCmd.ExecuteReader()
                While rdr.Read()
                    targetColumns.Add(New ColumnInfo With {
                    .Name = rdr.GetString(0),
                    .IsNullable = rdr.GetString(1) = "YES",
                    .DataType = rdr.GetString(2),
                    .IsIdentity = Convert.ToInt32(rdr("IsIdentity")) = 1
                })
                End While
            End Using
        End Using

        ' Clear target
        Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
            delCmd.ExecuteNonQuery()
        End Using

        ' Build SELECT query with all non-identity columns that exist in source
        Dim sourceColumns As New List(Of String)()
        Using srcColsCmd As New SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T ORDER BY ORDINAL_POSITION", srcConn)
            srcColsCmd.Parameters.AddWithValue("@S", schemaName)
            srcColsCmd.Parameters.AddWithValue("@T", tableName)
            Using rdr = srcColsCmd.ExecuteReader()
                While rdr.Read()
                    sourceColumns.Add(rdr.GetString(0))
                End While
            End Using
        End Using

        Dim selectColumns = targetColumns.Where(Function(c) Not c.IsIdentity AndAlso sourceColumns.Contains(c.Name)).Select(Function(c) $"[{c.Name}]").ToList()
        If selectColumns.Count = 0 Then
            _logger.Log($" No common columns found for fallback")
            Return
        End If

        Dim selectQuery = $"SELECT {String.Join(", ", selectColumns)} FROM [{schemaName}].[{tableName}]"

        ' Use simple INSERT statements with proper NULL handling
        Using srcCmd As New SqlCommand(selectQuery, srcConn)
            Using reader As SqlDataReader = srcCmd.ExecuteReader()
                Using localConn As New SqlConnection(trgConn.ConnectionString)
                    localConn.Open()

                    Dim rowsInserted As Integer = 0

                    While reader.Read()
                        ' Build dynamic INSERT statement
                        Dim columns As New List(Of String)()
                        Dim values As New List(Of String)()
                        Dim parameters As New List(Of SqlParameter)()
                        Dim paramIndex As Integer = 0

                        For Each targetCol In targetColumns
                            If targetCol.IsIdentity Then Continue For ' Skip identity columns
                            If Not sourceColumns.Contains(targetCol.Name) Then Continue For ' Skip columns not in source

                            Dim columnName = targetCol.Name
                            Dim paramName = $"@p{paramIndex}"

                            Try
                                ' Get column ordinal safely
                                Dim ordinal As Integer = -1
                                Try
                                    ordinal = reader.GetOrdinal(columnName)
                                Catch
                                    Continue For ' Column doesn't exist in reader
                                End Try

                                If reader.IsDBNull(ordinal) Then
                                    If targetCol.IsNullable Then
                                        columns.Add($"[{columnName}]")
                                        values.Add("NULL")
                                    Else
                                        ' Provide default value for non-nullable columns
                                        Select Case targetCol.DataType.ToLower()
                                            Case "int", "bigint", "smallint", "tinyint"
                                                columns.Add($"[{columnName}]")
                                                values.Add("0")
                                            Case "varchar", "nvarchar", "char", "nchar", "text", "ntext"
                                                columns.Add($"[{columnName}]")
                                                values.Add("''")
                                            Case "datetime", "date", "smalldatetime"
                                                columns.Add($"[{columnName}]")
                                                values.Add("GETDATE()")
                                            Case "bit"
                                                columns.Add($"[{columnName}]")
                                                values.Add("0")
                                            Case Else
                                                columns.Add($"[{columnName}]")
                                                values.Add("NULL")
                                        End Select
                                    End If
                                Else
                                    columns.Add($"[{columnName}]")
                                    values.Add(paramName)
                                    parameters.Add(New SqlParameter(paramName, reader.GetValue(ordinal)))
                                    paramIndex += 1
                                End If
                            Catch ex As Exception
                                _logger.Log($" Error processing column {columnName}: {ex.Message}")
                                ' Skip problematic columns
                            End Try
                        Next

                        If columns.Count > 0 Then
                            Try
                                Dim insertSql = $"INSERT INTO [{schemaName}].[{tableName}] ({String.Join(", ", columns)}) VALUES ({String.Join(", ", values)})"
                                Using insertCmd As New SqlCommand(insertSql, localConn)
                                    insertCmd.Parameters.AddRange(parameters.ToArray())
                                    insertCmd.ExecuteNonQuery()
                                    rowsInserted += 1
                                End Using
                            Catch insertEx As Exception
                                _logger.Log($" Error inserting row {rowsInserted + 1}: {insertEx.Message}")
                            End Try
                        End If
                    End While

                    _logger.Log($" Fallback completed for {tableName}: {rowsInserted} rows inserted")
                End Using
            End Using
        End Using
    End Sub

    ' Fixed fallback method
    Private Sub CopyDataFallback1(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
        _logger.Log($" Using enhanced fallback method for {tableName}")

        Dim schemaName As String = "dbo"
        Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
            schemaCmd.Parameters.AddWithValue("@T", tableName)
            Dim s = schemaCmd.ExecuteScalar()
            If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
        End Using

        ' Get target column information
        Dim targetColumns As New List(Of ColumnInfo)()
        Using colsCmd As New SqlCommand("SELECT COLUMN_NAME, IS_NULLABLE, DATA_TYPE, COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') as IsIdentity FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T ORDER BY ORDINAL_POSITION", trgConn)
            colsCmd.Parameters.AddWithValue("@S", schemaName)
            colsCmd.Parameters.AddWithValue("@T", tableName)
            Using rdr = colsCmd.ExecuteReader()
                While rdr.Read()
                    targetColumns.Add(New ColumnInfo With {
                    .Name = rdr.GetString(0),
                    .IsNullable = rdr.GetString(1) = "YES",
                    .DataType = rdr.GetString(2),
                    .IsIdentity = Convert.ToInt32(rdr("IsIdentity")) = 1
                })
                End While
            End Using
        End Using

        ' Clear target
        Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
            delCmd.ExecuteNonQuery()
        End Using

        ' Build SELECT query with all non-identity columns
        Dim selectColumns = targetColumns.Where(Function(c) Not c.IsIdentity).Select(Function(c) $"[{c.Name}]").ToList()
        Dim selectQuery = $"SELECT {String.Join(", ", selectColumns)} FROM [{schemaName}].[{tableName}]"

        ' Use simple INSERT statements with proper NULL handling
        Using srcCmd As New SqlCommand(selectQuery, srcConn)
            Using reader As SqlDataReader = srcCmd.ExecuteReader()
                Using localConn As New SqlConnection(trgConn.ConnectionString)
                    localConn.Open()

                    Dim rowsInserted As Integer = 0
                    Dim paramIndex As Integer = 0

                    While reader.Read()
                        ' Build dynamic INSERT statement
                        Dim columns As New List(Of String)()
                        Dim values As New List(Of String)()
                        Dim parameters As New List(Of SqlParameter)()

                        For Each targetCol In targetColumns
                            If targetCol.IsIdentity Then Continue For ' Skip identity columns

                            Dim columnName = targetCol.Name
                            Dim paramName = $"@p{paramIndex}"

                            Try
                                If reader.IsDBNull(columnName) Then
                                    If targetCol.IsNullable Then
                                        columns.Add($"[{columnName}]")
                                        values.Add("NULL")
                                    Else
                                        ' Provide default value for non-nullable columns
                                        Select Case targetCol.DataType.ToLower()
                                            Case "int", "bigint", "smallint", "tinyint"
                                                columns.Add($"[{columnName}]")
                                                values.Add("0")
                                            Case "varchar", "nvarchar", "char", "nchar", "text", "ntext"
                                                columns.Add($"[{columnName}]")
                                                values.Add("''")
                                            Case "datetime", "date", "smalldatetime"
                                                columns.Add($"[{columnName}]")
                                                values.Add("GETDATE()")
                                            Case "bit"
                                                columns.Add($"[{columnName}]")
                                                values.Add("0")
                                            Case Else
                                                columns.Add($"[{columnName}]")
                                                values.Add("NULL")
                                        End Select
                                    End If
                                Else
                                    columns.Add($"[{columnName}]")
                                    values.Add(paramName)
                                    parameters.Add(New SqlParameter(paramName, reader(columnName)))
                                    paramIndex += 1
                                End If
                            Catch ex As Exception
                                _logger.Log($" Error processing column {columnName}: {ex.Message}")
                                ' Skip problematic columns
                            End Try
                        Next

                        If columns.Count > 0 Then
                            Try
                                Dim insertSql = $"INSERT INTO [{schemaName}].[{tableName}] ({String.Join(", ", columns)}) VALUES ({String.Join(", ", values)})"
                                Using insertCmd As New SqlCommand(insertSql, localConn)
                                    insertCmd.Parameters.AddRange(parameters.ToArray())
                                    insertCmd.ExecuteNonQuery()
                                    rowsInserted += 1
                                End Using
                            Catch insertEx As Exception
                                _logger.Log($" Error inserting row {rowsInserted + 1}: {insertEx.Message}")
                            End Try
                        End If
                    End While

                    _logger.Log($" Fallback completed for {tableName}: {rowsInserted} rows inserted")
                End Using
            End Using
        End Using
    End Sub

    Private Function GetRowCount(conn As SqlConnection, schemaName As String, tableName As String) As Integer
        Using countCmd As New SqlCommand($"SELECT COUNT(*) FROM [{schemaName}].[{tableName}]", conn)
            Return CInt(countCmd.ExecuteScalar())
        End Using
    End Function

    Private Sub CopyDataWithFkCheck(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
        Try
            Dim schemaName As String = "dbo"
            Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
                schemaCmd.Parameters.AddWithValue("@T", tableName)
                Dim s = schemaCmd.ExecuteScalar()
                If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
            End Using

            ' Check if table has identity column
            Dim hasIdentity As Boolean = False
            Using identityCmd As New SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T AND COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1", trgConn)
                identityCmd.Parameters.AddWithValue("@S", schemaName)
                identityCmd.Parameters.AddWithValue("@T", tableName)
                hasIdentity = CInt(identityCmd.ExecuteScalar()) > 0
            End Using

            ' Read source data
            Dim data As New DataTable()
            Using srcCmd As New SqlCommand($"SELECT * FROM [{schemaName}].[{tableName}]", srcConn)
                Using reader As SqlDataReader = srcCmd.ExecuteReader()
                    data.Load(reader)
                End Using
            End Using

            _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

            If data.Rows.Count = 0 Then Exit Sub

            ' Clear target
            Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
                delCmd.ExecuteNonQuery()
            End Using

            ' Insert rows one by one with FK validation
            Using localConn As New SqlConnection(trgConn.ConnectionString)
                localConn.Open()

                ' Enable identity insert if needed
                If hasIdentity Then
                    Using enableCmd As New SqlCommand($"SET IDENTITY_INSERT [{schemaName}].[{tableName}] ON", localConn)
                        enableCmd.ExecuteNonQuery()
                    End Using
                End If

                Dim successCount As Integer = 0
                Dim errorCount As Integer = 0

                For Each row As DataRow In data.Rows
                    Try
                        ' Build INSERT statement for this row
                        Dim columns As New List(Of String)()
                        Dim values As New List(Of String)()
                        Dim parameters As New List(Of SqlParameter)()

                        For Each col As DataColumn In data.Columns
                            If row.IsNull(col.ColumnName) Then
                                columns.Add($"[{col.ColumnName}]")
                                values.Add("NULL")
                            Else
                                columns.Add($"[{col.ColumnName}]")
                                values.Add($"@p{columns.Count}")
                                parameters.Add(New SqlParameter($"@p{columns.Count}", row(col.ColumnName)))
                            End If
                        Next

                        Dim insertSql = $"INSERT INTO [{schemaName}].[{tableName}] ({String.Join(", ", columns)}) VALUES ({String.Join(", ", values)})"
                        Using insertCmd As New SqlCommand(insertSql, localConn)
                            insertCmd.Parameters.AddRange(parameters.ToArray())
                            insertCmd.ExecuteNonQuery()
                            successCount += 1
                        End Using

                    Catch ex As Exception
                        _logger.Log($"  Skipped row due to error: {ex.Message}")
                        errorCount += 1
                    End Try
                Next

                ' Disable identity insert if it was enabled
                If hasIdentity Then
                    Using disableCmd As New SqlCommand($"SET IDENTITY_INSERT [{schemaName}].[{tableName}] OFF", localConn)
                        disableCmd.ExecuteNonQuery()
                    End Using
                End If

                _logger.Log($" ✅ Copied {successCount} rows into DentistX.{tableName} ({errorCount} rows skipped due to errors)")
            End Using

        Catch ex As Exception
            _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")
        End Try
    End Sub

#End Region


    '#Region "Data Copy"

    '    Private Sub CopyData(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '        Try
    '            ' --- Get schema name ---
    '            Dim schemaName As String = "dbo"
    '            Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
    '                schemaCmd.Parameters.AddWithValue("@T", tableName)
    '                Dim s = schemaCmd.ExecuteScalar()
    '                If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '            End Using

    '            _logger.Log($" Target {tableName} initially has {GetRowCount(trgConn, schemaName, tableName)} rows")

    '            ' --- Get ALL target column information (including nullability) ---
    '            Dim targetColumns As New List(Of ColumnInfo)()
    '            Using colsCmd As New SqlCommand("SELECT COLUMN_NAME, IS_NULLABLE, COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') as IsIdentity FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T ORDER BY ORDINAL_POSITION", trgConn)
    '                colsCmd.Parameters.AddWithValue("@S", schemaName)
    '                colsCmd.Parameters.AddWithValue("@T", tableName)
    '                Using rdr = colsCmd.ExecuteReader()
    '                    While rdr.Read()
    '                        targetColumns.Add(New ColumnInfo With {
    '                        .Name = rdr.GetString(0),
    '                        .IsNullable = rdr.GetString(1) = "YES",
    '                        .IsIdentity = Convert.ToInt32(rdr("IsIdentity")) = 1
    '                    })
    '                    End While
    '                End Using
    '            End Using

    '            ' --- Get ALL source column information ---
    '            Dim sourceColumns As New List(Of String)()
    '            Using srcColsCmd As New SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T ORDER BY ORDINAL_POSITION", srcConn)
    '                srcColsCmd.Parameters.AddWithValue("@S", schemaName)
    '                srcColsCmd.Parameters.AddWithValue("@T", tableName)
    '                Using rdr = srcColsCmd.ExecuteReader()
    '                    While rdr.Read()
    '                        sourceColumns.Add(rdr.GetString(0))
    '                    End While
    '                End Using
    '            End Using

    '            ' --- Build SELECT query using columns that exist in BOTH source and target, excluding target identity columns ---
    '            Dim selectColumns As New List(Of String)()
    '            For Each targetCol In targetColumns
    '                ' Skip identity columns in target
    '                If targetCol.IsIdentity Then
    '                    _logger.Log($" Excluding identity column '{targetCol.Name}'")
    '                    Continue For
    '                End If

    '                ' Only include columns that exist in source
    '                If sourceColumns.Contains(targetCol.Name) Then
    '                    selectColumns.Add($"[{targetCol.Name}]")
    '                Else
    '                    _logger.Log($" ⚠️ Column '{targetCol.Name}' exists in target but not in source")
    '                End If
    '            Next

    '            ' Check if we have required non-nullable columns
    '            Dim missingRequiredColumns = targetColumns.Where(Function(c) Not c.IsNullable AndAlso Not c.IsIdentity AndAlso Not sourceColumns.Contains(c.Name)).ToList()
    '            If missingRequiredColumns.Any() Then
    '                _logger.Log($" ❌ Cannot copy {tableName}: Missing required columns in source: {String.Join(", ", missingRequiredColumns.Select(Function(c) c.Name))}")
    '                Return
    '            End If

    '            Dim selectQuery As String = $"SELECT {String.Join(", ", selectColumns)} FROM [{schemaName}].[{tableName}]"

    '            _logger.Log($" Using {selectColumns.Count} columns for copy: {String.Join(", ", selectColumns)}")

    '            ' --- Read source data ---
    '            Dim data As New DataTable()
    '            Using srcCmd As New SqlCommand(selectQuery, srcConn)
    '                Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                    data.Load(reader)
    '                End Using
    '            End Using

    '            _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

    '            If data.Rows.Count = 0 Then
    '                _logger.Log($" No data to copy for {tableName}")
    '                Exit Sub
    '            End If

    '            ' --- Clear target table ---
    '            Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
    '                Dim deletedRows = delCmd.ExecuteNonQuery()
    '                _logger.Log($" Cleared {deletedRows} rows from target")
    '            End Using

    '            ' --- Use SqlBulkCopy with safe column mapping ---
    '            Using localConn As New SqlConnection(trgConn.ConnectionString)
    '                localConn.Open()

    '                Using bulkCopy As New SqlBulkCopy(localConn)
    '                    bulkCopy.DestinationTableName = $"[{schemaName}].[{tableName}]"
    '                    bulkCopy.BatchSize = 1000
    '                    bulkCopy.BulkCopyTimeout = 600

    '                    ' Map all columns from our selected data
    '                    For Each column As DataColumn In data.Columns
    '                        bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName)
    '                    Next

    '                    bulkCopy.WriteToServer(data)
    '                End Using

    '                ' Verify insertion
    '                Dim finalCount = GetRowCount(localConn, schemaName, tableName)
    '                _logger.Log($" Immediate verification: {tableName} now has {finalCount} rows")
    '            End Using

    '            ' Final verification
    '            Dim totalCount = GetRowCount(trgConn, schemaName, tableName)
    '            _logger.Log($" Final count for {tableName}: {totalCount} rows")

    '            If totalCount = data.Rows.Count Then
    '                _logger.Log($" ✅ Successfully copied {data.Rows.Count} rows into DentistX.{tableName}")
    '            Else
    '                _logger.Log($" ⚠️ Copy incomplete: Expected {data.Rows.Count}, got {totalCount} rows")
    '            End If

    '        Catch ex As Exception
    '            _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")

    '            ' Try fallback method for problematic tables
    '            _logger.Log($" Attempting fallback method for {tableName}...")
    '            Try
    '                CopyDataFallback(srcConn, trgConn, tableName)
    '            Catch fallbackEx As Exception
    '                _logger.Log($" Fallback also failed: {fallbackEx.Message}")
    '                Throw
    '            End Try
    '        End Try
    '    End Sub

    '    Private Class ColumnInfo
    '        Public Property Name As String
    '        Public Property DataType As String
    '        Public Property IsNullable As Boolean
    '        Public Property IsIdentity As Boolean
    '    End Class

    '    ' Improved fallback method that handles NULL values properly
    '    Private Sub CopyDataFallback(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '        _logger.Log($" Using fallback method for {tableName}")

    '        Dim schemaName As String = "dbo"
    '        Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
    '            schemaCmd.Parameters.AddWithValue("@T", tableName)
    '            Dim s = schemaCmd.ExecuteScalar()
    '            If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '        End Using

    '        ' Get target column information for proper NULL handling
    '        Dim targetColumns As New List(Of ColumnInfo)()
    '        Using colsCmd As New SqlCommand("SELECT COLUMN_NAME, IS_NULLABLE, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T ORDER BY ORDINAL_POSITION", trgConn)
    '            colsCmd.Parameters.AddWithValue("@S", schemaName)
    '            colsCmd.Parameters.AddWithValue("@T", tableName)
    '            Using rdr = colsCmd.ExecuteReader()
    '                While rdr.Read()
    '                    targetColumns.Add(New ColumnInfo With {
    '                    .Name = rdr.GetString(0),
    '                    .IsNullable = rdr.GetString(1) = "YES",
    '                    .DataType = rdr.GetString(2)
    '                })
    '                End While
    '            End Using
    '        End Using

    '        ' Clear target
    '        Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
    '            delCmd.ExecuteNonQuery()
    '        End Using

    '        ' Use simple INSERT statements with proper NULL handling
    '        Using srcCmd As New SqlCommand($"SELECT * FROM [{schemaName}].[{tableName}]", srcConn)
    '            Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                Using localConn As New SqlConnection(trgConn.ConnectionString)
    '                    localConn.Open()

    '                    Dim rowsInserted As Integer = 0

    '                    While reader.Read()
    '                        ' Build dynamic INSERT statement
    '                        Dim columns As New List(Of String)()
    '                        Dim values As New List(Of String)()
    '                        Dim parameters As New List(Of SqlParameter)()

    '                        For Each targetCol In targetColumns
    '                            Dim columnName = targetCol.Name
    '                            Dim paramName = $"@p{columns.Count}"

    '                            If reader.IsDBNull(columnName) Then
    '                                If targetCol.IsNullable Then
    '                                    columns.Add($"[{columnName}]")
    '                                    values.Add("NULL")
    '                                Else
    '                                    ' Provide default value for non-nullable columns
    '                                    Select Case targetCol.DataType.ToLower()
    '                                        Case "int", "bigint", "smallint", "tinyint"
    '                                            columns.Add($"[{columnName}]")
    '                                            values.Add("0")
    '                                        Case "varchar", "nvarchar", "char", "nchar", "text", "ntext"
    '                                            columns.Add($"[{columnName}]")
    '                                            values.Add("''")
    '                                        Case "datetime", "date", "smalldatetime"
    '                                            columns.Add($"[{columnName}]")
    '                                            values.Add("GETDATE()")
    '                                        Case "bit"
    '                                            columns.Add($"[{columnName}]")
    '                                            values.Add("0")
    '                                        Case Else
    '                                            columns.Add($"[{columnName}]")
    '                                            values.Add("NULL")
    '                                    End Select
    '                                End If
    '                            Else
    '                                columns.Add($"[{columnName}]")
    '                                values.Add(paramName)
    '                                parameters.Add(New SqlParameter(paramName, reader(columnName)))
    '                            End If
    '                        Next

    '                        If columns.Count > 0 Then
    '                            Dim insertSql = $"INSERT INTO [{schemaName}].[{tableName}] ({String.Join(", ", columns)}) VALUES ({String.Join(", ", values)})"
    '                            Using insertCmd As New SqlCommand(insertSql, localConn)
    '                                ' Only add parameters for non-NULL values
    '                                For Each param In parameters
    '                                    insertCmd.Parameters.Add(param)
    '                                Next
    '                                insertCmd.ExecuteNonQuery()
    '                                rowsInserted += 1
    '                            End Using
    '                        End If
    '                    End While

    '                    _logger.Log($" Fallback completed for {tableName}: {rowsInserted} rows inserted")
    '                End Using
    '            End Using
    '        End Using
    '    End Sub

    '    Private Function GetRowCount(conn As SqlConnection, schemaName As String, tableName As String) As Integer
    '        Using countCmd As New SqlCommand($"SELECT COUNT(*) FROM [{schemaName}].[{tableName}]", conn)
    '            Return CInt(countCmd.ExecuteScalar())
    '        End Using
    '    End Function




    '    'Private Sub CopyData(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '    '    Try
    '    '        ' --- Get schema name ---
    '    '        Dim schemaName As String = "dbo"
    '    '        Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
    '    '            schemaCmd.Parameters.AddWithValue("@T", tableName)
    '    '            Dim s = schemaCmd.ExecuteScalar()
    '    '            If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '    '        End Using

    '    '        _logger.Log($" Target {tableName} initially has {GetRowCount(trgConn, schemaName, tableName)} rows")

    '    '        ' --- Check if target has identity column ---
    '    '        Dim targetIdentityColumn As String = Nothing
    '    '        Using identityCmd As New SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T AND COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1", trgConn)
    '    '            identityCmd.Parameters.AddWithValue("@S", schemaName)
    '    '            identityCmd.Parameters.AddWithValue("@T", tableName)
    '    '            Dim result = identityCmd.ExecuteScalar()
    '    '            If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
    '    '                targetIdentityColumn = result.ToString()
    '    '            End If
    '    '        End Using

    '    '        ' --- Get target column list (excluding identity) ---
    '    '        Dim targetColumns As New List(Of String)()
    '    '        Using colsCmd As New SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T AND COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 0 ORDER BY ORDINAL_POSITION", trgConn)
    '    '            colsCmd.Parameters.AddWithValue("@S", schemaName)
    '    '            colsCmd.Parameters.AddWithValue("@T", tableName)
    '    '            Using rdr = colsCmd.ExecuteReader()
    '    '                While rdr.Read()
    '    '                    targetColumns.Add(rdr.GetString(0))
    '    '                End While
    '    '            End Using
    '    '        End Using

    '    '        ' --- Build SELECT query using ONLY columns that exist in target ---
    '    '        Dim selectColumns As New List(Of String)()
    '    '        For Each targetCol In targetColumns
    '    '            selectColumns.Add($"[{targetCol}]")
    '    '        Next

    '    '        Dim selectQuery As String
    '    '        If selectColumns.Count > 0 Then
    '    '            selectQuery = $"SELECT {String.Join(", ", selectColumns)} FROM [{schemaName}].[{tableName}]"
    '    '        Else
    '    '            selectQuery = $"SELECT * FROM [{schemaName}].[{tableName}]"
    '    '        End If

    '    '        If Not String.IsNullOrEmpty(targetIdentityColumn) Then
    '    '            _logger.Log($" Excluding identity column '{targetIdentityColumn}' from source data")
    '    '        End If
    '    '        _logger.Log($" Using {selectColumns.Count} columns for copy")

    '    '        ' --- Read source data ---
    '    '        Dim data As New DataTable()
    '    '        Using srcCmd As New SqlCommand(selectQuery, srcConn)
    '    '            Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '    '                data.Load(reader)
    '    '            End Using
    '    '        End Using

    '    '        _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

    '    '        If data.Rows.Count = 0 Then
    '    '            _logger.Log($" No data to copy for {tableName}")
    '    '            Exit Sub
    '    '        End If

    '    '        ' --- Clear target table ---
    '    '        Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
    '    '            Dim deletedRows = delCmd.ExecuteNonQuery()
    '    '            _logger.Log($" Cleared {deletedRows} rows from target")
    '    '        End Using

    '    '        ' --- Use SqlBulkCopy with safe column mapping ---
    '    '        Using localConn As New SqlConnection(trgConn.ConnectionString)
    '    '            localConn.Open()

    '    '            Using bulkCopy As New SqlBulkCopy(localConn)
    '    '                bulkCopy.DestinationTableName = $"[{schemaName}].[{tableName}]"
    '    '                bulkCopy.BatchSize = 1000
    '    '                bulkCopy.BulkCopyTimeout = 600

    '    '                ' Map only columns that exist in both source DataTable and target table
    '    '                For Each column As DataColumn In data.Columns
    '    '                    If targetColumns.Contains(column.ColumnName) Then
    '    '                        bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName)
    '    '                    Else
    '    '                        _logger.Log($"  Skipping column '{column.ColumnName}' - not found in target table")
    '    '                    End If
    '    '                Next

    '    '                ' Double-check we have at least one column mapping
    '    '                If bulkCopy.ColumnMappings.Count = 0 Then
    '    '                    _logger.Log($" ⚠️ No valid column mappings for {tableName}. Using default mapping.")
    '    '                    bulkCopy.WriteToServer(data) ' Let SqlBulkCopy try to auto-map
    '    '                Else
    '    '                    bulkCopy.WriteToServer(data)
    '    '                End If
    '    '            End Using

    '    '            ' Verify insertion
    '    '            Dim finalCount = GetRowCount(localConn, schemaName, tableName)
    '    '            _logger.Log($" Immediate verification: {tableName} now has {finalCount} rows")
    '    '        End Using

    '    '        ' Final verification
    '    '        Dim totalCount = GetRowCount(trgConn, schemaName, tableName)
    '    '        _logger.Log($" Final count for {tableName}: {totalCount} rows")

    '    '        If totalCount = data.Rows.Count Then
    '    '            _logger.Log($" ✅ Successfully copied {data.Rows.Count} rows into DentistX.{tableName}")
    '    '        Else
    '    '            _logger.Log($" ⚠️ Copy incomplete: Expected {data.Rows.Count}, got {totalCount} rows")
    '    '        End If

    '    '    Catch ex As Exception
    '    '        _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")

    '    '        ' Try fallback method for problematic tables
    '    '        If ex.Message.Contains("ColumnMapping") Then
    '    '            _logger.Log($" Attempting fallback method for {tableName}...")
    '    '            Try
    '    '                CopyDataFallback(srcConn, trgConn, tableName)
    '    '                Exit Sub
    '    '            Catch fallbackEx As Exception
    '    '                _logger.Log($" Fallback also failed: {fallbackEx.Message}")
    '    '            End Try
    '    '        End If

    '    '        Throw
    '    '    End Try
    '    'End Sub

    '    '' Fallback method for tables with column mapping issues
    '    'Private Sub CopyDataFallback(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '    '    _logger.Log($" Using fallback method for {tableName}")

    '    '    Dim schemaName As String = "dbo"
    '    '    Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
    '    '        schemaCmd.Parameters.AddWithValue("@T", tableName)
    '    '        Dim s = schemaCmd.ExecuteScalar()
    '    '        If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '    '    End Using

    '    '    ' Clear target
    '    '    Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
    '    '        delCmd.ExecuteNonQuery()
    '    '    End Using

    '    '    ' Use simple INSERT statements for each row
    '    '    Using srcCmd As New SqlCommand($"SELECT * FROM [{schemaName}].[{tableName}]", srcConn)
    '    '        Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '    '            Using localConn As New SqlConnection(trgConn.ConnectionString)
    '    '                localConn.Open()

    '    '                While reader.Read()
    '    '                    ' Build dynamic INSERT statement
    '    '                    Dim columns As New List(Of String)()
    '    '                    Dim values As New List(Of String)()
    '    '                    Dim parameters As New List(Of SqlParameter)()

    '    '                    For i As Integer = 0 To reader.FieldCount - 1
    '    '                        Dim columnName = reader.GetName(i)
    '    '                        If Not reader.IsDBNull(i) Then
    '    '                            columns.Add($"[{columnName}]")
    '    '                            values.Add($"@p{i}")
    '    '                            parameters.Add(New SqlParameter($"@p{i}", reader.GetValue(i)))
    '    '                        End If
    '    '                    Next

    '    '                    If columns.Count > 0 Then
    '    '                        Dim insertSql = $"INSERT INTO [{schemaName}].[{tableName}] ({String.Join(", ", columns)}) VALUES ({String.Join(", ", values)})"
    '    '                        Using insertCmd As New SqlCommand(insertSql, localConn)
    '    '                            insertCmd.Parameters.AddRange(parameters.ToArray())
    '    '                            insertCmd.ExecuteNonQuery()
    '    '                        End Using
    '    '                    End If
    '    '                End While
    '    '            End Using
    '    '        End Using
    '    '    End Using

    '    '    _logger.Log($" Fallback completed for {tableName}")
    '    'End Sub

    '    'Private Function GetRowCount(conn As SqlConnection, schemaName As String, tableName As String) As Integer
    '    '    Using countCmd As New SqlCommand($"SELECT COUNT(*) FROM [{schemaName}].[{tableName}]", conn)
    '    '        Return CInt(countCmd.ExecuteScalar())
    '    '    End Using
    '    'End Function



    '    Private Sub CopyDataWorkingVersion1(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '        Try
    '            ' --- Get schema name ---
    '            Dim schemaName As String = "dbo"
    '            Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
    '                schemaCmd.Parameters.AddWithValue("@T", tableName)
    '                Dim s = schemaCmd.ExecuteScalar()
    '                If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '            End Using

    '            ' --- Check current row count in target BEFORE delete ---
    '            Dim initialCount As Integer = 0
    '            Using countCmd As New SqlCommand($"SELECT COUNT(*) FROM [{schemaName}].[{tableName}]", trgConn)
    '                initialCount = CInt(countCmd.ExecuteScalar())
    '            End Using
    '            _logger.Log($" Target {tableName} initially has {initialCount} rows")

    '            ' --- Clear target table ---
    '            Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
    '                Dim deletedRows = delCmd.ExecuteNonQuery()
    '                _logger.Log($" Deleted {deletedRows} rows from target {tableName}")
    '            End Using

    '            ' --- Read source data ---
    '            Dim selectSQL As String = $"SELECT * FROM [{schemaName}].[{tableName}]"
    '            Dim data As New DataTable()

    '            Using srcCmd As New SqlCommand(selectSQL, srcConn)
    '                Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                    data.Load(reader)
    '                End Using
    '            End Using

    '            _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

    '            If data.Rows.Count = 0 Then
    '                _logger.Log($" No data to copy for {tableName}")
    '                Exit Sub
    '            End If

    '            ' --- Insert data using SqlDataAdapter ---
    '            Using localConn As New SqlConnection(trgConn.ConnectionString)
    '                localConn.Open()

    '                ' Test connection by inserting one row manually first
    '                _logger.Log($" Testing insert for {tableName}...")

    '                Using adapter As New SqlDataAdapter($"SELECT * FROM [{schemaName}].[{tableName}] WHERE 1=0", localConn)
    '                    Using builder As New SqlCommandBuilder(adapter)
    '                        builder.ConflictOption = ConflictOption.OverwriteChanges
    '                        adapter.InsertCommand = builder.GetInsertCommand()

    '                        ' Set up event handler to see what's happening
    '                        AddHandler adapter.RowUpdated, Sub(sender, e)
    '                                                           If e.Status = UpdateStatus.ErrorsOccurred Then
    '                                                               _logger.Log($" Error inserting row: {e.Errors.Message}")
    '                                                           ElseIf e.Status = UpdateStatus.Continue Then
    '                                                               _logger.Log($" Successfully inserted row {e.RecordsAffected}")
    '                                                           End If
    '                                                       End Sub

    '                        Dim rowsAffected = adapter.Update(data)
    '                        _logger.Log($" SqlDataAdapter reports {rowsAffected} rows affected")
    '                    End Using
    '                End Using

    '                ' Verify insertion immediately
    '                Using verifyCmd As New SqlCommand($"SELECT COUNT(*) FROM [{schemaName}].[{tableName}]", localConn)
    '                    Dim finalCount = CInt(verifyCmd.ExecuteScalar())
    '                    _logger.Log($" Immediate verification: {tableName} now has {finalCount} rows")
    '                End Using
    '            End Using

    '            ' --- Final verification ---
    '            Using finalCmd As New SqlCommand($"SELECT COUNT(*) FROM [{schemaName}].[{tableName}]", trgConn)
    '                Dim finalCount = CInt(finalCmd.ExecuteScalar())
    '                _logger.Log($" Final count for {tableName}: {finalCount} rows")
    '            End Using

    '            _logger.Log($" ✅ Copied {data.Rows.Count} rows into DentistX.{tableName}")

    '        Catch ex As Exception
    '            _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")
    '            _logger.Log($" Stack trace: {ex.StackTrace}")
    '            Throw
    '        End Try
    '    End Sub

    '    Private Sub CopyDataWorkingVersion(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '        Try
    '            ' --- Simple approach: Use SqlDataAdapter with proper INSERT commands ---
    '            Dim schemaName As String = "dbo"
    '            Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
    '                schemaCmd.Parameters.AddWithValue("@T", tableName)
    '                Dim s = schemaCmd.ExecuteScalar()
    '                If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '            End Using

    '            ' Clear target table
    '            Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
    '                delCmd.ExecuteNonQuery()
    '            End Using

    '            ' Use SqlDataAdapter which handles identity columns automatically
    '            Dim selectSQL As String = $"SELECT * FROM [{schemaName}].[{tableName}]"
    '            Dim data As New DataTable()

    '            Using srcCmd As New SqlCommand(selectSQL, srcConn)
    '                Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                    data.Load(reader)
    '                End Using
    '            End Using

    '            If data.Rows.Count > 0 Then
    '                Using localConn As New SqlConnection(trgConn.ConnectionString)
    '                    localConn.Open()

    '                    Using adapter As New SqlDataAdapter($"SELECT * FROM [{schemaName}].[{tableName}] WHERE 1=0", localConn)
    '                        Using builder As New SqlCommandBuilder(adapter)
    '                            adapter.InsertCommand = builder.GetInsertCommand()
    '                            adapter.Update(data)
    '                        End Using
    '                    End Using
    '                End Using
    '            End If

    '            _logger.Log($" ✅ Copied {data.Rows.Count} rows into DentistX.{tableName}")

    '        Catch ex As Exception
    '            _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")
    '            Throw
    '        End Try
    '    End Sub


    '    Private Sub CopyData9(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '        Try
    '            ' --- 1️⃣ Get schema name ---
    '            Dim schemaName As String = "dbo"
    '            Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
    '                schemaCmd.Parameters.AddWithValue("@T", tableName)
    '                Dim s = schemaCmd.ExecuteScalar()
    '                If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '            End Using

    '            ' --- 2️⃣ Check if target table has identity column ---
    '            Dim targetHasIdentity As Boolean = False
    '            Dim identityColumnName As String = Nothing
    '            Using idCmd As New SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T AND COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1", trgConn)
    '                idCmd.Parameters.AddWithValue("@S", schemaName)
    '                idCmd.Parameters.AddWithValue("@T", tableName)
    '                Dim result = idCmd.ExecuteScalar()
    '                If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
    '                    targetHasIdentity = True
    '                    identityColumnName = result.ToString()
    '                End If
    '            End Using

    '            ' --- 3️⃣ Build column list for SELECT query (exclude identity column from source if target has identity) ---
    '            Dim columnList As String
    '            If targetHasIdentity Then
    '                ' Get all non-identity columns from source
    '                Using colCmd As New SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T AND COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 0", srcConn)
    '                    colCmd.Parameters.AddWithValue("@S", schemaName)
    '                    colCmd.Parameters.AddWithValue("@T", tableName)
    '                    Using rdr = colCmd.ExecuteReader()
    '                        Dim columns As New List(Of String)()
    '                        While rdr.Read()
    '                            columns.Add($"[{rdr.GetString(0)}]")
    '                        End While
    '                        columnList = String.Join(", ", columns)
    '                    End Using
    '                End Using
    '            Else
    '                columnList = "*"
    '            End If

    '            ' --- 4️⃣ Read source data ---
    '            Dim selectSQL As String = $"SELECT {columnList} FROM [{schemaName}].[{tableName}]"
    '            Dim data As New DataTable()
    '            Using srcCmd As New SqlCommand(selectSQL, srcConn)
    '                Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                    data.Load(reader)
    '                End Using
    '            End Using
    '            _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

    '            If data.Rows.Count = 0 Then Exit Sub

    '            ' --- 5️⃣ Delete all target rows before insert ---
    '            Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
    '                delCmd.ExecuteNonQuery()
    '            End Using

    '            ' --- 6️⃣ Open local connection for bulk copy ---
    '            Using localConn As New SqlConnection(trgConn.ConnectionString)
    '                localConn.Open()

    '                ' --- 7️⃣ Handle identity insert if target has identity column ---
    '                If targetHasIdentity Then
    '                    ' Enable identity insert to allow explicit values
    '                    Using onCmd As New SqlCommand($"SET IDENTITY_INSERT [{schemaName}].[{tableName}] ON", localConn)
    '                        onCmd.ExecuteNonQuery()
    '                    End Using
    '                    _logger.Log($"  IDENTITY_INSERT ON for [{schemaName}].[{tableName}]")
    '                End If

    '                ' --- 8️⃣ Bulk copy ---
    '                Using bulkCopy As New SqlBulkCopy(localConn)
    '                    bulkCopy.DestinationTableName = $"[{schemaName}].[{tableName}]"
    '                    bulkCopy.BulkCopyTimeout = 600

    '                    ' Map columns explicitly
    '                    For Each col As DataColumn In data.Columns
    '                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName)
    '                    Next

    '                    bulkCopy.WriteToServer(data)
    '                End Using

    '                _logger.Log($" ✅ Copied {data.Rows.Count} rows into DentistX.{tableName}")

    '                ' --- 9️⃣ Disable identity insert if it was enabled ---
    '                If targetHasIdentity Then
    '                    Using offCmd As New SqlCommand($"SET IDENTITY_INSERT [{schemaName}].[{tableName}] OFF", localConn)
    '                        offCmd.ExecuteNonQuery()
    '                    End Using
    '                    _logger.Log($"  IDENTITY_INSERT OFF for [{schemaName}].[{tableName}]")
    '                End If
    '            End Using

    '        Catch ex As Exception
    '            _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")
    '            ' Ensure IDENTITY_INSERT is OFF on error
    '            Try
    '                Using fixConn As New SqlConnection(trgConn.ConnectionString)
    '                    fixConn.Open()
    '                    Dim schemaName As String = "dbo"
    '                    Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", fixConn)
    '                        schemaCmd.Parameters.AddWithValue("@T", tableName)
    '                        Dim s = schemaCmd.ExecuteScalar()
    '                        If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '                    End Using
    '                    Using offCmd As New SqlCommand($"SET IDENTITY_INSERT [{schemaName}].[{tableName}] OFF", fixConn)
    '                        offCmd.ExecuteNonQuery()
    '                    End Using
    '                End Using
    '            Catch
    '                ' Ignore
    '            End Try
    '            Throw ' Re-throw to maintain error flow
    '        End Try
    '    End Sub
    '    Private Sub CopyData8TryAgain(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '        Try
    '            ' --- 1️⃣ Get schema name first ---
    '            Dim schemaName As String = "dbo"
    '            Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
    '                schemaCmd.Parameters.AddWithValue("@T", tableName)
    '                Dim s = schemaCmd.ExecuteScalar()
    '                If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '            End Using

    '            ' --- 2️⃣ Check if table has identity column in SOURCE ---
    '            Dim hasIdentity As Boolean = False
    '            Dim identityColumnName As String = Nothing
    '            Using idCmd As New SqlCommand($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T AND COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1", srcConn)
    '                idCmd.Parameters.AddWithValue("@S", schemaName)
    '                idCmd.Parameters.AddWithValue("@T", tableName)
    '                Dim result = idCmd.ExecuteScalar()
    '                If result IsNot Nothing AndAlso Not Convert.IsDBNull(result) Then
    '                    hasIdentity = True
    '                    identityColumnName = result.ToString()
    '                End If
    '            End Using

    '            ' --- 3️⃣ Build SELECT query that explicitly includes identity column if it exists ---
    '            Dim selectSQL As String
    '            If hasIdentity AndAlso Not String.IsNullOrEmpty(identityColumnName) Then
    '                ' Get all column names except identity, then add identity column explicitly
    '                Using colCmd As New SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T AND COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 0", srcConn)
    '                    colCmd.Parameters.AddWithValue("@S", schemaName)
    '                    colCmd.Parameters.AddWithValue("@T", tableName)
    '                    Using rdr = colCmd.ExecuteReader()
    '                        Dim columns As New List(Of String)()
    '                        While rdr.Read()
    '                            columns.Add($"[{rdr.GetString(0)}]")
    '                        End While
    '                        ' Add identity column at the end to ensure proper mapping
    '                        selectSQL = $"SELECT {String.Join(", ", columns)}, [{identityColumnName}] FROM [{schemaName}].[{tableName}]"
    '                    End Using
    '                End Using
    '            Else
    '                selectSQL = $"SELECT * FROM [{schemaName}].[{tableName}]"
    '            End If

    '            ' --- 4️⃣ Read source data ---
    '            Dim data As New DataTable()
    '            Using srcCmd As New SqlCommand(selectSQL, srcConn)
    '                Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                    data.Load(reader)
    '                End Using
    '            End Using
    '            _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

    '            If data.Rows.Count = 0 Then Exit Sub

    '            ' --- 5️⃣ Delete all target rows before insert ---
    '            Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
    '                delCmd.ExecuteNonQuery()
    '            End Using

    '            ' --- 6️⃣ Open local connection for identity handling + bulk copy ---
    '            Using localConn As New SqlConnection(trgConn.ConnectionString)
    '                localConn.Open()

    '                ' ➤ Enable identity insert only if source has identity data
    '                If hasIdentity AndAlso data.Columns.Contains(identityColumnName) Then
    '                    Using onCmd As New SqlCommand($"SET IDENTITY_INSERT [{schemaName}].[{tableName}] ON", localConn)
    '                        onCmd.ExecuteNonQuery()
    '                    End Using
    '                    _logger.Log($"  IDENTITY_INSERT ON for [{schemaName}].[{tableName}]")
    '                End If

    '                ' --- 7️⃣ Bulk copy ---
    '                Using bulkCopy As New SqlBulkCopy(localConn)
    '                    bulkCopy.DestinationTableName = $"[{schemaName}].[{tableName}]"
    '                    bulkCopy.BulkCopyTimeout = 600

    '                    For Each col As DataColumn In data.Columns
    '                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName)
    '                    Next

    '                    bulkCopy.WriteToServer(data)
    '                End Using

    '                _logger.Log($" ✅ Copied {data.Rows.Count} rows into DentistX.{tableName}")

    '                ' --- 8️⃣ Disable identity insert after copy ---
    '                If hasIdentity AndAlso data.Columns.Contains(identityColumnName) Then
    '                    Using offCmd As New SqlCommand($"SET IDENTITY_INSERT [{schemaName}].[{tableName}] OFF", localConn)
    '                        offCmd.ExecuteNonQuery()
    '                    End Using
    '                    _logger.Log($"  IDENTITY_INSERT OFF for [{schemaName}].[{tableName}]")
    '                End If
    '            End Using

    '        Catch ex As Exception
    '            _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")
    '            ' Ensure IDENTITY_INSERT is OFF on error
    '            Try
    '                Using fixConn As New SqlConnection(trgConn.ConnectionString)
    '                    fixConn.Open()
    '                    Dim schemaName As String = "dbo"
    '                    Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", fixConn)
    '                        schemaCmd.Parameters.AddWithValue("@T", tableName)
    '                        Dim s = schemaCmd.ExecuteScalar()
    '                        If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '                    End Using
    '                    Using offCmd As New SqlCommand($"SET IDENTITY_INSERT [{schemaName}].[{tableName}] OFF", fixConn)
    '                        offCmd.ExecuteNonQuery()
    '                    End Using
    '                End Using
    '            Catch
    '                ' Ignore
    '            End Try
    '        End Try
    '    End Sub
    '    Private Sub CopyData7(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '        Try
    '            ' --- 1️⃣ Read source data ---
    '            Dim selectSQL As String = $"SELECT * FROM [{tableName}]"
    '            Dim data As New DataTable()
    '            Using srcCmd As New SqlCommand(selectSQL, srcConn)
    '                Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                    data.Load(reader)
    '                End Using
    '            End Using
    '            _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

    '            If data.Rows.Count = 0 Then Exit Sub

    '            ' --- 2️⃣ Determine schema name ---
    '            Dim schemaName As String = "dbo"
    '            Using schemaCmd As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@T", trgConn)
    '                schemaCmd.Parameters.AddWithValue("@T", tableName)
    '                Dim s = schemaCmd.ExecuteScalar()
    '                If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '            End Using

    '            ' --- 3️⃣ Delete all target rows before insert ---
    '            Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
    '                delCmd.ExecuteNonQuery()
    '            End Using

    '            ' --- 4️⃣ Open local connection for identity handling + bulk copy ---
    '            Using localConn As New SqlConnection(trgConn.ConnectionString)
    '                localConn.Open()

    '                ' ➤ Enable identity insert (returns identity column name or Nothing)
    '                Dim identityName As String = EnableIdentityInsert(localConn, tableName)

    '                ' --- 5️⃣ Get destination column list (schema-aware) ---
    '                Dim destCols As New List(Of String)
    '                Using colCmd As New SqlCommand("
    '                SELECT COLUMN_NAME 
    '                FROM INFORMATION_SCHEMA.COLUMNS
    '                WHERE TABLE_SCHEMA=@S AND TABLE_NAME=@T
    '                ORDER BY ORDINAL_POSITION", localConn)
    '                    colCmd.Parameters.AddWithValue("@S", schemaName)
    '                    colCmd.Parameters.AddWithValue("@T", tableName)
    '                    Using rdr = colCmd.ExecuteReader()
    '                        While rdr.Read()
    '                            destCols.Add(rdr.GetString(0))
    '                        End While
    '                    End Using
    '                End Using

    '                ' --- 6️⃣ Trim columns not present in destination (keep identity col if present) ---
    '                For i As Integer = data.Columns.Count - 1 To 0 Step -1
    '                    Dim col As DataColumn = data.Columns(i)
    '                    If Not destCols.Contains(col.ColumnName) AndAlso (String.IsNullOrEmpty(identityName) OrElse col.ColumnName <> identityName) Then
    '                        data.Columns.Remove(col)
    '                    End If
    '                Next

    '                ' --- 7️⃣ Check that identity column exists if enabled ---
    '                If Not String.IsNullOrEmpty(identityName) Then
    '                    If Not data.Columns.Contains(identityName) Then
    '                        _logger.Log($"  Aborting copy for {tableName}: identity column '{identityName}' missing in source DataTable.")
    '                        DisableIdentityInsert(localConn, tableName)
    '                        Exit Sub
    '                    End If
    '                End If

    '                ' --- 8️⃣ Bulk copy on same connection ---
    '                Using bulkCopy As New SqlBulkCopy(localConn)
    '                    bulkCopy.DestinationTableName = $"{schemaName}.{tableName}"
    '                    bulkCopy.BulkCopyTimeout = 600

    '                    For Each col As DataColumn In data.Columns
    '                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName)
    '                    Next

    '                    bulkCopy.WriteToServer(data)
    '                End Using

    '                _logger.Log($" ✅ Copied {data.Rows.Count} rows into DentistX.{tableName}")

    '                ' --- 9️⃣ Disable identity insert after copy ---
    '                If Not String.IsNullOrEmpty(identityName) Then
    '                    DisableIdentityInsert(localConn, tableName)
    '                End If
    '            End Using

    '        Catch ex As Exception
    '            _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")
    '            ' Fallback: ensure IDENTITY_INSERT is OFF
    '            Try
    '                Using fixConn As New SqlConnection(trgConn.ConnectionString)
    '                    fixConn.Open()
    '                    DisableIdentityInsert(fixConn, tableName)
    '                End Using
    '            Catch
    '                ' Ignore
    '            End Try
    '        End Try
    '    End Sub





    '#End Region





#Region "Helpers: FK & Triggers"

    Private Function GetColumnLength(conn As SqlConnection, tableName As String, columnName As String) As Integer
        Dim sql As String =
        "SELECT CHARACTER_MAXIMUM_LENGTH 
         FROM INFORMATION_SCHEMA.COLUMNS 
         WHERE TABLE_NAME = @TableName AND COLUMN_NAME = @ColumnName"
        Using cmd As New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@TableName", tableName)
            cmd.Parameters.AddWithValue("@ColumnName", columnName)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                Return Convert.ToInt32(result)
            Else
                Return 0
            End If
        End Using
    End Function


    Private Sub DisableAllForeignKeys()
        Using conn As New SqlConnection(TargetConnectionString)
            conn.Open()
            Dim q = "
            DECLARE @sql NVARCHAR(MAX)=''
            SELECT @sql=@sql+'ALTER TABLE '+QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id))+'.'+
                   QUOTENAME(OBJECT_NAME(parent_object_id))+' NOCHECK CONSTRAINT '+QUOTENAME(name)+';'
            FROM sys.foreign_keys WHERE is_disabled=0
            EXEC sp_executesql @sql"
            Using cmd As New SqlCommand(q, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
        _logger.Log("Foreign keys disabled.")
    End Sub

    Private Sub EnableAllForeignKeys()
        Using conn As New SqlConnection(TargetConnectionString)
            conn.Open()
            Dim q = "
            DECLARE @sql NVARCHAR(MAX)=''
            SELECT @sql=@sql+'ALTER TABLE '+QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id))+'.'+
                   QUOTENAME(OBJECT_NAME(parent_object_id))+' WITH CHECK CHECK CONSTRAINT '+QUOTENAME(name)+';'
            FROM sys.foreign_keys WHERE is_disabled=1
            EXEC sp_executesql @sql"
            Using cmd As New SqlCommand(q, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
        _logger.Log("Foreign keys re-enabled.")
    End Sub

    Private Sub DisableAllTriggers()
        Using conn As New SqlConnection(TargetConnectionString)
            conn.Open()
            Dim q = "
            DECLARE @sql NVARCHAR(MAX)=''
            SELECT @sql=@sql+'DISABLE TRIGGER '+QUOTENAME(t.name)+' ON '+
                   QUOTENAME(OBJECT_SCHEMA_NAME(t.parent_id))+'.'+QUOTENAME(OBJECT_NAME(t.parent_id))+';'
            FROM sys.triggers t WHERE t.is_disabled=0 AND t.parent_id>0
            EXEC sp_executesql @sql"
            Using cmd As New SqlCommand(q, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
        _logger.Log("Triggers disabled.")
    End Sub

    Private Sub EnableAllTriggers()
        Using conn As New SqlConnection(TargetConnectionString)
            conn.Open()
            Dim q = "
            DECLARE @sql NVARCHAR(MAX)=''
            SELECT @sql=@sql+'ENABLE TRIGGER '+QUOTENAME(t.name)+' ON '+
                   QUOTENAME(OBJECT_SCHEMA_NAME(t.parent_id))+'.'+QUOTENAME(OBJECT_NAME(t.parent_id))+';'
            FROM sys.triggers t WHERE t.is_disabled=1 AND t.parent_id>0
            EXEC sp_executesql @sql"
            Using cmd As New SqlCommand(q, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
        _logger.Log("Triggers re-enabled.")
    End Sub

#End Region

End Class
