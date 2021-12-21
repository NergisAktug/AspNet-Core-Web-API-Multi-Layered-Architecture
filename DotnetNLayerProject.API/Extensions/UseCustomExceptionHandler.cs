using DotnetNLayerProject.API.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNLayerProject.API.Extensions
{
   public static class UseCustomExceptionHandler
    {
        //Extension classlar mutlaka static class ve static method olmalıdır.
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config => //UseExceptionHandler isimli bir middleware kullandık.Bu sayde tum exceptionları yakalayacak
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var ex = error.Error;

                        ErrorDto errorDto = new ErrorDto();
                        errorDto.Status = 500;
                        errorDto.Errors.Add(ex.Message);

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto));
                    }

                });

            });
        }

    }
}
