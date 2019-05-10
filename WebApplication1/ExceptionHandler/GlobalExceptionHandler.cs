using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using EmployeeService.Models;
using Serilog;


namespace EmployeeService.ExceptionHandler
{
    public static class GlobalExceptionHandler
    {
              

        public static void OnConfiguringException(this IApplicationBuilder app)
        {


            app.UseExceptionHandler(appError =>

            {
                appError.Run(async Context =>

                {
                    Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    Context.Response.ContentType = "application/json";

                    var contextFeature = Context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        Log.Error($"Error occurred: {contextFeature.Error}");

                        await Context.Response.WriteAsync(new ExceptionModel()
                        {
                            StatusCode = Context.Response.StatusCode,
                            Message = "Internal server error"


                        }.ToString());

                    }



                }


                );

            }


            );


        }


    }
}
