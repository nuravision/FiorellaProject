using Microsoft.AspNetCore.Http;

namespace Fiorella.Helpers.Extentions
{
    public static class FileExtentions
    {
        public static bool CheckFileSize(this IFormFile file ,int size)
        {
            return file.Length/1024<size;
        }
        public static bool CheckFileType(this IFormFile file, string pattern) { 
            return file.ContentType.Contains(pattern);
        }
        public async static Task SaveFileToLocalAsync(this IFormFile file, string path) { 
            using(FileStream stream =new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
        public static void DeleteFileFromLocal(this string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
