Imports System.Windows.Forms
Imports System.Reflection
Imports Dapper
Imports System.Data.SqlClient

Public Class FrmAccessManager
    Dim FormData As New FormAccessDATA

    Private Sub FrmAccessManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadForms()
    End Sub

    Private Sub LoadForms()
        Dim Forms = FormData.SelectAll()
        GridControl1.DataSource = Forms
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAddForm.Click
        'InsertAllFormsIntoFormsTable()
        'InsertOrUpdateFormsWithDescriptions()
        Dim FormName = TxtFormName.Text.Trim()
        If String.IsNullOrEmpty(FormName) Then
            MessageBox.Show("Form name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim newForm As New FormAccess With {.FormName = FormName}
        If FormData.Add(newForm) Then
            MessageBox.Show("Form added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadForms()
        Else
            MessageBox.Show("Failed to add Form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEditForm.Click
        If GridView1.FocusedRowHandle < 0 Then
            MessageBox.Show("Select a Form to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim selectedForm As FormAccess = GridView1.GetRow(GridView1.FocusedRowHandle)
        TxtFormName.Text = selectedForm.FormName
        If String.IsNullOrEmpty(TxtFormName.Text) Then
            MessageBox.Show("Form name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim updated As New FormAccess With {
            .FormID = selectedForm.FormID,
            .FormName = TxtFormName.Text.Trim(),
            .Description = selectedForm.Description,
            .DisplayTitle = selectedForm.DisplayTitle,
            .DisplayTitleAr = selectedForm.DisplayTitleAr}
        If FormData.Update(updated) Then
            MessageBox.Show("Form updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadForms()
        Else
            MessageBox.Show("Failed to update Form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelForm.Click
        If GridView1.FocusedRowHandle < 0 Then
            MessageBox.Show("Select a Form to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim selectedForm As FormAccess = GridView1.GetRow(GridView1.FocusedRowHandle)
        If FormData.Delete(selectedForm.FormID) Then
            MessageBox.Show("Form deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadForms()
        Else
            MessageBox.Show("Failed to delete Form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub



    Private Sub InsertOrUpdateFormsWithDescriptions()
        Dim formTypes = From t In Assembly.GetExecutingAssembly().GetTypes()
                        Where t.IsSubclassOf(GetType(Form)) AndAlso Not t.IsAbstract
                        Select t

        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Open()

            For Each formType In formTypes
                Try
                    ' Instantiate the form to access its .Text property
                    Dim formInstance As Form = CType(Activator.CreateInstance(formType), Form)
                    Dim formName As String = formType.Name
                    Dim description As String = formInstance.Text

                    ' Check if the form already exists
                    Dim exists = conn.ExecuteScalar(Of Integer)(
                    "SELECT COUNT(*) FROM Forms WHERE FormName = @FormName", New With {.FormName = formName})

                    If exists = 0 Then
                        ' Insert new
                        conn.Execute("INSERT INTO Forms (FormName, Description) VALUES (@FormName, @Description)",
                                 New With {.FormName = formName, .Description = description})
                    Else
                        ' Update existing description
                        conn.Execute("UPDATE Forms SET Description = @Description WHERE FormName = @FormName",
                                 New With {.FormName = formName, .Description = description})
                    End If

                    formInstance.Dispose()

                Catch ex As Exception
                    ' Skip forms that fail to instantiate
                    Console.WriteLine($"Skipped {formType.Name}: {ex.Message}")
                End Try
            Next
        End Using

        MessageBox.Show("Forms updated successfully with descriptions.")
    End Sub


    Private Sub InsertAllFormsIntoFormsTable()
        Dim formTypes = From t In Assembly.GetExecutingAssembly().GetTypes()
                        Where t.IsSubclassOf(GetType(Form)) AndAlso Not t.IsAbstract
                        Select t

        Using conn As New SqlConnection(DentistXDATA.GetConnection.ConnectionString)
            conn.Open()
            For Each formType In formTypes
                Dim formName = formType.Name
                Dim formDESC = formType.ToString
                ' Check if it already exists
                Dim exists = conn.ExecuteScalar(Of Integer)(
                "SELECT COUNT(*) FROM Forms WHERE FormName = @FormName", New With {.FormName = formName})

                If exists = 0 Then
                    conn.Execute("INSERT INTO Forms (FormName) VALUES (@FormName)", New With {.FormName = formName, .Description = formDESC})
                End If
            Next
        End Using

        MessageBox.Show("Forms inserted successfully.")
    End Sub

End Class
