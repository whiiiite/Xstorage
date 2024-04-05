using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Xstorage.Entities.Models;
using Xstorage.Mixins;

namespace Xstorage.Shared
{
    public class DirectoryHelper
    {
        private static string StandartPathToFolders
        {
            get
            {
                return "C:\\";
            }
        }

        /// <summary>
        /// Creates folder for store user storages
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Absolute Path to folder</returns>
        public static string CreateUserFolder()
        {
            string path;
            do
            {
                path = Path.Combine(StandartPathToFolders, Consts.UserFolders, Generator.RandomString(64));
            }
            while (Directory.Exists(path));

            Directory.CreateDirectory(path);

            return path;
        }

        /// <summary>
        /// Creates folder for user's storage on server
        /// </summary>
        /// <param name="userFolderPath"></param>
        /// <param name="storageName"></param>
        public static void CreateUserFolderStorage(string path)
        {
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Retrieve data of items of file system folders and files
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static IEnumerable<FileSystemItemData> GetDataOfItems(string dirPath)
        {
            DirectoryInfo dirinfo = new DirectoryInfo(dirPath);

            IEnumerable<DirectoryInfo> dirs = dirinfo.GetDirectories();
            IEnumerable<FileInfo> files = dirinfo.GetFiles();

            FileSystemItemData[] names = new FileSystemItemData[dirs.Count() + files.Count()];
            int indexer = 0;
            foreach (var item in dirs)
            {
                names[indexer] = new FileSystemItemData
                {
                    Name = item.Name,
                    Path = item.FullName,
                    LastEditDate = item.LastWriteTime,
                    Size = 0,
                    IsDirectory = true
                };
                indexer++;
            }

            foreach (var item in files)
            {
                names[indexer] = new FileSystemItemData
                {
                    Name = item.Name,
                    Path = item.FullName,
                    LastEditDate = item.LastWriteTime,
                    Size = item.Length,
                    IsDirectory = false
                };
                indexer++;
            }

            return names;
        }


        /// <summary>
        /// Creates folder/directory in storage by specific path
        /// </summary>
        /// <returns>Path of created directory</returns>
        public static string CreateDirectoryInStorage(string path, string dirName)
        {
            string newFolderPath = Path.Combine(path, dirName);
            newFolderPath = GetUniqueDirectoryName(newFolderPath);
            Directory.CreateDirectory(newFolderPath);
            return newFolderPath;
        }


        public static string GetUniqueDirectoryName(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                int count = 1;
                while (Directory.Exists(dirPath + $"({count})"))
                {
                    count++;
                }
                dirPath += $"({count})";
            }

            return dirPath;
        }
    }
}
