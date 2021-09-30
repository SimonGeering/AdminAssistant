using AccountsForm = AdminAssistant.WinForms.Modules.AccountsModule.AccountsForm;

namespace AdminAssistant.WinForms;


public partial class MainForm : Syncfusion.WinForms.Controls.SfForm
{
    private readonly AccountsForm _accountsForm;

    public MainForm(AccountsForm accountsForm)
    {
        _accountsForm = accountsForm;

        InitializeComponent();

        SuspendLayout();

        ThemeName = Program.ThemeName;
        IsMdiContainer = true;
        Text = "Admin Assistant";

        _accountsForm.MdiParent = this;
        _accountsForm.Show();

        ResumeLayout(false);
    }
}
