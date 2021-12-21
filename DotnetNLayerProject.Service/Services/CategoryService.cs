using DotnetNLayerProject.Core.Models;
using DotnetNLayerProject.Core.Repositories;
using DotnetNLayerProject.Core.Services;
using DotnetNLayerProject.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IRepository<Category> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<Category> GetWithProductsByIdAsync(int categoryId)
        {
          return await _unitOfWork.categories.GetWithProductsByIdAsync(categoryId);
        }
    }
}
