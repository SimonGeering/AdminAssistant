using AdminAssistant.Abstractions.DomainModel.Shared;
using AdminAssistant.Abstractions.DomainModel.Shared.Validation;

namespace AdminAssistant.DomainModel.Shared.Validation;

internal sealed class EntityDescriptionValidator : AbstractValidator<EntityDescription>, IEntityDescriptionValidator
{
    public EntityDescriptionValidator()
        => RuleFor(x => x.Value)
            .NotEmpty()
            .MaximumLength(EntityDescription.MaxLength);
}
