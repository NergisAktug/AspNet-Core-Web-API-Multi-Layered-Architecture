﻿using DotnetNLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Core.Services
{
   public interface IService<TEntity> where TEntity:class
    {
        Task<TEntity> GetByIdAsync(int Id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);//Find(x=>x.id==Id) seklinde bir ifade kullanabilmek icin bunun Expression Func olması lazım       
        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);//Herhangi bir parametreye gore don
        //category.SingleOrDefault(x=>x.name="kalem"); ifadesi ismi kalem olan sadece bir tane deger donecek,TEntity buradaki x'e tekabul ediyor x'de category'ye tekabul ediyor, gonderilen sorgunun db'de olup olması icin donen deger de bool'a denk geliyor
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);//Toplu ekleme islemi
        void Remove(TEntity entity);//Silme
        void RemoveRange(IEnumerable<TEntity> entities);//Toplu silme islemi
        TEntity Update(TEntity entity);
    }
}
