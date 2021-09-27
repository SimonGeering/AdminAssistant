using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

public record BankAccountUpdateCommand(BankAccount BankAccount) : IRequest<Result<BankAccount>>;
