using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

public record BankAccountInfoQuery(int OwnerID) : IRequest<Result<IEnumerable<BankAccountInfo>>>;
