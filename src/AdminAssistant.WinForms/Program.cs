internal static class Program
{
    public const string ThemeName = "Office2016Black";

    [STAThread]
    static void Main()
    {
        // V19.2.0.57
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDk0NzEzQDMxMzkyZTMyMmUzME4rWk1peSsxWlVQdXdacjJtQURiSGFoVlYrWHhaWW9qNURRUmpsTW5mZ1k9;NDk0NzE0QDMxMzkyZTMyMmUzMGVRbjZmV1Vhbk8rSEVxQzdTYmJJY0hmYVMxZGhldHFIT0UxTEsxdm1pWnM9;NDk0NzE1QDMxMzkyZTMyMmUzMExIL1ZBOHBmRm40a0hEZmphOWF1VnFoMnF6OVNYaHdTYkNsYzB0MFZ4N289;NDk0NzE2QDMxMzkyZTMyMmUzMGRjV1BXcDdnaDJlTHJEL3lPZXdQaTVQY2swYTdyckpkZmJTNWNBZFR6SDg9;NDk0NzE3QDMxMzkyZTMyMmUzMFJjQ2g0aFdwdVhpbkYvNUZjTjRFejRaQlVMUHEybWNuQXAvalJ6Yk1tT0U9;NDk0NzE4QDMxMzkyZTMyMmUzMFQ3RkNvQXZDUjlnMFVvaU9aTE1IVVpaN2J5LzlKVWFqNGpOS0VSTU5QZUk9;NDk0NzE5QDMxMzkyZTMyMmUzMFlSc01xd3lTN2FBR1l0N0RGTzI2bE9zNEZERXRNS1ljcDBFVkMySXMwM2M9;NDk0NzIwQDMxMzkyZTMyMmUzMFh6OGY0Y3hYTGF1R0FOQVFHN0xyWVVtSzlUY2RKa1IremVBK1NJNmdPYzQ9;NDk0NzIxQDMxMzkyZTMyMmUzMGxxQUg5Wk9nY2lkRVFNMkg4dmVRTTdrdzJXWkRZRVl3QldhcnorVXUrdEU9;NDk0NzIyQDMxMzkyZTMyMmUzMGtBdGFNVjFQQnh0Ynp5a2E4VVRBdFRUTlRqVXZzN0lEY3pXblMzVVcxckE9");

        ApplicationConfiguration.Initialize();
        Syncfusion.WinForms.Controls.SfSkinManager.LoadAssembly(typeof(Syncfusion.WinForms.Themes.Office2016Theme).Assembly);

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.Run(new AdminAssistant.WinForms.Main());
    }
}
