using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CidadesTuristicas.Pages.CityManager
{
    public class CityListModel : PageModel
    {
        public List<string> Cities { get; set; } = new List<string>();

        public void OnGet()
        {
            // Lista de cidades conforme solicitado
            Cities = new List<string> { "Rio", "São Paulo", "Brasília" };
        }
    }
}