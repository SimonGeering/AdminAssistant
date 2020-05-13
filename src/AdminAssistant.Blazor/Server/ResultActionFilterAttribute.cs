using Microsoft.AspNetCore.Mvc.Filters;

namespace AdminAssistant.Blazor.Server
{
    public class ResultActionFilterAttribute : Ardalis.Result.SampleWeb.MyResultActionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
