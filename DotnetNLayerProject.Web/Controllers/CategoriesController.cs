using AutoMapper;
using DotnetNLayerProject.Core.Models;
using DotnetNLayerProject.Core.Services;
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
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService,IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
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
            await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return View(_mapper.Map<CategoryDto>(category));

        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto category)
        {
            _categoryService.Update(_mapper.Map<Category>(category));
            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(category);
            return RedirectToAction("Index");
        }

    }
}
