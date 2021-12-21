using DotnetNLayerProject.API.DTOs;
using DotnetNLayerProject.Core.Models;
using DotnetNLayerProject.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNLayerProject.API.Filters
{
    public class NotFoundFilter:ActionFilterAttribute
    {
        private readonly IProductService _productService;
         
        public NotFoundFilter(IProductService productService)
        {
            _productService = productService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Burada Values parametrelere gelen degerleri alır.Bu Filter Parametresi Id olan action uygulanacagı icin id degerlerini alır 
            int id =(int)context.ActionArguments.Values.FirstOrDefault();
            var product = await _productService.GetByIdAsync(id);
            if (product!=null)
            {
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Status = 404;

                errorDto.Errors.Add($"id'si {id} olan ürün veritabanında bulunamadı.");

                context.Result = new NotFoundObjectResult(errorDto);
            }

        }

    }
}
