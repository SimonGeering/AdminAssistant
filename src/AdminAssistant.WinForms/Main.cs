namespace AdminAssistant.WinForms;

public partial class Main : Syncfusion.WinForms.Controls.SfForm
{
    public Main()
    {
        InitializeComponent();

        SuspendLayout();

        ThemeName = Program.ThemeName;
        ShowIcon = false;
        Text = "Admin Assistant";

        ResumeLayout(false);
    }
}
