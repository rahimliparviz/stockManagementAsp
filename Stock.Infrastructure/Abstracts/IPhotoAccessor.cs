using Microsoft.AspNetCore.Http;

namespace Stock.Infrastructure.Abstracts
{
    public interface IPhotoAccessor
    {
        string Add(string folder, IFormFile file);

        void Delete(string filePath);
    }
}