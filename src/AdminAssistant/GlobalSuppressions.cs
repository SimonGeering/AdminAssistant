// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Known list of enum values", Scope = "member", Target = "~M:AdminAssistant.UI.Shared.ModuleSelectionItem.#ctor(AdminAssistant.UI.Shared.ModuleEnum,System.String,System.String,System.String)")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Readability", Scope = "member", Target = "~M:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountByIDHandler.Handle(AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountByIDQuery,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Ardalis.Result.Result{AdminAssistant.DomainModel.Modules.AccountsModule.BankAccount}}")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountCreateHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountInfoHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountTransactionsByBankAccountIDHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountTypesHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountUpdateHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankByIDHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankCreateHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankUpdateHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.CoreModule.CQRS.CurrenciesHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.CoreModule.CQRS.CurrencyByIDHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.CoreModule.CQRS.CurrencyCreateHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.CoreModule.CQRS.CurrencyUpdateHandler")]
[assembly: SuppressMessage("Members", "CRR0043:Unused type", Justification = "CQRS", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.DocumentsModule.CQRS.DocumentHandler")]
[assembly: SuppressMessage("Spellchecker", "CRRSP06:A misspelled word has been found", Justification = "Test Data", Scope = "member", Target = "~M:AdminAssistant.DomainModel.Shared.UserContextProvider.GetCurrentUser~AdminAssistant.DomainModel.Shared.User")]
[assembly: SuppressMessage("Spellchecker", "CRRSP05:A misspelled word has been found", Justification = "MediatR", Scope = "member", Target = "~M:Microsoft.Extensions.DependencyInjection.DependencyInjectionExtensions.AddAdminAssistantServerSideDomainModel(Microsoft.Extensions.DependencyInjection.IServiceCollection)")]
