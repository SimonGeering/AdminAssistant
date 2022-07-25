namespace Ardalis.Result;

public static class Extensions
{
    public static IDictionary<string, string[]> ToErrorDetails(this List<ValidationError> validationErrors)
    {
        var errorDetails = new Dictionary<string, string[]>();

        validationErrors.ForEach((err)
            => errorDetails.Add(err.Identifier, new[] { err.ErrorMessage }));

        return errorDetails;
    }
}
