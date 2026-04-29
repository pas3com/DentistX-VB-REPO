Imports System.Drawing
Imports System.IO
Imports System.Linq

''' <summary>
''' Supplier purchase invoice: clinic header from <see cref="ClinicDATA"/>, line items loaded by invoice id.
''' All layout is in the designer; this file only assigns data. No report parameters (nothing shown in the parameter UI).
''' </summary>
Public Class SupplierInvoice

    Public Sub New()
        InitializeComponent()
        Me.RequestParameters = False
    End Sub

    ''' <param name="supplier">Supplier master record (or minimal instance with at least name).</param>
    ''' <param name="invoice">Buy invoice header; <see cref="BuyInvoice.InvoiceID"/> selects line items.</param>
    Friend Sub New(supplier As Supplier, invoice As BuyInvoice)
        Me.New()
        If supplier Is Nothing Then Throw New ArgumentNullException(NameOf(supplier))
        If invoice Is Nothing Then Throw New ArgumentNullException(NameOf(invoice))

        Dim lineRepo As New BuyInvoiceLineItemRepository(DentistXDATA.GetConnection.ConnectionString)
        Me.DataSource = lineRepo.GetByInvoice(invoice.InvoiceID).ToList()

        Dim clinic = New ClinicDATA().SelectAll().FirstOrDefault()
        If clinic IsNot Nothing Then
            ApplyClinic(clinic)
        Else
            xrPicClinicLogo.Visible = False
        End If

        ApplySupplier(supplier)
        ApplyInvoice(invoice)
    End Sub

    Private Sub ApplyClinic(c As Clinic)
        xrLblClinicNameEn.Text = Nz(c.ClinicNameEn)
        xrLblClinicNameAr.Text = Nz(c.ClinicNameAr)

        xrLblEngDrVal.Text = Nz(c.DrNameEn)
        xrLblEngSpecVal.Text = Nz(c.SpecialistEn)
        xrLblEngAddrVal.Text = Nz(c.AddressEn)
        xrLblEngPhoneVal.Text = Nz(c.Phone)
        xrLblEngMobileVal.Text = Nz(c.Mobile)
        xrLblEngEmailVal.Text = Nz(c.Email)

        xrLblArDrVal.Text = Nz(c.DrNameAr)
        xrLblArSpecVal.Text = Nz(c.SpecialistAr)
        xrLblArAddrVal.Text = Nz(c.AddressAr)
        xrLblArPhoneVal.Text = Nz(c.Phone)
        xrLblArMobileVal.Text = Nz(c.Mobile)
        xrLblArEmailVal.Text = Nz(c.Email)

        SetClinicLogo(c.ClinicLogo)
    End Sub

    Private Sub ApplySupplier(s As Supplier)
        xrLblSupNameVal.Text = Nz(s.SupplierName)
        xrLblSupContactVal.Text = Nz(s.ContactPerson)
        xrLblSupPhoneVal.Text = Nz(s.PhoneNumber)
        xrLblSupEmailVal.Text = Nz(s.EmailAddress)
        xrLblSupAddrVal.Text = Nz(s.PhysicalAddress)
        xrLblSupPayVal.Text = Nz(s.PaymentTerms)
    End Sub

    Private Sub ApplyInvoice(inv As BuyInvoice)
        xrLblInvNoVal.Text = inv.InvoiceID.ToString()
        xrLblInvDateVal.Text = inv.InvoiceDate.ToString("dd/MM/yyyy")
        xrLblInvDueVal.Text = inv.DueDate.ToString("dd/MM/yyyy")
        xrLblInvStatusVal.Text = Nz(inv.InvoiceStatus)
        xrLblInvTotalVal.Text = inv.TotalAmount.ToString("N2")
        xrLblGrandTotalVal.Text = inv.TotalAmount.ToString("N2")
    End Sub

    Private Sub SetClinicLogo(logoBytes As Byte())
        If logoBytes Is Nothing OrElse logoBytes.Length = 0 Then
            xrPicClinicLogo.Visible = False
            Return
        End If
        Try
            Using ms As New MemoryStream(logoBytes)
                xrPicClinicLogo.Image = Image.FromStream(ms)
            End Using
            xrPicClinicLogo.Visible = True
        Catch
            xrPicClinicLogo.Visible = False
        End Try
    End Sub

    Private Shared Function Nz(s As String) As String
        Return If(String.IsNullOrWhiteSpace(s), "—", s.Trim())
    End Function

End Class
