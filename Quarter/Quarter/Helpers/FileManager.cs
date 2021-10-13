using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Helpers
{
    public class FileManager
    {
        public static string Save(string rootPath, string folder, IFormFile file)
        {
            string fileName = file.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            string newFileName = Guid.NewGuid().ToString() + fileName;

            string path = Path.Combine(rootPath, folder, newFileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return newFileName;
        }

        public static bool Delete(string rootPath, string folder, string fileName)
        {
            string deletePath = Path.Combine(rootPath, folder, fileName);

            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
                return true;
            }
            return false;
        }
    }
}
