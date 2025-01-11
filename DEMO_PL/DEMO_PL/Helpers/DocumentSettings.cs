using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace DEMO_PL.Helpers
{
    public static class DocumentSettings
    {
        public static async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            // 1- get located folder path

            //string folderPath = Directory.GetCurrentDirectory() + @"\wwwroot\files\" ;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            
            // 2- get file name and make it unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3- get file path

            string filePath = Path.Combine(folderPath, fileName);

            //4- save file as streams : [Data Per Time]

           using var fs = new FileStream(filePath, FileMode.Create);
          
            await file.CopyToAsync(fs);

           return fileName;

        }
        public static async Task DeleteFileAsync(string fileName, string folderName)
        {
            if (fileName is not null && folderName is not null)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }
    }
}
