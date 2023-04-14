namespace StudentEnrollment.Api.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FileUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string UploadFile(byte[] file, string imageName)
        {
            if (file == null)
            {
                return string.Empty;
            }
            var folderPath = "studentpictures";
            var url = httpContextAccessor.HttpContext?.Request.Host.Value;
            var ext = Path.GetExtension(imageName);
            var fileName = $"{Guid.NewGuid()}{ext}";

            var path = $"{webHostEnvironment.WebRootPath}\\{folderPath}\\{fileName}";
            UploadImage(file, path);
            return $"https://{url}/{folderPath}/{fileName}";
        }

        private void UploadImage(byte[] fileBytes, string path)
        {
            FileInfo file = new FileInfo(path);
            file?.Directory?.Create();
            var fileStream = file?.Create();
            fileStream?.Write(fileBytes, 0, fileBytes.Length);
            fileStream?.Close();
        }
    }
}
