// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Readability", Scope = "member", Target = "~M:AdminAssistant.UI.Modules.AccountsModule.BankAccountEditDialogViewModel.GetValidationMessageForField(System.String,FluentValidation.Results.ValidationResult)~System.String")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Readability", Scope = "member", Target = "~M:AdminAssistant.UI.Modules.AccountsModule.BankAccountEditDialogViewModel.GetValidationClassForField(System.String,FluentValidation.Results.ValidationResult)~System.String")]
[assembly: SuppressMessage("Info Code Smell", "S1133:Deprecated code should be removed", Justification = "Known TODO", Scope = "member", Target = "~M:AdminAssistant.UI.ViewModelBase.OnInitializedAsync~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Minor Code Smell", "S3604:Member initializer values should not be redundant", Justification = "https://github.com/SonarSource/sonar-dotnet/issues/7624", Scope = "member", Target = "~P:AdminAssistant.Modules.AccountsModule.UI.AccountsViewModel.SubHeaderText")]
[assembly: SuppressMessage("Minor Code Smell", "S3604:Member initializer values should not be redundant", Justification = "https://github.com/SonarSource/sonar-dotnet/issues/7624", Scope = "member", Target = "~P:AdminAssistant.Modules.AccountsModule.UI.AccountsViewModel.HeaderText")]
[assembly: SuppressMessage("Minor Code Smell", "S3604:Member initializer values should not be redundant", Justification = "https://github.com/SonarSource/sonar-dotnet/issues/7624", Scope = "member", Target = "~P:AdminAssistant.Modules.AccountsModule.UI.BankAccountTransactionListViewModel.Transactions")]
[assembly: SuppressMessage("Minor Code Smell", "S3604:Member initializer values should not be redundant", Justification = "https://github.com/SonarSource/sonar-dotnet/issues/7624", Scope = "member", Target = "~F:AdminAssistant.Modules.CoreModule.AdminUI.CurrencyListViewModel._currencies")]



[assembly: SuppressMessage("Major Code Smell", "S4035:Classes implementing \"IEquatable<T>\" should be sealed", Justification = "False  positive - fails to detect generic implementation.", Scope = "type", Target = "~T:AdminAssistant.Framework.Primitives.DomainEntity`1")]
[assembly: SuppressMessage("Major Code Smell", "S4035:Classes implementing \"IEquatable<T>\" should be sealed", Justification = "False  positive - fails to detect generic implementation.", Scope = "type", Target = "~T:AdminAssistant.Framework.Primitives.ValueObject")]
[assembly: SuppressMessage("Major Bug", "S3343:Caller information parameters should come at the end of the parameter list", Justification = "False  positive - not possible due to param array.", Scope = "member", Target = "~M:AdminAssistant.Infra.Providers.ILoggingProvider.Start(System.String,System.String,System.String,System.Int32,System.Object[])")]
[assembly: SuppressMessage("Major Bug", "S3343:Caller information parameters should come at the end of the parameter list", Justification = "False  positive - not possible due to param array.", Scope = "member", Target = "~M:AdminAssistant.Infra.Providers.ILoggingProvider.Finish(System.String,System.String,System.String,System.Int32,System.Object[])")]
[assembly: SuppressMessage("Major Bug", "S3343:Caller information parameters should come at the end of the parameter list", Justification = "False  positive - not possible due to param array.", Scope = "member", Target = "~M:AdminAssistant.Infra.Providers.ILoggingProvider.Finish``1(``0,System.String,System.String,System.String,System.Int32,System.Object[])~``0")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AccountsModule.CQRS.BankAccountTypesQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.ContactsModule.CQRS.ContactQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.CalendarModule.CQRS.ReminderQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.AssetRegisterModule.CQRS.AssetQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.CoreModule.CQRS.CurrenciesQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.DocumentsModule.CQRS.DocumentQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.MailModule.CQRS.MailMessageQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.TasksModule.CQRS.TaskListQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "False positive - MediatR implementation", Scope = "type", Target = "~T:AdminAssistant.DomainModel.Modules.BudgetModule.CQRS.BudgetQuery")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Reviewed - Prefer readability of the verbose form", Scope = "member", Target = "~M:AdminAssistant.Framework.Primitives.DomainEntity`1.Equals(System.Object)~System.Boolean")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Reviewed - Prefer readability of the verbose form", Scope = "member", Target = "~M:AdminAssistant.Framework.Primitives.DomainEntity`1.Equals(AdminAssistant.Framework.Primitives.DomainEntity{`0})~System.Boolean")]
[assembly: SuppressMessage("Minor Code Smell", "S6605:Collection-specific \"Exists\" method should be used instead of the \"Any\" extension", Justification = "Requires unknown generic", Scope = "member", Target = "~M:AdminAssistant.Framework.TypeMapping.MappingProfileBase.ApplyIMapToMappings(System.Reflection.Assembly)")]
[assembly: SuppressMessage("Minor Code Smell", "S6605:Collection-specific \"Exists\" method should be used instead of the \"Any\" extension", Justification = "Requires unknown generic", Scope = "member", Target = "~M:AdminAssistant.Framework.TypeMapping.MappingProfileBase.ApplyIMapFromMappings(System.Reflection.Assembly)")]
