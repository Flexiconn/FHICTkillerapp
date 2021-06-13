using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logic
{
    public static class FileControl
    {
        public static string AddFileToSystem(IFormFile file, string path) {
            
            if (file != null)
            {
                string pathString = System.IO.Path.Combine("wwwroot/data/IMG/", path);
                System.IO.Directory.CreateDirectory(pathString);
                file.CopyTo(new FileStream(System.IO.Path.Combine(pathString, file.FileName.ToString()), FileMode.Create));
                return (pathString + file.FileName.ToString());
            }
            return null;
        }
    }
}
