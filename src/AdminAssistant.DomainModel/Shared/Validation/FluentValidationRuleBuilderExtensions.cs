using Ardalis.GuardClauses;

namespace AdminAssistant.Shared.Validation;

internal static class FluentValidationRuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, EntityName> ValidEntityName<T>(this IRuleBuilder<T, EntityName> ruleBuilder, string overridePropertyName)
    {
        Guard.Against.NullOrEmpty(overridePropertyName);
        return ruleBuilder.ChildRules(childRule => childRule.RuleFor(x => x.Value).NotEmpty().MaximumLength(EntityName.MaxLength).WithName(overridePropertyName));
    }
    public static IRuleBuilderOptions<T, EntityDescription> ValidEntityDescription<T>(this IRuleBuilder<T, EntityDescription> ruleBuilder, string overridePropertyName)
    {
        Guard.Against.NullOrEmpty(overridePropertyName);
        return ruleBuilder.ChildRules(childRule => childRule.RuleFor(x => x.Value).NotEmpty().MaximumLength(EntityDescription.MaxLength).WithName(overridePropertyName));
    }
}
