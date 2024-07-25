using RingoMedia.DataAccess.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace RingoMedia.UI.Services
{
    public class UploadService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public UploadService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string UploadFile(IFormFile file, string folderName)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid()}{extension}";

            var uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, folderName);
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            // Combine the path with the filename
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Path.Combine("upload", fileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
