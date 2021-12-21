using DotnetNLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNLayerProject.API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} alanı gereklidir")]
        public string Name { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="{0} alabı 1'den büyük bir deger olmalıdır")]
        public int Stock { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "{0} alabı 1'den büyük bir deger olmalıdır")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
      
    }
}
