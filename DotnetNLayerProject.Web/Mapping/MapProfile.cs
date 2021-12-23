using AutoMapper;
using DotnetNLayerProject.Core.Models;
using DotnetNLayerProject.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Web.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryDto>();//Category gordugun an CategoryDTO'ya donustur.
            CreateMap<CategoryDto, Category>();//CategoryDto'yu gordugun an Category'ye donustur
            CreateMap<Category, CategoryWithProductDto>();
            CreateMap<CategoryWithProductDto, CategoryDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductWithCategoryDto, Product>();
            CreateMap<Product, ProductWithCategoryDto>();

            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();
        }
    }
}
