using AdminAssistant.UI.Shared;
using AccountsComponent = AdminAssistant.WinForms.Modules.AccountsModule.AccountsComponent;

namespace AdminAssistant.WinForms;


public partial class MainForm : Syncfusion.WinForms.Controls.SfForm
{
    private readonly IMainWindowViewModel _mainWindowViewModel;
    private readonly AccountsComponent _acccountsComponent;

    public MainForm(
        IMainWindowViewModel mainWindowViewModel,
        AccountsComponent acccountsComponent)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _acccountsComponent = acccountsComponent;

        InitializeComponent();

        SuspendLayout();

        //this.comboDropDown1.DataBindings.Add()

        Controls.Add(acccountsComponent);
        acccountsComponent.Dock = DockStyle.Fill;

        //IsMdiContainer = true;
        Text = "Admin Assistant";

        ResumeLayout(false);
    }
}
