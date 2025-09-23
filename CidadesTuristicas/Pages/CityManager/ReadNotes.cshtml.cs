using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CidadesTuristicas.Pages.CityManager
{
    public class ReadNotesModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public ReadNotesModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public List<FileInfo> Files { get; set; } = new List<FileInfo>();
        public string? SelectedFileName { get; set; }
        public string? FileContent { get; set; }
        public DateTime? FileCreatedDate { get; set; }
        public long? FileSize { get; set; }

        public void OnGet(string? fileName = null)
        {
            LoadFiles();

            if (!string.IsNullOrEmpty(fileName))
            {
                LoadFileContent(fileName);
            }
        }

        private void LoadFiles()
        {
            var filesPath = Path.Combine(_environment.WebRootPath, "files");
            
            if (Directory.Exists(filesPath))
            {
                var directoryInfo = new DirectoryInfo(filesPath);
                Files = directoryInfo.GetFiles("*.txt")
                                   .OrderByDescending(f => f.CreationTime)
                                   .ToList();
            }
        }

        private void LoadFileContent(string fileName)
        {
            try
            {
                var filePath = Path.Combine(_environment.WebRootPath, "files", fileName);
                
                if (System.IO.File.Exists(filePath))
                {
                    SelectedFileName = fileName;
                    FileContent = System.IO.File.ReadAllText(filePath);
                    
                    var fileInfo = new FileInfo(filePath);
                    FileCreatedDate = fileInfo.CreationTime;
                    FileSize = fileInfo.Length;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao ler arquivo: {ex.Message}");
            }
        }
    }
}