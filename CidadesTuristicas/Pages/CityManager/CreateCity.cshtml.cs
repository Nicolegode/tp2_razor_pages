using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CidadesTuristicas.Pages.CityManager
{
    public class CreateCityModel : PageModel
    {
        public string? CityName { get; set; }
        
        public void OnGet()
        {
        }

        public IActionResult OnPost(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                ModelState.AddModelError("cityName", "O nome da cidade é obrigatório.");
                return Page();
            }

            CityName = cityName;
            
            return Page();
        }
    }
}