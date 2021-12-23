using AutoMapper;
using DotnetNLayerProject.Core.Models;
using DotnetNLayerProject.Core.Services;
using DotnetNLayerProject.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public async  Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(_mapper.Map<IEnumerable<ProductDto>>(products));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            await _productService.AddAsync(_mapper.Map<Product>(productDto));
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var newproduct = await _productService.GetByIdAsync(id);

            return View(_mapper.Map<ProductDto>(newproduct));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
             _productService.Update(_mapper.Map<Product>(productDto));
            return RedirectToAction("Index");
        }
    }
}
