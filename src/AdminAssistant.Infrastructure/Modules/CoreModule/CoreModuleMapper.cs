using AdminAssistant.Infrastructure.EntityFramework.Model.Core;

namespace AdminAssistant.Modules.CoreModule;

public static class CoreModuleMapper
{
    public static List<CurrencyEntity> ToCurrencyEntityList(this List<Currency> domainObjects)
        => domainObjects.Select(ToCurrencyEntity).ToList();

    public static CurrencyEntity ToCurrencyEntity(this Currency domainObject)
        => new()
        {
            CurrencyID = domainObject.CurrencyID.Value,
            Symbol = domainObject.Symbol,
            DecimalFormat = domainObject.DecimalFormat
        };

    public static Currency ToCurrency(this CurrencyEntity entity)
        => new()
        {
            CurrencyID = new CurrencyId(entity.CurrencyID),
            Symbol = entity.Symbol,
            DecimalFormat = entity.DecimalFormat
        };

    public static List<Currency> ToCurrencyList(this List<CurrencyEntity> entities)
        => entities.Select(ToCurrency).ToList();
}
