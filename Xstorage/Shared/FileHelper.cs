using System.Text.RegularExpressions;

namespace Xstorage.Shared
{
    public static class FileHelper
    {
        /// <summary>
        /// Creates file in storage by specific path
        /// </summary>
        /// <param name="userFolderPath"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns>Path of created file</returns>
        public static string CreateFileInStorage(string path, string fileName)
        {
            string filePath = Path.Combine(path, fileName);
            fileName = GetUniqueFileName(filePath);
            filePath = Path.Combine(path, fileName);
            File.Create(filePath).Close();
            return filePath;
        }

        /// <summary>w
        /// Finds unique file name and return it
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetUniqueFileName(string filePath)
        {
            string newFileName = Path.GetFileName(filePath);
            if (File.Exists(filePath))
            {
                string folderPath = Path.GetDirectoryName(filePath)!;
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string fileExtension = Path.GetExtension(filePath);
                int number = 1;

                Match regex = Regex.Match(fileName, @"^(.+) \((\d+)\)$");

                if (regex.Success)
                {
                    fileName = regex.Groups[1].Value;
                    number = int.Parse(regex.Groups[2].Value);
                }

                do
                {
                    newFileName = $"{fileName}({number}){fileExtension}";
                    filePath = Path.Combine(folderPath, newFileName);
                    number++;
                }
                while (File.Exists(filePath));
            }

            return newFileName;
        }
    }
}
