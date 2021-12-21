using DotnetNLayerProject.Core.Repositories;
using DotnetNLayerProject.Core.UnitOfWorks;
using DotnetNLayerProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Data.UnitOfWorks
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;
        /*
         eger _productRepository adında bir deger varsa bunu _productRepository adında bir degiskene ata, _productRepository'nı da Products objesine ata,
         eger _productRepository adında bir deger yoksa o zaman yeni bir baglantı yarat bunuda yine IProductRepository interfacesine at
         ProductRepository, IProductRepository'yi implemente ettiği için dönüş tipleri arasında IProductRepository'da var.
         */
        public IProductRepository Products => _productRepository=_productRepository ?? new ProductRepository(_context);//??(2 tane soru işareti) eger null ise

        public ICategoryRepository categories => _categoryRepository=_categoryRepository ?? new CategoryRepository(_context);
        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }


        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
