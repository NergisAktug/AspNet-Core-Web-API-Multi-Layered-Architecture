using AutoMapper;
//using DotnetNLayerProject.Core.Models;
//using DotnetNLayerProject.Core.Services;
using DotnetNLayerProject.Web.ApiService;
using DotnetNLayerProject.Web.DTOs;
using DotnetNLayerProject.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Web.Controllers
{
    public class CategoriesController : Controller
    {
        //private readonly ICategoryService _categoryService; artık buna gerek yok cunku web projem artık servis katmanıyla değil direk api ile haberleşiyor
        private readonly CategoryApiService _categoryApiService;//startup.cs'de AddHttpClient eklendigi icin bu servisi nerede kullanmak istiyorsak onun constructor'ına eklenmelidir.
        private readonly IMapper _mapper;
        public CategoriesController(/*ICategoryService categoryService,*/IMapper mapper,CategoryApiService categoryApiService)
        {
            //_categoryService = categoryService;
            //_mapper = mapper;
            _categoryApiService = categoryApiService;
        }
        public async Task<IActionResult> Index()
        {
            //var categories = await _categoryService.GetAllAsync(); artık business(service) katmanıyla değil direk api ile haberleşmektedir.
            var categories = await _categoryApiService.GetAllAsync();
            return View(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }
        public IActionResult Create()
        {
            return View();
        }
        /*Thread tarafında asenkron kullanılması çok daha performanslıdır.
         *Methodlar asenkron olursa Saniyede karsılayacak istek sayısı senkrona gore cok daha fazla olur
         */
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            //await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));
            await _categoryApiService.AddAsync(categoryDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            //var category = await _categoryService.GetByIdAsync(id);
            //return View(_mapper.Map<CategoryDto>(category));
            var category = await _categoryApiService.GetByIdAsync(id);
            return View(category);

        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto category)
        {
            // _categoryService.Update(_mapper.Map<Category>(category));
            await _categoryApiService.Update(category);
            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            //var category = _categoryService.GetByIdAsync(id).Result;
            //  _categoryService.Remove(category);
            
            await _categoryApiService.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
