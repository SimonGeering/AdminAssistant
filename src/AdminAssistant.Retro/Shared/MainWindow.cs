using AdminAssistant.Shared.UI;

namespace AdminAssistant.Retro.Shared
{
    internal class MainWindow : Toplevel
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
                new MenuBarItem("_View", Array.Empty<MenuItem>())
                //     new MenuBarItem ("_Accounts", new MenuItem []
                //     {
                //         new MenuItem ("_BankAccountEdit", "", () => AccountsBankAccountEdit(scope)),
                //     })
            ]
        };

        private StatusBar MainStatusBar => new(
        [
            new Shortcut(Application.QuitKey, "Quit", MainWindow.Quit),
        ]);

        private static void Quit() => Application.RequestStop();

        // static void AccountsBankAccountEdit(IServiceScope scope)
        // {
        //     var bankAccountEditDialog = scope.ServiceProvider.GetRequiredService<BankAccountEditDialog>();
        //     Application.Run(bankAccountEditDialog);
        // }
    }
}
