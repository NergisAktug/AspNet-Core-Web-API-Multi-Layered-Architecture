using AutoMapper;
using DotnetNLayerProject.API.DTOs;
using DotnetNLayerProject.API.Filters;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;//Hangi interface'deki islemleri yapılmasını istiyorsak oradan obje uretiliyor
        private readonly IMapper _mapper;//Entity'deki objeleri Client tarafındaki Dto'ya donusturmek icin kullaılan Library'dir. 
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;//Bunu yapmamızın sebebi constructor'da belirtilen interface'i kim implemente alirsa onu referans alır burada
            _mapper = mapper;
        }
        
        
       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));////Entity'den List seklinde gelen veriler burada DTO'ya cevrilir.Ve Client tarafına DTO seklinde gider
        }
       

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategoryById(int Id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(Id);//Product Id'ye gore hem product hemde category bilgilerini getirir
            return Ok(_mapper.Map<ProductWithCategoryDto>(product));
        }
        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var newProduct = await _productService.AddAsync(_mapper.Map<Product>(productDto));//Client tarafında gelen DTO, Entity'e cevrilerek db'ye kayit islemi yapiliyor.
            return Created(string.Empty, _mapper.Map<ProductDto>(newProduct));
        }
        [HttpPut]
        public IActionResult Update(ProductDto productDto)
        {
            if (productDto.Id==0)
            {
                throw new Exception("Id alanı gereklidir");
            }
            var product = _productService.Update(_mapper.Map<Product>(productDto));
            return NoContent();
        }
        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpDelete("{id}")]
        public IActionResult Remove(int Id)
        {
            var product = _productService.GetByIdAsync(Id).Result;
            _productService.Remove(product);
            return NoContent();
        }


    }
}
