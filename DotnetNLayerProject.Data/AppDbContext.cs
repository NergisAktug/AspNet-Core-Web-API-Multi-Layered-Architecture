using DotnetNLayerProject.Core.Models;
using DotnetNLayerProject.Data.Configurations;
using DotnetNLayerProject.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetNLayerProject.Data
{
   public class AppDbContext:DbContext
    {
        //options hangi DB'yi kullanacagınızı ifade eder.Bunu startup'da belirtmemiz gerekiyor
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)//base ile DbContext'in constructoruna gonderilir.
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Burada amac category ve product classlarımızı tabloya donustururken nasıl donusucek yani,buradaki propertyler tabloya donusurken uzunlugu ne olacak
            //Tablolar olusurken surunlar ne olacagı burada belirtilir
            // modelBuilder.Entity<Product>().Property(x=>x.Id) Bunlar burayada yazılabilirdi ama burayı temiz tutmak amacıyla bu kodlar herbir tablo icin ilgili configuration dosyasında yapıldı

            modelBuilder.ApplyConfiguration(new ProductConfiguration());//olusturulan configurasyon dosyalarını burada bir obje olusturarak yapılan ayarların olusmasını saglıyoruz.
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());


            modelBuilder.ApplyConfiguration(new ProductSeed(new int[] {1,2 }));
            modelBuilder.ApplyConfiguration(new CategorySeed(new int[] {1,2}));

            modelBuilder.Entity<Person>().HasKey(X => X.Id);
            modelBuilder.Entity<Person>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Person>().Property(x => x.Name).HasMaxLength(100);
            modelBuilder.Entity<Person>().Property(x => x.SurName).HasMaxLength(100);


        }


    }
}
