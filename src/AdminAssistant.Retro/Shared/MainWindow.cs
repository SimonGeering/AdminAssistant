using AdminAssistant.Shared.UI;

namespace AdminAssistant.Retro.Shared
{
    internal sealed class MainWindow : Toplevel
    {
        private IMainWindowViewModel VM { get; init; }
        public MainWindow(IMainWindowViewModel vm)
        {
            VM = vm;
            Title = "Admin Assistant";
            Add(MainMenu);
            Add(MainStatusBar);
        }

        private MenuBar MainMenu => new()
        {
            UseKeysUpDownAsKeysLeftRight = true,
            Key = KeyCode.F9,
            Title = "TestMenuBar",
            Menus =
            [
                new MenuBarItem("_File",
                [
                    new MenuItem("_Quit", "", MainWindow.Quit)
                ]),
                new MenuBarItem("_View",
         [
                    new MenuItem("_Accounts", "", () => ShowModule("Accounts")),
                    new MenuItem("A_dmin", "", () => ShowModule("Admin")),
                    new MenuItem("A_ssetRegister", "", () => ShowModule("AssetRegister")),
                    new MenuItem("_Billing", "", () => ShowModule("Billing")),
                    new MenuItem("B_udget", "", () => ShowModule("Budget")),
                    new MenuItem("_Calendar", "", () => ShowModule("Calendar")),
                    new MenuItem("C_ontacts", "", () => ShowModule("Contacts")),
                    new MenuItem("Dashboard", "", () => ShowModule("Dashboard")),
                    new MenuItem("_Documents", "", () => ShowModule("Documents")),
                    new MenuItem("_Notes", "", () => ShowModule("Notes")),
                    new MenuItem("_Mail", "", () => ShowModule("Mail")),
                    new MenuItem("_Reports", "", () => ShowModule("Reports")),
                    new MenuItem("_Tasks", "", () => ShowModule("Tasks")),
                ])
            ]
        };

        // Placeholder method for launching module views
        private static void ShowModule(string moduleName)
            => MessageBox.Query(40, 7, "Module Launch", $"You selected: {moduleName}", "OK");

        private static StatusBar MainStatusBar => new(
        [
            new Shortcut(Application.QuitKey, "Quit", MainWindow.Quit),
        ]);

        private static void Quit() => Application.RequestStop();

#pragma warning disable S125
        // static void AccountsBankAccountEdit(IServiceScope scope)
        // {
        //     var bankAccountEditDialog = scope.ServiceProvider.GetRequiredService<BankAccountEditDialog>();
        //     Application.Run(bankAccountEditDialog);
        // }
#pragma warning restore S125
    }
}
