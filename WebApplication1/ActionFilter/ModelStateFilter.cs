using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeService.Models;
using EmployeeService.infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeService.ActionFilter
{
    public class ModelStateFilter : IActionFilter
    {
        //Please read more about action filter in asp.net mvc
        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

        }


        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

        
}
