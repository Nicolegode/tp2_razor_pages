using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CidadesTuristicas.Pages.CityManager
{
    public class CreateCityModel : PageModel
    {
        [BindProperty]
        public string CityName { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(CityName))
            {
                ModelState.AddModelError(nameof(CityName), "O nome da cidade é obrigatório.");
                return Page();
            }

            return Page();
        }
    }
}