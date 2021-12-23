using DotnetNLayerProject.Web.DTOs;
using DotnetNLayerProject.Core.Models;
using DotnetNLayerProject.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Web.Filters
{
    public class NotFoundFilter:ActionFilterAttribute
    {
        private readonly ICategoryService _categoryService;
         
        public NotFoundFilter(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Burada Values parametrelere gelen degerleri alır.Bu Filter Parametresi Id olan action uygulanacagı icin id degerlerini alır 
            int id =(int)context.ActionArguments.Values.FirstOrDefault();
            var category = await _categoryService.GetByIdAsync(id);
            if (category!=null)
            {
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                

                errorDto.Errors.Add($"id'si {id} olan category veritabanında bulunamadı.");

                context.Result = new RedirectToActionResult("Error","Home",errorDto);
                    // api projelerinde tipi Json olan bu fonksiyon donulurken new NotFoundObjectResult(errorDto);
            }

        }

    }
}
