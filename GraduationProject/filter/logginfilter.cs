using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace GraduationProject.filter
{
    public class Logginfilter : Attribute, IActionFilter
    {
        private Stopwatch _stopwatch;
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            Console.WriteLine($"Excuted Action : {context.ActionDescriptor.DisplayName}");
            Console.WriteLine($"Excution Time : {_stopwatch.ElapsedMilliseconds} ms");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
            Console.WriteLine($"Excution Action : {context.ActionDescriptor.DisplayName}");
            Console.WriteLine($"Request Method : {context.HttpContext.Request.Method}");
            Console.WriteLine($"Request Path : {context.HttpContext.Request.Path}");
        }
    }
}
