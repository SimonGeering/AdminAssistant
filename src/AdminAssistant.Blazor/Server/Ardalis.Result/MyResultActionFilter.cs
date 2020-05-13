#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CA1710 // Identifiers should have correct suffix
//----------------------------------------------------------------------------------------------------------------------
//     Author:  Steve Smith (https://github.com/ardalis)
//     GitHub:  https://github.com/ardalis/Result/blob/master/sample/Ardalis.Result.SampleWeb/MyResultActionFilter.cs
//     License: MIT
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ardalis.Result.SampleWeb
{
    // TODO: Move to an Ardalis.Result.AspNetCore Nuget package
    public class MyResultActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!((context.Result as ObjectResult).Value is IResult result)) return;

            if (!(context.Controller is ControllerBase controller)) return;

            if (result.Status == ResultStatus.NotFound)
                context.Result = controller.NotFound();

            if (result.Status == ResultStatus.Invalid)
            {
                foreach (var error in result.ValidationErrors)
                {
                    (context.Controller as ControllerBase).ModelState.AddModelError(error.Key, error.Value);
                }

                context.Result = controller.BadRequest(controller.ModelState);
            }

            if (result.Status == ResultStatus.Ok)
            {
                context.Result = new OkObjectResult(result.GetValue());
            }
        }
    }
}
#pragma warning restore CA1710 // Identifiers should have correct suffix
#pragma warning restore CS8602 // Dereference of a possibly null reference.
