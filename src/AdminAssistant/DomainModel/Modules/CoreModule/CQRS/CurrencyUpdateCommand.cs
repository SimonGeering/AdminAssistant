using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.Validation;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS
{
    public class CurrencyUpdateCommand : IRequest<Result<Currency>>
    {
        public CurrencyUpdateCommand(Currency currency) => Currency = currency;

        public Currency Currency { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
        internal class CurrencyUpdateHandler : RequestHandlerBase<CurrencyUpdateCommand, Result<Currency>>
        {
            private readonly ICurrencyRepository _currencyRepository;
            private readonly ICurrencyValidator _currencyValidator;

            public CurrencyUpdateHandler(ILoggingProvider loggingProvider, ICurrencyRepository currencyRepository, ICurrencyValidator currencyValidator)
                : base(loggingProvider)
            {
                _currencyRepository = currencyRepository;
                _currencyValidator = currencyValidator;
            }

            public override async Task<Result<Currency>> Handle(CurrencyUpdateCommand command, CancellationToken cancellationToken)
            {
                var validationResult = await _currencyValidator.ValidateAsync(command.Currency, cancellationToken).ConfigureAwait(false);

                if (validationResult.IsValid == false)
                {
                    return Result<Currency>.Invalid(validationResult.AsErrors());
                }

                var result = await _currencyRepository.SaveAsync(command.Currency).ConfigureAwait(false);
                return Result<Currency>.Success(result);
            }
        }
    }
}