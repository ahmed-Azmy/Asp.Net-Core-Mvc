using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName);

            var FileName = $"{Guid.NewGuid()}{file.FileName}";

            var FilePath = Path.Combine(FolderPath, FileName);

            using var stream = new FileStream(FilePath, FileMode.Create);

            file.CopyTo(stream);

            return FileName;
        }

        public static void DeleteFile(string fileName , string folderName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName, fileName);

            if(File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}
