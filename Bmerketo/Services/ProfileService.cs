using Microsoft.Extensions.Hosting;

namespace Bmerketo.Services
{
    public class ProfileService
    {
        private readonly IWebHostEnvironment _environment;
      
        public ProfileService(IWebHostEnvironment environment)
        {
            _environment = environment;
            
        }

        public async Task<string> UploadProfileImageAsync(IFormFile profilImage)
        {
            var wwwroot = $"{_environment.WebRootPath}/Image/Profile";
            var imageName = $"profile_{Guid.NewGuid()}{Path.GetExtension(profilImage.FileName)}";
            string filePath = $"{wwwroot}/{imageName}";

            using var fs = new FileStream(filePath, FileMode.Create);
            await profilImage.CopyToAsync(fs);
            return imageName;
        }
        //public async Task<string> DeleteProfileImageAsync(Guid id)
        //{
        //    //Delete image from wwwroot
        //    var deleteimage = await _userService.GetByIdAsync(id);
        //    var imagePath = Path.Combine(_environment.WebRootPath, "image/product", deleteimage.ImageName);
        //    if (System.IO.File.Exists(imagePath))
        //        System.IO.File.Delete(imagePath);
        //    return imagePath;
        //}
    }
}
