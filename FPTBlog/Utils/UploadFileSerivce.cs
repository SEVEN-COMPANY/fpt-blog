using System;
using System.IO;
using FPTBlog.Utils.Interface;
using Microsoft.AspNetCore.Http;

namespace FPTBlog.Utils {
    public class UploadFileService : IUploadFileService {
        readonly string pathUrl = "/public/image/";
        readonly string folderUrl = "/wwwroot/public/image/";
        public static string[] imageExtension = { "png", "jpg", "jpeg" };
        public bool CheckFileExtension(IFormFile file, string[] extensions) {
            bool result = false;
            string fileExtension = file.FileName.ToLower().Split(".")[file.FileName.ToLower().Split(".").Length - 1];
            foreach (string extension in extensions) {
                if (extension == fileExtension) {
                    result = true;
                }
            }
            return result;
        }

        public bool CheckFileSize(IFormFile file, int limit) {

            // Unit: MB
            return file.Length < limit * 1024 * 1024;
        }

        public string Upload(IFormFile file) {
            string formatFolderUrl = "." + folderUrl;
            string fileExtension = file.FileName.ToLower().Split(".")[file.FileName.ToLower().Split(".").Length - 1];
            string fortmatFileName = System.Guid.NewGuid().ToString() + "." + fileExtension;

            try {
                if (!Directory.Exists(formatFolderUrl)) {
                    Directory.CreateDirectory(formatFolderUrl);
                }

                using (FileStream fileStream = System.IO.File.Create(formatFolderUrl + fortmatFileName)) {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                return pathUrl + fortmatFileName;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
