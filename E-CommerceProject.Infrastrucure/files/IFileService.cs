using Microsoft.AspNetCore.Http;

namespace E_CommerceProject.Infrastructure.files
{
    public interface IFileService
    {
        public Task<string> UploadImage(string location, IFormFile image);
        public bool DeleteImage(string location, string imagePath);

    }
}
