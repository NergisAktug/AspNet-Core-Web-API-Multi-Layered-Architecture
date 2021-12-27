using DotnetNLayerProject.Web.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotnetNLayerProject.Web.ApiService
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;
        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /*
         *      public static string ToJson(this object obj)
                {
                var item = JsonConvert.SerializeObject(obj); //Gelen veriyi obje'den jsona cevirir
                return item;
                }
         
         
                public static T ToObject(this string item)
                {
                var obj = JsonConvert.DeserializeObject(item);//Gelen veriyi json'dan objeye cevirme
                return obj;
                }
         
         */
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            IEnumerable<CategoryDto> categoryDtos;

            var response = await _httpClient.GetAsync("categories");
            /*200 ila baslayanlar basarılı durum kodları donenler
             *300 ile baslayanlar yonlendirme durum kodları
             *400 ile baslayan Client tarafında yapılan hataları ifade eder.
             *500 ile baslayanlar server hataları
             */
            if (response.IsSuccessStatusCode)//api'ler geriye bir http durum kodu dönüyor.200 ila baslayan bir durum kodu donuyorsa api'den, bu istek basarılı demek
            {
                categoryDtos = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync());//jsonda'dan objeye cevirme 
            }
            else
            {
                categoryDtos = null;
            }
            return categoryDtos;
        }


        public async Task<CategoryDto> AddAsync(CategoryDto categoryDto)
        {
            //Encoding.UTF8, json'nın hangi formatta encoding yapılacagı belirtir, "application/json" ise json'nın veri tipini gosterir
            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto),Encoding.UTF8,"application/json");//gelen verileri categoryDto'a cevirmeye yarar
            var response = await _httpClient.PostAsync("categories", stringContent);
            if (response.IsSuccessStatusCode)
            {
                categoryDto = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());//response'dan donen datayı okuyarak CategoryDto'ya donusturuyor
                return categoryDto;
            }
            else
            {
                //loglama yap
                return null;
            }
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }


        public async Task<bool> Update(CategoryDto categoryDto)
        {
            //Yapılan ilk islem categoryDto nesenesini api'ye gondermektir.
            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("categories", stringContent);
            if (response.IsSuccessStatusCode)
            {
                categoryDto = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<bool> Remove(int id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
