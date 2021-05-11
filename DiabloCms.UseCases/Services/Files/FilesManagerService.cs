using System;
using System.IO;
using System.Threading.Tasks;
using DiabloCms.Shared;
using DiabloCms.UseCases.Contracts.File;
using HarabaSourceGenerators.Common.Attributes;

namespace DiabloCms.UseCases.Services.Files
{
    [Inject]
    public partial class FilesManagerService : IFilesManagerService
    {
        public string BaseStaticPath => "wwwroot";
        public string ProductPhotoPath => "ProductPhoto";

        public async Task<Result<string>> UploadPhoto(Stream photo, string fileName)
        {
            try
            {
                var result = await UploadFileAsync(photo, ProductPhotoPath, fileName);
                return Result<string>.SuccessWith(result);
            }
            catch (Exception)
            {
                return Result<string>.FailureParams("Cannot save file");
            }
        }

        public async Task<string> UploadFileAsync(Stream file, string baseDirectory, string fileName)
        {
            var path = Path.Combine(
                Environment.CurrentDirectory,
                BaseStaticPath,
                baseDirectory);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, fileName);

            if (File.Exists(path)) return "File already exists";

            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream)
                .ConfigureAwait(false);

            return Path.Combine(baseDirectory, fileName);
        }

        public Task DeleteFileAsync(string url)
        {
            return Task.Run(() =>
            {
                var path = Path.Combine(Environment.CurrentDirectory, BaseStaticPath, url);

                if (!File.Exists(path)) throw new Exception("File Not Found");

                var file = new FileInfo(path);
                file.Delete();
            });
        }
    }
}