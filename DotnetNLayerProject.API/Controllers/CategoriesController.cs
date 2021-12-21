using AutoMapper;
using DotnetNLayerProject.API.DTOs;
using DotnetNLayerProject.Core.Models;
using DotnetNLayerProject.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;//Hangi interface'deki islemleri yapılmasını istiyorsak oradan obje uretiliyor
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService,IMapper mapper)
        {
            _categoryService = categoryService;//Bunu yapmamızın sebebi constructor'dabelirtilen interface'i kim implemente alirsa onu referans alır burada
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));//Entity'den List seklinde gelen veriler burada DTO'ya cevrilir.
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProductById(int id)
        {
            var category = await _categoryService.GetWithProductsByIdAsync(id);
            return Ok(_mapper.Map<CategoryWithProductDto>(category));
        }


        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var newcategory=await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));//Bu bir entity
            return Created(string.Empty,_mapper.Map<CategoryDto>(newcategory));
        }
        [HttpPut]
        public IActionResult Update(CategoryDto categoryDto)
        {
            var category = _categoryService.Update(_mapper.Map<Category>(categoryDto));

            return NoContent();//Client'a 204 ile baslayan success durum kodu doner
        }
        [HttpDelete("{id}")]
        public IActionResult Remove(int Id)
        {
            var category = _categoryService.GetByIdAsync(Id).Result;//Result METODU asenkron ve awir keywordunu kullanmadan asenkron islemi yapmamızı saglar.
            _categoryService.Remove(category);
            return NoContent();
        }

      


    }
}
