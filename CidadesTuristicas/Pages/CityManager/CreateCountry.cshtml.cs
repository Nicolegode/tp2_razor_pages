using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using CidadesTuristicas.Models;

namespace CidadesTuristicas.Pages.CityManager
{
    public class CreateCountryModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public Country? CreatedCountry { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Nome obrigatório")]
            public string CountryName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Código obrigatório")]
            [StringLength(2, MinimumLength = 2, ErrorMessage = "O código deve ter exatamente 2 caracteres (ex: BR)")]
            public string CountryCode { get; set; } = string.Empty;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CreatedCountry = new Country
            {
                CountryName = Input.CountryName,
                CountryCode = Input.CountryCode
            };

            Input = new InputModel();
            return Page();
        }
    }
}