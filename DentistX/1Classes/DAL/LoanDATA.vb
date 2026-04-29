Imports System.Data.SqlClient
Imports Dapper

Public Class LoanDATA


    Private ConnectionString As String = DentistXDATA.GetConnection.ConnectionString


    Public Function GetContacts() As List(Of Contact)
        Using conn As New SqlConnection(ConnectionString)
            Return conn.Query(Of Contact)("SELECT * FROM Contacts ORDER BY CName").ToList()
        End Using
    End Function

    Public Function GetContactBalances() As List(Of ContactBalance)
        Using conn As New SqlConnection(ConnectionString)
            Return conn.Query(Of ContactBalance)("SELECT * FROM vw_ContactBalances ORDER BY CName").ToList()
        End Using
    End Function

    Public Function GetLoansByContact(contactId As Integer) As List(Of LoanBalance)
        Using conn As New SqlConnection(ConnectionString)
            Return conn.Query(Of LoanBalance)("SELECT * FROM vw_LoanBalances WHERE ContactID = @ContactID", New With {contactId}).ToList()
        End Using
    End Function

    Public Function GetLoansByContact(contactId As Integer, loanId As Integer) As List(Of LoanBalance)
        Using conn As New SqlConnection(ConnectionString)
            Return conn.Query(Of LoanBalance)("SELECT * FROM vw_LoanBalances WHERE ContactID = @ContactID AND LoanID = @LoanID", New With {contactId, loanId}).ToList()
        End Using
    End Function

    Public Function GetRepayments(loanId As Integer) As List(Of Repayment)
        Using conn As New SqlConnection(ConnectionString)
            Return conn.Query(Of Repayment)("SELECT * FROM Repayments WHERE LoanID = @LoanID ORDER BY RepaymentDate", New With {loanId}).ToList()
        End Using
    End Function

    Public Function AddLoan(loan As Loan) As Integer
        Using conn As New SqlConnection(ConnectionString)
            Dim param = New With {
                loan.ContactID,
                loan.Amount,
                loan.Direction,
                loan.Description,
                loan.LoanDate
            }
            Return conn.ExecuteScalar(Of Integer)("sp_AddLoan", param, commandType:=CommandType.StoredProcedure)
        End Using
    End Function

    Public Function UpdateLoan(loan As Loan) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "
            UPDATE Loans 
            SET 
                ContactID = @ContactID,
                Amount = @Amount,
                Direction = @Direction,
                Description = @Description,
                LoanDate = @LoanDate
            WHERE LoanID = @LoanID
        "
            Dim affectedRows = conn.Execute(sql, loan)
            Return affectedRows > 0
        End Using
    End Function

    Public Function DeleteLoan(loanId As Integer) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "DELETE FROM Loans WHERE LoanID = @LoanID"
            Dim affectedRows = conn.Execute(sql, New With {loanId})
            Return affectedRows > 0
        End Using
    End Function

    Public Function AddRepayment(repayment As Repayment) As Integer
        Using conn As New SqlConnection(ConnectionString)
            Dim param = New With {
                repayment.LoanID,
                repayment.Amount,
                repayment.Notes,
                repayment.RepaymentDate
            }
            Return conn.ExecuteScalar(Of Integer)("sp_AddRepayment", param, commandType:=CommandType.StoredProcedure)
        End Using
    End Function

    Public Function UpdateRepayment(repayment As Repayment) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "
            UPDATE Repayments
            SET
                LoanID = @LoanID,
                Amount = @Amount,
                Notes = @Notes,
                RepaymentDate = @RepaymentDate
            WHERE RepaymentID = @RepaymentID
        "
            Dim affectedRows = conn.Execute(sql, repayment)
            Return affectedRows > 0
        End Using
    End Function

    Public Function DeleteRepayment(repaymentId As Integer) As Boolean
        Using conn As New SqlConnection(ConnectionString)
            Dim sql As String = "DELETE FROM Repayments WHERE RepaymentID = @RepaymentID"
            Dim affectedRows = conn.Execute(sql, New With {repaymentId})
            Return affectedRows > 0
        End Using
    End Function



End Class
