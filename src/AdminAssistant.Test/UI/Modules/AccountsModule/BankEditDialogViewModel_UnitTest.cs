#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    public class BankEditDialogViewModel_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        [SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "WIP")]
        public async Task ShowAnNewBankAccount_WhenOpenedForCreate()
        {
            await Task.Delay(1000).ConfigureAwait(false);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
