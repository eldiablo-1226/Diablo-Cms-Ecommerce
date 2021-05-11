using System.IO;
using System.Threading.Tasks;
using DiabloCms.Shared;

namespace DiabloCms.UseCases.Contracts.File
{
    public interface IFilesManagerService
    {
        string BaseStaticPath { get; }
        string ProductPhotoPath { get; }
        Task<Result<string>> UploadPhoto(Stream photo, string fileName);
        Task<string> UploadFileAsync(Stream file, string baseDirectory, string fileName);
        Task DeleteFileAsync(string url);
    }
}