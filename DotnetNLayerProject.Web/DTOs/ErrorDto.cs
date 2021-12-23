using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Web.DTOs
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new List<string>();
        }
        public List<String> Errors { get; set; }//Birden fazla hata meydana gelebileceği için tipi String olan bir List donülmektedir
        public int Status { get; set; }
    }
}
