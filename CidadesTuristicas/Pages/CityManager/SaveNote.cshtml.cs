using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CidadesTuristicas.Pages.CityManager
{
    public class SaveNoteModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public SaveNoteModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string? FileName { get; set; }
        public bool FileSaved { get; set; } = false;

        public class InputModel
        {
            [Required(ErrorMessage = "O conteúdo é obrigatório")]
            [Display(Name = "Conteúdo da Nota")]
            public string Content { get; set; } = string.Empty;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Gera nome do arquivo com timestamp
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                FileName = $"note-{timestamp}.txt";

                // Caminho completo do arquivo
                var filePath = Path.Combine(_environment.WebRootPath, "files", FileName);

                // Garante que a pasta existe
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory!);
                }

                // Escreve o conteúdo no arquivo
                await System.IO.File.WriteAllTextAsync(filePath, Input.Content);

                FileSaved = true;
                
                // Limpa o formulário após salvar
                Input = new InputModel();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao salvar arquivo: {ex.Message}");
            }

            return Page();
        }
    }
}