// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Spellchecker", "CRRSP05:A misspelled word has been found", Justification = "Url", Scope = "member", Target = "~M:AdminAssistant.Infra.DAL.EntityFramework.IApplicationDbContext.SaveChangesAsync(System.Threading.CancellationToken)~System.Threading.Tasks.Task{System.Int32}")]
[assembly: SuppressMessage("Spellchecker", "CRRSP08:A misspelled word has been found", Justification = "MediatR", Scope = "namespace", Target = "~N:AdminAssistant.Framework.MediatR")]
[assembly: SuppressMessage("Spellchecker", "CRRSP04:A misspelled word has been found", Justification = "MediatR", Scope = "type", Target = "~T:AdminAssistant.Framework.MediatR.LoggingBehaviour`2")]
[assembly: SuppressMessage("Members", "CRR0026:Unused member", Justification = "DB Design", Scope = "member", Target = "~F:AdminAssistant.Infra.DAL.EntityFramework.Model.BillingSchema.Name")]
[assembly: SuppressMessage("Members", "CRR0026:Unused member", Justification = "DB Design", Scope = "member", Target = "~F:AdminAssistant.Infra.DAL.EntityFramework.Model.CalendarSchema.Name")]

[assembly: SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "EF Migration Generated Code", Scope = "member", Target = "~M:AdminAssistant.Infra.DAL.EntityFramework.Migrations.Initial.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "EF Migration Generated Code", Scope = "member", Target = "~M:AdminAssistant.Infra.DAL.EntityFramework.Migrations.Initial.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "EF Migration Generated Code", Scope = "member", Target = "~M:AdminAssistant.Infra.DAL.EntityFramework.Migrations.Initial.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
[assembly: SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "Readability", Scope = "member", Target = "~M:AdminAssistant.Framework.MediatR.LoggingBehaviour`2.Handle(`0,System.Threading.CancellationToken,MediatR.RequestHandlerDelegate{`1})~System.Threading.Tasks.Task{`1}")]
