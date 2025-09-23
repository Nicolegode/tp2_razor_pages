using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using CidadesTuristicas.Models;

namespace CidadesTuristicas.Pages.CityManager
{
    public class CreateCountryModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public List<Country> Countries { get; set; } = new List<Country>();

        public class InputModel
        {
            [Required(ErrorMessage = "Nome obrigatório")]
            [Display(Name = "Nome do País")]
            public string CountryName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Código obrigatório")]
            [StringLength(2, MinimumLength = 2, ErrorMessage = "O código deve ter exatamente 2 caracteres (ex: BR)")]
            [Display(Name = "Código do País")]
            public string CountryCode { get; set; } = string.Empty;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(Input.CountryName) && !string.IsNullOrEmpty(Input.CountryCode))
            {
                var firstLetterName = Input.CountryName.Trim().ToUpper().FirstOrDefault();
                var firstLetterCode = Input.CountryCode.Trim().ToUpper().FirstOrDefault();

                if (firstLetterName != firstLetterCode)
                {
                    ModelState.AddModelError(nameof(Input.CountryCode), 
                        $"O código do país deve começar com a mesma letra do nome. Nome começa com '{firstLetterName}', código deve começar com '{firstLetterName}'.");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var country = new Country
            {
                CountryName = Input.CountryName.Trim(),
                CountryCode = Input.CountryCode.Trim().ToUpper()
            };

            Countries.Add(country);
            
            Input = new InputModel();
            
            TempData["SuccessMessage"] = $"País '{country.CountryName}' ({country.CountryCode}) cadastrado com sucesso!";

            return Page();
        }
    }
}