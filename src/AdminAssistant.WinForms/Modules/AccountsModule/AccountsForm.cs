namespace AdminAssistant.WinForms.Modules.AccountsModule;

public partial class AccountsForm : Syncfusion.WinForms.Controls.SfForm
{
    public AccountsForm()
    {
        InitializeComponent();

        SuspendLayout();

        ThemeName = Program.ThemeName;
        Text = "Accounts";

        ResumeLayout(false);
    }
}

