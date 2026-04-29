Public Class FormAbout
    Private Sub btnRequestLicense_Click(sender As Object, e As EventArgs) Handles btnRequestLicense.Click
        CursorLicManager.CreateRequestFileWithForm(Me)
    End Sub

    Private Sub btnApplyLicense_Click(sender As Object, e As EventArgs) Handles btnApplyLicense.Click
        CursorLicManager.ReadAndApplyResponseFile(Me)
        CursorLicManager.GetLicenseInfo()
    End Sub

    Private Sub FormAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtStatus.Text = CursorLicManager.GetStatusText()
        txtAbout.Text = If(Eng,
            "DentistX is an all-in-one clinic management system that streamlines daily workflow " &
            "and keeps your practice organized. It provides patient files, visits and treatment history, " &
            "appointment scheduling, billing and invoices, prescriptions, inventory tracking, " &
            "and reporting dashboards to help you monitor performance and revenue." &
            Environment.NewLine & Environment.NewLine &
            "Key features include:" & Environment.NewLine &
            "- Patient profiles with comprehensive clinical notes and images" & Environment.NewLine &
            "- Fast appointment planning with reminders" & Environment.NewLine &
            "- Treatment plans, billing, and receipts" & Environment.NewLine &
            "- Stock and supplier management for materials and medications" & Environment.NewLine &
            "- Analytics and reports for business insights",
            "DentistX هو نظام متكامل لإدارة عيادة الأسنان يسهّل سير العمل اليومي وينظّم بيانات العيادة. " &
            "يوفر ملفات المرضى وسجل الزيارات وخطط العلاج، وجدولة المواعيد، والفوترة والإيصالات، " &
            "والوصفات الطبية، وإدارة المخزون، ولوحات التقارير لمتابعة الأداء والإيرادات." &
            Environment.NewLine & Environment.NewLine &
            "أهم المزايا:" & Environment.NewLine &
            "- ملفات مرضى شاملة مع الملاحظات والصور السريرية" & Environment.NewLine &
            "- تخطيط سريع للمواعيد مع التذكيرات" & Environment.NewLine &
            "- خطط علاج، فواتير، وإيصالات" & Environment.NewLine &
            "- إدارة المواد والأدوية والموردين" & Environment.NewLine &
            "- تحليلات وتقارير لمتابعة الأداء")
    End Sub
End Class