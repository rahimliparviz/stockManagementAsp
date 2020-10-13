using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Stock.Infrastructure.Abstracts;

namespace Stock.Infrastructure.Concrete
{
    public class PhotoAccessor:IPhotoAccessor
    {
        private readonly IWebHostEnvironment _env;

        public PhotoAccessor(IWebHostEnvironment env)
        {
            _env = env;
        }
        public string Add(string folderName, IFormFile file)
        {
            if (!Directory.Exists(_env.WebRootPath+"\\"+folderName+"\\"))
            {
                Directory.CreateDirectory(_env.WebRootPath + "\\"+folderName+"\\");
            }
            // string path = _env.WebRootPath+"\\"+folderName+"\\" + Guid.NewGuid().ToString() +file.FileName;
            string path = folderName+"\\" + Guid.NewGuid().ToString() +file.FileName;
            using (var fileStream = new FileStream( _env.WebRootPath+"\\"+path,FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return path;
        }

        public void Delete(string path)
        {
            string filePath = _env.WebRootPath + "\\" + path;
            if ((System.IO.File.Exists(filePath)))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}