using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using CidadesTuristicas.Models;

namespace CidadesTuristicas.Pages.CityManager
{
    public class CreateCountryModel : PageModel
    {
        [BindProperty]
        public List<InputModel> Countries { get; set; } = new List<InputModel>();

        public List<Country> CreatedCountries { get; set; } = new List<Country>();
        public string SuccessMessage { get; set; } = string.Empty;

        public class InputModel
        {
            [Required(ErrorMessage = "O Nome é obrigatório.")]
            [Display(Name = "Nome do País")]
            public string CountryName { get; set; } = string.Empty;

            [Required(ErrorMessage = "O Código é obrigatório.")]
            [StringLength(2, MinimumLength = 2, ErrorMessage = "O Código deve ter exatamente 2 caracteres (ex: BR).")]
            [Display(Name = "Código")]
            public string CountryCode { get; set; } = string.Empty;
        }

        public void OnGet()
        {
            // Gera dinamicamente 5 linhas no formulário
            for (int i = 0; i < 5; i++)
            {
                Countries.Add(new InputModel());
            }
        }

        public IActionResult OnPost()
        {
            // Remove erros de validação para países completamente vazios
            for (int i = 0; i < Countries.Count; i++)
            {
                var country = Countries[i];
                if (string.IsNullOrWhiteSpace(country.CountryName) && 
                    string.IsNullOrWhiteSpace(country.CountryCode))
                {
                    ModelState.Remove($"Countries[{i}].CountryName");
                    ModelState.Remove($"Countries[{i}].CountryCode");
                }
            }

            // Filtra países preenchidos
            var filledCountries = Countries.Where(c => 
                !string.IsNullOrWhiteSpace(c.CountryName) || 
                !string.IsNullOrWhiteSpace(c.CountryCode)).ToList();

            // Verifica se pelo menos um país foi preenchido
            if (!filledCountries.Any())
            {
                ModelState.AddModelError(string.Empty, "Pelo menos um país deve ser preenchido.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Cria os países válidos
            CreatedCountries = filledCountries
                .Where(c => !string.IsNullOrWhiteSpace(c.CountryName) && 
                           !string.IsNullOrWhiteSpace(c.CountryCode))
                .Select(c => new Country
                {
                    CountryName = c.CountryName.Trim(),
                    CountryCode = c.CountryCode.Trim().ToUpper()
                })
                .ToList();

            SuccessMessage = $"Países cadastrados com sucesso: {CreatedCountries.Count} registro(s).";

            // Limpa o formulário para novo cadastro
            Countries.Clear();
            for (int i = 0; i < 5; i++)
            {
                Countries.Add(new InputModel());
            }

            return Page();
        }
    }
}