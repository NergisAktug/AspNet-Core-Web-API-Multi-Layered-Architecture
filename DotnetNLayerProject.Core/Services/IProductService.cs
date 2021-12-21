using DotnetNLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Core.Services
{
    public interface IProductService:IService<Product>
    {
        Task<Product> GetWithCategoryByIdAsync(int productId);
        //bool ControlInnerBarcode(Product product);



    }
}
