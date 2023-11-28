using AdminAssistant.Abstractions.DomainModel.Shared;
using AdminAssistant.Abstractions.DomainModel.Shared.Validation;

namespace AdminAssistant.DomainModel.Shared.Validation;

internal sealed class EntityNameValidator : AbstractValidator<EntityName>, IEntityNameValidator
{
    public EntityNameValidator()
        => RuleFor(x => x.Value)
            .NotEmpty()
            .MaximumLength(EntityName.MaxLength);
}
