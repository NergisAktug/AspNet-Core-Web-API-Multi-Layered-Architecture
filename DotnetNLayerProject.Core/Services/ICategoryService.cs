using DotnetNLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Core.Services
{
   public interface ICategoryService:IService<Category>
    {
        Task<Category> GetWithProductsByIdAsync(int categoryId);

        //Category özgü methodlarınız varsa burada tanımlayabiliriz.

    }
}
