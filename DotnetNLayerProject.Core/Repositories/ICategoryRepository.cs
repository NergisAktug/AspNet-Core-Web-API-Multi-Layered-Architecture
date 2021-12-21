using DotnetNLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Core.Repositories
{
   public interface ICategoryRepository:IRepository<Category>
    {
        //Sadece bir modele(entitye) ait bir islem yaptırmak istiyorsak bu sekilde IRepository'e ait interface tanımlayabiliriz.

        Task<Category> GetWithProductsByIdAsync(int categoryId);


    }
}
