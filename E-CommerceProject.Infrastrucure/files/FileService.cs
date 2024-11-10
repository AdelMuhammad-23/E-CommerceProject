﻿using E_CommerceProject.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace E_CommerceProject.Infrastructure.files
{

    public class FileService : IFileService
    {
        private readonly IAppEnvironment _appEnvironment;

        public FileService(IAppEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public async Task<string> UploadImage(string location, IFormFile image)
        {
            //Get Location
            var path = _appEnvironment.WebRootPath + "/" + location + "/";

            #region Check Extension
            var extension = Path.GetExtension(image.FileName);
            var allowExtensions = new string[] { ".JPG", ".PNG", ".SVG" };
            var extensionComparer = allowExtensions.Contains(extension, StringComparer.InvariantCultureIgnoreCase);
            if (!extensionComparer)
                return "this extension is not allowed";
            #endregion

            #region Check Image Size
            var allowedSize = image.Length is > 0 and < 4_000_000;
            if (!allowedSize)
                return "this image is too big";
            #endregion

            #region Store Image
            var newFileName = $"{Guid.NewGuid().ToString().Replace("-", string.Empty)}{extension}";
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream filestreem = File.Create(path + newFileName))
                {
                    await image.CopyToAsync(filestreem);
                    await filestreem.FlushAsync();
                    return $"/{location}/{newFileName}";
                }
            }
            catch (Exception)
            {
                return "FailedToUploadImage";
            }
            #endregion
        }


    }
}
