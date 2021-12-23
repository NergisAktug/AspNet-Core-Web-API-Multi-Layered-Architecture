using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Web.DTOs
{
    public class CategoryWithProductDto:CategoryDto
    {
        public IEnumerable<ProductDto> Products { get; set; }//Category Entity'sindeki Products ile aynı olmalıdır.
    }
}
