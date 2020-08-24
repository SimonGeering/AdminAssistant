using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace Ardalis.Result.FluentValidation
{
    /// <summary>
    /// Use this until Ardalis releases the NuGet package.
    /// </summary>
    /// <remarks>
    /// Taken from https://github.com/ardalis/Result/blob/master/src/Ardalis.Result.FluentValidation/FluentValidationResultExtensions.cs
    /// </remarks>
    public static class FluentValidationResultExtensions
    {
        public static List<ValidationError> AsErrors(this ValidationResult valResult)
        {
            var resultErrors = new List<ValidationError>();

            foreach (var valFailure in valResult.Errors)
            {
                resultErrors.Add(new ValidationError() {
                    Severity = FromSeverity(valFailure.Severity),
                    ErrorMessage = valFailure.ErrorMessage,
                    Identifier = valFailure.PropertyName
                });
            }

            return resultErrors;
        }

        public static ValidationSeverity FromSeverity(Severity severity) => severity switch
        {
            Severity.Error => ValidationSeverity.Error,
            Severity.Warning => ValidationSeverity.Warning,
            Severity.Info => ValidationSeverity.Info,
            _ => throw new ArgumentOutOfRangeException(nameof(severity), "Unexpected Severity"),
        };
    }
}
