using DotnetNLayerProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Core.UnitOfWorks
{
   public interface IUnitOfWork
    {
        IProductRepository Products { get;}//Best Practices açısından Repositoryler burada cagrilir
        ICategoryRepository categories { get;}
        Task CommitAsync();//İmplemente edildigi zaman Entity Framework tarafında saveChange methodunu cagiracak.
        void Commit();
    }
}
