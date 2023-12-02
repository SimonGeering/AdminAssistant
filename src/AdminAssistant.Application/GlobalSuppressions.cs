// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.Modules.AccountsModule.Queries.BankQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.Modules.AccountsModule.Queries.BankAccountTypesQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.Modules.ContactsModule.Queries.ContactQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.Modules.CalendarModule.Queries.ReminderQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.Modules.AssetRegisterModule.Queries.AssetQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.Modules.CoreModule.Queries.CurrenciesQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.Modules.DocumentsModule.Queries.DocumentQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.Modules.MailModule.Queries.MailMessageQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.Modules.TasksModule.Queries.TaskListQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.Modules.BudgetModule.Queries.BudgetQuery")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Reviewed - Prefer readability of the verbose form", Scope = "member", Target = "~M:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountByIDQueryHandler.Handle(AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountByIDQuery,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Ardalis.Result.Result{AdminAssistant.DomainModel.Modules.AccountsModule.BankAccount}}")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Reviewed - Prefer readability of the verbose form", Scope = "member", Target = "~M:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankByIDQueryHandler.Handle(AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankByIDQuery,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Ardalis.Result.Result{AdminAssistant.DomainModel.Modules.AccountsModule.Bank}}")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Reviewed - Prefer readability of the verbose form", Scope = "member", Target = "~M:AdminAssistant.DomainModel.Modules.ContactsModule.CQRS.ContactByIDQueryHandler.Handle(AdminAssistant.DomainModel.Modules.ContactsModule.CQRS.ContactByIDQuery,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Ardalis.Result.Result{AdminAssistant.DomainModel.Modules.ContactsModule.Contact}}")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Reviewed - Prefer readability of the verbose form", Scope = "member", Target = "~M:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountTransactionsByBankAccountIDQueryHandler.Handle(AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountTransactionsByBankAccountIDQuery,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Ardalis.Result.Result{System.Collections.Generic.IEnumerable{AdminAssistant.DomainModel.Modules.AccountsModule.BankAccountTransaction}}}")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Reviewed - Prefer readability of the verbose form", Scope = "member", Target = "~M:AdminAssistant.DomainModel.Modules.CoreModule.CQRS.CurrencyByIDQueryHandler.Handle(AdminAssistant.DomainModel.Modules.CoreModule.CQRS.CurrencyByIDQuery,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Ardalis.Result.Result{AdminAssistant.DomainModel.Modules.CoreModule.Currency}}")]
