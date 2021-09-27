using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using MediatR;
using Ardalis.Result.FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

public record BankAccountCreateCommand(BankAccount BankAccount) : IRequest<Result<BankAccount>>;
