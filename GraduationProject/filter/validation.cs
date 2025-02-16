using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoForMvc.Filters
{
    public class ValidationFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new
                    NotFoundObjectResult(context.ModelState);
            //BadRequestObjectResult(context.ModelState);
        }
    }
}
