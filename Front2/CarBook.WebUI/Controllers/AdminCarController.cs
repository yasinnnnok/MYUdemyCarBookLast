using CarBook.Dto.BrandDtos;
using CarBook.Dto.CarDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;


namespace CarBook.WebUI.Controllers
{
    public class AdminCarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client =_httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7060/api/Cars/GetCarWithBrand");
            if (responseMessage.IsSuccessStatusCode) 
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDtos>>(jsonData);
                return View(values);

            }

            return View();
        }

        [HttpGet]
        public  async Task<IActionResult> CreateCar()
        {
            //clint oluştur . GetASync ile istek at
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7060/api/Brands");
            //Geşen contenti stringe oluştur
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);


            List<SelectListItem> brandValues = (from x in values
                                                select new SelectListItem
                                                {
                                                    Text=x.name,
                                                    Value = x.brandID.ToString()
                                                }).ToList();

            ViewBag.BrandValues = brandValues;
            return View();
        }




    }
}
