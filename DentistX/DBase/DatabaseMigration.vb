Imports System.Data.SqlClient
Imports System.Text

Public Class DatabaseMigration
    Private sourceConnectionString As String = "Data Source=.;Initial Catalog=Dentist;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"
    Private targetConnectionString As String = "Data Source=.;Initial Catalog=DentistX;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"

    Public Sub MigrateDentistToDentistX()
        Try
            Console.WriteLine("Starting database migration...")

            ' Step 1: Get tables in dependency order (parent tables first)
            Dim tablesInOrder As List(Of String) = GetTablesInDependencyOrder()

            ' Step 2: Disable foreign keys in target database
            DisableAllForeignKeys()

            ' Step 3: Disable triggers in target database
            DisableAllTriggers()

            ' Step 4: Migrate data table by table in correct order
            For Each tableName In tablesInOrder
                Console.WriteLine($"Migrating table: {tableName}")
                CopyTableData(tableName)
            Next

            ' Step 5: Re-enable foreign keys
            EnableAllForeignKeys()

            ' Step 6: Re-enable triggers
            EnableAllTriggers()

            Console.WriteLine("Database migration completed successfully!")

        Catch ex As Exception
            Console.WriteLine($"Error during migration: {ex.Message}")
            ' Re-enable constraints even if migration fails
            Try
                EnableAllForeignKeys()
                EnableAllTriggers()
            Catch
                ' Ignore errors during cleanup
            End Try
            Throw
        End Try
    End Sub

    Private Function GetTablesInDependencyOrder() As List(Of String)
        Dim tables As New List(Of String)()

        Using conn As New SqlConnection(sourceConnectionString)
            conn.Open()

            ' Robust approach using a loop to build dependency levels
            Dim dependencyQuery = "
        SELECT 
            OBJECT_NAME(fk.parent_object_id) AS ChildTable,
            OBJECT_NAME(fk.referenced_object_id) AS ParentTable
        FROM sys.foreign_keys fk
        WHERE fk.parent_object_id <> fk.referenced_object_id"  '-- Exclude self-references

            Dim dependencies As New Dictionary(Of String, List(Of String))()
            Dim allTables As List(Of String) = GetTableNames(conn)

            ' Initialize dependency dictionary
            For Each tableName In allTables
                dependencies(tableName) = New List(Of String)()
            Next

            ' Build dependency graph
            Using command As New SqlCommand(dependencyQuery, conn)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim childTable As String = reader("ChildTable").ToString()
                        Dim parentTable As String = reader("ParentTable").ToString()

                        If dependencies.ContainsKey(childTable) Then
                            dependencies(childTable).Add(parentTable)
                        End If
                    End While
                End Using
            End Using

            ' Topological sort
            Dim visited As New Dictionary(Of String, Boolean)()
            Dim tempMark As New Dictionary(Of String, Boolean)()

            For Each table In allTables
                visited(table) = False
                tempMark(table) = False
            Next

            For Each table In allTables
                If Not visited(table) Then
                    TopologicalSort(table, dependencies, visited, tempMark, tables)
                End If
            Next

        End Using

        Console.WriteLine($"Found {tables.Count} tables in dependency order")
        Return tables
    End Function

    Private Sub TopologicalSort(tableName As String,
                           dependencies As Dictionary(Of String, List(Of String)),
                           visited As Dictionary(Of String, Boolean),
                           tempMark As Dictionary(Of String, Boolean),
                           sortedList As List(Of String))

        If tempMark(tableName) Then
            Throw New Exception("Cyclic dependency detected in table: " & tableName)
        End If

        If Not visited(tableName) Then
            tempMark(tableName) = True

            ' Process all dependencies
            For Each dependency In dependencies(tableName)
                TopologicalSort(dependency, dependencies, visited, tempMark, sortedList)
            Next

            tempMark(tableName) = False
            visited(tableName) = True
            sortedList.Add(tableName)
        End If
    End Sub
    Private Function GetTablesInDependencyOrder1() As List(Of String)
        Dim tables As New List(Of String)()

        Using conn As New SqlConnection(sourceConnectionString)
            conn.Open()

            ' Query to get tables in order based on foreign key dependencies
            Dim query = "
            WITH TableDependencies AS (
                SELECT 
                    t.name AS TableName,
                    p.name AS ParentTableName,
                    0 AS Level
                FROM sys.tables t
                LEFT JOIN sys.foreign_keys fk ON t.object_id = fk.parent_object_id
                LEFT JOIN sys.tables p ON fk.referenced_object_id = p.object_id
                WHERE p.name IS NULL OR t.name = p.name
                
                UNION ALL
                
                SELECT 
                    t.name AS TableName,
                    p.name AS ParentTableName,
                    td.Level + 1
                FROM sys.tables t
                INNER JOIN sys.foreign_keys fk ON t.object_id = fk.parent_object_id
                INNER JOIN sys.tables p ON fk.referenced_object_id = p.object_id
                INNER JOIN TableDependencies td ON p.name = td.TableName
                WHERE t.name <> p.name
            )
            SELECT DISTINCT TableName
            FROM TableDependencies
            ORDER BY MAX(Level) OVER (PARTITION BY TableName), TableName"

            Using command As New SqlCommand(query, conn)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        tables.Add(reader("TableName").ToString())
                    End While
                End Using
            End Using

            ' Fallback: if the recursive query fails, get all tables and sort manually
            If tables.Count = 0 Then
                tables = GetTableNames(conn)
                ' Simple heuristic: put common lookup tables first
                Dim priorityTables As String() = {"Users", "Patients", "AppointmentTypes", "TreatmentTypes"}
                tables = tables.OrderBy(Function(t) Array.IndexOf(priorityTables, t)).ToList()
            End If
        End Using

        Return tables
    End Function

    Private Function GetTableNames(connection As SqlConnection) As List(Of String)
        Dim tableNames As New List(Of String)()

        Dim query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME"

        Using command As New SqlCommand(query, connection)
            Using reader As SqlDataReader = command.ExecuteReader()
                While reader.Read()
                    tableNames.Add(reader("TABLE_NAME").ToString())
                End While
            End Using
        End Using

        Return tableNames
    End Function

    Private Sub CopyTableData2(tableName As String)
        Try
            ' Check if table has identity column in TARGET database
            Dim hasIdentity As Boolean = CheckIfTableHasIdentity(targetConnectionString, tableName)

            ' Also check source to make sure we have data to migrate
            Dim sourceHasData As Boolean = CheckIfTableHasData(tableName)

            If Not sourceHasData Then
                Console.WriteLine($"No data found in table: {tableName}")
                Return
            End If

            Using sourceConn As New SqlConnection(sourceConnectionString)
                sourceConn.Open()

                ' Read data from source table
                Dim sourceData As New DataTable()
                Using sourceCommand As New SqlCommand($"SELECT * FROM [{tableName}]", sourceConn)
                    Using adapter As New SqlDataAdapter(sourceCommand)
                        adapter.Fill(sourceData)
                    End Using
                End Using

                ' Insert data into target table
                Using targetConn As New SqlConnection(targetConnectionString)
                    targetConn.Open()

                    ' Handle identity columns
                    If hasIdentity Then
                        Console.WriteLine($"Enabling IDENTITY_INSERT for table: {tableName}")
                        Using enableCommand As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] ON", targetConn)
                            enableCommand.ExecuteNonQuery()
                        End Using
                    End If

                    Try
                        Using bulkCopy As New SqlBulkCopy(targetConn)
                            bulkCopy.DestinationTableName = $"[{tableName}]"
                            bulkCopy.BatchSize = 1000
                            bulkCopy.BulkCopyTimeout = 300 ' 5 minutes

                            ' Map columns explicitly
                            For Each column As DataColumn In sourceData.Columns
                                bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName)
                            Next

                            bulkCopy.WriteToServer(sourceData)
                        End Using

                    Finally
                        ' Always turn off IDENTITY_INSERT
                        If hasIdentity Then
                            Using disableCommand As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] OFF", targetConn)
                                disableCommand.ExecuteNonQuery()
                            End Using
                            Console.WriteLine($"Disabled IDENTITY_INSERT for table: {tableName}")
                        End If
                    End Try
                End Using

                Console.WriteLine($"Successfully migrated {sourceData.Rows.Count} rows from table: {tableName}")

            End Using
        Catch ex As Exception
            Console.WriteLine($"Error migrating table {tableName}: {ex.Message}")
            Console.WriteLine($"Stack trace: {ex.StackTrace}")
            Throw New Exception($"Failed to migrate table {tableName}: {ex.Message}", ex)
        End Try
    End Sub
    Private Sub CopyTableData1(tableName As String)
        Try
            Dim sourceData As New DataTable()
            Dim hasIdentity As Boolean
            Using sourceConn As New SqlConnection(sourceConnectionString)
                sourceConn.Open()

                ' Check if table has identity column
                hasIdentity = CheckIfTableHasIdentity1(sourceConn, tableName)

                ' Read data from source table
                Using sourceCommand As New SqlCommand($"SELECT * FROM [{tableName}]", sourceConn)
                    Using adapter As New SqlDataAdapter(sourceCommand)
                        adapter.Fill(sourceData)
                    End Using
                End Using

                If sourceData.Rows.Count = 0 Then
                    Console.WriteLine($"No data found in table: {tableName}")
                    Return
                End If
            End Using

            ' Insert data into target table
            Using targetConn As New SqlConnection(targetConnectionString)
                targetConn.Open()

                ' Handle identity columns
                If hasIdentity Then
                    Using enableCommand As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] ON", targetConn)
                        enableCommand.ExecuteNonQuery()
                    End Using
                End If

                ' Clear existing data from target table (optional)
                'Uncomment the next line if you want to clear target table first
                ClearTableData(targetConn, tableName)

                Using bulkCopy As New SqlBulkCopy(targetConn)
                    bulkCopy.DestinationTableName = $"[{tableName}]"
                    bulkCopy.BatchSize = 1000
                    bulkCopy.BulkCopyTimeout = 300 ' 5 minutes

                    ' Map columns explicitly
                    For Each column As DataColumn In sourceData.Columns
                        bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName)
                    Next

                    bulkCopy.WriteToServer(sourceData)
                End Using

                If hasIdentity Then
                    Using disableCommand As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] OFF", targetConn)
                        disableCommand.ExecuteNonQuery()
                    End Using
                End If
            End Using

            Console.WriteLine($"Successfully migrated {sourceData.Rows.Count} rows from table: {tableName}")


        Catch ex As Exception
            Console.WriteLine($"Error migrating table {tableName}: {ex.Message}")
            Throw New Exception($"Failed to migrate table {tableName}: {ex.Message}", ex)
        End Try
    End Sub
    Private Function CheckIfTableHasIdentity1(connection As SqlConnection, tableName As String) As Boolean
        Dim query = "SELECT COUNT(*) FROM sys.columns WHERE object_id = OBJECT_ID(@TableName) AND is_identity = 1"

        Using command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@TableName", tableName)
            Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
            Return count > 0
        End Using
    End Function
    Private Sub CopyTableData3(tableName As String)
        Try
            Console.WriteLine($"Starting migration for table: {tableName}")

            ' Debug: Check identity in both databases
            Dim sourceHasIdentity As Boolean = CheckIfTableHasIdentity(sourceConnectionString, tableName)
            Dim targetHasIdentity As Boolean = CheckIfTableHasIdentity(targetConnectionString, tableName)

            Console.WriteLine($"Identity columns - Source: {sourceHasIdentity}, Target: {targetHasIdentity}")

            If sourceHasIdentity And Not targetHasIdentity Then
                Console.WriteLine($"WARNING: Source table {tableName} has identity but target doesn't!")
            End If

            Using sourceConn As New SqlConnection(sourceConnectionString)
                sourceConn.Open()

                ' Read data from source table
                Dim sourceData As New DataTable()
                Using sourceCommand As New SqlCommand($"SELECT * FROM [{tableName}]", sourceConn)
                    Using adapter As New SqlDataAdapter(sourceCommand)
                        adapter.Fill(sourceData)
                    End Using
                End Using

                Console.WriteLine($"Read {sourceData.Rows.Count} rows from source table {tableName}")

                If sourceData.Rows.Count = 0 Then
                    Console.WriteLine($"No data to migrate for table: {tableName}")
                    Return
                End If

                ' Insert data into target table
                Using targetConn As New SqlConnection(targetConnectionString)
                    targetConn.Open()
                    ' Clear existing data from target table (optional)
                    'Uncomment the next line if you want to clear target table first
                    ClearTableData(targetConn, tableName)

                    ' Handle identity columns - use TARGET database setting
                    If targetHasIdentity Then
                        Console.WriteLine($"Enabling IDENTITY_INSERT for target table: {tableName}")
                        Using enableCommand As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] ON", targetConn)
                            enableCommand.ExecuteNonQuery()
                        End Using
                    End If

                    Try
                        Using bulkCopy As New SqlBulkCopy(targetConn)
                            bulkCopy.DestinationTableName = $"[{tableName}]"
                            bulkCopy.BatchSize = 1000
                            bulkCopy.BulkCopyTimeout = 300

                            ' Map columns explicitly
                            For Each column As DataColumn In sourceData.Columns
                                bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName)
                            Next

                            Console.WriteLine($"Starting bulk copy to target table: {tableName}")
                            bulkCopy.WriteToServer(sourceData)
                            Console.WriteLine($"Bulk copy completed for table: {tableName}")
                        End Using

                    Finally
                        ' Always turn off IDENTITY_INSERT
                        If targetHasIdentity Then
                            Console.WriteLine($"Disabling IDENTITY_INSERT for target table: {tableName}")
                            Using disableCommand As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] OFF", targetConn)
                                disableCommand.ExecuteNonQuery()
                            End Using
                        End If
                    End Try
                End Using

                Console.WriteLine($"Successfully migrated {sourceData.Rows.Count} rows from table: {tableName}")

            End Using
        Catch ex As Exception
            Console.WriteLine($"ERROR migrating table {tableName}: {ex.Message}")
            Console.WriteLine($"Stack trace: {ex.StackTrace}")
            Throw New Exception($"Failed to migrate table {tableName}: {ex.Message}", ex)
        End Try
    End Sub

#Region "CopyTableData"
    Private Sub CopyTableData(tableName As String)
        Try
            Console.WriteLine($"Starting migration for table: {tableName}")

            ' Check identity in target database
            Dim targetHasIdentity As Boolean = CheckIfTableHasIdentity(targetConnectionString, tableName)

            Using sourceConn As New SqlConnection(sourceConnectionString)
                sourceConn.Open()

                ' Read data from source table
                Dim sourceData As New DataTable()
                Using sourceCommand As New SqlCommand($"SELECT * FROM [{tableName}]", sourceConn)
                    Using adapter As New SqlDataAdapter(sourceCommand)
                        adapter.Fill(sourceData)
                    End Using
                End Using

                Console.WriteLine($"Read {sourceData.Rows.Count} rows from source table {tableName}")

                If sourceData.Rows.Count = 0 Then
                    Console.WriteLine($"No data to migrate for table: {tableName}")
                    Return
                End If
                Using targetConn As New SqlConnection(targetConnectionString)
                    ClearTargetTable(targetConn, tableName)
                End Using


                ' Insert data into target table
                Using targetConn As New SqlConnection(targetConnectionString)
                    targetConn.Open()

                    If targetHasIdentity Then
                        ' For identity tables, use the SqlDataAdapter approach
                        InsertUsingDataAdapter(targetConn, tableName, sourceData)
                    Else
                        ' For non-identity tables, use bulk copy for better performance
                        Using bulkCopy As New SqlBulkCopy(targetConn)
                            bulkCopy.DestinationTableName = $"[{tableName}]"
                            bulkCopy.BatchSize = 1000
                            bulkCopy.BulkCopyTimeout = 300

                            For Each column As DataColumn In sourceData.Columns
                                bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName)
                            Next

                            Console.WriteLine($"Starting bulk copy to target table: {tableName}")
                            bulkCopy.WriteToServer(sourceData)
                            Console.WriteLine($"Bulk copy completed for table: {tableName}")
                        End Using
                    End If
                End Using

                Console.WriteLine($"Successfully migrated {sourceData.Rows.Count} rows from table: {tableName}")

            End Using
        Catch ex As Exception
            Console.WriteLine($"ERROR migrating table {tableName}: {ex.Message}")
            Throw New Exception($"Failed to migrate table {tableName}: {ex.Message}", ex)
        End Try
    End Sub

    Private Sub InsertUsingDataAdapter(connection As SqlConnection, tableName As String, data As DataTable)
        Console.WriteLine($"Using DataAdapter for identity table: {tableName}")

        ' Enable IDENTITY_INSERT
        Using enableCommand As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] ON", connection)
            enableCommand.ExecuteNonQuery()
        End Using

        Try
            Using adapter As New SqlDataAdapter()
                ' Build INSERT command
                adapter.InsertCommand = BuildInsertCommand(connection, tableName, data)

                ' Set UpdateBatchSize for better performance with multiple rows
                adapter.UpdateBatchSize = 100

                ' Perform the insert
                Dim rowsAffected As Integer = adapter.Update(data)
                Console.WriteLine($"Inserted {rowsAffected} rows into {tableName}")

            End Using

        Finally
            ' Disable IDENTITY_INSERT
            Using disableCommand As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] OFF", connection)
                disableCommand.ExecuteNonQuery()
            End Using
        End Try
    End Sub

    Private Sub InsertDataWithIdentity(connection As SqlConnection, tableName As String, data As DataTable)
        Console.WriteLine($"Using DataAdapter for table with identity: {tableName}")

        ' Enable IDENTITY_INSERT
        Using enableCommand As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] ON", connection)
            enableCommand.ExecuteNonQuery()
        End Using

        Try
            Using adapter As New SqlDataAdapter()
                ' Configure the adapter for insert
                adapter.InsertCommand = BuildInsertCommand(connection, tableName, data)

                ' Perform the insert
                Dim rowsAffected As Integer = adapter.Update(data)
                Console.WriteLine($"Inserted {rowsAffected} rows into {tableName}")

            End Using

        Finally
            ' Disable IDENTITY_INSERT
            Using disableCommand As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] OFF", connection)
                disableCommand.ExecuteNonQuery()
            End Using
        End Try
    End Sub

    Private Function BuildInsertCommand(connection As SqlConnection, tableName As String, data As DataTable) As SqlCommand
        ' Build a list of column names
        Dim columns As New List(Of String)()
        For Each column As DataColumn In data.Columns
            columns.Add(column.ColumnName)
        Next

        ' Build the INSERT SQL text dynamically
        Dim insertSql As String = $"INSERT INTO [{tableName}] ([{String.Join("], [", columns)}]) VALUES (@{String.Join(", @", columns)})"

        ' Prepare the SqlCommand
        Dim command As New SqlCommand(insertSql, connection)

        ' Add parameters for each column (mapping DataTable types to SQL types)
        For Each column As DataColumn In data.Columns
            Dim sqlType As SqlDbType = GetSqlDbType(column.DataType)
            command.Parameters.Add("@" & column.ColumnName, sqlType, If(column.MaxLength > 0, column.MaxLength, 0), column.ColumnName)
        Next

        Return command
    End Function


    Private Function BuildInsertCommand1(connection As SqlConnection, tableName As String, data As DataTable) As SqlCommand
        Dim columns As New List(Of String)()
        For Each column As DataColumn In data.Columns
            columns.Add(column.ColumnName)
        Next

        Dim columnList As String = "[" & String.Join("], [", columns) & "]"
        Dim parameterList As String = "@" & String.Join(", @", columns)
        Dim insertSql As String = $"INSERT INTO [{tableName}] ({columnList}) VALUES ({parameterList})"

        Dim command As New SqlCommand(insertSql, connection)

        ' Add parameters
        For Each column As DataColumn In data.Columns
            command.Parameters.Add("@" & column.ColumnName, GetSqlDbType(column.DataType), column.MaxLength, column.ColumnName)
        Next

        Return command
    End Function


    Private Function GetSqlDbType(type As Type) As SqlDbType
        If type Is GetType(String) Then Return SqlDbType.NVarChar
        If type Is GetType(Integer) Then Return SqlDbType.Int
        If type Is GetType(Long) Then Return SqlDbType.BigInt
        If type Is GetType(Decimal) Then Return SqlDbType.Decimal
        If type Is GetType(Double) Then Return SqlDbType.Float
        If type Is GetType(Single) Then Return SqlDbType.Real
        If type Is GetType(Boolean) Then Return SqlDbType.Bit
        If type Is GetType(DateTime) Then Return SqlDbType.DateTime
        If type Is GetType(Byte()) Then Return SqlDbType.VarBinary
        Return SqlDbType.Variant
    End Function



#End Region

    Private Function CheckIfTableHasIdentity(connectionString As String, tableName As String) As Boolean
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            Dim query = "SELECT COUNT(*) FROM sys.columns WHERE object_id = OBJECT_ID(@TableName) AND is_identity = 1"

            Using command As New SqlCommand(query, conn)
                command.Parameters.AddWithValue("@TableName", tableName)
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                Console.WriteLine($"Table {tableName} has {count} identity columns in database")
                Return count > 0
            End Using
        End Using
    End Function

    Private Function CheckIfTableHasData(tableName As String) As Boolean
        Using conn As New SqlConnection(sourceConnectionString)
            conn.Open()
            Dim query = "SELECT COUNT(*) FROM [" & tableName & "]"

            Using command As New SqlCommand(query, conn)
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function

    Private Sub DisableAllForeignKeys()
        Using conn As New SqlConnection(targetConnectionString)
            conn.Open()
            Dim query = "
            DECLARE @sql NVARCHAR(MAX) = ''
            SELECT @sql = @sql + 'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' + 
                   QUOTENAME(OBJECT_NAME(parent_object_id)) + ' NOCHECK CONSTRAINT ' + 
                   QUOTENAME(name) + ';' + CHAR(13)
            FROM sys.foreign_keys
            WHERE is_disabled = 0
            
            EXEC sp_executesql @sql"

            Using command As New SqlCommand(query, conn)
                command.ExecuteNonQuery()
            End Using
            Console.WriteLine("All foreign keys disabled")
        End Using
    End Sub

    Private Sub EnableAllForeignKeys()
        Using conn As New SqlConnection(targetConnectionString)
            conn.Open()
            Dim query = "
            DECLARE @sql NVARCHAR(MAX) = ''
            SELECT @sql = @sql + 'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' + 
                   QUOTENAME(OBJECT_NAME(parent_object_id)) + ' WITH CHECK CHECK CONSTRAINT ' + 
                   QUOTENAME(name) + ';' + CHAR(13)
            FROM sys.foreign_keys
            WHERE is_disabled = 1
            
            EXEC sp_executesql @sql"

            Using command As New SqlCommand(query, conn)
                command.ExecuteNonQuery()
            End Using
            Console.WriteLine("All foreign keys enabled and checked")
        End Using
    End Sub

    Private Sub DisableAllTriggers()
        Using conn As New SqlConnection(targetConnectionString)
            conn.Open()
            Dim query = "
            DECLARE @sql NVARCHAR(MAX) = ''
            SELECT @sql = @sql + 'DISABLE TRIGGER ' + QUOTENAME(OBJECT_SCHEMA_NAME(t.object_id)) + '.' + 
                   QUOTENAME(t.name) + ' ON ' + QUOTENAME(OBJECT_SCHEMA_NAME(t.parent_id)) + '.' + 
                   QUOTENAME(OBJECT_NAME(t.parent_id)) + ';' + CHAR(13)
            FROM sys.triggers t
            WHERE t.is_disabled = 0 AND t.parent_id > 0
            
            EXEC sp_executesql @sql"

            Using command As New SqlCommand(query, conn)
                command.ExecuteNonQuery()
            End Using
            Console.WriteLine("All triggers disabled")
        End Using
    End Sub

    Private Sub EnableAllTriggers()
        Using conn As New SqlConnection(targetConnectionString)
            conn.Open()
            Dim query = "
            DECLARE @sql NVARCHAR(MAX) = ''
            SELECT @sql = @sql + 'ENABLE TRIGGER ' + QUOTENAME(OBJECT_SCHEMA_NAME(t.object_id)) + '.' + 
                   QUOTENAME(t.name) + ' ON ' + QUOTENAME(OBJECT_SCHEMA_NAME(t.parent_id)) + '.' + 
                   QUOTENAME(OBJECT_NAME(t.parent_id)) + ';' + CHAR(13)
            FROM sys.triggers t
            WHERE t.is_disabled = 1 AND t.parent_id > 0
            
            EXEC sp_executesql @sql"

            Using command As New SqlCommand(query, conn)
                command.ExecuteNonQuery()
            End Using
            Console.WriteLine("All triggers enabled")
        End Using
    End Sub

    Private Sub ClearTargetTable(connection As SqlConnection, tableName As String)
        Try
            Using command As New SqlCommand($"TRUNCATE TABLE [{tableName}]", connection)
                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                Console.WriteLine($"Cleared {rowsAffected} rows from {tableName}")
            End Using
        Catch ex As Exception
            Console.WriteLine($"Warning: Could not clear table {tableName}: {ex.Message}")
        End Try
        'Dim cmd As New SqlCommand($"TRUNCATE TABLE [{tableName}]", connection)
        'cmd.ExecuteNonQuery()
    End Sub

    Private Sub ClearTableData(connection As SqlConnection, tableName As String)
        Try
            Using command As New SqlCommand($"DELETE FROM [{tableName}]", connection)
                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                Console.WriteLine($"Cleared {rowsAffected} rows from {tableName}")
            End Using
        Catch ex As Exception
            Console.WriteLine($"Warning: Could not clear table {tableName}: {ex.Message}")
        End Try
    End Sub

    ' Method to verify foreign key constraints after migration
    Public Sub VerifyForeignKeys()
        Using conn As New SqlConnection(targetConnectionString)
            conn.Open()
            Dim query = "
            DECLARE @TableName NVARCHAR(MAX)
            DECLARE @ConstraintName NVARCHAR(MAX)
            DECLARE @Sql NVARCHAR(MAX)
            
            DECLARE constraint_cursor CURSOR FOR
            SELECT 
                OBJECT_NAME(fk.parent_object_id) AS TableName,
                fk.name AS ConstraintName
            FROM sys.foreign_keys fk
            WHERE fk.is_disabled = 0
            
            OPEN constraint_cursor
            FETCH NEXT FROM constraint_cursor INTO @TableName, @ConstraintName
            
            WHILE @@FETCH_STATUS = 0
            BEGIN
                BEGIN TRY
                    SET @Sql = 'ALTER TABLE ' + QUOTENAME(@TableName) + 
                              ' WITH CHECK CHECK CONSTRAINT ' + QUOTENAME(@ConstraintName)
                    EXEC sp_executesql @Sql
                    PRINT 'Verified: ' + @ConstraintName
                END TRY
                BEGIN CATCH
                    PRINT 'Failed: ' + @ConstraintName + ' - ' + ERROR_MESSAGE()
                END CATCH
                
                FETCH NEXT FROM constraint_cursor INTO @TableName, @ConstraintName
            END
            
            CLOSE constraint_cursor
            DEALLOCATE constraint_cursor"

            Using command As New SqlCommand(query, conn)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub




#Region "Unused Code From DatabaseSynchronizer"

    'Private Function GetColumnList2(connection As SqlConnection, tableName As String) As List(Of ColumnInfo)
    '    Dim cols As New List(Of ColumnInfo)()
    '    Dim sql As String =
    '    "SELECT COLUMN_NAME AS ColName, DATA_TYPE AS DataType 
    '     FROM INFORMATION_SCHEMA.COLUMNS 
    '     WHERE TABLE_NAME = @TableName 
    '     ORDER BY ORDINAL_POSITION"

    '    Using cmd As New SqlCommand(sql, connection)
    '        cmd.Parameters.AddWithValue("@TableName", tableName)
    '        Using rdr = cmd.ExecuteReader()
    '            While rdr.Read()
    '                Dim ci As New ColumnInfo With {
    '                .Name = rdr("ColName").ToString(),
    '                .Type = rdr("DataType").ToString()
    '            }
    '                cols.Add(ci)
    '            End While
    '        End Using
    '    End Using
    '    Return cols
    'End Function

    'Private Function GetColumnList1(conn As SqlConnection, tableName As String) As List(Of (Name As String, DataType As String))
    '    Dim list As New List(Of (String, String))
    '    Dim cmd As New SqlCommand("
    '        SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
    '        FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@T", conn)
    '    cmd.Parameters.AddWithValue("@T", tableName)
    '    Using rdr = cmd.ExecuteReader()
    '        While rdr.Read()
    '            Dim type As String = rdr("DATA_TYPE").ToString()
    '            Dim len = If(IsDBNull(rdr("CHARACTER_MAXIMUM_LENGTH")), -1, CInt(rdr("CHARACTER_MAXIMUM_LENGTH")))
    '            If len > 0 AndAlso Not {"text", "ntext", "image"}.Contains(type) Then
    '                type &= "(" & len & ")"
    '            End If
    '            list.Add((rdr("COLUMN_NAME").ToString(), type))
    '        End While
    '    End Using
    '    Return list
    'End Function


    'Private Sub EnsureSchemaMatch1(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '    ' Compare columns and add missing ones in Dentist
    '    Dim srcCols = GetColumnList(srcConn, tableName)
    '    Dim trgCols = GetColumnList(trgConn, tableName)

    '    For Each trgCol In trgCols
    '        If Not srcCols.Any(Function(c) c.Name = trgCol.Name) Then
    '            Dim alterSQL = $"ALTER TABLE [{tableName}] ADD [{trgCol.Name}] {trgCol.Type}"
    '            Using cmd As New SqlCommand(alterSQL, srcConn)
    '                cmd.ExecuteNonQuery()
    '                _logger.Log($"Added column {trgCol.Name} to Dentist.{tableName}")
    '            End Using
    '        End If
    '    Next
    'End Sub

    'Private Sub CopyData6(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '    Dim hasIdentity As Boolean = False

    '    Try
    '        ' --- 1️⃣ Read source data ---
    '        Dim selectSQL As String = $"SELECT * FROM [{tableName}]"
    '        Dim data As New DataTable()
    '        Using srcCmd As New SqlCommand(selectSQL, srcConn)
    '            Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                data.Load(reader)
    '            End Using
    '        End Using
    '        _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

    '        If data.Rows.Count = 0 Then Exit Sub

    '        ' --- figure out schema for this table (defaults to dbo) ---
    '        Dim schemaName As String = "dbo"
    '        Using scm As New SqlCommand("SELECT TOP 1 TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @T", trgConn)
    '            scm.Parameters.AddWithValue("@T", tableName)
    '            Dim s = scm.ExecuteScalar()
    '            If s IsNot Nothing AndAlso Not Convert.IsDBNull(s) Then schemaName = s.ToString()
    '        End Using

    '        Dim fullName As String = $"{schemaName}.{tableName}" ' for OBJECT_ID

    '        '' --- 2️⃣ Check if target table has an identity column (use schema-qualified name) ---
    '        'Using idCmd As New SqlCommand("
    '        'SELECT TOP 1 [name]
    '        'FROM sys.columns
    '        'WHERE object_id = OBJECT_ID(@FullName) AND is_identity = 1", trgConn)
    '        '    idCmd.Parameters.AddWithValue("@FullName", fullName)
    '        '    Dim result = idCmd.ExecuteScalar()
    '        '    hasIdentity = (result IsNot Nothing AndAlso Not Convert.IsDBNull(result))
    '        'End Using
    '        ' Detect identity column correctly (cannot use parameter with OBJECT_ID)
    '        Dim identitySQL As String = $"
    'SELECT TOP 1 [name]
    'FROM sys.columns
    'WHERE object_id = OBJECT_ID(N'{schemaName}.{tableName}') AND is_identity = 1"
    '        Using idCmd As New SqlCommand(identitySQL, trgConn)
    '            Dim result = idCmd.ExecuteScalar()
    '            hasIdentity = (result IsNot Nothing AndAlso Not Convert.IsDBNull(result))
    '        End Using

    '        ' --- 3️⃣ Delete all target rows (schema-qualified) ---
    '        Using delCmd As New SqlCommand($"DELETE FROM [{schemaName}].[{tableName}]", trgConn)
    '            delCmd.ExecuteNonQuery()
    '        End Using

    '        ' --- prepare names ---
    '        Dim bracketedName As String = $"[{schemaName}].[{tableName}]"
    '        Dim schemaQualifiedForBulk As String = $"{schemaName}.{tableName}"

    '        ' --- 4️⃣ Open a new local connection for identity handling ---
    '        Using localConn As New SqlConnection(trgConn.ConnectionString)
    '            localConn.Open()

    '            '' If has identity, turn ON (note: schema-qualified)
    '            'If hasIdentity Then
    '            '    Using onCmd As New SqlCommand($"SET IDENTITY_INSERT {bracketedName} ON", localConn)
    '            '        onCmd.ExecuteNonQuery()
    '            '    End Using
    '            'End If
    '            If hasIdentity Then
    '                _logger.Log($"  Enabling IDENTITY_INSERT for {bracketedName}")
    '                Using onCmd As New SqlCommand($"SET IDENTITY_INSERT {bracketedName} ON", localConn)
    '                    onCmd.ExecuteNonQuery()
    '                End Using
    '            End If

    '            ' --- 5️⃣ Get destination column metadata (name, nullable, type) ---
    '            Dim destMeta As New Dictionary(Of String, (IsNullable As Boolean, DataType As String))
    '            Using metaCmd As New SqlCommand("
    '            SELECT COLUMN_NAME, IS_NULLABLE, DATA_TYPE
    '            FROM INFORMATION_SCHEMA.COLUMNS
    '            WHERE TABLE_SCHEMA = @schema AND TABLE_NAME = @table", localConn)
    '                metaCmd.Parameters.AddWithValue("@schema", schemaName)
    '                metaCmd.Parameters.AddWithValue("@table", tableName)
    '                Using rdr = metaCmd.ExecuteReader()
    '                    While rdr.Read()
    '                        Dim cname = rdr("COLUMN_NAME").ToString()
    '                        Dim isNull = rdr("IS_NULLABLE").ToString().Equals("YES", StringComparison.OrdinalIgnoreCase)
    '                        Dim dtype = rdr("DATA_TYPE").ToString()
    '                        destMeta(cname) = (isNull, dtype)
    '                    End While
    '                End Using
    '            End Using

    '            '' --- Find identity column name (if any) using schema-qualified OBJECT_ID ---
    '            'Dim identityName As String = Nothing
    '            'If hasIdentity Then
    '            '    Using idNameCmd As New SqlCommand("
    '            '    SELECT TOP 1 [name]
    '            '    FROM sys.columns
    '            '    WHERE object_id = OBJECT_ID(@FullName) AND is_identity = 1", localConn)
    '            '        idNameCmd.Parameters.AddWithValue("@FullName", fullName)
    '            '        identityName = TryCast(idNameCmd.ExecuteScalar(), String)
    '            '    End Using
    '            'End If
    '            Dim identityName As String = Nothing
    '            If hasIdentity Then
    '                Dim idNameSQL As String = $"
    '    SELECT TOP 1 [name]
    '    FROM sys.columns
    '    WHERE object_id = OBJECT_ID(N'{schemaName}.{tableName}') AND is_identity = 1"
    '                Using idNameCmd As New SqlCommand(idNameSQL, localConn)
    '                    identityName = TryCast(idNameCmd.ExecuteScalar(), String)
    '                End Using
    '            End If

    '            ' --- 6️⃣ Remove any source columns that don't exist in destination (keep identity) ---
    '            For i As Integer = data.Columns.Count - 1 To 0 Step -1
    '                Dim col As DataColumn = data.Columns(i)
    '                If Not destMeta.ContainsKey(col.ColumnName) AndAlso (identityName Is Nothing OrElse col.ColumnName <> identityName) Then
    '                    data.Columns.Remove(col)
    '                End If
    '            Next

    '            ' --- 7️⃣ Remove rows that would violate NOT NULL columns (safer than inserting bad defaults) ---
    '            Dim rowsToRemove As New List(Of DataRow)
    '            For Each row As DataRow In data.Rows
    '                Dim bad As Boolean = False
    '                For Each col As DataColumn In data.Columns
    '                    Dim cName = col.ColumnName
    '                    Dim meta As (IsNullable As Boolean, DataType As String)
    '                    If destMeta.TryGetValue(cName, meta) Then
    '                        If Not meta.IsNullable AndAlso row.IsNull(cName) Then
    '                            bad = True
    '                            _logger.Log($" Skipping a row for {tableName} because target column '{cName}' is NOT NULL but source value is NULL.")
    '                            Exit For
    '                        End If
    '                    End If
    '                Next
    '                If bad Then rowsToRemove.Add(row)
    '            Next
    '            For Each r In rowsToRemove
    '                data.Rows.Remove(r)
    '            Next

    '            If data.Rows.Count = 0 Then
    '                If hasIdentity Then
    '                    Using offCmd As New SqlCommand($"SET IDENTITY_INSERT {bracketedName} OFF", localConn)
    '                        offCmd.ExecuteNonQuery()
    '                    End Using
    '                End If
    '                _logger.Log($" No rows left to copy for {tableName} after removing invalid rows.")
    '                Exit Sub
    '            End If

    '            ' --- 8️⃣ Bulk copy ---
    '            Using bulkCopy As New SqlBulkCopy(localConn)
    '                bulkCopy.DestinationTableName = schemaQualifiedForBulk
    '                bulkCopy.BulkCopyTimeout = 600
    '                For Each col As DataColumn In data.Columns
    '                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName)
    '                Next
    '                bulkCopy.WriteToServer(data)
    '            End Using

    '            '' --- 9️⃣ Turn identity off if we set it on ---
    '            'If hasIdentity Then
    '            '    Using offCmd As New SqlCommand($"SET IDENTITY_INSERT {bracketedName} OFF", localConn)
    '            '        offCmd.ExecuteNonQuery()
    '            '    End Using
    '            'End If
    '            If hasIdentity Then
    '                _logger.Log($"  Disabling IDENTITY_INSERT for {bracketedName}")
    '                Using offCmd As New SqlCommand($"SET IDENTITY_INSERT {bracketedName} OFF", localConn)
    '                    offCmd.ExecuteNonQuery()
    '                End Using
    '            End If

    '        End Using

    '        _logger.Log($" ✅ Copied {data.Rows.Count} rows into DentistX.{tableName}")

    '    Catch ex As Exception
    '        _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")

    '        ' --- 7️⃣ Safety net: force IDENTITY_INSERT OFF globally ---
    '        Try
    '            Using fixConn As New SqlConnection(trgConn.ConnectionString)
    '                fixConn.Open()
    '                'Dim bracketedName As String = $"[dbo].[{tableName.Replace(""["", """").Replace(""]"", """")}]"
    '                'Using cmd As New SqlCommand($"SET IDENTITY_INSERT {bracketedName} OFF", fixConn)
    '                Using cmd As New SqlCommand($"SET IDENTITY_INSERT {tableName} OFF", fixConn)
    '                    cmd.ExecuteNonQuery()
    '                End Using
    '            End Using
    '        Catch
    '            ' Ignore secondary failures
    '        End Try
    '    End Try
    'End Sub


    'Private Sub CopyData5(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '    Dim hasIdentity As Boolean = False

    '    Try
    '        ' --- 1️⃣ Read source data ---
    '        Dim selectSQL As String = $"SELECT * FROM [{tableName}]"
    '        Dim data As New DataTable()
    '        Using srcCmd As New SqlCommand(selectSQL, srcConn)
    '            Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                data.Load(reader)
    '            End Using
    '        End Using
    '        _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

    '        If data.Rows.Count = 0 Then Exit Sub

    '        ' --- 2️⃣ Check if target table has an identity column ---
    '        Using idCmd As New SqlCommand("
    '        SELECT TOP 1 [name]
    '        FROM sys.columns
    '        WHERE object_id = OBJECT_ID('dbo.' + @Table) AND is_identity = 1", trgConn)
    '            idCmd.Parameters.AddWithValue("@Table", tableName)
    '            Dim result = idCmd.ExecuteScalar()
    '            hasIdentity = (result IsNot Nothing AndAlso Not Convert.IsDBNull(result))
    '        End Using

    '        ' --- 3️⃣ Delete all target rows ---
    '        Using delCmd As New SqlCommand($"DELETE FROM [{tableName}]", trgConn)
    '            delCmd.ExecuteNonQuery()
    '        End Using

    '        ' --- Prepare bracketed and schema-qualified names for identity commands and bulk copy ---
    '        Dim rawTable = tableName.Replace("[", "").Replace("]", "")
    '        Dim bracketedName As String
    '        Dim schemaQualifiedForBulk As String
    '        If rawTable.Contains(".") Then
    '            Dim parts = rawTable.Split("."c)
    '            bracketedName = $"[{parts(0)}].[{parts(1)}]"
    '            schemaQualifiedForBulk = $"{parts(0)}.{parts(1)}"
    '        Else
    '            bracketedName = $"[dbo].[{rawTable}]"
    '            schemaQualifiedForBulk = $"dbo.{rawTable}"
    '        End If

    '        ' --- 4️⃣ Open a new local connection for identity handling ---
    '        Using localConn As New SqlConnection(trgConn.ConnectionString)
    '            localConn.Open()

    '            ' If has identity, turn ON (note: requires schema-qualified table name)
    '            If hasIdentity Then
    '                Using onCmd As New SqlCommand($"SET IDENTITY_INSERT {bracketedName} ON", localConn)
    '                    onCmd.ExecuteNonQuery()
    '                End Using
    '            End If

    '            ' --- 5️⃣ Prepare column mappings ---
    '            Dim destCols = GetColumnList(localConn, tableName)
    '            Dim destNames = destCols.Select(Function(c) c.Name).ToList()

    '            '' Remove columns from source that don't exist in destination (iterate backwards)
    '            'For i As Integer = data.Columns.Count - 1 To 0 Step -1
    '            '    Dim col As DataColumn = data.Columns(i)
    '            '    If Not destNames.Contains(col.ColumnName) Then
    '            '        data.Columns.Remove(col)
    '            '    End If
    '            'Next


    '            ' Keep only columns that exist in destination
    '            ' But DO NOT remove identity column if present
    '            Dim identityName As String = Nothing
    '            If hasIdentity Then
    '                Using idNameCmd As New SqlCommand("
    '                                SELECT TOP 1 [name]
    '                                FROM sys.columns
    '                                WHERE object_id = OBJECT_ID('dbo.' + @Table) AND is_identity = 1", trgConn)
    '                    idNameCmd.Parameters.AddWithValue("@Table", tableName)
    '                    identityName = TryCast(idNameCmd.ExecuteScalar(), String)
    '                End Using
    '            End If

    '            For i As Integer = data.Columns.Count - 1 To 0 Step -1
    '                Dim col As DataColumn = data.Columns(i)
    '                If Not destNames.Contains(col.ColumnName) AndAlso
    '                    (identityName Is Nothing OrElse col.ColumnName <> identityName) Then
    '                    data.Columns.Remove(col)
    '                End If
    '            Next

    '            Using bulkCopy As New SqlBulkCopy(localConn)
    '                bulkCopy.DestinationTableName = schemaQualifiedForBulk
    '                bulkCopy.BulkCopyTimeout = 600

    '                For Each col As DataColumn In data.Columns
    '                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName)
    '                Next

    '                bulkCopy.WriteToServer(data)
    '            End Using

    '            ' --- 6️⃣ Always turn IDENTITY_INSERT OFF ---
    '            If hasIdentity Then
    '                Using offCmd As New SqlCommand($"SET IDENTITY_INSERT {bracketedName} OFF", localConn)
    '                    offCmd.ExecuteNonQuery()
    '                End Using
    '            End If
    '        End Using

    '        _logger.Log($" ✅ Copied {data.Rows.Count} rows into DentistX.{tableName}")

    '    Catch ex As Exception
    '        _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")

    '        ' --- 7️⃣ Safety net: force IDENTITY_INSERT OFF globally ---
    '        Try
    '            Using fixConn As New SqlConnection(trgConn.ConnectionString)
    '                fixConn.Open()
    '                Dim rawTable = tableName.Replace("[", "").Replace("]", "")
    '                Dim bracketedName As String
    '                If rawTable.Contains(".") Then
    '                    Dim parts = rawTable.Split("."c)
    '                    bracketedName = $"[{parts(0)}].[{parts(1)}]"
    '                Else
    '                    bracketedName = $"[dbo].[{rawTable}]"
    '                End If
    '                Using cmd As New SqlCommand($"SET IDENTITY_INSERT {bracketedName} OFF", fixConn)
    '                    cmd.ExecuteNonQuery()
    '                End Using
    '            End Using
    '        Catch
    '            ' Ignore secondary failures
    '        End Try
    '    End Try
    'End Sub

    'Private Sub CopyData4(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '    Dim hasIdentity As Boolean = False

    '    Try
    '        ' --- 1️⃣ Read source data ---
    '        Dim selectSQL As String = $"SELECT * FROM [{tableName}]"
    '        Dim data As New DataTable()
    '        Using srcCmd As New SqlCommand(selectSQL, srcConn)
    '            Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                data.Load(reader)
    '            End Using
    '        End Using
    '        _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

    '        If data.Rows.Count = 0 Then Exit Sub

    '        ' --- 2️⃣ Check if target table has an identity column ---
    '        Using idCmd As New SqlCommand("
    '        SELECT COLUMN_NAME 
    '        FROM sys.columns 
    '        WHERE object_id = OBJECT_ID(@Table) AND is_identity = 1", trgConn)
    '            idCmd.Parameters.AddWithValue("@Table", tableName)
    '            Dim result = idCmd.ExecuteScalar()
    '            hasIdentity = (result IsNot Nothing)
    '        End Using

    '        ' --- 3️⃣ Delete all target rows ---
    '        Using delCmd As New SqlCommand($"DELETE FROM [{tableName}]", trgConn)
    '            delCmd.ExecuteNonQuery()
    '        End Using

    '        ' --- 4️⃣ Open a new local connection for identity handling ---
    '        Using localConn As New SqlConnection(trgConn.ConnectionString)
    '            localConn.Open()

    '            ' If has identity, turn ON
    '            If hasIdentity Then
    '                Using onCmd As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] ON", localConn)
    '                    onCmd.ExecuteNonQuery()
    '                End Using
    '            End If

    '            ' --- 5️⃣ Prepare column mappings ---
    '            Dim destCols = GetColumnList(localConn, tableName)
    '            Dim destNames = destCols.Select(Function(c) c.Name).ToList()

    '            For Each col As DataColumn In data.Columns.Cast(Of DataColumn).ToList()
    '                If Not destNames.Contains(col.ColumnName) Then
    '                    data.Columns.Remove(col)
    '                End If
    '            Next

    '            Using bulkCopy As New SqlBulkCopy(localConn)
    '                bulkCopy.DestinationTableName = tableName
    '                bulkCopy.BulkCopyTimeout = 600

    '                For Each col As DataColumn In data.Columns
    '                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName)
    '                Next

    '                bulkCopy.WriteToServer(data)
    '            End Using

    '            ' --- 6️⃣ Always turn IDENTITY_INSERT OFF ---
    '            If hasIdentity Then
    '                Using offCmd As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] OFF", localConn)
    '                    offCmd.ExecuteNonQuery()
    '                End Using
    '            End If
    '        End Using

    '        _logger.Log($" ✅ Copied {data.Rows.Count} rows into DentistX.{tableName}")

    '    Catch ex As Exception
    '        _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")

    '        ' --- 7️⃣ Safety net: force IDENTITY_INSERT OFF globally ---
    '        Try
    '            Using fixConn As New SqlConnection(trgConn.ConnectionString)
    '                fixConn.Open()
    '                Using cmd As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] OFF", fixConn)
    '                    cmd.ExecuteNonQuery()
    '                End Using
    '            End Using
    '        Catch
    '            ' Ignore secondary failures
    '        End Try
    '    End Try
    'End Sub


    'Private Sub CopyData3(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '    Try
    '        ' --- 1️⃣ Read all data from source ---
    '        Dim selectSQL As String = $"SELECT * FROM [{tableName}]"
    '        Dim data As New DataTable()
    '        Using srcCmd As New SqlCommand(selectSQL, srcConn)
    '            Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                data.Load(reader)
    '            End Using
    '        End Using
    '        _logger.Log($" Read {data.Rows.Count} rows from Dentist.{tableName}")

    '        If data.Rows.Count = 0 Then Exit Sub

    '        ' --- 2️⃣ Get destination column list ---
    '        'Dim destCols As New List(Of (Name As String, TypeName As String))
    '        Dim destCols = GetColumnList(trgConn, tableName)
    '        Dim destColNames As HashSet(Of String) = New HashSet(Of String)(
    '        destCols.Select(Function(c) c.Name),
    '        StringComparer.OrdinalIgnoreCase
    '    )

    '        ' --- 3️⃣ Drop extra columns ---
    '        For Each col As DataColumn In data.Columns.Cast(Of DataColumn).ToList()
    '            If Not destColNames.Contains(col.ColumnName) Then
    '                _logger.Log($" Dropping extra column {col.ColumnName} from Dentist.{tableName}")
    '                data.Columns.Remove(col)
    '            End If
    '        Next

    '        ' --- 4️⃣ Clear target table ---
    '        Using delCmd As New SqlCommand($"DELETE FROM [{tableName}]", trgConn)
    '            delCmd.ExecuteNonQuery()
    '        End Using

    '        ' --- 5️⃣ Check if table has an IDENTITY column ---
    '        Dim hasIdentity As Boolean = False
    '        Using chkCmd As New SqlCommand("
    '        SELECT COUNT(*) FROM sys.columns 
    '        WHERE object_id = OBJECT_ID(@Table) AND is_identity = 1", trgConn)
    '            chkCmd.Parameters.AddWithValue("@Table", tableName)
    '            hasIdentity = (CInt(chkCmd.ExecuteScalar()) > 0)
    '        End Using

    '        ' --- 6️⃣ Enable IDENTITY_INSERT if necessary ---
    '        If hasIdentity Then
    '            Using onCmd As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] ON", trgConn)
    '                onCmd.ExecuteNonQuery()
    '            End Using
    '        End If

    '        ' --- 7️⃣ Bulk copy into DentistX ---
    '        Using bulkCopy As New SqlBulkCopy(trgConn)
    '            bulkCopy.DestinationTableName = tableName
    '            bulkCopy.BulkCopyTimeout = 600
    '            bulkCopy.WriteToServer(data)
    '        End Using

    '        ' --- 8️⃣ Disable IDENTITY_INSERT ---
    '        If hasIdentity Then
    '            Using offCmd As New SqlCommand($"SET IDENTITY_INSERT [{tableName}] OFF", trgConn)
    '                offCmd.ExecuteNonQuery()
    '            End Using
    '        End If

    '        _logger.Log($" ✅ Copied {data.Rows.Count} rows into DentistX.{tableName}")

    '    Catch ex As Exception
    '        _logger.Log($" ⚠️ Error copying table {tableName}: {ex.Message}")
    '    End Try
    'End Sub

    'Private Sub CopyData2(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '    Try

    '        ' --- 1️⃣ Read all data from source ---
    '        Dim selectSQL As String = $"SELECT * FROM [{tableName}]"
    '        Dim data As New DataTable()
    '        Using srcCmd As New SqlCommand(selectSQL, srcConn)
    '            Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                data.Load(reader)
    '            End Using
    '        End Using
    '        _logger.Log($"Read {data.Rows.Count} rows from Dentist.{tableName}")

    '        If data.Rows.Count = 0 Then Exit Sub

    '        ' --- 2️⃣ Get destination column names from DentistX ---
    '        'Dim destCols As New List(Of (Name As String, TypeName As String))
    '        Dim destCols = GetColumnList(trgConn, tableName)
    '        Dim destColNames As HashSet(Of String) = New HashSet(Of String)(
    '        destCols.Select(Function(c) c.Name),
    '        StringComparer.OrdinalIgnoreCase
    '    )

    '        ' --- 3️⃣ Drop extra columns from source ---
    '        For Each col As DataColumn In data.Columns.Cast(Of DataColumn).ToList()
    '            If Not destColNames.Contains(col.ColumnName) Then
    '                _logger.Log($"Dropping extra column {col.ColumnName} from Dentist.{tableName}")
    '                data.Columns.Remove(col)
    '            End If
    '        Next

    '        ' --- 4️⃣ Handle type mismatches ---
    '        Dim destTypeMap As Dictionary(Of String, String) = destCols.ToDictionary(Function(c) c.Name, Function(c) c.Type, StringComparer.OrdinalIgnoreCase)

    '        For Each col As DataColumn In data.Columns
    '            Dim targetTypeName As String = ""
    '            If destTypeMap.TryGetValue(col.ColumnName, targetTypeName) Then
    '                For Each row As DataRow In data.Rows
    '                    If row.IsNull(col) Then Continue For

    '                    Try
    '                        ' Only handle conversions for common mismatches
    '                        Select Case targetTypeName.ToLower()
    '                            Case "int", "bigint", "smallint", "tinyint"
    '                                row(col) = Convert.ToInt32(row(col))
    '                            Case "bit"
    '                                Dim val As String = row(col).ToString().Trim().ToLower()
    '                                row(col) = (val = "1" OrElse val = "true" OrElse val = "yes")
    '                            Case "decimal", "numeric", "money", "float", "real"
    '                                row(col) = Convert.ToDecimal(row(col))
    '                            Case "datetime", "smalldatetime", "date"
    '                                row(col) = Convert.ToDateTime(row(col))
    '                            Case "uniqueidentifier"
    '                                If Not Guid.TryParse(row(col).ToString(), Nothing) Then
    '                                    row(col) = DBNull.Value
    '                                End If
    '                        End Select
    '                    Catch ex As Exception
    '                        ' Set to DBNull if conversion fails
    '                        row(col) = DBNull.Value
    '                    End Try
    '                Next
    '            End If
    '        Next

    '        ' --- 5️⃣ Delete all data from DentistX before inserting ---
    '        Using delCmd As New SqlCommand($"DELETE FROM [{tableName}]", trgConn)
    '            delCmd.ExecuteNonQuery()
    '        End Using

    '        ' --- 6️⃣ Bulk copy into DentistX ---
    '        Using bulkCopy As New SqlBulkCopy(trgConn)
    '            bulkCopy.DestinationTableName = tableName
    '            bulkCopy.BulkCopyTimeout = 600
    '            bulkCopy.WriteToServer(data)
    '        End Using

    '        _logger.Log($"✅ Copied {data.Rows.Count} rows into DentistX.{tableName}")
    '    Catch ex As Exception
    '        _logger.Log($"⚠️ Error copying table {tableName}: {ex.Message}")
    '    End Try
    'End Sub

    'Private Sub CopyData1(srcConn As SqlConnection, trgConn As SqlConnection, tableName As String)
    '    Try
    '        Dim selectSQL As String = $"SELECT * FROM [{tableName}]"
    '        Dim data As New DataTable()
    '        Using srcCmd As New SqlCommand(selectSQL, srcConn)
    '            Using reader As SqlDataReader = srcCmd.ExecuteReader()
    '                data.Load(reader)
    '            End Using
    '        End Using

    '        _logger.Log($"Read {data.Rows.Count} rows from {tableName}")

    '        If data.Rows.Count = 0 Then Exit Sub

    '        ' Clear target first (keeping PK identities)
    '        Using delCmd As New SqlCommand($"DELETE FROM [{tableName}]", trgConn)
    '            delCmd.ExecuteNonQuery()
    '        End Using

    '        Using bulkCopy As New SqlBulkCopy(trgConn)
    '            bulkCopy.DestinationTableName = tableName
    '            bulkCopy.WriteToServer(data)
    '        End Using

    '        _logger.Log($"Copied {data.Rows.Count} rows to DentistX.{tableName}")
    '    Catch ex As Exception
    '        _logger.Log($"⚠️ Error copying table {tableName}: {ex.Message}")
    '    End Try
    'End Sub

#End Region

End Class
